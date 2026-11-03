using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using Servicios;
using System.Data;

namespace DAL
{
    // Consultas historicas: la escritura se realiza exclusivamente con
    // TR_Producto_RegistrarCambio, no desde el formulario de auditoria.
    public class ProductoCambioDAL
    {
        private readonly DAO_AccesoDatos _accesoDatos = new DAO_AccesoDatos();

        // La restauracion activa una version historica; el trigger de SQL Server
        // sincroniza Producto y crea la nueva version del historial.
        // Las DVH/DVV y la bitacora de eventos se guardan en la misma transaccion.
        public void RestaurarVersion(long idCambio, int idUsuario, string usuario)
        {
            var dvDAL = new DigitoVerificadorDAL();
            var eventosDAL = new BitacoraEventoDAL();

            using SqlConnection conexion = _accesoDatos.ObtenerConexion();
            conexion.Open();
            using SqlTransaction tx = conexion.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                int codigo;
                string nombre;
                string categoria;
                decimal precio;
                int stock;
                int minimo;
                bool activo;

                const string leer = @"
                    SELECT p.CodigoProducto, h.Nombre, h.Categoria,
                           h.PrecioUnitario, h.StockDisponible, h.StockMinimo,
                           h.ActivoProducto, h.Act
                    FROM dbo.Producto_C AS h
                    INNER JOIN dbo.Producto AS p WITH (UPDLOCK, HOLDLOCK)
                        ON p.CodigoProducto = h.CodigoProducto
                    WHERE h.IdCambio = @IdCambio";

                using (SqlCommand cmd = new SqlCommand(leer, conexion, tx))
                {
                    cmd.Parameters.Add("@IdCambio", SqlDbType.BigInt).Value = idCambio;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                        throw new InvalidOperationException("La version seleccionada no existe.");

                    codigo = reader.GetInt32(0);
                    nombre = reader.GetString(1);
                    categoria = reader.GetString(2);
                    precio = reader.GetDecimal(3);
                    stock = reader.GetInt32(4);
                    minimo = reader.GetInt32(5);
                    activo = reader.GetBoolean(6);
                    if (reader.GetBoolean(7))
                        throw new InvalidOperationException("La version ya esta vigente.");
                }

                // Evitar reactivar una version que ya coincide con el producto:
                // no generaria una nueva modificacion historica.
                const string comparar = @"
                    SELECT COUNT(*) FROM dbo.Producto
                    WHERE CodigoProducto = @Codigo
                      AND Nombre = @Nombre AND Categoria = @Categoria
                      AND PrecioUnitario = @Precio
                      AND StockDisponible = @Stock
                      AND StockMinimo = @Minimo AND Activo = @Activo";
                using (SqlCommand cmd = new SqlCommand(comparar, conexion, tx))
                {
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombre;
                    cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar, 80).Value = categoria;
                    SqlParameter pPrecio = cmd.Parameters.Add("@Precio", SqlDbType.Decimal);
                    pPrecio.Precision = 12;
                    pPrecio.Scale = 2;
                    pPrecio.Value = precio;
                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = stock;
                    cmd.Parameters.Add("@Minimo", SqlDbType.Int).Value = minimo;
                    cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = activo;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                        throw new InvalidOperationException(
                            "Esta version ya coincide con los datos actuales.");
                }

                // Primero se desactiva la vigente para respetar el indice unico.
                // Esta modificacion NO activa el trigger de restauracion.
                const string desactivar = @"
                    UPDATE dbo.Producto_C SET Act = 0
                    WHERE CodigoProducto = @Codigo AND Act = 1";
                using (SqlCommand cmd = new SqlCommand(desactivar, conexion, tx))
                {
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException(
                            "No se encontro una version vigente del producto.");
                }

                // Al marcar Act=1 el trigger de Producto_C actualiza Producto.
                // El trigger de Producto desactiva la version seleccionada
                // y crea una instantanea NUEVA con Act=1.
                const string activar = @"
                    UPDATE dbo.Producto_C SET Act = 1
                    WHERE IdCambio = @IdCambio AND CodigoProducto = @Codigo
                      AND Act = 0";
                using (SqlCommand cmd = new SqlCommand(activar, conexion, tx))
                {
                    cmd.Parameters.Add("@IdCambio", SqlDbType.BigInt).Value = idCambio;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException(
                            "No se pudo activar la version seleccionada.");
                }

                const string verificar = @"
                    SELECT COUNT(*) FROM dbo.Producto_C
                    WHERE CodigoProducto = @Codigo AND Act = 1";
                using (SqlCommand cmd = new SqlCommand(verificar, conexion, tx))
                {
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
                        throw new InvalidOperationException(
                            "No quedo una unica version vigente del producto.");
                }

                dvDAL.RecalcularDVEnTransaccion("Producto", conexion, tx);

                eventosDAL.Registrar(new BitacoraEvento
                {
                    IdUsuario = idUsuario,
                    Usuario = usuario,
                    FechaHora = DateTime.Now,
                    Modulo = "Buffet",
                    Accion = "Restaurar version producto",
                    Criticidad = "Media",
                    Resultado = "Exitoso",
                    Descripcion = $"Producto {codigo}: version historica {idCambio} restaurada. "
                                + $"Incluye precio, stock y estado comercial."
                }, conexion, tx);

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

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
