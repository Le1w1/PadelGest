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
        private Label lblAnalisis = null!;
        private Label lblReposicion = null!;
        private Label lblCategorias = null!;
        private Label kpiVentas = null!;
        private Label kpiEstimacion = null!;
        private Label kpiAlertas = null!;
        private Label kpiReponer = null!;
        private TabControl tabs = null!;
        private TabPage tabPanorama = null!;
        private TabPage tabProductos = null!;
        private TabPage tabReposicion = null!;
        private TabPage tabCategorias = null!;
        private Panel pnlGrafico = null!;
        private Button btnActualizar = null!;
        private Button btnVolver = null!;
        private DataGridView dgvReporte = null!;
        private DataGridView dgvReposicion = null!;
        private DataGridView dgvCategorias = null!;
        private DataGridViewTextBoxColumn colProducto = null!;
        private DataGridViewTextBoxColumn colCategoria = null!;
        private DataGridViewTextBoxColumn colVentas = null!;
        private DataGridViewTextBoxColumn colPromedio = null!;
        private DataGridViewTextBoxColumn colEstimacion = null!;
        private DataGridViewTextBoxColumn colStock = null!;
        private DataGridViewTextBoxColumn colMinimo = null!;
        private DataGridViewTextBoxColumn colCobertura = null!;
        private DataGridViewTextBoxColumn colReposicion = null!;
        private DataGridViewTextBoxColumn colPrioridad = null!;
        private DataGridViewTextBoxColumn colIngreso = null!;
        private DataGridViewTextBoxColumn repProducto = null!;
        private DataGridViewTextBoxColumn repCategoria = null!;
        private DataGridViewTextBoxColumn repPrioridad = null!;
        private DataGridViewTextBoxColumn repStock = null!;
        private DataGridViewTextBoxColumn repMinimo = null!;
        private DataGridViewTextBoxColumn repEstimacion = null!;
        private DataGridViewTextBoxColumn repCobertura = null!;
        private DataGridViewTextBoxColumn repCantidad = null!;
        private DataGridViewTextBoxColumn catNombre = null!;
        private DataGridViewTextBoxColumn catProductos = null!;
        private DataGridViewTextBoxColumn catVentas = null!;
        private DataGridViewTextBoxColumn catEstimacion = null!;
        private DataGridViewTextBoxColumn catReposicion = null!;
        private DataGridViewTextBoxColumn catValor = null!;

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
            lblAnalisis = new Label();
            lblReposicion = new Label();
            lblCategorias = new Label();
            kpiVentas = new Label();
            kpiEstimacion = new Label();
            kpiAlertas = new Label();
            kpiReponer = new Label();
            tabs = new TabControl();
            tabPanorama = new TabPage();
            tabProductos = new TabPage();
            tabReposicion = new TabPage();
            tabCategorias = new TabPage();
            pnlGrafico = new Panel();
            btnActualizar = new Button();
            btnVolver = new Button();
            dgvReporte = new DataGridView();
            dgvReposicion = new DataGridView();
            dgvCategorias = new DataGridView();
            colProducto = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colVentas = new DataGridViewTextBoxColumn();
            colPromedio = new DataGridViewTextBoxColumn();
            colEstimacion = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colMinimo = new DataGridViewTextBoxColumn();
            colCobertura = new DataGridViewTextBoxColumn();
            colReposicion = new DataGridViewTextBoxColumn();
            colPrioridad = new DataGridViewTextBoxColumn();
            colIngreso = new DataGridViewTextBoxColumn();
            repProducto = new DataGridViewTextBoxColumn();
            repCategoria = new DataGridViewTextBoxColumn();
            repPrioridad = new DataGridViewTextBoxColumn();
            repStock = new DataGridViewTextBoxColumn();
            repMinimo = new DataGridViewTextBoxColumn();
            repEstimacion = new DataGridViewTextBoxColumn();
            repCobertura = new DataGridViewTextBoxColumn();
            repCantidad = new DataGridViewTextBoxColumn();
            catNombre = new DataGridViewTextBoxColumn();
            catProductos = new DataGridViewTextBoxColumn();
            catVentas = new DataGridViewTextBoxColumn();
            catEstimacion = new DataGridViewTextBoxColumn();
            catReposicion = new DataGridViewTextBoxColumn();
            catValor = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReposicion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // frmReportePredictivo
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1200, 820);
            MinimumSize = new Size(1030, 720);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte predictivo del buffet";
            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(24, 15);
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Text = "REPORTE PREDICTIVO DEL BUFFET";
            // lblPeriodo
            lblPeriodo.AutoSize = true;
            lblPeriodo.Location = new Point(26, 61);
            lblPeriodo.Text = "Ventas de los ultimos 28 dias | Pronostico a 7 dias";
            // lblNota
            lblNota.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNota.Location = new Point(26, 724);
            lblNota.Size = new Size(1150, 44);
            lblNota.ForeColor = Color.Gainsboro;
            lblNota.Font = new Font("Segoe UI", 9F);
            // tabs
            tabs.Location = new Point(24, 189);
            tabs.Size = new Size(1152, 567);
            tabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tabs.Controls.Add(tabPanorama);
            tabs.Controls.Add(tabProductos);
            tabs.Controls.Add(tabReposicion);
            tabs.Controls.Add(tabCategorias);
            // tabPanorama
            tabPanorama.Name = "tabPanorama";
            tabPanorama.Text = "Panorama";
            tabPanorama.BackColor = Color.FromArgb(18, 18, 18);
            tabPanorama.Controls.Add(lblAnalisis);
            tabPanorama.Controls.Add(pnlGrafico);
            tabPanorama.Controls.Add(lblResumen);
            // tabProductos
            tabProductos.Name = "tabProductos";
            tabProductos.Text = "Productos";
            tabProductos.BackColor = Color.FromArgb(18, 18, 18);
            tabProductos.Controls.Add(dgvReporte);
            // tabReposicion
            tabReposicion.Name = "tabReposicion";
            tabReposicion.Text = "Reposicion";
            tabReposicion.BackColor = Color.FromArgb(18, 18, 18);
            tabReposicion.Controls.Add(dgvReposicion);
            tabReposicion.Controls.Add(lblReposicion);
            // tabCategorias
            tabCategorias.Name = "tabCategorias";
            tabCategorias.Text = "Categorias";
            tabCategorias.BackColor = Color.FromArgb(18, 18, 18);
            tabCategorias.Controls.Add(dgvCategorias);
            tabCategorias.Controls.Add(lblCategorias);
            // lblResumen
            lblResumen.Dock = DockStyle.Top;
            lblResumen.Height = 40;
            lblResumen.Padding = new Padding(12, 8, 8, 0);
            lblResumen.ForeColor = Color.FromArgb(214, 246, 36);
            lblResumen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            // pnlGrafico
            pnlGrafico.Dock = DockStyle.Top;
            pnlGrafico.Height = 272;
            pnlGrafico.BackColor = Color.FromArgb(24, 70, 138);
            pnlGrafico.Paint += pnlGrafico_Paint;
            pnlGrafico.Resize += pnlGrafico_Resize;
            // lblAnalisis
            lblAnalisis.Dock = DockStyle.Fill;
            lblAnalisis.Padding = new Padding(14, 18, 14, 10);
            lblAnalisis.ForeColor = Color.White;
            lblAnalisis.Font = new Font("Segoe UI", 10F);
            // lblReposicion
            lblReposicion.Dock = DockStyle.Top;
            lblReposicion.Height = 65;
            lblReposicion.Padding = new Padding(10, 10, 10, 4);
            lblReposicion.ForeColor = Color.White;
            // lblCategorias
            lblCategorias.Dock = DockStyle.Top;
            lblCategorias.Height = 65;
            lblCategorias.Padding = new Padding(10, 10, 10, 4);
            lblCategorias.ForeColor = Color.White;
            // kpiVentas
            kpiVentas.Name = "kpiVentas";
            kpiVentas.Location = new Point(24, 97);
            kpiVentas.Size = new Size(277, 76);
            kpiVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            kpiVentas.BackColor = Color.FromArgb(24, 70, 138);
            kpiVentas.ForeColor = Color.FromArgb(214, 246, 36);
            kpiVentas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            kpiVentas.TextAlign = ContentAlignment.MiddleCenter;
            // kpiEstimacion
            kpiEstimacion.Name = "kpiEstimacion";
            kpiEstimacion.Location = new Point(313, 97);
            kpiEstimacion.Size = new Size(277, 76);
            kpiEstimacion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            kpiEstimacion.BackColor = Color.FromArgb(24, 70, 138);
            kpiEstimacion.ForeColor = Color.FromArgb(214, 246, 36);
            kpiEstimacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            kpiEstimacion.TextAlign = ContentAlignment.MiddleCenter;
            // kpiAlertas
            kpiAlertas.Name = "kpiAlertas";
            kpiAlertas.Location = new Point(602, 97);
            kpiAlertas.Size = new Size(277, 76);
            kpiAlertas.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            kpiAlertas.BackColor = Color.FromArgb(24, 70, 138);
            kpiAlertas.ForeColor = Color.FromArgb(214, 246, 36);
            kpiAlertas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            kpiAlertas.TextAlign = ContentAlignment.MiddleCenter;
            // kpiReponer
            kpiReponer.Name = "kpiReponer";
            kpiReponer.Location = new Point(891, 97);
            kpiReponer.Size = new Size(277, 76);
            kpiReponer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            kpiReponer.BackColor = Color.FromArgb(24, 70, 138);
            kpiReponer.ForeColor = Color.FromArgb(214, 246, 36);
            kpiReponer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            kpiReponer.TextAlign = ContentAlignment.MiddleCenter;
            // dgvReporte
            dgvReporte.Name = "dgvReporte";
            dgvReporte.Dock = DockStyle.Fill;
            dgvReporte.AutoGenerateColumns = false;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AllowUserToDeleteRows = false;
            dgvReporte.ReadOnly = true;
            dgvReporte.MultiSelect = false;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.BackgroundColor = Color.White;
            dgvReporte.ForeColor = Color.Black;
            dgvReporte.BorderStyle = BorderStyle.None;
            dgvReporte.EnableHeadersVisualStyles = false;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 70, 138);
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvReporte.ColumnHeadersHeight = 34;
            dgvReporte.DefaultCellStyle.ForeColor = Color.Black;
            dgvReporte.DefaultCellStyle.BackColor = Color.White;
            dgvReporte.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvReporte.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 242, 249);
            dgvReporte.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReporte.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvReporte.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReporte.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReporte.Columns.AddRange(new DataGridViewColumn[] { colProducto, colCategoria, colVentas, colPromedio, colEstimacion, colStock, colMinimo, colCobertura, colReposicion, colPrioridad, colIngreso });
            // colProducto
            colProducto.Name = "colProducto";
            colProducto.DataPropertyName = "Producto";
            colProducto.HeaderText = "Producto";
            colProducto.FillWeight = 155F;
            // colCategoria
            colCategoria.Name = "colCategoria";
            colCategoria.DataPropertyName = "Categoria";
            colCategoria.HeaderText = "Categoria";
            colCategoria.FillWeight = 105F;
            // colVentas
            colVentas.Name = "colVentas";
            colVentas.DataPropertyName = "Vendidas28Dias";
            colVentas.HeaderText = "Vendidas28Dias";
            colVentas.FillWeight = 85F;
            // colPromedio
            colPromedio.Name = "colPromedio";
            colPromedio.DataPropertyName = "PromedioDiario";
            colPromedio.HeaderText = "PromedioDiario";
            colPromedio.FillWeight = 85F;
            colPromedio.DefaultCellStyle.Format = "N2";
            // colEstimacion
            colEstimacion.Name = "colEstimacion";
            colEstimacion.DataPropertyName = "Estimadas7Dias";
            colEstimacion.HeaderText = "Estimadas7Dias";
            colEstimacion.FillWeight = 85F;
            // colStock
            colStock.Name = "colStock";
            colStock.DataPropertyName = "StockDisponible";
            colStock.HeaderText = "StockDisponible";
            colStock.FillWeight = 65F;
            // colMinimo
            colMinimo.Name = "colMinimo";
            colMinimo.DataPropertyName = "StockMinimo";
            colMinimo.HeaderText = "StockMinimo";
            colMinimo.FillWeight = 65F;
            // colCobertura
            colCobertura.Name = "colCobertura";
            colCobertura.DataPropertyName = "CoberturaDias";
            colCobertura.HeaderText = "CoberturaDias";
            colCobertura.FillWeight = 90F;
            colCobertura.DefaultCellStyle.Format = "N1";
            // colReposicion
            colReposicion.Name = "colReposicion";
            colReposicion.DataPropertyName = "ReposicionSugerida";
            colReposicion.HeaderText = "ReposicionSugerida";
            colReposicion.FillWeight = 85F;
            // colPrioridad
            colPrioridad.Name = "colPrioridad";
            colPrioridad.DataPropertyName = "Prioridad";
            colPrioridad.HeaderText = "Prioridad";
            colPrioridad.FillWeight = 90F;
            // colIngreso
            colIngreso.Name = "colIngreso";
            colIngreso.DataPropertyName = "IngresoPotencial7Dias";
            colIngreso.HeaderText = "IngresoPotencial7Dias";
            colIngreso.FillWeight = 110F;
            colIngreso.DefaultCellStyle.Format = "N2";
            // dgvReposicion
            dgvReposicion.Name = "dgvReposicion";
            dgvReposicion.Dock = DockStyle.Fill;
            dgvReposicion.AutoGenerateColumns = false;
            dgvReposicion.AllowUserToAddRows = false;
            dgvReposicion.AllowUserToDeleteRows = false;
            dgvReposicion.ReadOnly = true;
            dgvReposicion.MultiSelect = false;
            dgvReposicion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReposicion.RowHeadersVisible = false;
            dgvReposicion.BackgroundColor = Color.White;
            dgvReposicion.ForeColor = Color.Black;
            dgvReposicion.BorderStyle = BorderStyle.None;
            dgvReposicion.EnableHeadersVisualStyles = false;
            dgvReposicion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReposicion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 70, 138);
            dgvReposicion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReposicion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvReposicion.ColumnHeadersHeight = 34;
            dgvReposicion.DefaultCellStyle.ForeColor = Color.Black;
            dgvReposicion.DefaultCellStyle.BackColor = Color.White;
            dgvReposicion.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvReposicion.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReposicion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 242, 249);
            dgvReposicion.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReposicion.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvReposicion.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReposicion.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvReposicion.Columns.AddRange(new DataGridViewColumn[] { repProducto, repCategoria, repPrioridad, repStock, repMinimo, repEstimacion, repCobertura, repCantidad });
            // repProducto
            repProducto.Name = "repProducto";
            repProducto.DataPropertyName = "Producto";
            repProducto.HeaderText = "Producto";
            repProducto.FillWeight = 170F;
            // repCategoria
            repCategoria.Name = "repCategoria";
            repCategoria.DataPropertyName = "Categoria";
            repCategoria.HeaderText = "Categoria";
            repCategoria.FillWeight = 105F;
            // repPrioridad
            repPrioridad.Name = "repPrioridad";
            repPrioridad.DataPropertyName = "Prioridad";
            repPrioridad.HeaderText = "Prioridad";
            repPrioridad.FillWeight = 80F;
            // repStock
            repStock.Name = "repStock";
            repStock.DataPropertyName = "StockDisponible";
            repStock.HeaderText = "StockDisponible";
            repStock.FillWeight = 65F;
            // repMinimo
            repMinimo.Name = "repMinimo";
            repMinimo.DataPropertyName = "StockMinimo";
            repMinimo.HeaderText = "StockMinimo";
            repMinimo.FillWeight = 65F;
            // repEstimacion
            repEstimacion.Name = "repEstimacion";
            repEstimacion.DataPropertyName = "Estimadas7Dias";
            repEstimacion.HeaderText = "Estimadas7Dias";
            repEstimacion.FillWeight = 95F;
            // repCobertura
            repCobertura.Name = "repCobertura";
            repCobertura.DataPropertyName = "CoberturaDias";
            repCobertura.HeaderText = "CoberturaDias";
            repCobertura.FillWeight = 95F;
            repCobertura.DefaultCellStyle.Format = "N1";
            // repCantidad
            repCantidad.Name = "repCantidad";
            repCantidad.DataPropertyName = "ReposicionSugerida";
            repCantidad.HeaderText = "ReposicionSugerida";
            repCantidad.FillWeight = 95F;
            // dgvCategorias
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.Dock = DockStyle.Fill;
            dgvCategorias.AutoGenerateColumns = false;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.ReadOnly = true;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.BackgroundColor = Color.White;
            dgvCategorias.ForeColor = Color.Black;
            dgvCategorias.BorderStyle = BorderStyle.None;
            dgvCategorias.EnableHeadersVisualStyles = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategorias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 70, 138);
            dgvCategorias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCategorias.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCategorias.ColumnHeadersHeight = 34;
            dgvCategorias.DefaultCellStyle.ForeColor = Color.Black;
            dgvCategorias.DefaultCellStyle.BackColor = Color.White;
            dgvCategorias.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvCategorias.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCategorias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236, 242, 249);
            dgvCategorias.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCategorias.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 246, 36);
            dgvCategorias.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCategorias.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvCategorias.Columns.AddRange(new DataGridViewColumn[] { catNombre, catProductos, catVentas, catEstimacion, catReposicion, catValor });
            // catNombre
            catNombre.Name = "catNombre";
            catNombre.DataPropertyName = "Categoria";
            catNombre.HeaderText = "Categoria";
            catNombre.FillWeight = 170F;
            // catProductos
            catProductos.Name = "catProductos";
            catProductos.DataPropertyName = "Productos";
            catProductos.HeaderText = "Productos";
            catProductos.FillWeight = 90F;
            // catVentas
            catVentas.Name = "catVentas";
            catVentas.DataPropertyName = "Ventas";
            catVentas.HeaderText = "Ventas";
            catVentas.FillWeight = 105F;
            // catEstimacion
            catEstimacion.Name = "catEstimacion";
            catEstimacion.DataPropertyName = "Estimacion";
            catEstimacion.HeaderText = "Estimacion";
            catEstimacion.FillWeight = 105F;
            // catReposicion
            catReposicion.Name = "catReposicion";
            catReposicion.DataPropertyName = "Reposicion";
            catReposicion.HeaderText = "Reposicion";
            catReposicion.FillWeight = 105F;
            // catValor
            catValor.Name = "catValor";
            catValor.DataPropertyName = "ValorPotencial";
            catValor.HeaderText = "ValorPotencial";
            catValor.FillWeight = 140F;
            catValor.DefaultCellStyle.Format = "N2";
            // btnActualizar
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnActualizar.Location = new Point(898, 772);
            btnActualizar.Size = new Size(130, 34);
            btnActualizar.BackColor = Color.FromArgb(214, 246, 36);
            btnActualizar.ForeColor = Color.Black;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;
            // btnVolver
            btnVolver.Name = "btnVolver";
            btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnVolver.Location = new Point(1040, 772);
            btnVolver.Size = new Size(130, 34);
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.ForeColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F);
            btnVolver.Text = "Volver";
            btnVolver.Click += btnVolver_Click;
            Controls.Add(lblTitulo);
            Controls.Add(lblPeriodo);
            Controls.Add(kpiVentas);
            Controls.Add(kpiEstimacion);
            Controls.Add(kpiAlertas);
            Controls.Add(kpiReponer);
            Controls.Add(tabs);
            Controls.Add(btnActualizar);
            Controls.Add(btnVolver);
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReposicion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
