using BE;
using BLL;
using Servicios;
using UI.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;

namespace UI
{
    public partial class frmReportePredictivo : Form, IObservadorIdioma
    {
        private Button btnExportarPdf = null!;
        private readonly ReportePredictivoBLL _reporteBLL = new ReportePredictivoBLL();
        private List<ReportePredictivoBE> _datos = new List<ReportePredictivoBE>();
        private static string T(string clave) => Traductor.Instancia.Traducir(clave);
        // Los cuatro controles KPI se instancian individualmente en el Designer.
        private Label[] kpis = null!;
        private readonly Dictionary<DataGridView, (int columna, bool ascendente)> _ordenGrillas = new();

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
            // Se agrega sin modificar los cálculos ni los controles del informe.
            btnActualizar.Location = new Point(btnActualizar.Left - 142, btnActualizar.Top);
            btnExportarPdf = new Button
            {
                Name = "btnExportarPdf",
                Text = "Exportar PDF",
                Location = new Point(898, 772),
                Size = new Size(130, 34),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = Color.FromArgb(214, 246, 36),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnExportarPdf.FlatAppearance.BorderSize = 0;
            btnExportarPdf.Click += btnExportarPdf_Click;
            Controls.Add(btnExportarPdf);

            // Permitir minimizar, maximizar y restaurar con los botones nativos.
            // Al restaurar, la ventana debe entrar en el area util de la pantalla.
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            Rectangle area = Screen.FromControl(this).WorkingArea;
            Size = new Size(Math.Min(Width, area.Width - 40),
                            Math.Min(Height, area.Height - 40));
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;

            // Los importes siempre son pesos argentinos, sin depender del idioma de Windows.
            CultureInfo moneda = CultureInfo.GetCultureInfo("es-AR");
            dgvReporte.Columns["colIngreso"]!.DefaultCellStyle.Format = "C2";
            dgvReporte.Columns["colIngreso"]!.DefaultCellStyle.FormatProvider = moneda;
            dgvCategorias.Columns["catValor"]!.DefaultCellStyle.Format = "C2";
            dgvCategorias.Columns["catValor"]!.DefaultCellStyle.FormatProvider = moneda;

            Resize += frmReportePredictivo_Resize;
            AjustarIndicadores();
            AyudaContextual.Configurar(this);
            ActualizarIdioma();
            Load += frmReportePredictivo_Load;
            FormClosed += frmReportePredictivo_FormClosed;
            dgvReporte.CellFormatting += PintarPrioridad;
            dgvReposicion.CellFormatting += PintarPrioridad;

            // Las grillas usan List<T>, que no se ordena automaticamente por el encabezado.
            ConfigurarOrdenamiento(dgvReporte);
            ConfigurarOrdenamiento(dgvReposicion);
            ConfigurarOrdenamiento(dgvCategorias);
            dgvReporte.ColumnHeaderMouseClick += (sender, e) =>OrdenarGrilla<ReportePredictivoBE>(dgvReporte, e.ColumnIndex);
            dgvReposicion.ColumnHeaderMouseClick += (sender, e) =>OrdenarGrilla<ReportePredictivoBE>(dgvReposicion, e.ColumnIndex);
            dgvCategorias.ColumnHeaderMouseClick += (sender, e) =>OrdenarGrilla<ResumenCategoria>(dgvCategorias, e.ColumnIndex);
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
            btnExportarPdf.Text = string.Equals(Traductor.Instancia.CodigoIdiomaActual, "EN",
                StringComparison.OrdinalIgnoreCase) ? "Export PDF" : "Exportar PDF";
            btnVolver.Text = T("frmReportePredictivo.Volver");
            tabPanorama.Text = T("frmReportePredictivo.TabPanorama");
            tabProductos.Text = T("frmReportePredictivo.TabProductos");
            tabReposicion.Text = T("frmReportePredictivo.TabReposicion");
            tabCategorias.Text = T("frmReportePredictivo.TabCategorias");

            string[] columnas = {
                "Producto", "Categoria", "Ventas", "Promedio", "Estimacion", "Stock","Minimo", "Cobertura", "Reposicion", "Prioridad", "Ingreso"
            };
            foreach (string columna in columnas)
                dgvReporte.Columns["col" + columna]!.HeaderText =
                    T("frmReportePredictivo.Col" + columna);

            string[] columnasRep = {
                "Producto", "Categoria", "Prioridad", "Stock", "Minimo","Estimacion", "Cobertura", "Cantidad"
            };
            foreach (string columna in columnasRep)
                dgvReposicion.Columns["rep" + columna]!.HeaderText =T("frmReportePredictivo.Col" + (columna == "Cantidad" ? "Reposicion" : columna));

            string[] columnasCat = { "Nombre", "Productos", "Ventas", "Estimacion", "Reposicion", "Valor" };
            foreach (string columna in columnasCat)
                dgvCategorias.Columns["cat" + columna]!.HeaderText =T("frmReportePredictivo.Cat" + columna);

            MostrarIndicadores();
            pnlGrafico.Invalidate();
            pnlDistribucion.Invalidate();
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

        // Cada encabezado alterna ascendente / descendente y muestra una flecha.
        private static void ConfigurarOrdenamiento(DataGridView grilla)
        {
            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        private void OrdenarGrilla<T>(DataGridView grilla, int indiceColumna)
        {
            if (indiceColumna < 0 || grilla.DataSource is not List<T> datos)
                return;

            DataGridViewColumn columna = grilla.Columns[indiceColumna];
            PropertyDescriptor? propiedad =TypeDescriptor.GetProperties(typeof(T))[columna.DataPropertyName];

            if (propiedad == null) return;

            bool ascendente = !_ordenGrillas.TryGetValue(grilla, out var anterior)
                || anterior.columna != indiceColumna || !anterior.ascendente;

            // Ordenar con los valores originales, no el texto formateado de la celda:
            // los importes, cantidades y promedios se comparan numericamente.
            object Clave(T fila)
            {
                object valor = propiedad.GetValue(fila) ?? string.Empty;
                if (columna.DataPropertyName == "Prioridad" && valor is string prioridad)
                    return prioridad switch
                    {
                        "Critica" => 0,
                        "Alta" => 1,
                        "Normal" => 2,
                        _ => 3
                    };
                return valor;
            }

            List<T> ordenados = ascendente
                ? datos.OrderBy(Clave, Comparer<object>.Default).ToList()
                : datos.OrderByDescending(Clave, Comparer<object>.Default).ToList();

            grilla.DataSource = null;
            grilla.DataSource = ordenados;
            grilla.ClearSelection();

            foreach (DataGridViewColumn otra in grilla.Columns)
                otra.HeaderCell.SortGlyphDirection = SortOrder.None;
            columna.HeaderCell.SortGlyphDirection = ascendente? SortOrder.Ascending : SortOrder.Descending;
            _ordenGrillas[grilla] = (indiceColumna, ascendente);
        }

        private void CargarReporte()
        {
            try
            {
                _datos = _reporteBLL.ObtenerPronostico();
                dgvReporte.DataSource = null;
                dgvReporte.DataSource = _datos;
                dgvReporte.ClearSelection();

                List<ReportePredictivoBE> urgentes = _datos.Where(x => x.ReposicionSugerida > 0).OrderBy(x => x.Prioridad == "Critica" ? 0 : 1).ThenByDescending(x => x.ReposicionSugerida).ToList();
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

                // Al actualizar datos se restablece el orden y las flechas.
                _ordenGrillas.Clear();
                foreach (DataGridView grilla in new[] { dgvReporte, dgvReposicion, dgvCategorias })
                    foreach (DataGridViewColumn columna in grilla.Columns)
                        columna.HeaderCell.SortGlyphDirection = SortOrder.None;

                MostrarIndicadores();
                pnlGrafico.Invalidate();
                pnlDistribucion.Invalidate();
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

            lblResumen.Text = string.Format(T("frmReportePredictivo.ResumenAmpliado"),_datos.Count, criticos, sinVentas);
            lblReposicion.Text = string.Format(T("frmReportePredictivo.TextoReposicion"),alertas, reponer, criticos);
            lblCategorias.Text = string.Format(T("frmReportePredictivo.TextoCategorias"),_datos.Select(x => x.Categoria).Distinct().Count(), ingresoEstimado);

            string productoMayor = _datos.OrderByDescending(x => x.Estimadas7Dias).ThenBy(x => x.Producto).Select(x => x.Producto).FirstOrDefault()?? T("frmReportePredictivo.SinDatos");
            string urgencia = _datos.Where(x => x.ReposicionSugerida > 0).OrderByDescending(x => x.ReposicionSugerida).Select(x => x.Producto).FirstOrDefault() ?? T("frmReportePredictivo.SinUrgencias");
            lblAnalisis.Text = string.Format(T("frmReportePredictivo.Analisis"),productoMayor, urgencia, ingresoEstimado, sinVentas);
        }

        private void pnlGrafico_Resize(object? sender, EventArgs e) => pnlGrafico.Invalidate();

        // Mantiene las cuatro tarjetas distribuidas uniformemente cuando cambia
        // la resolucion del monitor. No depende de Anchor de cada etiqueta.
        private void frmReportePredictivo_Resize(object? sender, EventArgs e)
        {
            AjustarIndicadores();
            pnlGrafico.Invalidate();
            pnlDistribucion.Invalidate();
        }

        private void AjustarIndicadores()
        {
            if (kpis == null) return;
            const int margen = 24;
            const int separacion = 12;
            int ancho = Math.Max(120, (ClientSize.Width - 2 * margen - 3 * separacion) / 4);
            for (int i = 0; i < kpis.Length; i++)
            {
                kpis[i].Location = new Point(margen + i * (ancho + separacion), 97);
                kpis[i].Size = new Size(ancho, 76);
            }
            // La altura de las 8 barras permanece fija. En pantallas bajas
            // tabPanorama habilita scroll vertical en lugar de ocultar barras.
        }

        private void btnExportarPdf_Click(object? sender, EventArgs e)
        {
            if (_datos.Count == 0)
            {
                MessageBox.Show(this, "No hay datos para exportar.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Documento PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                FileName = "PadelGest_ReporteInteligente_" + DateTime.Today.ToString("yyyyMMdd")
            };
            if (dialogo.ShowDialog(this) != DialogResult.OK) return;
            bool exitoso = false;
            try
            {
                ReportePredictivoPdf.Exportar(dialogo.FileName, _datos, DateTime.Now);
                exitoso = true;
                MessageBox.Show(this, "El reporte se exportó correctamente.", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo exportar el PDF: " + ex.Message,
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                AuditoriaExportacionPdf.Registrar("Reporte inteligente",
                    "Buffet", exitoso);
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e) => CargarReporte();
        private void btnVolver_Click(object? sender, EventArgs e) => Close();

        // Torta de participacion en la demanda estimada semanal por categoria.
        // Muestra las 5 categorias de mayor proyeccion y agrupa el resto en Otros.
        // No representa ventas cobradas ni probabilidades.
        private void pnlDistribucion_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(24, 70, 138));
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using Font titulo = new Font("Segoe UI", 11F, FontStyle.Bold);
            using Font texto = new Font("Segoe UI", 9F);
            using Font textoNegrita = new Font("Segoe UI", 9F, FontStyle.Bold);
            using SolidBrush blanco = new SolidBrush(Color.White);
            using SolidBrush subtitulo = new SolidBrush(Color.Gainsboro);
            g.DrawString(T("frmReportePredictivo.Distribucion"), titulo, blanco, 14, 12);
            g.DrawString(T("frmReportePredictivo.DistribucionNota"), texto, subtitulo, 14, 40);

            var categorias = _datos
                .Where(x => x.Estimadas7Dias > 0)
                .GroupBy(x => string.IsNullOrWhiteSpace(x.Categoria)
                    ? T("frmReportePredictivo.SinCategoria") : x.Categoria)
                .Select(x => new { Nombre = x.Key, Cantidad = x.Sum(p => (long)p.Estimadas7Dias) })
                .OrderByDescending(x => x.Cantidad)
                .ThenBy(x => x.Nombre)
                .ToList();
            long total = categorias.Sum(x => x.Cantidad);
            if (total == 0)
            {
                g.DrawString(T("frmReportePredictivo.SinEstimacion"), texto, blanco, 20, 105);
                return;
            }

            var datos = categorias.Take(5).Select(x => (x.Nombre, x.Cantidad)).ToList();
            long otros = categorias.Skip(5).Sum(x => x.Cantidad);
            if (otros > 0)
                datos.Add((T("frmReportePredictivo.Otros"), otros));

            Color[] colores =
            {
                Color.FromArgb(214, 246, 36),
                Color.FromArgb(73, 200, 233),
                Color.FromArgb(255, 181, 79),
                Color.FromArgb(186, 147, 255),
                Color.FromArgb(101, 224, 162),
                Color.FromArgb(185, 193, 208)
            };

            int lado = Math.Max(100, Math.Min(190, Math.Min(pnlDistribucion.ClientSize.Height - 92,
                (pnlDistribucion.ClientSize.Width - 65) / 3)));
            int xTorta = 24;
            int yTorta = 77;
            Rectangle rect = new Rectangle(xTorta, yTorta, lado, lado);
            float inicio = -90f;
            for (int i = 0; i < datos.Count; i++)
            {
                float angulo = (float)(360.0 * datos[i].Cantidad / total);
                using SolidBrush pincel = new SolidBrush(colores[i]);
                g.FillPie(pincel, rect, inicio, angulo);
                inicio += angulo;
            }

            // Centro de la dona. La participacion se calcula en unidades, no dinero.
            int interior = (int)(lado * .55);
            Rectangle agujero = new Rectangle(xTorta + (lado - interior) / 2,
                yTorta + (lado - interior) / 2, interior, interior);
            using (SolidBrush fondo = new SolidBrush(Color.FromArgb(24, 70, 138)))
                g.FillEllipse(fondo, agujero);
            string totalTexto = total.ToString("N0");
            SizeF tam = g.MeasureString(totalTexto, textoNegrita);
            g.DrawString(totalTexto, textoNegrita, blanco,
                xTorta + (lado - tam.Width) / 2, yTorta + lado / 2f - 18);
            string unidades = T("frmReportePredictivo.Unidades");
            tam = g.MeasureString(unidades, texto);
            g.DrawString(unidades, texto, blanco,
                xTorta + (lado - tam.Width) / 2, yTorta + lado / 2f + 1);

            int xLeyenda = xTorta + lado + 35;
            float anchoDisponible = Math.Max(85, pnlDistribucion.ClientSize.Width - xLeyenda - 15);
            for (int i = 0; i < datos.Count; i++)
            {
                float y = 79 + i * 31;
                using SolidBrush pincel = new SolidBrush(colores[i]);
                g.FillRectangle(pincel, xLeyenda, y + 4, 13, 13);
                decimal porcentaje = 100m * datos[i].Cantidad / total;
                string nombre = datos[i].Nombre;
                string valor = $"{porcentaje:0.#}% ({datos[i].Cantidad:N0})";
                float anchoValor = g.MeasureString(valor, textoNegrita).Width;
                float xNombre = xLeyenda + 21;
                float anchoNombreMaximo = Math.Max(30, anchoDisponible - anchoValor - 33);
                while (nombre.Length > 4 && g.MeasureString(nombre, texto).Width > anchoNombreMaximo)
                    nombre = nombre.Substring(0, nombre.Length - 2);
                if (nombre != datos[i].Nombre) nombre = nombre.TrimEnd() + "...";
                g.DrawString(nombre, texto, blanco, xNombre, y);
                // El valor acompaña al nombre en vez de quedar pegado
                // al extremo derecho del panel en pantallas anchas.
                float xValor = xNombre + Math.Min(
                    g.MeasureString(nombre, texto).Width, anchoNombreMaximo) + 15;
                g.DrawString(valor, textoNegrita, blanco, xValor, y);
            }
        }

        // Las ocho barras se dibujan en un panel de altura fija, desplazable
        // desde tabPanorama cuando la pantalla no ofrece altura suficiente.
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

            List<ReportePredictivoBE> productos = _datos.Where(x => x.Estimadas7Dias > 0).OrderByDescending(x => x.Estimadas7Dias).ThenBy(x => x.Producto).Take(8).ToList();
            g.DrawString(string.Format(T("frmReportePredictivo.Grafico"), productos.Count), titulo, blanco, 14, 12);
            g.DrawString(T("frmReportePredictivo.GraficoNota"), texto, blanco, 14, 36);

            if (productos.Count == 0)
            {
                g.DrawString(T("frmReportePredictivo.SinDatos"), texto, blanco, 15, 52);
                return;
            }

            int mayor = Math.Max(1, productos.Max(x => x.Estimadas7Dias));
            const int margenIzquierdo = 225;
            int anchoGrafico = Math.Max(10, pnlGrafico.ClientSize.Width - margenIzquierdo - 78);
            // 265px permiten 8 filas de 22px, con titulo y subtitulo.
            int paso = Math.Max(16, Math.Min(24, (pnlGrafico.ClientSize.Height - 76) / productos.Count));
            for (int i = 0; i < productos.Count; i++)
            {
                ReportePredictivoBE p = productos[i];
                int y = 61 + i * paso;
                if (y + paso > pnlGrafico.ClientSize.Height) break;
                string nombre = p.Producto.Length > 27? p.Producto.Substring(0, 24) + "..." : p.Producto;
                g.DrawString(nombre, texto, blanco, 14, y);
                float longitud = anchoGrafico * (float)p.Estimadas7Dias / mayor;
                g.FillRectangle(verde, margenIzquierdo, y + 2, longitud, paso - 5);
                g.DrawString(p.Estimadas7Dias.ToString(), texto, blanco,margenIzquierdo + longitud + 9, y);
            }
        }
    }
}
