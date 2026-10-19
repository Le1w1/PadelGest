using DAL.Servicios;
using Servicios.DigitoVerificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    /// Logica de negocio del Digito Verificador. Orquesta las tres piezas:
    ///   - DigitoVerificador (Servicios): la formula (concatenar + hashear).
    ///   - TablasProtegidas (Servicios): que columnas y PK tiene cada tabla.
    ///   - DigitoVerificadorDAL (DAL): leer/escribir DVH por fila y DVV por tabla.

    /// Expone dos operaciones
    ///   - RecalcularDV(tabla): modo GENERACION. Recalcula y persiste el DVH de
    ///     cada fila y el DVV de la tabla. Se llama despues de cada escritura.
    ///   - VerificarIntegridad(): modo DETECCION. Compara contra lo guardado y
    ///     distingue registros modificados, agregados y eliminados.

    public class DigitoVerificadorBLL
    {
        private readonly DigitoVerificadorDAL _dvDAL;
        private readonly DigitoVerificador _digitoVerificador;

        public DigitoVerificadorBLL()
        {
            _dvDAL = new DigitoVerificadorDAL();
            _digitoVerificador = new DigitoVerificador();
        }

        // MODO GENERACION
        /// Recalcula y persiste los Digitos Verificadores de UNA tabla:
        /// el DVH de cada fila y el DVV de la tabla. Se invoca despues de cada
        /// alta/modificacion/baja sobre esa tabla.
        public void RecalcularDV(string nombreTabla)
        {
            TablaProtegida tabla = ObtenerTabla(nombreTabla);
            var registros = _dvDAL.LeerRegistros(tabla);

            var dvhCalculados = new List<string>();

            foreach (var registro in registros)
            {
                // DVH de la fila: se calcula con las columnas de datos EN ORDEN.
                string dvh = CalcularDVHDeRegistro(tabla, registro);

                // Persistir el DVH en la fila, identificada por su PK.
                var valoresPk = ExtraerPk(tabla, registro);
                _dvDAL.ActualizarDVH(tabla, dvh, valoresPk);

                dvhCalculados.Add(dvh);
            }

            // DVV de la tabla: hash de la concatenacion de los DVH ordenados por PK.
            string dvv = _digitoVerificador.CalcularDVV(dvhCalculados);
            _dvDAL.GuardarDVV(tabla.Nombre, dvv);
        }

        /// Recalcula el DV de TODAS las tablas protegidas. Util para el arranque
        /// inicial del sistema de cero o para la reparacion por recalculo.
        public void RecalcularTodo()
        {
            foreach (TablaProtegida tabla in TablasProtegidas.Todas)
                RecalcularDV(tabla.Nombre);
        }

        // MODO DETECCION
        /// Verifica la integridad de todas las tablas protegidas.
        ///
        /// Clasificacion utilizada:
        ///   - DVH vacio/NULL: registro agregado por fuera del sistema.
        ///   - DVH guardado distinto del DVH recalculado: registro modificado.
        ///   - El DVV formado con los DVH GUARDADOS de los registros originales
        ///     que aun existen no coincide con el DVV persistido: hubo una baja.
        ///
        /// Para detectar la baja se excluyen primero los registros agregados.
        /// Esto evita mostrar una falsa baja cuando el unico cambio fue un alta.
        /// Tambien permite detectar simultaneamente un alta y una baja.
        public List<Inconsistencia> VerificarIntegridad()
        {
            var inconsistencias = new List<Inconsistencia>();

            foreach (TablaProtegida tabla in TablasProtegidas.Todas)
            {
                string dvvGuardado = _dvDAL.LeerDVV(tabla.Nombre);

                // Base sin DV generado: no es corrupcion, se saltea la tabla.
                if (dvvGuardado == null)
                    continue;

                var registros = _dvDAL.LeerRegistros(tabla);

                // Conservamos los DVH que ya existian antes de la alteracion.
                // Un registro agregado manualmente normalmente llega sin DVH,
                // por lo que no debe participar de esta reconstruccion del DVV original.
                var dvhGuardadosRegistrosExistentes = new List<string>();

                foreach (var registro in registros)
                {
                    string identificadorPk = ConstruirIdentificadorPk(tabla, registro);
                    string dvhCalculado = CalcularDVHDeRegistro(tabla, registro);

                    string dvhGuardado = registro["DVH"] == null || registro["DVH"] == System.DBNull.Value? null: registro["DVH"].ToString();

                    // Sin DVH guardado: la fila no paso por el mecanismo normal
                    // de alta del sistema, por lo que se considera agregada externamente.
                    if (string.IsNullOrWhiteSpace(dvhGuardado))
                    {
                        inconsistencias.Add(new Inconsistencia
                        {
                            Tabla = tabla.Nombre,
                            Tipo = Inconsistencia.TipoInconsistencia.RegistroAgregado,
                            IdentificadorPk = identificadorPk
                        });

                        continue;
                    }

                    // El DVH guardado representa el estado valido previo de esta fila.
                    // Se usa para verificar si el conjunto original de registros sigue completo.
                    dvhGuardadosRegistrosExistentes.Add(dvhGuardado);

                    // Si la fila sigue existiendo pero sus datos ya no producen el mismo DVH,
                    // fue modificada por fuera del sistema.
                    if (dvhCalculado != dvhGuardado)
                    {
                        inconsistencias.Add(new Inconsistencia
                        {
                            Tabla = tabla.Nombre, Tipo = Inconsistencia.TipoInconsistencia.RegistroModificado,IdentificadorPk = identificadorPk
                        });
                    }
                }

                // IMPORTANTE: para detectar bajas usamos los DVH GUARDADOS, no los
                // recalculados. Asi una simple modificacion de datos no se confunde con
                // una eliminacion. Los registros agregados ya fueron excluidos arriba.
                string dvvRegistrosQueDeberianSeguir = _digitoVerificador.CalcularDVV(dvhGuardadosRegistrosExistentes);

                if (dvvRegistrosQueDeberianSeguir != dvvGuardado)
                {
                    inconsistencias.Add(new Inconsistencia
                    {
                        Tabla = tabla.Nombre, Tipo = Inconsistencia.TipoInconsistencia.RegistroEliminado, IdentificadorPk = string.Empty
                    });
                }
            }

            return inconsistencias;
        }

        // AUXILIARES
        /// Calcula el DVH de un registro tomando sus columnas de datos EN EL
        /// ORDEN definido en TablaProtegida. Es el mismo metodo usado al generar
        /// y al verificar: por eso nunca se desincronizan.
        private string CalcularDVHDeRegistro(TablaProtegida tabla, Dictionary<string, object> registro)
        {
            var valores = tabla.ColumnasDatos.Select(col => registro[col]);
            return _digitoVerificador.CalcularDVH(valores);
        }

        /// Extrae del registro solo las columnas que forman la PK.
        private Dictionary<string, object> ExtraerPk(TablaProtegida tabla, Dictionary<string, object> registro)
        {
            var pk = new Dictionary<string, object>();
            foreach (string colPk in tabla.ColumnasPk) 
                pk[colPk] = registro[colPk];
            return pk;
        }

        /// Arma el identificador legible de la PK para reportar la fila.
        /// Ej: "IdUsuario=5" o "IdRol=3, IdFamilia=7".
        private string ConstruirIdentificadorPk(TablaProtegida tabla, Dictionary<string, object> registro)
        {
            var partes = tabla.ColumnasPk.Select(col => col + "=" + registro[col]);
            return string.Join(", ", partes);
        }

        /// Busca la definicion de una tabla protegida por nombre.
        private TablaProtegida ObtenerTabla(string nombreTabla)
        {
            TablaProtegida tabla = TablasProtegidas.Todas.FirstOrDefault(t => t.Nombre == nombreTabla);

            if (tabla == null)
                throw new System.Exception("La tabla '" + nombreTabla + "' no esta registrada como tabla protegida.");

            return tabla;
        }
    }
}
