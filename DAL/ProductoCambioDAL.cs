using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    // Consultas historicas: la escritura se realiza exclusivamente con
    // TR_Producto_RegistrarCambio, no desde el formulario de auditoria.
    public class ProductoCambioDAL
    {
        private readonly DAO_AccesoDatos _accesoDatos = new DAO_AccesoDatos();

        public List<ProductoCambioBE> Consultar(
            int? codigoProducto, string nombre,
            DateTime? fechaDesde, DateTime? fechaHasta,
            bool soloVigentes)
        {
            const string sql = @"
                SELECT TOP (500)
                       IdCambio, CodigoProducto, FechaHora, Nombre, Categoria,
                       PrecioUnitario, StockDisponible, StockMinimo,
                       ActivoProducto, Act, TipoCambio
                FROM dbo.Producto_C
                WHERE (@Codigo IS NULL OR CodigoProducto = @Codigo)
                  AND (@Nombre = N'' OR Nombre LIKE N'%' + @Nombre + N'%')
                  AND (@Desde IS NULL OR FechaHora >= @Desde)
                  AND (@HastaExclusiva IS NULL OR FechaHora < @HastaExclusiva)
                  AND (@SoloVigentes = 0 OR Act = 1)
                ORDER BY FechaHora DESC, IdCambio DESC";

            using SqlConnection conexion = _accesoDatos.ObtenerConexion();
            using SqlCommand cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value =
                codigoProducto.HasValue ? codigoProducto.Value : DBNull.Value;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value =
                nombre ?? string.Empty;
            cmd.Parameters.Add("@Desde", SqlDbType.DateTime2).Value =
                fechaDesde.HasValue ? fechaDesde.Value.Date : DBNull.Value;
            cmd.Parameters.Add("@HastaExclusiva", SqlDbType.DateTime2).Value =
                fechaHasta.HasValue ? fechaHasta.Value.Date.AddDays(1) : DBNull.Value;
            cmd.Parameters.Add("@SoloVigentes", SqlDbType.Bit).Value = soloVigentes;

            List<ProductoCambioBE> cambios = new List<ProductoCambioBE>();
            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cambios.Add(new ProductoCambioBE
                {
                    IdCambio = reader.GetInt64(reader.GetOrdinal("IdCambio")),
                    CodigoProducto = reader.GetInt32(reader.GetOrdinal("CodigoProducto")),
                    FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Categoria = reader.GetString(reader.GetOrdinal("Categoria")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    StockDisponible = reader.GetInt32(reader.GetOrdinal("StockDisponible")),
                    StockMinimo = reader.GetInt32(reader.GetOrdinal("StockMinimo")),
                    ActivoProducto = reader.GetBoolean(reader.GetOrdinal("ActivoProducto")),
                    Act = reader.GetBoolean(reader.GetOrdinal("Act")),
                    TipoCambio = reader.GetString(reader.GetOrdinal("TipoCambio"))
                });
            }
            return cambios;
        }
    }
}
