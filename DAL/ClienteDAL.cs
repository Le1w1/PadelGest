using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ClienteDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        private const string COLUMNAS_CLIENTE ="DNI, Nombre, Apellido, Telefono, CorreoElectronico";

        public ClienteDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        /// Mapea un registro de BD a un objeto ClienteBE
        private ClienteBE MapearCliente(SqlDataReader reader)
        {
            return new ClienteBE
            {
                DNI = reader["DNI"].ToString() ?? string.Empty,
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Apellido = reader["Apellido"].ToString() ?? string.Empty,
                Telefono = reader["Telefono"].ToString() ?? string.Empty,
                CorreoElectronico = reader["CorreoElectronico"].ToString() ?? string.Empty
            };
        }


        /// Verifica si un Cliente existe por su DNI
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


        /// Inserta un nuevo Cliente identificado por su DNI (clave primaria natural).
        public ClienteBE Insertar(ClienteBE cliente)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    INSERT INTO Cliente
                    (
                        DNI,Nombre,Apellido,Telefono,CorreoElectronico
                    )
                    VALUES
                    (
                        @DNI,@Nombre,@Apellido,@Telefono,@CorreoElectronico
                    )";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = cliente.DNI;
                    comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = cliente.Nombre;
                    comando.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50).Value = cliente.Apellido;
                    comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 30).Value = cliente.Telefono;
                    comando.Parameters.Add("@CorreoElectronico", SqlDbType.NVarChar, 150).Value = cliente.CorreoElectronico;

                    conexion.Open();
                    comando.ExecuteNonQuery();
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
        public List<ClienteBE> ListarTodos()
        {
            var clientes = new List<ClienteBE>();
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();
            using SqlCommand comando = new SqlCommand("SELECT " + COLUMNAS_CLIENTE + " FROM Cliente ORDER BY Apellido, Nombre", conexion);
            conexion.Open();
            using SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read()) clientes.Add(MapearCliente(reader));
            return clientes;
        }

        public bool Modificar(ClienteBE cliente)
        {
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();
            using SqlCommand comando = new SqlCommand(
                "UPDATE Cliente SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono, CorreoElectronico=@CorreoElectronico WHERE DNI=@DNI", conexion);
            AgregarParametrosCliente(comando, cliente);
            conexion.Open();
            return comando.ExecuteNonQuery() == 1;
        }

        public bool EliminarSinOperaciones(string dni)
        {
            // La misma instruccion comprueba las referencias y elimina solo si no hay historial.
            const string sql = @"DELETE FROM Cliente
                WHERE DNI=@DNI
                AND NOT EXISTS (SELECT 1 FROM Reserva WHERE DNI=@DNI)
                AND NOT EXISTS (SELECT 1 FROM Factura WHERE DNI=@DNI)
                AND NOT EXISTS (SELECT 1 FROM FacturaBuffet WHERE DNI=@DNI)";
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();
            using SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;
            conexion.Open();
            return comando.ExecuteNonQuery() == 1;
        }

        private static void AgregarParametrosCliente(SqlCommand comando, ClienteBE cliente)
        {
            comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = cliente.DNI;
            comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = cliente.Nombre;
            comando.Parameters.Add("@Apellido", SqlDbType.NVarChar, 50).Value = cliente.Apellido;
            comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 30).Value = cliente.Telefono;
            comando.Parameters.Add("@CorreoElectronico", SqlDbType.NVarChar, 150).Value = cliente.CorreoElectronico;
        }
    }
}
