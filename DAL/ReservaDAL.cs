using BE;
using DAL.Servicios;
using Microsoft.Data.SqlClient;
using Servicios;
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

        // Registra factura y reserva (incluyendo los datos del pago) en una transacción atómica.
        public ReservaBE RegistrarReserva(ReservaBE reserva,FacturaBE factura,BitacoraEvento evento)
        {
            using SqlConnection conexion = _conexionDAL.ObtenerConexion();

            conexion.Open();

            using SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                ValidarDisponibilidad(conexion,transaccion,reserva);

                (int idFactura,int idReserva) = PersistirOperacion(conexion,transaccion,reserva,factura);

                ActualizarDigitosVerificadores(conexion,transaccion);

                _bitacoraEventoDAL.Registrar(evento,conexion,transaccion);

                // Si todo se ejecuta correctamente, se confirma la transacción
                transaccion.Commit();

                ActualizarEntidadesRegistradas(reserva,factura,idFactura,idReserva);

                return reserva;
            }
            catch
            {
                // Si ocurre algún error, se revierte la transacción
                transaccion.Rollback();
                throw;
            }
        }

        private void ValidarDisponibilidad(SqlConnection conexion,SqlTransaction transaccion,ReservaBE reserva)
        {
            ValidarTurnoDisponible(conexion,transaccion,reserva);

            ValidarDisponibilidadEquipamiento(conexion,transaccion,reserva.Fecha,reserva.Horario,"Paleta",reserva.CantidadPaletas,"STOCK_PALETAS_INSUFICIENTE");

            ValidarDisponibilidadEquipamiento(conexion,transaccion,reserva.Fecha,reserva.Horario,"Pelota",reserva.CantidadPelotas,"STOCK_PELOTAS_INSUFICIENTE");
        }


        // Persiste la factura y la reserva con sus datos de pago en la misma transacción.
        private (int IdFactura,int IdReserva) PersistirOperacion(SqlConnection conexion,SqlTransaction transaccion,ReservaBE reserva,FacturaBE factura)
        {
            int idFactura = InsertarFacturaPagada(conexion,transaccion,factura);

            reserva.IdFactura = idFactura;

            int idReserva = InsertarReserva(conexion,transaccion,reserva);

            return (idFactura,idReserva);
        }


        // Actualiza los dígitos verificadores de las tablas afectadas dentro de la misma transacción
        private void ActualizarDigitosVerificadores(SqlConnection conexion,SqlTransaction transaccion)
        {
            // La integridad global se verifica antes de iniciar sesión. Los DV de
            // las tablas modificadas se regeneran dentro de la misma transacción.
            _digitoVerificadorDAL.RecalcularDVEnTransaccion("Factura",conexion,transaccion);
            _digitoVerificadorDAL.RecalcularDVEnTransaccion("Reserva",conexion,transaccion);
        }

        // Actualiza los Ids generados y los estados de las entidades registradas  
        private void ActualizarEntidadesRegistradas(ReservaBE reserva,FacturaBE factura,int idFactura,int idReserva)
        {
            factura.IdFactura = idFactura;
            factura.Estado = "Pagada";

            reserva.IdReserva = idReserva;
            reserva.Estado = "Reservada";
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


        // Valida que la cantidad solicitada de equipamiento no supere el stock disponible
        private void ValidarDisponibilidadEquipamiento(SqlConnection conexion,SqlTransaction transaccion,DateTime fecha,TimeSpan horario,string tipo,int cantidadSolicitada,string codigoError)
        {
            if (cantidadSolicitada <= 0)
                return;

            int stockMaximo = ObtenerStockEquipamiento(conexion,transaccion,tipo,codigoError);

            int cantidadReservada = ObtenerCantidadReservada(conexion,transaccion,fecha,horario,tipo);

            if (cantidadReservada + cantidadSolicitada > stockMaximo)
                throw new InvalidOperationException(codigoError);
        }

        private int ObtenerStockEquipamiento(SqlConnection conexion,SqlTransaction transaccion,string tipo,string codigoError)
        {
            const string queryStock = @"
                SELECT StockDisponible
                FROM Equipamiento WITH (UPDLOCK, HOLDLOCK)
                WHERE Tipo = @Tipo
                  AND Activo = 1";

            using (SqlCommand comandoStock =new SqlCommand(queryStock, conexion, transaccion))
            {
                comandoStock.Parameters.Add("@Tipo", SqlDbType.NVarChar, 30).Value = tipo;

                object? resultado = comandoStock.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    throw new InvalidOperationException(codigoError);

                return Convert.ToInt32(resultado);
            }
        }

        // Obtiene la cantidad de equipamiento reservado para una fecha y horario específicos
        private int ObtenerCantidadReservada(SqlConnection conexion,SqlTransaction transaccion,DateTime fecha,TimeSpan horario,string tipo)
        {
            string columna =tipo.Equals("Paleta", StringComparison.OrdinalIgnoreCase)? "CantidadPaletas": "CantidadPelotas";

            string queryReservado = $@"
                SELECT ISNULL(SUM({columna}), 0)
                FROM Reserva WITH (UPDLOCK, HOLDLOCK)
                WHERE Fecha = @Fecha
                  AND Horario = @Horario
                  AND Estado <> N'Cancelada'";

            using (SqlCommand comandoReservado =new SqlCommand(queryReservado, conexion, transaccion))
            {
                comandoReservado.Parameters.Add("@Fecha", SqlDbType.Date).Value = fecha.Date;
                comandoReservado.Parameters.Add("@Horario", SqlDbType.Time).Value = horario;

                return Convert.ToInt32(comandoReservado.ExecuteScalar());
            }
        }


        // Inserta la factura con estado "Pagada" y devuelve el IdFactura generado
        private int InsertarFacturaPagada(SqlConnection conexion,SqlTransaction transaccion,FacturaBE factura)
        {
            const string query = @"
                INSERT INTO Factura
                (
                    DNI, IdCancha, IdTarifa, FechaHoraEmision, FechaReserva, Horario,
                    CantidadPaletas, CantidadPelotas, ImporteTarifa, ImporteEquipamiento, ImporteTotal, Estado
                )
                OUTPUT INSERTED.IdFactura
                VALUES
                (
                    @DNI, @IdCancha, @IdTarifa, @FechaHoraEmision, @FechaReserva, @Horario,
                    @CantidadPaletas, @CantidadPelotas, @ImporteTarifa,@ImporteEquipamiento, @ImporteTotal, N'Pagada'
                )";

            using (SqlCommand comando =new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = factura.DNI;
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

        // Inserta la reserva con estado "Reservada" y devuelve el IdReserva generado
        private int InsertarReserva(SqlConnection conexion,SqlTransaction transaccion,ReservaBE reserva)
        {
            const string query = @"
                INSERT INTO Reserva
                (
                    Codigo, DNI, IdCancha, IdTarifa, IdFactura, Fecha,
                    Horario, CantidadPaletas, CantidadPelotas, Estado,
                    Banco, Ultimos4Tarjeta, ImportePago, FechaHoraPago, EstadoPago, CodigoAutorizacion
                )
                OUTPUT INSERTED.IdReserva
                VALUES
                (
                    @Codigo, @DNI, @IdCancha, @IdTarifa, @IdFactura, @Fecha,
                    @Horario, @CantidadPaletas, @CantidadPelotas, N'Reservada',
                    @Banco, @Ultimos4Tarjeta, @ImportePago, @FechaHoraPago, @EstadoPago, @CodigoAutorizacion
                )";

            using (SqlCommand comando =new SqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.Add("@Codigo", SqlDbType.NVarChar, 30).Value = reserva.Codigo;
                comando.Parameters.Add("@DNI", SqlDbType.NVarChar, 8).Value = reserva.DNI;
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = reserva.IdCancha;
                comando.Parameters.Add("@IdTarifa", SqlDbType.Int).Value = reserva.IdTarifa;
                comando.Parameters.Add("@IdFactura", SqlDbType.Int).Value = reserva.IdFactura;
                comando.Parameters.Add("@Fecha", SqlDbType.Date).Value = reserva.Fecha.Date;
                comando.Parameters.Add("@Horario", SqlDbType.Time).Value = reserva.Horario;
                comando.Parameters.Add("@CantidadPaletas", SqlDbType.Int).Value = reserva.CantidadPaletas;
                comando.Parameters.Add("@CantidadPelotas", SqlDbType.Int).Value = reserva.CantidadPelotas;
                comando.Parameters.Add("@Banco", SqlDbType.NVarChar, 80).Value = reserva.Banco;
                comando.Parameters.Add("@Ultimos4Tarjeta", SqlDbType.Char, 4).Value = reserva.Ultimos4Tarjeta;

                var importePago = comando.Parameters.Add("@ImportePago", SqlDbType.Decimal);
                importePago.Precision = 12;
                importePago.Scale = 2;
                importePago.Value = reserva.ImportePago;

                comando.Parameters.Add("@FechaHoraPago", SqlDbType.DateTime2).Value = reserva.FechaHoraPago;
                comando.Parameters.Add("@EstadoPago", SqlDbType.NVarChar, 20).Value = reserva.EstadoPago;
                comando.Parameters.Add("@CodigoAutorizacion", SqlDbType.NVarChar, 50).Value = reserva.CodigoAutorizacion;

                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}
