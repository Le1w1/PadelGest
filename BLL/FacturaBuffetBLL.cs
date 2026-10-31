using System;
using System.Linq;
using BE;
using DAL;
using Servicios;

namespace BLL
{
    // La factura se prepara en memoria; solo el cobro aprobado se persiste.
    public class FacturaBuffetBLL
    {
        private readonly ProductoBLL _productoBLL = new ProductoBLL();
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();
        private readonly FacturaBuffetDAL _facturaDAL = new FacturaBuffetDAL();

        //vendedor actual de la sesión activa; lanza excepción si no hay sesión activa o no tiene permiso de venta
        private static int Vendedor()
        {
            SM.Instancia.RequierePermiso("BUF_VENDER");
            Usuario usuario = SM.Instancia.UsuarioActual;
            if (usuario == null || usuario.IdUsuario <= 0)
                throw new Exception("No hay vendedor con sesión activa.");
            return usuario.IdUsuario;
        }

        //valida que la factura sea del vendedor actual; lanza excepción si no lo es
        private static void Propietario(FacturaBuffetBE factura)
        {
            if (factura == null || factura.IdVendedor != Vendedor())
                throw new Exception("Factura inválida o de otro vendedor.");
        }

        //valida que la factura sea editable; lanza excepción si no lo es
        private static void Editable(FacturaBuffetBE factura)
        {
            Propietario(factura);
            if (factura.IdFactura != 0 || factura.Estado != "EnPreparacion")
                throw new Exception("La factura no admite cambios.");
        }

        //genera una nueva factura en memoria, sin persistirla
        public FacturaBuffetBE GenerarFactura()
        {
            return new FacturaBuffetBE
            {
                IdVendedor = Vendedor(),
                FechaHoraEmision = DateTime.Now,
                Estado = "EnPreparacion"
            };
        }


        //agrega un producto a la factura en memoria; si ya estaba, suma la cantidad
        public FacturaBuffetBE AgregarProducto(FacturaBuffetBE factura, ProductoBE producto, int cantidad)
        {
            Editable(factura);
            if (producto == null) throw new Exception("Producto inválido.");
            // busca el detalle del producto en la factura; si no existe, lo crea; si existe, suma la cantidad
            DetalleFacturaBE detalle = factura.Detalles.FirstOrDefault(x => x.CodigoProducto == producto.CodigoProducto);
            int total = checked(cantidad + (detalle == null ? 0 : detalle.Cantidad));
            ValidarProductoEnMemoria(producto, total);
          
            if (detalle == null)
                factura.Detalles.Add(new DetalleFacturaBE
                {
                    CodigoProducto = producto.CodigoProducto,
                    NombreProducto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.PrecioUnitario
                });
            else detalle.Cantidad = total;
            CalcularTotal(factura);
            return factura;
        }


        //modifica la cantidad de un producto ya agregado a la factura en memoria
        public FacturaBuffetBE ModificarCantidad(FacturaBuffetBE factura, ProductoBE producto, int cantidad)
        {
            Editable(factura);
            if (producto == null) throw new Exception("Producto inválido.");
            DetalleFacturaBE detalle = factura.Detalles.FirstOrDefault(x => x.CodigoProducto == producto.CodigoProducto);
            if (detalle == null) throw new Exception("Producto no incluido.");
            ValidarProductoEnMemoria(producto, cantidad);
            detalle.Cantidad = cantidad;
            CalcularTotal(factura);
            return factura;
        }

        // Valida el catálogo ya cargado, sin consultar SQL por cada selección.
        // Antes de cobrar se vuelve a verificar el stock real en la base.
        private static void ValidarProductoEnMemoria(ProductoBE producto, int cantidad)
        {
            if (producto.CodigoProducto <= 0) throw new Exception("Código de producto inválido.");
            if (cantidad <= 0) throw new Exception("Cantidad inválida.");
            if (!producto.Activo) throw new Exception("Producto no disponible.");
            if (producto.PrecioUnitario <= 0) throw new Exception("Precio inválido.");
            if (producto.StockDisponible < cantidad) throw new Exception("Stock insuficiente.");
        }

        //quita un producto de la factura en memoria
        public FacturaBuffetBE QuitarProducto(FacturaBuffetBE factura, int codigoProducto)
        {
            Editable(factura);
            if (factura.Detalles.RemoveAll(x => x.CodigoProducto == codigoProducto) == 0)
                throw new Exception("Producto no incluido.");
            CalcularTotal(factura);
            return factura;
        }


        //asocia un cliente a la factura en memoria; si no existe, lanza excepción
        public FacturaBuffetBE AsociarCliente(FacturaBuffetBE factura, string dni)
        {
            Editable(factura);
            if (factura.Detalles.Count == 0) throw new Exception("La factura está vacía.");
            ClienteBE cliente = _clienteBLL.BuscarPorDNI(dni);
            if (cliente == null) throw new Exception("Cliente no registrado.");
            factura.DNI = cliente.DNI;
            factura.Estado = "Listo";
            return factura;
        }


        //calcula el total de la factura en memoria; lanza excepción si el total es mayor a 9999999999.99
        private static void CalcularTotal(FacturaBuffetBE factura)
        {
            decimal total = factura.Detalles.Aggregate(0m, (s, d) => checked(s + d.Subtotal));
            if (total > 9999999999.99m) throw new Exception("Total fuera de rango.");
            factura.ImporteTotal = total;
        }


        //valida que la factura esté lista para cobrar; lanza excepción si no lo está
        public void ValidarAntesDeCobrar(FacturaBuffetBE factura)
        {
            Propietario(factura);
            if (factura.IdFactura != 0 || factura.Estado != "Listo" ||
                string.IsNullOrEmpty(factura.DNI) || factura.Detalles.Count == 0 || factura.ImporteTotal <= 0)
                throw new Exception("Factura no lista para cobrar.");
            if (_clienteBLL.BuscarPorDNI(factura.DNI) == null)
                throw new Exception("El cliente no existe.");
            decimal total = 0;

            foreach (DetalleFacturaBE d in factura.Detalles)
            {
                if (d.Cantidad <= 0 || d.PrecioUnitario <= 0)
                    throw new Exception("Detalle inválido.");
                _productoBLL.SeleccionarProducto(d.CodigoProducto, d.Cantidad);
                total = checked(total + d.Subtotal);
            }

            if (total != factura.ImporteTotal) throw new Exception("Total inconsistente.");
        }


        //registra la factura en la base de datos, junto con el pago aprobado; devuelve la factura con los datos actualizados
        public FacturaBuffetBE RegistrarFactura(FacturaBuffetBE factura, PagoBE pago)
        {
            Propietario(factura);
            if (factura.Estado != "Listo" && factura.Estado != "Pagada")
                throw new Exception("Factura no lista.");
            if (pago == null || pago.Estado != "Aprobado" ||string.IsNullOrWhiteSpace(pago.CodigoAutorizacion) ||string.IsNullOrWhiteSpace(pago.Banco) ||
                string.IsNullOrWhiteSpace(pago.Ultimos4Tarjeta) ||pago.Ultimos4Tarjeta.Length != 4 ||pago.Importe <= 0 || pago.Importe != factura.ImporteTotal)
                throw new Exception("Pago aprobado inválido.");
            
            FacturaBuffetBE resultado = _facturaDAL.RegistrarFactura(factura, SM.Instancia.UsuarioActual.NombreUsuario, pago);
            factura.IdFactura = resultado.IdFactura;
            factura.NumeroFactura = resultado.NumeroFactura;
            factura.FechaHoraEmision = resultado.FechaHoraEmision;
            factura.Estado = resultado.Estado;
            factura.Banco = resultado.Banco;
            factura.Ultimos4Tarjeta = resultado.Ultimos4Tarjeta;
            factura.FechaHoraPago = resultado.FechaHoraPago;
            factura.CodigoAutorizacion = resultado.CodigoAutorizacion;
            return resultado;
        }
    }
}
