using BE;
using DAL;
using Servicios;

namespace BLL
{
    // Orquesta CUN13 / CUN14, una vez recibida la autorizacion de CUN12.
    public class VentaBLL
    {
        private readonly VentaDAL _ventaDAL;

        public VentaBLL()
        {
            _ventaDAL = new VentaDAL();
        }

        public void ValidarAntesDeCobrar(int idCarrito)
        {
            int vendedor = ObtenerVendedor();
            _ventaDAL.ValidarDisponibilidadParaCobrar(idCarrito, vendedor);
        }

        public VentaBE RegistrarVenta(int idCarrito, PagoBE pago)
        {
            int idVendedor = ObtenerVendedor();

            if (pago == null || pago.Estado != "Aprobado" ||
                string.IsNullOrWhiteSpace(pago.CodigoAutorizacion) ||
                string.IsNullOrWhiteSpace(pago.Banco) || pago.Importe <= 0 ||
                pago.Ultimos4Tarjeta.Length != 4)
                throw new Exception("No existe un pago aprobado valido para registrar la venta.");

            // Reintento idempotente: si ya se registro este codigo, se reutiliza.
            VentaBE? existente = _ventaDAL.BuscarPorAutorizacion(
                idCarrito, idVendedor, pago.CodigoAutorizacion);

            if (existente != null)
                return existente;

            Usuario usuario = SM.Instancia.UsuarioActual;
            return _ventaDAL.RegistrarVenta(idCarrito, idVendedor, usuario.NombreUsuario, pago);
        }

        private static int ObtenerVendedor()
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");

            Usuario usuario = SM.Instancia.UsuarioActual;
            if (usuario == null || usuario.IdUsuario <= 0)
                throw new Exception("No hay un vendedor con sesion activa.");

            return usuario.IdUsuario;
        }
    }
}
