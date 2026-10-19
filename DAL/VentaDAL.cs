using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using Servicios;
using System.Data;

namespace DAL
{
    // CUN13 Registrar Venta y CUN14 Actualizar Stock.
    // Venta, FacturaVenta, stock, estado del carrito, DVH/DVV y bitacora
    // se escriben en la MISMA transaccion SQL Server.
    public class VentaDAL
    {
        private readonly DAO_AccesoDatos _accesoDatos;
        private readonly DigitoVerificadorDAL _dvDAL;
        private readonly BitacoraEventoDAL _bitacoraDAL;

        public VentaDAL()
        {
            _accesoDatos = new DAO_AccesoDatos();
            _dvDAL = new DigitoVerificadorDAL();
            _bitacoraDAL = new BitacoraEventoDAL();
        }

        public VentaBE RegistrarVenta(int idCarrito, int idVendedor, string nombreUsuario, PagoBE pago)
        {
            using SqlConnection conexion = _accesoDatos.ObtenerConexion();
            conexion.Open();
            using SqlTransaction tx = conexion.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                string dni;
                decimal totalCarrito;
                const string queryCarrito = @"
                    SELECT DNI, ImporteTotal, Estado
                    FROM dbo.Carrito WITH (UPDLOCK, HOLDLOCK)
                    WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor";

                using (SqlCommand cmd = new SqlCommand(queryCarrito, conexion, tx))
                {
                    cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                    cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                    using SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                        throw new Exception("El carrito no existe o pertenece a otro vendedor.");

                    string estado = reader["Estado"].ToString() ?? string.Empty;
                    if (estado != "Listo")
                        throw new Exception("El carrito ya no esta listo para registrar una venta.");

                    dni = reader["DNI"] == DBNull.Value ? string.Empty : reader["DNI"].ToString() ?? string.Empty;
                    totalCarrito = Convert.ToDecimal(reader["ImporteTotal"]);
                }

                if (string.IsNullOrWhiteSpace(dni) || totalCarrito <= 0)
                    throw new Exception("El carrito no tiene un cliente o importe valido.");

                // La suma de DetalleCarrito es la fuente de verdad del total.
                // Se conserva el precio historico seleccionado en cada renglon.
                List<(int CodigoProducto, int Cantidad, decimal Precio)> detalles = new();
                const string queryDetalle = @"
                    SELECT CodigoProducto, Cantidad, PrecioUnitario
                    FROM dbo.DetalleCarrito WITH (HOLDLOCK)
                    WHERE IdCarrito = @IdCarrito ORDER BY CodigoProducto";

                using (SqlCommand cmd = new SqlCommand(queryDetalle, conexion, tx))
                {
                    cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        detalles.Add((
                            Convert.ToInt32(reader["CodigoProducto"]),
                            Convert.ToInt32(reader["Cantidad"]),
                            Convert.ToDecimal(reader["PrecioUnitario"])));
                    }
                }

                if (detalles.Count == 0)
                    throw new Exception("No se puede registrar una venta sin productos.");

                decimal totalCalculado = 0;
                foreach (var detalle in detalles)
                {
                    if (detalle.Cantidad <= 0 || detalle.Precio <= 0)
                        throw new Exception("El carrito contiene productos con valores invalidos.");

                    totalCalculado = checked(totalCalculado + detalle.Cantidad * detalle.Precio);
                }

                if (totalCalculado != totalCarrito || pago.Importe != totalCalculado)
                    throw new Exception("El importe del carrito cambio despues del cobro. No se registro la venta.");

                if (pago.Estado != "Aprobado" || string.IsNullOrWhiteSpace(pago.CodigoAutorizacion))
                    throw new Exception("Se necesita un pago aprobado para registrar la venta.");

                // Actualizacion condicional: JAMAS deja stock negativo.
                // Si un producto queda sin stock antes de finalizar, se revierten
                // todos los cambios realizados en esta misma transaccion.
                const string queryStock = @"
                    UPDATE dbo.Producto
                    SET StockDisponible = StockDisponible - @Cantidad
                    WHERE CodigoProducto = @CodigoProducto
                      AND Activo = 1 AND StockDisponible >= @Cantidad";

                foreach (var detalle in detalles)
                {
                    using SqlCommand cmd = new SqlCommand(queryStock, conexion, tx);
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                    cmd.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = detalle.CodigoProducto;

                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No hay stock suficiente de uno de los productos. No se registro la venta.");
                }

                DateTime ahora = DateTime.Now;
                string codigoVenta = "VEN-" + ahora.ToString("yyyyMMdd") + "-"
                    + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
                int idVenta;

                const string queryVenta = @"
                    INSERT INTO dbo.Venta
                    (Codigo, IdCarrito, DNI, IdVendedor, FechaHora,
                     ImporteTotal, Estado, Banco, Ultimos4Tarjeta, ImportePago,
                     FechaHoraPago, EstadoPago, CodigoAutorizacion)
                    OUTPUT INSERTED.IdVenta
                    VALUES
                    (@Codigo, @IdCarrito, @DNI, @IdVendedor, @FechaHora,
                     @ImporteTotal, N'Registrada', @Banco, @Ultimos4Tarjeta, @ImportePago,
                     @FechaHoraPago, @EstadoPago, @CodigoAutorizacion)";

                using (SqlCommand cmd = new SqlCommand(queryVenta, conexion, tx))
                {
                    cmd.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = codigoVenta;
                    cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                    cmd.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = dni;
                    cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                    cmd.Parameters.Add("@FechaHora", SqlDbType.DateTime2).Value = ahora;
                    AgregarDecimal(cmd, "@ImporteTotal", totalCalculado);
                    cmd.Parameters.Add("@Banco", SqlDbType.NVarChar, 80).Value = pago.Banco;
                    cmd.Parameters.Add("@Ultimos4Tarjeta", SqlDbType.Char, 4).Value = pago.Ultimos4Tarjeta;
                    AgregarDecimal(cmd, "@ImportePago", pago.Importe);
                    cmd.Parameters.Add("@FechaHoraPago", SqlDbType.DateTime2).Value = pago.FechaHora;
                    cmd.Parameters.Add("@EstadoPago", SqlDbType.NVarChar, 20).Value = pago.Estado;
                    cmd.Parameters.Add("@CodigoAutorizacion", SqlDbType.NVarChar, 50)
                        .Value = pago.CodigoAutorizacion;
                    idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                }

                int idFactura;
                const string queryFactura = @"
                    INSERT INTO dbo.FacturaVenta (IdVenta, FechaHoraEmision, ImporteTotal, Estado)
                    OUTPUT INSERTED.IdFacturaVenta
                    VALUES (@IdVenta, @Fecha, @Importe, N'Pagada')";

                using (SqlCommand cmd = new SqlCommand(queryFactura, conexion, tx))
                {
                    cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = ahora;
                    AgregarDecimal(cmd, "@Importe", totalCalculado);
                    idFactura = Convert.ToInt32(cmd.ExecuteScalar());
                }

                const string queryActualizarCarrito = @"
                    UPDATE dbo.Carrito SET Estado = N'Vendido'
                    WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor AND Estado = N'Listo'";

                using (SqlCommand cmd = new SqlCommand(queryActualizarCarrito, conexion, tx))
                {
                    cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                    cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("No se pudo finalizar el carrito.");
                }

                // Los DV protegen las filas modificadas y su conjunto completo.
                foreach (string tabla in new[] { "Producto", "Carrito", "Venta", "FacturaVenta" })
                    _dvDAL.RecalcularDVEnTransaccion(tabla, conexion, tx);

                _bitacoraDAL.Registrar(new BitacoraEvento
                {
                    IdUsuario = idVendedor,
                    Usuario = nombreUsuario,
                    FechaHora = ahora,
                    Modulo = "Buffet",
                    Accion = "Registrar venta",
                    Criticidad = "Alta",
                    Resultado = "Exitoso",
                    Descripcion = $"Venta {codigoVenta} registrada. DNI: {dni}. "
                        + $"Productos distintos: {detalles.Count}. Importe: {totalCalculado:N2}. "
                        + $"Autorizacion: {pago.CodigoAutorizacion}."
                }, conexion, tx);

                tx.Commit();

                return new VentaBE
                {
                    IdVenta = idVenta,
                    Codigo = codigoVenta,
                    IdCarrito = idCarrito,
                    DNI = dni,
                    IdVendedor = idVendedor,
                    FechaHora = ahora,
                    ImporteTotal = totalCalculado,
                    Estado = "Registrada",
                    Banco = pago.Banco,
                    Ultimos4Tarjeta = pago.Ultimos4Tarjeta,
                    ImportePago = pago.Importe,
                    FechaHoraPago = pago.FechaHora,
                    EstadoPago = pago.Estado,
                    CodigoAutorizacion = pago.CodigoAutorizacion,
                    Factura = new FacturaVentaBE
                    {
                        IdFacturaVenta = idFactura,
                        IdVenta = idVenta,
                        FechaHoraEmision = ahora,
                        ImporteTotal = totalCalculado,
                        Estado = "Pagada"
                    }
                };
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        // Recupera la venta ante un reintento posterior a un COMMIT cuyo resultado
        // no llego a la GUI. Nunca inserta un cobro duplicado.
        public VentaBE? BuscarPorAutorizacion(int idCarrito, int idVendedor, string codigoAutorizacion)
        {
            using SqlConnection conexion = _accesoDatos.ObtenerConexion();
            conexion.Open();
            const string query = @"
                SELECT V.IdVenta, V.Codigo, V.IdCarrito, V.DNI, V.IdVendedor,
                       V.FechaHora, V.ImporteTotal, V.Estado, V.Banco, V.Ultimos4Tarjeta,
                       V.ImportePago, V.FechaHoraPago, V.EstadoPago, V.CodigoAutorizacion,
                       F.IdFacturaVenta, F.FechaHoraEmision, F.ImporteTotal AS ImporteFactura,
                       F.Estado AS EstadoFactura
                FROM dbo.Venta V
                JOIN dbo.FacturaVenta F ON F.IdVenta = V.IdVenta
                WHERE V.IdCarrito = @IdCarrito AND V.IdVendedor = @IdVendedor
                      AND V.CodigoAutorizacion = @Autorizacion";
            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
            cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
            cmd.Parameters.Add("@Autorizacion", SqlDbType.NVarChar, 50).Value = codigoAutorizacion;

            using SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;

            return new VentaBE
            {
                IdVenta = Convert.ToInt32(rd["IdVenta"]),
                Codigo = rd["Codigo"].ToString() ?? string.Empty,
                IdCarrito = Convert.ToInt32(rd["IdCarrito"]),
                DNI = rd["DNI"].ToString() ?? string.Empty,
                IdVendedor = Convert.ToInt32(rd["IdVendedor"]),
                FechaHora = Convert.ToDateTime(rd["FechaHora"]),
                ImporteTotal = Convert.ToDecimal(rd["ImporteTotal"]),
                Estado = rd["Estado"].ToString() ?? string.Empty,
                Banco = rd["Banco"].ToString() ?? string.Empty,
                Ultimos4Tarjeta = rd["Ultimos4Tarjeta"].ToString() ?? string.Empty,
                ImportePago = Convert.ToDecimal(rd["ImportePago"]),
                FechaHoraPago = Convert.ToDateTime(rd["FechaHoraPago"]),
                EstadoPago = rd["EstadoPago"].ToString() ?? string.Empty,
                CodigoAutorizacion = rd["CodigoAutorizacion"].ToString() ?? string.Empty,
                Factura = new FacturaVentaBE
                {
                    IdFacturaVenta = Convert.ToInt32(rd["IdFacturaVenta"]),
                    IdVenta = Convert.ToInt32(rd["IdVenta"]),
                    FechaHoraEmision = Convert.ToDateTime(rd["FechaHoraEmision"]),
                    ImporteTotal = Convert.ToDecimal(rd["ImporteFactura"]),
                    Estado = rd["EstadoFactura"].ToString() ?? string.Empty
                }
            };
        }

        // Pre-chequeo orientativo antes de solicitar autorizacion bancaria.
        // La validacion definitiva se repite bajo bloqueo en RegistrarVenta().
        public void ValidarDisponibilidadParaCobrar(int idCarrito, int idVendedor)
        {
            using SqlConnection conexion = _accesoDatos.ObtenerConexion();
            conexion.Open();
            const string query = @"
                SELECT COUNT(*)
                FROM dbo.Carrito C
                JOIN dbo.DetalleCarrito D ON D.IdCarrito = C.IdCarrito
                LEFT JOIN dbo.Producto P ON P.CodigoProducto = D.CodigoProducto
                WHERE C.IdCarrito = @IdCarrito AND C.IdVendedor = @IdVendedor
                  AND C.Estado = N'Listo' AND C.DNI IS NOT NULL
                  AND (P.CodigoProducto IS NULL OR P.Activo = 0
                       OR P.StockDisponible < D.Cantidad)";
            const string queryEncabezado = @"
                SELECT COUNT(*)
                FROM dbo.Carrito
                WHERE IdCarrito = @IdCarrito AND IdVendedor = @IdVendedor
                  AND Estado = N'Listo' AND DNI IS NOT NULL AND ImporteTotal > 0";
            const string queryCantidad = @"
                SELECT COUNT(*) FROM dbo.DetalleCarrito WHERE IdCarrito = @IdCarrito";

            int EjecutarCuenta(string texto)
            {
                using SqlCommand cmd = new SqlCommand(texto, conexion);
                cmd.Parameters.Add("@IdCarrito", SqlDbType.Int).Value = idCarrito;
                cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = idVendedor;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

            if (EjecutarCuenta(queryEncabezado) != 1 || EjecutarCuenta(queryCantidad) == 0)
                throw new Exception("El carrito no esta listo para cobrar.");

            if (EjecutarCuenta(query) != 0)
                throw new Exception("La disponibilidad de uno de los productos cambio. Revisar el stock.");
        }

        private static void AgregarDecimal(SqlCommand comando, string nombre, decimal valor)
        {
            SqlParameter param = comando.Parameters.Add(nombre, SqlDbType.Decimal);
            param.Precision = 12;
            param.Scale = 2;
            param.Value = valor;
        }
    }
}
