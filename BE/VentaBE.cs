namespace BE
{
    // Venta registrada por el buffet. Los datos del pago se conservan aqui
    // (sin guardar numero completo de tarjeta ni codigo de seguridad).
    public class VentaBE
    {
        public int IdVenta { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int IdCarrito { get; set; }
        public string DNI { get; set; } = string.Empty;
        public int IdVendedor { get; set; }
        public DateTime FechaHora { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Banco { get; set; } = string.Empty;
        public string Ultimos4Tarjeta { get; set; } = string.Empty;
        public decimal ImportePago { get; set; }
        public DateTime FechaHoraPago { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
        public string CodigoAutorizacion { get; set; } = string.Empty;
        public FacturaVentaBE Factura { get; set; } = new FacturaVentaBE();
    }
}
