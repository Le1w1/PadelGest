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

        // Consulta de productos activos para el filtro de la bitacora de cambios.
        // Se utiliza el permiso de gestion, sin habilitar operaciones de venta.
        public List<ProductoBE> ObtenerProductosActivosParaAuditoria()
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            return _productoDAL.ObtenerProductosActivos();
        }

        // El mantenimiento de productos requiere un permiso independiente de la venta.
        // La consulta tambien puede utilizar BUF_CONSULTAR_STOCK.
        public List<ProductoBE> ObtenerTodos()
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");

            return _productoDAL.ObtenerTodos();
        }

        public void CrearProducto(string nombre, string categoria,
            decimal precio, int stockInicial, int stockMinimo)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            ValidarDatos(nombre, categoria, precio, stockMinimo);

            if (stockInicial < 0)
                throw new Exception(T("Errores.Productos.StockInicial"));

            _productoDAL.CrearProducto(nombre.Trim(), categoria.Trim(),
                precio, stockInicial, stockMinimo,
                SM.Instancia.UsuarioActual.IdUsuario,
                SM.Instancia.UsuarioActual.NombreUsuario);
        }

        public void ModificarProducto(int codigo, string nombre, string categoria,
            decimal precio, int stockMinimo)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            if (codigo <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));

            ValidarDatos(nombre, categoria, precio, stockMinimo);
            // El stock NO se edita aqui, se modifica con ReponerStock o al vender.
            _productoDAL.ModificarProducto(codigo, nombre.Trim(), categoria.Trim(),
                precio, stockMinimo, SM.Instancia.UsuarioActual.IdUsuario,
                SM.Instancia.UsuarioActual.NombreUsuario);
        }

        // Guarda los datos del producto y suma, si corresponde, unidades de stock
        // en una sola transaccion y con un unico registro historico.
        public void ModificarProductoYReponer(int codigo, string nombre, string categoria,
            decimal precio, int stockMinimo, int cantidadReponer)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            if (codigo <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));
            ValidarDatos(nombre, categoria, precio, stockMinimo);
            if (cantidadReponer < 0)
                throw new ArgumentException("Las unidades a agregar no pueden ser negativas.");

            _productoDAL.ModificarProductoYReponer(codigo, nombre.Trim(), categoria.Trim(),
                precio, stockMinimo, cantidadReponer,
                SM.Instancia.UsuarioActual.IdUsuario,
                SM.Instancia.UsuarioActual.NombreUsuario);
        }

        public void CambiarEstado(int codigo, bool activar)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            if (codigo <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));

            _productoDAL.CambiarEstado(codigo, activar,
                SM.Instancia.UsuarioActual.IdUsuario,
                SM.Instancia.UsuarioActual.NombreUsuario);
        }

        public void ReponerStock(int codigo, int cantidad)
        {
            SM.Instancia.RequierePermiso("PRO_GESTIONAR");
            if (codigo <= 0)
                throw new Exception(T("Errores.Buffet.CodigoProductoInvalido"));
            if (cantidad <= 0)
                throw new Exception(T("Errores.Productos.ReposicionInvalida"));

            _productoDAL.ReponerStock(codigo, cantidad,
                SM.Instancia.UsuarioActual.IdUsuario,
                SM.Instancia.UsuarioActual.NombreUsuario);
        }

        private static void ValidarDatos(string nombre, string categoria,
            decimal precio, int stockMinimo)
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Trim().Length > 120)
                throw new Exception(T("Errores.Productos.NombreInvalido"));
            if (string.IsNullOrWhiteSpace(categoria) || categoria.Trim().Length > 80)
                throw new Exception(T("Errores.Productos.CategoriaInvalida"));
            if (precio <= 0 || precio > 9999999999.99m ||
                decimal.Round(precio, 2) != precio)
                throw new Exception(T("Errores.Productos.PrecioInvalido"));
            if (stockMinimo < 0)
                throw new Exception(T("Errores.Productos.StockMinimoInvalido"));
        }

        // CUN010: verifica la seleccion y devuelve el producto actualizado.
       
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
