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
            DateTime fecha,
            TimeSpan horario,
            int cantidadPaletas,
            int cantidadPelotas)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (cliente == null || cliente.IdCliente <= 0)
            {
                throw new Exception(T("Errores.Reserva.ClienteInvalido"));
            }

            if (cancha == null || cancha.IdCancha <= 0)
            {
                throw new Exception(T("Errores.Reserva.CanchaInvalida"));
            }

            if (tarifa == null || tarifa.IdTarifa <= 0)
            {
                throw new Exception(T("Errores.Reserva.TarifaInvalida"));
            }

            if (facturaPagada == null ||
                facturaPagada.IdFactura <= 0 ||
                !facturaPagada.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(T("Errores.Reserva.FacturaNoPagada"));
            }

            if (facturaPagada.IdCliente != cliente.IdCliente ||
                facturaPagada.IdCancha != cancha.IdCancha ||
                facturaPagada.IdTarifa != tarifa.IdTarifa)
            {
                throw new Exception(T("Errores.Reserva.DatosInconsistentes"));
            }

            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            if (cantidadPaletas < 0 || cantidadPelotas < 0)
            {
                throw new Exception(T("Errores.Reserva.EquipamientoInvalido"));
            }

            ReservaBE reserva = new ReservaBE
            {
                Codigo = GenerarCodigoReserva(fecha),
                IdCliente = cliente.IdCliente,
                IdCancha = cancha.IdCancha,
                IdTarifa = tarifa.IdTarifa,
                IdFactura = facturaPagada.IdFactura,
                Fecha = fecha.Date,
                Horario = horario,
                CantidadPaletas = cantidadPaletas,
                CantidadPelotas = cantidadPelotas,
                Estado = "Reservada"
            };

            try
            {
                ReservaBE registrada = _reservaDAL.RegistrarReserva(reserva);
                _digitoVerificadorBLL.RecalcularDV("Reserva");
                return registrada;
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "TURNO_NO_DISPONIBLE")
            {
                throw new Exception(T("Errores.Reserva.TurnoYaNoDisponible"));
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
