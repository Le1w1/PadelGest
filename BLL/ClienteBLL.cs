using BE;
using DAL;
using BLL.Servicios;
using Servicios;
using System.Text.RegularExpressions;

namespace BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDAL;
        private readonly DigitoVerificadorBLL _digitoVerificadorBLL;
        private readonly BitacoraEventoBLL _bitacoraEventoBLL;

        public ClienteBLL()
        {
            _clienteDAL = new ClienteDAL();
            _digitoVerificadorBLL = new DigitoVerificadorBLL();
            _bitacoraEventoBLL = new BitacoraEventoBLL();
        }

        private static string T (string clave) => Traductor.Instancia.Traducir(clave);

        /// Busca un Cliente por DNI luego de validar el dato ingresado.
        public ClienteBE? BuscarPorDNI(string dni)
        {
            SM.Instancia.RequierePermiso("CLI_CONSULTAR");

            dni = (dni ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new Exception(T("Errores.Cliente.DNIObligatorio"));
            }

            if (!EsDNIValido(dni))
            {
                throw new Exception(T("Errores.Cliente.DNIInvalido"));
            }

            return _clienteDAL.BuscarPorDNI(dni);
        }


        /// Registra un nuevo Cliente luego de validar todos los datos ingresados.
        public ClienteBE RegistrarCliente(string dni,string nombre,string apellido,string telefono,string correoElectronico)
        {
            SM.Instancia.RequierePermiso("CLI_REGISTRAR");

            Usuario usuarioActual = SM.Instancia.UsuarioActual;
            if (usuarioActual == null)
            {
                throw new Exception("No hay un usuario activo para registrar el cliente.");
            }

            dni = (dni ?? string.Empty).Trim();
            nombre = (nombre ?? string.Empty).Trim();
            apellido = (apellido ?? string.Empty).Trim();
            telefono = (telefono ?? string.Empty).Trim();
            correoElectronico = (correoElectronico ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(dni) ||string.IsNullOrWhiteSpace(nombre) ||string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(telefono) ||string.IsNullOrWhiteSpace(correoElectronico))
            {
                throw new Exception(T("Errores.Cliente.CamposObligatorios"));
            }

            if (!EsDNIValido(dni))
            {
                throw new Exception(T("Errores.Cliente.DNIInvalido"));
            }

            if (!EsNombreOApellidoValido(nombre))
            {
                throw new Exception(T("Errores.Cliente.NombreInvalido"));
            }

            if (!EsNombreOApellidoValido(apellido))
            {
                throw new Exception(T("Errores.Cliente.ApellidoInvalido"));
            }

            if (!EsTelefonoValido(telefono))
            {
                throw new Exception(T("Errores.Cliente.TelefonoInvalido"));
            }

            if (!EsCorreoValido(correoElectronico))
            {
                throw new Exception(T("Errores.Cliente.CorreoInvalido"));
            }

            if (_clienteDAL.ExistePorDNI(dni))
            {
                throw new Exception(T("Errores.Cliente.DNIYaRegistrado"));
            }

            ClienteBE cliente = new ClienteBE
            {
                DNI = dni,
                Nombre = nombre,
                Apellido = apellido,
                Telefono = telefono,
                CorreoElectronico = correoElectronico
            };

            ClienteBE clienteRegistrado = _clienteDAL.Insertar(cliente);

            // Cliente es una tabla protegida: toda escritura debe regenerar DVH/DVV.
            _digitoVerificadorBLL.RecalcularDV("Cliente");

            _bitacoraEventoBLL.Registrar(usuarioActual.IdUsuario,usuarioActual.NombreUsuario,"Cliente","Registrar cliente","Media","Exitoso",$"Se registró el cliente DNI {clienteRegistrado.DNI} con IdCliente {clienteRegistrado.IdCliente}.");

            return clienteRegistrado;
        }

        #region Validaciones
        private bool EsDNIValido(string dni)
        {
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        private bool EsNombreOApellidoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[\p{L}\s'-]{2,50}$");
        }

        private bool EsTelefonoValido(string telefono)
        {
            return Regex.IsMatch(telefono, @"^\d{8,15}$");
        }

        private bool EsCorreoValido(string correoElectronico)
        {
            return Regex.IsMatch(correoElectronico,@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        #endregion
    }
}
