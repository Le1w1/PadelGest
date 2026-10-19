using BE;
using DAL;
using Servicios;

namespace BLL
{
    public class ProductoBLL
    {
        private readonly ProductoDAL _productoDAL;

        public ProductoBLL()
        {
            _productoDAL = new ProductoDAL();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        // CUN09: permite consultar el catalogo a quien vende o consulta stock.
        public List<ProductoBE> ObtenerProductos()
        {
            if (!SM.Instancia.TienePermiso("BUF_VENDER") &&!SM.Instancia.TienePermiso("BUF_CONSULTAR_STOCK"))
            {
                SM.Instancia.RequierePermiso("BUF_VENDER");
            }

            return _productoDAL.ObtenerProductosActivos();
        }

        // CUN09: verifica la seleccion y devuelve el producto actualizado.
        // No reserva unidades ni modifica el stock: eso corresponde a CUN13/CUN14.
        public ProductoBE SeleccionarProducto(int codigoProducto, int cantidad)
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");

            if (codigoProducto <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));

            if (cantidad <= 0)
                throw new Exception(T("Errores.Buffet.CantidadInvalida"));

            ProductoBE? producto = _productoDAL.BuscarPorCodigo(codigoProducto);

            if (producto == null || !producto.Activo)
                throw new Exception(T("Errores.Buffet.ProductoNoDisponible"));

            if (producto.PrecioUnitario <= 0)
                throw new Exception(T("Errores.Buffet.PrecioInvalido"));

            if (producto.StockDisponible < cantidad)
                throw new Exception(T("Errores.Buffet.StockInsuficiente"));

            return producto;
        }
    }
}
