using BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class TarifaDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        public TarifaDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        /// <summary>
        /// Obtiene la tarifa activa correspondiente al horario seleccionado.
        /// El rango se interpreta como HoraDesde inclusiva y HoraHasta exclusiva.
        /// </summary>
        public TarifaBE? ObtenerTarifa(TimeSpan horario)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query = @"
                    SELECT TOP 1
                        IdTarifa,
                        TipoTarifa,
                        Importe,
                        HoraDesde,
                        HoraHasta,
                        Activo
                    FROM Tarifa
                    WHERE Activo = 1
                      AND @Horario >= HoraDesde
                      AND @Horario < HoraHasta
                    ORDER BY HoraDesde DESC";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@Horario", SqlDbType.Time).Value = horario;

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TarifaBE
                            {
                                IdTarifa = Convert.ToInt32(reader["IdTarifa"]),
                                TipoTarifa = reader["TipoTarifa"].ToString() ?? string.Empty,
                                Importe = Convert.ToDecimal(reader["Importe"]),
                                HoraDesde = (TimeSpan)reader["HoraDesde"],
                                HoraHasta = (TimeSpan)reader["HoraHasta"],
                                Activo = Convert.ToBoolean(reader["Activo"])
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
