using BE;
using Servicios;
using System.Text.RegularExpressions;

namespace BLL
{
    // valida los datos de tarjeta y obtiene la autorizacion bancaria
    // simulada, siguiendo la misma mecanica de PN1.
    public class CobroVentaBLL
    {
        private readonly BancoServicio _bancoServicio;

        public CobroVentaBLL()
        {
            _bancoServicio = new BancoServicio();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        public ResultadoCobroVentaBE CobrarVenta(CarritoBE carrito, string banco, string numeroTarjeta,DateTime vencimiento, string codigoSeguridad, bool respuestaSimuladaAprobada)
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");

            if (carrito == null || carrito.Estado != "Listo" ||string.IsNullOrWhiteSpace(carrito.DNI) || carrito.ImporteTotal <= 0 || carrito.Detalles.Count == 0)
                throw new Exception(T("Errores.Buffet.CarritoNoListo"));

            banco = (banco ?? string.Empty).Trim();
            numeroTarjeta = numeroTarjeta ?? string.Empty;
            // Se admiten espacios para separar grupos (ej.: 1111 2222 3333 4444),
            // pero no letras, guiones ni otros simbolos.
            if (!Regex.IsMatch(numeroTarjeta, @"^[0-9 ]+$"))
                throw new Exception(T("Errores.Cobro.TarjetaInvalida"));

            // BancoServicio y PagoBE reciben siempre los digitos sin espacios.
            numeroTarjeta = numeroTarjeta.Replace(" ", "");
            codigoSeguridad = (codigoSeguridad ?? string.Empty).Trim();

            if (!Regex.IsMatch(banco, @"^[\p{L}0-9 .&'-]{2,80}$"))
                throw new Exception(T("Errores.Cobro.BancoInvalido"));

            if (!Regex.IsMatch(numeroTarjeta, @"^[0-9]{13,19}$"))
                throw new Exception(T("Errores.Cobro.TarjetaInvalida"));

            DateTime finMes = new DateTime(vencimiento.Year, vencimiento.Month,DateTime.DaysInMonth(vencimiento.Year, vencimiento.Month));

            if (finMes < DateTime.Today)
                throw new Exception(T("Errores.Cobro.VencimientoInvalido"));

            if (!Regex.IsMatch(codigoSeguridad, @"^\d{3,4}$"))
                throw new Exception(T("Errores.Cobro.CodigoSeguridadInvalido"));

            ResultadoAutorizacionBanco autorizacion = _bancoServicio.AutorizarPago(
                carrito.DNI, numeroTarjeta, carrito.ImporteTotal, respuestaSimuladaAprobada, "VEN");

            if (!autorizacion.Aprobado)
                return new ResultadoCobroVentaBE { Aprobado = false };

            return new ResultadoCobroVentaBE
            {
                Aprobado = true,
                Pago = new PagoBE
                {
                    Banco = banco,
                    Ultimos4Tarjeta = numeroTarjeta[^4..],
                    Importe = carrito.ImporteTotal,
                    FechaHora = DateTime.Now,
                    Estado = "Aprobado",
                    CodigoAutorizacion = autorizacion.CodigoAutorizacion
                }
            };
        }


        // PN2 actualizado: autorización simulada de una factura preparada en memoria.
        public ResultadoCobroVentaBE CobrarFactura(FacturaBuffetBE factura, string banco, string numeroTarjeta,DateTime vencimiento, string codigoSeguridad, bool respuestaSimuladaAprobada)
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");

            if (factura == null || factura.Estado != "Listo" ||string.IsNullOrWhiteSpace(factura.DNI) || factura.ImporteTotal <= 0 || factura.Detalles.Count == 0)
                throw new Exception(T("Errores.Cobro.FacturaInvalida"));

            banco = (banco ?? string.Empty).Trim();
            numeroTarjeta = numeroTarjeta ?? string.Empty;
            // Se admiten espacios para separar grupos (ej.: 1111 2222 3333 4444),
            // pero no letras, guiones ni otros simbolos.
            if (!Regex.IsMatch(numeroTarjeta, @"^[0-9 ]+$"))
                throw new Exception(T("Errores.Cobro.TarjetaInvalida"));

            // BancoServicio y PagoBE reciben siempre los digitos sin espacios.
            numeroTarjeta = numeroTarjeta.Replace(" ", "");
            codigoSeguridad = (codigoSeguridad ?? string.Empty).Trim();

            if (!Regex.IsMatch(banco, @"^[\p{L}0-9 .&'-]{2,80}$"))
                throw new Exception(T("Errores.Cobro.BancoInvalido"));

            if (!Regex.IsMatch(numeroTarjeta, @"^[0-9]{13,19}$"))
                throw new Exception(T("Errores.Cobro.TarjetaInvalida"));

            DateTime finMes = new DateTime(vencimiento.Year, vencimiento.Month,DateTime.DaysInMonth(vencimiento.Year, vencimiento.Month));

            if (finMes < DateTime.Today)
                throw new Exception(T("Errores.Cobro.VencimientoInvalido"));

            if (!Regex.IsMatch(codigoSeguridad, @"^\d{3,4}$"))
                throw new Exception(T("Errores.Cobro.CodigoSeguridadInvalido"));

            ResultadoAutorizacionBanco autorizacion = _bancoServicio.AutorizarPago(
                factura.DNI, numeroTarjeta, factura.ImporteTotal, respuestaSimuladaAprobada, "FB");

            if (!autorizacion.Aprobado)
                return new ResultadoCobroVentaBE { Aprobado = false };

            return new ResultadoCobroVentaBE
            {
                Aprobado = true,
                Pago = new PagoBE
                {
                    Banco = banco,
                    Ultimos4Tarjeta = numeroTarjeta[^4..],
                    Importe = factura.ImporteTotal,
                    FechaHora = DateTime.Now,
                    Estado = "Aprobado",
                    CodigoAutorizacion = autorizacion.CodigoAutorizacion
                }
            };
        }

    }
}
