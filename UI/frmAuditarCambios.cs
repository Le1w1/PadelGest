using BE;
using BLL;
using Servicios;
using UI.Servicios;

namespace UI
{
    // Auditoria historica de productos. La consulta es de solo lectura,
    // salvo la restauracion confirmada de una version anterior mediante triggers SQL.
    public partial class frmAuditarCambios : Form, IObservadorIdioma
    {
        private readonly ProductoCambioBLL _cambiosBLL = new ProductoCambioBLL();

        public frmAuditarCambios()
        {
            InitializeComponent();
            AyudaContextual.Configurar(this);
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
            btnBuscar.Text = T("frmAuditarCambios.Buscar");
            btnLimpiar.Text = T("frmAuditarCambios.Limpiar");
            btnRestaurar.Text = T("frmAuditarCambios.Restaurar");
            btnVolver.Text = T("frmAuditarCambios.Volver");

            CambiarTitulo("colCodigo", "frmAuditarCambios.ColCodigo");
            CambiarTitulo("colFecha", "frmAuditarCambios.ColFecha");
            CambiarTitulo("colHora", "frmAuditarCambios.ColHora");
            CambiarTitulo("colNombre", "frmAuditarCambios.ColNombre");
            CambiarTitulo("colPrecio", "frmAuditarCambios.ColPrecio");
            CambiarTitulo("colStock", "frmAuditarCambios.ColStock");
            CambiarTitulo("colEstado", "frmAuditarCambios.ColEstadoProducto");
            CambiarTitulo("colVigente", "frmAuditarCambios.ColVigente");

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
                    codigo, txtNombre.Text, desde, hasta, false);

                dgvCambios.DataSource = null;
                dgvCambios.DataSource = cambios;

                // Reafirmar la configuracion visual despues de enlazar los datos.
                // Evita que una configuracion heredada deje columnas o encabezados ocultos.
                dgvCambios.ColumnHeadersVisible = true;
                dgvCambios.RowHeadersVisible = false;
                foreach (DataGridViewColumn columna in dgvCambios.Columns)
                {
                    columna.Visible = true;
                    columna.HeaderCell.Style.ForeColor = Color.White;
                    columna.DefaultCellStyle.ForeColor = Color.Black;
                    columna.DefaultCellStyle.SelectionForeColor = Color.Black;
                }

                dgvCambios.Refresh();
                dgvCambios.ClearSelection();
                btnRestaurar.Enabled = false;
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
            Consultar();
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();

        private void dgvCambios_SelectionChanged(object sender, EventArgs e)
        {
            btnRestaurar.Enabled =
                dgvCambios.CurrentRow?.Selected == true &&
                dgvCambios.CurrentRow.DataBoundItem is ProductoCambioBE cambio &&
                !cambio.Act && SM.Instancia.TienePermiso("PRO_GESTIONAR");
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvCambios.CurrentRow?.Selected != true ||
                dgvCambios.CurrentRow.DataBoundItem is not ProductoCambioBE cambio || cambio.Act)
                return;

            string mensaje = string.Format(T("frmAuditarCambios.ConfirmarRestauracion"),
                cambio.Nombre, cambio.FechaHora, cambio.PrecioUnitario,
                cambio.StockDisponible, cambio.ActivoProducto
                    ? T("frmAuditarCambios.Activo") : T("frmAuditarCambios.Inactivo"));

            if (MessageBox.Show(mensaje, Text, MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                _cambiosBLL.RestaurarVersion(cambio.IdCambio);
                Consultar();
                MessageBox.Show(T("frmAuditarCambios.RestauracionCorrecta"), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Act de Producto_C se muestra literalmente (1 = vigente, 0 = historico).
        private void dgvCambios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is not bool valor)
                return;

            string columna = dgvCambios.Columns[e.ColumnIndex].Name;
            if (columna == "colVigente")
            {
                e.Value = valor ? "1" : "0";
                e.FormattingApplied = true;
            }
            else if (columna == "colEstado")
            {
                e.Value = T(valor
                    ? "frmAuditarCambios.Disponible"
                    : "frmAuditarCambios.Deshabilitado");
                e.FormattingApplied = true;
            }
        }
    }
}
