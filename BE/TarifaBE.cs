namespace BE
{
    public class TarifaBE
    {
        public int IdTarifa { get; set; }
        public string TipoTarifa { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public TimeSpan HoraDesde { get; set; }
        public TimeSpan HoraHasta { get; set; }
        public bool Activo { get; set; }
    }
}
