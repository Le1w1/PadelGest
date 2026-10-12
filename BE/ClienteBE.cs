namespace BE
{
    public class ClienteBE
    {
        public string DNI { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Apellido}, {Nombre} - DNI {DNI}";
        }
    }
}
