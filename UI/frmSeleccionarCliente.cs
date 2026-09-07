using BE;
using BLL;
using Servicios;

namespace UI
{
    public partial class frmSeleccionarCliente : Form, IObservadorIdioma
    {
        private readonly ClienteBLL _clienteBLL;
        private ClienteBE? _clienteEncontrado;

        public ClienteBE? ClienteSeleccionado { get; private set; }

        public frmSeleccionarCliente()
        {
            InitializeComponent();
            _clienteBLL = new ClienteBLL();
            ActualizarIdioma();
        }

        private void frmSeleccionarCliente_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            txtDNI.Focus();
        }

        private void frmSeleccionarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmSeleccionarCliente.Title");
            lblTitulo.Text = t.Traducir("frmSeleccionarCliente.LblTitulo");
            gbBusqueda.Text = t.Traducir("frmSeleccionarCliente.GbBusqueda");
            lblDNI.Text = t.Traducir("frmSeleccionarCliente.LblDNI");
            btnBuscar.Text = t.Traducir("frmSeleccionarCliente.BtnBuscar");

            gbDatosCliente.Text = t.Traducir("frmSeleccionarCliente.GbDatos");
            lblDNIValorTitulo.Text = t.Traducir("frmSeleccionarCliente.LblDNI");
            lblNombreTitulo.Text = t.Traducir("frmSeleccionarCliente.LblNombre");
            lblApellidoTitulo.Text = t.Traducir("frmSeleccionarCliente.LblApellido");
            lblTelefonoTitulo.Text = t.Traducir("frmSeleccionarCliente.LblTelefono");
            lblCorreoTitulo.Text = t.Traducir("frmSeleccionarCliente.LblCorreo");

            btnConfirmar.Text = t.Traducir("frmSeleccionarCliente.BtnConfirmar");
            btnRegistrarCliente.Text = t.Traducir("frmSeleccionarCliente.BtnRegistrar");
            btnVolver.Text = t.Traducir("frmSeleccionarCliente.BtnVolver");
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtDNI, string.Empty);
            lblMensaje.Text = string.Empty;
            LimpiarClienteEncontrado();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                lblMensaje.Text = string.Empty;
                LimpiarClienteEncontrado();

                string dni = txtDNI.Text.Trim();

                if (string.IsNullOrWhiteSpace(dni))
                {
                    string mensaje =
                        Traductor.Instancia.Traducir("Errores.Cliente.DNIObligatorio");

                    errorProvider.SetError(txtDNI, mensaje);
                    lblMensaje.Text = mensaje;
                    txtDNI.Focus();
                    return;
                }

                _clienteEncontrado = _clienteBLL.BuscarPorDNI(dni);

                if (_clienteEncontrado == null)
                {
                    lblMensaje.Text =
                        Traductor.Instancia.Traducir("frmSeleccionarCliente.MsgNoRegistrado");

                    btnRegistrarCliente.Visible = true;
                    btnRegistrarCliente.Enabled =
                        SM.Instancia.TienePermiso("CLI_REGISTRAR");
                    return;
                }

                MostrarCliente(_clienteEncontrado);
                btnConfirmar.Enabled = true;
            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtDNI, ex.Message);
                lblMensaje.Text = ex.Message;
                txtDNI.Focus();
            }
        }

        private void MostrarCliente(ClienteBE cliente)
        {
            lblDNIValor.Text = cliente.DNI;
            lblNombreValor.Text = cliente.Nombre;
            lblApellidoValor.Text = cliente.Apellido;
            lblTelefonoValor.Text = cliente.Telefono;
            lblCorreoValor.Text = cliente.CorreoElectronico;
        }

        private void LimpiarClienteEncontrado()
        {
            _clienteEncontrado = null;
            ClienteSeleccionado = null;

            lblDNIValor.Text = "-";
            lblNombreValor.Text = "-";
            lblApellidoValor.Text = "-";
            lblTelefonoValor.Text = "-";
            lblCorreoValor.Text = "-";

            btnConfirmar.Enabled = false;
            btnRegistrarCliente.Visible = false;
            btnRegistrarCliente.Enabled = false;
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            using frmRegistrarCliente formRegistrar = new frmRegistrarCliente(dni);

            if (formRegistrar.ShowDialog(this) == DialogResult.OK &&
                formRegistrar.ClienteRegistrado != null)
            {
                _clienteEncontrado = formRegistrar.ClienteRegistrado;

                MostrarCliente(_clienteEncontrado);
                btnRegistrarCliente.Visible = false;
                btnRegistrarCliente.Enabled = false;
                btnConfirmar.Enabled = true;

                lblMensaje.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarCliente.MsgRegistradoIdentificado");
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_clienteEncontrado == null)
            {
                lblMensaje.Text =
                    Traductor.Instancia.Traducir("frmSeleccionarCliente.MsgDebeBuscar");
                return;
            }

            ClienteSeleccionado = _clienteEncontrado;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
