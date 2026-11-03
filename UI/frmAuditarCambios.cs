using BE;
using BLL;
using Servicios;

namespace UI
{
    // Auditoria historica de productos. Es una consulta de solo lectura:
    // los registros de Producto_C se crean desde el trigger SQL Server.
    public partial class frmAuditarCambios : Form, IObservadorIdioma
    {
        private readonly ProductoCambioBLL _cambiosBLL = new ProductoCambioBLL();

        public frmAuditarCambios()
        {
            InitializeComponent();
            dgvCambios.AutoGenerateColumns = false;
            ActualizarIdioma();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private void frmAuditarCambios_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            try
            {
                SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            Consultar();
        }

        private void frmAuditarCambios_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            Text = T("frmAuditarCambios.Title");
            lblTitulo.Text = T("frmAuditarCambios.Titulo");
            lblCodigo.Text = T("frmAuditarCambios.Codigo");
            lblNombre.Text = T("frmAuditarCambios.Nombre");
            lblDesde.Text = T("frmAuditarCambios.Desde");
            lblHasta.Text = T("frmAuditarCambios.Hasta");
            chkSoloVigentes.Text = T("frmAuditarCambios.SoloVigentes");
            btnBuscar.Text = T("frmAuditarCambios.Buscar");
            btnLimpiar.Text = T("frmAuditarCambios.Limpiar");
            btnVolver.Text = T("frmAuditarCambios.Volver");
            lblAviso.Text = T("frmAuditarCambios.Aviso");

            CambiarTitulo("colIdCambio", "frmAuditarCambios.ColIdCambio");
            CambiarTitulo("colCodigo", "frmAuditarCambios.ColCodigo");
            CambiarTitulo("colFecha", "frmAuditarCambios.ColFecha");
            CambiarTitulo("colNombre", "frmAuditarCambios.ColNombre");
            CambiarTitulo("colCategoria", "frmAuditarCambios.ColCategoria");
            CambiarTitulo("colPrecio", "frmAuditarCambios.ColPrecio");
            CambiarTitulo("colStock", "frmAuditarCambios.ColStock");
            CambiarTitulo("colMinimo", "frmAuditarCambios.ColMinimo");
            CambiarTitulo("colActivo", "frmAuditarCambios.ColActivo");
            CambiarTitulo("colVigente", "frmAuditarCambios.ColVigente");
            CambiarTitulo("colTipo", "frmAuditarCambios.ColTipo");

            if (dgvCambios.DataSource is List<ProductoCambioBE> datos)
                ActualizarCantidad(datos.Count);
            dgvCambios.Invalidate();
        }

        private void CambiarTitulo(string columna, string clave)
        {
            if (dgvCambios.Columns.Contains(columna))
                dgvCambios.Columns[columna]!.HeaderText = T(clave);
        }

        private void Consultar()
        {
            try
            {
                int? codigo = nudCodigo.Value == 0 ? null : (int)nudCodigo.Value;
                DateTime? desde = dtpDesde.Checked ? dtpDesde.Value.Date : null;
                DateTime? hasta = dtpHasta.Checked ? dtpHasta.Value.Date : null;

                List<ProductoCambioBE> cambios = _cambiosBLL.ConsultarCambios(
                    codigo, txtNombre.Text, desde, hasta, chkSoloVigentes.Checked);

                dgvCambios.DataSource = null;
                dgvCambios.DataSource = cambios;
                dgvCambios.ClearSelection();
                ActualizarCantidad(cambios.Count);
            }
            catch (Exception ex)
            {
                // No ocultar un resultado previo tras un fallo de conexion o permisos.
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarCantidad(int cantidad)
        {
            lblCantidad.Text = string.Format(T("frmAuditarCambios.Resultados"), cantidad);
            if (cantidad == 500)
                lblCantidad.Text += " " + T("frmAuditarCambios.Limite");
        }

        private void btnBuscar_Click(object sender, EventArgs e) => Consultar();

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            nudCodigo.Value = 0;
            txtNombre.Clear();
            dtpDesde.Checked = false;
            dtpHasta.Checked = false;
            chkSoloVigentes.Checked = false;
            Consultar();
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvCambios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is not bool valor)
                return;

            string nombre = dgvCambios.Columns[e.ColumnIndex].Name;
            if (nombre == "colActivo")
            {
                e.Value = valor ? T("frmAuditarCambios.Activo")
                                : T("frmAuditarCambios.Inactivo");
                e.FormattingApplied = true;
            }
            else if (nombre == "colVigente")
            {
                e.Value = valor ? T("frmAuditarCambios.Vigente")
                                : T("frmAuditarCambios.Anterior");
                e.FormattingApplied = true;
            }
        }
    }
}
