namespace BE
{
    public class ReservaBE
    {
        public int IdReserva { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public int IdCancha { get; set; }
        public int IdTarifa { get; set; }
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
        public int CantidadPaletas { get; set; }
        public int CantidadPelotas { get; set; }
        public string Estado { get; set; } = string.Empty;
        // El pago aprobado se persiste como parte de Reserva (sin tabla Pago).
        public string Banco { get; set; } = string.Empty;
        public string Ultimos4Tarjeta { get; set; } = string.Empty;
        public decimal ImportePago { get; set; }
        public DateTime FechaHoraPago { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
        public string CodigoAutorizacion { get; set; } = string.Empty;
    }
}
