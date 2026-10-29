using BE;
using BLL;
using Servicios;

namespace UI
{
    // Gestion de Productos del buffet: Vendedor y Dueno con permiso BUF_VENDER.
    // Los codigos de SQL se mantienen en BE, pero NO se exponen en la grilla.
    public partial class frmGestionProductos : Form, IObservadorIdioma
    {
        private readonly ProductoBLL _productoBLL;
        private List<ProductoBE> _productos = new List<ProductoBE>();
        private ProductoBE? _seleccionado;

        public frmGestionProductos()
        {
            InitializeComponent();
            dgvProductos.AutoGenerateColumns = false;
            _productoBLL = new ProductoBLL();
            ActualizarIdioma();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private void frmGestionProductos_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            try
            {
                CargarProductos();
                NuevoProducto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void frmGestionProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            Text = T("frmGestionProductos.Title");
            lblTitulo.Text = T("frmGestionProductos.Titulo");
            lblBuscar.Text = T("frmGestionProductos.Buscar");
            chkMostrarInactivos.Text = T("frmGestionProductos.Inactivos");
            gbDatos.Text = T("frmGestionProductos.Datos");
            lblNombre.Text = T("frmGestionProductos.Nombre");
            lblCategoria.Text = T("frmGestionProductos.Categoria");
            lblPrecio.Text = T("frmGestionProductos.Precio");
            lblMinimo.Text = T("frmGestionProductos.Minimo");
            lblStockInicial.Text = T("frmGestionProductos.StockInicial");
            lblReposicion.Text = T("frmGestionProductos.Reposicion");
            btnNuevo.Text = T("frmGestionProductos.Nuevo");
            btnCrear.Text = T("frmGestionProductos.Crear");
            btnModificar.Text = T("frmGestionProductos.Modificar");
            btnReponer.Text = T("frmGestionProductos.Reponer");
            btnVolver.Text = T("frmGestionProductos.Volver");

            // Los nombres de columna proceden del Designer, nunca se autogeneran.
            TituloColumna("colNombre", "frmGestionProductos.ColNombre");
            TituloColumna("colCategoria", "frmGestionProductos.ColCategoria");
            TituloColumna("colPrecio", "frmGestionProductos.ColPrecio");
            TituloColumna("colStock", "frmGestionProductos.ColStock");
            TituloColumna("colMinimo", "frmGestionProductos.ColMinimo");
            TituloColumna("colEstado", "frmGestionProductos.ColEstado");

            ActualizarBotones();
            dgvProductos.Invalidate();
        }

        private void TituloColumna(string nombre, string clave)
        {
            if (dgvProductos.Columns.Contains(nombre))
                dgvProductos.Columns[nombre]!.HeaderText = T(clave);
        }

        private void CargarProductos()
        {
            _productos = _productoBLL.ObtenerTodos();

            string textoCategoria = cmbCategoria.Text;
            cmbCategoria.Items.Clear();
            foreach (string categoria in _productos.Select(p => p.Categoria).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(c => c))
            {
                cmbCategoria.Items.Add(categoria);
            }
            cmbCategoria.Text = textoCategoria;

            FiltrarProductos();
        }

        private void FiltrarProductos()
        {
            string buscar = txtBuscar.Text.Trim();
            List<ProductoBE> resultado = _productos.Where(p => (chkMostrarInactivos.Checked || p.Activo)
                    && (p.Nombre.Contains(buscar, StringComparison.OrdinalIgnoreCase)|| 
                    p.Categoria.Contains(buscar, StringComparison.OrdinalIgnoreCase))).ToList();

            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = resultado;
            dgvProductos.ClearSelection();
            _seleccionado = null;
            ActualizarBotones();
            lblCantidad.Text = string.Format(T("frmGestionProductos.Resultados"), resultado.Count);
        }

        private void NuevoProducto()
        {
            _seleccionado = null;
            dgvProductos.ClearSelection();
            txtNombre.Clear();
            cmbCategoria.Text = "";
            nudPrecio.Value = nudPrecio.Minimum;
            nudMinimo.Value = 0;
            nudStockInicial.Value = 0;
            nudReposicion.Value = 1;
            lblMensaje.Text = T("frmGestionProductos.ModoNuevo");
            ActualizarBotones();
            txtNombre.Focus();
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow?.Selected != true ||dgvProductos.CurrentRow.DataBoundItem is not ProductoBE p)
                return;

            _seleccionado = p;
            txtNombre.Text = p.Nombre;
            cmbCategoria.Text = p.Categoria;
            nudPrecio.Value = Math.Clamp(p.PrecioUnitario, nudPrecio.Minimum, nudPrecio.Maximum);
            nudMinimo.Value = Math.Clamp(p.StockMinimo, 0, (int)nudMinimo.Maximum);
            nudStockInicial.Value = 0;
            nudReposicion.Value = 1;
            lblMensaje.Text = string.Empty;
            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            bool gestion = SM.Instancia.TienePermiso("BUF_VENDER");
            bool elegido = _seleccionado != null;
            btnCrear.Enabled = gestion && !elegido;
            btnModificar.Enabled = gestion && elegido;
            btnEstado.Enabled = gestion && elegido;
            btnEstado.Text = elegido && !_seleccionado!.Activo? T("frmGestionProductos.Reactivar"): T("frmGestionProductos.Desactivar");
            btnReponer.Enabled = gestion && elegido && _seleccionado!.Activo;
            nudStockInicial.Enabled = !elegido;
            nudReposicion.Enabled = gestion && elegido && _seleccionado!.Activo;
        }

        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProductos.Columns[e.ColumnIndex].Name != "colEstado")
                return;

            if (dgvProductos.Rows[e.RowIndex].DataBoundItem is not ProductoBE p)
                return;

            string clave = !p.Activo ? "frmGestionProductos.EstadoInactivo"
                : p.StockDisponible == 0 ? "frmGestionProductos.EstadoAgotado"
                : p.StockDisponible <= p.StockMinimo ? "frmGestionProductos.EstadoBajo"
                : "frmGestionProductos.EstadoDisponible";
            e.Value = T(clave);
            e.FormattingApplied = true;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => FiltrarProductos();
        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e) => FiltrarProductos();
        private void btnNuevo_Click(object sender, EventArgs e) => NuevoProducto();

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try { CargarProductos(); lblMensaje.Text = T("frmGestionProductos.CatalogoActualizado"); }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            EjecutarCambio(() => _productoBLL.CrearProducto(
                txtNombre.Text, cmbCategoria.Text, nudPrecio.Value,(int)nudStockInicial.Value, (int)nudMinimo.Value),"frmGestionProductos.Creado");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            int codigo = _seleccionado.CodigoProducto;
            EjecutarCambio(() => _productoBLL.ModificarProducto(codigo, txtNombre.Text, cmbCategoria.Text, nudPrecio.Value,(int)nudMinimo.Value),"frmGestionProductos.Modificado");
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            bool activar = !_seleccionado.Activo;
            string clave = activar ? "frmGestionProductos.ConfirmarReactivar": "frmGestionProductos.ConfirmarDesactivar";
            DialogResult respuesta = MessageBox.Show(string.Format(T(clave), _seleccionado.Nombre),Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            int codigo = _seleccionado.CodigoProducto;
            EjecutarCambio(() => _productoBLL.CambiarEstado(codigo, activar),activar ? "frmGestionProductos.Reactivado": "frmGestionProductos.Desactivado");
        }

        private void btnReponer_Click(object sender, EventArgs e)
        {
            if (_seleccionado == null) return;
            int codigo = _seleccionado.CodigoProducto;
            int cantidad = (int)nudReposicion.Value;
            DialogResult respuesta = MessageBox.Show(string.Format(T("frmGestionProductos.ConfirmarReposicion"),cantidad, _seleccionado.Nombre),Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            EjecutarCambio(() => _productoBLL.ReponerStock(codigo, cantidad),"frmGestionProductos.Repuesto");
        }

        private void EjecutarCambio(Action operacion, string claveMensaje)
        {
            try
            {
                operacion();
                CargarProductos();
                NuevoProducto();
                lblMensaje.Text = T(claveMensaje);
                MessageBox.Show(T(claveMensaje), Text,MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarError(Exception ex)
        {
            lblMensaje.Text = ex.Message;
            MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnVolver_Click(object sender, EventArgs e) => Close();
    }
}
