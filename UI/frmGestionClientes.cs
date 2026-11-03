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
        private readonly ClienteXmlBLL _xml = new ClienteXmlBLL();
        private bool _mostrandoXml;
        public frmGestionClientes()
        {
            InitializeComponent();
            ActualizarIdioma();
            grilla.SelectionChanged += (_, _) => Seleccionar();
            btnNuevo.Click += (_, _) =>
            {
                if (_mostrandoXml) Cargar();
                else Limpiar();
            };
            btnRegistrar.Click += (_, _) => Ejecutar(() => _clientes.RegistrarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnModificar.Click += (_, _) => Ejecutar(() => _clientes.ModificarCliente(dni.Text, nombre.Text, apellido.Text, telefono.Text, correo.Text));
            btnEliminar.Click += (_, _) => {
                if (MessageBox.Show(T("frmGestionClientes.ConfirmarEliminar"), T("frmGestionClientes.Confirmacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Ejecutar(() => _clientes.EliminarCliente(dni.Text));
            };
            btnActualizar.Click += (_, _) => Cargar();
            btnElegirExportar.Click += (_, _) => SeleccionarDestinoXml();
            btnElegirImportar.Click += (_, _) => SeleccionarOrigenXml();
            btnSerializar.Click += (_, _) => SerializarClientes();
            btnDeserializar.Click += (_, _) => DeserializarClientes();
            btnLimpiarRutas.Click += (_, _) =>
            {
                txtRutaExportar.Clear();
                txtRutaImportar.Clear();
            };
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
            gbXml.Text = T("frmGestionClientes.XmlTitulo");
            lblRutaExportar.Text = T("frmGestionClientes.XmlRutaSalida");
            lblRutaImportar.Text = T("frmGestionClientes.XmlRutaEntrada");
            btnElegirExportar.Text = T("frmGestionClientes.XmlExaminar");
            btnElegirImportar.Text = T("frmGestionClientes.XmlExaminar");
            btnSerializar.Text = T("frmGestionClientes.XmlSerializar");
            btnDeserializar.Text = T("frmGestionClientes.XmlDeserializar");
            btnLimpiarRutas.Text = T("frmGestionClientes.XmlLimpiarRutas");
            ayudaBotones.SetToolTip(btnNuevo, T("frmGestionClientes.XmlAyudaNuevo"));
            ayudaBotones.SetToolTip(btnActualizar, T("frmGestionClientes.XmlAyudaActualizar"));

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
            List<ClienteBE> clientes = _clientes.ListarTodos();
            _mostrandoXml = false;
            grilla.DataSource = null;
            grilla.DataSource = clientes;
            btnRegistrar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            Limpiar();
        }

        private void SeleccionarDestinoXml()
        {
            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Title = T("frmGestionClientes.XmlElegirDestino"),
                Filter = "XML (*.xml)|*.xml",
                DefaultExt = "xml",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = "Clientes.xml"
            };
            if (dialogo.ShowDialog(this) == DialogResult.OK)
                txtRutaExportar.Text = dialogo.FileName;
        }

        private void SeleccionarOrigenXml()
        {
            using OpenFileDialog dialogo = new OpenFileDialog
            {
                Title = T("frmGestionClientes.XmlElegirOrigen"),
                Filter = "XML (*.xml)|*.xml",
                CheckFileExists = true,
                Multiselect = false
            };
            if (dialogo.ShowDialog(this) == DialogResult.OK)
                txtRutaImportar.Text = dialogo.FileName;
        }

        private void SerializarClientes()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutaExportar.Text))
                    throw new InvalidOperationException(T("frmGestionClientes.XmlSinDestino"));

                // Solo lee los registros visibles de la grilla, nunca la BD.
                List<ClienteBE> clientes = new List<ClienteBE>();
                foreach (DataGridViewRow fila in grilla.Rows)
                {
                    if (fila.Visible && fila.DataBoundItem is ClienteBE cliente)
                        clientes.Add(cliente);
                }
                _xml.Serializar(clientes, txtRutaExportar.Text);
                MessageBox.Show(string.Format(
                    T("frmGestionClientes.XmlExportado"), clientes.Count), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeserializarClientes()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutaImportar.Text))
                    throw new InvalidOperationException(T("frmGestionClientes.XmlSinOrigen"));

                // No se hace INSERT ni UPDATE: solo se reemplazan las filas
                // visualizadas. Actualizar vuelve a consultar SQL Server.
                List<ClienteBE> clientes = _xml.Deserializar(txtRutaImportar.Text);
                grilla.DataSource = null;
                _mostrandoXml = true;
                grilla.DataSource = clientes;
                btnRegistrar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                Limpiar();
                MessageBox.Show(string.Format(
                    T("frmGestionClientes.XmlImportado"), clientes.Count), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
