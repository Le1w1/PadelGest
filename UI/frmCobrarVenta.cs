using BE;
using BLL;
using Servicios;

namespace UI
{
    // CUN12: formulario de cobro simulado, basado en PN1.
    // La venta se registra automaticamente desde frmGenerarCarrito
    // cuando este formulario devuelve un Pago aprobado.
    public partial class frmCobrarVenta : Form, IObservadorIdioma
    {
        private readonly CobroVentaBLL _cobroBLL;
        private readonly CarritoBE _carrito;
        public PagoBE? PagoAprobado { get; private set; }
        public bool PagoRechazado { get; private set; }

        public frmCobrarVenta(CarritoBE carrito)
        {
            InitializeComponent();
            _carrito = carrito;
            _cobroBLL = new CobroVentaBLL();
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
            lblClienteValor.Text = _carrito.DNI ?? "-";
            int cantidad = _carrito.Detalles.Sum(d => d.Cantidad);
            lblProductosValor.Text = cantidad.ToString();
            lblTotalValor.Text = _carrito.ImporteTotal.ToString("C2");
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            lblMensaje.Text = string.Empty;
            try
            {
                if (cboRespuestaBanco.SelectedIndex < 0)
                    throw new Exception(T("Errores.Cobro.RespuestaBancoObligatoria"));

                ResultadoCobroVentaBE resultado = _cobroBLL.CobrarVenta(_carrito,txtBanco.Text, txtNumeroTarjeta.Text,dtpVencimiento.Value, txtCodigoSeguridad.Text,cboRespuestaBanco.SelectedIndex == 0);

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
