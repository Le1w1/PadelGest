using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CobroReservaDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        public CobroReservaDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        public FacturaBE CrearFacturaPendiente(FacturaBE factura)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    INSERT INTO Factura
                    (
                        IdCliente,
                        IdCancha,
                        IdTarifa,
                        FechaHoraEmision,
                        FechaReserva,
                        Horario,
                        CantidadPaletas,
                        CantidadPelotas,
                        ImporteTarifa,
                        ImporteEquipamiento,
                        ImporteTotal,
                        Estado
                    )
                    OUTPUT INSERTED.IdFactura
                    VALUES
                    (
                        @IdCliente,
                        @IdCancha,
                        @IdTarifa,
                        @FechaHoraEmision,
                        @FechaReserva,
                        @Horario,
                        @CantidadPaletas,
                        @CantidadPelotas,
                        @ImporteTarifa,
                        @ImporteEquipamiento,
                        @ImporteTotal,
                        N'Pendiente'
                    )";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = factura.IdCliente;
                    comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = factura.IdCancha;
                    comando.Parameters.Add("@IdTarifa", SqlDbType.Int).Value = factura.IdTarifa;
                    comando.Parameters.Add("@FechaHoraEmision", SqlDbType.DateTime2).Value = factura.FechaHoraEmision;
                    comando.Parameters.Add("@FechaReserva", SqlDbType.Date).Value = factura.FechaReserva.Date;
                    comando.Parameters.Add("@Horario", SqlDbType.Time).Value = factura.Horario;
                    comando.Parameters.Add("@CantidadPaletas", SqlDbType.Int).Value = factura.CantidadPaletas;
                    comando.Parameters.Add("@CantidadPelotas", SqlDbType.Int).Value = factura.CantidadPelotas;

                    var pTarifa = comando.Parameters.Add("@ImporteTarifa", SqlDbType.Decimal);
                    pTarifa.Precision = 12;
                    pTarifa.Scale = 2;
                    pTarifa.Value = factura.ImporteTarifa;

                    var pEquipamiento = comando.Parameters.Add("@ImporteEquipamiento", SqlDbType.Decimal);
                    pEquipamiento.Precision = 12;
                    pEquipamiento.Scale = 2;
                    pEquipamiento.Value = factura.ImporteEquipamiento;

                    var pTotal = comando.Parameters.Add("@ImporteTotal", SqlDbType.Decimal);
                    pTotal.Precision = 12;
                    pTotal.Scale = 2;
                    pTotal.Value = factura.ImporteTotal;

                    conexion.Open();

                    factura.IdFactura = Convert.ToInt32(comando.ExecuteScalar());
                    factura.Estado = "Pendiente";
                    return factura;
                }
            }
        }

        public void CancelarFactura(int idFactura)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                const string query = @"
                    UPDATE Factura
                    SET Estado = N'Cancelada'
                    WHERE IdFactura = @IdFactura
                      AND Estado = N'Pendiente'";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura;

                    conexion.Open();

                    if (comando.ExecuteNonQuery() != 1)
                    {
                        throw new Exception(
                            "No se pudo cancelar la factura pendiente.");
                    }
                }
            }
        }

        /// <summary>
        /// Registra el pago aprobado y marca la factura como Pagada dentro de
        /// una misma transacción local.
        /// </summary>
        public PagoBE RegistrarPagoAprobado(PagoBE pago)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        const string queryPago = @"
                            INSERT INTO Pago
                            (
                                IdFactura,
                                Banco,
                                Ultimos4Tarjeta,
                                Importe,
                                FechaHora,
                                Estado,
                                CodigoAutorizacion
                            )
                            OUTPUT INSERTED.IdPago
                            VALUES
                            (
                                @IdFactura,
                                @Banco,
                                @Ultimos4Tarjeta,
                                @Importe,
                                @FechaHora,
                                N'Aprobado',
                                @CodigoAutorizacion
                            )";

                        using (SqlCommand comandoPago =
                            new SqlCommand(queryPago, conexion, transaccion))
                        {
                            comandoPago.Parameters.Add("@IdFactura", SqlDbType.Int).Value = pago.IdFactura;
                            comandoPago.Parameters.Add("@Banco", SqlDbType.NVarChar, 80).Value = pago.Banco;
                            comandoPago.Parameters.Add("@Ultimos4Tarjeta", SqlDbType.Char, 4).Value = pago.Ultimos4Tarjeta;

                            var pImporte = comandoPago.Parameters.Add("@Importe", SqlDbType.Decimal);
                            pImporte.Precision = 12;
                            pImporte.Scale = 2;
                            pImporte.Value = pago.Importe;

                            comandoPago.Parameters.Add("@FechaHora", SqlDbType.DateTime2).Value = pago.FechaHora;
                            comandoPago.Parameters.Add("@CodigoAutorizacion", SqlDbType.NVarChar, 50).Value = pago.CodigoAutorizacion;

                            pago.IdPago = Convert.ToInt32(comandoPago.ExecuteScalar());
                        }

                        const string queryFactura = @"
                            UPDATE Factura
                            SET Estado = N'Pagada'
                            WHERE IdFactura = @IdFactura
                              AND Estado = N'Pendiente'";

                        using (SqlCommand comandoFactura =
                            new SqlCommand(queryFactura, conexion, transaccion))
                        {
                            comandoFactura.Parameters.Add("@IdFactura", SqlDbType.Int).Value = pago.IdFactura;

                            if (comandoFactura.ExecuteNonQuery() != 1)
                            {
                                throw new Exception(
                                    "No se pudo actualizar la factura pendiente asociada al pago.");
                            }
                        }

                        transaccion.Commit();
                        pago.Estado = "Aprobado";
                        return pago;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
