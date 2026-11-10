/* PADELGEST - SQL Server 2019+ - INSTALACION LIMPIA
 SOLO primera instalacion, no ejecutar sobre PadelGestDB existente.
 Contiene estructura, datos iniciales, permisos y admin1. Sin operaciones.
 Archivo armado a partir del esquema ScriptBD.sql de entrega-3.
*/
USE [master];
GO
IF DB_ID(N'PadelGestDB') IS NOT NULL
    THROW 51000, 'La base PadelGestDB ya existe; no la sobrescriba.', 1;
GO
-- La instancia puede tener otra base asociada a archivos llamados
-- PadelGestDB.mdf / PadelGestDB_log.ldf (por ejemplo, una base de pruebas).
-- Se crean archivos fisicos NUEVOS con nombres unicos, sin tocar esos archivos.
-- El nombre LOGICO de la base permanece PadelGestDB.
DECLARE @RutaDatos nvarchar(4000) =
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath'));
DECLARE @RutaLog nvarchar(4000) =
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultLogPath'));
DECLARE @Sufijo varchar(32) = REPLACE(CONVERT(varchar(36), NEWID()), '-', '');
DECLARE @ArchivoDatos nvarchar(4000);
DECLARE @ArchivoLog nvarchar(4000);
DECLARE @Comando nvarchar(max);

IF @RutaDatos IS NULL OR @RutaLog IS NULL
    THROW 51001, 'SQL Server no informa las carpetas predeterminadas de datos y logs.', 1;

IF RIGHT(@RutaDatos, 1) NOT IN (N'\', N'/')
    SET @RutaDatos = @RutaDatos + N'\';
IF RIGHT(@RutaLog, 1) NOT IN (N'\', N'/')
    SET @RutaLog = @RutaLog + N'\';

SET @ArchivoDatos = @RutaDatos + N'PadelGestDB_' + @Sufijo + N'.mdf';
SET @ArchivoLog = @RutaLog + N'PadelGestDB_' + @Sufijo + N'_log.ldf';

SET @Comando =
    N'CREATE DATABASE [PadelGestDB] ON PRIMARY (' +
    N'NAME = N''PadelGestDB_Data'', FILENAME = N''' +
    REPLACE(@ArchivoDatos, N'''', N'''''') +
    N''') LOG ON (' +
    N'NAME = N''PadelGestDB_Log'', FILENAME = N''' +
    REPLACE(@ArchivoLog, N'''', N'''''') + N''')';

EXEC sys.sp_executesql @Comando;
GO
USE [PadelGestDB];
GO
/****** Object:  Table [dbo].[BitacoraEvento]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitacoraEvento](
	[IdEvento] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Usuario] [nvarchar](150) NOT NULL,
	[FechaHora] [datetime2](7) NOT NULL,
	[Modulo] [nvarchar](100) NOT NULL,
	[Accion] [nvarchar](150) NOT NULL,
	[Criticidad] [nvarchar](50) NOT NULL,
	[Resultado] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_BitacoraEvento] PRIMARY KEY CLUSTERED 
(
	[IdEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cancha]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cancha](
	[IdCancha] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Estado] [nvarchar](30) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Cancha] PRIMARY KEY CLUSTERED 
(
	[IdCancha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[DNI] [nvarchar](8) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](100) NOT NULL,
	[CorreoElectronico] [nvarchar](150) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[IdDetalleFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [int] NOT NULL,
	[CodigoProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](12, 2) NOT NULL,
	[Subtotal]  AS (CONVERT([decimal](18,2),CONVERT([decimal](18,2),[Cantidad])*[PrecioUnitario])) PERSISTED,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[IdDetalleFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitoVerificador](
	[NombreTabla] [nvarchar](100) NOT NULL,
	[DVV] [varchar](64) NULL,
 CONSTRAINT [PK_DigitoVerificador] PRIMARY KEY CLUSTERED 
(
	[NombreTabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipamiento]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipamiento](
	[IdEquipamiento] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [nvarchar](30) NOT NULL,
	[Importe] [decimal](12, 2) NOT NULL,
	[StockDisponible] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Equipamiento] PRIMARY KEY CLUSTERED 
(
	[IdEquipamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdCancha] [int] NOT NULL,
	[IdTarifa] [int] NOT NULL,
	[FechaHoraEmision] [datetime2](7) NOT NULL,
	[FechaReserva] [date] NOT NULL,
	[Horario] [time](0) NOT NULL,
	[CantidadPaletas] [int] NOT NULL,
	[CantidadPelotas] [int] NOT NULL,
	[ImporteTarifa] [decimal](12, 2) NOT NULL,
	[ImporteEquipamiento] [decimal](12, 2) NOT NULL,
	[ImporteTotal] [decimal](12, 2) NOT NULL,
	[Estado] [nvarchar](20) NOT NULL,
	[DVH] [varchar](64) NULL,
	[DNI] [nvarchar](8) NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaBuffet]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaBuffet](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[NumeroFactura] [nvarchar](30) NOT NULL,
	[DNI] [nvarchar](8) NOT NULL,
	[IdVendedor] [int] NOT NULL,
	[FechaHoraEmision] [datetime2](7) NOT NULL,
	[ImporteTotal] [decimal](12, 2) NOT NULL,
	[Estado] [nvarchar](20) NOT NULL,
	[Banco] [nvarchar](80) NOT NULL,
	[Ultimos4Tarjeta] [char](4) NOT NULL,
	[FechaHoraPago] [datetime2](7) NOT NULL,
	[CodigoAutorizacion] [nvarchar](50) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_FacturaBuffet] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[IdFamilia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia_Familia]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia_Familia](
	[IdFamiliaPadre] [int] NOT NULL,
	[IdFamiliaHija] [int] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Familia_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamiliaPadre] ASC,
	[IdFamiliaHija] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma](
	[IdIdioma] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](10) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 
(
	[IdIdioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermisoSimple]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermisoSimple](
	[IdPermisoSimple] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_PermisoSimple] PRIMARY KEY CLUSTERED 
(
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermisoSimple_Familia]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermisoSimple_Familia](
	[IdFamilia] [int] NOT NULL,
	[IdPermisoSimple] [int] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_PermisoSimple_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC,
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[CodigoProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](120) NOT NULL,
	[Categoria] [nvarchar](80) NOT NULL,
	[PrecioUnitario] [decimal](12, 2) NOT NULL,
	[StockDisponible] [int] NOT NULL,
	[StockMinimo] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto_C]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto_C](
	[IdCambio] [bigint] IDENTITY(1,1) NOT NULL,
	[CodigoProducto] [int] NOT NULL,
	[FechaHora] [datetime2](3) NOT NULL,
	[Nombre] [nvarchar](120) NOT NULL,
	[Categoria] [nvarchar](80) NOT NULL,
	[PrecioUnitario] [decimal](12, 2) NOT NULL,
	[StockDisponible] [int] NOT NULL,
	[StockMinimo] [int] NOT NULL,
	[ActivoProducto] [bit] NOT NULL,
	[Act] [bit] NOT NULL,
	[TipoCambio] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Producto_C] PRIMARY KEY CLUSTERED 
(
	[IdCambio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[IdCancha] [int] NOT NULL,
	[IdTarifa] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Horario] [time](0) NOT NULL,
	[Estado] [nvarchar](30) NOT NULL,
	[DVH] [varchar](64) NULL,
	[Codigo] [nvarchar](30) NOT NULL,
	[IdFactura] [int] NOT NULL,
	[CantidadPaletas] [int] NOT NULL,
	[CantidadPelotas] [int] NOT NULL,
	[DNI] [nvarchar](8) NOT NULL,
	[Banco] [nvarchar](80) NOT NULL,
	[Ultimos4Tarjeta] [char](4) NOT NULL,
	[ImportePago] [decimal](12, 2) NOT NULL,
	[FechaHoraPago] [datetime2](7) NOT NULL,
	[EstadoPago] [nvarchar](20) NOT NULL,
	[CodigoAutorizacion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RespaldoBD]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespaldoBD](
	[IdRespaldo] [int] IDENTITY(1,1) NOT NULL,
	[FechaHora] [datetime2](7) NOT NULL,
	[TipoOperacion] [nvarchar](20) NOT NULL,
	[RutaArchivo] [nvarchar](500) NOT NULL,
	[NombreArchivo] [nvarchar](200) NOT NULL,
	[Resultado] [nvarchar](20) NOT NULL,
	[Descripcion] [nvarchar](500) NULL,
	[DVH] [varchar](64) NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_RespaldoBD] PRIMARY KEY CLUSTERED 
(
	[IdRespaldo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_Familia]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Familia](
	[IdRol] [int] NOT NULL,
	[IdFamilia] [int] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Rol_Familia] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_PermisoSimple]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_PermisoSimple](
	[IdRol] [int] NOT NULL,
	[IdPermisoSimple] [int] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Rol_PermisoSimple] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarifa]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarifa](
	[IdTarifa] [int] IDENTITY(1,1) NOT NULL,
	[TipoTarifa] [nvarchar](50) NOT NULL,
	[Importe] [decimal](12, 2) NOT NULL,
	[HoraDesde] [time](0) NOT NULL,
	[HoraHasta] [time](0) NOT NULL,
	[Activo] [bit] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Tarifa] PRIMARY KEY CLUSTERED 
(
	[IdTarifa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[DNI] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[NombreUsuario] [nvarchar](100) NOT NULL,
	[PasswordHash] [char](64) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Bloqueado] [bit] NOT NULL,
	[IntentosFallidos] [int] NOT NULL,
	[DebeCambiarClave] [bit] NOT NULL,
	[IdIdioma] [int] NOT NULL,
	[IdRol] [int] NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cancha] ON;
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (1, N'Cancha 1', N'Disponible', N'e6e8bcf57788150e413e016d20f8de662b731665f5b7e1071025cb1919d07ce3')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (2, N'Cancha 2', N'Disponible', N'0aaac307006e492dabf3cfdbe1bc09849eaf4a9947d3753ec0a26bc94a3cc329')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (3, N'Cancha 3', N'Disponible', N'e76a1e12447a651bd0aa24debb03ca1a4df00165df4fa548c51fa6f4fcd9d128')
SET IDENTITY_INSERT [dbo].[Cancha] OFF;
GO
SET IDENTITY_INSERT [dbo].[Equipamiento] ON;
INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (1, N'Paleta', CAST(8000.00 AS Decimal(12, 2)), 8, 1, N'345646ab25ac506c73da5a5ecdcd9dd10858d9a5d96d33c46ab6ccf715f6a6f1')
INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (2, N'Pelota', CAST(1500.00 AS Decimal(12, 2)), 12, 1, N'27f8a517152ae46590b19ac12c70883c73339a1f60d04dc994ed76d8b754c9c6')
SET IDENTITY_INSERT [dbo].[Equipamiento] OFF;
GO
SET IDENTITY_INSERT [dbo].[Tarifa] ON;
INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (1, N'Diurna', CAST(30000.00 AS Decimal(12, 2)), CAST(N'08:30:00' AS Time), CAST(N'17:30:00' AS Time), 1, N'01d171f677f4fac8f86c9e8a44e8f8903a9ffaff7adf787d72c19f2bafdb3642')
INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (2, N'Nocturna', CAST(45000.00 AS Decimal(12, 2)), CAST(N'17:30:00' AS Time), CAST(N'22:00:00' AS Time), 1, N'dc1b62070c4361943e0b14a1c93dd2f52d3b09fb7532d1a091871ad7a4bcda3a')
SET IDENTITY_INSERT [dbo].[Tarifa] OFF;
GO
SET IDENTITY_INSERT [dbo].[Idioma] ON;
INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (1, N'ES', N'Español', N'1f83a4462935948663a6223389e13dab529e0db28bbf75ad071f08d161654e73')
INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (2, N'EN', N'English', N'b98cc3676414c0928ab8a26a2e721b1400a585267b969cd4b35b87a2b6d88b9f')
SET IDENTITY_INSERT [dbo].[Idioma] OFF;
GO
SET IDENTITY_INSERT [dbo].[Rol] ON;
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (1, N'Administrador', N'e968303d5a0735207eb00fd12bf270e9d83f5e269fd9bab80a26424e3954441f')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (2, N'Vendedor de Buffet', N'8fcd2473f9162ea7f574ab6fda26376b775efd5a2a7188d53648104f8d5ff67b')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (3, N'Recepcionista', N'9d65b912fda8c9906289143693ab2b3999de7c6430c4a6e001e7f57188e24b8c')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (4, N'Dueño', N'08f785674ea0f3e5871d825ef0977cf95f484245afc3195e7b5f82a014031d92')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (5, N'Encargado de Canchas', N'714816c1e538b87ea88b9540cce7d1f1afaf7eb6e5888ca34bf904630261ba56')
SET IDENTITY_INSERT [dbo].[Rol] OFF;
GO
SET IDENTITY_INSERT [dbo].[Familia] ON;
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (1, N'Sesión', N'8e7a9630253803d9fabd34ed9f974b1478805cac0f83f50375d7111f1ed2a39f')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (2, N'Gestión de Usuarios', N'88cb8da76653546918837cb79f7e5ae674e369c099953018b3b5eaccd3bba807')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (3, N'Bitácora', N'0ccc7005cfabb92cbac4fadf23553a1a26953dbf6b1cb5b0e33d614591ee8be5')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (4, N'RBAC', N'b7e2e53022e5e83073fc6e73189d6119493fafbd592b3de0163b3aaaf9c4db7a')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (5, N'Reservas', N'4f86698cec518bf106fdc37b75cd21f66c9476a6d8c7112e323b79491217f96e')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (6, N'Clientes', N'1b6cf98dffb4309a85c5504ba4439f5e746f8ee06b1bc1301ec352f6f772e2f4')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (7, N'Buffet', N'c3dada9b953d968c56dc27ea8d503f3988caf4db6fda9473b672a49c4e265ea9')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (8, N'Administración Total', N'2bcfc06f48977987ce2108e3e6b623735737b1976191816c53ed8ba93b5ab278')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (13, N'Canchas', N'e9d02c0a64e7727f9380cb3a264ceb60fedfa9918d25e562c735da7d2648f63e')
SET IDENTITY_INSERT [dbo].[Familia] OFF;
GO
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 2, N'a9e264cb4c953f958e10a18334af28d3229562f48c1ec33c0a5f94df26dcb5d9')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 3, N'29694ce63ca04a055a1f6284f59acd208be2cb74edc443689f231def0f55c18e')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 4, N'9ccad5595eaedbf07fc1ada91dcd2805c6c544430424ce91c69f709c02a52103')
GO
SET IDENTITY_INSERT [dbo].[PermisoSimple] ON;
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (1, N'USR_LISTAR', N'Listar Usuarios', N'9273818afb62c8ba73903ec8c9b417e26e24dbec3354cc8deb13d28ce063b328')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (2, N'USR_CREAR', N'Crear Usuario', N'4cf1d161bc5843d1a550b686ff489f2c9eae275cfaaacccb3bec924e1b341ad0')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (3, N'USR_MODIFICAR', N'Modificar Usuario', N'ffbdd81fee417dcabab9fe699945cb49ce2579f958b2677168ca6376d6798521')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (4, N'USR_ACTIVAR_DESACTIVAR', N'Activar / Desactivar Usuario', N'b517c71b0358096d030e17993a9ed13800608c25c5ece710fc2d827c08bd536c')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (5, N'USR_DESBLOQUEAR', N'Desbloquear Usuario', N'4ef634c37cbf8bc6972391c1b068dbde9e725ea3d0ae37ad1832b67d50ddd25c')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (6, N'USR_ASIGNAR_ROL', N'Asignar Rol a Usuario', N'3e768c3666455dd86b55258a949aa1ba648a642a93f723c538b114026eb8a806')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (7, N'BIT_AUDITAR', N'Auditar Bitácora de Eventos', N'cbd8b9d45164607a0b619860f7299a20243ad3298f6d72408bd30e7665d165ad')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (8, N'BIT_IMPRIMIR_PDF', N'Imprimir Bitácora a PDF', N'28bc9aa53acc019b9d6463a532042b7750b71d74be277667c2d7517738c1b0b2')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (9, N'ROL_LISTAR', N'Listar Roles', N'e9a3329fb3dc17901306a5c65204d38b395355648143d4938e6ce10f4e811605')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (10, N'ROL_GESTIONAR', N'Gestionar Roles (crear/modificar/eliminar)', N'bed27a127eb970581ac6237c31b0de65b6671d3340d9255ef136c79b28850c79')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (11, N'FAM_LISTAR', N'Listar Familias', N'922a223aec2f011224735695aaae52175315ebb542ddc88d2ca8a2e5ab0213c8')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (12, N'FAM_GESTIONAR', N'Gestionar Familias (crear/modificar/eliminar)', N'0469f49ffab4dff2162849613b067b0f874741eaafbc1e47a82ab5c035356dea')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (13, N'PS_LISTAR', N'Listar Permisos Simples', N'0557be4f32f412490c927dd153c38650155fe18ae6fa4153807ce84a5ec7c996')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (14, N'SES_RELOGIN', N'Re-Login en sesión activa', N'15a087c6879f43173c2f8a8653c8fc16d3a8ea71732a89a62b22a9302f98a9f7')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (15, N'SES_CAMBIAR_CLAVE', N'Cambiar Clave propia', N'49478eaf365fe1e3d3fbcc8d6283bd26b61e8fc11cbbc64341c39aac2fd20497')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (16, N'SES_CAMBIAR_IDIOMA', N'Cambiar Idioma', N'5995c3e9050d8a3614cdaec530c668bf67af824a4133adfad37773720b6b9483')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (17, N'SES_CERRAR', N'Cerrar Sesión', N'2b39b772fec8af900e0eb59236fcd31b7f8a86a1e16a9e171831aff6b3a7acb8')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (18, N'RES_CREAR', N'Crear Reserva', N'fb70e38fb0bcc6c458eefba3c10bef6ece09f95c0c66c286df947d952b666f2c')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (19, N'RES_CONSULTAR', N'Consultar Reservas', N'49b94026a7409ca19cdc9287c2b38a32ad52e9f1c5ccc3a231be1d2d97202bbe')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (20, N'RES_CONFIRMAR', N'Confirmar Reserva', N'8f109fee50a2772593ed9ce57b28d1c6d75763beeba24d714129149ce7d4a412')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (21, N'RES_REPROGRAMAR', N'Reprogramar Reserva', N'8d4a4e6fac0d257b423019be1e31fa62d620745fe941112d3e6e1b0dbaacf613')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (22, N'RES_CANCELAR', N'Cancelar Reserva', N'e539c686c911165f2b7c630a95b987a888de48b277a7ca3ebff7dd5dd04c98b9')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (23, N'CLI_CONSULTAR', N'Consultar Clientes', N'472ff97126660caa5450d233e973a9d570f6ad69401b3623808086c803ce2dad')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (24, N'CLI_REGISTRAR', N'Registrar Cliente', N'72ce69b71843bf5bc60d9129fa47b695c3f652eebcdb5cae9c58dd84c26dc31f')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (25, N'CLI_MODIFICAR', N'Modificar Cliente', N'599fc565d585208cce2a140a1d3b395b5936d42d8d0adcc11cd862ce8a8d634e')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (26, N'BUF_VENDER', N'Registrar Venta de Buffet', N'6286c0c330bdf2e5fcdb3b2c73beaba4ffb21ea6606ceb7dd961078ca81796c3')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (27, N'BUF_CONSULTAR_STOCK', N'Consultar Stock del Buffet', N'8316f15bbc562e8a4b02c2bf51173c13f16bb84973963aba7de4af7202a4f579')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (28, N'CAN_CONSULTAR_AGENDA', N'Consultar Agenda de Canchas', N'25eecef2df69e2458691670354b2ea9b5527e54b5dc51c934eeaa2fd3cef5053')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (29, N'BAK_GESTIONAR', N'Gestionar Respaldos (backup/restore)', N'a150b27a8d8e9fc3719ccc14c2e1c514709558f14332f4416441e9f6399c4eab')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (30, N'PRO_GESTIONAR', N'Gestionar Productos (ABM)', N'fb18f409aa4a5c6c407473b60cab68adcb4c9da195c71277c8df19d0e923ec10')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (31, N'CLI_ELIMINAR', N'Eliminar Cliente sin operaciones asociadas', N'da471fc2ea1ab0140eec18058b1355c0a80f74ebb3f61100db8ecbe77251eb54')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (32, N'CLI_DESENCRIPTAR_TELEFONO', N'Desencriptar telefono de cliente', N'53d00f1f67f0c09841fda4a5e2972562c0f1b417da4be0889075e19868d3ffd7')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (33, N'REP_PREDICTIVO', N'Consultar reporte predictivo de buffet', N'b30e2e9aeed3e7c8fadff598f52836bbd4015d59334376c2698aa0f23cebe4a0')
SET IDENTITY_INSERT [dbo].[PermisoSimple] OFF;
GO
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (1, 14, N'0a23a5f1bb3666753e1bd12f1101c4d1c0ab9689208d2740d5b1a5249e742453')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (1, 15, N'0b6dee7de65773d3d568a1a762afcca89fc60b5f78bfeee0a22959bafd33100e')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (1, 16, N'2d6d84bdd9690f8cbe131bea6c6bda694098ed37afe52cbf0db24afeabcb7fe3')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (1, 17, N'9c5b9bdce93ece747e786a407b042f0f08b80ce9f590b06d8515cfb00560c692')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 1, N'ab8acbe9b03fadaf050f68d7ca94737fff7eed9c76bc50c5d53f9ee0ff779beb')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 2, N'48e0d1fa9385dc6a9d4bdd8f871c84ee3836c335a763932147ef153cf0c171ab')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 3, N'558093d56c894f6f1c0081327fca31da662868870adf915ed557ed9c777271b6')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 4, N'8afeca9cdcef231a9f7bb4365c0918d5f152e0301183ca83fbda015005b925c1')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 5, N'0345c8892098e4ce43dca763d1e8ec63ce2ffb2b27d454c55ad483a3961dc466')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (2, 6, N'a511f271b4fa2f44317f8bf0a9fcc545e817281aba70ece5b2166ddde6d71186')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (3, 7, N'c95255dc0d87c332988bf693b9d3dabb8552e23c331f01339527e393ce0aaa69')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (3, 8, N'134c6442ce2d83145faa525d1a72d0fbe67838f28a2c7308863a43277d5e01ae')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (4, 9, N'f53030391c52cbb2d93db53d92254385de2cf25466147628391c85d407379500')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (4, 10, N'ce17c2097404593e0dc50e0f54096ee2126fa3115a5f671e05e8c71e484464f9')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (4, 11, N'4238211e30eb4632999efcf8c7743b1abf8d1e41e45ffd846a500973df75ef88')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (4, 12, N'f8dd8910201d974e10664256abf096861b2df72e044aeeeeb2a8738df2691c48')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (4, 13, N'8c35684bf37f290ed4e35e4eb8e7fd2a5ec035e188191f443d0c11c6c969c9b8')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (5, 18, N'b510b8ffcc7aeb8f051ca4553dc47090eb3c4487acd79a5246287fe13b886c21')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (5, 19, N'07514a7a90b5305853c92157e83acfd532bf90d6d1ed764d9de2675a21edfc20')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (5, 20, N'6fe4ad66cf3f457eb8547a4e564544602a278c94e2e4deceac84cd8967996ae7')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (5, 21, N'da5ea41067509fe2d1ba55ee1cfeff9a2d4a9616307b32a25e4cd30feccddf48')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (5, 22, N'2f5c312554ae0544a3bbc9440d62b16b14350b5dec2342e903d4f7b87c04b903')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (6, 23, N'df08a08a029d54adb2b88c3ba28a8723d881cfc63b107a55a3a17dd3e516c005')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (6, 24, N'4378837d3436e6162074a4fec1af3d1001c7b37c3bd6e977e717c6d6e3e4bf4b')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (6, 25, N'b4e746256717b4b06ffccfb298eb390e16f086b4ef32d45bf7af360991419e04')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (6, 31, N'10c4ff1b3937f5c85bb7a036bdc3be48a1ee144bc045aa74f5ae0f1a7fdeeb1b')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (7, 26, N'7f155abfca97e134f158f3c6b4d04d6707ff181ae78c16cde5732d96186d022e')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (7, 27, N'43751bc31fa90e39010c41ca3956b248442525c24376a578e8584b6faabb978d')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (8, 29, N'e3c65737f4c3a978fdab7ffb65ff727596a48ef2233745b1c863acfe4ebe1deb')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (13, 28, N'90f395f0b6f1de9c74af4eab4a3a4aac2927f091fe479344113ba1c9355a976b')
GO
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (1, 1, N'3f22d6d875f4e1635c6b9f02ac6ef85c76fa33ca7a7593ea6bed1ef2c9997414')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (1, 8, N'444abbca8de8dc89ae7445fe27afc743f74ddfd2fae5be74c464b587e10198cc')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (2, 1, N'ab8acbe9b03fadaf050f68d7ca94737fff7eed9c76bc50c5d53f9ee0ff779beb')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (2, 7, N'61bc29ecfce8757bde6fbb33b61ecd759f744bc1c1f53db4ea5f2ae0059838ae')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (3, 1, N'81e96b53ef6b10d23617ec3286caaefa081820565aae60315ccab78733478d71')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (3, 5, N'a6dad5d245abd5061945ce56b487542e3233969b36a260e85a4c09556aab60de')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 1, N'b6d71685bd9ab5b3cd6aa85bc38ee89f8d4c6d8c9d7afb87b0a4ff1b0915027f')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 5, N'cebc03007ff90caa5f14b0d24681820822c75b97db9a3658fd2c06b796794c97')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 6, N'13ab4ed36916168913964f52b4b98215893947f7540ef25ad80ed34593f97b1f')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 7, N'a7c9457857e60130f01a26bed4d4d64249afbbf8f2e041ca4d8d74fd6098d0b7')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 8, N'a96c9eb63a01b9fd39e82288d2de9d814102466f8c0684da35cc22919adb435e')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 13, N'8c35684bf37f290ed4e35e4eb8e7fd2a5ec035e188191f443d0c11c6c969c9b8')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (5, 1, N'cac33d2c18b2084313c957a5641e259f69b9f0d5afa2cc4b825b585d6534ccdc')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (5, 13, N'8ec99bdedaf4cc509dabae02553a61d2e49c4969309086f082fcc956568aa859')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (1, 6, N'2baee7362a0b8cf0d1054da7236a64097d98870dda36504bf743ff89f475930e')
GO
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (1, 30, N'b10f6285b7a31102d191a7f4ea6812baab6f313d257a92154487a5988ac05cae')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 30, N'96866a4ce8b4f0299246ba15185965f942ab653dd00f06c743b4dc521f759266')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 32, N'e40dbfc826024b8cfad402a6d4b21b1817f6a8ea535fa239d14cb2e1c9556c26')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 33, N'5ac69ddc4dc620d0fa43b9c6ebcc7e34c18b18a99a3d9fed74acd7c609ca1842')
GO
SET IDENTITY_INSERT [dbo].[Producto] ON;
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (1, N'Agua mineral sin gas 500 ml', N'Bebidas', CAST(1500.00 AS Decimal(12, 2)), 26, 10, 1, N'663db81774fd1a80e68d8106c6f8252541abbf30f295854b8244f34f87991226')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (2, N'Agua mineral 1.5 L', N'Bebidas', CAST(2800.00 AS Decimal(12, 2)), 10, 4, 1, N'68c33413583bc27c382f84876858548fbee2ea08147d9f6803c6308ad867a0a7')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (3, N'Agua con gas 500 ml', N'Bebidas', CAST(1600.00 AS Decimal(12, 2)), 11, 4, 1, N'13a9b44c3327cbb8a1218c09253c8d09668b6cdc4371f8ef0a431e8e95768bcf')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (4, N'Gaseosa cola 500 ml', N'Bebidas', CAST(2900.00 AS Decimal(12, 2)), 23, 8, 1, N'd900a44183c55b6bf846bedff0abbb93602971184bec52a84ce34990f0925d56')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (5, N'Gaseosa lima limon 500 ml', N'Bebidas', CAST(2900.00 AS Decimal(12, 2)), 17, 6, 1, N'3c43376be8fa09c309c845d52397174cc57bbfebdbeaae8f445ab96ca0b48535')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (6, N'Gaseosa naranja 500 ml', N'Bebidas', CAST(2850.00 AS Decimal(12, 2)), 5, 5, 1, N'5bfa791a4f7f857e884843449e44219f0aecabf5622a2dbbd318d171823ba3e5')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (7, N'Bebida isotonica naranja 500 ml', N'Bebidas', CAST(3600.00 AS Decimal(12, 2)), 15, 5, 1, N'c302ccd16667401d5ca695f63958cb8a6265044990bce3e3285456be2a2f679b')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (8, N'Bebida isotonica limon 500 ml', N'Bebidas', CAST(3600.00 AS Decimal(12, 2)), 2, 5, 1, N'4b1d452d75f894fafe93f97cf0aa89b0f4c3731a45b6e17aa4672148de6d9d02')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (9, N'Jugo de naranja 500 ml', N'Bebidas', CAST(2600.00 AS Decimal(12, 2)), 8, 4, 1, N'6f373f1b12dfbfded54a80a84a1c0d03004ddc0ebcff4ba78873c53faffeba18')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (10, N'Energizante lata 250 ml', N'Bebidas', CAST(5200.00 AS Decimal(12, 2)), 7, 3, 1, N'2052ec9e37bd6b0ba70edf4a0708d1451e25fd28dffd8e389f25f46e1d299790')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (11, N'Cafe expreso', N'Cafeteria', CAST(2500.00 AS Decimal(12, 2)), 39, 10, 1, N'520ab7374a232264b0c128576a4adc100cacc650438e72f0a635019672208941')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (12, N'Cafe con leche', N'Cafeteria', CAST(3200.00 AS Decimal(12, 2)), 16, 5, 1, N'f45d1694bbccb5a206042f011745278fdea28d9e827f25d026376e0f12b8c80e')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (13, N'Te', N'Cafeteria', CAST(2000.00 AS Decimal(12, 2)), 15, 4, 1, N'28cf2c39a323419a12a15c167d41fe3d859a15b6c472078f721a729afbb7eea9')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (14, N'Chocolate caliente', N'Cafeteria', CAST(3400.00 AS Decimal(12, 2)), 8, 3, 1, N'2f4fc1516cca448f3fd88cb223a29bbb726b3b694a62b78b398277fb8b972f3a')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (15, N'Alfajor de chocolate', N'Snacks', CAST(2000.00 AS Decimal(12, 2)), 19, 6, 1, N'18851eab8a60c18f81268e4ed0ab8b956ae39c5696698a631847f72298b7de08')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (16, N'Alfajor de maicena', N'Snacks', CAST(2100.00 AS Decimal(12, 2)), 6, 4, 1, N'04ffa772f37e41b821dc18c3b6990efc795b4f0e486ee1b95fc6d328d031dafa')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (17, N'Barra de cereal', N'Snacks', CAST(1800.00 AS Decimal(12, 2)), 12, 4, 1, N'a07e13fe3202f53958e30038a415bd1b7a255c56dd9f57cfe2bfbe819fba89b5')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (18, N'Galletitas dulces', N'Snacks', CAST(2700.00 AS Decimal(12, 2)), 13, 4, 1, N'23b0394d33e9b387bb18db45497165b0a0089524dd452307f11deb88803cca6f')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (19, N'Galletitas saladas', N'Snacks', CAST(2600.00 AS Decimal(12, 2)), 7, 3, 1, N'68cc3ed3690cb59b66cd5f04a994c7323852932c530af691c0de8d0d1b810f94')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (20, N'Papas fritas paquete', N'Snacks', CAST(3400.00 AS Decimal(12, 2)), 8, 3, 1, N'2cb4375ca8a4ba251722f43ede50f96674e39b5325a0870046be6d2b7f814fe2')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (21, N'Mani salado', N'Snacks', CAST(2600.00 AS Decimal(12, 2)), 5, 5, 1, N'4367642307c6cdf867633cc859dbb132d4d0bbd6075d7f81b87c7962e2b6dafa')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (22, N'Tostado jamon y queso', N'Comidas', CAST(6500.00 AS Decimal(12, 2)), 10, 3, 1, N'e134fe410b867566c8542e459cb0797597f13fb6937e58e56154cdb28a6dc815')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (23, N'Sandwich de pollo', N'Comidas', CAST(7900.00 AS Decimal(12, 2)), 6, 2, 1, N'97ea16d4e577b7f7a2ab32878c6f791b00a5c6dbacef345d3626a01d84a202ee')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (24, N'Sandwich vegetariano', N'Comidas', CAST(7300.00 AS Decimal(12, 2)), 4, 2, 1, N'55b7f5f76e36e3fc6f735d308cd6daba721671970206e30bb2ac72518049b564')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (25, N'Medialuna', N'Comidas', CAST(1700.00 AS Decimal(12, 2)), 15, 5, 1, N'5c6533c5940db77e7b052dbc5315fb69cc5765043e6fae5a13901576405c4006')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (26, N'Porcion de pizza', N'Comidas', CAST(5800.00 AS Decimal(12, 2)), 9, 3, 1, N'79930a3b325c66866b8113a612c0fa71d1a1e189c95f1641eb18ccaa0fd4d8fc')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (27, N'Caramelos unidad', N'Golosinas', CAST(500.00 AS Decimal(12, 2)), 50, 16, 1, N'2e028bb11b37f7b16c894e6ae50892add6c8ead0152ea4fcf3381a98b3b54823')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (28, N'Chicle unidad', N'Golosinas', CAST(900.00 AS Decimal(12, 2)), 18, 5, 1, N'48140df4f71053945a6c854e47e0ee81e4278397d6f87ad8be9015695394e8e8')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (29, N'Galletitas Toddy', N'Snacks', CAST(2400.00 AS Decimal(12, 2)), 6, 5, 1, N'bea57c69f0a31f93e72d70d5ef4f658ea7731d9e7841d721dd204b5bbab586d4')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (30, N'Coca Cola', N'Bebidas', CAST(2100.00 AS Decimal(12, 2)), 10, 5, 1, N'8e6dcacd7008d9dbae7e47d3d0f47aa56e10bc36bb3777e1058650142640ce5d')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (33, N'Gaseosa Pepsi', N'Bebidas', CAST(3100.00 AS Decimal(12, 2)), 9, 5, 1, N'ec267048b8d5c1d1384790cf628dbe2a86b04791f0f4fc87836672a56edd8b51')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (34, N'Prueba', N'Bebidas', CAST(2000.00 AS Decimal(12, 2)), 20, 5, 1, N'8748bb097e7f71aec345fbe793b5cdbbadf0e3d0ef525f82e5ce7ef3eb6e92c9')
SET IDENTITY_INSERT [dbo].[Producto] OFF;
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON;
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (1, N'Administrador', N'Inicial', N'99999999', N'admin1@padelgest.com', N'admin1', N'5456cc85efead5b7762b131e99389aa4b861199686c6a38f1f2bd4154c61b409', 1, 0, 0, 1, 1, 1, N'a0aaad8687f78ad82c5ab61b29cfc3a029d0af07558072b50ebaee15d2d82d69')
SET IDENTITY_INSERT [dbo].[Usuario] OFF;
GO
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cancha', '91129657cbb5230efd409301669fd05b677f7331542c2192547bd4da1b7bd1bd');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cliente', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'DetalleFactura', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Equipamiento', '1fb47453d7dee2f1e004895302f0abd6dcaa3a456f677af737b2669d1fe365f5');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Factura', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'FacturaBuffet', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia', 'fdd294e73b3531a63b8e55aff07cf3c5d17090230a00fa065860d52642a31e97');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia_Familia', '18c42e63ad2b66da1312f3417e15d5d971f8a253fbfb4897fbeb267656f4be79');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Idioma', 'b8206deae9aaca74a25998ec9d26c6b2b41fdf430ccc455979a8aed7a298a234');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple', '2b08d33a277ea163b8e4334c7224848afd8895113a4b0320a33d1cb75068904d');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple_Familia', '6dae4d91f38e8843405b2fc1df655b1e4b8e1a8ad80017e68ee4220c54e7d8f5');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Producto', '185f1a91e4767f12457c1f5df8409e98321ef9c54f8f4cf32e849bfea564c2de');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Reserva', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'RespaldoBD', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol', '700ba87ff88d54c32d59eac310a40e63e20df37bb09a5a3f7191354a747a30d3');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_Familia', '32b5356d0241e9fa61aec0d6b60e4ce854b7b877378c67761cb47c05d0cae856');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_PermisoSimple', '175283370c8b36253f4e2bd4a48c2c0fbb94e7b8a62274b88b6ae950883d5314');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Tarifa', '9cc41aa9db8a17e67681d251c2d21eb70e08af85b8f741319d72704670345d13');
INSERT INTO [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Usuario', '043d767ba5095982bf68a31b3335097f338526cee72669584b4f34e035eefa30');
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Cancha_Nombre]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Cancha] ADD  CONSTRAINT [UQ_Cancha_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_DetalleFactura_FacturaProducto]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[DetalleFactura] ADD  CONSTRAINT [UQ_DetalleFactura_FacturaProducto] UNIQUE NONCLUSTERED 
(
	[IdFactura] ASC,
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetalleFactura_Producto]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_DetalleFactura_Producto] ON [dbo].[DetalleFactura]
(
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Equipamiento_Tipo]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Equipamiento] ADD  CONSTRAINT [UQ_Equipamiento_Tipo] UNIQUE NONCLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_FacturaBuffet_CodigoAutorizacion]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [UQ_FacturaBuffet_CodigoAutorizacion] UNIQUE NONCLUSTERED 
(
	[CodigoAutorizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_FacturaBuffet_NumeroFactura]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [UQ_FacturaBuffet_NumeroFactura] UNIQUE NONCLUSTERED 
(
	[NumeroFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FacturaBuffet_DNI_Fecha]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_FacturaBuffet_DNI_Fecha] ON [dbo].[FacturaBuffet]
(
	[DNI] ASC,
	[FechaHoraEmision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FacturaBuffet_Vendedor_Fecha]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_FacturaBuffet_Vendedor_Fecha] ON [dbo].[FacturaBuffet]
(
	[IdVendedor] ASC,
	[FechaHoraEmision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Familia_Nombre]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [UQ_Familia_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Idioma_Codigo]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Idioma] ADD  CONSTRAINT [UQ_Idioma_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_PermisoSimple_Codigo]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[PermisoSimple] ADD  CONSTRAINT [UQ_PermisoSimple_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Producto_ActivoCategoriaNombre]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_Producto_ActivoCategoriaNombre] ON [dbo].[Producto]
(
	[Activo] ASC,
	[Categoria] ASC,
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductoC_Consulta]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_ProductoC_Consulta] ON [dbo].[Producto_C]
(
	[CodigoProducto] ASC,
	[FechaHora] DESC,
	[IdCambio] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UX_ProductoC_VersionVigente]    Script Date: 3/11/2026 21:27:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_ProductoC_VersionVigente] ON [dbo].[Producto_C]
(
	[CodigoProducto] ASC
)
WHERE ([Act]=(1))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Reserva_Disponibilidad]    Script Date: 3/11/2026 21:27:19 ******/
CREATE NONCLUSTERED INDEX [IX_Reserva_Disponibilidad] ON [dbo].[Reserva]
(
	[IdCancha] ASC,
	[Fecha] ASC,
	[Horario] ASC,
	[Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Reserva_Codigo]    Script Date: 3/11/2026 21:27:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Reserva_Codigo] ON [dbo].[Reserva]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Reserva_IdFactura]    Script Date: 3/11/2026 21:27:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Reserva_IdFactura] ON [dbo].[Reserva]
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Rol_Nombre]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [UQ_Rol_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_DNI]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_Email]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_NombreUsuario]    Script Date: 3/11/2026 21:27:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BitacoraEvento] ADD  CONSTRAINT [DF_BitacoraEvento_FechaHora]  DEFAULT (sysdatetime()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Equipamiento] ADD  CONSTRAINT [DF_Equipamiento_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [DF_FacturaBuffet_FechaHoraEmision]  DEFAULT (sysdatetime()) FOR [FechaHoraEmision]
GO
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [DF_FacturaBuffet_Estado]  DEFAULT (N'Pagada') FOR [Estado]
GO
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_Stock]  DEFAULT ((0)) FOR [StockDisponible]
GO
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_StockMinimo]  DEFAULT ((0)) FOR [StockMinimo]
GO
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Producto_C] ADD  CONSTRAINT [DF_ProductoC_FechaHora]  DEFAULT (sysdatetime()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Producto_C] ADD  CONSTRAINT [DF_ProductoC_Act]  DEFAULT ((1)) FOR [Act]
GO
ALTER TABLE [dbo].[Tarifa] ADD  CONSTRAINT [DF_Tarifa_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Bloqueado]  DEFAULT ((0)) FOR [Bloqueado]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_IntentosFallidos]  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_DebeCambiarClave]  DEFAULT ((1)) FOR [DebeCambiarClave]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_IdIdioma]  DEFAULT ((1)) FOR [IdIdioma]
GO
ALTER TABLE [dbo].[BitacoraEvento]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraEvento_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[BitacoraEvento] CHECK CONSTRAINT [FK_BitacoraEvento_Usuario]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_FacturaBuffet] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[FacturaBuffet] ([IdFactura])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_FacturaBuffet]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Producto] FOREIGN KEY([CodigoProducto])
REFERENCES [dbo].[Producto] ([CodigoProducto])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_Producto]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cancha] FOREIGN KEY([IdCancha])
REFERENCES [dbo].[Cancha] ([IdCancha])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cancha]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Tarifa] FOREIGN KEY([IdTarifa])
REFERENCES [dbo].[Tarifa] ([IdTarifa])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Tarifa]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [FK_FacturaBuffet_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [FK_FacturaBuffet_Cliente]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [FK_FacturaBuffet_Vendedor] FOREIGN KEY([IdVendedor])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [FK_FacturaBuffet_Vendedor]
GO
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK_FamFam_Hija] FOREIGN KEY([IdFamiliaHija])
REFERENCES [dbo].[Familia] ([IdFamilia])
GO
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK_FamFam_Hija]
GO
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK_FamFam_Padre] FOREIGN KEY([IdFamiliaPadre])
REFERENCES [dbo].[Familia] ([IdFamilia])
GO
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK_FamFam_Padre]
GO
ALTER TABLE [dbo].[PermisoSimple_Familia]  WITH CHECK ADD  CONSTRAINT [FK_PsFam_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([IdFamilia])
GO
ALTER TABLE [dbo].[PermisoSimple_Familia] CHECK CONSTRAINT [FK_PsFam_Familia]
GO
ALTER TABLE [dbo].[PermisoSimple_Familia]  WITH CHECK ADD  CONSTRAINT [FK_PsFam_PermisoSimple] FOREIGN KEY([IdPermisoSimple])
REFERENCES [dbo].[PermisoSimple] ([IdPermisoSimple])
GO
ALTER TABLE [dbo].[PermisoSimple_Familia] CHECK CONSTRAINT [FK_PsFam_PermisoSimple]
GO
ALTER TABLE [dbo].[Producto_C]  WITH CHECK ADD  CONSTRAINT [FK_Producto_C_Producto] FOREIGN KEY([CodigoProducto])
REFERENCES [dbo].[Producto] ([CodigoProducto])
GO
ALTER TABLE [dbo].[Producto_C] CHECK CONSTRAINT [FK_Producto_C_Producto]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cancha] FOREIGN KEY([IdCancha])
REFERENCES [dbo].[Cancha] ([IdCancha])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cancha]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cliente]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Factura] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[Factura] ([IdFactura])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Factura]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Tarifa] FOREIGN KEY([IdTarifa])
REFERENCES [dbo].[Tarifa] ([IdTarifa])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Tarifa]
GO
ALTER TABLE [dbo].[RespaldoBD]  WITH CHECK ADD  CONSTRAINT [FK_RespaldoBD_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[RespaldoBD] CHECK CONSTRAINT [FK_RespaldoBD_Usuario]
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_RolFam_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([IdFamilia])
GO
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_RolFam_Familia]
GO
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_RolFam_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_RolFam_Rol]
GO
ALTER TABLE [dbo].[Rol_PermisoSimple]  WITH CHECK ADD  CONSTRAINT [FK_RolPs_PermisoSimple] FOREIGN KEY([IdPermisoSimple])
REFERENCES [dbo].[PermisoSimple] ([IdPermisoSimple])
GO
ALTER TABLE [dbo].[Rol_PermisoSimple] CHECK CONSTRAINT [FK_RolPs_PermisoSimple]
GO
ALTER TABLE [dbo].[Rol_PermisoSimple]  WITH CHECK ADD  CONSTRAINT [FK_RolPs_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Rol_PermisoSimple] CHECK CONSTRAINT [FK_RolPs_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Idioma] FOREIGN KEY([IdIdioma])
REFERENCES [dbo].[Idioma] ([IdIdioma])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Idioma]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [CK_Cliente_DNI] CHECK  ((len([DNI])>=(7) AND len([DNI])<=(8) AND NOT [DNI] like '%[^0-9]%'))
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [CK_Cliente_DNI]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [CK_DetalleFactura_Cantidad] CHECK  (([Cantidad]>(0)))
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [CK_DetalleFactura_Cantidad]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [CK_DetalleFactura_Precio] CHECK  (([PrecioUnitario]>(0)))
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [CK_DetalleFactura_Precio]
GO
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Importe] CHECK  (([Importe]>=(0)))
GO
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Importe]
GO
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Stock] CHECK  (([StockDisponible]>=(0)))
GO
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Stock]
GO
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Tipo] CHECK  (([Tipo]=N'Pelota' OR [Tipo]=N'Paleta'))
GO
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Tipo]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_CantidadPaletas] CHECK  (([CantidadPaletas]>=(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_CantidadPaletas]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_CantidadPelotas] CHECK  (([CantidadPelotas]>=(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_CantidadPelotas]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_Estado] CHECK  (([Estado]=N'Cancelada' OR [Estado]=N'Pagada' OR [Estado]=N'Pendiente'))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_Estado]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteEquipamiento] CHECK  (([ImporteEquipamiento]>=(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteEquipamiento]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteTarifa] CHECK  (([ImporteTarifa]>=(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteTarifa]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteTotal] CHECK  (([ImporteTotal]>(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteTotal]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Autorizacion] CHECK  ((len(ltrim(rtrim([CodigoAutorizacion])))>(0)))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Autorizacion]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Banco] CHECK  ((len(ltrim(rtrim([Banco])))>(0)))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Banco]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Estado] CHECK  (([Estado]=N'Anulada' OR [Estado]=N'Pagada'))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Estado]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Importe] CHECK  (([ImporteTotal]>(0)))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Importe]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Numero] CHECK  ((len(ltrim(rtrim([NumeroFactura])))>(0)))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Numero]
GO
ALTER TABLE [dbo].[FacturaBuffet]  WITH CHECK ADD  CONSTRAINT [CK_FacturaBuffet_Tarjeta] CHECK  ((len([Ultimos4Tarjeta])=(4) AND NOT [Ultimos4Tarjeta] like '%[^0-9]%'))
GO
ALTER TABLE [dbo].[FacturaBuffet] CHECK CONSTRAINT [CK_FacturaBuffet_Tarjeta]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Categoria] CHECK  ((len(ltrim(rtrim([Categoria])))>(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Nombre] CHECK  ((len(ltrim(rtrim([Nombre])))>(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Nombre]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Precio] CHECK  (([PrecioUnitario]>(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Precio]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Stock] CHECK  (([StockDisponible]>=(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Stock]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_StockMinimo] CHECK  (([StockMinimo]>=(0)))
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_StockMinimo]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_CantidadPaletas] CHECK  (([CantidadPaletas]>=(0)))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_CantidadPaletas]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_CantidadPelotas] CHECK  (([CantidadPelotas]>=(0)))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_CantidadPelotas]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_EstadoPago] CHECK  (([EstadoPago]=N'Aprobado'))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_EstadoPago]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_ImportePago] CHECK  (([ImportePago]>(0)))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_ImportePago]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_Ultimos4Tarjeta] CHECK  ((len([Ultimos4Tarjeta])=(4) AND NOT [Ultimos4Tarjeta] like '%[^0-9]%'))
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_Ultimos4Tarjeta]
GO
ALTER TABLE [dbo].[Tarifa]  WITH CHECK ADD  CONSTRAINT [CK_Tarifa_Horario] CHECK  (([HoraDesde]<[HoraHasta]))
GO
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [CK_Tarifa_Horario]
GO
ALTER TABLE [dbo].[Tarifa]  WITH CHECK ADD  CONSTRAINT [CK_Tarifa_Importe] CHECK  (([Importe]>=(0)))
GO
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [CK_Tarifa_Importe]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_IntentosFallidos] CHECK  (([IntentosFallidos]>=(0)))
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_IntentosFallidos]
GO
/****** Object:  Trigger [dbo].[TR_Producto_RegistrarCambio]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_Producto_RegistrarCambio]
ON [dbo].[Producto]
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
ALTER TABLE [dbo].[Producto] ENABLE TRIGGER [TR_Producto_RegistrarCambio]
GO
/****** Object:  Trigger [dbo].[TR_ProductoC_RestaurarVersion]    Script Date: 3/11/2026 21:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_ProductoC_RestaurarVersion]
ON [dbo].[Producto_C]
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
-- Historial inicial del catalogo del buffet.
-- Los productos fueron insertados antes de crear el trigger, por eso se registra
-- explicitamente su primera version. Producto_C no participa en DVH/DVV.
-- Una vez modificado un producto, el trigger desactiva esta version (Act = 0)
-- y registra la nueva (Act = 1), permitiendo restaurar la original.
INSERT INTO dbo.Producto_C
    (CodigoProducto, FechaHora, Nombre, Categoria, PrecioUnitario,
     StockDisponible, StockMinimo, ActivoProducto, Act, TipoCambio)
SELECT p.CodigoProducto, SYSDATETIME(), p.Nombre, p.Categoria,
       p.PrecioUnitario, p.StockDisponible, p.StockMinimo, p.Activo,
       CONVERT(bit, 1), N'Alta'
FROM dbo.Producto AS p
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Producto_C AS h
    WHERE h.CodigoProducto = p.CodigoProducto
);
GO
-- Conteos iniciales
SELECT 'Rol' AS Tabla, COUNT(*) AS Registros FROM dbo.Rol
UNION ALL SELECT 'PermisoSimple', COUNT(*) FROM dbo.PermisoSimple
UNION ALL SELECT 'Usuario', COUNT(*) FROM dbo.Usuario
UNION ALL SELECT 'Producto', COUNT(*) FROM dbo.Producto
UNION ALL SELECT 'Producto_C', COUNT(*) FROM dbo.Producto_C
UNION ALL SELECT 'Cliente', COUNT(*) FROM dbo.Cliente
UNION ALL SELECT 'Reserva', COUNT(*) FROM dbo.Reserva;
GO
