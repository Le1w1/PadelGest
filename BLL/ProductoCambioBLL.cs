using BE;
using DAL;
using Servicios;

namespace BLL
{
    // El permiso PRO_GESTIONAR es el mismo que protege el Maestro de Productos.
    public class ProductoCambioBLL
    {
        private readonly ProductoCambioDAL _productoCambioDAL = new ProductoCambioDAL();

        public List<ProductoCambioBE> ConsultarCambios(
            int? codigoProducto = null, string nombre = "",
            DateTime? fechaDesde = null, DateTime? fechaHasta = null,
            bool soloVigentes = false)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            if (codigoProducto.HasValue && codigoProducto.Value <= 0)
                throw new ArgumentException("El codigo de producto debe ser positivo.");

            if (fechaDesde.HasValue && fechaHasta.HasValue &&
                fechaDesde.Value.Date > fechaHasta.Value.Date)
                throw new ArgumentException("La fecha desde no puede superar la fecha hasta.");

            if (fechaHasta.HasValue && fechaHasta.Value.Date == DateTime.MaxValue.Date)
                throw new ArgumentException("Fecha hasta fuera de rango.");

            nombre = (nombre ?? string.Empty).Trim();
            if (nombre.Length > 120)
                throw new ArgumentException("El nombre buscado excede 120 caracteres.");

            return _productoCambioDAL.Consultar(
                codigoProducto, nombre, fechaDesde, fechaHasta, soloVigentes);
        }
    }
}
