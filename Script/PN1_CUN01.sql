USE [PadelGestDB]
GO

/*
    PN1 - CUN01 Seleccionar Turno
    Estructura inicial de Cancha, Tarifa y Reserva.

    BitacoraEvento permanece excluida del sistema de Digitos Verificadores.
    Las nuevas tablas de negocio incluyen DVH y tienen su entrada de DVV
    en la tabla DigitoVerificador.
*/

IF OBJECT_ID(N'[dbo].[Cancha]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Cancha]
    (
        [IdCancha] [int] IDENTITY(1,1) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Estado] [nvarchar](30) NOT NULL,
        [DVH] [varchar](64) NULL,

        CONSTRAINT [PK_Cancha] PRIMARY KEY CLUSTERED ([IdCancha] ASC),
        CONSTRAINT [UQ_Cancha_Nombre] UNIQUE ([Nombre])
    );
END
GO

IF OBJECT_ID(N'[dbo].[Tarifa]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Tarifa]
    (
        [IdTarifa] [int] IDENTITY(1,1) NOT NULL,
        [TipoTarifa] [nvarchar](50) NOT NULL,
        [Importe] [decimal](12,2) NOT NULL,
        [HoraDesde] [time](0) NOT NULL,
        [HoraHasta] [time](0) NOT NULL,
        [Activo] [bit] NOT NULL CONSTRAINT [DF_Tarifa_Activo] DEFAULT ((1)),
        [DVH] [varchar](64) NULL,

        CONSTRAINT [PK_Tarifa] PRIMARY KEY CLUSTERED ([IdTarifa] ASC),
        CONSTRAINT [CK_Tarifa_Importe] CHECK ([Importe] >= 0),
        CONSTRAINT [CK_Tarifa_Horario] CHECK ([HoraDesde] < [HoraHasta])
    );
END
GO

IF OBJECT_ID(N'[dbo].[Reserva]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Reserva]
    (
        [IdReserva] [int] IDENTITY(1,1) NOT NULL,
        [IdCancha] [int] NOT NULL,
        [IdTarifa] [int] NOT NULL,
        [Fecha] [date] NOT NULL,
        [Horario] [time](0) NOT NULL,
        [Estado] [nvarchar](30) NOT NULL,
        [DVH] [varchar](64) NULL,

        CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED ([IdReserva] ASC),
        CONSTRAINT [FK_Reserva_Cancha] FOREIGN KEY ([IdCancha])
            REFERENCES [dbo].[Cancha] ([IdCancha]),
        CONSTRAINT [FK_Reserva_Tarifa] FOREIGN KEY ([IdTarifa])
            REFERENCES [dbo].[Tarifa] ([IdTarifa])
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[DigitoVerificador] WHERE [NombreTabla] = N'Cancha')
BEGIN
    INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV])
    VALUES (N'Cancha', NULL);
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[DigitoVerificador] WHERE [NombreTabla] = N'Tarifa')
BEGIN
    INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV])
    VALUES (N'Tarifa', NULL);
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[DigitoVerificador] WHERE [NombreTabla] = N'Reserva')
BEGIN
    INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV])
    VALUES (N'Reserva', NULL);
END
GO
