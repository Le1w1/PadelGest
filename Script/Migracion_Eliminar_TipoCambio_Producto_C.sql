-- PadelGest: migracion de Producto_C sin TipoCambio
-- Ejecutar UNA SOLA VEZ sobre una base existente, con respaldo previo.
-- No elimina filas historicas ni modifica IdCambio, Act o FechaHora.
USE [PadelGestDB];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
ALTER TRIGGER [dbo].[TR_Producto_RegistrarCambio]
ON [dbo].[Producto]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Multirregistro: un UPDATE puede cambiar varios productos.
    DECLARE @Cambios table (CodigoProducto int NOT NULL PRIMARY KEY);

    INSERT INTO @Cambios (CodigoProducto)
    SELECT i.CodigoProducto
    FROM inserted i
    LEFT JOIN deleted d ON d.CodigoProducto = i.CodigoProducto
    WHERE
    (
        d.CodigoProducto IS NULL
        OR EXISTS
        (
            SELECT i.Nombre, i.Categoria, i.PrecioUnitario,
                   i.StockDisponible, i.StockMinimo, i.Activo
            EXCEPT
            SELECT d.Nombre, d.Categoria, d.PrecioUnitario,
                   d.StockDisponible, d.StockMinimo, d.Activo
        )
    )
    -- Si la version historica ya fue marcada como vigente y sus valores
    -- coinciden con Producto, se trata de una restauracion: no insertar
    -- otra fila ni alterar la fecha original de Producto_C.
    AND NOT EXISTS
    (
        SELECT 1
        FROM dbo.Producto_C AS h
        WHERE h.CodigoProducto = i.CodigoProducto
          AND h.Act = 1
          AND h.Nombre = i.Nombre
          AND h.Categoria = i.Categoria
          AND h.PrecioUnitario = i.PrecioUnitario
          AND h.StockDisponible = i.StockDisponible
          AND h.StockMinimo = i.StockMinimo
          AND h.ActivoProducto = i.Activo
    );

    IF NOT EXISTS (SELECT 1 FROM @Cambios)
        RETURN;

    -- La version anterior deja de estar vigente. Nunca se borra.
    UPDATE historial
       SET Act = 0
    FROM dbo.Producto_C historial
    INNER JOIN @Cambios c ON c.CodigoProducto = historial.CodigoProducto
    WHERE historial.Act = 1;

    -- Nueva instantanea del producto dentro de la MISMA transaccion.
    INSERT INTO dbo.Producto_C
    (
        CodigoProducto, FechaHora, Nombre, Categoria, PrecioUnitario,
        StockDisponible, StockMinimo, ActivoProducto, Act
    )
    SELECT
        i.CodigoProducto, SYSDATETIME(),
        i.Nombre, i.Categoria, i.PrecioUnitario,
        i.StockDisponible, i.StockMinimo, i.Activo, 1
    FROM inserted i
    INNER JOIN @Cambios c ON c.CodigoProducto = i.CodigoProducto
    LEFT JOIN deleted d ON d.CodigoProducto = i.CodigoProducto;
END;
GO
IF COL_LENGTH('dbo.Producto_C', 'TipoCambio') IS NOT NULL
BEGIN
    DECLARE @constraintName sysname;
    SELECT @constraintName = dc.name
    FROM sys.default_constraints AS dc
    INNER JOIN sys.columns AS c ON c.object_id = dc.parent_object_id
        AND c.column_id = dc.parent_column_id
    WHERE dc.parent_object_id = OBJECT_ID(N'dbo.Producto_C')
      AND c.name = N'TipoCambio';
    IF @constraintName IS NOT NULL
    BEGIN
        DECLARE @cmd nvarchar(max) = N'ALTER TABLE dbo.Producto_C DROP CONSTRAINT ' + QUOTENAME(@constraintName);
        EXEC sp_executesql @cmd;
    END;
    ALTER TABLE dbo.Producto_C DROP COLUMN TipoCambio;
END;
GO
-- Verificar que las versiones historicas siguen presentes:
SELECT CodigoProducto, COUNT(*) AS TotalVersiones,
       SUM(CASE WHEN Act = 1 THEN 1 ELSE 0 END) AS VersionesVigentes
FROM dbo.Producto_C
GROUP BY CodigoProducto
ORDER BY CodigoProducto;
GO
