using BE;
using BLL.Servicios;
using DAL;
using Servicios;
using System.Text.RegularExpressions;

namespace BLL
{
    public class CobroReservaBLL
    {
        private readonly CobroReservaDAL _cobroDAL;
        private readonly BancoServicio _bancoServicio;
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL;

        public CobroReservaBLL()
        {
            _cobroDAL = new CobroReservaDAL();
            _bancoServicio = new BancoServicio();
            _digitoVerificadorBLL = new DigitoVerificadorBLL();
        }

        private static string T(string clave) =>
            Traductor.Instancia.Traducir(clave);

        public FacturaBE GenerarFactura(
            ClienteBE cliente,
            CanchaBE cancha,
            TarifaBE tarifa,
            DateTime fechaReserva,
            TimeSpan horario,
            int cantidadPaletas,
            int cantidadPelotas,
            decimal importeEquipamiento)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (cliente == null || cliente.IdCliente <= 0)
            {
                throw new Exception(T("Errores.Cobro.ClienteInvalido"));
            }

            if (cancha == null || cancha.IdCancha <= 0)
            {
                throw new Exception(T("Errores.Cobro.CanchaInvalida"));
            }

            if (tarifa == null || tarifa.IdTarifa <= 0 || tarifa.Importe < 0)
            {
                throw new Exception(T("Errores.Cobro.TarifaInvalida"));
            }

            ReglasReserva.ValidarFechaYHorario(fechaReserva, horario);

            if (cantidadPaletas < 0 || cantidadPelotas < 0 || importeEquipamiento < 0)
            {
                throw new Exception(T("Errores.Cobro.ImporteInvalido"));
            }

            decimal importeTotal = tarifa.Importe + importeEquipamiento;

            if (importeTotal <= 0)
            {
                throw new Exception(T("Errores.Cobro.ImporteInvalido"));
            }

            return new FacturaBE
            {
                IdCliente = cliente.IdCliente,
                IdCancha = cancha.IdCancha,
                IdTarifa = tarifa.IdTarifa,
                FechaHoraEmision = DateTime.Now,
                FechaReserva = fechaReserva.Date,
                Horario = horario,
                CantidadPaletas = cantidadPaletas,
                CantidadPelotas = cantidadPelotas,
                ImporteTarifa = tarifa.Importe,
                ImporteEquipamiento = importeEquipamiento,
                ImporteTotal = importeTotal,
                Estado = "Pendiente"
            };
        }

        public ResultadoCobroBE CobrarReserva(
            FacturaBE factura,
            ClienteBE cliente,
            string banco,
            string numeroTarjeta,
            DateTime fechaVencimiento,
            string codigoSeguridad,
            bool respuestaBancoAprobada)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (factura == null || cliente == null ||
                factura.IdCliente <= 0 ||
                factura.IdCliente != cliente.IdCliente ||
                factura.ImporteTotal <= 0)
            {
                throw new Exception(T("Errores.Cobro.FacturaInvalida"));
            }

            banco = (banco ?? string.Empty).Trim();
            numeroTarjeta = NormalizarNumeroTarjeta(numeroTarjeta);
            codigoSeguridad = (codigoSeguridad ?? string.Empty).Trim();

            ValidarBanco(banco);
            ValidarNumeroTarjeta(numeroTarjeta);
            ValidarVencimiento(fechaVencimiento);
            ValidarCodigoSeguridad(codigoSeguridad);

            // La factura se materializa en BD al primer intento de cobro.
            // Si el Banco rechaza la operación, permanece en estado Pendiente.
            if (factura.IdFactura == 0)
            {
                factura.FechaHoraEmision = DateTime.Now;
                _cobroDAL.CrearFacturaPendiente(factura);
                _digitoVerificadorBLL.RecalcularDV("Factura");
            }

            ResultadoAutorizacionBanco autorizacion =
                _bancoServicio.AutorizarPago(
                    cliente.DNI,
                    numeroTarjeta,
                    factura.ImporteTotal,
                    respuestaBancoAprobada);

            if (!autorizacion.Aprobado)
            {
                return new ResultadoCobroBE
                {
                    Aprobado = false,
                    Factura = factura,
                    Pago = null
                };
            }

            PagoBE pago = new PagoBE
            {
                IdFactura = factura.IdFactura,
                Banco = banco,
                Ultimos4Tarjeta = numeroTarjeta[^4..],
                Importe = factura.ImporteTotal,
                FechaHora = DateTime.Now,
                Estado = "Aprobado",
                CodigoAutorizacion = autorizacion.CodigoAutorizacion
            };

            _cobroDAL.RegistrarPagoAprobado(pago);

            factura.Estado = "Pagada";

            _digitoVerificadorBLL.RecalcularDV("Pago");
            _digitoVerificadorBLL.RecalcularDV("Factura");

            return new ResultadoCobroBE
            {
                Aprobado = true,
                Factura = factura,
                Pago = pago
            };
        }

        private void ValidarBanco(string banco)
        {
            if (string.IsNullOrWhiteSpace(banco))
            {
                throw new Exception(T("Errores.Cobro.BancoObligatorio"));
            }

            if (!Regex.IsMatch(banco, @"^[\p{L}0-9 .&'-]{2,80}$"))
            {
                throw new Exception(T("Errores.Cobro.BancoInvalido"));
            }
        }

        private void ValidarNumeroTarjeta(string numeroTarjeta)
        {
            if (!Regex.IsMatch(numeroTarjeta, @"^\d{13,19}$"))
            {
                throw new Exception(T("Errores.Cobro.TarjetaInvalida"));
            }
        }

        private void ValidarVencimiento(DateTime fechaVencimiento)
        {
            DateTime finMes =
                new DateTime(
                    fechaVencimiento.Year,
                    fechaVencimiento.Month,
                    DateTime.DaysInMonth(
                        fechaVencimiento.Year,
                        fechaVencimiento.Month));

            if (finMes < DateTime.Today)
            {
                throw new Exception(T("Errores.Cobro.VencimientoInvalido"));
            }
        }

        private void ValidarCodigoSeguridad(string codigoSeguridad)
        {
            if (!Regex.IsMatch(codigoSeguridad, @"^\d{3,4}$"))
            {
                throw new Exception(T("Errores.Cobro.CodigoSeguridadInvalido"));
            }
        }

        private string NormalizarNumeroTarjeta(string numeroTarjeta)
        {
            return Regex.Replace(
                numeroTarjeta ?? string.Empty,
                @"[\s-]",
                string.Empty);
        }

    }
}
