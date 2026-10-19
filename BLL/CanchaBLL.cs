using BE;
using DAL;
using Servicios;

namespace BLL
{
    public class CanchaBLL
    {
        private readonly CanchaDAL _canchaDAL;

        public CanchaBLL()
        {
            _canchaDAL = new CanchaDAL();
        }

        /// Obtiene todas las canchas registradas.
        public List<CanchaBE> ObtenerCanchas()
        {
            return _canchaDAL.ObtenerCanchas();
        }

        /// Obtiene los horarios de reserva válidos que todavía pueden seleccionarse
        /// para la fecha indicada.
        public List<TimeSpan> ObtenerHorariosDisponibles(DateTime fecha)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            return ReglasReserva.ObtenerHorariosDisponibles(fecha);
        }

        /// Obtiene las canchas disponibles para la fecha y horario seleccionados
        /// durante el proceso de reserva.
        public List<CanchaBE> ObtenerCanchasDisponibles(DateTime fecha, TimeSpan horario)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");
            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            return _canchaDAL.ObtenerCanchasDisponibles(fecha, horario);
        }
    }
}
