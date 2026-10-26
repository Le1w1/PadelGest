using System;
using System.Globalization;
using System.IO;
using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfFont = iTextSharp.text.Font;

namespace UI.Servicios
{
    // Genera comprobantes de operaciones ya registradas. No modifica la base
    // de datos, no solicita pagos y no actualiza stock.
    internal static class ComprobantePdf
    {
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-AR");
        private static readonly BaseColor Azul = new BaseColor(24, 70, 138);
        private static readonly BaseColor AzulOscuro = new BaseColor(14, 43, 92);
        private static readonly BaseColor GrisClaro = new BaseColor(241, 245, 249);

        private static string Dinero(decimal importe) => "$ " + importe.ToString("N2", Cultura);

        private static BaseFont FuenteBase()
        {
            return BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }

        private static void Encabezado(Document documento, string titulo, string referencia, BaseFont fuente)
        {
            PdfFont marca = new PdfFont(fuente, 19, PdfFont.BOLD, AzulOscuro);
            PdfFont subtitulo = new PdfFont(fuente, 13, PdfFont.BOLD, Azul);
            PdfFont normal = new PdfFont(fuente, 9, PdfFont.NORMAL, BaseColor.DARK_GRAY);

            Paragraph encabezado = new Paragraph("AIRPADEL", marca)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 4
            };
            documento.Add(encabezado);
            documento.Add(new Paragraph("PadelGest - " + titulo, subtitulo)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 8
            });
            documento.Add(new Paragraph(referencia, normal)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 18
            });
        }

        private static void Seccion(Document documento, string titulo, BaseFont fuente)
        {
            documento.Add(new Paragraph(titulo, new PdfFont(fuente, 11, PdfFont.BOLD, Azul))
            {
                SpacingBefore = 9,
                SpacingAfter = 5
            });
        }

        private static PdfPTable TablaDatos()
        {
            PdfPTable tabla = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 8
            };
            tabla.SetWidths(new float[] { 32, 68 });
            return tabla;
        }

        private static void Dato(PdfPTable tabla, string nombre, string valor, BaseFont fuente)
        {
            PdfPCell etiqueta = new PdfPCell(new Phrase(nombre, new PdfFont(fuente, 9, PdfFont.BOLD, AzulOscuro)))
            {
                BackgroundColor = GrisClaro,
                BorderColor = BaseColor.LIGHT_GRAY,
                Padding = 7
            };
            PdfPCell contenido = new PdfPCell(new Phrase(valor, new PdfFont(fuente, 9)))
            {
                BorderColor = BaseColor.LIGHT_GRAY,
                Padding = 7
            };
            tabla.AddCell(etiqueta);
            tabla.AddCell(contenido);
        }

        private static void Total(Document documento, decimal importe, BaseFont fuente)
        {
            documento.Add(new Paragraph("TOTAL ABONADO: " + Dinero(importe),
                new PdfFont(fuente, 14, PdfFont.BOLD, AzulOscuro))
            {
                Alignment = Element.ALIGN_RIGHT,
                SpacingBefore = 13,
                SpacingAfter = 14
            });
        }

        private static void Pie(Document documento, BaseFont fuente)
        {
            documento.Add(new Paragraph(
                "Comprobante informativo generado por PadelGest para AirPadel. " +
                "La autorización bancaria se simula en esta implementación académica. " +
                "Este documento no reemplaza un comprobante fiscal oficial.",
                new PdfFont(fuente, 8, PdfFont.ITALIC, BaseColor.GRAY))
            {
                SpacingBefore = 20,
                Alignment = Element.ALIGN_CENTER
            });
        }

        public static void ExportarReserva(
            string ruta, ReservaBE reserva, ClienteBE cliente,
            CanchaBE cancha, TarifaBE tarifa, FacturaBE factura)
        {
            if (string.IsNullOrWhiteSpace(reserva.Codigo) || factura.ImporteTotal <= 0)
                throw new InvalidOperationException("La reserva debe estar registrada y pagada antes de exportar.");

            using FileStream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            using Document documento = new Document(PageSize.A4, 42, 42, 44, 40);
            PdfWriter.GetInstance(documento, archivo);
            documento.Open();

            BaseFont fuente = FuenteBase();
            Encabezado(documento, "COMPROBANTE DE RESERVA", "Código: " + reserva.Codigo, fuente);

            Seccion(documento, "Datos del cliente", fuente);
            PdfPTable datosCliente = TablaDatos();
            Dato(datosCliente, "Nombre y apellido", cliente.Nombre + " " + cliente.Apellido, fuente);
            Dato(datosCliente, "DNI", cliente.DNI, fuente);
            documento.Add(datosCliente);

            Seccion(documento, "Datos de la reserva", fuente);
            PdfPTable datosReserva = TablaDatos();
            Dato(datosReserva, "Código de reserva", reserva.Codigo, fuente);
            Dato(datosReserva, "Fecha de juego", reserva.Fecha.ToString("dd/MM/yyyy"), fuente);
            Dato(datosReserva, "Horario", reserva.Horario.ToString(@"hh\:mm"), fuente);
            Dato(datosReserva, "Cancha", cancha.Nombre, fuente);
            Dato(datosReserva, "Tarifa", tarifa.TipoTarifa, fuente);
            Dato(datosReserva, "Importe de cancha", Dinero(factura.ImporteTarifa), fuente);
            Dato(datosReserva, "Paletas", reserva.CantidadPaletas.ToString(Cultura), fuente);
            Dato(datosReserva, "Pelotas", reserva.CantidadPelotas.ToString(Cultura), fuente);
            Dato(datosReserva, "Importe de equipamiento", Dinero(factura.ImporteEquipamiento), fuente);
            Dato(datosReserva, "Estado de reserva", reserva.Estado, fuente);
            documento.Add(datosReserva);

            Seccion(documento, "Información del cobro", fuente);
            PdfPTable pago = TablaDatos();
            Dato(pago, "Fecha de emisión", factura.FechaHoraEmision.ToString("dd/MM/yyyy HH:mm"), fuente);
            Dato(pago, "Estado del pago", reserva.EstadoPago, fuente);
            Dato(pago, "Medio de pago", "Tarjeta de crédito / débito", fuente);
            Dato(pago, "Banco", reserva.Banco, fuente);
            if (!string.IsNullOrWhiteSpace(reserva.Ultimos4Tarjeta))
                Dato(pago, "Tarjeta", "**** " + reserva.Ultimos4Tarjeta, fuente);
            if (!string.IsNullOrWhiteSpace(reserva.CodigoAutorizacion))
                Dato(pago, "Autorización", reserva.CodigoAutorizacion, fuente);
            documento.Add(pago);

            Total(documento, factura.ImporteTotal, fuente);
            Pie(documento, fuente);
            documento.Close();
        }

        public static void ExportarFacturaBuffet(string ruta, FacturaBuffetBE factura, ClienteBE cliente)
        {
            if (factura.IdFactura <= 0 || factura.Estado != "Pagada" ||
                factura.Detalles == null || factura.Detalles.Count == 0 ||
                factura.DNI != cliente.DNI)
                throw new InvalidOperationException("La factura debe estar registrada y pagada antes de exportar.");

            using FileStream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            using Document documento = new Document(PageSize.A4, 42, 42, 44, 40);
            PdfWriter.GetInstance(documento, archivo);
            documento.Open();

            BaseFont fuente = FuenteBase();
            Encabezado(documento, "FACTURA DE BUFFET", "N° " + factura.NumeroFactura, fuente);

            Seccion(documento, "Datos de la operación", fuente);
            PdfPTable datos = TablaDatos();
            Dato(datos, "Número de factura", factura.NumeroFactura, fuente);
            Dato(datos, "Fecha de emisión", factura.FechaHoraEmision.ToString("dd/MM/yyyy HH:mm"), fuente);
            Dato(datos, "Cliente", cliente.Nombre + " " + cliente.Apellido, fuente);
            Dato(datos, "DNI", factura.DNI, fuente);
            Dato(datos, "Estado", factura.Estado, fuente);
            documento.Add(datos);

            Seccion(documento, "Detalle de productos", fuente);
            PdfPTable detalle = new PdfPTable(4)
            {
                WidthPercentage = 100,
                HeaderRows = 1,
                SpacingAfter = 10
            };
            detalle.SetWidths(new float[] { 48, 12, 20, 20 });
            string[] cabeceras = { "Producto", "Cantidad", "Precio unitario", "Subtotal" };
            foreach (string cabecera in cabeceras)
            {
                detalle.AddCell(new PdfPCell(new Phrase(cabecera,
                    new PdfFont(fuente, 9, PdfFont.BOLD, BaseColor.WHITE)))
                {
                    BackgroundColor = Azul,
                    Padding = 8,
                    BorderColor = Azul
                });
            }

            foreach (DetalleFacturaBE renglon in factura.Detalles)
            {
                string nombre = string.IsNullOrWhiteSpace(renglon.NombreProducto)
                    ? "Producto " + renglon.CodigoProducto : renglon.NombreProducto;
                string[] valores =
                {
                    nombre,
                    renglon.Cantidad.ToString(Cultura),
                    Dinero(renglon.PrecioUnitario),
                    Dinero(renglon.Subtotal)
                };
                for (int i = 0; i < valores.Length; i++)
                {
                    detalle.AddCell(new PdfPCell(new Phrase(valores[i], new PdfFont(fuente, 9)))
                    {
                        Padding = 8,
                        BorderColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = i == 0 ? Element.ALIGN_LEFT : Element.ALIGN_RIGHT
                    });
                }
            }
            documento.Add(detalle);
            Total(documento, factura.ImporteTotal, fuente);

            Seccion(documento, "Información del pago", fuente);
            PdfPTable pago = TablaDatos();
            Dato(pago, "Medio de pago", "Tarjeta de crédito / débito", fuente);
            Dato(pago, "Banco", factura.Banco, fuente);
            if (!string.IsNullOrWhiteSpace(factura.Ultimos4Tarjeta))
                Dato(pago, "Tarjeta", "**** " + factura.Ultimos4Tarjeta, fuente);
            Dato(pago, "Fecha del pago", factura.FechaHoraPago.ToString("dd/MM/yyyy HH:mm"), fuente);
            Dato(pago, "Autorización", factura.CodigoAutorizacion, fuente);
            documento.Add(pago);

            Pie(documento, fuente);
            documento.Close();
        }
    }
}
