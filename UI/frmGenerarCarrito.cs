using BE;
using BLL;
using Servicios;

namespace UI
{
    // CUN08 Generar Factura (en memoria) y CUN10 Cobrar Factura.
    // CUN09 Registrar Cliente reutiliza el formulario de PN1.
    public partial class frmGenerarCarrito : Form, IObservadorIdioma
    {
        private readonly FacturaBuffetBLL _facturaBLL;
        private readonly ProductoBLL _productoBLL;
        private FacturaBuffetBE? _factura;
        private PagoBE? _pagoAprobado;
        private FacturaBuffetBE? _facturaRegistrada;

        public frmGenerarCarrito()
        {
            InitializeComponent();
            AsegurarColumnasGrillas();
            _facturaBLL = new FacturaBuffetBLL();
            _productoBLL = new ProductoBLL();
            ActualizarIdioma();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private void frmGenerarCarrito_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            try
            {
                // CUN08: factura local, sin INSERT de cabecera ni detalles hasta cobrar.
                _factura = _facturaBLL.GenerarFactura();
                CargarProductos();
                ActualizarFactura(_factura);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void frmGenerarCarrito_FormClosed(object sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        private void frmGenerarCarrito_FormClosing(object sender, FormClosingEventArgs e)
        {
            // El pago aprobado no se puede descartar: reintentar SOLO el registro.
            if (_pagoAprobado != null && _facturaRegistrada == null)
            {
                e.Cancel = true;
                MessageBox.Show(T("frmGenerarCarrito.PagoPendienteRegistro"),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_factura == null || _facturaRegistrada != null ||
                _factura.Estado == "Pagada")
                return;

            if (MessageBox.Show(T("frmGenerarCarrito.ConfirmarCancelar"),
                    Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;

            // No se persiste ni borra nada: el detalle de la factura estaba en memoria.
        }

        public void ActualizarIdioma()
        {
            Text = T("frmGenerarCarrito.Title");
            lblTitulo.Text = T("frmGenerarCarrito.Titulo");
            gbCatalogo.Text = T("frmGenerarCarrito.Catalogo");
            gbSeleccion.Text = T("frmGenerarCarrito.Seleccion");
            gbCarrito.Text = T("frmGenerarCarrito.Carrito");
            lblCantidad.Text = T("frmGenerarCarrito.Cantidad");
            btnAgregar.Text = T("frmGenerarCarrito.Agregar");
            btnModificar.Text = T("frmGenerarCarrito.Modificar");
            btnQuitar.Text = T("frmGenerarCarrito.Quitar");
            btnActualizarProductos.Text = T("frmGenerarCarrito.ActualizarProductos");
            btnSeleccionarCliente.Text = T("frmGenerarCarrito.SeleccionarCliente");
            btnCobrar.Text = T("frmGenerarCarrito.Cobrar");
            btnVolver.Text = T("frmGenerarCarrito.Volver");
            lblTituloTotal.Text = T("frmGenerarCarrito.Total");
            lblTituloCliente.Text = T("frmGenerarCarrito.Cliente");
            lblTituloEstado.Text = T("frmGenerarCarrito.Estado");

            // Los encabezados se buscan dentro de la grilla. Si Visual Studio
            // regenero el Designer y alguna columna ya no existe, evitamos
            // NullReferenceException y conservamos las traducciones disponibles.
            TraducirColumna(dgvProductos, "colProductoNombre", "frmGenerarCarrito.Producto");
            TraducirColumna(dgvProductos, "colProductoCategoria", "frmGenerarCarrito.Categoria");
            TraducirColumna(dgvProductos, "colProductoPrecio", "frmGenerarCarrito.Precio");
            TraducirColumna(dgvProductos, "colProductoStock", "frmGenerarCarrito.Stock");
            TraducirColumna(dgvDetalles, "colDetalleProducto", "frmGenerarCarrito.Producto");
            TraducirColumna(dgvDetalles, "colDetalleCantidad", "frmGenerarCarrito.Cantidad");
            TraducirColumna(dgvDetalles, "colDetalleSubtotal", "frmGenerarCarrito.Subtotal");

            if (_factura != null)
                ActualizarResumen();
        }

        // Se usa el nombre de columna declarado por el Designer en vez del
        // campo parcial, que puede haber sido regenerado en Visual Studio.
        private static void TraducirColumna(DataGridView grilla, string nombreColumna, string clave)
        {
            if (grilla == null || !grilla.Columns.Contains(nombreColumna))
                return;

            DataGridViewColumn? columna = grilla.Columns[nombreColumna];
            if (columna != null)
                columna.HeaderText = T(clave);
        }

        // Solo controla el mapeo de datos. El estilo visual sigue configurado
        // en el Designer. Esta proteccion evita que un Designer regenerado
        // deje ambas grillas vacias al tener AutoGenerateColumns = false.
        private void AsegurarColumnasGrillas()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvDetalles.AutoGenerateColumns = false;

            AsegurarColumnas(
                dgvProductos,
                new[] { "colProductoNombre", "colProductoCategoria",
                        "colProductoPrecio", "colProductoStock" },
                new[] { "Nombre", "Categoria", "PrecioUnitario", "StockDisponible" },
                new[] { "Producto", "Categoría", "Precio", "Stock" });

            AsegurarColumnas(
                dgvDetalles,
                new[] { "colDetalleProducto", "colDetalleCantidad", "colDetalleSubtotal" },
                new[] { "NombreProducto", "Cantidad", "Subtotal" },
                new[] { "Producto", "Cantidad", "Subtotal" });
        }

        private static void AsegurarColumnas(DataGridView grilla,
            string[] nombres, string[] propiedades, string[] titulos)
        {
            bool columnasCorrectas = grilla.Columns.Count == nombres.Length;

            if (columnasCorrectas)
            {
                for (int i = 0; i < nombres.Length; i++)
                {
                    DataGridViewColumn col = grilla.Columns[i];
                    if (col.Name != nombres[i] || col.DataPropertyName != propiedades[i])
                    {
                        columnasCorrectas = false;
                        break;
                    }
                }
            }

            if (columnasCorrectas)
                return;

            // Al reconstruir las columnas no se agrega ningun ID tecnico.
            grilla.Columns.Clear();

            for (int i = 0; i < nombres.Length; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
                {
                    Name = nombres[i],
                    DataPropertyName = propiedades[i],
                    HeaderText = titulos[i],
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.Automatic
                };

                if (i == 0)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                else if (propiedades[i] == "Categoria")
                    col.Width = 103;
                else if (propiedades[i] == "StockDisponible")
                    col.Width = 63;
                else if (propiedades[i] == "Cantidad")
                    col.Width = 79;
                else
                    col.Width = 95;

                if (propiedades[i] == "PrecioUnitario" || propiedades[i] == "Subtotal")
                    col.DefaultCellStyle.Format = "C2";

                grilla.Columns.Add(col);
            }
        }

        private void CargarProductos()
        {
            AsegurarColumnasGrillas();
            List<ProductoBE> productos = _productoBLL.ObtenerProductos();
            // El Designer define las cuatro columnas visibles. El enlace de datos
            // nunca debe generar otras propiedades, como CodigoProducto o Activo.
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;

            if (productos.Count == 0)
                lblMensaje.Text = T("frmGenerarCarrito.SinProductos");
            else
                lblMensaje.Text = string.Empty;
        }

        private void ActualizarFactura(FacturaBuffetBE factura)
        {
            _factura = factura;
            AsegurarColumnasGrillas();
            // Evita exponer los IDs tecnicos del detalle aunque se regenere
            // el Designer de Windows Forms.
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = factura.Detalles.ToList();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            if (_factura == null) return;
            bool editable = _factura.Estado == "EnPreparacion" && _pagoAprobado == null;
            bool tieneProductos = _factura.Detalles.Count > 0;

            lblTotal.Text = _factura.ImporteTotal.ToString("C2");
            lblEstado.Text = _factura.Estado switch
            {
                "EnPreparacion" => T("frmGenerarCarrito.EstadoEnPreparacion"),
                "Listo" => T("frmGenerarCarrito.EstadoListo"),
                "Pagada" => T("frmGenerarCarrito.EstadoVendido"),
                _ => _factura.Estado
            };
            lblCliente.Text = string.IsNullOrWhiteSpace(_factura.DNI)
                ? T("frmGenerarCarrito.SinCliente") : _factura.DNI;

            btnAgregar.Enabled = editable;
            btnModificar.Enabled = editable && tieneProductos;
            btnQuitar.Enabled = editable && tieneProductos;
            btnSeleccionarCliente.Enabled = editable && tieneProductos;
            btnActualizarProductos.Enabled = editable;
            nudCantidad.Enabled = editable;

            btnCobrar.Enabled = (_factura.Estado == "Listo" && _pagoAprobado == null)
                || (_pagoAprobado != null && _facturaRegistrada == null);
            btnCobrar.Text = _pagoAprobado != null && _facturaRegistrada == null
                ? T("frmGenerarCarrito.ReintentarRegistro") : T("frmGenerarCarrito.Cobrar");
            btnVolver.Enabled = _pagoAprobado == null;
        }

        private int ObtenerCodigoProducto()
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoBE producto)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));

            return producto.CodigoProducto;
        }

        private int ObtenerCodigoDetalle()
        {
            if (dgvDetalles.CurrentRow?.DataBoundItem is not DetalleFacturaBE detalle)
                throw new Exception(T("frmGenerarCarrito.SeleccioneDetalle"));

            return detalle.CodigoProducto;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (_factura == null) return;
            try
            {
                ActualizarFactura(_facturaBLL.AgregarProducto(
                    _factura, ObtenerCodigoProducto(), (int)nudCantidad.Value));
                lblMensaje.Text = T("frmGenerarCarrito.ProductoAgregado");
            }
            catch (Exception ex) { lblMensaje.Text = ex.Message; }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_factura == null) return;
            try
            {
                ActualizarFactura(_facturaBLL.ModificarCantidad(
                    _factura, ObtenerCodigoDetalle(), (int)nudCantidad.Value));
                lblMensaje.Text = T("frmGenerarCarrito.CantidadModificada");
            }
            catch (Exception ex) { lblMensaje.Text = ex.Message; }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (_factura == null) return;
            try
            {
                ActualizarFactura(_facturaBLL.QuitarProducto(_factura, ObtenerCodigoDetalle()));
                lblMensaje.Text = T("frmGenerarCarrito.ProductoQuitado");
            }
            catch (Exception ex) { lblMensaje.Text = ex.Message; }
        }

        private void btnActualizarProductos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarProductos();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            if (_factura == null || _factura.Detalles.Count == 0) return;
            // Reutiliza PN1: CUN09 Registrar Cliente se ejecuta solo si es necesario.
            using frmSeleccionarCliente formCliente = new frmSeleccionarCliente();
            if (formCliente.ShowDialog(this) != DialogResult.OK ||
                formCliente.ClienteSeleccionado == null) return;
            try
            {
                ActualizarFactura(_facturaBLL.AsociarCliente(
                    _factura, formCliente.ClienteSeleccionado.DNI));
                lblMensaje.Text = T("frmGenerarCarrito.ClienteAsignado");
            }
            catch (Exception ex) { lblMensaje.Text = ex.Message; }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (_factura == null) return;
            if (_pagoAprobado != null)
            {
                RegistrarFacturaAutomatica();
                return;
            }
            if (_factura.Estado != "Listo")
            {
                lblMensaje.Text = T("Errores.Cobro.FacturaInvalida");
                return;
            }

            try
            {
                _facturaBLL.ValidarAntesDeCobrar(_factura);
                using frmCobrarVenta formCobro = new frmCobrarVenta(_factura);
                DialogResult respuesta = formCobro.ShowDialog(this);
                if (respuesta == DialogResult.No && formCobro.PagoRechazado)
                {
                    // No hubo factura persistida ni unidades descontadas.
                    _factura = null;
                    MessageBox.Show(T("frmGenerarCarrito.CobroRechazado"),
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                if (respuesta == DialogResult.OK && formCobro.PagoAprobado != null)
                {
                    _pagoAprobado = formCobro.PagoAprobado;
                    ActualizarResumen();
                    RegistrarFacturaAutomatica();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarFacturaAutomatica()
        {
            if (_factura == null || _pagoAprobado == null || _facturaRegistrada != null)
                return;

            btnCobrar.Enabled = false;
            try
            {
                _facturaRegistrada = _facturaBLL.RegistrarFactura(_factura, _pagoAprobado);
                ActualizarResumen();
                string resumen = string.Format(
                    T("frmGenerarCarrito.VentaRegistrada"),
                    _facturaRegistrada.NumeroFactura,
                    _facturaRegistrada.IdFactura,
                    _facturaRegistrada.ImporteTotal.ToString("C2"));
                MessageBox.Show(resumen, T("frmGenerarCarrito.TituloVentaFinalizada"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = string.Format(T("frmGenerarCarrito.ErrorRegistro"), ex.Message);
                MessageBox.Show(lblMensaje.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed && _facturaRegistrada == null)
                    ActualizarResumen();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDetalles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow?.DataBoundItem is DetalleFacturaBE detalle)
            {
                nudCantidad.Value = Math.Min(nudCantidad.Maximum,
                    Math.Max(nudCantidad.Minimum, detalle.Cantidad));
            }
        }
    }
}
