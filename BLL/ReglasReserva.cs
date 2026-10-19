using Servicios;

namespace BLL
{
    internal static class ReglasReserva
    {
        public const int DiasAnticipacionMaxima = 7;

        private static readonly TimeSpan[] HorariosValidos =
        {
            new TimeSpan(8, 30, 0),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0),
            new TimeSpan(13, 0, 0),
            new TimeSpan(14, 30, 0),
            new TimeSpan(16, 0, 0),
            new TimeSpan(17, 30, 0),
            new TimeSpan(19, 0, 0),
            new TimeSpan(20, 30, 0)
        };

        // Método privado para traducir claves de error
        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        // Validar que la fecha y el horario de la reserva sean válidos
        public static void ValidarFechaYHorario(DateTime fecha, TimeSpan horario)
        {
            ValidarFecha(fecha);
            ValidarHorario(horario);

            DateTime inicioTurno = fecha.Date.Add(horario);

            if (inicioTurno <= DateTime.Now)
            {
                throw new Exception(T("Errores.Reserva.TurnoIniciado"));
            }
        }

        // Validar que la fecha de la reserva esté dentro del rango permitido
        public static void ValidarFecha(DateTime fecha)
        {
            DateTime fechaSeleccionada = fecha.Date;
            DateTime hoy = DateTime.Today;

            if (fechaSeleccionada < hoy)
            {
                throw new Exception(T("Errores.Reserva.FechaPasada"));
            }

            if (fechaSeleccionada > hoy.AddDays(DiasAnticipacionMaxima))
            {
                throw new Exception(T("Errores.Reserva.FechaFueraAnticipacion"));
            }
        }

        // Validar que el horario de la reserva sea uno de los horarios válidos
        public static void ValidarHorario(TimeSpan horario)
        {
            if (!HorariosValidos.Contains(horario))
            {
                throw new Exception(T("Errores.Reserva.HorarioInvalido"));
            }
        }

        // Obtener los horarios disponibles para una fecha específica
        public static List<TimeSpan> ObtenerHorariosDisponibles(DateTime fecha)
        {
            ValidarFecha(fecha);

            DateTime fechaSeleccionada = fecha.Date;

            return HorariosValidos.Where(horario => fechaSeleccionada.Add(horario) > DateTime.Now).ToList();
        }
    }
}
