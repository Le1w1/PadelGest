namespace BE
{
    // Resultado de una estimacion basada en ventas historicas del buffet.
    public class ReportePredictivoBE
    {
        public int CodigoProducto { get; set; }
        public string Producto { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Vendidas28Dias { get; set; }
        public decimal PromedioDiario { get; set; }
        public int Estimadas7Dias { get; set; }
        public int StockDisponible { get; set; }
        public int StockMinimo { get; set; }
        public int ReposicionSugerida { get; set; }
        public decimal CoberturaDias { get; set; }
        public decimal PrecioActual { get; set; }
        public decimal IngresoPotencial7Dias { get; set; }
        public string Prioridad { get; set; } = string.Empty;
    }
}
