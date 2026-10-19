using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using Servicios;
using System.Data;

namespace DAL
{
    public class CarritoDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;
        private readonly DigitoVerificadorDAL _digitoVerificadorDAL;
        private readonly BitacoraEventoDAL _bitacoraEventoDAL;

        public CarritoDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
            _digitoVerificadorDAL = new DigitoVerificadorDAL();
            _bitacoraEventoDAL = new BitacoraEventoDAL();
        }

        // Crea el encabezado del carrito sin DNI: el cliente se selecciona en CUN10.
        public CarritoBE CrearCarrito(int idVendedor)
        {
            int idCarrito;

            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    const string sql = @"
                        INSERT INTO dbo.Carrito (IdVendedor, ImporteTotal, Estado)
                        OUTPUT INSERTED.IdCarrito
                        VALUES (@IdVendedor, 0, N'EnPreparacion')";

                    using (SqlCommand comando = new SqlCommand(sql, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                        idCarrito = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("Carrito", conexion, transaccion);
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }

            return ObtenerCarrito(idCarrito, idVendedor);
        }

        // Solo se puede consultar el carrito creado por el usuario de la sesion.
        public CarritoBE ObtenerCarrito(int idCarrito, int idVendedor)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                const string sqlCarrito = @"
                    SELECT IdCarrito, DNI, IdVendedor, FechaHoraCreacion, ImporteTotal, Estado
                    FROM dbo.Carrito
                    WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor";

                CarritoBE carrito;
                using (SqlCommand comando = new SqlCommand(sqlCarrito, conexion))
                {
                    comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                    comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new Exception("No se encontro un carrito del vendedor actual.");

                        carrito = new CarritoBE
                        {
                            IdCarrito = Convert.ToInt32(reader["IdCarrito"]),
                            DNI = reader["DNI"] == DBNull.Value ? null : reader["DNI"].ToString(),
                            IdVendedor = Convert.ToInt32(reader["IdVendedor"]),
                            FechaHoraCreacion = Convert.ToDateTime(reader["FechaHoraCreacion"]),
                            ImporteTotal = Convert.ToDecimal(reader["ImporteTotal"]),
                            Estado = reader["Estado"].ToString() ?? string.Empty
                        };
                    }
                }

                const string sqlDetalles = @"
                    SELECT IdDetalleCarrito, IdCarrito, CodigoProducto, NombreProducto,
                           Cantidad, PrecioUnitario
                    FROM dbo.DetalleCarrito
                    WHERE IdCarrito = @IdCarrito
                    ORDER BY IdDetalleCarrito";

                using (SqlCommand comando = new SqlCommand(sqlDetalles, conexion))
                {
                    comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carrito.Detalles.Add(new DetalleCarritoBE
                            {
                                IdDetalleCarrito = Convert.ToInt32(reader["IdDetalleCarrito"]),
                                IdCarrito = Convert.ToInt32(reader["IdCarrito"]),
                                CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                                NombreProducto = reader["NombreProducto"].ToString() ?? string.Empty,
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                            });
                        }
                    }
                }

                return carrito;
            }
        }

        // Si sumar=true, acumula unidades. Si sumar=false, reemplaza la cantidad.
        // En ambos casos se verifica el stock real dentro de una transaccion.
        public CarritoBE GuardarProducto(int idCarrito, int idVendedor, int codigoProducto, int cantidad, bool sumar)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    ValidarCarritoEditable(conexion, transaccion, idCarrito, idVendedor);

                    string nombreProducto;
                    decimal precioActual;
                    int stockDisponible;
                    bool activo;

                    const string sqlProducto = @"
                        SELECT Nombre, PrecioUnitario, StockDisponible, Activo
                        FROM dbo.Producto WITH (UPDLOCK, HOLDLOCK)
                        WHERE CodigoProducto = @CodigoProducto";

                    using (SqlCommand comando = new SqlCommand(sqlProducto, conexion, transaccion))
                    {
                        comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;
                        using SqlDataReader reader = comando.ExecuteReader();

                        if (!reader.Read())
                            throw new Exception("El producto no existe.");

                        nombreProducto = reader["Nombre"].ToString() ?? string.Empty;
                        precioActual = Convert.ToDecimal(reader["PrecioUnitario"]);
                        stockDisponible = Convert.ToInt32(reader["StockDisponible"]);
                        activo = Convert.ToBoolean(reader["Activo"]);
                    }

                    if (!activo || precioActual <= 0)
                        throw new Exception("El producto no esta disponible para la venta.");

                    int cantidadAnterior = 0;
                    bool existeDetalle = false;

                    const string sqlActual = @"
                        SELECT Cantidad FROM dbo.DetalleCarrito WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCarrito = @IdCarrito AND CodigoProducto = @CodigoProducto";

                    using (SqlCommand comando = new SqlCommand(sqlActual, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;
                        object? valor = comando.ExecuteScalar();
                        if (valor != null && valor != DBNull.Value)
                        {
                            existeDetalle = true;
                            cantidadAnterior = Convert.ToInt32(valor);
                        }
                    }

                    if (!sumar && !existeDetalle)
                        throw new Exception("El producto no se encuentra en el carrito.");

                    int cantidadFinal = sumar ? checked(cantidadAnterior + cantidad) : cantidad;

                    if (cantidadFinal <= 0 || cantidadFinal > stockDisponible)
                        throw new Exception("La cantidad solicitada supera el stock disponible o no es valida.");

                    if (existeDetalle)
                    {
                        const string sqlUpdate = @"
                            UPDATE dbo.DetalleCarrito
                            SET Cantidad = @Cantidad
                            WHERE IdCarrito = @IdCarrito AND CodigoProducto = @CodigoProducto";

                        using SqlCommand comando = new SqlCommand(sqlUpdate, conexion, transaccion);
                        comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidadFinal;
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;
                        comando.ExecuteNonQuery();
                    }
                    else
                    {
                        const string sqlInsert = @"
                            INSERT INTO dbo.DetalleCarrito
                                (IdCarrito, CodigoProducto, NombreProducto, Cantidad, PrecioUnitario)
                            VALUES
                                (@IdCarrito, @CodigoProducto, @Nombre, @Cantidad, @Precio)";

                        using SqlCommand comando = new SqlCommand(sqlInsert, conexion, transaccion);
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;
                        comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombreProducto;
                        comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidadFinal;
                        AgregarImporte(comando, "@Precio", precioActual);
                        comando.ExecuteNonQuery();
                    }

                    RecalcularImporteCarrito(conexion, transaccion, idCarrito);
                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("DetalleCarrito", conexion, transaccion);
                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("Carrito", conexion, transaccion);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }

            return ObtenerCarrito(idCarrito, idVendedor);
        }

        public CarritoBE QuitarProducto(int idCarrito, int idVendedor, int codigoProducto)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    ValidarCarritoEditable(conexion, transaccion, idCarrito, idVendedor);

                    const string sql = @"
                        DELETE FROM dbo.DetalleCarrito
                        WHERE IdCarrito = @IdCarrito AND CodigoProducto = @CodigoProducto";

                    using (SqlCommand comando = new SqlCommand(sql, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = codigoProducto;

                        if (comando.ExecuteNonQuery() != 1)
                            throw new Exception("El producto no se encuentra en el carrito.");
                    }

                    RecalcularImporteCarrito(conexion, transaccion, idCarrito);
                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("DetalleCarrito", conexion, transaccion);
                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("Carrito", conexion, transaccion);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }

            return ObtenerCarrito(idCarrito, idVendedor);
        }

        // CUN10: el carrito pasa a Listo solo cuando existe el DNI y tiene productos.
        public CarritoBE AsociarCliente(int idCarrito, int idVendedor, string dni)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    ValidarCarritoEditable(conexion, transaccion, idCarrito, idVendedor);

                    const string sqlExiste = "SELECT COUNT(1) FROM dbo.Cliente WHERE DNI = @DNI";
                    using (SqlCommand comando = new SqlCommand(sqlExiste, conexion, transaccion))
                    {
                        comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;
                        if (Convert.ToInt32(comando.ExecuteScalar()) != 1)
                            throw new Exception("Debe seleccionar un cliente registrado.");
                    }

                    const string sqlCantidad = @"
                        SELECT COUNT(*) FROM dbo.DetalleCarrito WHERE IdCarrito = @IdCarrito";

                    using (SqlCommand comando = new SqlCommand(sqlCantidad, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        if (Convert.ToInt32(comando.ExecuteScalar()) == 0)
                            throw new Exception("No puede asociar un cliente a un carrito vacio.");
                    }

                    const string sqlActualizar = @"
                        UPDATE dbo.Carrito SET DNI = @DNI, Estado = N'Listo'
                        WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor";

                    using (SqlCommand comando = new SqlCommand(sqlActualizar, conexion, transaccion))
                    {
                        comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                        comando.ExecuteNonQuery();
                    }

                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("Carrito", conexion, transaccion);
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }

            return ObtenerCarrito(idCarrito, idVendedor);
        }

        // Se descarta el carrito que aun no origino una venta.
        // La eliminacion fisica y la auditoria se confirman juntas:
        // si falla cualquier paso, se revierte toda la transaccion.
        public void CancelarCarrito(int idCarrito, int idVendedor, string nombreUsuario)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();
                using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    string estadoAnterior;
                    decimal importeAnterior;

                    // Bloquear el carrito y verificar que es del vendedor actual.
                    const string sqlCarrito = @"
                        SELECT Estado, ImporteTotal
                        FROM dbo.Carrito WITH (UPDLOCK, HOLDLOCK)
                        WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor";

                    using (SqlCommand comando = new SqlCommand(sqlCarrito, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;

                        using SqlDataReader reader = comando.ExecuteReader();
                        if (!reader.Read())
                            throw new Exception("El carrito no existe o no pertenece al vendedor actual.");

                        estadoAnterior = reader["Estado"].ToString() ?? string.Empty;
                        importeAnterior = Convert.ToDecimal(reader["ImporteTotal"]);
                    }

                    // Un carrito ya vendido no se borra porque es soporte de
                    // Venta / FacturaVenta. En ese caso corresponde una anulacion.
                    if (estadoAnterior != "EnPreparacion" && estadoAnterior != "Listo")
                        throw new Exception("No se puede descartar un carrito en ese estado.");

                    int cantidadRenglones;
                    const string sqlCantidad = @"
                        SELECT COUNT(*) FROM dbo.DetalleCarrito
                        WHERE IdCarrito = @IdCarrito";

                    using (SqlCommand comando = new SqlCommand(sqlCantidad, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        cantidadRenglones = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    const string sqlEliminarDetalles = @"
                        DELETE FROM dbo.DetalleCarrito WHERE IdCarrito = @IdCarrito";

                    using (SqlCommand comando = new SqlCommand(sqlEliminarDetalles, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.ExecuteNonQuery();
                    }

                    const string sqlEliminarCarrito = @"
                        DELETE FROM dbo.Carrito
                        WHERE IdCarrito = @IdCarrito
                          AND IdVendedor = @IdVendedor
                          AND Estado IN (N'EnPreparacion', N'Listo')";

                    using (SqlCommand comando = new SqlCommand(sqlEliminarCarrito, conexion, transaccion))
                    {
                        comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                        comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                        if (comando.ExecuteNonQuery() != 1)
                            throw new Exception("No se pudo descartar el carrito.");
                    }

                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("DetalleCarrito", conexion, transaccion);
                    _digitoVerificadorDAL.RecalcularDVEnTransaccion("Carrito", conexion, transaccion);

                    BitacoraEvento evento = new BitacoraEvento
                    {
                        IdUsuario = idVendedor,
                        Usuario = nombreUsuario,
                        FechaHora = DateTime.Now,
                        Modulo = "Buffet",
                        Accion = "Cancelar carrito",
                        Criticidad = "Baja",
                        Resultado = "Exitoso",
                        Descripcion = $"Carrito interno {idCarrito} descartado sin venta ni cobro. "
                            + $"Estado previo: {estadoAnterior}. Renglones: {cantidadRenglones}. "
                            + $"Total previo: {importeAnterior:N2}."
                    };

                    _bitacoraEventoDAL.Registrar(evento, conexion, transaccion);
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private static void ValidarCarritoEditable(SqlConnection conexion, SqlTransaction transaccion,
                                                    int idCarrito, int idVendedor)
        {
            const string sql = @"
                SELECT Estado FROM dbo.Carrito WITH (UPDLOCK, HOLDLOCK)
                WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor";

            using SqlCommand comando = new SqlCommand(sql, conexion, transaccion);
            comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
            comando.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
            string? estado = comando.ExecuteScalar()?.ToString();

            if (estado != "EnPreparacion")
                throw new Exception("El carrito no existe, no pertenece al vendedor o ya fue confirmado.");
        }

        // Siempre se calcula desde DetalleCarrito; no se confia en importes de la GUI.
        private static void RecalcularImporteCarrito(SqlConnection conexion, SqlTransaction transaccion,
                                                     int idCarrito)
        {
            const string sqlTotal = @"
                SELECT ISNULL(SUM(CONVERT(DECIMAL(18, 2), Cantidad) * PrecioUnitario), 0)
                FROM dbo.DetalleCarrito WHERE IdCarrito = @IdCarrito";

            decimal total;
            using (SqlCommand comando = new SqlCommand(sqlTotal, conexion, transaccion))
            {
                comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                total = Convert.ToDecimal(comando.ExecuteScalar());
            }

            const string sqlUpdate = @"
                UPDATE dbo.Carrito SET ImporteTotal = @Importe
                WHERE IdCarrito = @IdCarrito";

            using (SqlCommand comando = new SqlCommand(sqlUpdate, conexion, transaccion))
            {
                AgregarImporte(comando, "@Importe", total);
                comando.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                comando.ExecuteNonQuery();
            }
        }

        private static void AgregarImporte(SqlCommand comando, string nombre, decimal importe)
        {
            SqlParameter parametro = comando.Parameters.Add(nombre, SqlDbType.Decimal);
            parametro.Precision = 12;
            parametro.Scale = 2;
            parametro.Value = importe;
        }
    }
}
