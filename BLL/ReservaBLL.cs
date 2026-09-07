using BE;
using BLL.Servicios;
using DAL;
using Servicios;

namespace BLL
{
    public class ReservaBLL
    {
        private readonly ReservaDAL _reservaDAL;
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL;

        public ReservaBLL()
        {
            _reservaDAL = new ReservaDAL();
            _digitoVerificadorBLL = new DigitoVerificadorBLL();
        }

        private static string T(string clave) =>
            Traductor.Instancia.Traducir(clave);

        public ReservaBE RegistrarReserva(
            ClienteBE cliente,
            CanchaBE cancha,
            TarifaBE tarifa,
            FacturaBE facturaPagada,
            PagoBE pagoAprobado,
            DateTime fecha,
            TimeSpan horario,
            int cantidadPaletas,
            int cantidadPelotas)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (cliente == null || cliente.IdCliente <= 0)
                throw new Exception(T("Errores.Reserva.ClienteInvalido"));

            if (cancha == null || cancha.IdCancha <= 0)
                throw new Exception(T("Errores.Reserva.CanchaInvalida"));

            if (tarifa == null || tarifa.IdTarifa <= 0)
                throw new Exception(T("Errores.Reserva.TarifaInvalida"));

            if (facturaPagada == null ||
                !facturaPagada.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(T("Errores.Reserva.FacturaNoPagada"));
            }

            if (pagoAprobado == null ||
                !pagoAprobado.Estado.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) ||
                pagoAprobado.Importe != facturaPagada.ImporteTotal)
            {
                throw new Exception(T("Errores.Reserva.PagoInvalido"));
            }

            if (facturaPagada.IdCliente != cliente.IdCliente ||
                facturaPagada.IdCancha != cancha.IdCancha ||
                facturaPagada.IdTarifa != tarifa.IdTarifa)
            {
                throw new Exception(T("Errores.Reserva.DatosInconsistentes"));
            }

            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            if (cantidadPaletas < 0 || cantidadPelotas < 0)
                throw new Exception(T("Errores.Reserva.EquipamientoInvalido"));

            if (cantidadPaletas > EquipamientoBLL.MaximoPaletasPorReserva)
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.MaximoPaletas"),
                    EquipamientoBLL.MaximoPaletasPorReserva));
            }

            if (cantidadPelotas > EquipamientoBLL.MaximoPelotasPorReserva)
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.MaximoPelotas"),
                    EquipamientoBLL.MaximoPelotasPorReserva));
            }

            facturaPagada.FechaHoraEmision = DateTime.Now;

            ReservaBE reserva = new ReservaBE
            {
                Codigo = GenerarCodigoReserva(fecha),
                IdCliente = cliente.IdCliente,
                IdCancha = cancha.IdCancha,
                IdTarifa = tarifa.IdTarifa,
                Fecha = fecha.Date,
                Horario = horario,
                CantidadPaletas = cantidadPaletas,
                CantidadPelotas = cantidadPelotas,
                Estado = "Reservada"
            };

            try
            {
                ReservaBE registrada =
                    _reservaDAL.RegistrarReserva(
                        reserva,
                        facturaPagada,
                        pagoAprobado);

                _digitoVerificadorBLL.RecalcularDV("Factura");
                _digitoVerificadorBLL.RecalcularDV("Pago");
                _digitoVerificadorBLL.RecalcularDV("Reserva");

                return registrada;
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "TURNO_NO_DISPONIBLE")
            {
                throw new Exception(T("Errores.Reserva.TurnoYaNoDisponible"));
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "STOCK_PALETAS_INSUFICIENTE")
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.StockInsuficienteRegistro"),
                    T("Equipamiento.Paleta")));
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "STOCK_PELOTAS_INSUFICIENTE")
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.StockInsuficienteRegistro"),
                    T("Equipamiento.Pelota")));
            }
        }

        private string GenerarCodigoReserva(DateTime fecha)
        {
            string aleatorio =
                Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();

            return $"RES-{fecha:yyyyMMdd}-{aleatorio}";
        }
    }
}
