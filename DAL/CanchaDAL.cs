using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CanchaDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        private const string COLUMNAS_CANCHA = "IdCancha, Nombre, Estado";

        public CanchaDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        /// Mapea un registro de la base de datos a un objeto CanchaBE.
        private CanchaBE MapearCancha(SqlDataReader reader)
        {
            return new CanchaBE
            {
                IdCancha = Convert.ToInt32(reader["IdCancha"]),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Estado = reader["Estado"].ToString() ?? string.Empty
            };
        }

        /// Obtiene todas las canchas registradas.
        public List<CanchaBE> ObtenerCanchas()
        {
            List<CanchaBE> canchas = new List<CanchaBE>();

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query = "SELECT " + COLUMNAS_CANCHA + " FROM Cancha ORDER BY Nombre";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            canchas.Add(MapearCancha(reader));
                        }
                    }
                }
            }

            return canchas;
        }


        /// Obtiene las canchas disponibles para una fecha y horario determinados.
        /// Una cancha debe encontrarse disponible y no poseer una reserva vigente
        /// para la misma fecha y horario.
        public List<CanchaBE> ObtenerCanchasDisponibles(DateTime fecha, TimeSpan horario)
        {
            List<CanchaBE> canchas = new List<CanchaBE>();

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query = @"
                    SELECT c.IdCancha, c.Nombre, c.Estado
                    FROM Cancha c
                    WHERE c.Estado = N'Disponible'
                      AND NOT EXISTS
                      (
                          SELECT 1
                          FROM Reserva r
                          WHERE r.IdCancha = c.IdCancha
                            AND r.Fecha = @Fecha
                            AND r.Horario = @Horario
                            AND r.Estado <> N'Cancelada'
                      )
                    ORDER BY c.Nombre";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@Fecha", SqlDbType.Date).Value = fecha.Date;
                    comando.Parameters.Add("@Horario", SqlDbType.Time).Value = horario;

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            canchas.Add(MapearCancha(reader));
                        }
                    }
                }
            }
            return canchas;
        }
    }
}
