namespace BE
{
    public class CanchaBE
    {
        public int IdCancha { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public override string ToString()
        {
            return Nombre;
        }
    }
}
