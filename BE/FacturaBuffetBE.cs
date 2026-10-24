using System;
using System.Collections.Generic;

namespace BE
{
    // CUN08: cabecera y detalles permanecen en memoria hasta el cobro aprobado.
    // En SQL corresponde a dbo.FacturaBuffet; no modifica BE.FacturaBE (PN1).
    public class FacturaBuffetBE
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public int IdVendedor { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = "EnPreparacion"; // Solo estado en memoria; SQL permite Pagada/Anulada.
        public string Banco { get; set; } = string.Empty;
        public string Ultimos4Tarjeta { get; set; } = string.Empty;
        public DateTime FechaHoraPago { get; set; }
        public string CodigoAutorizacion { get; set; } = string.Empty;
        public List<DetalleFacturaBE> Detalles { get; set; } = new List<DetalleFacturaBE>();
    }
}
