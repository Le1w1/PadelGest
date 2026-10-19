namespace BE
{
    public class EquipamientoBE
    {
        public int IdEquipamiento { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        public int StockDisponible { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            return Tipo;
        }
    }
}
