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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            lblTitulo = new Label();
            gbCatalogo = new GroupBox();
            dgvProductos = new DataGridView();
            gbSeleccion = new GroupBox();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnQuitar = new Button();
            btnActualizarProductos = new Button();
            gbCarrito = new GroupBox();
            dgvDetalles = new DataGridView();
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
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(25, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(700, 46);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Generar Carrito";
            // 
            // gbCatalogo
            // 
            gbCatalogo.BackColor = Color.FromArgb(24, 70, 138);
            gbCatalogo.Controls.Add(dgvProductos);
            gbCatalogo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbCatalogo.ForeColor = Color.White;
            gbCatalogo.Location = new Point(25, 75);
            gbCatalogo.Name = "gbCatalogo";
            gbCatalogo.Size = new Size(540, 400);
            gbCatalogo.TabIndex = 1;
            gbCatalogo.TabStop = false;
            gbCatalogo.Text = "Productos disponibles";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(24, 70, 138);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvProductos.DefaultCellStyle = dataGridViewCellStyle2;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.Location = new Point(15, 29);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dgvProductos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(510, 355);
            dgvProductos.TabIndex = 0;
            // 
            // gbSeleccion
            // 
            gbSeleccion.BackColor = Color.FromArgb(24, 70, 138);
            gbSeleccion.Controls.Add(lblCantidad);
            gbSeleccion.Controls.Add(nudCantidad);
            gbSeleccion.Controls.Add(btnAgregar);
            gbSeleccion.Controls.Add(btnModificar);
            gbSeleccion.Controls.Add(btnQuitar);
            gbSeleccion.Controls.Add(btnActualizarProductos);
            gbSeleccion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbSeleccion.ForeColor = Color.White;
            gbSeleccion.Location = new Point(25, 492);
            gbSeleccion.Name = "gbSeleccion";
            gbSeleccion.Size = new Size(540, 157);
            gbSeleccion.TabIndex = 2;
            gbSeleccion.TabStop = false;
            gbSeleccion.Text = "Seleccionar productos";
            // 
            // lblCantidad
            // 
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Location = new Point(15, 31);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(93, 25);
            lblCantidad.TabIndex = 0;
            lblCantidad.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            nudCantidad.BackColor = Color.White;
            nudCantidad.ForeColor = Color.Black;
            nudCantidad.Location = new Point(116, 31);
            nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(85, 25);
            nudCantidad.TabIndex = 1;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(214, 246, 36);
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(214, 28);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(135, 52);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar al carrito";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(214, 246, 36);
            btnModificar.FlatAppearance.BorderSize = 0;
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.ForeColor = Color.Black;
            btnModificar.Location = new Point(359, 28);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(165, 52);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar cantidad";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnQuitar
            // 
            btnQuitar.BackColor = Color.FromArgb(24, 70, 138);
            btnQuitar.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.ForeColor = Color.White;
            btnQuitar.Location = new Point(15, 93);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(225, 34);
            btnQuitar.TabIndex = 4;
            btnQuitar.Text = "Quitar producto";
            btnQuitar.UseVisualStyleBackColor = false;
            btnQuitar.Click += btnQuitar_Click;
            // 
            // btnActualizarProductos
            // 
            btnActualizarProductos.BackColor = Color.FromArgb(24, 70, 138);
            btnActualizarProductos.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnActualizarProductos.FlatStyle = FlatStyle.Flat;
            btnActualizarProductos.ForeColor = Color.White;
            btnActualizarProductos.Location = new Point(260, 93);
            btnActualizarProductos.Name = "btnActualizarProductos";
            btnActualizarProductos.Size = new Size(264, 34);
            btnActualizarProductos.TabIndex = 5;
            btnActualizarProductos.Text = "Actualizar catálogo";
            btnActualizarProductos.UseVisualStyleBackColor = false;
            btnActualizarProductos.Click += btnActualizarProductos_Click;
            // 
            // gbCarrito
            // 
            gbCarrito.BackColor = Color.FromArgb(24, 70, 138);
            gbCarrito.Controls.Add(dgvDetalles);
            gbCarrito.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbCarrito.ForeColor = Color.White;
            gbCarrito.Location = new Point(585, 75);
            gbCarrito.Name = "gbCarrito";
            gbCarrito.Size = new Size(540, 400);
            gbCarrito.TabIndex = 3;
            gbCarrito.TabStop = false;
            gbCarrito.Text = "Carrito";
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(24, 70, 138);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvDetalles.DefaultCellStyle = dataGridViewCellStyle5;
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.Location = new Point(15, 29);
            dgvDetalles.MultiSelect = false;
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dgvDetalles.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.Size = new Size(510, 355);
            dgvDetalles.TabIndex = 0;
            dgvDetalles.SelectionChanged += dgvDetalles_SelectionChanged;
            // 
            // lblTituloEstado
            // 
            lblTituloEstado.ForeColor = Color.White;
            lblTituloEstado.Location = new Point(595, 503);
            lblTituloEstado.Name = "lblTituloEstado";
            lblTituloEstado.Size = new Size(80, 25);
            lblTituloEstado.TabIndex = 4;
            lblTituloEstado.Text = "Estado:";
            // 
            // lblEstado
            // 
            lblEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(214, 246, 36);
            lblEstado.Location = new Point(685, 503);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(170, 25);
            lblEstado.TabIndex = 5;
            lblEstado.Text = "-";
            // 
            // lblTituloCliente
            // 
            lblTituloCliente.ForeColor = Color.White;
            lblTituloCliente.Location = new Point(595, 540);
            lblTituloCliente.Name = "lblTituloCliente";
            lblTituloCliente.Size = new Size(80, 25);
            lblTituloCliente.TabIndex = 6;
            lblTituloCliente.Text = "Cliente:";
            // 
            // lblCliente
            // 
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(685, 540);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(170, 25);
            lblCliente.TabIndex = 7;
            lblCliente.Text = "-";
            // 
            // lblTituloTotal
            // 
            lblTituloTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTituloTotal.ForeColor = Color.White;
            lblTituloTotal.Location = new Point(865, 495);
            lblTituloTotal.Name = "lblTituloTotal";
            lblTituloTotal.Size = new Size(110, 38);
            lblTituloTotal.TabIndex = 8;
            lblTituloTotal.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(214, 246, 36);
            lblTotal.Location = new Point(965, 490);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(160, 42);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "$ 0,00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSeleccionarCliente
            // 
            btnSeleccionarCliente.BackColor = Color.FromArgb(214, 246, 36);
            btnSeleccionarCliente.Enabled = false;
            btnSeleccionarCliente.FlatAppearance.BorderSize = 0;
            btnSeleccionarCliente.FlatStyle = FlatStyle.Flat;
            btnSeleccionarCliente.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSeleccionarCliente.ForeColor = Color.Black;
            btnSeleccionarCliente.Location = new Point(585, 600);
            btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            btnSeleccionarCliente.Size = new Size(240, 49);
            btnSeleccionarCliente.TabIndex = 10;
            btnSeleccionarCliente.Text = "Seleccionar cliente";
            btnSeleccionarCliente.UseVisualStyleBackColor = false;
            btnSeleccionarCliente.Click += btnSeleccionarCliente_Click;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.Silver;
            btnCobrar.Enabled = false;
            btnCobrar.FlatStyle = FlatStyle.Flat;
            btnCobrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCobrar.ForeColor = Color.Black;
            btnCobrar.Location = new Point(840, 597);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(285, 49);
            btnCobrar.TabIndex = 11;
            btnCobrar.Text = "Cobrar venta (próximamente)";
            btnCobrar.UseVisualStyleBackColor = false;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(970, 660);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(155, 36);
            btnVolver.TabIndex = 13;
            btnVolver.Text = "Volver / Cancelar";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 10F);
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(25, 652);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(875, 44);
            lblMensaje.TabIndex = 12;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmGenerarCarrito
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(1150, 708);
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
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmGenerarCarrito";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Generar Carrito";
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
