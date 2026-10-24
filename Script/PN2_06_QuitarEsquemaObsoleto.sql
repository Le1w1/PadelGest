/*
  PadelGest - PN2 - Limpieza controlada de la base SQL Server.
  SOLO luego de aplicar PN2_05 y comprobar que el circuito nuevo funciona.

  Elimina: Carrito, DetalleCarrito, Venta, FacturaVenta.
  CONSERVA: Factura (PN1), Reserva, Cliente, Producto, Usuario,
            FacturaBuffet, DetalleFactura, bitacora y toda la seguridad.

  IMPORTANTE:
  - Realizar BACKUP antes de ejecutar en la base real.
  - Si hay al menos UNA fila antigua, ABORTA para no perder ventas.
  - No hace TRUNCATE, DELETE de registros de negocio ni CASCADE.
  - No migra datos; las ventas historicas requieren migracion/archivo previo.
  - No ejecutar una version antigua del programa despues de esta limpieza.
*/
USE [PadelGestDB];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF DB_NAME() <> N'PadelGestDB'
        THROW 54000, 'Base de datos incorrecta: se esperaba PadelGestDB.', 1;

    IF OBJECT_ID(N'dbo.Factura',N'U') IS NULL
       OR OBJECT_ID(N'dbo.Reserva',N'U') IS NULL
       OR OBJECT_ID(N'dbo.FacturaBuffet',N'U') IS NULL
       OR OBJECT_ID(N'dbo.DetalleFactura',N'U') IS NULL
       OR OBJECT_ID(N'dbo.DigitoVerificador',N'U') IS NULL
        THROW 54001, 'No esta completo el modelo nuevo o PN1. No se elimina nada.', 1;

    -- Solo admite esquema antiguo completo, para no ocultar una migracion parcial.
    IF OBJECT_ID(N'dbo.Carrito',N'U') IS NULL
       OR OBJECT_ID(N'dbo.DetalleCarrito',N'U') IS NULL
       OR OBJECT_ID(N'dbo.Venta',N'U') IS NULL
       OR OBJECT_ID(N'dbo.FacturaVenta',N'U') IS NULL
        THROW 54002, 'Faltan tablas antiguas. Revisar manualmente: no se elimina nada.', 1;

    -- Si hay ventas, facturas, carritos o renglones previos, preservar
    -- historial y abortar en vez de descartarlo silenciosamente.
    IF EXISTS (SELECT 1 FROM dbo.FacturaVenta WITH (TABLOCKX, HOLDLOCK))
       OR EXISTS (SELECT 1 FROM dbo.Venta WITH (TABLOCKX, HOLDLOCK))
       OR EXISTS (SELECT 1 FROM dbo.DetalleCarrito WITH (TABLOCKX, HOLDLOCK))
       OR EXISTS (SELECT 1 FROM dbo.Carrito WITH (TABLOCKX, HOLDLOCK))
        THROW 54003, 'Existen registros en tablas PN2 antiguas. Respaldar y migrar o archivar antes de eliminarlas.', 1;

    -- Rechazar otras claves foraneas que apunten a las tablas a eliminar.
    IF EXISTS (
        SELECT 1
        FROM sys.foreign_keys AS fk
        WHERE fk.referenced_object_id IN (
            OBJECT_ID(N'dbo.Carrito'), OBJECT_ID(N'dbo.DetalleCarrito'),
            OBJECT_ID(N'dbo.Venta'), OBJECT_ID(N'dbo.FacturaVenta')
        )
        AND fk.parent_object_id NOT IN (
            OBJECT_ID(N'dbo.Carrito'), OBJECT_ID(N'dbo.DetalleCarrito'),
            OBJECT_ID(N'dbo.Venta'), OBJECT_ID(N'dbo.FacturaVenta')
        )
    )
        THROW 54004, 'Otras tablas tienen FK hacia el modelo antiguo. Revisar dependencias primero.', 1;

    -- Orden inverso de dependencias para respetar las FK.
    DROP TABLE dbo.FacturaVenta;
    DROP TABLE dbo.Venta;
    DROP TABLE dbo.DetalleCarrito;
    DROP TABLE dbo.Carrito;

    DELETE FROM dbo.DigitoVerificador
    WHERE NombreTabla IN (N'Carrito', N'DetalleCarrito', N'Venta', N'FacturaVenta');

    COMMIT TRANSACTION;
    PRINT N'OK: eliminadas las cuatro tablas obsoletas y sus entradas DVV.';
    PRINT N'PN1: Factura y Reserva intactas. PN2: FacturaBuffet y DetalleFactura intactas.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Validacion (puede ejecutarse tambien antes, sin modificar datos).
SELECT t.name AS TablaActual
FROM sys.tables AS t
WHERE t.schema_id = SCHEMA_ID(N'dbo')
  AND t.name IN (
      N'Factura', N'Reserva', N'Cliente', N'Producto',
      N'FacturaBuffet', N'DetalleFactura',
      N'Carrito', N'DetalleCarrito', N'Venta', N'FacturaVenta'
  )
ORDER BY t.name;

SELECT NombreTabla, DVV
FROM dbo.DigitoVerificador
WHERE NombreTabla IN (
    N'Factura', N'FacturaBuffet', N'DetalleFactura',
    N'Carrito', N'DetalleCarrito', N'Venta', N'FacturaVenta'
)
ORDER BY NombreTabla;
GO
