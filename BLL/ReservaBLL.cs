using BE;
using BLL.Servicios;
using DAL;
using Servicios;

namespace BLL
{
    public class ReservaBLL
    {
        private readonly ReservaDAL _reservaDAL;

        public ReservaBLL()
        {
            _reservaDAL = new ReservaDAL();
        }

        private static string T(string clave) =>Traductor.Instancia.Traducir(clave);

        public ReservaBE RegistrarReserva(ClienteBE cliente,CanchaBE cancha,TarifaBE tarifa,FacturaBE facturaPagada,PagoBE pagoAprobado,DateTime fecha,TimeSpan horario,int cantidadPaletas,int cantidadPelotas)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (cliente == null || cliente.IdCliente <= 0)
                throw new Exception(T("Errores.Reserva.ClienteInvalido"));

            if (cancha == null || cancha.IdCancha <= 0)
                throw new Exception(T("Errores.Reserva.CanchaInvalida"));

            if (tarifa == null || tarifa.IdTarifa <= 0)
                throw new Exception(T("Errores.Reserva.TarifaInvalida"));

            if (facturaPagada == null ||!facturaPagada.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(T("Errores.Reserva.FacturaNoPagada"));
            }

            if (pagoAprobado == null || !pagoAprobado.Estado.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) || pagoAprobado.Importe != facturaPagada.ImporteTotal)
            {
                throw new Exception(T("Errores.Reserva.PagoInvalido"));
            }

            if (facturaPagada.IdCliente != cliente.IdCliente || facturaPagada.IdCancha != cancha.IdCancha || facturaPagada.IdTarifa != tarifa.IdTarifa)
            {
                throw new Exception(T("Errores.Reserva.DatosInconsistentes"));
            }

            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            if (cantidadPaletas < 0 || cantidadPelotas < 0)
                throw new Exception(T("Errores.Reserva.EquipamientoInvalido"));

            if (cantidadPaletas > EquipamientoBLL.MaximoPaletasPorReserva)
            {
                throw new Exception(string.Format(T("Errores.Equipamiento.MaximoPaletas"),EquipamientoBLL.MaximoPaletasPorReserva));
            }

            if (cantidadPelotas > EquipamientoBLL.MaximoPelotasPorReserva)
            {
                throw new Exception(string.Format(T("Errores.Equipamiento.MaximoPelotas"),EquipamientoBLL.MaximoPelotasPorReserva));
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

            BitacoraEvento evento =
                CrearEventoBitacora(reserva,cliente,cancha,facturaPagada,cantidadPaletas,cantidadPelotas);

            try
            {
                return _reservaDAL.RegistrarReserva(reserva,facturaPagada,pagoAprobado,evento);
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "TURNO_NO_DISPONIBLE")
            {
                throw new Exception(T("Errores.Reserva.TurnoYaNoDisponible"));
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "STOCK_PALETAS_INSUFICIENTE")
            {
                throw new Exception(string.Format(T("Errores.Equipamiento.StockInsuficienteRegistro"),T("Equipamiento.Paleta")));
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "STOCK_PELOTAS_INSUFICIENTE")
            {
                throw new Exception(string.Format(T("Errores.Equipamiento.StockInsuficienteRegistro"),T("Equipamiento.Pelota")));
            }
        }

        private BitacoraEvento CrearEventoBitacora(ReservaBE reserva,ClienteBE cliente,CanchaBE cancha,FacturaBE factura,int cantidadPaletas,int cantidadPelotas)
        {
            Usuario usuario = SM.Instancia.UsuarioActual;

            if (usuario == null)
            {
                throw new Exception(
                    "No hay un usuario activo para registrar la reserva.");
            }

            string descripcion =
                $"Reserva {reserva.Codigo} registrada para el Cliente DNI {cliente.DNI}. " +
                $"Cancha: {cancha.Nombre}. " +
                $"Fecha: {reserva.Fecha:dd/MM/yyyy}. " +
                $"Horario: {reserva.Horario:hh\\:mm}. " +
                $"Paletas: {cantidadPaletas}. " +
                $"Pelotas: {cantidadPelotas}. " +
                $"Importe abonado: {factura.ImporteTotal:C}.";

            return new BitacoraEvento
            {
                IdUsuario = usuario.IdUsuario,
                Usuario = usuario.NombreUsuario,
                FechaHora = DateTime.Now,
                Modulo = "Reserva",
                Accion = "Registrar reserva",
                Criticidad = "Alta",
                Resultado = "Exitoso",
                Descripcion = descripcion
            };
        }

        private string GenerarCodigoReserva(DateTime fecha)
        {
            string aleatorio = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();

            return $"RES-{fecha:yyyyMMdd}-{aleatorio}";
        }
    }
}
