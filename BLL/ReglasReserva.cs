using Servicios;

namespace BLL
{
    internal static class ReglasReserva
    {
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

        private static string T(string clave) =>
            Traductor.Instancia.Traducir(clave);

        public static void ValidarFechaYHorario(DateTime fecha, TimeSpan horario)
        {
            ValidarHorario(horario);

            DateTime fechaSeleccionada = fecha.Date;
            DateTime hoy = DateTime.Today;

            if (fechaSeleccionada < hoy)
            {
                throw new Exception(T("Errores.Reserva.FechaPasada"));
            }

            if (fechaSeleccionada > hoy.AddDays(7))
            {
                throw new Exception(T("Errores.Reserva.FechaFueraAnticipacion"));
            }

            DateTime inicioTurno = fechaSeleccionada.Add(horario);

            if (inicioTurno <= DateTime.Now)
            {
                throw new Exception(T("Errores.Reserva.TurnoIniciado"));
            }
        }

        public static void ValidarHorario(TimeSpan horario)
        {
            if (!HorariosValidos.Contains(horario))
            {
                throw new Exception(T("Errores.Reserva.HorarioInvalido"));
            }
        }
    }
}
