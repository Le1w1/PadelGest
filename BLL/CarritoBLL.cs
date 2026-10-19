using BE;
using DAL;
using Servicios;
using System.Text.RegularExpressions;

namespace BLL
{
    public class CarritoBLL
    {
        private readonly CarritoDAL _carritoDAL;
        private readonly ProductoBLL _productoBLL;

        public CarritoBLL()
        {
            _carritoDAL = new CarritoDAL();
            _productoBLL = new ProductoBLL();
        }

        private static string T(string clave) => Traductor.Instancia.Traducir(clave);

        private static int ObtenerVendedor()
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");

            Usuario? vendedor = SM.Instancia.UsuarioActual;
            if (vendedor == null || vendedor.IdUsuario <= 0)
                throw new Exception(T("Errores.Buffet.SesionInvalida"));

            return vendedor.IdUsuario;
        }

        // CUN08: inicia un carrito vacio, todavia sin cliente ni venta.
        public CarritoBE GenerarCarrito()
        {
            return _carritoDAL.CrearCarrito(ObtenerVendedor());
        }

        // Permite reabrir la vista del carrito propio.
        public CarritoBE ConsultarCarrito(int idCarrito)
        {
            return _carritoDAL.ObtenerCarrito(idCarrito, ObtenerVendedor());
        }

        // CUN09/CUN08: incorpora cantidades a un renglon nuevo o existente.
        public CarritoBE AgregarProducto(int idCarrito, int codigoProducto, int cantidad)
        {
            int idVendedor = ObtenerVendedor();

            ValidarIdCarrito(idCarrito);
            _productoBLL.SeleccionarProducto(codigoProducto, cantidad);
            return _carritoDAL.GuardarProducto(idCarrito, idVendedor, codigoProducto, cantidad, true);
        }

        // Cambia la cantidad total de un renglon, no suma sobre la anterior.
        public CarritoBE ModificarCantidad(int idCarrito, int codigoProducto, int nuevaCantidad)
        {
            int idVendedor = ObtenerVendedor();

            ValidarIdCarrito(idCarrito);
            _productoBLL.SeleccionarProducto(codigoProducto, nuevaCantidad);
            return _carritoDAL.GuardarProducto(idCarrito, idVendedor, codigoProducto, nuevaCantidad, false);
        }

        public CarritoBE QuitarProducto(int idCarrito, int codigoProducto)
        {
            int idVendedor = ObtenerVendedor();

            ValidarIdCarrito(idCarrito);
            if (codigoProducto <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));

            return _carritoDAL.QuitarProducto(idCarrito, idVendedor, codigoProducto);
        }

        // CUN10: despues de seleccionar/registrar al cliente se asocia por DNI.
        // Listo indica que el carrito ya no puede modificarse antes del cobro.
        public CarritoBE AsociarCliente(int idCarrito, string dni)
        {
            int idVendedor = ObtenerVendedor();

            ValidarIdCarrito(idCarrito);
            dni = (dni ?? string.Empty).Trim();

            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
                throw new Exception(T("Errores.Buffet.DNIInvalido"));

            return _carritoDAL.AsociarCliente(idCarrito, idVendedor, dni);
        }

        // Cancelar antes de cobrar no produce una venta ni descuenta stock.
        public CarritoBE CancelarCarrito(int idCarrito)
        {
            ValidarIdCarrito(idCarrito);
            return _carritoDAL.CancelarCarrito(idCarrito, ObtenerVendedor());
        }

        private static void ValidarIdCarrito(int idCarrito)
        {
            if (idCarrito <= 0)
                throw new Exception(T("Errores.Buffet.CarritoInvalido"));
        }
    }
}
