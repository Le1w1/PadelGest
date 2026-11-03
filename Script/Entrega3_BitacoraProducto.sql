/*
  PadelGest - Tercera entrega
  Migracion incremental: bitacora historica de Producto (Producto_C).

  Ejecutar en SQL Server Management Studio sobre PadelGestDB existente.
  NO ejecutar nuevamente el script de instalacion limpia.
  Act = 1 indica la VERSION VIGENTE en Producto_C.
  ActivoProducto es la baja/alta logica comercial de dbo.Producto.
  El trigger ignora modificaciones exclusivas de DVH para evitar ruido.
*/
USE [PadelGestDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.Producto_C', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Producto_C
    (
        IdCambio          bigint IDENTITY(1,1) NOT NULL,
        CodigoProducto    int NOT NULL,
        FechaHora         datetime2(3) NOT NULL
            CONSTRAINT DF_ProductoC_FechaHora DEFAULT (SYSDATETIME()),
        Nombre            nvarchar(120) NOT NULL,
        Categoria         nvarchar(80) NOT NULL,
        PrecioUnitario    decimal(12,2) NOT NULL,
        StockDisponible   int NOT NULL,
        StockMinimo       int NOT NULL,
        ActivoProducto    bit NOT NULL,
        Act               bit NOT NULL
            CONSTRAINT DF_ProductoC_Act DEFAULT (1),
        TipoCambio        nvarchar(25) NOT NULL,
        CONSTRAINT PK_Producto_C PRIMARY KEY (IdCambio),
        CONSTRAINT FK_Producto_C_Producto
            FOREIGN KEY (CodigoProducto)
            REFERENCES dbo.Producto(CodigoProducto)
    );
END;
GO

-- Solo una version historica vigente por producto.
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Producto_C')
      AND name = N'UX_ProductoC_VersionVigente'
)
BEGIN
    CREATE UNIQUE INDEX UX_ProductoC_VersionVigente
        ON dbo.Producto_C (CodigoProducto)
        WHERE Act = 1;
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'dbo.Producto_C')
      AND name = N'IX_ProductoC_Consulta'
)
BEGIN
    CREATE INDEX IX_ProductoC_Consulta
        ON dbo.Producto_C (CodigoProducto, FechaHora DESC, IdCambio DESC);
END;
GO

CREATE OR ALTER TRIGGER dbo.TR_Producto_RegistrarCambio
ON dbo.Producto
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
    WHERE d.CodigoProducto IS NULL
       OR EXISTS
       (
           SELECT i.Nombre, i.Categoria, i.PrecioUnitario,
                  i.StockDisponible, i.StockMinimo, i.Activo
           EXCEPT
           SELECT d.Nombre, d.Categoria, d.PrecioUnitario,
                  d.StockDisponible, d.StockMinimo, d.Activo
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

-- Inicializacion para productos ya existentes.
-- Solo se realiza si el producto aun no tiene versiones historicas.
INSERT INTO dbo.Producto_C
(
    CodigoProducto, FechaHora, Nombre, Categoria, PrecioUnitario,
    StockDisponible, StockMinimo, ActivoProducto, Act, TipoCambio
)
SELECT
    p.CodigoProducto, SYSDATETIME(), p.Nombre, p.Categoria,
    p.PrecioUnitario, p.StockDisponible, p.StockMinimo,
    p.Activo, 1, N'Inicial'
FROM dbo.Producto p
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.Producto_C h
    WHERE h.CodigoProducto = p.CodigoProducto
);
GO

-- Verificacion: esta consulta NO debe devolver filas.
SELECT CodigoProducto, COUNT(*) AS CantidadVigentes
FROM dbo.Producto_C
WHERE Act = 1
GROUP BY CodigoProducto
HAVING COUNT(*) <> 1;
GO

-- Consulta de ejemplo:
SELECT h.IdCambio, h.CodigoProducto, h.FechaHora,
       h.Nombre, h.Categoria, h.PrecioUnitario,
       h.StockDisponible, h.StockMinimo,
       h.ActivoProducto, h.Act, h.TipoCambio
FROM dbo.Producto_C h
ORDER BY h.FechaHora DESC, h.IdCambio DESC;
GO
