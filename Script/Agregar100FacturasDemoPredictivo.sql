/*
 PADELGEST / PN2 - 100 FACTURAS ADICIONALES PARA DEMOSTRAR EL REPORTE.
 SOLO EN UNA COPIA DE PRUEBAS DE PadelGestDB. NO EN PRODUCCION.
 Se suma a las 56 facturas DEMOPRED ya creadas: 156 facturas simuladas en total.
 Genera 100 facturas y 300 renglones de detalle con productos variados.
 Los primeros 5 productos tienen demanda intencionalmente mayor (muestra didactica).
 SIN cobros bancarios reales. SIN movimientos de stock.
 Almacena Banco='DEMO - NO REAL', NumeroFactura 'DEMOPRED-EX-*'.
 Ejecutar UNA sola vez. Si ya existe el lote, cancela sin duplicar.
 Elimina TODOS los DEMOPRED usando QuitarDatosDemoReportePredictivo.sql.
 Las ventas demo no constituyen prueba funcional del cobro ni de la caja.
 Recalcula los DVH y DVV de FacturaBuffet y DetalleFactura.
*/
USE [PadelGestDB];
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM dbo.FacturaBuffet
               WHERE NumeroFactura LIKE N'DEMOPRED-EX-%')
        THROW 51101, 'El lote DEMOPRED-EX ya existe. No se duplicaron facturas.', 1;

    DECLARE @DNI nvarchar(8) =
        (SELECT TOP (1) DNI FROM dbo.Cliente ORDER BY DNI);
    DECLARE @Usuario int =
        (SELECT TOP (1) IdUsuario FROM dbo.Usuario WHERE Activo = 1 ORDER BY IdUsuario);

    IF @DNI IS NULL OR @Usuario IS NULL
        THROW 51102, 'Se requiere un cliente y un usuario activo en la base de pruebas.', 1;

    DECLARE @Productos TABLE
    (
        Numero int NOT NULL PRIMARY KEY,
        Codigo int NOT NULL,
        Precio decimal(12,2) NOT NULL
    );

    INSERT INTO @Productos (Numero, Codigo, Precio)
    SELECT ROW_NUMBER() OVER (ORDER BY CodigoProducto),
           CodigoProducto, PrecioUnitario
    FROM dbo.Producto WHERE Activo = 1 AND PrecioUnitario > 0;

    DECLARE @TotalProductos int = (SELECT COUNT(*) FROM @Productos);
    IF @TotalProductos < 3
        THROW 51103, 'Se requieren al menos tres productos activos con precio.', 1;

    DECLARE @Frecuentes int = CASE
        WHEN @TotalProductos >= 5 THEN 5 ELSE @TotalProductos END;
    DECLARE @N int = 1;

    WHILE @N <= 100
    BEGIN
        -- 100 operaciones distribuidas entre los 28 dias COMPLETOS anteriores a hoy.
        DECLARE @Dia int = 1 + ((@N * 17 + @N / 7) % 28);
        DECLARE @Fecha datetime2(7) = DATEADD(MINUTE, 480 + (@N % 12) * 43,
            DATEADD(DAY, -@Dia, CONVERT(datetime2(7), CONVERT(date, GETDATE()))));

        DECLARE @Numero nvarchar(30) =
            N'DEMOPRED-EX-' + RIGHT('0000' + CONVERT(varchar(4), @N), 4);
        DECLARE @IndiceA int = 1 + (@N % @Frecuentes);
        DECLARE @IndiceB int = 1 + ((@N * 7 + @N / 3) % @TotalProductos);
        IF @IndiceB = @IndiceA SET @IndiceB = 1 + (@IndiceB % @TotalProductos);
        DECLARE @IndiceC int = 1 + ((@N * 11 + 5) % @TotalProductos);
        WHILE @IndiceC IN (@IndiceA, @IndiceB)
            SET @IndiceC = 1 + (@IndiceC % @TotalProductos);

        DECLARE @CodigoA int, @CodigoB int, @CodigoC int;
        DECLARE @PrecioA decimal(12,2), @PrecioB decimal(12,2), @PrecioC decimal(12,2);
        SELECT @CodigoA = Codigo, @PrecioA = Precio FROM @Productos WHERE Numero = @IndiceA;
        SELECT @CodigoB = Codigo, @PrecioB = Precio FROM @Productos WHERE Numero = @IndiceB;
        SELECT @CodigoC = Codigo, @PrecioC = Precio FROM @Productos WHERE Numero = @IndiceC;

        -- Productos A, B y C tienen distintas frecuencias de venta.
        DECLARE @CantidadA int = 4 + ((@N * 3) % 9);
        DECLARE @CantidadB int = 1 + ((@N * 5) % 5);
        DECLARE @CantidadC int = 1 + ((@N + 2) % 3);
        DECLARE @Importe decimal(12,2) =
            @CantidadA * @PrecioA + @CantidadB * @PrecioB + @CantidadC * @PrecioC;

        INSERT INTO dbo.FacturaBuffet
            (NumeroFactura, DNI, IdVendedor, FechaHoraEmision, ImporteTotal,
             Estado, Banco, Ultimos4Tarjeta, FechaHoraPago, CodigoAutorizacion)
        VALUES (@Numero, @DNI, @Usuario, @Fecha, @Importe,
                N'Pagada', N'DEMO - NO REAL', '0000', @Fecha, @Numero);

        DECLARE @IdFactura int = CONVERT(int, SCOPE_IDENTITY());
        INSERT INTO dbo.DetalleFactura
            (IdFactura, CodigoProducto, Cantidad, PrecioUnitario)
        VALUES
            (@IdFactura, @CodigoA, @CantidadA, @PrecioA),
            (@IdFactura, @CodigoB, @CantidadB, @PrecioB),
            (@IdFactura, @CodigoC, @CantidadC, @PrecioC);

        SET @N += 1;
    END;

    -- Calcular DVH con el orden de columnas de TablasProtegidas.
    -- .NET InvariantCulture: DateTime MM/dd/yyyy HH:mm:ss y decimal(12,2) 0.00.
    UPDATE f SET DVH = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max), CONCAT(
            f.IdFactura, '|', f.NumeroFactura, '|', f.DNI, '|', f.IdVendedor, '|',
            FORMAT(f.FechaHoraEmision,'MM/dd/yyyy HH:mm:ss','en-US'), '|',
            FORMAT(f.ImporteTotal,'0.00','en-US'), '|', f.Estado, '|', f.Banco, '|',
            f.Ultimos4Tarjeta, '|',
            FORMAT(f.FechaHoraPago,'MM/dd/yyyy HH:mm:ss','en-US'), '|',
            f.CodigoAutorizacion))), 2))
    FROM dbo.FacturaBuffet f
    WHERE f.NumeroFactura LIKE N'DEMOPRED-EX-%';

    UPDATE d SET DVH = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max), CONCAT(
            d.IdDetalleFactura, '|', d.IdFactura, '|', d.CodigoProducto, '|',
            d.Cantidad, '|', FORMAT(d.PrecioUnitario,'0.00','en-US'), '|',
            FORMAT(d.Subtotal,'0.00','en-US'))), 2))
    FROM dbo.DetalleFactura d
    INNER JOIN dbo.FacturaBuffet f ON f.IdFactura = d.IdFactura
    WHERE f.NumeroFactura LIKE N'DEMOPRED-EX-%';

    -- DVV = SHA-256 de TODOS los DVH de cada tabla, ordenados por clave primaria.
    DECLARE @DVVFacturas varchar(64) = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max), ISNULL(
            (SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
             WITHIN GROUP (ORDER BY IdFactura)
             FROM dbo.FacturaBuffet), ''))), 2));

    DECLARE @DVVDetalles varchar(64) = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max), ISNULL(
            (SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
             WITHIN GROUP (ORDER BY IdDetalleFactura)
             FROM dbo.DetalleFactura), ''))), 2));

    UPDATE dbo.DigitoVerificador SET DVV = @DVVFacturas
    WHERE NombreTabla = N'FacturaBuffet';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV)
        VALUES (N'FacturaBuffet', @DVVFacturas);

    UPDATE dbo.DigitoVerificador SET DVV = @DVVDetalles
    WHERE NombreTabla = N'DetalleFactura';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV)
        VALUES (N'DetalleFactura', @DVVDetalles);

    COMMIT TRANSACTION;
    SELECT COUNT(*) AS FacturasAdicionales
    FROM dbo.FacturaBuffet WHERE NumeroFactura LIKE N'DEMOPRED-EX-%';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
