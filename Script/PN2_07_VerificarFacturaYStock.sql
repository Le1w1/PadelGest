/*
 PadelGest PN2 - Validacion NO destructiva del modelo final.
 Ejecutar despues de PN2_06 o de una instalacion limpia desde PadelGestDB.sql.
 No modifica datos ni stock.
*/
USE [PadelGestDB];
GO
SET NOCOUNT ON;

-- Las cuatro tablas antiguas deben faltar.
SELECT N'Tablas antiguas restantes' AS Control,
       COUNT(*) AS Cantidad
FROM sys.tables
WHERE schema_id = SCHEMA_ID(N'dbo')
  AND name IN (N'Carrito',N'DetalleCarrito',N'Venta',N'FacturaVenta');

-- PN1 y PN2 deben seguir disponibles.
SELECT T.name AS Tabla, COUNT(C.column_id) AS Columnas
FROM sys.tables T
JOIN sys.columns C ON C.object_id=T.object_id
WHERE T.schema_id=SCHEMA_ID(N'dbo')
  AND T.name IN (N'Factura',N'Reserva',N'Cliente',N'Producto',
                 N'FacturaBuffet',N'DetalleFactura')
GROUP BY T.name ORDER BY T.name;

-- Facturas de buffet registradas con su total almacenado
-- y suma de detalles, sin revelar números de tarjeta.
SELECT F.IdFactura, F.NumeroFactura, F.DNI, F.FechaHoraEmision,
       F.Estado, F.ImporteTotal,
       COUNT(D.IdDetalleFactura) AS ProductosDistintos,
       COALESCE(SUM(D.Subtotal),0) AS TotalDetalles,
       F.CodigoAutorizacion
FROM dbo.FacturaBuffet F
LEFT JOIN dbo.DetalleFactura D ON D.IdFactura=F.IdFactura
GROUP BY F.IdFactura,F.NumeroFactura,F.DNI,F.FechaHoraEmision,
         F.Estado,F.ImporteTotal,F.CodigoAutorizacion
ORDER BY F.IdFactura DESC;

-- No debe haber facturas cuyo total difiera de su detalle.
SELECT F.IdFactura, F.ImporteTotal,
       COALESCE(SUM(D.Subtotal),0) AS TotalDetalles
FROM dbo.FacturaBuffet F
LEFT JOIN dbo.DetalleFactura D ON D.IdFactura=F.IdFactura
GROUP BY F.IdFactura,F.ImporteTotal
HAVING F.ImporteTotal<>COALESCE(SUM(D.Subtotal),0);

-- Estado del inventario.
SELECT CodigoProducto, Nombre, StockDisponible, StockMinimo, Activo
FROM dbo.Producto ORDER BY CodigoProducto;

-- Integridad: no debe quedar DVV de las tablas antiguas
-- y SI debe existir el registro para ambas tablas nuevas.
SELECT NombreTabla, DVV
FROM dbo.DigitoVerificador
WHERE NombreTabla IN (N'Factura',N'Producto',
                     N'FacturaBuffet',N'DetalleFactura',
                     N'Carrito',N'DetalleCarrito',N'Venta',N'FacturaVenta')
ORDER BY NombreTabla;
GO
