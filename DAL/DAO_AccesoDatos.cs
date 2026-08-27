using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL
{
    public class DAO_AccesoDatos
    {
        private readonly string _cadenaConexion;

        // La eleccion se hace en tiempo de instalacion via variable de entorno
        // PADELGEST_ENTORNO ("UAI" "SEBA" o "LEO").
        private const string CADENA_LEO =
         @"Data Source=localhost\SQLEXPRESS;Initial Catalog=PadelGestDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        private const string CADENA_UAI =
         @"Data Source=.;Initial Catalog=PadelGestDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        

        public DAO_AccesoDatos()
        {
            var entorno = Environment.GetEnvironmentVariable("PADELGEST_ENTORNO");
            // Si la variable de entorno no está definida o está vacía, usar "LEO" como valor por defecto
            var entornoNormalizado = (string.IsNullOrWhiteSpace(entorno) ? "LEO" : entorno).ToUpperInvariant();

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
