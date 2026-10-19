using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using Servicios;
using System.Data;

namespace DAL
{
    public class ProductoDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;
        private readonly DigitoVerificadorDAL _dvDAL;
        private readonly BitacoraEventoDAL _bitacoraDAL;

        private const string COLUMNAS_PRODUCTO ="CodigoProducto, Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo";

        public ProductoDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
            _dvDAL = new DigitoVerificadorDAL();
            _bitacoraDAL = new BitacoraEventoDAL();
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

        // Gestion: incluye activos e inactivos, para poder reactivarlos.
        public List<ProductoBE> ObtenerTodos()
        {
            List<ProductoBE> productos = new List<ProductoBE>();
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();
            const string query = "SELECT " + COLUMNAS_PRODUCTO
                + " FROM dbo.Producto ORDER BY Categoria, Nombre, CodigoProducto";
            using SqlCommand cmd = new SqlCommand(query, conexion);
            conexion.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                productos.Add(MapearProducto(reader));
            return productos;
        }

        public void CrearProducto(string nombre, string categoria, decimal precio,
            int stockInicial, int stockMinimo, int idUsuario, string usuario)
        {
            EjecutarCambio("Crear producto", idUsuario, usuario, (conexion, tx) =>
            {
                ComprobarDuplicado(conexion, tx, nombre, categoria, 0);
                const string sql = @"
                    INSERT INTO dbo.Producto
                        (Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo)
                    OUTPUT INSERTED.CodigoProducto
                    VALUES (@Nombre, @Categoria, @Precio, @Stock, @Minimo, 1)";

                int codigo;
                using (SqlCommand cmd = new SqlCommand(sql, conexion, tx))
                {
                    AgregarNombreCategoria(cmd, nombre, categoria);
                    AgregarPrecio(cmd, precio);
                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = stockInicial;
                    cmd.Parameters.Add("@Minimo", SqlDbType.Int).Value = stockMinimo;
                    codigo = Convert.ToInt32(cmd.ExecuteScalar());
                }

                return $"Producto {codigo} creado: {nombre} ({categoria}). "
                    + $"Precio: {precio:N2}. Stock inicial: {stockInicial}. "
                    + $"Stock minimo: {stockMinimo}.";
            });
        }

        public void ModificarProducto(int codigo, string nombre, string categoria,
            decimal precio, int stockMinimo, int idUsuario, string usuario)
        {
            EjecutarCambio("Modificar producto", idUsuario, usuario, (conexion, tx) =>
            {
                ProductoBE anterior = ObtenerProductoBloqueado(conexion, tx, codigo);
                ComprobarDuplicado(conexion, tx, nombre, categoria, codigo);

                const string sql = @"
                    UPDATE dbo.Producto SET Nombre = @Nombre,
                        Categoria = @Categoria, PrecioUnitario = @Precio,
                        StockMinimo = @Minimo
                    WHERE CodigoProducto = @Codigo";
                using (SqlCommand cmd = new SqlCommand(sql, conexion, tx))
                {
                    AgregarNombreCategoria(cmd, nombre, categoria);
                    AgregarPrecio(cmd, precio);
                    cmd.Parameters.Add("@Minimo", SqlDbType.Int).Value = stockMinimo;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No se pudo modificar el producto.");
                }

                return $"Producto {codigo} modificado: "
                    + $"{anterior.Nombre}/{anterior.Categoria}, precio {anterior.PrecioUnitario:N2}, "
                    + $"minimo {anterior.StockMinimo} -> "
                    + $"{nombre}/{categoria}, precio {precio:N2}, minimo {stockMinimo}.";
            });
        }

        public void CambiarEstado(int codigo, bool activar, int idUsuario, string usuario)
        {
            EjecutarCambio(activar ? "Reactivar producto" : "Desactivar producto",
                idUsuario, usuario, (conexion, tx) =>
                {
                    ProductoBE producto = ObtenerProductoBloqueado(conexion, tx, codigo);
                    if (producto.Activo == activar)
                        throw new Exception("El producto ya tiene el estado seleccionado.");

                    const string sql = @"
                        UPDATE dbo.Producto SET Activo = @Activo
                        WHERE CodigoProducto = @Codigo";
                    using SqlCommand cmd = new SqlCommand(sql, conexion, tx);
                    cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = activar;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No se pudo actualizar el estado.");

                    return $"Producto {codigo} ({producto.Nombre}) "
                        + (activar ? "reactivado." : "desactivado.");
                });
        }

        public void ReponerStock(int codigo, int cantidad, int idUsuario, string usuario)
        {
            EjecutarCambio("Reponer stock", idUsuario, usuario, (conexion, tx) =>
            {
                ProductoBE producto = ObtenerProductoBloqueado(conexion, tx, codigo);
                if (!producto.Activo)
                    throw new Exception("No se puede reponer un producto desactivado.");

                // La suma se calcula en SQL con bloqueo y control de overflow.
                // Nunca se sobrescribe el stock que haya dejado otra venta.
                const string sql = @"
                    UPDATE dbo.Producto
                    SET StockDisponible = StockDisponible + @Cantidad
                    WHERE CodigoProducto = @Codigo
                      AND Activo = 1
                      AND StockDisponible <= 2147483647 - @Cantidad";
                using SqlCommand cmd = new SqlCommand(sql, conexion, tx);
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
                if (cmd.ExecuteNonQuery() != 1)
                    throw new Exception("No se pudo reponer el stock. Revise la cantidad.");

                return $"Producto {codigo} ({producto.Nombre}). "
                    + $"Stock: {producto.StockDisponible} + {cantidad} = "
                    + $"{(long)producto.StockDisponible + cantidad}.";
            });
        }

        // Mantiene Producto, sus DVH/DVV y la bitacora en una unica transaccion.
        private void EjecutarCambio(string accion, int idUsuario, string usuario,
            Func<SqlConnection, SqlTransaction, string> ejecutar)
        {
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();
            conexion.Open();
            using SqlTransaction tx = conexion.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                string descripcion = ejecutar(conexion, tx);
                _dvDAL.RecalcularDVEnTransaccion("Producto", conexion, tx);
                _bitacoraDAL.Registrar(new BitacoraEvento
                {
                    IdUsuario = idUsuario,
                    Usuario = usuario,
                    FechaHora = DateTime.Now,
                    Modulo = "Buffet",
                    Accion = accion,
                    Criticidad = "Media",
                    Resultado = "Exitoso",
                    Descripcion = descripcion
                }, conexion, tx);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private static ProductoBE ObtenerProductoBloqueado(
            SqlConnection conexion, SqlTransaction tx, int codigo)
        {
            const string sql = @"
                SELECT CodigoProducto, Nombre, Categoria, PrecioUnitario,
                       StockDisponible, StockMinimo, Activo
                FROM dbo.Producto WITH (UPDLOCK, HOLDLOCK)
                WHERE CodigoProducto = @Codigo";
            using SqlCommand cmd = new SqlCommand(sql, conexion, tx);
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigo;
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new Exception("El producto no existe.");
            return MapearProducto(reader);
        }

        private static void ComprobarDuplicado(SqlConnection conexion,
            SqlTransaction tx, string nombre, string categoria, int codigoExcluir)
        {
            // Serializable evita insertar simultaneamente el mismo nombre/categoria.
            const string sql = @"
                SELECT COUNT(*) FROM dbo.Producto WITH (UPDLOCK, HOLDLOCK)
                WHERE Nombre = @Nombre AND Categoria = @Categoria
                  AND CodigoProducto <> @Codigo";
            using SqlCommand cmd = new SqlCommand(sql, conexion, tx);
            AgregarNombreCategoria(cmd, nombre, categoria);
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = codigoExcluir;
            if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                throw new Exception("Ya existe un producto con ese nombre y categoria.");
        }

        private static void AgregarNombreCategoria(SqlCommand cmd,
            string nombre, string categoria)
        {
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombre;
            cmd.Parameters.Add("@Categoria", SqlDbType.NVarChar, 80).Value = categoria;
        }

        private static void AgregarPrecio(SqlCommand cmd, decimal precio)
        {
            SqlParameter param = cmd.Parameters.Add("@Precio", SqlDbType.Decimal);
            param.Precision = 12;
            param.Scale = 2;
            param.Value = precio;
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
