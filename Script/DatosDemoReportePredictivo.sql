/*
 PADELGEST - DATOS DE DEMOSTRACION DEL REPORTE PREDICTIVO (PN2).
 ATENCION: USAR SOLO EN BASE DE PRUEBAS. NO EJECUTAR EN PRODUCCION.
 Hace 56 facturas SIMULADAS (2 por dia durante 28 dias), con 112 detalles.
 Todos los comprobantes llevan prefijo DEMOPRED y NO representan cobros reales.
 Se basan en productos, un cliente y un usuario ya existentes.

 Las ventas se insertan como historial anterior al stock actual: NO descuentan
 existencias ni simulan cobros bancarios. Para pruebas integrales de stock/caja,
 registrar ventas reales mediante la interfaz, no utilizar este script.

 Recalcula los DVH/DVV de FacturaBuffet y DetalleFactura.
 Ejecutar primero en una COPIA de PadelGestDB con respaldo.
 Compatible con SQL Server 2019 o posterior.
*/
USE [PadelGestDB];
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @DNI nvarchar(8) =
        (SELECT TOP (1) DNI FROM dbo.Cliente ORDER BY DNI);
    DECLARE @Usuario int =
        (SELECT TOP (1) IdUsuario FROM dbo.Usuario WHERE Activo = 1 ORDER BY IdUsuario);
    IF @DNI IS NULL OR @Usuario IS NULL
        THROW 51010, 'Se necesita al menos un cliente y un usuario activo para generar datos demo.', 1;

    DECLARE @Productos TABLE
    (
        Numero int PRIMARY KEY,
        Codigo int NOT NULL,
        Precio decimal(12,2) NOT NULL
    );
    INSERT INTO @Productos (Numero, Codigo, Precio)
    SELECT ROW_NUMBER() OVER (ORDER BY CodigoProducto),
           CodigoProducto, PrecioUnitario
    FROM dbo.Producto
    WHERE Activo = 1 AND PrecioUnitario > 0;

    DECLARE @TotalProductos int = (SELECT COUNT(1) FROM @Productos);
    IF @TotalProductos < 2
        THROW 51011, 'Se necesitan al menos dos productos activos para los datos demo.', 1;

    -- No duplicar la muestra en una segunda ejecucion.
    IF EXISTS (SELECT 1 FROM dbo.FacturaBuffet
               WHERE NumeroFactura LIKE N'DEMOPRED-%')
        THROW 51012, 'Ya existen datos DEMOPRED. No se agregaron facturas duplicadas.', 1;

    DECLARE @Dia int = 28;
    WHILE @Dia >= 1
    BEGIN
        DECLARE @Turno int = 1;
        WHILE @Turno <= 2
        BEGIN
            DECLARE @Fecha datetime2(7) =
                DATEADD(MINUTE, @Turno * 90,
                    DATEADD(DAY, -@Dia, CAST(CAST(GETDATE() AS date) AS datetime2(7))));
            DECLARE @Numero nvarchar(30) =
                CONCAT(N'DEMOPRED-', CONVERT(char(8), @Fecha, 112), N'-', @Turno);
            DECLARE @Autorizacion nvarchar(50) =
                CONCAT(N'DEMOPRED-', CONVERT(char(8), @Fecha, 112), N'-', @Turno);

            DECLARE @IndiceA int = ((@Dia * 3 + @Turno) % @TotalProductos) + 1;
            DECLARE @IndiceB int = (@IndiceA % @TotalProductos) + 1;
            DECLARE @CodigoA int, @CodigoB int;
            DECLARE @PrecioA decimal(12,2), @PrecioB decimal(12,2);
            SELECT @CodigoA = Codigo, @PrecioA = Precio FROM @Productos WHERE Numero = @IndiceA;
            SELECT @CodigoB = Codigo, @PrecioB = Precio FROM @Productos WHERE Numero = @IndiceB;

            -- Cantidades variables para que el grafico no sea uniforme.
            DECLARE @CantidadA int = 1 + ((@Dia + @Turno) % 6);
            DECLARE @CantidadB int = 1 + ((@Dia * 2 + @Turno) % 4);
            DECLARE @Importe decimal(12,2) =
                @PrecioA * @CantidadA + @PrecioB * @CantidadB;

            INSERT INTO dbo.FacturaBuffet
                (NumeroFactura,DNI,IdVendedor,FechaHoraEmision,ImporteTotal,
                 Estado,Banco,Ultimos4Tarjeta,FechaHoraPago,CodigoAutorizacion)
            VALUES (@Numero,@DNI,@Usuario,@Fecha,@Importe,
                    N'Pagada',N'DEMO - NO REAL',N'0000',@Fecha,@Autorizacion);

            DECLARE @Factura int = CONVERT(int, SCOPE_IDENTITY());
            INSERT INTO dbo.DetalleFactura (IdFactura,CodigoProducto,Cantidad,PrecioUnitario)
            VALUES (@Factura,@CodigoA,@CantidadA,@PrecioA),
                   (@Factura,@CodigoB,@CantidadB,@PrecioB);

            SET @Turno += 1;
        END;
        SET @Dia -= 1;
    END;

    -- DVH: formato InvariantCulture de .NET para datetime y decimal.
    -- Fecha: MM/dd/yyyy HH:mm:ss (DateTime.ToString por defecto).
    UPDATE f SET DVH = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
      CONVERT(varchar(max), CONCAT(
        f.IdFactura, '|', f.NumeroFactura, '|', f.DNI, '|', f.IdVendedor, '|',
        FORMAT(f.FechaHoraEmision,'MM/dd/yyyy HH:mm:ss','en-US'), '|',
        FORMAT(f.ImporteTotal,'0.00','en-US'), '|', f.Estado, '|', f.Banco, '|',
        f.Ultimos4Tarjeta, '|',
        FORMAT(f.FechaHoraPago,'MM/dd/yyyy HH:mm:ss','en-US'), '|', f.CodigoAutorizacion
      ))), 2))
    FROM dbo.FacturaBuffet f
    WHERE f.NumeroFactura LIKE N'DEMOPRED-%';

    UPDATE d SET DVH = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
      CONVERT(varchar(max), CONCAT(
        d.IdDetalleFactura, '|', d.IdFactura, '|', d.CodigoProducto, '|',
        d.Cantidad, '|', FORMAT(d.PrecioUnitario,'0.00','en-US'), '|',
        FORMAT(d.Subtotal,'0.00','en-US')
      ))), 2))
    FROM dbo.DetalleFactura d
    INNER JOIN dbo.FacturaBuffet f ON f.IdFactura = d.IdFactura
    WHERE f.NumeroFactura LIKE N'DEMOPRED-%';

    -- Actualizar DVV a partir de todos los DVH, ordenados por PK.
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
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla,DVV)
        VALUES (N'FacturaBuffet',@DvvFacturas);

    UPDATE dbo.DigitoVerificador SET DVV = @DvvDetalles
        WHERE NombreTabla = N'DetalleFactura';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla,DVV)
        VALUES (N'DetalleFactura',@DvvDetalles);

    COMMIT TRANSACTION;
    SELECT COUNT(*) AS FacturasDemo FROM dbo.FacturaBuffet
    WHERE NumeroFactura LIKE N'DEMOPRED-%';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
