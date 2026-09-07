namespace Servicios
{
    /// <summary>
    /// Adaptador de desarrollo para representar la comunicación con el Banco.
    /// El proyecto no dispone actualmente de un endpoint bancario real.
    ///
    /// Durante la demostración, la respuesta se selecciona manualmente para
    /// simular lo que en un entorno real respondería el sistema del Banco.
    /// </summary>
    public class BancoServicio
    {
        public ResultadoAutorizacionBanco AutorizarPago(
            string dniCliente,
            string numeroTarjeta,
            decimal importe,
            bool respuestaSimuladaAprobada)
        {
            if (!respuestaSimuladaAprobada)
            {
                return new ResultadoAutorizacionBanco
                {
                    Aprobado = false,
                    CodigoAutorizacion = string.Empty
                };
            }

            return new ResultadoAutorizacionBanco
            {
                Aprobado = true,
                CodigoAutorizacion =
                    "SIM-" + Guid.NewGuid().ToString("N")[..12].ToUpperInvariant()
            };
        }
    }

    public class ResultadoAutorizacionBanco
    {
        public bool Aprobado { get; set; }
        public string CodigoAutorizacion { get; set; } = string.Empty;
    }
}
