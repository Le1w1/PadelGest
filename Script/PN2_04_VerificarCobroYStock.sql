/*
 PadelGest - PN2
 Consultas de verificación de CUN12, CUN13 y CUN14.
 SOLO LECTURA: no modifica la base ni recalcula integridad.
 Ejecutar despues de probar una venta aprobada o rechazada.
*/
USE [PadelGestDB];
GO

-- Ventas y facturas generadas (los datos bancarios residen en Venta).
SELECT TOP (20)
    V.Codigo AS CodigoVenta,
    V.DNI,
    V.FechaHora,
    V.ImporteTotal,
    V.Banco,
    V.Ultimos4Tarjeta,
    V.EstadoPago,
    V.CodigoAutorizacion,
    V.Estado AS EstadoVenta,
    F.IdFacturaVenta,
    F.Estado AS EstadoFactura
FROM dbo.Venta V
INNER JOIN dbo.FacturaVenta F ON F.IdVenta = V.IdVenta
ORDER BY V.FechaHora DESC;

-- Stock actual. Nunca debe ser negativo.
SELECT CodigoProducto, Nombre, Categoria, StockDisponible, StockMinimo, Activo
FROM dbo.Producto
ORDER BY Categoria, Nombre;

-- Carritos que quedaron vendidos y su cantidad de productos.
SELECT C.IdCarrito, C.DNI, C.Estado,
       COUNT(D.IdDetalleCarrito) AS ProductosDistintos,
       SUM(D.Cantidad) AS UnidadesVendidas,
       C.ImporteTotal
FROM dbo.Carrito C
INNER JOIN dbo.DetalleCarrito D ON D.IdCarrito = C.IdCarrito
WHERE C.Estado = N'Vendido'
GROUP BY C.IdCarrito, C.DNI, C.Estado, C.ImporteTotal
ORDER BY C.IdCarrito DESC;

-- Integridad de las tablas modificadas.
SELECT NombreTabla, DVV
FROM dbo.DigitoVerificador
WHERE NombreTabla IN (N'Producto', N'Carrito', N'DetalleCarrito',
                      N'Venta', N'FacturaVenta')
ORDER BY NombreTabla;

-- Evidencia de registro o cancelacion en bitacora.
SELECT TOP (20) FechaHora, Usuario, Modulo, Accion, Resultado, Descripcion
FROM dbo.BitacoraEvento
WHERE Modulo = N'Buffet'
ORDER BY FechaHora DESC;
GO
