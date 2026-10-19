/*
    PadelGest / AirPadel - PN2
    03 - Productos de PRUEBA para el buffet (precios ficticios en ARS)

    REQUISITOS:
    - Ejecutar en PadelGestDB DESPUES de crear las tablas PN2.
    - Realizar backup antes de ejecutar.
    - No ejecutar el archivo completo Pad elGestDB.sql de reconstruccion.
    - El script SOLO agrega productos si dbo.Producto esta VACIA.
      Si hay datos, se detiene y no toca el stock existente.
    - El script calcula DVH / DVV de Producto con el orden de
      TablasProtegidas y SHA-256 de UTF-8, sin tocar PN1.
    - Los nombres de ejemplo usan caracteres ASCII para que
      HASHBYTES sobre VARCHAR coincida con Encoding.UTF8 en C#.
      En produccion, calcular DV desde la aplicacion C#.
*/

USE [PadelGestDB];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.Producto', N'U') IS NULL
        THROW 53001, 'Falta dbo.Producto. Ejecute primero la migracion PN2.', 1;

    IF OBJECT_ID(N'dbo.DigitoVerificador', N'U') IS NULL
        THROW 53002, 'Falta dbo.DigitoVerificador.', 1;

    -- El seed es intencionalmente de un solo uso: nunca altera productos
    -- existentes, evita precios pisados y no mascara cambios previos.
    IF EXISTS (SELECT 1 FROM dbo.Producto WITH (UPDLOCK, HOLDLOCK))
        THROW 53003, 'dbo.Producto ya contiene registros. No se agrego ni modifico ningun producto.', 1;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.DigitoVerificador WITH (UPDLOCK, HOLDLOCK)
        WHERE NombreTabla = N'Producto'
    )
        THROW 53004, 'Falta el registro DVV de Producto.', 1;

    -- El DVV debe estar sin inicializar o corresponder a una tabla vacia.
    IF EXISTS (
        SELECT 1
        FROM dbo.DigitoVerificador
        WHERE NombreTabla = N'Producto'
          AND DVV IS NOT NULL
          AND DVV <> 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855'
    )
        THROW 53005, 'DVV de Producto no corresponde a tabla vacia. Revisar integridad antes de cargar.', 1;

    DECLARE @Catalogo TABLE
    (
        Orden INT PRIMARY KEY,
        Nombre NVARCHAR(120) NOT NULL,
        Categoria NVARCHAR(80) NOT NULL,
        PrecioUnitario DECIMAL(12,2) NOT NULL,
        StockDisponible INT NOT NULL,
        StockMinimo INT NOT NULL,
        Activo BIT NOT NULL
    );

    INSERT INTO @Catalogo
        (Orden, Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo)
    VALUES
    (1, N'Agua mineral sin gas 500 ml', N'Bebidas', 1500.00, 32, 10, 1),
    (2, N'Agua mineral 1.5 L', N'Bebidas', 2800.00, 12, 4, 1),
    (3, N'Agua con gas 500 ml', N'Bebidas', 1600.00, 9, 4, 1),
    (4, N'Gaseosa cola 500 ml', N'Bebidas', 2900.00, 25, 8, 1),
    (5, N'Gaseosa lima limon 500 ml', N'Bebidas', 2900.00, 18, 6, 1),
    (6, N'Gaseosa naranja 500 ml', N'Bebidas', 2850.00, 0, 5, 1),
    (7, N'Bebida isotonica naranja 500 ml', N'Bebidas', 3600.00, 15, 5, 1),
    (8, N'Bebida isotonica limon 500 ml', N'Bebidas', 3600.00, 2, 5, 1),
    (9, N'Jugo de naranja 500 ml', N'Bebidas', 2600.00, 9, 4, 1),
    (10, N'Energizante lata 250 ml', N'Bebidas', 5200.00, 7, 3, 1),
    (11, N'Cafe expreso', N'Cafeteria', 2500.00, 40, 10, 1),
    (12, N'Cafe con leche', N'Cafeteria', 3200.00, 16, 5, 1),
    (13, N'Te', N'Cafeteria', 2000.00, 15, 4, 1),
    (14, N'Chocolate caliente', N'Cafeteria', 3400.00, 8, 3, 1),
    (15, N'Alfajor de chocolate', N'Snacks', 2000.00, 20, 6, 1),
    (16, N'Alfajor de maicena', N'Snacks', 2100.00, 5, 5, 1),
    (17, N'Barra de cereal', N'Snacks', 1800.00, 12, 4, 1),
    (18, N'Galletitas dulces', N'Snacks', 2700.00, 13, 4, 1),
    (19, N'Galletitas saladas', N'Snacks', 2600.00, 8, 3, 1),
    (20, N'Papas fritas paquete', N'Snacks', 3400.00, 9, 3, 1),
    (21, N'Mani salado', N'Snacks', 2600.00, 5, 5, 1),
    (22, N'Tostado jamon y queso', N'Comidas', 6500.00, 10, 3, 1),
    (23, N'Sandwich de pollo', N'Comidas', 7900.00, 6, 2, 1),
    (24, N'Sandwich vegetariano', N'Comidas', 7300.00, 0, 2, 1),
    (25, N'Medialuna', N'Comidas', 1700.00, 15, 5, 1),
    (26, N'Porcion de pizza', N'Comidas', 5800.00, 9, 3, 1),
    (27, N'Caramelos unidad', N'Golosinas', 500.00, 50, 15, 1),
    (28, N'Chicle unidad', N'Golosinas', 900.00, 20, 5, 1),
    (29, N'Galletitas descontinuadas', N'Snacks', 2400.00, 6, 3, 0);

    INSERT INTO dbo.Producto
        (Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo)
    SELECT Nombre, Categoria, PrecioUnitario, StockDisponible, StockMinimo, Activo
    FROM @Catalogo
    ORDER BY Orden;

    /*
       DVH segun Servicios.DigitoVerificador.TablasProtegidas:
       CodigoProducto|Nombre|Categoria|PrecioUnitario|StockDisponible|StockMinimo|Activo

       Nota: en C# SqlDataReader produce DECIMAL(12,2), por lo que
       ToString(InvariantCulture) mantiene dos decimales, p. ej. 1500.00.
       Activo BIT se normaliza como 1 o 0.
       Todos los valores de este seed son ASCII (UTF8 = VARCHAR en ASCII).
    */
    UPDATE P
    SET DVH = LOWER(CONVERT(VARCHAR(64),
        HASHBYTES('SHA2_256',
            CONVERT(VARCHAR(MAX),
                CONCAT(
                    CONVERT(VARCHAR(12), P.CodigoProducto), '|',
                    CONVERT(VARCHAR(120), P.Nombre), '|',
                    CONVERT(VARCHAR(80), P.Categoria), '|',
                    CONVERT(VARCHAR(40), P.PrecioUnitario), '|',
                    CONVERT(VARCHAR(12), P.StockDisponible), '|',
                    CONVERT(VARCHAR(12), P.StockMinimo), '|',
                    CONVERT(VARCHAR(1), CONVERT(INT, P.Activo))
                )
            )
        ), 2))
    FROM dbo.Producto AS P;

    DECLARE @DVHConcatenados VARCHAR(MAX);
    SELECT @DVHConcatenados =
        STRING_AGG(CONVERT(VARCHAR(MAX), DVH), '|')
            WITHIN GROUP (ORDER BY CodigoProducto)
    FROM dbo.Producto;

    UPDATE dbo.DigitoVerificador
    SET DVV = LOWER(CONVERT(VARCHAR(64),
        HASHBYTES('SHA2_256', CONVERT(VARCHAR(MAX), @DVHConcatenados)), 2))
    WHERE NombreTabla = N'Producto';

    IF @@ROWCOUNT <> 1
        THROW 53006, 'No se pudo actualizar el DVV de Producto.', 1;

    IF (SELECT COUNT(*) FROM dbo.Producto) <> 29
        THROW 53007, 'Cantidad de productos inesperada. Revirtiendo cambios.', 1;

    IF EXISTS (
        SELECT 1
        FROM dbo.Producto
        WHERE DVH IS NULL OR LEN(DVH) <> 64
    )
        THROW 53008, 'Un producto no tiene DVH valido. Revirtiendo cambios.', 1;

    COMMIT TRANSACTION;
    PRINT N'Productos de prueba cargados con DVH/DVV calculados.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- COMPROBACION: stock 0, stock bajo, inactivo, y DVV.
SELECT CodigoProducto, Nombre, Categoria, PrecioUnitario,
       StockDisponible, StockMinimo, Activo, DVH
FROM dbo.Producto
ORDER BY Categoria, Nombre;

SELECT
    COUNT(*) AS TotalProductos,
    SUM(CASE WHEN Activo = 1 THEN 1 ELSE 0 END) AS ProductosActivos,
    SUM(CASE WHEN Activo = 0 THEN 1 ELSE 0 END) AS ProductosInactivos,
    SUM(CASE WHEN Activo = 1 AND StockDisponible = 0 THEN 1 ELSE 0 END) AS SinStock,
    SUM(CASE WHEN Activo = 1 AND StockDisponible > 0
                  AND StockDisponible <= StockMinimo THEN 1 ELSE 0 END) AS StockBajoOEnMinimo
FROM dbo.Producto;

SELECT NombreTabla, DVV
FROM dbo.DigitoVerificador
WHERE NombreTabla = N'Producto';
GO
