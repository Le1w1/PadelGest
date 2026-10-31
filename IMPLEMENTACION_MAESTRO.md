# Nuevo rol Maestro - rama feature/maestro-abm

Se conserva el esquema actual de Cliente sin campo Activo.

1. Hacer respaldo de la base de datos.
2. Ejecutar `Script/PermisosMaestro.sql`.
3. Recalcular los digitos verificadores de PermisoSimple en el flujo administrativo de reparacion antes de usar el nuevo permiso. Si la comprobacion de integridad bloquea el inicio, ingresar con la cuenta Administrador y usar la reparacion.
4. Desde Gestion de Roles y Familias crear el rol **Maestro** con los permisos indicados en el SQL. No asignar `BUF_VENDER`.
5. Mantener al Vendedor exclusivamente con el permiso de venta `BUF_VENDER` y los permisos comunes de sesion. **No** asignarle `CLI_REGISTRAR`, `CLI_MODIFICAR`, `CLI_ELIMINAR` ni `PRO_GESTIONAR`.
6. La facturacion continua consultando el DNI mediante `frmSeleccionarCliente`. Si el cliente no existe, se debe registrar previamente desde el perfil Maestro.
7. Verificar los casos con usuario Maestro y usuario Vendedor y validar los DV y bitacora.

## Cambios
- `frmGestionClientes`: listado, alta (reutiliza BLL), modificacion y baja fisica solo si no existen facturas ni reservas.
- `frmGestionProductos`: reutilizado, protegido con `PRO_GESTIONAR`.
- `frmSeleccionarCliente`: conserva busqueda por DNI, sin alta durante la venta.
- La BLL aplica los controles de autorizacion adicionales.
- No se edita el script original `PadelGestDB.sql`, pues contiene valores de DV calculados para su conjunto de datos inicial.

## Pendiente de verificacion
Compilar en Windows/.NET 9 y ejecutar pruebas funcionales y de permisos en SQL Server. No se ha realizado esa verificacion desde el conector.
