namespace BE
{
    public class FacturaBE
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdCancha { get; set; }
        public int IdTarifa { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public DateTime FechaReserva { get; set; }
        public TimeSpan Horario { get; set; }
        public int CantidadPaletas { get; set; }
        public int CantidadPelotas { get; set; }
        public decimal ImporteTarifa { get; set; }
        public decimal ImporteEquipamiento { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
