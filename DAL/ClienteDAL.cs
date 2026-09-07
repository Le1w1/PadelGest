using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ClienteDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        private const string COLUMNAS_CLIENTE =
            "IdCliente, DNI, Nombre, Apellido, Telefono, CorreoElectronico";

        public ClienteDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        private ClienteBE MapearCliente(SqlDataReader reader)
        {
            return new ClienteBE
            {
                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                DNI = reader["DNI"].ToString() ?? string.Empty,
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Apellido = reader["Apellido"].ToString() ?? string.Empty,
                Telefono = reader["Telefono"].ToString() ?? string.Empty,
                CorreoElectronico = reader["CorreoElectronico"].ToString() ?? string.Empty
            };
        }

        /// Busca un Cliente por DNI. Devuelve null si no existe.
        public ClienteBE? BuscarPorDNI(string dni)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query =
                    "SELECT " + COLUMNAS_CLIENTE + " FROM Cliente WHERE DNI = @DNI";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;

                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearCliente(reader);
                        }
                    }
                }
            }

            return null;
        }
    }
}
