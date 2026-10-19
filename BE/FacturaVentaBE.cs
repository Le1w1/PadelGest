namespace BE
{
    // Comprobante del PN2: es diferente de FacturaBE, que pertenece a las reservas.
    public class FacturaVentaBE
    {
        public int IdFacturaVenta { get; set; }
        public int IdVenta { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
