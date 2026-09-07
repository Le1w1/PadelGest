using BE;
using DAL;
using Servicios;

namespace BLL
{
    public class TarifaBLL
    {
        private readonly TarifaDAL _tarifaDAL;

        public TarifaBLL()
        {
            _tarifaDAL = new TarifaDAL();
        }

        /// Obtiene la tarifa activa correspondiente al horario seleccionado
        /// durante el proceso de reserva.
        public TarifaBE ObtenerTarifa(TimeSpan horario)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");
            ReglasReserva.ValidarHorario(horario);

            TarifaBE? tarifa = _tarifaDAL.ObtenerTarifa(horario);

            if (tarifa == null)
            {
                throw new Exception(Traductor.Instancia.Traducir("Errores.Reserva.TarifaNoDisponible"));
            }

            return tarifa;
        }
    }
}
