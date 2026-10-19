namespace UI
{
    partial class frmGenerarCarrito
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            gbCatalogo = new GroupBox();
            dgvProductos = new DataGridView();
            colProductoNombre = new DataGridViewTextBoxColumn();
            colProductoCategoria = new DataGridViewTextBoxColumn();
            colProductoPrecio = new DataGridViewTextBoxColumn();
            colProductoStock = new DataGridViewTextBoxColumn();
            gbSeleccion = new GroupBox();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnQuitar = new Button();
            btnActualizarProductos = new Button();
            gbCarrito = new GroupBox();
            dgvDetalles = new DataGridView();
            colDetalleProducto = new DataGridViewTextBoxColumn();
            colDetalleCantidad = new DataGridViewTextBoxColumn();
            colDetallePrecio = new DataGridViewTextBoxColumn();
            colDetalleSubtotal = new DataGridViewTextBoxColumn();
            lblTituloEstado = new Label();
            lblEstado = new Label();
            lblTituloCliente = new Label();
            lblCliente = new Label();
            lblTituloTotal = new Label();
            lblTotal = new Label();
            btnSeleccionarCliente = new Button();
            btnCobrar = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();
            gbCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            gbSeleccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            gbCarrito.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            SuspendLayout();

            // Colores AirPadel, configurados solamente desde el Designer.
            Color azulFondo = Color.FromArgb(14, 43, 92);
            Color azulPanel = Color.FromArgb(24, 70, 138);
            Color amarillo = Color.FromArgb(214, 246, 36);
            Color blanco = Color.White;
            Color negro = Color.Black;

            lblTitulo.Location = new Point(25, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(700, 46);
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = amarillo;
            lblTitulo.Text = "Generar Carrito";

            gbCatalogo.Location = new Point(25, 75);
            gbCatalogo.Name = "gbCatalogo";
            gbCatalogo.Size = new Size(540, 400);
            gbCatalogo.Text = "Productos disponibles";
            gbCatalogo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbCatalogo.BackColor = azulPanel;
            gbCatalogo.ForeColor = blanco;
            gbCatalogo.Controls.Add(dgvProductos);

            dgvProductos.Location = new Point(15, 29);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(510, 355);
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.BackgroundColor = blanco;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = azulPanel;
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = blanco;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.DefaultCellStyle.BackColor = blanco;
            dgvProductos.DefaultCellStyle.ForeColor = negro;
            dgvProductos.DefaultCellStyle.SelectionBackColor = amarillo;
            dgvProductos.DefaultCellStyle.SelectionForeColor = negro;
            dgvProductos.RowsDefaultCellStyle.ForeColor = negro;
            dgvProductos.RowsDefaultCellStyle.SelectionForeColor = negro;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[]
            {
                colProductoNombre, colProductoCategoria,
                colProductoPrecio, colProductoStock
            });

            colProductoNombre.DataPropertyName = "Nombre";
            colProductoNombre.Name = "colProductoNombre";
            colProductoNombre.HeaderText = "Producto";
            colProductoNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProductoCategoria.DataPropertyName = "Categoria";
            colProductoCategoria.Name = "colProductoCategoria";
            colProductoCategoria.HeaderText = "Categoría";
            colProductoCategoria.Width = 100;
            colProductoPrecio.DataPropertyName = "PrecioUnitario";
            colProductoPrecio.Name = "colProductoPrecio";
            colProductoPrecio.HeaderText = "Precio";
            colProductoPrecio.Width = 85;
            colProductoPrecio.DefaultCellStyle.Format = "C2";
            colProductoStock.DataPropertyName = "StockDisponible";
            colProductoStock.Name = "colProductoStock";
            colProductoStock.HeaderText = "Stock";
            colProductoStock.Width = 60;

            gbSeleccion.Location = new Point(25, 492);
            gbSeleccion.Name = "gbSeleccion";
            gbSeleccion.Size = new Size(540, 142);
            gbSeleccion.Text = "Seleccionar productos";
            gbSeleccion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbSeleccion.BackColor = azulPanel;
            gbSeleccion.ForeColor = blanco;
            gbSeleccion.Controls.Add(lblCantidad);
            gbSeleccion.Controls.Add(nudCantidad);
            gbSeleccion.Controls.Add(btnAgregar);
            gbSeleccion.Controls.Add(btnModificar);
            gbSeleccion.Controls.Add(btnQuitar);
            gbSeleccion.Controls.Add(btnActualizarProductos);

            lblCantidad.Location = new Point(15, 31);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(93, 25);
            lblCantidad.Text = "Cantidad:";
            lblCantidad.ForeColor = blanco;
            nudCantidad.Location = new Point(116, 31);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(85, 25);
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 100000;
            nudCantidad.Value = 1;
            nudCantidad.BackColor = blanco;
            nudCantidad.ForeColor = negro;

            btnAgregar.Location = new Point(214, 28);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(135, 34);
            btnAgregar.Text = "Agregar";
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.BackColor = amarillo;
            btnAgregar.ForeColor = negro;
            btnAgregar.Click += btnAgregar_Click;

            btnModificar.Location = new Point(359, 28);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(165, 34);
            btnModificar.Text = "Modificar cantidad";
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.FlatAppearance.BorderSize = 0;
            btnModificar.BackColor = amarillo;
            btnModificar.ForeColor = negro;
            btnModificar.Click += btnModificar_Click;

            btnQuitar.Location = new Point(15, 86);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(225, 34);
            btnQuitar.Text = "Quitar producto";
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.FlatAppearance.BorderSize = 1;
            btnQuitar.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnQuitar.BackColor = azulPanel;
            btnQuitar.ForeColor = blanco;
            btnQuitar.Click += btnQuitar_Click;

            btnActualizarProductos.Location = new Point(260, 86);
            btnActualizarProductos.Name = "btnActualizarProductos";
            btnActualizarProductos.Size = new Size(264, 34);
            btnActualizarProductos.Text = "Actualizar catálogo";
            btnActualizarProductos.FlatStyle = FlatStyle.Flat;
            btnActualizarProductos.FlatAppearance.BorderSize = 1;
            btnActualizarProductos.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnActualizarProductos.BackColor = azulPanel;
            btnActualizarProductos.ForeColor = blanco;
            btnActualizarProductos.Click += btnActualizarProductos_Click;

            gbCarrito.Location = new Point(585, 75);
            gbCarrito.Name = "gbCarrito";
            gbCarrito.Size = new Size(540, 400);
            gbCarrito.Text = "Carrito";
            gbCarrito.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbCarrito.BackColor = azulPanel;
            gbCarrito.ForeColor = blanco;
            gbCarrito.Controls.Add(dgvDetalles);

            dgvDetalles.Location = new Point(15, 29);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.Size = new Size(510, 355);
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.ReadOnly = true;
            dgvDetalles.MultiSelect = false;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.BackgroundColor = blanco;
            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = azulPanel;
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = blanco;
            dgvDetalles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.DefaultCellStyle.BackColor = blanco;
            dgvDetalles.DefaultCellStyle.ForeColor = negro;
            dgvDetalles.DefaultCellStyle.SelectionBackColor = amarillo;
            dgvDetalles.DefaultCellStyle.SelectionForeColor = negro;
            dgvDetalles.RowsDefaultCellStyle.ForeColor = negro;
            dgvDetalles.RowsDefaultCellStyle.SelectionForeColor = negro;
            dgvDetalles.Columns.AddRange(new DataGridViewColumn[]
            {
                colDetalleProducto, colDetalleCantidad, colDetallePrecio, colDetalleSubtotal
            });
            dgvDetalles.SelectionChanged += dgvDetalles_SelectionChanged;

            colDetalleProducto.DataPropertyName = "NombreProducto";
            colDetalleProducto.Name = "colDetalleProducto";
            colDetalleProducto.HeaderText = "Producto";
            colDetalleProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetalleCantidad.DataPropertyName = "Cantidad";
            colDetalleCantidad.Name = "colDetalleCantidad";
            colDetalleCantidad.HeaderText = "Cantidad";
            colDetalleCantidad.Width = 75;
            colDetallePrecio.DataPropertyName = "PrecioUnitario";
            colDetallePrecio.Name = "colDetallePrecio";
            colDetallePrecio.HeaderText = "Precio";
            colDetallePrecio.DefaultCellStyle.Format = "C2";
            colDetallePrecio.Width = 100;
            colDetalleSubtotal.DataPropertyName = "Subtotal";
            colDetalleSubtotal.Name = "colDetalleSubtotal";
            colDetalleSubtotal.HeaderText = "Subtotal";
            colDetalleSubtotal.DefaultCellStyle.Format = "C2";
            colDetalleSubtotal.Width = 100;

            lblTituloEstado.Location = new Point(595, 503);
            lblTituloEstado.Name = "lblTituloEstado";
            lblTituloEstado.Size = new Size(80, 25);
            lblTituloEstado.ForeColor = blanco;
            lblTituloEstado.Text = "Estado:";
            lblEstado.Location = new Point(685, 503);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(170, 25);
            lblEstado.ForeColor = amarillo;
            lblEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEstado.Text = "-";

            lblTituloCliente.Location = new Point(595, 540);
            lblTituloCliente.Name = "lblTituloCliente";
            lblTituloCliente.Size = new Size(80, 25);
            lblTituloCliente.ForeColor = blanco;
            lblTituloCliente.Text = "Cliente:";
            lblCliente.Location = new Point(685, 540);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(170, 25);
            lblCliente.ForeColor = blanco;
            lblCliente.Text = "-";

            lblTituloTotal.Location = new Point(865, 495);
            lblTituloTotal.Name = "lblTituloTotal";
            lblTituloTotal.Size = new Size(110, 38);
            lblTituloTotal.ForeColor = blanco;
            lblTituloTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTituloTotal.Text = "Total:";
            lblTotal.Location = new Point(965, 490);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(160, 42);
            lblTotal.ForeColor = amarillo;
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            lblTotal.Text = "$ 0,00";

            btnSeleccionarCliente.Location = new Point(585, 585);
            btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            btnSeleccionarCliente.Size = new Size(240, 49);
            btnSeleccionarCliente.Text = "Seleccionar cliente";
            btnSeleccionarCliente.FlatStyle = FlatStyle.Flat;
            btnSeleccionarCliente.FlatAppearance.BorderSize = 0;
            btnSeleccionarCliente.BackColor = amarillo;
            btnSeleccionarCliente.ForeColor = negro;
            btnSeleccionarCliente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSeleccionarCliente.Enabled = false;
            btnSeleccionarCliente.Click += btnSeleccionarCliente_Click;

            btnCobrar.Location = new Point(840, 585);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(285, 49);
            btnCobrar.Text = "Cobrar venta (próximamente)";
            btnCobrar.FlatStyle = FlatStyle.Flat;
            btnCobrar.BackColor = Color.Silver;
            btnCobrar.ForeColor = negro;
            btnCobrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCobrar.Enabled = false;

            lblMensaje.Location = new Point(25, 652);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(875, 44);
            lblMensaje.ForeColor = amarillo;
            lblMensaje.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;

            btnVolver.Location = new Point(970, 652);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(155, 36);
            btnVolver.Text = "Volver / Cancelar";
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 1;
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnVolver.BackColor = azulPanel;
            btnVolver.ForeColor = blanco;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.Click += btnVolver_Click;

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = azulFondo;
            ForeColor = blanco;
            Font = new Font("Segoe UI", 9.5F);
            ClientSize = new Size(1150, 708);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmGenerarCarrito";
            Text = "PadelGest - Generar Carrito";
            Controls.Add(lblTitulo);
            Controls.Add(gbCatalogo);
            Controls.Add(gbSeleccion);
            Controls.Add(gbCarrito);
            Controls.Add(lblTituloEstado);
            Controls.Add(lblEstado);
            Controls.Add(lblTituloCliente);
            Controls.Add(lblCliente);
            Controls.Add(lblTituloTotal);
            Controls.Add(lblTotal);
            Controls.Add(btnSeleccionarCliente);
            Controls.Add(btnCobrar);
            Controls.Add(lblMensaje);
            Controls.Add(btnVolver);
            FormClosing += frmGenerarCarrito_FormClosing;
            FormClosed += frmGenerarCarrito_FormClosed;
            Load += frmGenerarCarrito_Load;

            gbCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            gbSeleccion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            gbCarrito.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Label lblTitulo;
        private GroupBox gbCatalogo;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn colProductoNombre;
        private DataGridViewTextBoxColumn colProductoCategoria;
        private DataGridViewTextBoxColumn colProductoPrecio;
        private DataGridViewTextBoxColumn colProductoStock;
        private GroupBox gbSeleccion;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private Button btnModificar;
        private Button btnQuitar;
        private Button btnActualizarProductos;
        private GroupBox gbCarrito;
        private DataGridView dgvDetalles;
        private DataGridViewTextBoxColumn colDetalleProducto;
        private DataGridViewTextBoxColumn colDetalleCantidad;
        private DataGridViewTextBoxColumn colDetallePrecio;
        private DataGridViewTextBoxColumn colDetalleSubtotal;
        private Label lblTituloEstado;
        private Label lblEstado;
        private Label lblTituloCliente;
        private Label lblCliente;
        private Label lblTituloTotal;
        private Label lblTotal;
        private Button btnSeleccionarCliente;
        private Button btnCobrar;
        private Button btnVolver;
        private Label lblMensaje;
    }
}
