using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    partial class frmReportePredictivo
    {
        private System.ComponentModel.IContainer? components = null;
        private Label lblTitulo = null!;
        private Label lblPeriodo = null!;
        private Label lblResumen = null!;
        private Label lblNota = null!;
        private DataGridView dgvReporte = null!;
        private Panel pnlGrafico = null!;
        private Button btnActualizar = null!;
        private Button btnVolver = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblPeriodo = new Label();
            lblResumen = new Label();
            lblNota = new Label();
            dgvReporte = new DataGridView();
            pnlGrafico = new Panel();
            btnActualizar = new Button();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            SuspendLayout();

            BackColor = Color.FromArgb(18, 18, 18);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1090, 710);
            MinimumSize = new Size(850, 620);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte Predictivo - Buffet";

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(24, 16);
            lblTitulo.Text = "REPORTE PREDICTIVO DEL BUFFET";

            lblPeriodo.AutoSize = true;
            lblPeriodo.Location = new Point(26, 61);
            lblPeriodo.Text = "Histórico: 28 días completos | Pronóstico: 7 días";

            lblResumen.AutoSize = true;
            lblResumen.ForeColor = Color.FromArgb(214, 246, 36);
            lblResumen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResumen.Location = new Point(26, 91);
            lblResumen.Text = "Productos: 0";

            dgvReporte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReporte.Location = new Point(24, 127);
            dgvReporte.Size = new Size(1042, 265);
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AllowUserToDeleteRows = false;
            dgvReporte.ReadOnly = true;
            dgvReporte.MultiSelect = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.BackgroundColor = Color.White;
            dgvReporte.ForeColor = Color.Black;
            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 70, 138);
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvReporte.EnableHeadersVisualStyles = false;
            dgvReporte.DefaultCellStyle.ForeColor = Color.Black;
            dgvReporte.DefaultCellStyle.BackColor = Color.White;
            dgvReporte.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvReporte.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 242, 249);
            dgvReporte.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProducto", DataPropertyName = "Producto", FillWeight = 155 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategoria", DataPropertyName = "Categoria", FillWeight = 105 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVentas", DataPropertyName = "Vendidas28Dias", FillWeight = 90 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPromedio", DataPropertyName = "PromedioDiario", FillWeight = 94, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" } });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstimacion", DataPropertyName = "Estimadas7Dias", FillWeight = 100 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStock", DataPropertyName = "StockDisponible", FillWeight = 88 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMinimo", DataPropertyName = "StockMinimo", FillWeight = 84 });
            dgvReporte.Columns.Add(new DataGridViewTextBoxColumn { Name = "colReposicion", DataPropertyName = "ReposicionSugerida", FillWeight = 103 });

            pnlGrafico.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlGrafico.Location = new Point(24, 412);
            pnlGrafico.Size = new Size(1042, 188);
            pnlGrafico.BackColor = Color.FromArgb(24, 70, 138);
            pnlGrafico.Paint += pnlGrafico_Paint;

            lblNota.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNota.AutoSize = false;
            lblNota.Location = new Point(24, 608);
            lblNota.Size = new Size(1042, 42);
            lblNota.ForeColor = Color.Gainsboro;
            lblNota.Text = "Estimación orientativa: no reemplaza la decisión de compra.";

            btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Location = new Point(784, 663);
            btnActualizar.Size = new Size(135, 34);
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.BackColor = Color.FromArgb(214, 246, 36);
            btnActualizar.ForeColor = Color.Black;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;

            btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnVolver.Name = "btnVolver";
            btnVolver.Location = new Point(931, 663);
            btnVolver.Size = new Size(135, 34);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.ForeColor = Color.White;
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;

            Controls.Add(lblTitulo);
            Controls.Add(lblPeriodo);
            Controls.Add(lblResumen);
            Controls.Add(dgvReporte);
            Controls.Add(pnlGrafico);
            Controls.Add(lblNota);
            Controls.Add(btnActualizar);
            Controls.Add(btnVolver);
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
