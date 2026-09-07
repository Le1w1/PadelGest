using BE;
using BLL;
using Servicios;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class frmRegistrarCliente : Form, IObservadorIdioma
    {
        private readonly ClienteBLL _clienteBLL;
        private readonly string _dniInicial;

        public ClienteBE? ClienteRegistrado { get; private set; }

        public frmRegistrarCliente(string dniInicial = "")
        {
            InitializeComponent();
_clienteBLL = new ClienteBLL();
            _dniInicial = (dniInicial ?? string.Empty).Trim();

            ActualizarIdioma();
        }

        private void frmRegistrarCliente_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);

            txtDNI.Text = _dniInicial;

            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                txtDNI.Focus();
            }
            else
            {
                txtNombre.Focus();
            }
        }

        private void frmRegistrarCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            var t = Traductor.Instancia;

            Text = t.Traducir("frmRegistrarCliente.Title");
            lblTitulo.Text = t.Traducir("frmRegistrarCliente.LblTitulo");
            gbDatos.Text = t.Traducir("frmRegistrarCliente.GbDatos");
            lblDNI.Text = t.Traducir("frmRegistrarCliente.LblDNI");
            lblNombre.Text = t.Traducir("frmRegistrarCliente.LblNombre");
            lblApellido.Text = t.Traducir("frmRegistrarCliente.LblApellido");
            lblTelefono.Text = t.Traducir("frmRegistrarCliente.LblTelefono");
            lblCorreo.Text = t.Traducir("frmRegistrarCliente.LblCorreo");
            btnRegistrar.Text = t.Traducir("frmRegistrarCliente.BtnRegistrar");
            btnVolver.Text = t.Traducir("frmRegistrarCliente.BtnVolver");
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Campo_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
            lblMensaje.Text = string.Empty;
        }

        private bool ValidarCamposUI()
        {
            errorProvider.Clear();
            lblMensaje.Text = string.Empty;

            string dni = txtDNI.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre) ||string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(correo))
            {
                lblMensaje.Text = Traductor.Instancia.Traducir("Errores.Cliente.CamposObligatorios");
                return false;
            }

            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
            {
                string mensaje = Traductor.Instancia.Traducir("Errores.Cliente.DNIInvalido");

                errorProvider.SetError(txtDNI, mensaje);
                lblMensaje.Text = mensaje;
                txtDNI.Focus();
                return false;
            }

            if (!Regex.IsMatch(nombre, @"^[\p{L}\s'-]{2,50}$"))
            {
                string mensaje = Traductor.Instancia.Traducir("Errores.Cliente.NombreInvalido");

                errorProvider.SetError(txtNombre, mensaje);
                lblMensaje.Text = mensaje;
                txtNombre.Focus();
                return false;
            }

            if (!Regex.IsMatch(apellido, @"^[\p{L}\s'-]{2,50}$"))
            {
                string mensaje = Traductor.Instancia.Traducir("Errores.Cliente.ApellidoInvalido");

                errorProvider.SetError(txtApellido, mensaje);
                lblMensaje.Text = mensaje;
                txtApellido.Focus();
                return false;
            }

            if (!Regex.IsMatch(telefono, @"^\d{8,15}$"))
            {
                string mensaje = Traductor.Instancia.Traducir("Errores.Cliente.TelefonoInvalido");

                errorProvider.SetError(txtTelefono, mensaje);
                lblMensaje.Text = mensaje;
                txtTelefono.Focus();
                return false;
            }

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                string mensaje = Traductor.Instancia.Traducir("Errores.Cliente.CorreoInvalido");

                errorProvider.SetError(txtCorreo, mensaje);
                lblMensaje.Text = mensaje;
                txtCorreo.Focus();
                return false;
            }

            return true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposUI())
            {
                return;
            }

            try
            {
                ClienteRegistrado = _clienteBLL.RegistrarCliente(txtDNI.Text,txtNombre.Text,txtApellido.Text,txtTelefono.Text,txtCorreo.Text);

                MessageBox.Show(Traductor.Instancia.Traducir("frmRegistrarCliente.MsgRegistrado"),Text,MessageBoxButtons.OK,MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message,Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
