namespace UI
{
    partial class frmFacturaRegistrada
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
            gbDatos = new GroupBox();
            lblNumeroTitulo = new Label();
            lblNumeroValor = new Label();
            lblFechaTitulo = new Label();
            lblFechaValor = new Label();
            lblClienteTitulo = new Label();
            lblClienteValor = new Label();
            lblDniTitulo = new Label();
            lblDniValor = new Label();
            lblBancoTitulo = new Label();
            lblBancoValor = new Label();
            lblTotalTitulo = new Label();
            lblTotalValor = new Label();
            dgvDetalles = new DataGridView();
            colProducto = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            lblMensaje = new Label();
            btnExportarPdf = new Button();
            btnFinalizar = new Button();
            gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 17);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(700, 46);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Factura registrada";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbDatos
            // 
            gbDatos.BackColor = Color.FromArgb(24, 70, 138);
            gbDatos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbDatos.ForeColor = Color.White;
            gbDatos.Location = new Point(20, 79);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(700, 421);
            gbDatos.TabIndex = 1;
            gbDatos.TabStop = false;
            gbDatos.Text = "Detalle de la factura";
            gbDatos.Controls.AddRange(new Control[] {
                lblNumeroTitulo, lblNumeroValor, lblFechaTitulo, lblFechaValor,
                lblClienteTitulo, lblClienteValor, lblDniTitulo, lblDniValor,
                lblBancoTitulo, lblBancoValor, lblTotalTitulo, lblTotalValor,
                dgvDetalles
            });
            // 
            // Campos de la factura
            // 
            lblNumeroTitulo.Location = new Point(24, 35);
            lblNumeroTitulo.Name = "lblNumeroTitulo";
            lblNumeroTitulo.Size = new Size(140, 23);
            lblNumeroTitulo.Text = "N° de factura:";
            lblNumeroValor.Location = new Point(163, 35);
            lblNumeroValor.Name = "lblNumeroValor";
            lblNumeroValor.Size = new Size(480, 23);
            lblNumeroValor.Text = "-";

            lblFechaTitulo.Location = new Point(24, 68);
            lblFechaTitulo.Name = "lblFechaTitulo";
            lblFechaTitulo.Size = new Size(140, 23);
            lblFechaTitulo.Text = "Fecha:";
            lblFechaValor.Location = new Point(163, 68);
            lblFechaValor.Name = "lblFechaValor";
            lblFechaValor.Size = new Size(480, 23);
            lblFechaValor.Text = "-";

            lblClienteTitulo.Location = new Point(24, 101);
            lblClienteTitulo.Name = "lblClienteTitulo";
            lblClienteTitulo.Size = new Size(140, 23);
            lblClienteTitulo.Text = "Cliente:";
            lblClienteValor.Location = new Point(163, 101);
            lblClienteValor.Name = "lblClienteValor";
            lblClienteValor.Size = new Size(480, 23);
            lblClienteValor.Text = "-";

            lblDniTitulo.Location = new Point(24, 134);
            lblDniTitulo.Name = "lblDniTitulo";
            lblDniTitulo.Size = new Size(140, 23);
            lblDniTitulo.Text = "DNI:";
            lblDniValor.Location = new Point(163, 134);
            lblDniValor.Name = "lblDniValor";
            lblDniValor.Size = new Size(480, 23);
            lblDniValor.Text = "-";
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.BorderStyle = BorderStyle.FixedSingle;
            dgvDetalles.ColumnHeadersHeight = 29;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDetalles.Columns.AddRange(new DataGridViewColumn[] {
                colProducto, colCantidad, colPrecio, colSubtotal
            });
            dgvDetalles.DefaultCellStyle.BackColor = Color.White;
            dgvDetalles.DefaultCellStyle.ForeColor = Color.Black;
            dgvDetalles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvDetalles.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.GridColor = Color.FromArgb(210, 215, 220);
            dgvDetalles.Location = new Point(23, 173);
            dgvDetalles.MultiSelect = false;
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.RowTemplate.Height = 28;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.Size = new Size(654, 135);
            dgvDetalles.TabIndex = 0;
            // 
            // Columnas
            // 
            colProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProducto.HeaderText = "Producto";
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            colCantidad.Width = 75;
            colCantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrecio.HeaderText = "Precio unitario";
            colPrecio.Name = "colPrecio";
            colPrecio.ReadOnly = true;
            colPrecio.Width = 135;
            colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrecio.DefaultCellStyle.Format = "C2";
            colSubtotal.HeaderText = "Subtotal";
            colSubtotal.Name = "colSubtotal";
            colSubtotal.ReadOnly = true;
            colSubtotal.Width = 135;
            colSubtotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSubtotal.DefaultCellStyle.Format = "C2";
            // 
            // Pago y total
            // 
            lblBancoTitulo.Location = new Point(24, 324);
            lblBancoTitulo.Name = "lblBancoTitulo";
            lblBancoTitulo.Size = new Size(140, 25);
            lblBancoTitulo.Text = "Pago:";
            lblBancoValor.Location = new Point(163, 324);
            lblBancoValor.Name = "lblBancoValor";
            lblBancoValor.Size = new Size(480, 25);
            lblBancoValor.Text = "-";
            lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalTitulo.Location = new Point(365, 369);
            lblTotalTitulo.Name = "lblTotalTitulo";
            lblTotalTitulo.Size = new Size(125, 29);
            lblTotalTitulo.Text = "Abonado:";
            lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalValor.Location = new Point(485, 369);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(185, 29);
            lblTotalValor.Text = "-";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(20, 510);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(700, 31);
            lblMensaje.TabIndex = 2;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExportarPdf
            // 
            btnExportarPdf.BackColor = Color.FromArgb(24, 70, 138);
            btnExportarPdf.FlatAppearance.BorderColor = Color.FromArgb(101, 141, 199);
            btnExportarPdf.FlatStyle = FlatStyle.Flat;
            btnExportarPdf.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExportarPdf.ForeColor = Color.White;
            btnExportarPdf.Location = new Point(355, 550);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(185, 34);
            btnExportarPdf.TabIndex = 3;
            btnExportarPdf.Text = "Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = false;
            btnExportarPdf.Click += btnExportarPdf_Click;
            // 
            // btnFinalizar
            // 
            btnFinalizar.BackColor = Color.FromArgb(214, 246, 36);
            btnFinalizar.FlatAppearance.BorderSize = 0;
            btnFinalizar.FlatStyle = FlatStyle.Flat;
            btnFinalizar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFinalizar.ForeColor = Color.FromArgb(18, 18, 18);
            btnFinalizar.Location = new Point(560, 550);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(160, 34);
            btnFinalizar.TabIndex = 4;
            btnFinalizar.Text = "Finalizar";
            btnFinalizar.UseVisualStyleBackColor = false;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // frmFacturaRegistrada
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(740, 600);
            ControlBox = false;
            Controls.Add(lblTitulo);
            Controls.Add(gbDatos);
            Controls.Add(lblMensaje);
            Controls.Add(btnExportarPdf);
            Controls.Add(btnFinalizar);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmFacturaRegistrada";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Factura registrada";
            FormClosed += frmFacturaRegistrada_FormClosed;
            Load += frmFacturaRegistrada_Load;
            gbDatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Label lblTitulo;
        private GroupBox gbDatos;
        private Label lblNumeroTitulo;
        private Label lblNumeroValor;
        private Label lblFechaTitulo;
        private Label lblFechaValor;
        private Label lblClienteTitulo;
        private Label lblClienteValor;
        private Label lblDniTitulo;
        private Label lblDniValor;
        private Label lblBancoTitulo;
        private Label lblBancoValor;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private DataGridView dgvDetalles;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colSubtotal;
        private Label lblMensaje;
        private Button btnExportarPdf;
        private Button btnFinalizar;
    }
}
