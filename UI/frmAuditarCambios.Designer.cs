namespace UI
{
    partial class frmAuditarCambios
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblCodigo = new Label();
            nudCodigo = new NumericUpDown();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            chkSoloVigentes = new CheckBox();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            lblCantidad = new Label();
            dgvCambios = new DataGridView();
            colIdCambio = new DataGridViewTextBoxColumn();
            colCodigo = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colMinimo = new DataGridViewTextBoxColumn();
            colActivo = new DataGridViewTextBoxColumn();
            colVigente = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            btnVolver = new Button();
            btnRestaurar = new Button();
            lblAviso = new Label();

            ((System.ComponentModel.ISupportInitialize)nudCodigo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCambios).BeginInit();
            SuspendLayout();

            lblTitulo.Location = new Point(25, 12);
            lblTitulo.Size = new Size(1120, 45);
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "BITACORA DE CAMBIOS - PRODUCTOS";

            lblCodigo.Location = new Point(25, 75);
            lblCodigo.Size = new Size(70, 27);
            lblCodigo.ForeColor = Color.White;
            lblCodigo.Text = "Código:";
            lblCodigo.Name = "lblCodigo";

            nudCodigo.Location = new Point(105, 72);
            nudCodigo.Size = new Size(120, 28);
            nudCodigo.Minimum = 0;
            nudCodigo.Maximum = int.MaxValue;
            nudCodigo.BackColor = Color.White;
            nudCodigo.ForeColor = Color.Black;
            nudCodigo.Name = "nudCodigo";

            lblNombre.Location = new Point(248, 75);
            lblNombre.Size = new Size(72, 27);
            lblNombre.ForeColor = Color.White;
            lblNombre.Text = "Nombre:";
            lblNombre.Name = "lblNombre";

            txtNombre.Location = new Point(328, 72);
            txtNombre.Size = new Size(280, 28);
            txtNombre.MaxLength = 120;
            txtNombre.BackColor = Color.White;
            txtNombre.ForeColor = Color.Black;
            txtNombre.Name = "txtNombre";

            lblDesde.Location = new Point(25, 124);
            lblDesde.Size = new Size(100, 26);
            lblDesde.ForeColor = Color.White;
            lblDesde.Text = "Fecha inicial:";
            lblDesde.Name = "lblDesde";

            dtpDesde.Location = new Point(127, 121);
            dtpDesde.Size = new Size(165, 28);
            dtpDesde.Format = DateTimePickerFormat.Custom;
            dtpDesde.CustomFormat = "dd/MM/yyyy";
            dtpDesde.ShowCheckBox = true;
            dtpDesde.Checked = false;
            dtpDesde.CalendarForeColor = Color.Black;
            dtpDesde.Name = "dtpDesde";

            lblHasta.Location = new Point(310, 124);
            lblHasta.Size = new Size(95, 26);
            lblHasta.ForeColor = Color.White;
            lblHasta.Text = "Fecha final:";
            lblHasta.Name = "lblHasta";

            dtpHasta.Location = new Point(405, 121);
            dtpHasta.Size = new Size(165, 28);
            dtpHasta.Format = DateTimePickerFormat.Custom;
            dtpHasta.CustomFormat = "dd/MM/yyyy";
            dtpHasta.ShowCheckBox = true;
            dtpHasta.Checked = false;
            dtpHasta.CalendarForeColor = Color.Black;
            dtpHasta.Name = "dtpHasta";

            chkSoloVigentes.Location = new Point(638, 75);
            chkSoloVigentes.Size = new Size(232, 27);
            chkSoloVigentes.ForeColor = Color.White;
            chkSoloVigentes.Text = "Solo versiones vigentes";
            chkSoloVigentes.Name = "chkSoloVigentes";

            btnBuscar.Location = new Point(910, 70);
            btnBuscar.Size = new Size(115, 37);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;

            btnLimpiar.Location = new Point(1040, 70);
            btnLimpiar.Size = new Size(135, 37);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;

            lblCantidad.Location = new Point(785, 153);
            lblCantidad.Size = new Size(390, 25);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.ForeColor = Color.FromArgb(214, 246, 36);
            lblCantidad.TextAlign = ContentAlignment.MiddleRight;
            lblCantidad.Text = "Registros: 0";

            dgvCambios.Location = new Point(25, 187);
            dgvCambios.Size = new Size(1150, 420);
            dgvCambios.Name = "dgvCambios";
            dgvCambios.BackgroundColor = Color.White;
            dgvCambios.BorderStyle = BorderStyle.None;
            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.AllowUserToAddRows = false;
            dgvCambios.AllowUserToDeleteRows = false;
            dgvCambios.AllowUserToResizeRows = false;
            dgvCambios.ReadOnly = true;
            dgvCambios.MultiSelect = false;
            dgvCambios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCambios.RowHeadersVisible = false;
            dgvCambios.EnableHeadersVisualStyles = false;
            dgvCambios.ColumnHeadersHeight = 32;
            dgvCambios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 70, 138);
            dgvCambios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCambios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCambios.DefaultCellStyle.BackColor = Color.White;
            dgvCambios.DefaultCellStyle.ForeColor = Color.Black;
            dgvCambios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvCambios.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCambios.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCambios.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCambios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 242, 250);
            dgvCambios.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCambios.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCambios.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCambios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCambios.Columns.AddRange(new DataGridViewColumn[]
            {
                colIdCambio, colCodigo, colFecha, colNombre, colCategoria,
                colPrecio, colStock, colMinimo, colActivo, colVigente, colTipo
            });
            dgvCambios.CellFormatting += dgvCambios_CellFormatting;
            dgvCambios.SelectionChanged += dgvCambios_SelectionChanged;

            colIdCambio.Name = "colIdCambio";
            colIdCambio.DataPropertyName = "IdCambio";
            colIdCambio.HeaderText = "Cambio";
            colIdCambio.Width = 74;

            colCodigo.Name = "colCodigo";
            colCodigo.DataPropertyName = "CodigoProducto";
            colCodigo.HeaderText = "Código";
            colCodigo.Width = 82;

            colFecha.Name = "colFecha";
            colFecha.DataPropertyName = "FechaHora";
            colFecha.HeaderText = "Fecha y hora";
            colFecha.Width = 146;
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            colNombre.Name = "colNombre";
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Producto";
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.MinimumWidth = 165;

            colCategoria.Name = "colCategoria";
            colCategoria.DataPropertyName = "Categoria";
            colCategoria.HeaderText = "Categoría";
            colCategoria.Width = 118;

            colPrecio.Name = "colPrecio";
            colPrecio.DataPropertyName = "PrecioUnitario";
            colPrecio.HeaderText = "Precio";
            colPrecio.Width = 105;
            colPrecio.DefaultCellStyle.Format = "C2";

            colStock.Name = "colStock";
            colStock.DataPropertyName = "StockDisponible";
            colStock.HeaderText = "Stock";
            colStock.Width = 65;

            colMinimo.Name = "colMinimo";
            colMinimo.DataPropertyName = "StockMinimo";
            colMinimo.HeaderText = "Mínimo";
            colMinimo.Width = 75;

            colActivo.Name = "colActivo";
            colActivo.DataPropertyName = "ActivoProducto";
            colActivo.HeaderText = "Estado";
            colActivo.Width = 87;

            colVigente.Name = "colVigente";
            colVigente.DataPropertyName = "Act";
            colVigente.HeaderText = "Versión";
            colVigente.Width = 90;

            colTipo.Name = "colTipo";
            colTipo.DataPropertyName = "TipoCambio";
            colTipo.HeaderText = "Operación";
            colTipo.Width = 116;

            lblAviso.Location = new Point(25, 620);
            lblAviso.Size = new Size(750, 45);
            lblAviso.Name = "lblAviso";
            lblAviso.ForeColor = Color.White;
            lblAviso.Text = "Historial de solo lectura. Los cambios se registran automáticamente al modificar productos.";

            btnRestaurar.Location = new Point(790, 630);
            btnRestaurar.Size = new Size(205, 42);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Text = "Restaurar versión";
            btnRestaurar.Enabled = false;
            btnRestaurar.BackColor = Color.FromArgb(214, 246, 36);
            btnRestaurar.ForeColor = Color.Black;
            btnRestaurar.FlatStyle = FlatStyle.Flat;
            btnRestaurar.FlatAppearance.BorderSize = 0;
            btnRestaurar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRestaurar.Click += btnRestaurar_Click;

            btnVolver.Location = new Point(1015, 630);
            btnVolver.Size = new Size(160, 42);
            btnVolver.Name = "btnVolver";
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            btnBuscar.BackColor = Color.FromArgb(214, 246, 36);
            btnBuscar.ForeColor = Color.Black;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.BackColor = Color.FromArgb(214, 246, 36);
            btnLimpiar.ForeColor = Color.Black;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVolver.BackColor = Color.FromArgb(214, 246, 36);
            btnVolver.ForeColor = Color.Black;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.ForeColor = Color.White;

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 690);
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "frmAuditarCambios";
            Text = "PadelGest - Bitácora de Cambios";
            Controls.AddRange(new Control[]
            {
                lblTitulo, lblCodigo, nudCodigo, lblNombre, txtNombre,
                lblDesde, dtpDesde, lblHasta, dtpHasta, chkSoloVigentes,
                btnBuscar, btnLimpiar, lblCantidad, dgvCambios, lblAviso, btnRestaurar, btnVolver
            });
            Load += frmAuditarCambios_Load;
            FormClosed += frmAuditarCambios_FormClosed;

            ((System.ComponentModel.ISupportInitialize)nudCodigo).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCambios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblCodigo;
        private NumericUpDown nudCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private CheckBox chkSoloVigentes;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Label lblCantidad;
        private DataGridView dgvCambios;
        private DataGridViewTextBoxColumn colIdCambio;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCategoria;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colMinimo;
        private DataGridViewTextBoxColumn colActivo;
        private DataGridViewTextBoxColumn colVigente;
        private DataGridViewTextBoxColumn colTipo;
        private Label lblAviso;
        private Button btnRestaurar;
        private Button btnVolver;
    }
}
