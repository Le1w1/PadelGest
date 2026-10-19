namespace UI
{
    partial class frmGestionProductos
    {
        private System.ComponentModel.IContainer? components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            chkMostrarInactivos = new CheckBox();
            btnActualizar = new Button();
            lblCantidad = new Label();
            dgvProductos = new DataGridView();
            colNombre = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colMinimo = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            gbDatos = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblPrecio = new Label();
            nudPrecio = new NumericUpDown();
            lblMinimo = new Label();
            nudMinimo = new NumericUpDown();
            lblStockInicial = new Label();
            nudStockInicial = new NumericUpDown();
            lblReposicion = new Label();
            nudReposicion = new NumericUpDown();
            btnNuevo = new Button();
            btnCrear = new Button();
            btnModificar = new Button();
            btnEstado = new Button();
            btnReponer = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudReposicion).BeginInit();
            gbDatos.SuspendLayout();
            SuspendLayout();

            Color fondo = Color.FromArgb(14, 43, 92);
            Color panel = Color.FromArgb(24, 70, 138);
            Color amarillo = Color.FromArgb(214, 246, 36);

            lblTitulo.Location = new Point(25, 14);
            lblTitulo.Size = new Size(960, 45);
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = amarillo;
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "GESTION DE PRODUCTOS - BUFFET";

            lblBuscar.Location = new Point(25, 77);
            lblBuscar.Size = new Size(68, 27);
            lblBuscar.Text = "Buscar:";
            lblBuscar.Name = "lblBuscar";
            txtBuscar.Location = new Point(98, 73);
            txtBuscar.Size = new Size(410, 28);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.ForeColor = Color.Black;
            txtBuscar.BackColor = Color.White;
            txtBuscar.MaxLength = 120;
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            chkMostrarInactivos.Location = new Point(528, 76);
            chkMostrarInactivos.Size = new Size(205, 26);
            chkMostrarInactivos.Name = "chkMostrarInactivos";
            chkMostrarInactivos.Checked = true;
            chkMostrarInactivos.ForeColor = Color.White;
            chkMostrarInactivos.Text = "Mostrar inactivos";
            chkMostrarInactivos.CheckedChanged += chkMostrarInactivos_CheckedChanged;

            btnActualizar.Location = new Point(775, 70);
            btnActualizar.Size = new Size(190, 36);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Text = "Actualizar catálogo";
            btnActualizar.BackColor = panel;
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Click += btnActualizar_Click;

            lblCantidad.Location = new Point(985, 77);
            lblCantidad.Size = new Size(155, 27);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.ForeColor = amarillo;
            lblCantidad.TextAlign = ContentAlignment.MiddleRight;

            dgvProductos.Location = new Point(25, 117);
            dgvProductos.Size = new Size(1115, 285);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersHeight = 32;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = panel;
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvProductos.DefaultCellStyle.BackColor = Color.White;
            dgvProductos.DefaultCellStyle.ForeColor = Color.Black;
            dgvProductos.DefaultCellStyle.SelectionBackColor = amarillo;
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProductos.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvProductos.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[]
            {
                colNombre, colCategoria, colPrecio, colStock, colMinimo, colEstado
            });
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;

            colNombre.Name = "colNombre";
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Producto";
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.MinimumWidth = 240;
            colCategoria.Name = "colCategoria";
            colCategoria.DataPropertyName = "Categoria";
            colCategoria.HeaderText = "Categoría";
            colCategoria.Width = 160;
            colPrecio.Name = "colPrecio";
            colPrecio.DataPropertyName = "PrecioUnitario";
            colPrecio.HeaderText = "Precio";
            colPrecio.Width = 135;
            colPrecio.DefaultCellStyle.Format = "C2";
            colStock.Name = "colStock";
            colStock.DataPropertyName = "StockDisponible";
            colStock.HeaderText = "Stock";
            colStock.Width = 100;
            colMinimo.Name = "colMinimo";
            colMinimo.DataPropertyName = "StockMinimo";
            colMinimo.HeaderText = "Mínimo";
            colMinimo.Width = 100;
            colEstado.Name = "colEstado";
            colEstado.HeaderText = "Estado";
            colEstado.Width = 170;

            gbDatos.Location = new Point(25, 421);
            gbDatos.Size = new Size(1115, 172);
            gbDatos.Name = "gbDatos";
            gbDatos.Text = "Datos del producto";
            gbDatos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbDatos.BackColor = panel;
            gbDatos.ForeColor = Color.White;
            gbDatos.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre, lblCategoria, cmbCategoria,
                lblPrecio, nudPrecio, lblMinimo, nudMinimo,
                lblStockInicial, nudStockInicial, lblReposicion, nudReposicion
            });

            lblNombre.Location = new Point(16, 32);
            lblNombre.Size = new Size(92, 26);
            lblNombre.Text = "Nombre:";
            lblNombre.Name = "lblNombre";
            txtNombre.Location = new Point(114, 31);
            txtNombre.Size = new Size(298, 29);
            txtNombre.MaxLength = 120;
            txtNombre.BackColor = Color.White;
            txtNombre.ForeColor = Color.Black;
            txtNombre.Name = "txtNombre";

            lblCategoria.Location = new Point(436, 32);
            lblCategoria.Size = new Size(100, 26);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Text = "Categoría:";
            cmbCategoria.Location = new Point(536, 31);
            cmbCategoria.Size = new Size(258, 29);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCategoria.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCategoria.BackColor = Color.White;
            cmbCategoria.ForeColor = Color.Black;
            cmbCategoria.MaxLength = 80;

            lblPrecio.Location = new Point(813, 32);
            lblPrecio.Size = new Size(76, 26);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Text = "Precio:";
            nudPrecio.Location = new Point(896, 31);
            nudPrecio.Size = new Size(195, 29);
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Increment = 100M;
            nudPrecio.Minimum = 0.01M;
            nudPrecio.Maximum = 99999999.99M;
            nudPrecio.ThousandsSeparator = true;
            nudPrecio.BackColor = Color.White;
            nudPrecio.ForeColor = Color.Black;
            nudPrecio.Name = "nudPrecio";

            lblMinimo.Location = new Point(16, 103);
            lblMinimo.Size = new Size(115, 28);
            lblMinimo.Name = "lblMinimo";
            lblMinimo.Text = "Stock mínimo:";
            nudMinimo.Location = new Point(134, 102);
            nudMinimo.Size = new Size(128, 29);
            nudMinimo.Maximum = 2147483647M;
            nudMinimo.BackColor = Color.White;
            nudMinimo.ForeColor = Color.Black;
            nudMinimo.Name = "nudMinimo";

            lblStockInicial.Location = new Point(282, 103);
            lblStockInicial.Size = new Size(130, 28);
            lblStockInicial.Name = "lblStockInicial";
            lblStockInicial.Text = "Stock inicial:";
            nudStockInicial.Location = new Point(419, 102);
            nudStockInicial.Size = new Size(130, 29);
            nudStockInicial.Maximum = 2147483647M;
            nudStockInicial.BackColor = Color.White;
            nudStockInicial.ForeColor = Color.Black;
            nudStockInicial.Name = "nudStockInicial";

            lblReposicion.Location = new Point(579, 103);
            lblReposicion.Size = new Size(158, 28);
            lblReposicion.Name = "lblReposicion";
            lblReposicion.Text = "Unidades a reponer:";
            nudReposicion.Location = new Point(746, 102);
            nudReposicion.Size = new Size(130, 29);
            nudReposicion.Minimum = 1;
            nudReposicion.Maximum = 2147483647M;
            nudReposicion.Value = 1;
            nudReposicion.BackColor = Color.White;
            nudReposicion.ForeColor = Color.Black;
            nudReposicion.Name = "nudReposicion";

            btnNuevo.Location = new Point(25, 614);
            btnNuevo.Size = new Size(152, 42);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Text = "Nuevo";
            btnNuevo.Click += btnNuevo_Click;
            btnCrear.Location = new Point(190, 614);
            btnCrear.Size = new Size(152, 42);
            btnCrear.Name = "btnCrear";
            btnCrear.Text = "Crear";
            btnCrear.Click += btnCrear_Click;
            btnModificar.Location = new Point(355, 614);
            btnModificar.Size = new Size(172, 42);
            btnModificar.Name = "btnModificar";
            btnModificar.Text = "Guardar cambios";
            btnModificar.Click += btnModificar_Click;
            btnEstado.Location = new Point(540, 614);
            btnEstado.Size = new Size(172, 42);
            btnEstado.Name = "btnEstado";
            btnEstado.Text = "Desactivar";
            btnEstado.Click += btnEstado_Click;
            btnReponer.Location = new Point(725, 614);
            btnReponer.Size = new Size(172, 42);
            btnReponer.Name = "btnReponer";
            btnReponer.Text = "Reponer stock";
            btnReponer.Click += btnReponer_Click;
            btnVolver.Location = new Point(986, 614);
            btnVolver.Size = new Size(154, 42);
            btnVolver.Name = "btnVolver";
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            foreach (Button boton in new[] { btnNuevo, btnCrear, btnModificar, btnEstado, btnReponer, btnVolver })
            {
                boton.BackColor = amarillo;
                boton.ForeColor = Color.Black;
                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            btnVolver.BackColor = panel;
            btnVolver.ForeColor = Color.White;

            lblMensaje.Location = new Point(25, 666);
            lblMensaje.Size = new Size(1115, 40);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.ForeColor = amarillo;

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1165, 711);
            BackColor = fondo;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmGestionProductos";
            Text = "PadelGest - Gestion Productos";
            Controls.AddRange(new Control[]
            {
                lblTitulo, lblBuscar, txtBuscar, chkMostrarInactivos, btnActualizar,
                lblCantidad, dgvProductos, gbDatos, btnNuevo, btnCrear, btnModificar,
                btnEstado, btnReponer, btnVolver, lblMensaje
            });
            Load += frmGestionProductos_Load;
            FormClosed += frmGestionProductos_FormClosed;

            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudReposicion).EndInit();
            gbDatos.ResumeLayout(false);
            gbDatos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private CheckBox chkMostrarInactivos;
        private Button btnActualizar;
        private Label lblCantidad;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCategoria;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colMinimo;
        private DataGridViewTextBoxColumn colEstado;
        private GroupBox gbDatos;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblPrecio;
        private NumericUpDown nudPrecio;
        private Label lblMinimo;
        private NumericUpDown nudMinimo;
        private Label lblStockInicial;
        private NumericUpDown nudStockInicial;
        private Label lblReposicion;
        private NumericUpDown nudReposicion;
        private Button btnNuevo;
        private Button btnCrear;
        private Button btnModificar;
        private Button btnEstado;
        private Button btnReponer;
        private Button btnVolver;
        private Label lblMensaje;
    }
}
