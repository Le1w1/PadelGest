using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfFont = iTextSharp.text.Font;
using PdfImage = iTextSharp.text.Image;

namespace UI.Servicios
{
    // Presenta resultados ya calculados por la BLL; no vuelve a estimar demanda.
    internal static class ReportePredictivoPdf
    {
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-AR");
        private static readonly BaseColor Azul = new BaseColor(24, 70, 138);
        private static readonly BaseColor Suave = new BaseColor(236, 242, 249);
        private static readonly Color[] Colores =
        {
            Color.FromArgb(214, 246, 36), Color.FromArgb(73, 200, 233),
            Color.FromArgb(255, 181, 79), Color.FromArgb(186, 147, 255),
            Color.FromArgb(101, 224, 162), Color.FromArgb(185, 193, 208)
        };

        private static string Numero(decimal n) => n.ToString("N2", Cultura);
        private static string Dinero(decimal n) => "$ " + Numero(n);

        private static void Seccion(Document documento, string titulo, BaseFont fuente)
        {
            documento.Add(new Paragraph(titulo, new PdfFont(fuente, 12, PdfFont.BOLD, Azul))
            { SpacingBefore = 14, SpacingAfter = 8 });
        }

        private static PdfPTable Tabla(BaseFont fuente, params string[] cabeceras)
        {
            PdfPTable tabla = new PdfPTable(cabeceras.Length)
            { WidthPercentage = 100, HeaderRows = 1, SpacingAfter = 10, SplitLate = false };
            foreach (string texto in cabeceras)
                tabla.AddCell(new PdfPCell(new Phrase(texto, new PdfFont(fuente, 8, PdfFont.BOLD, BaseColor.WHITE)))
                { BackgroundColor = Azul, Padding = 6, BorderColor = Azul });
            return tabla;
        }

        private static void Fila(PdfPTable tabla, BaseFont fuente, params string[] valores)
        {
            foreach (string valor in valores)
                tabla.AddCell(new PdfPCell(new Phrase(valor, new PdfFont(fuente, 8)))
                { Padding = 6, BorderColor = BaseColor.LIGHT_GRAY,
                    BackgroundColor = tabla.Rows.Count % 2 == 0 ? BaseColor.WHITE : Suave });
        }

        private static byte[] Dona(List<ReportePredictivoBE> datos)
        {
            var grupos = datos.Where(x => x.Estimadas7Dias > 0)
                .GroupBy(x => string.IsNullOrWhiteSpace(x.Categoria) ? "Sin categoría" : x.Categoria)
                .Select(g => new { Nombre = g.Key, Cantidad = g.Sum(x => (long)x.Estimadas7Dias) })
                .OrderByDescending(x => x.Cantidad).ThenBy(x => x.Nombre).ToList();
            long total = grupos.Sum(x => x.Cantidad);
            using Bitmap bitmap = new Bitmap(950, 340);
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using System.Drawing.Font fuente = new System.Drawing.Font("Segoe UI", 13);
            using System.Drawing.Font negrita = new System.Drawing.Font("Segoe UI", 15, FontStyle.Bold);
            using SolidBrush texto = new SolidBrush(Color.FromArgb(35, 44, 60));
            if (total == 0)
                g.DrawString("No hay demanda estimada para representar.", fuente, texto, 20, 120);
            else
            {
                var sectores = grupos.Take(5).Select(x => (x.Nombre, x.Cantidad)).ToList();
                long otros = grupos.Skip(5).Sum(x => x.Cantidad);
                if (otros > 0) sectores.Add(("Otros", otros));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(36, 36, 268, 268);
                float inicio = -90f;
                for (int i = 0; i < sectores.Count; i++)
                {
                    float angulo = 360f * sectores[i].Cantidad / total;
                    using SolidBrush color = new SolidBrush(Colores[i]);
                    g.FillPie(color, rect, inicio, angulo);
                    inicio += angulo;
                }
                g.FillEllipse(Brushes.White, 105, 105, 130, 130);
                string totalTexto = total.ToString("N0", Cultura);
                SizeF medida = g.MeasureString(totalTexto, negrita);
                g.DrawString(totalTexto, negrita, texto, 170 - medida.Width / 2, 150);
                for (int i = 0; i < sectores.Count; i++)
                {
                    float y = 40 + i * 48;
                    using SolidBrush color = new SolidBrush(Colores[i]);
                    g.FillRectangle(color, 355, y + 3, 20, 20);
                    decimal porcentaje = 100m * sectores[i].Cantidad / total;
                    string nombre = sectores[i].Nombre;
                    if (nombre.Length > 32) nombre = nombre.Substring(0, 29) + "...";
                    g.DrawString(nombre, fuente, texto, 392, y);
                    g.DrawString(porcentaje.ToString("0.#", Cultura) + "% (" +
                        sectores[i].Cantidad.ToString("N0", Cultura) + ")", fuente, texto, 730, y);
                }
            }
            using MemoryStream memoria = new MemoryStream();
            bitmap.Save(memoria, ImageFormat.Png);
            return memoria.ToArray();
        }

        public static void Exportar(string ruta, IReadOnlyList<ReportePredictivoBE> resultados,
            DateTime fechaGeneracion)
        {
            if (resultados == null) throw new ArgumentNullException(nameof(resultados));
            List<ReportePredictivoBE> datos = resultados.ToList();
            using FileStream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            using Document documento = new Document(PageSize.A4.Rotate(), 35, 35, 36, 36);
            PdfWriter escritor = PdfWriter.GetInstance(documento, archivo);
            documento.Open();
            BaseFont fuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            documento.Add(new Paragraph("AIRPADEL | PADELGEST",
                new PdfFont(fuente, 18, PdfFont.BOLD, Azul)) { SpacingAfter = 5 });
            documento.Add(new Paragraph("REPORTE INTELIGENTE DEL BUFFET",
                new PdfFont(fuente, 14, PdfFont.BOLD)) { SpacingAfter = 5 });
            documento.Add(new Paragraph("Generado: " + fechaGeneracion.ToString("dd/MM/yyyy HH:mm") +
                "  |  Histórico: " + fechaGeneracion.Date.AddDays(-28).ToString("dd/MM/yyyy") +
                " al " + fechaGeneracion.Date.AddDays(-1).ToString("dd/MM/yyyy") +
                "  |  Pronóstico: 7 días", new PdfFont(fuente, 9)) { SpacingAfter = 10 });

            Seccion(documento, "01. Panorama general", fuente);
            PdfPTable indicadores = Tabla(fuente, "Unidades vendidas (28d)", "Demanda estimada (7d)",
                "Productos con reposición", "Unidades a reponer", "Productos críticos");
            Fila(indicadores, fuente,
                datos.Sum(x => (long)x.Vendidas28Dias).ToString("N0", Cultura),
                datos.Sum(x => (long)x.Estimadas7Dias).ToString("N0", Cultura),
                datos.Count(x => x.ReposicionSugerida > 0).ToString(Cultura),
                datos.Sum(x => (long)x.ReposicionSugerida).ToString("N0", Cultura),
                datos.Count(x => x.Prioridad == "Critica").ToString(Cultura));
            documento.Add(indicadores);

            Seccion(documento, "02. Distribución de demanda estimada por categoría", fuente);
            PdfImage dona = PdfImage.GetInstance(Dona(datos));
            dona.ScaleToFit(720f, 245f);
            documento.Add(dona);
            documento.Add(new Paragraph("Participación en unidades proyectadas; cinco categorías principales y resto agrupado en Otros.",
                new PdfFont(fuente, 8)) { SpacingAfter = 12 });

            Seccion(documento, "03. Prioridades de reposición", fuente);
            PdfPTable reposicion = Tabla(fuente, "Producto", "Categoría", "Prioridad", "Stock",
                "Demanda 7d", "Reposición");
            reposicion.SetWidths(new float[] { 28, 21, 14, 10, 13, 14 });
            foreach (ReportePredictivoBE p in datos.Where(x => x.ReposicionSugerida > 0)
                .OrderBy(x => x.Prioridad == "Critica" ? 0 : x.Prioridad == "Alta" ? 1 : 2)
                .ThenByDescending(x => x.ReposicionSugerida))
                Fila(reposicion, fuente, p.Producto, p.Categoria, p.Prioridad,
                    p.StockDisponible.ToString(Cultura), p.Estimadas7Dias.ToString(Cultura),
                    p.ReposicionSugerida.ToString(Cultura));
            if (reposicion.Rows.Count == 1) Fila(reposicion, fuente, "Sin reposición sugerida", "", "", "", "", "");
            documento.Add(reposicion);

            Seccion(documento, "04. Detalle por producto", fuente);
            PdfPTable detalle = Tabla(fuente, "Producto", "Categoría", "Ventas 28d", "Promedio/día",
                "Estimación 7d", "Stock", "Mínimo", "Cobertura/d", "Reponer", "Prioridad", "Ingreso potencial");
            detalle.SetWidths(new float[] { 22, 17, 10, 11, 11, 8, 8, 11, 9, 11, 17 });
            foreach (ReportePredictivoBE p in datos.OrderByDescending(x => x.Estimadas7Dias))
                Fila(detalle, fuente, p.Producto, p.Categoria, p.Vendidas28Dias.ToString(Cultura),
                    Numero(p.PromedioDiario), p.Estimadas7Dias.ToString(Cultura),
                    p.StockDisponible.ToString(Cultura), p.StockMinimo.ToString(Cultura),
                    Numero(p.CoberturaDias), p.ReposicionSugerida.ToString(Cultura),
                    p.Prioridad, Dinero(p.IngresoPotencial7Dias));
            documento.Add(detalle);

            Seccion(documento, "05. Análisis por categoría", fuente);
            PdfPTable categorias = Tabla(fuente, "Categoría", "Productos", "Ventas 28d",
                "Estimación 7d", "Reposición", "Ingreso potencial");
            foreach (var g in datos.GroupBy(x => x.Categoria).OrderByDescending(g => g.Sum(x => x.Estimadas7Dias)))
                Fila(categorias, fuente, g.Key, g.Count().ToString(Cultura),
                    g.Sum(x => (long)x.Vendidas28Dias).ToString(Cultura),
                    g.Sum(x => (long)x.Estimadas7Dias).ToString(Cultura),
                    g.Sum(x => (long)x.ReposicionSugerida).ToString(Cultura),
                    Dinero(g.Sum(x => x.IngresoPotencial7Dias)));
            documento.Add(categorias);
            documento.Add(new Paragraph("Metodología: facturas pagadas de los últimos 28 días completos; " +
                "estimación para los próximos 7 días. Las proyecciones son orientativas y " +
                "no generan compras ni modifican existencias.", new PdfFont(fuente, 8))
                { SpacingBefore = 12 });
            documento.Close();
        }
    }
}
