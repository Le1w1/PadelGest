/*
 ELIMINAR SOLAMENTE LOS DATOS DE PRUEBA DEMOPRED DEL REPORTE.
 Incluye tanto las 56 facturas iniciales como las 100 adicionales DEMOPRED-EX.
 Ejecutar si se uso alguno de los scripts de datos demo.
 No borra facturas reales ni modifica el stock. Se recomienda respaldo previo.
*/
USE [PadelGestDB];
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DELETE d
    FROM dbo.DetalleFactura d
    INNER JOIN dbo.FacturaBuffet f ON f.IdFactura = d.IdFactura
    WHERE f.NumeroFactura LIKE N'DEMOPRED-%'
      AND f.CodigoAutorizacion LIKE N'DEMOPRED-%'
      AND f.Banco = N'DEMO - NO REAL';

    DELETE FROM dbo.FacturaBuffet
    WHERE NumeroFactura LIKE N'DEMOPRED-%'
      AND CodigoAutorizacion LIKE N'DEMOPRED-%'
      AND Banco = N'DEMO - NO REAL';

    DECLARE @DvvFacturas varchar(64) = LOWER(CONVERT(varchar(64),
      HASHBYTES('SHA2_256', CONVERT(varchar(max),
        ISNULL((SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
            WITHIN GROUP (ORDER BY IdFactura) FROM dbo.FacturaBuffet), ''))), 2));
    DECLARE @DvvDetalles varchar(64) = LOWER(CONVERT(varchar(64),
      HASHBYTES('SHA2_256', CONVERT(varchar(max),
        ISNULL((SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
            WITHIN GROUP (ORDER BY IdDetalleFactura) FROM dbo.DetalleFactura), ''))), 2));

    UPDATE dbo.DigitoVerificador SET DVV = @DvvFacturas
      WHERE NombreTabla = N'FacturaBuffet';
    UPDATE dbo.DigitoVerificador SET DVV = @DvvDetalles
      WHERE NombreTabla = N'DetalleFactura';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
