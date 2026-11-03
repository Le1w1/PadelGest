/*
 PadelGest - Entrega 3 - Prueba de restauracion desde Producto_C
 Ejecutar en base de PRUEBAS despues de:
   1. Script/Entrega3_BitacoraProducto.sql
   2. Script/Entrega3_RestauracionProducto.sql
 Todas las modificaciones de esta prueba se revierten con ROLLBACK.
*/
USE [PadelGestDB];
GO
SET NOCOUNT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Nombre nvarchar(120) = N'TEST_RESTAURACION_' + LEFT(CONVERT(nvarchar(36), NEWID()), 8);
    DECLARE @Codigo int;
    DECLARE @VersionAnterior bigint;

    INSERT INTO dbo.Producto
        (Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo)
    VALUES
        (@Nombre, N'Prueba', 100.00, 20, 5, 1);
    SET @Codigo = CONVERT(int, SCOPE_IDENTITY());

    SELECT @VersionAnterior = IdCambio FROM dbo.Producto_C
    WHERE CodigoProducto = @Codigo AND Act = 1;

    IF @VersionAnterior IS NULL
        THROW 51200, 'El trigger de Producto no genero la version inicial.', 1;

    UPDATE dbo.Producto
    SET PrecioUnitario = 200.00, StockDisponible = 15
    WHERE CodigoProducto = @Codigo;

    IF (SELECT COUNT(*) FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo AND Act = 1) <> 1
        THROW 51201, 'La modificacion no dejo una unica version vigente.', 1;

    -- Restaurar una version: desactivar vigente, activar antigua.
    UPDATE dbo.Producto_C
    SET Act = 0
    WHERE CodigoProducto = @Codigo AND Act = 1;

    UPDATE dbo.Producto_C
    SET Act = 1
    WHERE IdCambio = @VersionAnterior AND Act = 0;

    IF (SELECT PrecioUnitario FROM dbo.Producto
        WHERE CodigoProducto = @Codigo) <> 100.00
        THROW 51202, 'No se restauro el precio historico.', 1;

    IF (SELECT StockDisponible FROM dbo.Producto
        WHERE CodigoProducto = @Codigo) <> 20
        THROW 51203, 'No se restauro el stock historico.', 1;

    IF (SELECT COUNT(*) FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo AND Act = 1) <> 1
        THROW 51204, 'La restauracion no dejo una unica version vigente.', 1;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo
          AND Act = 1
          AND IdCambio <> @VersionAnterior
    )
        THROW 51205, 'No se creo una nueva version de tipo Restauracion.', 1;

    IF (SELECT COUNT(*) FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo) <> 3
        THROW 51206, 'Se esperaban 3 versiones historicas.', 1;

    ROLLBACK TRANSACTION;
    PRINT N'OK: alta, modificacion y restauracion. Cambios revertidos.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO