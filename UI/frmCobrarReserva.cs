using BE;
using BLL;
using Servicios;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class frmCobrarReserva : Form, IObservadorIdioma
    {
        private readonly CobroReservaBLL _cobroBLL;
        private readonly ClienteBE _cliente;
        private readonly CanchaBE _cancha;
        private readonly TarifaBE _tarifa;
        private readonly FacturaBE _factura;

        public FacturaBE? FacturaPagada { get; private set; }
        public PagoBE? PagoAprobado { get; private set; }

        public frmCobrarReserva(
            ClienteBE cliente,
            CanchaBE cancha,
            TarifaBE tarifa,
            DateTime fechaReserva,
            TimeSpan horario,
            int cantidadPaletas,
            int cantidadPelotas,
            decimal importeEquipamiento)
        {
            InitializeComponent();

            _cobroBLL = new CobroReservaBLL();
            _cliente = cliente;
            _cancha = cancha;
            _tarifa = tarifa;

            _factura = _cobroBLL.GenerarFactura(
                cliente,
                cancha,
                tarifa,
                fechaReserva,
                horario,
                cantidadPaletas,
                cantidadPelotas,
                importeEquipamiento);

            ActualizarIdioma();
            MostrarFactura();
        }

        private void frmCobrarReserva_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);

            dtpVencimiento.Format = DateTimePickerFormat.Custom;
            dtpVencimiento.CustomFormat = "MM/yy";
            dtpVencimiento.ShowUpDown = true;
            dtpVencimiento.MinDate = DateTime.Today;
            dtpVencimiento.Value = DateTime.Today.AddYears(1);

            txtBanco.Focus();
        }

        private void frmCobrarReserva_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmCobrarReserva.Title");
            lblTitulo.Text = t.Traducir("frmCobrarReserva.LblTitulo");
            gbFactura.Text = t.Traducir("frmCobrarReserva.GbFactura");
            gbTarjeta.Text = t.Traducir("frmCobrarReserva.GbTarjeta");

            lblClienteTitulo.Text = t.Traducir("frmCobrarReserva.LblCliente");
            lblTurnoTitulo.Text = t.Traducir("frmCobrarReserva.LblTurno");
            lblCanchaTitulo.Text = t.Traducir("frmCobrarReserva.LblCancha");
            lblTarifaTitulo.Text = t.Traducir("frmCobrarReserva.LblTarifa");
            lblEquipamientoTitulo.Text = t.Traducir("frmCobrarReserva.LblEquipamiento");
            lblTotalTitulo.Text = t.Traducir("frmCobrarReserva.LblTotal");

            lblBanco.Text = t.Traducir("frmCobrarReserva.LblBanco");
            lblNumeroTarjeta.Text = t.Traducir("frmCobrarReserva.LblNumeroTarjeta");
            lblVencimiento.Text = t.Traducir("frmCobrarReserva.LblVencimiento");
            lblCodigoSeguridad.Text = t.Traducir("frmCobrarReserva.LblCodigoSeguridad");
            lblRespuestaBanco.Text = t.Traducir("frmCobrarReserva.LblRespuestaBanco");
            CargarRespuestasBanco();

            btnCobrar.Text = t.Traducir("frmCobrarReserva.BtnCobrar");
            btnVolver.Text = t.Traducir("frmCobrarReserva.BtnVolver");
        }

        private void CargarRespuestasBanco()
        {
            int seleccionAnterior = cboRespuestaBanco.SelectedIndex;

            cboRespuestaBanco.Items.Clear();
            cboRespuestaBanco.Items.Add(
                Traductor.Instancia.Traducir("frmCobrarReserva.RespuestaAprobada"));
            cboRespuestaBanco.Items.Add(
                Traductor.Instancia.Traducir("frmCobrarReserva.RespuestaRechazada"));

            cboRespuestaBanco.SelectedIndex =
                seleccionAnterior >= 0 ? seleccionAnterior : -1;
        }

        private void MostrarFactura()
        {
            lblClienteValor.Text =
                $"{_cliente.DNI} - {_cliente.Nombre} {_cliente.Apellido}";

            lblTurnoValor.Text =
                _factura.FechaReserva.ToString("d") + " " +
                _factura.Horario.ToString(@"hh\:mm");

            lblCanchaValor.Text = _cancha.Nombre;
            lblTarifaValor.Text =
                $"{_tarifa.TipoTarifa} - {_factura.ImporteTarifa:C}";

            lblEquipamientoValor.Text =
                $"Paletas: {_factura.CantidadPaletas} | " +
                $"Pelotas: {_factura.CantidadPelotas} | " +
                $"{_factura.ImporteEquipamiento:C}";

            lblTotalValor.Text = _factura.ImporteTotal.ToString("C");
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void Campo_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            lblMensaje.Text = string.Empty;
        }

        private bool ValidarUI()
        {
            errorProvider.Clear();
            lblMensaje.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtBanco.Text))
            {
                MostrarError(txtBanco, "Errores.Cobro.BancoObligatorio");
                return false;
            }

            string tarjeta =
                Regex.Replace(txtNumeroTarjeta.Text ?? string.Empty, @"[\s-]", "");

            if (!Regex.IsMatch(tarjeta, @"^\d{13,19}$"))
            {
                MostrarError(txtNumeroTarjeta, "Errores.Cobro.TarjetaInvalida");
                return false;
            }

            if (!Regex.IsMatch(txtCodigoSeguridad.Text.Trim(), @"^\d{3,4}$"))
            {
                MostrarError(
                    txtCodigoSeguridad,
                    "Errores.Cobro.CodigoSeguridadInvalido");
                return false;
            }

            if (cboRespuestaBanco.SelectedIndex < 0)
            {
                MostrarError(
                    cboRespuestaBanco,
                    "Errores.Cobro.RespuestaBancoObligatoria");
                return false;
            }

            return true;
        }

        private void MostrarError(Control control, string clave)
        {
            string mensaje = Traductor.Instancia.Traducir(clave);
            errorProvider.SetError(control, mensaje);
            lblMensaje.Text = mensaje;
            control.Focus();
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (!ValidarUI())
            {
                return;
            }

            try
            {
                ResultadoCobroBE resultado = _cobroBLL.CobrarReserva(
                    _factura,
                    _cliente,
                    txtBanco.Text,
                    txtNumeroTarjeta.Text,
                    dtpVencimiento.Value,
                    txtCodigoSeguridad.Text,
                    cboRespuestaBanco.SelectedIndex == 0);

                if (!resultado.Aprobado)
                {
                    lblMensaje.Text =
                        Traductor.Instancia.Traducir("frmCobrarReserva.MsgRechazado");

                    MessageBox.Show(
                        lblMensaje.Text,
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                FacturaPagada = resultado.Factura;
                PagoAprobado = resultado.Pago;

                MessageBox.Show(
                    Traductor.Instancia.Traducir("frmCobrarReserva.MsgAprobado"),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;

                MessageBox.Show(
                    ex.Message,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
