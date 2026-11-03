/*
  PRUEBAS DE BITACORA PRODUCTO_C
  SOLO ejecutar en PadelGestDB de pruebas, despues de Entrega3_BitacoraProducto.sql.
  Todo el escenario se revierte con ROLLBACK.
*/
USE [PadelGestDB];
GO
SET NOCOUNT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @NombrePrueba nvarchar(120) =
        N'Prueba auditoria ' + CONVERT(nvarchar(36), NEWID());
    DECLARE @Codigo int;

    INSERT INTO dbo.Producto
        (Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo)
    VALUES (@NombrePrueba, N'Pruebas', 100.00, 5, 2, 1);

    SET @Codigo = CONVERT(int, SCOPE_IDENTITY());

    IF (SELECT COUNT(*) FROM dbo.Producto_C WHERE CodigoProducto = @Codigo) <> 1
        THROW 51001, 'ERROR: el alta debe crear una version.', 1;

    UPDATE dbo.Producto
       SET StockDisponible = 7
    WHERE CodigoProducto = @Codigo;

    IF (SELECT COUNT(*) FROM dbo.Producto_C WHERE CodigoProducto = @Codigo) <> 2
        THROW 51002, 'ERROR: el cambio de stock debe crear otra version.', 1;

    UPDATE dbo.Producto
       SET DVH = REPLICATE('0', 64)
    WHERE CodigoProducto = @Codigo;

    IF (SELECT COUNT(*) FROM dbo.Producto_C WHERE CodigoProducto = @Codigo) <> 2
        THROW 51003, 'ERROR: actualizar solo DVH no debe crear version.', 1;

    UPDATE dbo.Producto
       SET Activo = 0
    WHERE CodigoProducto = @Codigo;

    IF (SELECT COUNT(*) FROM dbo.Producto_C WHERE CodigoProducto = @Codigo) <> 3
        THROW 51004, 'ERROR: desactivar debe crear otra version.', 1;

    IF (SELECT COUNT(*) FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo AND Act = 1) <> 1
        THROW 51005, 'ERROR: debe existir una sola version vigente.', 1;

    IF NOT EXISTS
    (
        SELECT 1 FROM dbo.Producto_C
        WHERE CodigoProducto = @Codigo
          AND Act = 1 AND ActivoProducto = 0
          AND TipoCambio = N'Desactivacion'
    )
        THROW 51006, 'ERROR: la ultima version no coincide.', 1;

    ROLLBACK TRANSACTION;
    PRINT N'PRUEBAS CORRECTAS. Se revirtieron todos los cambios.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
