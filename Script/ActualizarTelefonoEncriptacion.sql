USE [PadelGestDB]
GO

-- Ampliar la columna para almacenar el telefono encriptado en Base64.
ALTER TABLE [dbo].[Cliente]
ALTER COLUMN [Telefono] NVARCHAR(100) NOT NULL;
GO
