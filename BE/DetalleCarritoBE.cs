namespace BE
{
    // Guarda la cantidad y el precio unitario de cada producto seleccionado.
    // El precio es el de la seleccion, no depende de cambios posteriores en Producto.
    public class DetalleCarritoBE
    {
        public int IdDetalleCarrito { get; set; }
        public int IdCarrito { get; set; }
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
