namespace Servicios.DigitoVerificador
{
    /// Representa una inconsistencia de integridad detectada por el Digito Verificador.
    ///
    /// Se distinguen tres casos:
    ///   - RegistroModificado: el registro existe, tiene DVH guardado y el DVH recalculado no coincide.
    ///   - RegistroAgregado: el registro existe pero no tiene DVH generado por el sistema.
    ///   - RegistroEliminado: el conjunto esperado de DVH ya no coincide, una vez descartadas las altas detectadas.
    public class Inconsistencia
    {
        public enum TipoInconsistencia
        {
            RegistroModificado,
            RegistroAgregado,
            RegistroEliminado
        }

        /// Tabla afectada.
        public string Tabla { get; set; }

        /// Tipo de inconsistencia detectada.
        public TipoInconsistencia Tipo { get; set; }

        /// Identificador del registro afectado cuando puede determinarse por su PK.
        /// Ej: "IdUsuario=5" o "IdRol=3, IdFamilia=7".
        /// Para una baja externa queda vacio porque el registro ya no existe en la tabla.
        public string IdentificadorPk { get; set; }

        public override string ToString()
        {
            return Tipo switch
            {
                TipoInconsistencia.RegistroAgregado =>
                    "Tabla " + Tabla + ", registro " + IdentificadorPk + ": fue agregado fuera del sistema.",

                TipoInconsistencia.RegistroEliminado =>
                    "Tabla " + Tabla + ": se detecto al menos un registro eliminado fuera del sistema.",

                _ =>
                    "Tabla " + Tabla + ", registro " + IdentificadorPk + ": fue modificado fuera del sistema."
            };
        }
    }
}
