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

        /// Obtiene el equipamiento activo disponible para alquiler.
        public List<EquipamientoBE> ObtenerEquipamientosActivos()
        {
            List<EquipamientoBE> equipamientos = new List<EquipamientoBE>();

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    SELECT IdEquipamiento, Tipo, Importe, StockDisponible, Activo
                    FROM Equipamiento
                    WHERE Activo = 1
                    ORDER BY Tipo";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipamientos.Add(MapearEquipamiento(reader));
                        }
                    }
                }
            }

            return equipamientos;
        }
    }
}
