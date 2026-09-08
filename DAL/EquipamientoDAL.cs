using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class EquipamientoDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        public EquipamientoDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        /// Mapea un registro de BD a un objeto EquipamientoBE
        private EquipamientoBE MapearEquipamiento(SqlDataReader reader)
        {
            return new EquipamientoBE
            {
                IdEquipamiento = Convert.ToInt32(reader["IdEquipamiento"]),
                Tipo = reader["Tipo"].ToString() ?? string.Empty,
                Importe = Convert.ToDecimal(reader["Importe"]),
                StockDisponible = Convert.ToInt32(reader["StockDisponible"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }


        /// Obtiene el equipamiento disponible para una fecha y horario.
        /// Para cada turno se descuenta solamente lo ya reservado en ese mismoturno.
        /// las reservas de otros horarios no afectan la disponibilidad.
        public List<EquipamientoBE> ObtenerEquipamientosDisponiblesPorTurno(DateTime fecha,TimeSpan horario)
        {
            List<EquipamientoBE> equipamientos = new List<EquipamientoBE>();

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    SELECT
                        e.IdEquipamiento, e.Tipo, e.Importe, e.StockDisponible, e.Activo,
                        ISNULL((
                            SELECT SUM(r.CantidadPaletas)
                            FROM Reserva r
                            WHERE r.Fecha = @Fecha
                              AND r.Horario = @Horario
                              AND r.Estado <> N'Cancelada'
                        ), 0) AS PaletasReservadas,
                        ISNULL((
                            SELECT SUM(r.CantidadPelotas)
                            FROM Reserva r
                            WHERE r.Fecha = @Fecha
                              AND r.Horario = @Horario
                              AND r.Estado <> N'Cancelada'
                        ), 0) AS PelotasReservadas
                    FROM Equipamiento e
                    WHERE e.Activo = 1
                    ORDER BY e.Tipo";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = fecha.Date;
                    comando.Parameters.Add("@Horario", System.Data.SqlDbType.Time).Value = horario;

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EquipamientoBE equipamiento = MapearEquipamiento(reader);

                            int reservado = equipamiento.Tipo.Equals("Paleta", StringComparison.OrdinalIgnoreCase)? Convert.ToInt32(reader["PaletasReservadas"]): Convert.ToInt32(reader["PelotasReservadas"]);

                            equipamiento.StockDisponible =Math.Max(0, equipamiento.StockDisponible - reservado);

                            equipamientos.Add(equipamiento);
                        }
                    }
                }
            }
            return equipamientos;
        }
    }
}
