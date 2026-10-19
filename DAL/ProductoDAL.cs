using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProductoDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        private const string COLUMNAS_PRODUCTO ="CodigoProducto, Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo";

        public ProductoDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        // Convierte los datos de SQL Server a una entidad del buffet.
        private static ProductoBE MapearProducto(SqlDataReader reader)
        {
            return new ProductoBE
            {
                CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Categoria = reader["Categoria"].ToString() ?? string.Empty,
                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                StockDisponible = Convert.ToInt32(reader["StockDisponible"]),
                StockMinimo = Convert.ToInt32(reader["StockMinimo"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

        // CUN09: Muestra tambien los productos sin stock para poder comunicar
        // su indisponibilidad al cliente. Nunca muestra productos desactivados.
        public List<ProductoBE> ObtenerProductosActivos()
        {
            List<ProductoBE> productos = new List<ProductoBE>();

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query = "SELECT " + COLUMNAS_PRODUCTO+ " FROM dbo.Producto WHERE Activo = 1 ORDER BY Categoria, Nombre, CodigoProducto";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                            productos.Add(MapearProducto(reader));
                    }
                }
            }

            return productos;
        }

        // Se consulta directamente la BD para conocer el estado y stock actuales.
        // La disponibilidad se verificara nuevamente al registrar la venta.
        public ProductoBE? BuscarPorCodigo(int codigoProducto)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                string query = "SELECT " + COLUMNAS_PRODUCTO+ " FROM dbo.Producto WHERE CodigoProducto = @CodigoProducto";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapearProducto(reader);
                    }
                }
            }

            return null;
        }
    }
}
