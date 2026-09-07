using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ReservaDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;

        public ReservaDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
        }

        /// <summary>
        /// Valida nuevamente la disponibilidad y registra la Reserva de forma
        /// atómica. SERIALIZABLE + UPDLOCK/HOLDLOCK impiden que dos procesos
        /// registren simultáneamente la misma cancha, fecha y horario.
        /// </summary>
        public ReservaBE RegistrarReserva(ReservaBE reserva)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion =
                    conexion.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        const string queryDisponibilidad = @"
                            SELECT TOP 1 IdReserva
                            FROM Reserva WITH (UPDLOCK, HOLDLOCK)
                            WHERE IdCancha = @IdCancha
                              AND Fecha = @Fecha
                              AND Horario = @Horario
                              AND Estado <> N'Cancelada'";

                        using (SqlCommand comandoDisponibilidad =
                            new SqlCommand(queryDisponibilidad, conexion, transaccion))
                        {
                            comandoDisponibilidad.Parameters.Add("@IdCancha", SqlDbType.Int).Value =
                                reserva.IdCancha;
                            comandoDisponibilidad.Parameters.Add("@Fecha", SqlDbType.Date).Value =
                                reserva.Fecha.Date;
                            comandoDisponibilidad.Parameters.Add("@Horario", SqlDbType.Time).Value =
                                reserva.Horario;

                            object? existente = comandoDisponibilidad.ExecuteScalar();

                            if (existente != null && existente != DBNull.Value)
                            {
                                throw new InvalidOperationException("TURNO_NO_DISPONIBLE");
                            }
                        }

                        const string queryInsert = @"
                            INSERT INTO Reserva
                            (
                                Codigo,
                                IdCliente,
                                IdCancha,
                                IdTarifa,
                                IdFactura,
                                Fecha,
                                Horario,
                                CantidadPaletas,
                                CantidadPelotas,
                                Estado
                            )
                            OUTPUT INSERTED.IdReserva
                            VALUES
                            (
                                @Codigo,
                                @IdCliente,
                                @IdCancha,
                                @IdTarifa,
                                @IdFactura,
                                @Fecha,
                                @Horario,
                                @CantidadPaletas,
                                @CantidadPelotas,
                                N'Reservada'
                            )";

                        using (SqlCommand comandoInsert =
                            new SqlCommand(queryInsert, conexion, transaccion))
                        {
                            comandoInsert.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value =
                                reserva.Codigo;
                            comandoInsert.Parameters.Add("@IdCliente", SqlDbType.Int).Value =
                                reserva.IdCliente;
                            comandoInsert.Parameters.Add("@IdCancha", SqlDbType.Int).Value =
                                reserva.IdCancha;
                            comandoInsert.Parameters.Add("@IdTarifa", SqlDbType.Int).Value =
                                reserva.IdTarifa;
                            comandoInsert.Parameters.Add("@IdFactura", SqlDbType.Int).Value =
                                reserva.IdFactura;
                            comandoInsert.Parameters.Add("@Fecha", SqlDbType.Date).Value =
                                reserva.Fecha.Date;
                            comandoInsert.Parameters.Add("@Horario", SqlDbType.Time).Value =
                                reserva.Horario;
                            comandoInsert.Parameters.Add("@CantidadPaletas", SqlDbType.Int).Value =
                                reserva.CantidadPaletas;
                            comandoInsert.Parameters.Add("@CantidadPelotas", SqlDbType.Int).Value =
                                reserva.CantidadPelotas;

                            reserva.IdReserva =
                                Convert.ToInt32(comandoInsert.ExecuteScalar());
                        }

                        // CUN07 - Actualización automática de stock.
                        // Se ejecuta después del INSERT de la Reserva, pero antes
                        // del COMMIT para que Reserva + stock sean una sola unidad
                        // atómica: si falla el stock, también se revierte la Reserva.
                        DescontarStockEquipamiento(
                            conexion,
                            transaccion,
                            "Paleta",
                            reserva.CantidadPaletas,
                            "STOCK_PALETAS_INSUFICIENTE");

                        DescontarStockEquipamiento(
                            conexion,
                            transaccion,
                            "Pelota",
                            reserva.CantidadPelotas,
                            "STOCK_PELOTAS_INSUFICIENTE");

                        transaccion.Commit();
                        reserva.Estado = "Reservada";
                        return reserva;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        private void DescontarStockEquipamiento(
            SqlConnection conexion,
            SqlTransaction transaccion,
            string tipo,
            int cantidad,
            string codigoError)
        {
            if (cantidad <= 0)
            {
                return;
            }

            const string query = @"
                UPDATE Equipamiento
                SET StockDisponible = StockDisponible - @Cantidad
                WHERE Tipo = @Tipo
                  AND Activo = 1
                  AND StockDisponible >= @Cantidad";

            using (SqlCommand comando =
                new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@Tipo", SqlDbType.NVarChar, 30).Value = tipo;
                comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas != 1)
                {
                    throw new InvalidOperationException(codigoError);
                }
            }
        }
    }
}
