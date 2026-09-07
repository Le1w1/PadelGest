namespace BE
{
    public class ReservaBE
    {
        public int IdReserva { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public int IdCancha { get; set; }
        public int IdTarifa { get; set; }
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
        public int CantidadPaletas { get; set; }
        public int CantidadPelotas { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
