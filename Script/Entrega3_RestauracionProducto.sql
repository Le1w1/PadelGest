/*
  PadelGest - Tercera entrega - Bitacora de cambios
  Complemento del script Entrega3_BitacoraProducto.sql.
  SQL Server almacena los triggers como objetos de PadelGestDB.

  TR_Producto_RegistrarCambio (ya instalado):
       Producto INSERT/UPDATE -> Producto_C historico.
  TR_ProductoC_RestaurarVersion (nuevo):
       Activacion de una version historica -> actualiza Producto.

  Nota: Producto.Activo es baja logica del producto.
        Producto_C.Act es version historica vigente.
  IMPORTANTE: Ejecutar despues de Entrega3_BitacoraProducto.sql.
*/
USE [PadelGestDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.Producto_C', N'U') IS NULL
    THROW 51100, 'Debe ejecutar primero Entrega3_BitacoraProducto.sql.', 1;
GO

CREATE OR ALTER TRIGGER dbo.TR_ProductoC_RestaurarVersion
ON dbo.Producto_C
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Solo se restaura una version cuando Act cambia de 0 a 1.
    -- Los cambios de 1 a 0 generados por la auditoria no restauran.
    DECLARE @Restauradas TABLE
    (
        CodigoProducto int NOT NULL PRIMARY KEY,
        IdCambio bigint NOT NULL
    );

    INSERT INTO @Restauradas (CodigoProducto, IdCambio)
    SELECT i.CodigoProducto, i.IdCambio
    FROM inserted i
    INNER JOIN deleted d ON d.IdCambio = i.IdCambio
    WHERE i.Act = 1 AND d.Act = 0;

    IF NOT EXISTS (SELECT 1 FROM @Restauradas)
        RETURN;

    -- Esta operacion dispara TR_Producto_RegistrarCambio. El trigger de
    -- Producto agrega una NUEVA version y desactiva la anterior.
    -- El trabajo se ejecuta dentro de la misma transaccion del usuario.
    UPDATE p
       SET p.Nombre = h.Nombre,
           p.Categoria = h.Categoria,
           p.PrecioUnitario = h.PrecioUnitario,
           p.StockDisponible = h.StockDisponible,
           p.StockMinimo = h.StockMinimo,
           p.Activo = h.ActivoProducto
    FROM dbo.Producto AS p
    INNER JOIN @Restauradas AS r ON r.CodigoProducto = p.CodigoProducto
    INNER JOIN dbo.Producto_C AS h ON h.IdCambio = r.IdCambio;

    -- Producto_C se mantiene historica: solamente insertamos nuevos registros
    -- o modificamos Act. No se reescriben nombres, stock ni tipos anteriores.
END;
GO

-- Verificar que los dos triggers esten habilitados:
SELECT t.name AS TriggerNombre,
       OBJECT_NAME(t.parent_id) AS Tabla,
       t.is_disabled AS Deshabilitado
FROM sys.triggers t
WHERE t.name IN
    (N'TR_Producto_RegistrarCambio', N'TR_ProductoC_RestaurarVersion');
GO
