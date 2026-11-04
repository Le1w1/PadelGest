using System.Security.Cryptography;
using System.Text;

namespace Servicios
{
    // Encriptacion RESEVERSIBLE sencilla para el telefono de los clientes.
    public static class CriptografiaReversible
    {
        // No modificar esta clave mientras existan telefonos encriptados en la base.
        private const string CLAVE = "PadelGest-AES-Telefono-2026-Entrega";
        private const string PREFIJO = "ENC1:";
        private const int TAMANO_NONCE = 12;
        private const int TAMANO_TAG = 16;

        private static readonly byte[] Clave = SHA256.HashData(Encoding.UTF8.GetBytes(CLAVE));

        public static string Encriptar(string texto)
        {
            if (string.IsNullOrEmpty(texto) || EstaEncriptado(texto))
                return texto;

            byte[] nonce = RandomNumberGenerator.GetBytes(TAMANO_NONCE);
            byte[] datos = Encoding.UTF8.GetBytes(texto);
            byte[] cifrado = new byte[datos.Length];
            byte[] tag = new byte[TAMANO_TAG];

            using (AesGcm aes = new AesGcm(Clave, TAMANO_TAG))
            {
                aes.Encrypt(nonce, datos, cifrado, tag);
            }

            // Se guarda nonce + tag + texto cifrado para poder desencriptarlo despues.
            byte[] resultado = new byte[nonce.Length + tag.Length + cifrado.Length];
            Buffer.BlockCopy(nonce, 0, resultado, 0, nonce.Length);
            Buffer.BlockCopy(tag, 0, resultado, nonce.Length, tag.Length);
            Buffer.BlockCopy(cifrado, 0, resultado, nonce.Length + tag.Length, cifrado.Length);

            return PREFIJO + Convert.ToBase64String(resultado);
        }

        public static string Desencriptar(string texto)
        {
            // Permite leer los telefonos que ya estaban guardados antes de incorporar el cifrado.
            if (string.IsNullOrEmpty(texto) || !EstaEncriptado(texto))
                return texto;

            byte[] datos = Convert.FromBase64String(texto.Substring(PREFIJO.Length));
            if (datos.Length < TAMANO_NONCE + TAMANO_TAG)
                throw new CryptographicException("El telefono encriptado no tiene un formato valido.");

            byte[] nonce = new byte[TAMANO_NONCE];
            byte[] tag = new byte[TAMANO_TAG];
            byte[] cifrado = new byte[datos.Length - TAMANO_NONCE - TAMANO_TAG];
            byte[] original = new byte[cifrado.Length];

            Buffer.BlockCopy(datos, 0, nonce, 0, nonce.Length);
            Buffer.BlockCopy(datos, nonce.Length, tag, 0, tag.Length);
            Buffer.BlockCopy(datos, nonce.Length + tag.Length, cifrado, 0, cifrado.Length);

            using (AesGcm aes = new AesGcm(Clave, TAMANO_TAG))
            {
                aes.Decrypt(nonce, cifrado, tag, original);
            }

            return Encoding.UTF8.GetString(original);
        }

        public static bool EstaEncriptado(string texto)
        {
            return texto != null && texto.StartsWith(PREFIJO, StringComparison.Ordinal);
        }
    }
}