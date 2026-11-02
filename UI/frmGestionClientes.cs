using BE;
using BLL;
using Servicios;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    // ABM de clientes para usuarios con permiso CLI_MODIFICAR. No altera la tabla Cliente.
    public partial class frmGestionClientes : Form, IObservadorIdioma
    {
        private readonly ClienteBLL _clientes = new ClienteBLL();
        public frmGestionClientes()
        {
            InitializeComponent();
            ActualizarIdioma();
            grilla.SelectionChanged += (_, _) => Seleccionar();
            btnNuevo.Click += (_, _) => Limpiar();
            btnRegistrar.Click += (_, _) => Ejecutar(() => _clientes.RegistrarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnModificar.Click += (_, _) => Ejecutar(() => _clientes.ModificarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnEliminar.Click += (_, _) => {
                if (MessageBox.Show(T("frmGestionClientes.ConfirmarEliminar"), T("frmGestionClientes.Confirmacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Ejecutar(() => _clientes.EliminarCliente(dni.Text));
            };
            btnActualizar.Click += (_, _) => Cargar();
            btnVolver.Click += (_, _) => Close();
            Load += (_, _) =>
            {
                SM.Instancia.Suscribir(this);
                try { SM.Instancia.RequierePermiso("CLI_MODIFICAR"); Cargar(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); Close(); }
            };
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        public void ActualizarIdioma()
        {
            Text = T("frmGestionClientes.Title");
            lblTitulo.Text = T("frmGestionClientes.Titulo");
            gbDatos.Text = T("frmGestionClientes.Datos");
            lbldni.Text = T("frmGestionClientes.DNI");
            lblnombre.Text = T("frmGestionClientes.Nombre");
            lblapellido.Text = T("frmGestionClientes.Apellido");
            lbltelefono.Text = T("frmGestionClientes.Telefono");
            lblcorreo.Text = T("frmGestionClientes.Correo");
            lblListado.Text = T("frmGestionClientes.Listado");
            colDNI.HeaderText = T("frmGestionClientes.DNI");
            colNombre.HeaderText = T("frmGestionClientes.Nombre");
            colApellido.HeaderText = T("frmGestionClientes.Apellido");
            colTelefono.HeaderText = T("frmGestionClientes.Telefono");
            colCorreoElectronico.HeaderText = T("frmGestionClientes.ColCorreo");
            btnNuevo.Text = T("frmGestionClientes.Nuevo");
            btnRegistrar.Text = T("frmGestionClientes.Registrar");
            btnModificar.Text = T("frmGestionClientes.Modificar");
            btnEliminar.Text = T("frmGestionClientes.Eliminar");
            btnActualizar.Text = T("frmGestionClientes.Actualizar");
            btnVolver.Text = T("frmGestionClientes.Volver");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void Seleccionar()
        {
            if (grilla.CurrentRow?.Selected != true ||
                grilla.CurrentRow.DataBoundItem is not ClienteBE c) return;
            dni.Text = c.DNI;
            dni.ReadOnly = true; // Identificador primario; no se modifica.
            nombre.Text = c.Nombre;
            apellido.Text = c.Apellido;
            telefono.Text = c.Telefono;
            correo.Text = c.CorreoElectronico;
        }

        private void Limpiar()
        {
            grilla.ClearSelection();
            dni.ReadOnly = false;
            foreach (TextBox t in new[] { dni, nombre, apellido, telefono, correo }) t.Clear();
            dni.Focus();
        }

        private void Cargar()
        {
            grilla.DataSource = null;
            grilla.DataSource = _clientes.ListarTodos();
            Limpiar();
        }

        private void Ejecutar(Action accion)
        {
            try
            {
                accion();
                Cargar();
                MessageBox.Show(T("frmGestionClientes.OperacionCorrecta"), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
