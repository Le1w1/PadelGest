using BE;
using BLL;
using Servicios;

namespace UI
{
    // CUN12: formulario de cobro simulado, basado en PN1.
    // La venta se registra automaticamente desde frmGenerarCarrito
    // cuando este formulario devuelve un Pago aprobado.
    public partial class frmCobrarFactura : Form, IObservadorIdioma
    {
        private readonly CobroFacturaBLL _cobroBLL;
        private readonly FacturaBuffetBE _factura;
        public PagoBE? PagoAprobado { get; private set; }
        public bool PagoRechazado { get; private set; }

        public frmCobrarFactura(FacturaBuffetBE factura)
        {
            InitializeComponent();
            _factura = factura;
            _cobroBLL = new CobroFacturaBLL();
            ActualizarIdioma();
            MostrarResumen();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private void frmCobrarVenta_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            dtpVencimiento.Format = DateTimePickerFormat.Custom;
            dtpVencimiento.CustomFormat = "MM/yyyy";
            dtpVencimiento.ShowUpDown = true;
            dtpVencimiento.MinDate = DateTime.Today;
            dtpVencimiento.Value = DateTime.Today.AddYears(1);
            txtBanco.Focus();
        }

        private void frmCobrarVenta_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            Text = T("frmCobrarVenta.Title");
            lblTitulo.Text = T("frmCobrarVenta.Titulo");
            gbResumen.Text = T("frmCobrarVenta.GbResumen");
            gbTarjeta.Text = T("frmCobrarVenta.GbTarjeta");
            lblClienteTitulo.Text = T("frmCobrarReserva.LblCliente");
            lblProductosTitulo.Text = T("frmCobrarVenta.Productos");
            lblTotalTitulo.Text = T("frmCobrarReserva.LblTotal");
            lblBanco.Text = T("frmCobrarReserva.LblBanco");
            lblTarjeta.Text = T("frmCobrarReserva.LblNumeroTarjeta");
            lblVencimiento.Text = T("frmCobrarReserva.LblVencimiento");
            lblSeguridad.Text = T("frmCobrarReserva.LblCodigoSeguridad");
            lblRespuesta.Text = T("frmCobrarReserva.LblRespuestaBanco");
            btnConfirmar.Text = T("frmCobrarVenta.BtnConfirmar");
            btnVolver.Text = T("frmCobrarReserva.BtnVolver");

            int seleccion = cboRespuestaBanco.SelectedIndex;
            cboRespuestaBanco.Items.Clear();
            cboRespuestaBanco.Items.Add(T("frmCobrarReserva.RespuestaAprobada"));
            cboRespuestaBanco.Items.Add(T("frmCobrarReserva.RespuestaRechazada"));
            cboRespuestaBanco.SelectedIndex = seleccion >= 0 ? seleccion : -1;
        }

        private void MostrarResumen()
        {
            lblClienteValor.Text = _factura.DNI ?? "-";
            int cantidad = _factura.Detalles.Sum(d => d.Cantidad);
            lblProductosValor.Text = cantidad.ToString();
            lblTotalValor.Text = _factura.ImporteTotal.ToString("C2");
        }

        // Impide escribir letras, espacios y simbolos en los campos numericos.
        // La BLL vuelve a validar para impedir datos invalidos pegados.
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                (e.KeyChar < '0' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        // Permite separar visualmente el numero de tarjeta con espacios.
        // El CVV sigue aceptando exclusivamente digitos.
        private void NumerosTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            lblMensaje.Text = string.Empty;
            try
            {
                if (cboRespuestaBanco.SelectedIndex < 0)
                    throw new Exception(T("Errores.Cobro.RespuestaBancoObligatoria"));

                ResultadoCobroFacturaBE resultado = _cobroBLL.CobrarFactura(_factura,txtBanco.Text, txtNumeroTarjeta.Text,dtpVencimiento.Value, txtCodigoSeguridad.Text,cboRespuestaBanco.SelectedIndex == 0);

                // Limpiar datos sensibles tan pronto como el banco responde.
                txtNumeroTarjeta.Clear();
                txtCodigoSeguridad.Clear();

                if (!resultado.Aprobado)
                {
                    PagoRechazado = true;
                    DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                PagoAprobado = resultado.Pago;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                    btnConfirmar.Enabled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
