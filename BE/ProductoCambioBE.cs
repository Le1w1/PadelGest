namespace BE
{
    // Una version historica de dbo.Producto, almacenada en dbo.Producto_C.
    // Act marca la version vigente del historial; ActivoProducto
    // representa el estado comercial que tenia el producto en esa version.
    public class ProductoCambioBE
    {
        public long IdCambio { get; set; }
        public int CodigoProducto { get; set; }
        public DateTime FechaHora { get; set; }
        // Propiedades de solo lectura para mostrar fecha y hora en columnas separadas.
        public string Fecha => FechaHora.ToString("dd/MM/yyyy");
        public string Hora => FechaHora.ToString("HH:mm:ss");
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public int StockMinimo { get; set; }
        public bool ActivoProducto { get; set; }
        public bool Act { get; set; }
        public string TipoCambio { get; set; } = string.Empty;
    }
}
