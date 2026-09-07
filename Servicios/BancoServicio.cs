namespace Servicios
{
    /// <summary>
    /// Adaptador de desarrollo para representar la comunicación con el Banco.
    /// El proyecto no dispone actualmente de un endpoint bancario real.
    ///
    /// Tarjetas de prueba:
    /// - 4111111111111111: aprobada.
    /// - 4000000000000002: rechazada.
    /// </summary>
    public class BancoServicio
    {
        public const string TarjetaRechazadaPrueba = "4000000000000002";

        public ResultadoAutorizacionBanco AutorizarPago(
            string dniCliente,
            string numeroTarjeta,
            decimal importe)
        {
            if (numeroTarjeta == TarjetaRechazadaPrueba)
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
