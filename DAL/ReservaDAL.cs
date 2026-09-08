using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ReservaDAL
    {
        private readonly DAO_AccesoDatos _conexionDAL;
        private readonly DigitoVerificadorDAL _digitoVerificadorDAL;
        private readonly BitacoraEventoDAL _bitacoraEventoDAL;

        public ReservaDAL()
        {
            _conexionDAL = new DAO_AccesoDatos();
            _digitoVerificadorDAL = new DigitoVerificadorDAL();
            _bitacoraEventoDAL = new BitacoraEventoDAL();
        }

        //registra una reserva, su factura y su pago en una transacción atómica
        public ReservaBE RegistrarReserva(ReservaBE reserva,FacturaBE factura,PagoBE pago,global::Servicios.BitacoraEvento evento)
        {
            using (SqlConnection conexion = _conexionDAL.ObtenerConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion =
                    conexion.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        ValidarTurnoDisponible(conexion, transaccion, reserva);
                        ValidarDisponibilidadEquipamiento(conexion,transaccion,reserva.Fecha,reserva.Horario,"Paleta",reserva.CantidadPaletas,"STOCK_PALETAS_INSUFICIENTE");

                        ValidarDisponibilidadEquipamiento(conexion,transaccion,reserva.Fecha,reserva.Horario,"Pelota",reserva.CantidadPelotas,"STOCK_PELOTAS_INSUFICIENTE");

                        int idFactura = InsertarFacturaPagada(conexion, transaccion, factura);

                        int idPago = InsertarPagoAprobado(conexion,transaccion,pago,idFactura);

                        reserva.IdFactura = idFactura;

                        int idReserva =
                            InsertarReserva(conexion, transaccion, reserva);

                        // Los DV de las tablas modificadas se generan dentro
                        // de la misma transacción. Si cualquiera falla, no se
                        // confirma Factura, Pago ni Reserva.
                        _digitoVerificadorDAL.RecalcularDVEnTransaccion("Factura",conexion,transaccion);

                        _digitoVerificadorDAL.RecalcularDVEnTransaccion("Pago",conexion,transaccion);

                        _digitoVerificadorDAL.RecalcularDVEnTransaccion("Reserva",conexion,transaccion);

                        // El evento de auditoría también forma parte del mismo
                        // COMMIT para que nunca exista una Reserva exitosa sin
                        // su correspondiente registro en BitacoraEvento.
                        _bitacoraEventoDAL.Registrar(evento,conexion,transaccion);

                        transaccion.Commit();

                        factura.IdFactura = idFactura;
                        factura.Estado = "Pagada";

                        pago.IdPago = idPago;
                        pago.IdFactura = idFactura;
                        pago.Estado = "Aprobado";

                        reserva.IdReserva = idReserva;
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

        // Valida que no exista otra reserva para la misma cancha, fecha y horario
        private void ValidarTurnoDisponible(SqlConnection conexion,SqlTransaction transaccion,ReservaBE reserva)
        {
            const string query = @"
                SELECT TOP 1 IdReserva
                FROM Reserva WITH (UPDLOCK, HOLDLOCK)
                WHERE IdCancha = @IdCancha
                  AND Fecha = @Fecha
                  AND Horario = @Horario
                  AND Estado <> N'Cancelada'";

            using (SqlCommand comando = new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = reserva.IdCancha;
                comando.Parameters.Add("@Fecha", SqlDbType.Date).Value = reserva.Fecha.Date;
                comando.Parameters.Add("@Horario", SqlDbType.Time).Value = reserva.Horario;

                object? existente = comando.ExecuteScalar();

                if (existente != null && existente != DBNull.Value)
                    throw new InvalidOperationException("TURNO_NO_DISPONIBLE");
            }
        }


        /// Valida que la cantidad solicitada de equipamiento no supere el stock disponible
        private void ValidarDisponibilidadEquipamiento(SqlConnection conexion,SqlTransaction transaccion,DateTime fecha,TimeSpan horario,string tipo,int cantidadSolicitada,string codigoError)
        {
            if (cantidadSolicitada <= 0)
                return;

            const string queryStock = @"
                SELECT StockDisponible
                FROM Equipamiento WITH (UPDLOCK, HOLDLOCK)
                WHERE Tipo = @Tipo
                  AND Activo = 1";

            int stockMaximo;

            using (SqlCommand comandoStock =new SqlCommand(queryStock, conexion, transaccion))
            {
                comandoStock.Parameters.Add("@Tipo", SqlDbType.NVarChar, 30).Value = tipo;

                object? resultado = comandoStock.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    throw new InvalidOperationException(codigoError);

                stockMaximo = Convert.ToInt32(resultado);
            }

            string columna =tipo.Equals("Paleta", StringComparison.OrdinalIgnoreCase)? "CantidadPaletas": "CantidadPelotas";

            string queryReservado = $@"
                SELECT ISNULL(SUM({columna}), 0)
                FROM Reserva WITH (UPDLOCK, HOLDLOCK)
                WHERE Fecha = @Fecha
                  AND Horario = @Horario
                  AND Estado <> N'Cancelada'";

            int cantidadReservada;

            using (SqlCommand comandoReservado =new SqlCommand(queryReservado, conexion, transaccion))
            {
                comandoReservado.Parameters.Add("@Fecha", SqlDbType.Date).Value = fecha.Date;
                comandoReservado.Parameters.Add("@Horario", SqlDbType.Time).Value = horario;

                cantidadReservada =Convert.ToInt32(comandoReservado.ExecuteScalar());
            }

            if (cantidadReservada + cantidadSolicitada > stockMaximo)
                throw new InvalidOperationException(codigoError);
        }


        // Inserta la factura con estado "Pagada" y devuelve el IdFactura generado
        private int InsertarFacturaPagada(SqlConnection conexion,SqlTransaction transaccion,FacturaBE factura)
        {
            const string query = @"
                INSERT INTO Factura
                (
                    IdCliente, IdCancha, IdTarifa, FechaHoraEmision, FechaReserva, Horario,
                    CantidadPaletas, CantidadPelotas, ImporteTarifa, ImporteEquipamiento, ImporteTotal, Estado
                )
                OUTPUT INSERTED.IdFactura
                VALUES
                (
                    @IdCliente, @IdCancha, @IdTarifa, @FechaHoraEmision, @FechaReserva, @Horario,
                    @CantidadPaletas, @CantidadPelotas, @ImporteTarifa,@ImporteEquipamiento, @ImporteTotal, N'Pagada'
                )";

            using (SqlCommand comando =new SqlCommand(query, conexion, transaccion))
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

                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        // Inserta el pago con estado "Aprobado" y devuelve el IdPago generado
        private int InsertarPagoAprobado(SqlConnection conexion,SqlTransaction transaccion,PagoBE pago,int idFactura)
        {
            const string query = @"
                INSERT INTO Pago
                (
                    IdFactura, Banco, Ultimos4Tarjeta, Importe, FechaHora, Estado, CodigoAutorizacion
                )
                OUTPUT INSERTED.IdPago
                VALUES
                (
                    @IdFactura, @Banco, @Ultimos4Tarjeta, @Importe, @FechaHora, N'Aprobado', @CodigoAutorizacion
                )";

            using (SqlCommand comando =
                new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@IdFactura", SqlDbType.Int).Value = idFactura;
                comando.Parameters.Add("@Banco", SqlDbType.NVarChar, 80).Value = pago.Banco;
                comando.Parameters.Add("@Ultimos4Tarjeta", SqlDbType.Char, 4).Value = pago.Ultimos4Tarjeta;

                var pImporte = comando.Parameters.Add("@Importe", SqlDbType.Decimal);
                pImporte.Precision = 12;
                pImporte.Scale = 2;
                pImporte.Value = pago.Importe;

                comando.Parameters.Add("@FechaHora", SqlDbType.DateTime2).Value = pago.FechaHora;
                comando.Parameters.Add("@CodigoAutorizacion", SqlDbType.NVarChar, 50).Value = pago.CodigoAutorizacion;

                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        // Inserta la reserva con estado "Reservada" y devuelve el IdReserva generado
        private int InsertarReserva(SqlConnection conexion,SqlTransaction transaccion,ReservaBE reserva)
        {
            const string query = @"
                INSERT INTO Reserva
                (
                    Codigo, IdCliente, IdCancha, IdTarifa, IdFactura, Fecha,
                    Horario, CantidadPaletas, CantidadPelotas, Estado
                )
                OUTPUT INSERTED.IdReserva
                VALUES
                (
                    @Codigo, @IdCliente, @IdCancha, @IdTarifa, @IdFactura, @Fecha,
                    @Horario, @CantidadPaletas, @CantidadPelotas, N'Reservada'
                )";

            using (SqlCommand comando =new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = reserva.Codigo;
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = reserva.IdCliente;
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = reserva.IdCancha;
                comando.Parameters.Add("@IdTarifa", SqlDbType.Int).Value = reserva.IdTarifa;
                comando.Parameters.Add("@IdFactura", SqlDbType.Int).Value = reserva.IdFactura;
                comando.Parameters.Add("@Fecha", SqlDbType.Date).Value = reserva.Fecha.Date;
                comando.Parameters.Add("@Horario", SqlDbType.Time).Value = reserva.Horario;
                comando.Parameters.Add("@CantidadPaletas", SqlDbType.Int).Value = reserva.CantidadPaletas;
                comando.Parameters.Add("@CantidadPelotas", SqlDbType.Int).Value = reserva.CantidadPelotas;

                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}
