namespace BE
{
    // La autorizacion bancaria simulada no persiste por si misma.
    public class ResultadoCobroFacturaBE
    {
        public bool Aprobado { get; set; }
        public PagoBE? Pago { get; set; }
    }
}
