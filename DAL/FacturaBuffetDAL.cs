using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BE;
using Microsoft.Data.SqlClient;
using Servicios;

namespace DAL
{
    // Factura, detalles, stock, dígitos verificadores y bitácora: una transacción.
    public class FacturaBuffetDAL
    {
        private readonly DAO_AccesoDatos _acceso = new DAO_AccesoDatos();


        /// Registra una factura de buffet con sus detalles y actualiza el stock de productos.
        public FacturaBuffetBE RegistrarFactura(FacturaBuffetBE factura, string usuario, PagoBE pago,
            Action<BitacoraEvento, SqlConnection, SqlTransaction> registrarServicios)
        {
            if (registrarServicios == null) throw new ArgumentNullException(nameof(registrarServicios));
            if (factura == null || pago == null || factura.Detalles == null ||factura.Detalles.Count == 0 || string.IsNullOrWhiteSpace(factura.DNI) ||factura.IdVendedor <= 0 || factura.ImporteTotal <= 0 || pago.Estado != "Aprobado" ||
                pago.Importe != factura.ImporteTotal || string.IsNullOrWhiteSpace(pago.Banco) ||string.IsNullOrWhiteSpace(pago.CodigoAutorizacion) ||string.IsNullOrWhiteSpace(pago.Ultimos4Tarjeta) || pago.Ultimos4Tarjeta.Length != 4)
                throw new Exception("Factura o pago aprobado inválidos.");

            List<DetalleFacturaBE> detalles = factura.Detalles.Select(d => new DetalleFacturaBE
            {
                CodigoProducto = d.CodigoProducto,
                NombreProducto = d.NombreProducto,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario
            }).OrderBy(d => d.CodigoProducto).ToList();

            if (detalles.Any(d => d.CodigoProducto <= 0 || d.Cantidad <= 0 ||d.PrecioUnitario <= 0 || d.PrecioUnitario > 9999999999.99m || decimal.Round(d.PrecioUnitario, 2) != d.PrecioUnitario) ||detalles.GroupBy(d => d.CodigoProducto).Any(g => g.Count() > 1))
                throw new Exception("Detalles inválidos o productos duplicados.");

            decimal total = 0m;
            foreach (DetalleFacturaBE d in detalles)
                total = checked(total + d.Subtotal);
            if (total != factura.ImporteTotal || total > 9999999999.99m)
                throw new Exception("El total no coincide con los detalles.");

            using SqlConnection conexion = _acceso.ObtenerConexion();
            conexion.Open();
            
            using SqlTransaction tx = conexion.BeginTransaction(IsolationLevel.Serializable);
            
            try
            {
                FacturaBuffetBE existente = BuscarPorAutorizacion(conexion, tx, pago.CodigoAutorizacion);
                if (existente != null)
                {
                    if (existente.DNI != factura.DNI || existente.IdVendedor != factura.IdVendedor ||existente.ImporteTotal != total ||
                        existente.Banco != pago.Banco || existente.Ultimos4Tarjeta != pago.Ultimos4Tarjeta || existente.Detalles.Count != detalles.Count ||
                        existente.Detalles.Any(e => !detalles.Any(d =>e.CodigoProducto == d.CodigoProducto && e.Cantidad == d.Cantidad &&e.PrecioUnitario == d.PrecioUnitario)))
                        throw new Exception("La autorización ya se utilizó para una factura diferente.");
                    tx.Commit();
                    return existente;
                }

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.Cliente WITH (HOLDLOCK) WHERE DNI = @DNI", conexion, tx))
                {
                    cmd.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = factura.DNI;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
                        throw new Exception("El cliente no existe.");
                }

                const string descontar = @"UPDATE dbo.Producto
                    SET StockDisponible = StockDisponible - @Cantidad
                    WHERE CodigoProducto = @CodigoProducto AND Activo = 1
                    AND StockDisponible >= @Cantidad";
                foreach (DetalleFacturaBE detalle in detalles)
                {
                    using SqlCommand cmd = new SqlCommand(descontar, conexion, tx);
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                    cmd.Parameters.Add("@CodigoProducto", SqlDbType.Int).Value = detalle.CodigoProducto;
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new Exception("Stock insuficiente. La transacción se revirtió.");
                }

                DateTime fecha = DateTime.Now;
                string numero = "FB-" + fecha.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N")[..10].ToUpperInvariant();
                int idFactura;
                const string alta = @"INSERT INTO dbo.FacturaBuffet
                    (NumeroFactura,DNI,IdVendedor,FechaHoraEmision,ImporteTotal,
                     Estado,Banco,Ultimos4Tarjeta,FechaHoraPago,CodigoAutorizacion)
                    OUTPUT INSERTED.IdFactura
                    VALUES (@Numero,@DNI,@IdVendedor,@Fecha,@Total,
                            N'Pagada',@Banco,@Ultimos4,@FechaPago,@Codigo)";
                using (SqlCommand cmd = new SqlCommand(alta, conexion, tx))
                {
                    cmd.Parameters.Add("@Numero", SqlDbType.NVarChar, 30).Value = numero;
                    cmd.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = factura.DNI;
                    cmd.Parameters.Add("@IdVendedor", SqlDbType.Int).Value = factura.IdVendedor;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = fecha;
                    Decimal(cmd, "@Total", total);
                    cmd.Parameters.Add("@Banco", SqlDbType.NVarChar, 80).Value = pago.Banco;
                    cmd.Parameters.Add("@Ultimos4", SqlDbType.Char, 4).Value = pago.Ultimos4Tarjeta;
                    cmd.Parameters.Add("@FechaPago", SqlDbType.DateTime2).Value = pago.FechaHora;
                    cmd.Parameters.Add("@Codigo", SqlDbType.NVarChar, 50).Value = pago.CodigoAutorizacion;
                    idFactura = Convert.ToInt32(cmd.ExecuteScalar());
                }

                const string altaDetalle = @"INSERT INTO dbo.DetalleFactura
                    (IdFactura,CodigoProducto,Cantidad,PrecioUnitario)
                    OUTPUT INSERTED.IdDetalleFactura
                    VALUES (@Factura,@Producto,@Cantidad,@Precio)";
                
                foreach (DetalleFacturaBE detalle in detalles)
                {
                    using SqlCommand cmd = new SqlCommand(altaDetalle, conexion, tx);
                    cmd.Parameters.Add("@Factura", SqlDbType.Int).Value = idFactura;
                    cmd.Parameters.Add("@Producto", SqlDbType.Int).Value = detalle.CodigoProducto;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                    Decimal(cmd, "@Precio", detalle.PrecioUnitario);
                    detalle.IdDetalleFactura = Convert.ToInt32(cmd.ExecuteScalar());
                    detalle.IdFactura = idFactura;
                }

                // La capa BLL coordina DV y bitácora usando esta misma transacción.
                registrarServicios(new BitacoraEvento
                {
                    IdUsuario = factura.IdVendedor,
                    Usuario = usuario,
                    FechaHora = fecha,
                    Modulo = "Buffet",
                    Accion = "Registrar factura",
                    Criticidad = "Alta",
                    Resultado = "Exitoso",
                    Descripcion = $"Factura {numero} pagada. DNI {factura.DNI}. " +
                                  $"Productos {detalles.Count}. Total {total:N2}. " +
                                  $"Autorización {pago.CodigoAutorizacion}."
                }, conexion, tx);

                tx.Commit();
                return new FacturaBuffetBE
                {
                    IdFactura = idFactura,
                    NumeroFactura = numero,
                    DNI = factura.DNI,
                    IdVendedor = factura.IdVendedor,
                    FechaHoraEmision = fecha,
                    ImporteTotal = total,
                    Estado = "Pagada",
                    Banco = pago.Banco,
                    Ultimos4Tarjeta = pago.Ultimos4Tarjeta,
                    FechaHoraPago = pago.FechaHora,
                    CodigoAutorizacion = pago.CodigoAutorizacion,
                    Detalles = detalles
                };
            }
            catch
            {
                try { tx.Rollback(); } catch { }
                throw;
            }
        }

        // Recuperación idempotente por autorización; nunca descuenta stock dos veces.
        private static FacturaBuffetBE BuscarPorAutorizacion(
            SqlConnection conexion, SqlTransaction tx, string autorizacion)
        {
            const string sql = @"SELECT IdFactura,NumeroFactura,DNI,IdVendedor,
                FechaHoraEmision,ImporteTotal,Estado,Banco,Ultimos4Tarjeta,
                FechaHoraPago,CodigoAutorizacion
                FROM dbo.FacturaBuffet WITH (UPDLOCK,HOLDLOCK)
                WHERE CodigoAutorizacion = @Codigo";
            FacturaBuffetBE factura = null;
            using (SqlCommand cmd = new SqlCommand(sql, conexion, tx))
            {
                cmd.Parameters.Add("@Codigo", SqlDbType.NVarChar, 50).Value = autorizacion;
                using SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                    factura = new FacturaBuffetBE
                    {
                        IdFactura = Convert.ToInt32(rd["IdFactura"]),
                        NumeroFactura = rd["NumeroFactura"].ToString() ?? "",
                        DNI = rd["DNI"].ToString() ?? "",
                        IdVendedor = Convert.ToInt32(rd["IdVendedor"]),
                        FechaHoraEmision = Convert.ToDateTime(rd["FechaHoraEmision"]),
                        ImporteTotal = Convert.ToDecimal(rd["ImporteTotal"]),
                        Estado = rd["Estado"].ToString() ?? "",
                        Banco = rd["Banco"].ToString() ?? "",
                        Ultimos4Tarjeta = rd["Ultimos4Tarjeta"].ToString() ?? "",
                        FechaHoraPago = Convert.ToDateTime(rd["FechaHoraPago"]),
                        CodigoAutorizacion = rd["CodigoAutorizacion"].ToString() ?? ""
                    };
            }
            if (factura == null) return null;
            const string detalleSql = @"SELECT D.IdDetalleFactura,D.IdFactura,
                D.CodigoProducto,D.Cantidad,D.PrecioUnitario,P.Nombre
                FROM dbo.DetalleFactura D
                JOIN dbo.Producto P ON P.CodigoProducto=D.CodigoProducto
                WHERE D.IdFactura=@IdFactura ORDER BY D.IdDetalleFactura";
            using (SqlCommand cmd = new SqlCommand(detalleSql, conexion, tx))
            {
                cmd.Parameters.Add("@IdFactura", SqlDbType.Int).Value = factura.IdFactura;
                using SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                    factura.Detalles.Add(new DetalleFacturaBE
                    {
                        IdDetalleFactura = Convert.ToInt32(rd["IdDetalleFactura"]),
                        IdFactura = Convert.ToInt32(rd["IdFactura"]),
                        CodigoProducto = Convert.ToInt32(rd["CodigoProducto"]),
                        NombreProducto = rd["Nombre"].ToString() ?? "",
                        Cantidad = Convert.ToInt32(rd["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(rd["PrecioUnitario"])
                    });
            }
            return factura;
        }

        private static void Decimal(SqlCommand cmd, string nombre, decimal valor)
        {
            SqlParameter p = cmd.Parameters.Add(nombre, SqlDbType.Decimal);
            p.Precision = 12;
            p.Scale = 2;
            p.Value = valor;
        }
    }
}
