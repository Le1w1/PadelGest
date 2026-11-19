-- PARCHE INCREMENTAL (SQL Server 2019) - SOLO RAMA DE PRUEBA
-- Ejecutar una vez en la base de datos que usa la aplicacion.
-- No borra datos ni recrea tablas. Conviene realizar un respaldo antes.
-- Al restaurar una version de Producto_C, conserva la misma fila y FechaHora.
-- Las altas y modificaciones normales de Producto siguen creando historial.
--
-- COMPROBACION MANUAL RECOMENDADA:
-- 1. Consultar el COUNT(*) de Producto_C para un CodigoProducto.
-- 2. Restaurar desde PadelGest una version antigua con datos distintos.
-- 3. Verificar que COUNT(*) no aumento, IdCambio/FechaHora no cambiaron,
--    la version elegida tiene Act=1 y el producto actual coincide.
-- 4. Modificar normalmente el precio: debe crear una version nueva.
USE [PadelGestDB];
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER TRIGGER [dbo].[TR_Producto_RegistrarCambio]
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
        StockDisponible, StockMinimo, ActivoProducto, Act, TipoCambio
    )
    SELECT
        i.CodigoProducto, SYSDATETIME(),
        i.Nombre, i.Categoria, i.PrecioUnitario,
        i.StockDisponible, i.StockMinimo, i.Activo, 1,
        CASE
            WHEN d.CodigoProducto IS NULL THEN N'Alta'
            WHEN i.Activo <> d.Activo AND i.Activo = 0
                THEN N'Desactivacion'
            WHEN i.Activo <> d.Activo AND i.Activo = 1
                THEN N'Reactivacion'
            WHEN i.StockDisponible <> d.StockDisponible
                 AND i.Nombre = d.Nombre
                 AND i.Categoria = d.Categoria
                 AND i.PrecioUnitario = d.PrecioUnitario
                 AND i.StockMinimo = d.StockMinimo
                 AND i.Activo = d.Activo
                THEN N'Stock'
            ELSE N'Modificacion'
        END
    FROM inserted i
    INNER JOIN @Cambios c ON c.CodigoProducto = i.CodigoProducto
    LEFT JOIN deleted d ON d.CodigoProducto = i.CodigoProducto;
END;
GO
