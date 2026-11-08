-- PadelGest - Inicializar historial para productos existentes.
-- Ejecutar SOLO sobre una BD existente donde Producto_C aun no tenga
-- versiones para algunos productos (por ejemplo, instalacion limpia anterior).
-- No borra informacion, no modifica Producto ni sus DVH/DVV.
-- Si un producto ya fue modificado y carece de historial, esta operacion
-- registra su estado ACTUAL, no puede reconstruir estados pasados.
USE [PadelGestDB];
GO
SET XACT_ABORT ON;
GO
BEGIN TRANSACTION;

INSERT INTO dbo.Producto_C
    (CodigoProducto, FechaHora, Nombre, Categoria, PrecioUnitario,
     StockDisponible, StockMinimo, ActivoProducto, Act, TipoCambio)
SELECT p.CodigoProducto, SYSDATETIME(), p.Nombre, p.Categoria,
       p.PrecioUnitario, p.StockDisponible, p.StockMinimo, p.Activo,
       CONVERT(bit, 1), N'Alta'
FROM dbo.Producto AS p
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.Producto_C AS h
    WHERE h.CodigoProducto = p.CodigoProducto
);

SELECT COUNT(*) AS CantidadProductos FROM dbo.Producto;
SELECT COUNT(*) AS VersionesInicialesVigentes
FROM dbo.Producto_C WHERE Act = 1;

COMMIT TRANSACTION;
GO
