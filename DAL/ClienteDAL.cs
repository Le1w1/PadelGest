using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ClienteDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        private const string COLUMNAS_CLIENTE ="IdCliente, DNI, Nombre, Apellido, Telefono, CorreoElectronico";

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

        public bool ExistePorDNI(string dni)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = "SELECT COUNT(1) FROM Cliente WHERE DNI = @DNI";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;
                    conexion.Open();

                    return Convert.ToInt32(comando.ExecuteScalar()) > 0;
                }
            }
        }

        public ClienteBE Insertar(ClienteBE cliente)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    INSERT INTO Cliente
                    (
                        DNI,
                        Nombre,
                        Apellido,
                        Telefono,
                        CorreoElectronico
                    )
                    OUTPUT INSERTED.IdCliente
                    VALUES
                    (
                        @DNI,
                        @Nombre,
                        @Apellido,
                        @Telefono,
                        @CorreoElectronico
                    )";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = cliente.DNI;
                    comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = cliente.Nombre;
                    comando.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50).Value = cliente.Apellido;
                    comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 30).Value = cliente.Telefono;
                    comando.Parameters.Add("@CorreoElectronico", SqlDbType.NVarChar, 150).Value = cliente.CorreoElectronico;

                    conexion.Open();

                    cliente.IdCliente = Convert.ToInt32(comando.ExecuteScalar());
                    return cliente;
                }
            }
        }

        /// Busca un Cliente por DNI. Devuelve null si no existe.
        public ClienteBE? BuscarPorDNI(string dni)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query ="SELECT " + COLUMNAS_CLIENTE + " FROM Cliente WHERE DNI = @DNI";

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
