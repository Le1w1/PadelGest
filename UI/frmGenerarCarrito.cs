using BE;
using BLL;
using Servicios;

namespace UI
{
    // CUN08 Generar Carrito, CUN09 Seleccionar Productos y CUN10/11 Cliente.
    // CUN12 (cobro) se incorporara despues; aqui no se registra una venta.
    public partial class frmGenerarCarrito : Form, IObservadorIdioma
    {
        private readonly CarritoBLL _carritoBLL;
        private readonly ProductoBLL _productoBLL;
        private CarritoBE? _carrito;
        private readonly VentaBLL _ventaBLL;
        private PagoBE? _pagoAprobado;
        private VentaBE? _ventaRegistrada;

        public frmGenerarCarrito()
        {
            InitializeComponent();
            _carritoBLL = new CarritoBLL();
            _productoBLL = new ProductoBLL();
            _ventaBLL = new VentaBLL();
            ActualizarIdioma();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private void frmGenerarCarrito_Load(object sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            try
            {
                _carrito = _carritoBLL.GenerarCarrito();
                CargarProductos();
                ActualizarCarrito(_carrito);
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

        // Evita dejar carritos en preparacion sin cerrar cuando se usa la X.
        private void frmGenerarCarrito_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Igual que en PN1: si el banco aprobo el pago, no se puede
            // descartar la operacion antes de registrar la venta.
            if (_pagoAprobado != null && _ventaRegistrada == null)
            {
                e.Cancel = true;
                MessageBox.Show(T("frmGenerarCarrito.PagoPendienteRegistro"),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_carrito == null ||
                (_carrito.Estado != "EnPreparacion" && _carrito.Estado != "Listo"))
                return;

            DialogResult decision = MessageBox.Show(
                T("frmGenerarCarrito.ConfirmarCancelar"),
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (decision != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                // El carrito sin cobro se descarta de las tablas operativas.
                // Su cancelacion queda registrada en BitacoraEvento.
                _carritoBLL.CancelarCarrito(_carrito.IdCarrito);
                _carrito = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
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
            TraducirColumna(dgvDetalles, "colDetallePrecio", "frmGenerarCarrito.Precio");
            TraducirColumna(dgvDetalles, "colDetalleSubtotal", "frmGenerarCarrito.Subtotal");

            if (_carrito != null)
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

        private void CargarProductos()
        {
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

        private void ActualizarCarrito(CarritoBE carrito)
        {
            _carrito = carrito;
            // Evita exponer los IDs tecnicos del detalle aunque se regenere
            // el Designer de Windows Forms.
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = carrito.Detalles.ToList();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            if (_carrito == null)
                return;

            bool editable = _carrito.Estado == "EnPreparacion" && _pagoAprobado == null;
            bool tieneProductos = _carrito.Detalles.Count > 0;

            lblTotal.Text = _carrito.ImporteTotal.ToString("C2");
            lblEstado.Text = _carrito.Estado;
            lblCliente.Text = string.IsNullOrWhiteSpace(_carrito.DNI)
                ? T("frmGenerarCarrito.SinCliente")
                : _carrito.DNI;

            btnAgregar.Enabled = editable;
            btnModificar.Enabled = editable && tieneProductos;
            btnQuitar.Enabled = editable && tieneProductos;
            btnSeleccionarCliente.Enabled = editable && tieneProductos;
            btnActualizarProductos.Enabled = editable;
            nudCantidad.Enabled = editable;

            btnCobrar.Enabled = (_carrito.Estado == "Listo" && _pagoAprobado == null)
                || (_pagoAprobado != null && _ventaRegistrada == null);
            btnCobrar.Text = _pagoAprobado != null && _ventaRegistrada == null
                ? T("frmGenerarCarrito.ReintentarRegistro")
                : T("frmGenerarCarrito.Cobrar");
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
            if (dgvDetalles.CurrentRow?.DataBoundItem is not DetalleCarritoBE detalle)
                throw new Exception(T("frmGenerarCarrito.SeleccioneDetalle"));

            return detalle.CodigoProducto;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (_carrito == null) return;
            try
            {
                int codigo = ObtenerCodigoProducto();
                ActualizarCarrito(_carritoBLL.AgregarProducto(
                    _carrito.IdCarrito, codigo, (int)nudCantidad.Value));
                lblMensaje.Text = T("frmGenerarCarrito.ProductoAgregado");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_carrito == null) return;
            try
            {
                int codigo = ObtenerCodigoDetalle();
                ActualizarCarrito(_carritoBLL.ModificarCantidad(
                    _carrito.IdCarrito, codigo, (int)nudCantidad.Value));
                lblMensaje.Text = T("frmGenerarCarrito.CantidadModificada");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (_carrito == null) return;
            try
            {
                int codigo = ObtenerCodigoDetalle();
                ActualizarCarrito(_carritoBLL.QuitarProducto(_carrito.IdCarrito, codigo));
                lblMensaje.Text = T("frmGenerarCarrito.ProductoQuitado");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
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
            if (_carrito == null || _carrito.Detalles.Count == 0) return;

            // Reutiliza la seleccion y el registro de Cliente del PN1.
            using frmSeleccionarCliente formCliente = new frmSeleccionarCliente();
            if (formCliente.ShowDialog(this) != DialogResult.OK ||
                formCliente.ClienteSeleccionado == null)
                return;

            try
            {
                ActualizarCarrito(_carritoBLL.AsociarCliente(
                    _carrito.IdCarrito, formCliente.ClienteSeleccionado.DNI));
                lblMensaje.Text = T("frmGenerarCarrito.ClienteAsignado");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (_carrito == null)
                return;

            // Una autorizacion ya aprobada SOLO permite reintentar el registro.
            // Nunca se vuelve a llamar al banco para ese mismo pago.
            if (_pagoAprobado != null)
            {
                RegistrarVentaAutomatica();
                return;
            }

            if (_carrito.Estado != "Listo")
            {
                lblMensaje.Text = T("Errores.Buffet.CarritoNoListo");
                return;
            }

            try
            {
                _ventaBLL.ValidarAntesDeCobrar(_carrito.IdCarrito);

                using frmCobrarVenta formCobro = new frmCobrarVenta(_carrito);
                DialogResult respuesta = formCobro.ShowDialog(this);

                if (respuesta == DialogResult.No && formCobro.PagoRechazado)
                {
                    // Pago rechazado: no existe Venta, se descarta el carrito.
                    _carritoBLL.CancelarCarrito(_carrito.IdCarrito);
                    _carrito = null;
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
                    RegistrarVentaAutomatica();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarVentaAutomatica()
        {
            if (_carrito == null || _pagoAprobado == null || _ventaRegistrada != null)
                return;

            btnCobrar.Enabled = false;

            try
            {
                _ventaRegistrada = _ventaBLL.RegistrarVenta(
                    _carrito.IdCarrito, _pagoAprobado);

                _carrito = _carritoBLL.ConsultarCarrito(_carrito.IdCarrito);
                ActualizarCarrito(_carrito);

                string resumen = string.Format(
                    T("frmGenerarCarrito.VentaRegistrada"),
                    _ventaRegistrada.Codigo,
                    _ventaRegistrada.Factura.IdFacturaVenta,
                    _ventaRegistrada.ImporteTotal.ToString("C2"));

                MessageBox.Show(resumen, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = string.Format(
                    T("frmGenerarCarrito.ErrorRegistro"), ex.Message);
                MessageBox.Show(lblMensaje.Text, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed && _ventaRegistrada == null)
                    ActualizarResumen();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDetalles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow?.DataBoundItem is DetalleCarritoBE detalle)
            {
                nudCantidad.Value = Math.Min(nudCantidad.Maximum,
                    Math.Max(nudCantidad.Minimum, detalle.Cantidad));
            }
        }
    }
}
