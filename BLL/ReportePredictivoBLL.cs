using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ReportePredictivoBLL
    {
        private readonly ReportePredictivoDAL _dal = new ReportePredictivoDAL();

        public List<ReportePredictivoBE> ObtenerPronostico()
        {
            // Permiso propio del reporte: no se confunde con venta ni mantenimiento.
            SM.Instancia.RequierePermiso("REP_PREDICTIVO");

            // Son 28 dias calendario completos: ayer inclusive, hoy no.
            DateTime hasta = DateTime.Today;
            DateTime desde = hasta.AddDays(-28);
            List<ReportePredictivoBE> productos = _dal.ObtenerVentas(desde, hasta);

            foreach (ReportePredictivoBE producto in productos)
            {
                producto.PromedioDiario = Math.Round(producto.Vendidas28Dias / 28m, 2);
                // Redondeo hacia arriba para evitar subestimar unidades fraccionarias.
                producto.Estimadas7Dias = (int)Math.Min(int.MaxValue,
                    Math.Ceiling(producto.Vendidas28Dias * 7m / 28m));

                // Stock objetivo = demanda proyectada + stock minimo.
                // La sugerencia no modifica existencias ni genera compras.
                long faltante = (long)producto.Estimadas7Dias +
                    producto.StockMinimo - producto.StockDisponible;
                producto.ReposicionSugerida = faltante <= 0 ? 0 :
                    faltante > int.MaxValue ? int.MaxValue : (int)faltante;
            }
            return productos;
        }
    }
}
