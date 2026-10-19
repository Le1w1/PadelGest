namespace BE
{
    // Carrito de venta del buffet. Puede existir sin cliente hasta CUN10.
    public class CarritoBE
    {
        public int IdCarrito { get; set; }
        public string? DNI { get; set; }
        public int IdVendedor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<DetalleCarritoBE> Detalles { get; set; } = new List<DetalleCarritoBE>();
    }
}
