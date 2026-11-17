namespace BE
{
    // Producto comercializado en el buffet. El stock cambia solamente al registrar
    // una venta aprobada, nunca al agregar un producto al carrito.
    public class ProductoBE
    {
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return $"{Nombre} - {Categoria}";
        }
    }
}
