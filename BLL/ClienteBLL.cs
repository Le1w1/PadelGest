using BE;
using DAL;
using Servicios;
using System.Text.RegularExpressions;

namespace BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDAL;

        public ClienteBLL()
        {
            _clienteDAL = new ClienteDAL();
        }

        private static string T(string clave) =>
            Traductor.Instancia.Traducir(clave);

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

        private bool EsDNIValido(string dni)
        {
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }
    }
}
