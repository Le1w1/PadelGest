USE [PadelGestDB];
GO

-- Ejecutar una sola vez en una BD existente (se puede repetir sin duplicar datos).
-- El permiso se asigna al rol Administrador, no a un usuario especifico.
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @IdRol int = (SELECT TOP (1) IdRol FROM dbo.Rol WHERE Nombre = N'Administrador');
    IF @IdRol IS NULL
        THROW 50001, 'No se encontro el rol Administrador.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.PermisoSimple WHERE Codigo = N'CLI_DESENCRIPTAR_TELEFONO')
    BEGIN
        INSERT INTO dbo.PermisoSimple (Codigo, Nombre)
        VALUES (N'CLI_DESENCRIPTAR_TELEFONO', N'Desencriptar telefono de cliente');
    END;

    DECLARE @IdPermiso int = (
        SELECT TOP (1) IdPermisoSimple
        FROM dbo.PermisoSimple WHERE Codigo = N'CLI_DESENCRIPTAR_TELEFONO'
    );

    IF NOT EXISTS (
        SELECT 1 FROM dbo.Rol_PermisoSimple
        WHERE IdRol = @IdRol AND IdPermisoSimple = @IdPermiso
    )
    BEGIN
        INSERT INTO dbo.Rol_PermisoSimple (IdRol, IdPermisoSimple)
        VALUES (@IdRol, @IdPermiso);
    END;

    -- Misma formula SHA-256 que utiliza PadelGest: concatenacion con "|" en orden de columnas.
    UPDATE dbo.PermisoSimple
    SET DVH = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
        CONVERT(varchar(max),
            CONCAT(CONVERT(varchar(12), IdPermisoSimple), '|', Codigo, '|', Nombre)
        )), 2))
    WHERE IdPermisoSimple = @IdPermiso;

    UPDATE dbo.Rol_PermisoSimple
    SET DVH = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
        CONVERT(varchar(max),
            CONCAT(CONVERT(varchar(12), IdRol), '|', CONVERT(varchar(12), IdPermisoSimple))
        )), 2))
    WHERE IdRol = @IdRol AND IdPermisoSimple = @IdPermiso;

    -- DVV = SHA-256 de todos los DVH ordenados por clave primaria, unidos por "|".
    DECLARE @DVVPermisos varchar(64) = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
        CONVERT(varchar(max), ISNULL((
            SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
                   WITHIN GROUP (ORDER BY IdPermisoSimple)
            FROM dbo.PermisoSimple
        ), ''))
    ), 2));

    DECLARE @DVVRolesPermisos varchar(64) = LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256',
        CONVERT(varchar(max), ISNULL((
            SELECT STRING_AGG(CONVERT(varchar(max), DVH), '|')
                   WITHIN GROUP (ORDER BY IdRol, IdPermisoSimple)
            FROM dbo.Rol_PermisoSimple
        ), ''))
    ), 2));

    UPDATE dbo.DigitoVerificador SET DVV = @DVVPermisos WHERE NombreTabla = N'PermisoSimple';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV) VALUES (N'PermisoSimple', @DVVPermisos);

    UPDATE dbo.DigitoVerificador SET DVV = @DVVRolesPermisos WHERE NombreTabla = N'Rol_PermisoSimple';
    IF @@ROWCOUNT = 0
        INSERT INTO dbo.DigitoVerificador (NombreTabla, DVV) VALUES (N'Rol_PermisoSimple', @DVVRolesPermisos);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
