-- PadelGest - T03: habilitar reporte predictivo SOLO al rol Dueño.
-- Ejecutar en SSMS contra la base existente. No borra ni modifica ventas, clientes o stock.
-- Requiere SQL Server 2019 o posterior. Compatible con ejecuciones repetidas.
USE [PadelGestDB];
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @IdDueno int = (
        SELECT TOP (1) IdRol FROM dbo.Rol
        WHERE Nombre IN (N'Dueño', N'Dueno')
        ORDER BY IdRol
    );
    IF @IdDueno IS NULL
        THROW 51001, 'No se encontro el rol Dueno. Verifique su nombre en dbo.Rol.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.PermisoSimple WHERE Codigo = N'REP_PREDICTIVO')
    BEGIN
        INSERT INTO dbo.PermisoSimple (Codigo, Nombre)
        VALUES (N'REP_PREDICTIVO', N'Consultar reporte predictivo de buffet');
    END;

    DECLARE @IdPermiso int = (
        SELECT TOP (1) IdPermisoSimple FROM dbo.PermisoSimple
        WHERE Codigo = N'REP_PREDICTIVO'
    );

    IF NOT EXISTS (
        SELECT 1 FROM dbo.Rol_PermisoSimple
        WHERE IdRol = @IdDueno AND IdPermisoSimple = @IdPermiso
    )
    BEGIN
        INSERT INTO dbo.Rol_PermisoSimple (IdRol, IdPermisoSimple)
        VALUES (@IdDueno, @IdPermiso);
    END;

    -- Actualizar DVH de los registros afectados con el mismo criterio
    -- de las tablas protegidas de PadelGest.
    UPDATE dbo.PermisoSimple SET DVH = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max),
            CONCAT(CONVERT(varchar(12), IdPermisoSimple), '|', Codigo, '|', Nombre))), 2))
    WHERE IdPermisoSimple = @IdPermiso;

    UPDATE dbo.Rol_PermisoSimple SET DVH = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max),
            CONCAT(CONVERT(varchar(12), IdRol), '|',
                   CONVERT(varchar(12), IdPermisoSimple)))), 2))
    WHERE IdRol = @IdDueno AND IdPermisoSimple = @IdPermiso;

    -- DVV: SHA-256 de los DVH en orden de clave primaria.
    DECLARE @DVVPermisos varchar(64) = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max),
            ISNULL((SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
                WITHIN GROUP (ORDER BY IdPermisoSimple)
                FROM dbo.PermisoSimple), ''))), 2));

    DECLARE @DVVRolesPermisos varchar(64) = LOWER(CONVERT(varchar(64),
        HASHBYTES('SHA2_256', CONVERT(varchar(max),
            ISNULL((SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
                WITHIN GROUP (ORDER BY IdRol, IdPermisoSimple)
                FROM dbo.Rol_PermisoSimple), ''))), 2));

    UPDATE dbo.DigitoVerificador
    SET DVV = @DVVPermisos WHERE NombreTabla = N'PermisoSimple';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV)
        VALUES (N'PermisoSimple', @DVVPermisos);

    UPDATE dbo.DigitoVerificador
    SET DVV = @DVVRolesPermisos WHERE NombreTabla = N'Rol_PermisoSimple';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV)
        VALUES (N'Rol_PermisoSimple', @DVVRolesPermisos);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Verificacion de asignacion directa (no incluye permisos heredados).
SELECT r.Nombre AS Rol, p.Codigo AS Permiso
FROM dbo.Rol_PermisoSimple rp
JOIN dbo.Rol r ON r.IdRol = rp.IdRol
JOIN dbo.PermisoSimple p ON p.IdPermisoSimple = rp.IdPermisoSimple
WHERE p.Codigo = N'REP_PREDICTIVO';
GO
