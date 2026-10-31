-- Ejecutar una sola vez en PadelGestDB, con un respaldo previo.
-- No altera la estructura ni los datos de la tabla Cliente.
-- IMPORTANTE: Los permisos son tablas protegidas por DVH/DVV.
-- Despues de ejecutar, recalcular DV de PermisoSimple desde la
-- funcionalidad de reparacion del Administrador ANTES de operar normalmente.
IF NOT EXISTS (SELECT 1 FROM dbo.PermisoSimple WHERE Codigo = 'PRO_GESTIONAR')
    INSERT dbo.PermisoSimple (Codigo, Nombre, DVH)
    VALUES (N'PRO_GESTIONAR', N'Gestionar Productos (ABM)', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.PermisoSimple WHERE Codigo = 'CLI_ELIMINAR')
    INSERT dbo.PermisoSimple (Codigo, Nombre, DVH)
    VALUES (N'CLI_ELIMINAR', N'Eliminar Cliente sin operaciones asociadas', NULL);
GO

-- En Gestion de Roles y Familias, crear el Rol "Maestro" con:
-- SES_CAMBIAR_CLAVE, SES_CAMBIAR_IDIOMA, SES_CERRAR,
-- CLI_CONSULTAR, CLI_REGISTRAR, CLI_MODIFICAR, CLI_ELIMINAR,
-- PRO_GESTIONAR.
-- No asignar BUF_VENDER al Maestro.
-- Para el Vendedor mantener BUF_VENDER (facturacion);
-- consultar DNI dentro de frmSeleccionarCliente no exige CLI_MODIFICAR.
