using Microsoft.Data.SqlClient;
using Microsoft.Win32;

namespace DAL.Servicios
{
    public class DAO_AccesoDatos
    {
        private readonly string _cadenaConexion;

        // La eleccion de SQL Server se guarda por usuario durante la instalacion
        // en HKCU\\Environment\\PADELGEST_ENTORNO ("UAI" o "LEO").
        // Se conserva la variable de entorno como alternativa para
        // instalaciones anteriores y ejecucion directa desde Visual Studio.
        private const string CADENA_LEO =
         @"Data Source=localhost\SQLEXPRESS;Initial Catalog=PadelGestDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        private const string CADENA_UAI =
         @"Data Source=.;Initial Catalog=PadelGestDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        

        public DAO_AccesoDatos()
        {
            // Leer primero la configuracion persistida de este usuario.
            // No depender de que Windows haya propagado variables de entorno
            // a procesos nuevos tras una instalacion sin elevacion.
            string? entorno = null;
            try
            {
                using RegistryKey? clave = Registry.CurrentUser.OpenSubKey("Environment");
                entorno = clave?.GetValue("PADELGEST_ENTORNO") as string;
            }
            catch (System.Security.SecurityException)
            {
                // En contextos restringidos, usar la variable de entorno.
            }
            catch (UnauthorizedAccessException)
            {
                // En contextos restringidos, usar la variable de entorno.
            }
            if (string.IsNullOrWhiteSpace(entorno))
                entorno = Environment.GetEnvironmentVariable("PADELGEST_ENTORNO");
            var entornoNormalizado = (string.IsNullOrWhiteSpace(entorno) ? "UAI" : entorno.Trim()).ToUpperInvariant();

            _cadenaConexion = entornoNormalizado switch
            {
                "UAI" => CADENA_UAI,
                "LEO" => CADENA_LEO,
                _ => throw new InvalidOperationException($"Variable de entorno PADELGEST_ENTORNO con valor inválido: '{entornoNormalizado}'. Valores válidos: 'UAI', 'LEO'. Contacte al administrador.")
            };
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }

        /// Devuelve una conexion apuntando a la base [master] en lugar de PadelGestDB.
        /// Se usa exclusivamente para las operaciones BACKUP DATABASE y RESTORE DATABASE,
        /// que no pueden ejecutarse desde una conexion contra la propia base a restaurar.
        /// Reutiliza los demas parametros de la cadena original (Data Source, seguridad,
        /// encriptacion) cambiando solo el Initial Catalog.
        public SqlConnection ObtenerConexionMaster()
        {
            // Se construye una nueva cadena a partir de la original, cambiando solo el Initial Catalog
            var builder = new SqlConnectionStringBuilder(_cadenaConexion);
            builder.InitialCatalog = "master";
            return new SqlConnection(builder.ConnectionString);
        }


        /// Devuelve el nombre de la base de datos objetivo (PadelGestDB).
        /// Necesario para armar comandos BACKUP/RESTORE que referencian a la base por nombre.
        /// Se obtiene de la cadena para que si algun dia se parametriza la cadena
        /// (App.config, instalador), este metodo siga funcionando sin cambios.
        public string ObtenerNombreBaseDatos()
        {
            var builder = new SqlConnectionStringBuilder(_cadenaConexion);
            return builder.InitialCatalog;
        }
    }
}
