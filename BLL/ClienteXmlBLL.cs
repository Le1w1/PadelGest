using BE;
using BLL.Servicios;
using Servicios;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BLL
{
    // Servicio XML del Maestro de Clientes.
    // Los datos se leen de la grilla y del archivo XML, no de la tabla Cliente.
    // Solo persiste en SQL Server la auditoria de la accion.
    public class ClienteXmlBLL
    {
        private static readonly XmlSerializer Serializador =
            new XmlSerializer(typeof(List<ClienteBE>));

        private readonly BitacoraEventoBLL _bitacora = new BitacoraEventoBLL();

        private void RegistrarOperacion(string accion, int cantidad)
        {
            var usuario = SM.Instancia.UsuarioActual;
            if (usuario == null)
                throw new InvalidOperationException("No hay una sesion activa para registrar la operacion XML.");

            // No guardar DNI, correo, telefono ni la ruta completa del archivo.
            _bitacora.Registrar(usuario.IdUsuario, usuario.NombreUsuario,
                "Cliente", accion, "Media", "Exitoso",
                $"Se procesaron {cantidad} clientes mediante la operacion XML. " +
                "No se modificaron registros de la tabla Cliente.");
        }

        public void Serializar(List<ClienteBE> clientes, string ruta)
        {
            SM.Instancia.RequierePermiso("CLI_MODIFICAR");
            if (clientes == null)
                throw new ArgumentNullException(nameof(clientes));
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Seleccione un archivo XML de destino.");

            // Se guarda exactamente la lista suministrada por la pantalla.
            // Los using terminan antes del evento: solo se registra si la
            // escritura y el cierre del archivo fueron correctos.
            // Nunca exportar un telefono en claro si el usuario no es Administrador.
            List<ClienteBE> clientesExportar = clientes.Select(cliente => CopiarConTelefonoVisible(cliente)).ToList();

            using (FileStream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write))
            using (XmlWriter escritor = XmlWriter.Create(archivo, new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                CloseOutput = false
            }))
            {
                Serializador.Serialize(escritor, clientesExportar);
            }

            RegistrarOperacion("Serializar clientes", clientes.Count);
        }

        public List<ClienteBE> Deserializar(string ruta)
        {
            SM.Instancia.RequierePermiso("CLI_MODIFICAR");
            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                throw new FileNotFoundException("Seleccione un archivo XML existente.", ruta);

            // Evitar XML externos (XXE) y archivos excesivamente grandes.
            using FileStream archivo = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (archivo.Length > 5_000_000)
                throw new InvalidOperationException("El XML supera el limite de 5 MB.");

            using XmlReader lector = XmlReader.Create(archivo, new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
                MaxCharactersInDocument = 5_000_000,
                CloseInput = false
            });
            if (Serializador.Deserialize(lector) is not List<ClienteBE> clientes)
                throw new InvalidOperationException("El archivo no contiene una lista valida de clientes.");

            foreach (ClienteBE cliente in clientes)
            {
                if (cliente == null ||
                    string.IsNullOrWhiteSpace(cliente.DNI) ||
                    string.IsNullOrWhiteSpace(cliente.Nombre) ||
                    string.IsNullOrWhiteSpace(cliente.Apellido) ||
                    string.IsNullOrWhiteSpace(cliente.Telefono) ||
                    string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
                    throw new InvalidOperationException("El archivo XML contiene clientes incompletos.");
            }

            // El XML tambien se presenta segun el rol que inicio sesion.
            List<ClienteBE> clientesVisibles = clientes.Select(cliente => CopiarConTelefonoVisible(cliente)).ToList();

            RegistrarOperacion("Deserializar clientes", clientes.Count);
            return clientesVisibles;
        }

        private static ClienteBE CopiarConTelefonoVisible(ClienteBE cliente)
        {
            string telefono = cliente.Telefono;
            if (SM.Instancia.PuedeDesencriptarTelefono())
                telefono = CriptografiaReversible.Desencriptar(telefono);
            else if (!CriptografiaReversible.EstaEncriptado(telefono))
                telefono = CriptografiaReversible.Encriptar(telefono);

            return new ClienteBE
            {
                DNI = cliente.DNI,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = telefono,
                CorreoElectronico = cliente.CorreoElectronico
            };
        }
    }
}
