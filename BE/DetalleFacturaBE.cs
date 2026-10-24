namespace BE
{
    // Renglón de factura. No se guarda en SQL hasta aprobar el pago.
    public class DetalleFacturaBE
    {
        public int IdDetalleFactura { get; set; }
        public int IdFactura { get; set; }
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty; // Para mostrar en pantalla; no es columna de DetalleFactura.
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario; // Columna calculada PERSISTED en SQL Server.
    }
}
