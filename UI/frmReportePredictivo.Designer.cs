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
        private Label[] kpis = null!;
        private TabControl tabs = null!;
        private TabPage tabPanorama = null!;
        private TabPage tabProductos = null!;
        private TabPage tabReposicion = null!;
        private TabPage tabCategorias = null!;
        private DataGridView dgvReporte = null!;
        private DataGridView dgvReposicion = null!;
        private DataGridView dgvCategorias = null!;
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
            SuspendLayout();
            BackColor = Color.FromArgb(18, 18, 18);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1200, 820);
            MinimumSize = new Size(1030, 720);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte predictivo del buffet";

            lblTitulo = new Label {
                AutoSize = true, Location = new Point(24, 15),
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(214, 246, 36),
                Text = "REPORTE PREDICTIVO DEL BUFFET"
            };
            lblPeriodo = new Label { AutoSize = true, Location = new Point(26, 61) };

            // Indicadores superiores: las cuatro tarjetas se ajustan al ancho.
            TableLayoutPanel indicadores = new TableLayoutPanel {
                Name = "pnlIndicadores", Location = new Point(24, 93),
                Size = new Size(1152, 80), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 4, RowCount = 1, BackColor = Color.FromArgb(18,18,18)
            };
            for (int i = 0; i < 4; i++)
                indicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            kpis = new Label[4];
            for (int i = 0; i < kpis.Length; i++)
            {
                Panel contenedor = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 9, 0),
                    BackColor = Color.FromArgb(24, 70, 138) };
                Label dato = new Label {
                    Dock = DockStyle.Fill, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(214,246,36), Font = new Font("Segoe UI", 11F, FontStyle.Bold)
                };
                kpis[i] = dato;
                contenedor.Controls.Add(dato);
                indicadores.Controls.Add(contenedor, i, 0);
            }

            tabs = new TabControl {
                Location = new Point(24, 189), Size = new Size(1152, 527),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            tabPanorama = new TabPage { Name = "tabPanorama", Text = "Panorama", BackColor = Color.FromArgb(18,18,18) };
            tabProductos = new TabPage { Name = "tabProductos", Text = "Productos", BackColor = Color.FromArgb(18,18,18) };
            tabReposicion = new TabPage { Name = "tabReposicion", Text = "Reposición", BackColor = Color.FromArgb(18,18,18) };
            tabCategorias = new TabPage { Name = "tabCategorias", Text = "Categorías", BackColor = Color.FromArgb(18,18,18) };
            tabs.TabPages.AddRange(new[] { tabPanorama, tabProductos, tabReposicion, tabCategorias });

            lblResumen = new Label { Dock = DockStyle.Top, Height = 40, Padding = new Padding(12,8,8,0),
                ForeColor = Color.FromArgb(214,246,36), Font = new Font("Segoe UI",10F,FontStyle.Bold) };
            pnlGrafico = new Panel { Dock = DockStyle.Top, Height = 272,
                BackColor = Color.FromArgb(24,70,138) };
            pnlGrafico.Paint += pnlGrafico_Paint;
            pnlGrafico.Resize += (_, _) => pnlGrafico.Invalidate();
            lblAnalisis = new Label { Dock = DockStyle.Fill, Padding = new Padding(14,18,14,10),
                AutoSize = false, ForeColor = Color.White, Font = new Font("Segoe UI",10F) };
            tabPanorama.Controls.Add(lblAnalisis);
            tabPanorama.Controls.Add(pnlGrafico);
            tabPanorama.Controls.Add(lblResumen);

            dgvReporte = CrearGrilla();
            dgvReporte.Dock = DockStyle.Fill;
            dgvReporte.Columns.Add(Columna("colProducto","Producto",155,"Producto"));
            dgvReporte.Columns.Add(Columna("colCategoria","Categoria",105,"Categoria"));
            dgvReporte.Columns.Add(Columna("colVentas","Vendidas28Dias",85,"Vendidas"));
            dgvReporte.Columns.Add(Columna("colPromedio","PromedioDiario",85,"Promedio", "N2"));
            dgvReporte.Columns.Add(Columna("colEstimacion","Estimadas7Dias",85,"Estimadas"));
            dgvReporte.Columns.Add(Columna("colStock","StockDisponible",65,"Stock"));
            dgvReporte.Columns.Add(Columna("colMinimo","StockMinimo",65,"Minimo"));
            dgvReporte.Columns.Add(Columna("colCobertura","CoberturaDias",90,"Cobertura","N1"));
            dgvReporte.Columns.Add(Columna("colReposicion","ReposicionSugerida",85,"Reposicion"));
            dgvReporte.Columns.Add(Columna("colPrioridad","Prioridad",90,"Prioridad"));
            dgvReporte.Columns.Add(Columna("colIngreso","IngresoPotencial7Dias",110,"Ingreso", "N2"));
            tabProductos.Controls.Add(dgvReporte);

            lblReposicion = new Label { Dock = DockStyle.Top, Height = 65, Padding = new Padding(10,10,10,4),
                ForeColor = Color.White, Font = new Font("Segoe UI",10F) };
            dgvReposicion = CrearGrilla();
            dgvReposicion.Dock = DockStyle.Fill;
            dgvReposicion.Columns.Add(Columna("repProducto","Producto",170,"Producto"));
            dgvReposicion.Columns.Add(Columna("repCategoria","Categoria",105,"Categoria"));
            dgvReposicion.Columns.Add(Columna("repPrioridad","Prioridad",80,"Prioridad"));
            dgvReposicion.Columns.Add(Columna("repStock","StockDisponible",65,"Stock"));
            dgvReposicion.Columns.Add(Columna("repMinimo","StockMinimo",65,"Minimo"));
            dgvReposicion.Columns.Add(Columna("repEstimacion","Estimadas7Dias",95,"Estimadas"));
            dgvReposicion.Columns.Add(Columna("repCobertura","CoberturaDias",95,"Cobertura","N1"));
            dgvReposicion.Columns.Add(Columna("repCantidad","ReposicionSugerida",95,"Reposicion"));
            tabReposicion.Controls.Add(dgvReposicion);
            tabReposicion.Controls.Add(lblReposicion);

            lblCategorias = new Label { Dock = DockStyle.Top, Height = 65, Padding = new Padding(10,10,10,4),
                ForeColor = Color.White, Font = new Font("Segoe UI",10F) };
            dgvCategorias = CrearGrilla();
            dgvCategorias.Dock = DockStyle.Fill;
            dgvCategorias.Columns.Add(Columna("catNombre","Categoria",170,"Categoria"));
            dgvCategorias.Columns.Add(Columna("catProductos","Productos",90,"Productos"));
            dgvCategorias.Columns.Add(Columna("catVentas","Ventas",105,"Vendidas"));
            dgvCategorias.Columns.Add(Columna("catEstimacion","Estimacion",105,"Estimadas"));
            dgvCategorias.Columns.Add(Columna("catReposicion","Reposicion",105,"Reposicion"));
            dgvCategorias.Columns.Add(Columna("catValor","ValorPotencial",140,"Ingreso","N2"));
            tabCategorias.Controls.Add(dgvCategorias);
            tabCategorias.Controls.Add(lblCategorias);

            lblNota = new Label { Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Location = new Point(26,724), Size = new Size(1150,44), ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI",9F) };

            btnActualizar = new Button { Name = "btnActualizar",
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(898,772), Size = new Size(130,34),
                BackColor = Color.FromArgb(214,246,36), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI",10F,FontStyle.Bold), Text = "Actualizar" };
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += btnActualizar_Click;

            btnVolver = new Button { Name = "btnVolver",
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(1040,772), Size = new Size(130,34),
                BackColor = Color.FromArgb(24,70,138), ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI",10F,FontStyle.Bold), Text = "Volver" };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += btnVolver_Click;

            Controls.Add(lblTitulo);
            Controls.Add(lblPeriodo);
            Controls.Add(indicadores);
            Controls.Add(tabs);
            Controls.Add(lblNota);
            Controls.Add(btnActualizar);
            Controls.Add(btnVolver);
            ResumeLayout(false);
            PerformLayout();
        }

        private static DataGridView CrearGrilla()
        {
            DataGridView g = new DataGridView {
                AutoGenerateColumns = false, AllowUserToAddRows = false,
                AllowUserToDeleteRows = false, ReadOnly = true, MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false, BackgroundColor = Color.White,
                ForeColor = Color.Black, BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24,70,138);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI",9F,FontStyle.Bold);
            g.ColumnHeadersHeight = 34;
            g.DefaultCellStyle.ForeColor = Color.Black;
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214,246,36);
            g.DefaultCellStyle.SelectionForeColor = Color.Black;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(236,242,249);
            g.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(214,246,36);
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            return g;
        }

        private static DataGridViewTextBoxColumn Columna(string nombre, string propiedad,
            float peso, string titulo, string formato = "")
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn {
                Name = nombre, DataPropertyName = propiedad, HeaderText = titulo, FillWeight = peso
            };
            if (formato.Length > 0) columna.DefaultCellStyle.Format = formato;
            return columna;
        }
    }
}
