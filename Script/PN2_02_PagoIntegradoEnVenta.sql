/*
 PadelGest - PN2
 Migracion 02: registrar el pago aprobado dentro de dbo.Venta.
 NO modifica las tablas ni los registros de PN1.
 Para bases que YA ejecutaron PN2_01_CrearTablasBuffet.sql.
 IMPORTANTE: hacer BACKUP antes de ejecutar.
 Se detiene SIN CAMBIOS si hay ventas o pagos de buffet registrados:
 esas operaciones requieren migracion de datos supervisada.
*/
USE [PadelGestDB];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF DB_NAME() <> N'PadelGestDB'
        THROW 52000, 'La base activa debe ser PadelGestDB.', 1;

    IF OBJECT_ID(N'dbo.Venta', N'U') IS NULL
       OR OBJECT_ID(N'dbo.PagoVenta', N'U') IS NULL
       OR OBJECT_ID(N'dbo.DigitoVerificador', N'U') IS NULL
        THROW 52001, 'No se encontro el esquema PN2 original. No se realizaron cambios.', 1;

    IF EXISTS (SELECT 1 FROM dbo.Venta)
       OR EXISTS (SELECT 1 FROM dbo.PagoVenta)
        THROW 52002, 'Existen ventas o pagos registrados. Detener y migrar esos registros antes de eliminar PagoVenta.', 1;

    IF COL_LENGTH(N'dbo.Venta', N'Banco') IS NOT NULL
       OR COL_LENGTH(N'dbo.Venta', N'Ultimos4Tarjeta') IS NOT NULL
       OR COL_LENGTH(N'dbo.Venta', N'ImportePago') IS NOT NULL
       OR COL_LENGTH(N'dbo.Venta', N'FechaHoraPago') IS NOT NULL
       OR COL_LENGTH(N'dbo.Venta', N'EstadoPago') IS NOT NULL
       OR COL_LENGTH(N'dbo.Venta', N'CodigoAutorizacion') IS NOT NULL
        THROW 52003, 'Venta ya tiene alguna columna de pago. Revisar el esquema; no se realizaron cambios.', 1;

    -- Compilar cada cambio de esquema despues de ejecutar el anterior,
    -- sin salir de la transaccion ni del bloque TRY/CATCH.
    EXEC sys.sp_executesql N'
        ALTER TABLE dbo.Venta ADD
            Banco NVARCHAR(80) NOT NULL,
            Ultimos4Tarjeta CHAR(4) NOT NULL,
            ImportePago DECIMAL(12,2) NOT NULL,
            FechaHoraPago DATETIME2(7) NOT NULL,
            EstadoPago NVARCHAR(20) NOT NULL,
            CodigoAutorizacion NVARCHAR(50) NOT NULL;
    ';

    EXEC sys.sp_executesql N'
        ALTER TABLE dbo.Venta ADD
            CONSTRAINT CK_Venta_Banco
                CHECK (LEN(LTRIM(RTRIM(Banco))) > 0),
            CONSTRAINT CK_Venta_Ultimos4Tarjeta
                CHECK (LEN(Ultimos4Tarjeta) = 4 AND Ultimos4Tarjeta NOT LIKE ''%[^0-9]%''),
            CONSTRAINT CK_Venta_ImportePago
                CHECK (ImportePago > 0),
            CONSTRAINT CK_Venta_EstadoPago
                CHECK (EstadoPago IN (N''Aprobado'', N''Revertido'')),
            CONSTRAINT CK_Venta_CodigoAutorizacion
                CHECK (LEN(LTRIM(RTRIM(CodigoAutorizacion))) > 0);
    ';

    EXEC sys.sp_executesql N'
        ALTER TABLE dbo.Venta
            ADD CONSTRAINT UQ_Venta_CodigoAutorizacion UNIQUE (CodigoAutorizacion);
    ';

    DROP TABLE dbo.PagoVenta;

    -- La tabla eliminada ya no participa del control de integridad.
    DELETE FROM dbo.DigitoVerificador
    WHERE NombreTabla = N'PagoVenta';

    -- Venta esta vacia (comprobado arriba): el DVV nuevo lo inicializa la app.
    UPDATE dbo.DigitoVerificador
    SET DVV = NULL
    WHERE NombreTabla = N'Venta';

    COMMIT TRANSACTION;
    PRINT N'PN2: pago integrado en Venta y tabla PagoVenta eliminada correctamente.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Verificacion: Venta = 15 columnas; PagoVenta no debe existir.
SELECT T.name AS TablaPN2, COUNT(C.column_id) AS CantidadColumnas
FROM sys.tables T
JOIN sys.columns C ON T.object_id = C.object_id
WHERE T.schema_id = SCHEMA_ID(N'dbo')
  AND T.name IN (N'Producto', N'Carrito', N'DetalleCarrito',
                 N'Venta', N'FacturaVenta', N'PagoVenta')
GROUP BY T.name
ORDER BY T.name;

SELECT NombreTabla, DVV
FROM dbo.DigitoVerificador
WHERE NombreTabla IN (N'Venta', N'FacturaVenta', N'PagoVenta');
GO
