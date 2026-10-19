namespace BE
{
    public class ResultadoCobroBE
    {
        public bool Aprobado { get; set; }
        public FacturaBE Factura { get; set; } = new FacturaBE();
        public PagoBE? Pago { get; set; }
    }
}
