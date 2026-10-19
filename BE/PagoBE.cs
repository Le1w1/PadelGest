namespace BE
{
    // Resultado transitorio del cobro; no se persiste en una tabla Pago.
    public class PagoBE
    {
        public string Banco { get; set; } = string.Empty;
        public string Ultimos4Tarjeta { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string CodigoAutorizacion { get; set; } = string.Empty;
    }
}
