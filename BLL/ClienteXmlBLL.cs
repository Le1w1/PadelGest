using BE;
using Servicios;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BLL
{
    // Servicio XML del Maestro de Clientes. No utiliza DAL ni SQL Server.
    // La lista se entrega desde el DataGridView de la interfaz.
    public class ClienteXmlBLL
    {
        private static readonly XmlSerializer Serializador =
            new XmlSerializer(typeof(List<ClienteBE>));

        public void Serializar(List<ClienteBE> clientes, string ruta)
        {
            SM.Instancia.RequierePermiso("CLI_MODIFICAR");
            if (clientes == null)
                throw new ArgumentNullException(nameof(clientes));
            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Seleccione un archivo XML de destino.");

            // Se guarda exactamente la lista suministrada por la pantalla.
            using FileStream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write);
            using XmlWriter escritor = XmlWriter.Create(archivo, new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                CloseOutput = false
            });
            Serializador.Serialize(escritor, clientes);
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

            return clientes;
        }
    }
}
