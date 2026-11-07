using BE;
using BLL;
using Servicios;
using UI.Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class frmReportePredictivo : Form, IObservadorIdioma
    {
        private readonly ReportePredictivoBLL _reporteBLL = new ReportePredictivoBLL();
        private List<ReportePredictivoBE> _datos = new List<ReportePredictivoBE>();
        private static string T(string clave) => Traductor.Instancia.Traducir(clave);
        // Los cuatro controles KPI se instancian individualmente en el Designer.
        private Label[] kpis = null!;

        private class ResumenCategoria
        {
            public string Categoria { get; set; } = string.Empty;
            public int Productos { get; set; }
            public long Ventas { get; set; }
            public long Estimacion { get; set; }
            public long Reposicion { get; set; }
            public decimal ValorPotencial { get; set; }
        }

        public frmReportePredictivo()
        {
            InitializeComponent();
            kpis = new[] { kpiVentas, kpiEstimacion, kpiAlertas, kpiReponer };
            AyudaContextual.Configurar(this);
            ActualizarIdioma();
            Load += frmReportePredictivo_Load;
            FormClosed += frmReportePredictivo_FormClosed;
            dgvReporte.CellFormatting += PintarPrioridad;
            dgvReposicion.CellFormatting += PintarPrioridad;
        }

        private void frmReportePredictivo_Load(object? sender, EventArgs e)
        {
            SM.Instancia.Suscribir(this);
            CargarReporte();
        }

        private void frmReportePredictivo_FormClosed(object? sender, FormClosedEventArgs e)
        {
            SM.Instancia.Desuscribir(this);
        }

        public void ActualizarIdioma()
        {
            Text = T("frmReportePredictivo.Title");
            lblTitulo.Text = T("frmReportePredictivo.Titulo");
            lblPeriodo.Text = string.Format(T("frmReportePredictivo.Periodo"),
                DateTime.Today.AddDays(-28).ToShortDateString(),
                DateTime.Today.AddDays(-1).ToShortDateString());
            lblNota.Text = T("frmReportePredictivo.Nota");
            btnActualizar.Text = T("frmReportePredictivo.Actualizar");
            btnVolver.Text = T("frmReportePredictivo.Volver");
            tabPanorama.Text = T("frmReportePredictivo.TabPanorama");
            tabProductos.Text = T("frmReportePredictivo.TabProductos");
            tabReposicion.Text = T("frmReportePredictivo.TabReposicion");
            tabCategorias.Text = T("frmReportePredictivo.TabCategorias");

            string[] columnas = {
                "Producto", "Categoria", "Ventas", "Promedio", "Estimacion", "Stock",
                "Minimo", "Cobertura", "Reposicion", "Prioridad", "Ingreso"
            };
            foreach (string columna in columnas)
                dgvReporte.Columns["col" + columna]!.HeaderText =
                    T("frmReportePredictivo.Col" + columna);

            string[] columnasRep = {
                "Producto", "Categoria", "Prioridad", "Stock", "Minimo",
                "Estimacion", "Cobertura", "Cantidad"
            };
            foreach (string columna in columnasRep)
                dgvReposicion.Columns["rep" + columna]!.HeaderText =
                    T("frmReportePredictivo.Col" + (columna == "Cantidad" ? "Reposicion" : columna));

            string[] columnasCat = { "Nombre", "Productos", "Ventas", "Estimacion", "Reposicion", "Valor" };
            foreach (string columna in columnasCat)
                dgvCategorias.Columns["cat" + columna]!.HeaderText =
                    T("frmReportePredictivo.Cat" + columna);

            MostrarIndicadores();
            pnlGrafico.Invalidate();
            dgvReporte.Invalidate();
            dgvReposicion.Invalidate();
        }

        private string PrioridadTraducida(string prioridad)
        {
            return prioridad switch
            {
                "Critica" => T("frmReportePredictivo.Critica"),
                "Alta" => T("frmReportePredictivo.Alta"),
                "Normal" => T("frmReportePredictivo.Normal"),
                _ => T("frmReportePredictivo.SinHistorial")
            };
        }

        private void PintarPrioridad(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grilla = (DataGridView)sender!;
            if (e.RowIndex < 0 || grilla.Columns[e.ColumnIndex].DataPropertyName != "Prioridad")
                return;
            if (e.Value is not string prioridad) return;
            e.Value = PrioridadTraducida(prioridad);
            e.FormattingApplied = true;
            e.CellStyle.ForeColor = prioridad switch
            {
                "Critica" => Color.DarkRed,
                "Alta" => Color.DarkOrange,
                _ => Color.Black
            };
            e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        private void CargarReporte()
        {
            try
            {
                _datos = _reporteBLL.ObtenerPronostico();
                dgvReporte.DataSource = null;
                dgvReporte.DataSource = _datos;
                dgvReporte.ClearSelection();

                List<ReportePredictivoBE> urgentes = _datos
                    .Where(x => x.ReposicionSugerida > 0)
                    .OrderBy(x => x.Prioridad == "Critica" ? 0 : 1)
                    .ThenByDescending(x => x.ReposicionSugerida).ToList();
                dgvReposicion.DataSource = null;
                dgvReposicion.DataSource = urgentes;
                dgvReposicion.ClearSelection();

                List<ResumenCategoria> categorias = _datos.GroupBy(x => x.Categoria)
                    .Select(g => new ResumenCategoria
                    {
                        Categoria = g.Key,
                        Productos = g.Count(),
                        Ventas = g.Sum(x => (long)x.Vendidas28Dias),
                        Estimacion = g.Sum(x => (long)x.Estimadas7Dias),
                        Reposicion = g.Sum(x => (long)x.ReposicionSugerida),
                        ValorPotencial = g.Sum(x => x.IngresoPotencial7Dias)
                    })
                    .OrderByDescending(x => x.Estimacion).ThenBy(x => x.Categoria).ToList();
                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = categorias;
                dgvCategorias.ClearSelection();

                MostrarIndicadores();
                pnlGrafico.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void MostrarIndicadores()
        {
            long ventas = _datos.Sum(x => (long)x.Vendidas28Dias);
            long estimadas = _datos.Sum(x => (long)x.Estimadas7Dias);
            long reponer = _datos.Sum(x => (long)x.ReposicionSugerida);
            int criticos = _datos.Count(x => x.Prioridad == "Critica");
            int alertas = _datos.Count(x => x.ReposicionSugerida > 0);
            int sinVentas = _datos.Count(x => x.Vendidas28Dias == 0);
            decimal ingresoEstimado = _datos.Sum(x => x.IngresoPotencial7Dias);

            kpis[0].Text = string.Format(T("frmReportePredictivo.KpiVentas"), ventas);
            kpis[1].Text = string.Format(T("frmReportePredictivo.KpiEstimacion"), estimadas);
            kpis[2].Text = string.Format(T("frmReportePredictivo.KpiAlertas"), alertas);
            kpis[3].Text = string.Format(T("frmReportePredictivo.KpiReponer"), reponer);

            lblResumen.Text = string.Format(T("frmReportePredictivo.ResumenAmpliado"),
                _datos.Count, criticos, sinVentas);
            lblReposicion.Text = string.Format(T("frmReportePredictivo.TextoReposicion"),
                alertas, reponer, criticos);
            lblCategorias.Text = string.Format(T("frmReportePredictivo.TextoCategorias"),
                _datos.Select(x => x.Categoria).Distinct().Count(), ingresoEstimado);

            string productoMayor = _datos.OrderByDescending(x => x.Estimadas7Dias)
                .ThenBy(x => x.Producto).Select(x => x.Producto).FirstOrDefault()
                ?? T("frmReportePredictivo.SinDatos");
            string urgencia = _datos.Where(x => x.ReposicionSugerida > 0)
                .OrderByDescending(x => x.ReposicionSugerida).Select(x => x.Producto)
                .FirstOrDefault() ?? T("frmReportePredictivo.SinUrgencias");
            lblAnalisis.Text = string.Format(T("frmReportePredictivo.Analisis"),
                productoMayor, urgencia, ingresoEstimado, sinVentas);
        }

        private void pnlGrafico_Resize(object? sender, EventArgs e) => pnlGrafico.Invalidate();

        private void btnActualizar_Click(object? sender, EventArgs e) => CargarReporte();
        private void btnVolver_Click(object? sender, EventArgs e) => Close();

        // Escala una sola barra por producto y limita el dibujo al area del panel.
        // El ancho de barra y sus numeros siempre quedan dentro de los bordes.
        private void pnlGrafico_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(24, 70, 138));
            g.SetClip(pnlGrafico.ClientRectangle);
            using Font titulo = new Font("Segoe UI", 11F, FontStyle.Bold);
            using Font texto = new Font("Segoe UI", 9F);
            using SolidBrush blanco = new SolidBrush(Color.White);
            using SolidBrush verde = new SolidBrush(Color.FromArgb(214, 246, 36));

            g.DrawString(T("frmReportePredictivo.Grafico"), titulo, blanco, 14, 12);
            List<ReportePredictivoBE> productos = _datos
                .Where(x => x.Estimadas7Dias > 0)
                .OrderByDescending(x => x.Estimadas7Dias)
                .ThenBy(x => x.Producto)
                .Take(8).ToList();

            if (productos.Count == 0)
            {
                g.DrawString(T("frmReportePredictivo.SinDatos"), texto, blanco, 15, 52);
                return;
            }

            int mayor = Math.Max(1, productos.Max(x => x.Estimadas7Dias));
            const int margenIzquierdo = 225;
            int anchoGrafico = Math.Max(10, pnlGrafico.ClientSize.Width - margenIzquierdo - 78);
            int paso = Math.Max(20, Math.Min(24, (pnlGrafico.ClientSize.Height - 64) / productos.Count));
            for (int i = 0; i < productos.Count; i++)
            {
                ReportePredictivoBE p = productos[i];
                int y = 49 + i * paso;
                if (y + paso > pnlGrafico.ClientSize.Height) break;
                string nombre = p.Producto.Length > 27
                    ? p.Producto.Substring(0, 24) + "..." : p.Producto;
                g.DrawString(nombre, texto, blanco, 14, y);
                float longitud = anchoGrafico * (float)p.Estimadas7Dias / mayor;
                g.FillRectangle(verde, margenIzquierdo, y + 2, longitud, paso - 5);
                g.DrawString(p.Estimadas7Dias.ToString(), texto, blanco,
                    margenIzquierdo + longitud + 9, y);
            }
        }
    }
}
