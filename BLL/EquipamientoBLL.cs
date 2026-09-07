using BE;
using DAL;
using Servicios;

namespace BLL
{
    public class EquipamientoBLL
    {
        public const int MaximoPaletasPorReserva = 4;
        public const int MaximoPelotasPorReserva = 3;

        private readonly EquipamientoDAL _equipamientoDAL;

        public EquipamientoBLL()
        {
            _equipamientoDAL = new EquipamientoDAL();
        }

        private static string T(string clave) =>
            Traductor.Instancia.Traducir(clave);

        /// Obtiene el equipamiento activo para mostrar stock e importe.
        public List<EquipamientoBE> ObtenerEquipamientosActivos(
            DateTime fecha,
            TimeSpan horario)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");
            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            return _equipamientoDAL.ObtenerEquipamientosDisponiblesPorTurno(
                fecha,
                horario);
        }

        /// Valida cantidades, limites y stock actual. Si todo es valido,
        /// devuelve el importe total del equipamiento solicitado.
        public decimal ValidarYCalcularImporte(
            DateTime fecha,
            TimeSpan horario,
            int cantidadPaletas,
            int cantidadPelotas)
        {
            SM.Instancia.RequierePermiso("RES_CREAR");

            if (cantidadPaletas < 0 || cantidadPelotas < 0)
            {
                throw new Exception(T("Errores.Equipamiento.CantidadInvalida"));
            }

            if (cantidadPaletas == 0 && cantidadPelotas == 0)
            {
                throw new Exception(T("Errores.Equipamiento.DebeSeleccionar"));
            }

            if (cantidadPaletas > MaximoPaletasPorReserva)
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.MaximoPaletas"),
                    MaximoPaletasPorReserva));
            }

            if (cantidadPelotas > MaximoPelotasPorReserva)
            {
                throw new Exception(string.Format(
                    T("Errores.Equipamiento.MaximoPelotas"),
                    MaximoPelotasPorReserva));
            }

            ReglasReserva.ValidarFechaYHorario(fecha, horario);

            List<EquipamientoBE> equipamientos =
                _equipamientoDAL.ObtenerEquipamientosDisponiblesPorTurno(
                    fecha,
                    horario);

            EquipamientoBE? paleta = equipamientos.FirstOrDefault(
                e => e.Tipo.Equals("Paleta", StringComparison.OrdinalIgnoreCase));

            EquipamientoBE? pelota = equipamientos.FirstOrDefault(
                e => e.Tipo.Equals("Pelota", StringComparison.OrdinalIgnoreCase));

            decimal total = 0;

            if (cantidadPaletas > 0)
            {
                if (paleta == null)
                {
                    throw new Exception(string.Format(
                        T("Errores.Equipamiento.NoDisponible"),
                        T("Equipamiento.Paleta")));
                }

                if (cantidadPaletas > paleta.StockDisponible)
                {
                    throw new Exception(string.Format(
                        T("Errores.Equipamiento.StockInsuficiente"),
                        T("Equipamiento.Paleta"),
                        paleta.StockDisponible));
                }

                total += cantidadPaletas * paleta.Importe;
            }

            if (cantidadPelotas > 0)
            {
                if (pelota == null)
                {
                    throw new Exception(string.Format(
                        T("Errores.Equipamiento.NoDisponible"),
                        T("Equipamiento.Pelota")));
                }

                if (cantidadPelotas > pelota.StockDisponible)
                {
                    throw new Exception(string.Format(
                        T("Errores.Equipamiento.StockInsuficiente"),
                        T("Equipamiento.Pelota"),
                        pelota.StockDisponible));
                }

                total += cantidadPelotas * pelota.Importe;
            }

            return total;
        }
    }
}
