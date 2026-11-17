using System;
using System.Diagnostics;
using BLL.Servicios;
using Servicios;

namespace UI.Servicios
{
    // Registra las exportaciones PDF sin modificar ni revertir la operación
    // comercial que ya fue persistida en la base de datos.
    internal static class AuditoriaExportacionPdf
    {
        public static void Registrar(string modulo, string referencia, bool exitoso)
        {
            try
            {
                Usuario usuarioActual = SM.Instancia.UsuarioActual;
                int idUsuario = usuarioActual != null ? usuarioActual.IdUsuario : 0;
                string usuario = usuarioActual != null
                    ? usuarioActual.NombreUsuario : "Sistema";

                string descripcion = exitoso ? "Se exportó a PDF el comprobante de " + modulo + " " + referencia + "." : "Falló la exportación a PDF del comprobante de " + modulo + " " + referencia + ".";

                new BitacoraEventoBLL().Registrar(idUsuario, usuario, modulo, "Imprimir PDF", "Media",exitoso ? "Exitoso" : "Fallido", descripcion);
            }
            catch (Exception ex)
            {
                // El PDF y el registro de la reserva/venta no dependen de esta
                // escritura adicional en Bitácora.
                Debug.WriteLine("No se pudo auditar la exportación PDF: " + ex.Message);
            }
        }
    }
}
