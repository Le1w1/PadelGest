/*
 PadelGest - Demostracion completa - SQL Server 2019 o superior
 Basado en la exportacion de la BD de demostracion del 09/11/2026.
 Contiene datos, usuarios, historial y ventas tal como fueron exportados.
 ATENCION: Solo ejecutar en una instancia donde PadelGestDB NO exista.
 No ejecutarlo sobre una base de datos con informacion que deba conservarse.
*/
USE [master];
GO
IF DB_ID(N'PadelGestDB') IS NOT NULL
    THROW 51000, 'PadelGestDB ya existe en esta instancia. No se sobrescribio nada.', 1;
GO
-- Nombres fisicos exclusivos para evitar conflictos con archivos .mdf previos.
DECLARE @RutaDatos nvarchar(4000) = CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath'));
DECLARE @RutaLog nvarchar(4000) = CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultLogPath'));
DECLARE @Sufijo varchar(32) = REPLACE(CONVERT(varchar(36), NEWID()), '-', '');
DECLARE @ArchivoDatos nvarchar(4000), @ArchivoLog nvarchar(4000), @Comando nvarchar(max);
IF @RutaDatos IS NULL OR @RutaLog IS NULL
    THROW 51001, 'SQL Server no informa la ruta predeterminada de datos o logs.', 1;
IF RIGHT(@RutaDatos,1) NOT IN (N'\', N'/') SET @RutaDatos = @RutaDatos + N'\';
IF RIGHT(@RutaLog,1) NOT IN (N'\', N'/') SET @RutaLog = @RutaLog + N'\';
SET @ArchivoDatos = @RutaDatos + N'PadelGestDB_Demo_' + @Sufijo + N'.mdf';
SET @ArchivoLog = @RutaLog + N'PadelGestDB_Demo_' + @Sufijo + N'_log.ldf';
SET @Comando = N'CREATE DATABASE [PadelGestDB] ON PRIMARY (NAME=N''PadelGestDB_Demo_Data'', FILENAME=N''' + REPLACE(@ArchivoDatos,N'''',N'''''') + N''') LOG ON (NAME=N''PadelGestDB_Demo_Log'', FILENAME=N''' + REPLACE(@ArchivoLog,N'''',N'''''') + N''')';
EXEC sys.sp_executesql @Comando;
GO
USE [PadelGestDB]
GO
/****** Object:  Table [dbo].[BitacoraEvento]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Cancha]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Cliente]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Equipamiento]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Factura]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[FacturaBuffet]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Familia]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Familia_Familia]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Idioma]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[PermisoSimple]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[PermisoSimple_Familia]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Producto]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Producto_C]    Script Date: 9/11/2026 12:48:40 ******/
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

 CONSTRAINT [PK_Producto_C] PRIMARY KEY CLUSTERED 
(
	[IdCambio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[RespaldoBD]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Rol]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Rol_Familia]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Rol_PermisoSimple]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Tarifa]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 9/11/2026 12:48:40 ******/
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
SET IDENTITY_INSERT [dbo].[BitacoraEvento] ON 

INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (1, 1, N'admin1', CAST(N'2026-11-09T12:36:53.7627017' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (2, 1, N'admin1', CAST(N'2026-11-09T12:38:12.4296299' AS DateTime2), N'Administrador', N'Crear Usuario', N'Alta', N'Exitoso', N'El administrador creó el usuario: Leonel11113333 con el Rol: Dueño')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (3, 1, N'admin1', CAST(N'2026-11-09T12:38:28.3048803' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: admin1 (Rol: Administrador)')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (4, 1, N'admin1', CAST(N'2026-11-09T12:38:57.3136563' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: dueño11113333 (Rol: Dueño)')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (5, 1, N'admin1', CAST(N'2026-11-09T12:39:08.4140761' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (6, 2, N'dueño11113333', CAST(N'2026-11-09T12:39:20.5597531' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (7, 2, N'dueño11113333', CAST(N'2026-11-09T12:41:31.7957285' AS DateTime2), N'Cliente', N'Serializar clientes', N'Media', N'Exitoso', N'Se procesaron 10 clientes mediante la operacion XML. No se modificaron registros de la tabla Cliente.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (8, 2, N'dueño11113333', CAST(N'2026-11-09T12:42:42.4660321' AS DateTime2), N'Cliente', N'Registrar cliente', N'Media', N'Exitoso', N'Se registró el cliente DNI 11111111.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (9, 2, N'dueño11113333', CAST(N'2026-11-09T12:42:56.2003082' AS DateTime2), N'Cliente', N'Deserializar clientes', N'Media', N'Exitoso', N'Se procesaron 10 clientes mediante la operacion XML. No se modificaron registros de la tabla Cliente.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (10, 2, N'dueño11113333', CAST(N'2026-11-09T12:43:21.5929686' AS DateTime2), N'Buffet', N'Registrar factura', N'Alta', N'Exitoso', N'Factura FB-20261109-6C87736DFB pagada. DNI 11111111. Productos 1. Total 1.600,00. Autorización FB-1ECC9073E211.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (11, 2, N'dueño11113333', CAST(N'2026-11-09T12:43:31.2766872' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
SET IDENTITY_INSERT [dbo].[BitacoraEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[Cancha] ON 

INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (1, N'Cancha 1', N'Disponible', N'e6e8bcf57788150e413e016d20f8de662b731665f5b7e1071025cb1919d07ce3')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (2, N'Cancha 2', N'Disponible', N'0aaac307006e492dabf3cfdbe1bc09849eaf4a9947d3753ec0a26bc94a3cc329')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (3, N'Cancha 3', N'Disponible', N'e76a1e12447a651bd0aa24debb03ca1a4df00165df4fa548c51fa6f4fcd9d128')
SET IDENTITY_INSERT [dbo].[Cancha] OFF
GO
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'11111111', N'leo', N'rod', N'ENC1:L0dL9JZf37NcJtRfIlk98siOEUxcF4pnouOMzdtDyrLFu/l8mBo=', N'asd@sad.com', N'b41ebba982267e83a9a751f29558407961bc54cba1310f93dc1cad0f589d0f3a')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000001', N'Lucia', N'Fernandez', N'ENC1:oGhahdesTAEpvlHPtTk3Jvcz5JsonSIiqfVYBzIuYjbkK5pQVi8=', N'lucia.fernandez.demo@example.com', N'cd6305069b9fb1089a6df602be8b5d27b904aed934fac14192de017c218cc098')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000002', N'Mateo', N'Gonzalez', N'ENC1:rS6OqDomOETHLWLj7o9moa8GbaXEP5W0HIPFl1h5dikxrSddeyg=', N'mateo.gonzalez.demo@example.com', N'72483aea9f5b61afa03b0c3e9a51d58bdba66d554b93b384f7e7e1101b749911')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000003', N'Sofia', N'Martinez', N'ENC1:h52VS9h0XDQOm1pZs4cLWSMH2adg8AuTYMg4vu7qSs94BLoWbRw=', N'sofia.martinez.demo@example.com', N'a3e3e1302734d46e2502afd90cc9b51829aaa82ccc3dfdc124420d7e19b8a303')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000004', N'Tomas', N'Rodriguez', N'ENC1:Hh5F2PXbSJi4w2AV3km0PgljOiIXDOrqMtew87W3dFcC0uTA4Js=', N'tomas.rodriguez.demo@example.com', N'a0ed7c17817e9c4ce9a5199c193ee9f4a2900b472f257579817f956f1b0139fb')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000005', N'Valentina', N'Lopez', N'ENC1:QowJgBiTf8F95oAM7jB1NpQjTEefEkCBgQCevVG2+aFsx9BzaKk=', N'valentina.lopez.demo@example.com', N'6eebadf45c99f1434045a3d4e99776412b9b778793f3d7c79beffc2437b211d1')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000006', N'Benjamin', N'Perez', N'ENC1:zgDbevtyfCmU9k/xi2s4vXjFgqTvRgUMyxlHyJhUGW78cTfCsIE=', N'benjamin.perez.demo@example.com', N'1ec3cb3da3b88e655cd5842b729d8bde11f27ab6d11d6bcb01a3a23f31d7a77a')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000007', N'Martina', N'Sanchez', N'ENC1:9yCpFl3Q/tLHwuBu7IAJArewjs1CVO40fLfsFWF4chJnDAbvnP4=', N'martina.sanchez.demo@example.com', N'47123a42cb409e9eaf5d1275d3c0c15b773c3dbb3d94f0a8a759eb872b41e8e6')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000008', N'Joaquin', N'Romero', N'ENC1:Di7F2hO23fLQbHAjdo2c77zE1sav4OWVhVvkmvsTUWwiefFr9fg=', N'joaquin.romero.demo@example.com', N'6c85bcfaa0bb5a2b130f0dcce982c8c9b4a89b4ba532edec94d85b24872e351a')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000009', N'Catalina', N'Torres', N'ENC1:nyZcQrt5bw3JY5U83UqPu23krM9KYtaCcGWj8GkXm8f4js3Jejo=', N'catalina.torres.demo@example.com', N'ea7f634e617fdc27895a1658b2991260d9f083eaf8f40387746425374bbbaedf')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'41000010', N'Franco', N'Diaz', N'ENC1:aP1CERHLRGsuG4QndKlx+UpWXCXf0xE6AGIdYxhzaEYxehjZaYU=', N'franco.diaz.demo@example.com', N'2e30229aaf80a3ec2b2400f718bc72584bdaeda3f5842945e259a6ee7e8d662e')
GO
SET IDENTITY_INSERT [dbo].[DetalleFactura] ON 

INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (1, 1, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'49e4dc07a6e3ee0458887f3d3df8b4df9df04168b56aa2308476129085d3eb73')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (2, 2, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'd4fb257a1021226ce7f5509d89f91117e046c6c8d0af4984d70ae5f649de75d6')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (3, 3, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'b66285b861dd9fb4cd68680fd638f0acf919e36c6977b3e3033c9079f269eddc')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (4, 4, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'cea141ffbcd01c8156926c9e4cb1cc852acc285582314626469c80bd7a14d8ae')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (5, 5, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'0515dc0f257a419efbbe6f60309459718aacb540ae0d07bb108eb26ace305a87')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (6, 6, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'2f89798979311752defbe98f367b58e16f8c4e6686d2d78844d7779bdc90ee83')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (7, 7, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'e85a0ecca4b8f76e335caae9da2e662cce991da6d29d671f5e7efb28c520ffc1')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (8, 8, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'adebc1fa14ed791134d7e7bd827c388c456119c9ceb594ff927c2c628b3f62a4')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (9, 9, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'c306b5d9e58adbbea52386c64d5eaa0451c9758265569c8577220c7150bf739c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (10, 10, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'50037b1f7a905021835aae3b40a6260a693fc0e181b968ba15e2acfafd1dd34f')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (11, 11, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'ab0dcd5a6974d93ff62c28207d848a71ffda01ff5ed3531ef8454cc4a8324d8a')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (12, 12, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'766a2b5c123dc81ff0bf3fd91e096ff7b8af05d4caadebbf3a17456200f1a842')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (13, 13, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'd501c0c034d219dac60a54901a471f08090f75c0f597fbc332819d8413cc9e44')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (14, 14, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'2907066a84e6e99777b9180dfa942a554957fc8bbd5ae0fa09f4e4ee0bb6a9b9')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (15, 15, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'ab66f97690511024548d70c4a90f50bd49f94c952a98d756883f3652f9bfc7b6')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (16, 16, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'364433e0446c3699b10f0f21aecdb0fbe25e1d6cc761cdbf7fc6990604bcb905')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (17, 17, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'837875b3232fdf4d6de0be9e80ae0eece4e617aa67621e9177506ac3d1876f9b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (18, 18, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'2cbc66891c6287d37c423713118064b3e1e24b46b7baab0afac585b19c45b7b5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (19, 19, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'0122c94fc605633f43e3f0cdb056aee7185a84aab3321663b182c8b276d5c575')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (20, 20, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'ed18f21bcfa6c35a82e7e502ec10e907b9bba07754b6a87b77185d2107acaedd')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (21, 21, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'6a1eefad9543e8f678c8157bbb7397a1c35dd9cb3835bdad816e22aa143c003d')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (22, 22, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'644ee829e2b0c7b4ead579c0b0dc5f57cacf073a57455eb5d3f2e304115d5d16')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (23, 23, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'49151832a72a974297cd8b7f1d1e4329705d826131569e93af9cabb72eacc7e6')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (24, 24, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'b8284174dddb7d2793c4b8b90a7f2177102c7b3b37d8297a6554a2d25d8cf80e')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (25, 25, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'1d71428f11a84ef9a0721a6100fa8f6434909291c4f23a19cc31c25aa55406e4')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (26, 26, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'f0f22a58ec6fdf4412e4ef234ea5263c319df86a7a53f31417d36d6066a40e9b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (27, 27, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'087a2405aec047f7d297b27be526689ce68715dfe7e790cf949d7cd551af84ba')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (28, 28, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'4f32c0074d3089c8b731808572a0ab07af49189be40a11de13338f8193b76f9a')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (29, 29, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'cb39d86417da30800f8643699f0963425181c0f109e98e54547aea539527a04e')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (30, 30, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'13bd93cb59eef77da18bdcd3063aa894ba0a87f9def301c99196371246d6861b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (31, 31, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'cd22e8a9e2b630938cbb5170d72b69d820c4cf39873395496db853b8586afd91')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (32, 32, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'7ea3fe0d39b10c05a7d271ae13a5fc50ea781824535682e47d245b0d4f14fc04')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (33, 33, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'6c5183852486a746d3909aeebb54cda93ae14c90ce283ec4a93f83736e46a269')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (34, 34, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'3acab3180d8c68fb6354347e5f69c8356f2d4fc6b5b4bfd54e7e5ec8a0630f93')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (35, 35, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'86c2378cb58eff97098bb4000a03b18d6a1868e9e6634c719fe70f04548c998b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (36, 36, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'88457d655550989f76865f57f9fc4702db721fb378786e9a5d0fc6e03eb33fac')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (37, 37, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'37aa02980905ae2c064e788aff404712813f53e5917ee2ed726f83e8a1c07ba6')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (38, 38, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'a272f7b32671e5777f9329f0a1d6e59a08401539ae7a13e6ac5e66ec27d4b76c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (39, 39, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'ecd28e1760fa74ce05743db1e7d74ebec5572a64166027559743edf0b482973d')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (40, 40, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'dffd6320ac05fec55b86a0082f3732a291a1c7d316a823c543a8f9c1ffb216fb')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (41, 41, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'9e21d962d5f732e85e82e7b97ecd1d08b97c8a1e4dbaa55f271869d70e368ffb')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (42, 42, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'e6e6fe92008deab32ed1f47d57d61c7e13aeca3419b912aca53ba41621ef08fe')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (43, 43, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'657bb1627043a161594e39b849ec3921b543343842962d98c8bd306cd8320d19')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (44, 44, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'58992593f4231edd1aa53b46a6c18d697875dea87b948e6dcaa81887bece3109')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (45, 45, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'bfa7ed5f693c2db90690fe65039cbd0cefd9b4915a9ebaca84318182fa39ae88')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (46, 46, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'278a754152c4aac8bb50bb705e2d394f69ea1411803c2c49650d9b3729ed92b8')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (47, 47, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'0ce694d7c3fffa5a1c650419793b3965429eea68a558782bf68f65720c3d647c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (48, 48, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'b6545a7c038ce28c3e06c09beda2f3a26ca582c6d7feaae02517b6e995855b4c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (49, 49, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'7384bd5a3c391c35b07c4b4f9065fc4209ecad5ef808a1a710ee5adabd13528c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (50, 50, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'ef8c5b17d6c1ab645a92af922f7477e04e495c41e06b2889fe6df9598b1632c5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (51, 51, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'10f64fbf00d9b17138245f90bdeed725996a2424c889c66b4d3010491a3d0bb1')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (52, 52, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'c52bb2fda05ae24dfe6b00f4ab2a39b5662b82f9783e46ca4aaf1a1b1b7514e3')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (53, 53, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'c475af03a6bf83a748d12032baebce116dca2d0120641f80edab2d8f8350a77b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (54, 54, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'ae0aacec4daf8849dbfa18e0a3e7ced251ce0effbc4915e406950a18997f81b5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (55, 55, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'58f469bb671c79b907b8b888860310e546323407e27116b46710df37dcc1441c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (56, 56, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'2a030a7c3e93673ef7e5c3d01d3ef184e78a1c2487869eea23333918e0892670')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (57, 57, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'813300ee2a7613100e60b653012a963f3802103ce7ea07a25fee97afa4369695')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (58, 58, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'b17373b539913e632e3ee3637d1b627f72f0f432222323f8674f13b570da1aa3')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (59, 59, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'c5afb5bfab6d92e08c0b82c560a20fa64b24a5287d48678523bb908d93a177c7')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (60, 60, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'c46806cf654bbe8b7770022e4650c56310624a94681de4c373f57108a3ea0c2e')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (61, 61, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'8d7743d7f426011207ba097777f956fe1f4129db5a316282613abe0dc1c05a02')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (62, 62, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'327c86c079ec89e4662e27d3151ee94e846851e3156c6b4ced0c0ba9ba4b38f5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (63, 63, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'cd294a5205cd1b32c34f514f33bc4ac6b6ff591af4421537a1dfe7ef0ee18705')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (64, 64, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'002b6d2d2037b41351daf8d1566c9a2b44ba2b592ca50bd3d66ec0375e9230f9')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (65, 65, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'3c8f9c2e3f4c551e0c488d0ed46a018c86a76766b917ab3792f108cd28c05b16')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (66, 66, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'a41e05b47b0c8b031827626d0acbeef14082f06bee14c801977b451536e12059')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (67, 67, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'38357f9f3b91fb12bb06ea232c70ccea04cb0d55e71b9f8c9f422a7a2d0c2ec2')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (68, 68, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'e9c277327bfd26256458abf4bd8097bf95bbdfa64eae57506d466fe55e5999a8')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (69, 69, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'36fd2822d04cbe365372529b561daafcbf0130fd28af4da11acff77c6ee10607')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (70, 70, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'b9d85e564bbf3e85c122d858b77221d140634fc413c5bee72d5e6190222303ba')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (71, 71, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'bc52fb5169a070daf1c04a1f6997bb901228b3525d6bfa4f770b0d460e294f17')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (72, 72, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'315aa29ad0709d6b5b3d79100443344ea8f1cc066bbfecc5ce21da9ec97a8ee0')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (73, 73, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'36b0f0ef7d730909515f467aab0c6c8d996c58db5439726cb3acc58f8a790f48')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (74, 74, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'a8a034cd9cf0eee36c8f098b29d0023533ede16734d8a5938410ce5f773a9d53')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (75, 75, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'b3ba4c30a8e44b47065a3c76e50241e48d7922cc6e585b54e574d5a767628e63')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (76, 76, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'073de219159a72707bf1542f98a7bd3e2640e41352c895c014e725fb0a9bd844')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (77, 77, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'cab3c3f28d4c8e8091b73145cd9a2e36b09c67120765f832359439a6f0a183fa')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (78, 78, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'bdfa0213c35d36afe3a281501103314f46a2b1740c354bb999e5f6816ae2d7dd')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (79, 79, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'ad10093655c4f8248d5fcb9c27c170d1b5cc3ee9ab9148832e9dedb97d10be08')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (80, 80, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'ee9d45d74f2cd9ed7d5f515a27aab4d189d5fd5f0a7eec4069efea87e12924ca')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (81, 81, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'deb3a03ed95eed46d070d041be6497f1d4dac6918ae33865da11dcf1543c13b1')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (82, 82, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'1cd53560750360f54ee7e0a96883969d1b2238c48aab645e07729e7b05ae2687')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (83, 83, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'33445af844cb984c0437f4d3211dfc37642f49ef8eb1d3b965330b651cdb7571')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (84, 84, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'94dea2cdb33a33601a1c0b05e8a8ea6857775544adb7e98cb753d6fee6ac2f4a')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (85, 85, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'57e1e27c02533e59db76925a1a8bb5bfadb6c73f4199403bf9f45f61156ddbad')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (86, 86, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'5efc8f9cdb1b11c630e1ca6930b207c44c3f48bbf52f646bba2e99b2f6f18dd5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (87, 87, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'1f1564a760e687c1400d08b066128ab7a0629a6ffadb290c215414de77998b1b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (88, 88, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'd80ebfda462510c670dad74370c9d185435b960ad1c21859934492916ec59fa3')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (89, 89, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'daf4a0c534aa52fd123a80dd3782e04d434c480dcfdb6cb50a489df2439292a5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (90, 90, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'994946612855370a739df7e4833f56225f3cf653eda2fa83c3c45b99610359c6')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (91, 91, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'58b3c7a35d7b269af5e04d48eb24c3d37dbe266103c99f961192b0fe880ce32c')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (92, 92, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'4a3e6354006980938ae7d246b99e9fe1ccaf64d8b3436ee8c30f911a868c671e')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (93, 93, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'c1997c6f54f25fefea85042472ac5270b3a7e23089aab490431eebf494c270e5')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (94, 94, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'259cf1062e0eac51f9f7ee3c818bbdc8eb468e9ec70cc50e48c76710adcba9b7')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (95, 95, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'3458e75bec059f0e22799c3a52e4a384acb883d21566cb940a5b937821d21479')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (96, 96, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'ed539c813ec6bae0a5ee930b5e6fdc5394896e8ff68f6d02444eaecc75d612c9')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (97, 97, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'c8829a5b57bd7e7ef05e9bde335e05d81478b49435ac48a936990e9504857814')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (98, 98, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'eb4040b23e47fc447044dc964c792ea9e44500bcf6bbe2e55d1512ee243f9c5f')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (99, 99, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'24d340b634d42598ce5b7436a7ed2ce9af6787d3eeebe100e5f8f6cd164f1f19')
GO
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (100, 100, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'c6349c2d81f319e3c632a71c62d88d7e56760e612450293f2e6da2b319fbb828')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (101, 101, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'f465d6e956bac37894bc7f19ec418669b7da66a2d74d929b1ef71556eece8e58')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (102, 102, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'22a388f1fab888cf4cc5565791ab72dc576764012405a0591389d29b91b644ef')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (103, 103, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'e91104d8f975580ce164129bf9d7efd493889de0d52530074fac85e76b2cc05a')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (104, 104, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'a255cc46654f2239e8276e8c066656413b3d57c5f1af8b9ea1d8122604916bab')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (105, 105, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'420e5955119970e99009a1a879d3fc73e4bfc2853283ad3e11ddc637a89c7918')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (106, 106, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'119081953aaba3e8c1ed62c47c2395f09db073bcbe1f5384c9cd527607704adb')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (107, 107, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'562596e9add8a4485301be1463a2f9bee61b8e75fe8ea6c3f316e7154411f5c2')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (108, 108, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'7ffbb4f37b489f084628caecabfd0006562eb97a409420e4c9df5b7f7e7cbe32')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (109, 109, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'8fa9372aa8aa5719a65ecb90930b9a7e37d8ace037fe05e12f0aa9e4bd9cbf60')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (110, 110, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'b2154b7ae33396f9b5eb290443d7676abf196d3df4bf60563c4cd8145ee75c96')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (111, 111, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'59cd2d40f1eecf15d00d9a410d81ec760a248c843136d72e897fddf7a4421f15')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (112, 112, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'b1cbb6c38b0788fb445bd32e9e90e98efaafa2c6cfe4da2e3bdf97fe583a06d0')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (113, 113, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'c1d01f3c543fdf18f01405b4f1eafd718ab1a37638c9f505450982b4f21adc17')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (114, 114, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'2e9660c3727108367f1a5471d22fddb0bf0556b2ac6efce106c2e92fe01718a2')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (115, 115, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'76cccd07227b1d67679e861b1f3ab00fb7e5d0ffb76cfffb2ba420b8eecd72ce')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (116, 116, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'333ba09c6f028539f2410c2129dffe9cddd70da728c4a26b390d8491cf45822e')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (117, 117, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'9786dbc6f6ca10c7c7d14c2b8b4c89f1031982c25e779bd0f35400d10b08b4df')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (118, 118, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'5260fcc310ea2db1b46b994818ac03d40d2b6f9ead929bcf9dd4833489208cd9')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (119, 119, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'cc1d3d6ca3106d0ad2b4e08ffbf3d4b8a70f55c50b0f2c2b9f098e59ca84cacd')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (120, 120, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'9142822be71323c01279fa1882c35f4a970dfce187fd64dcb366c2a9489e9725')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (121, 121, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'ec7f9ca927968c389b0a356cb87e72bc7ffd90f3ef5eacce0e7ec92849f0794b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (122, 122, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'a0fb8c3ef8f098037393b031517d194bb82f6e4d4b2ad5e545a2540f50690ab1')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (123, 123, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'cdb4d38a92eaea70846ad383057cf5cfacc96386e925799546048db59d64e26b')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (124, 124, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'b6939946e349ac5d11add7415fec9171736755bb12c08a5d60197f4c0b1da9da')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (125, 125, 10, 2, CAST(5200.00 AS Decimal(12, 2)), N'0d3a03617645ac0170a4e85c596d5ca56681ef8e8dd2755fb2cb9cc9f3879415')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (126, 126, 11, 3, CAST(2500.00 AS Decimal(12, 2)), N'b4071439755a5e7a8df0a566f4da506ee2c3822e639064de7e954e80682d2c27')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (127, 127, 12, 4, CAST(3200.00 AS Decimal(12, 2)), N'0501d6127d0852044332ef56786e17777d7c2d1dace9e43f9d15ce107ca12004')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (128, 128, 1, 1, CAST(1500.00 AS Decimal(12, 2)), N'dae5306a354ca4bfb1a46418b9ed840c6eaa0a258100ca3e2d7ea12f147ef500')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (129, 129, 1, 2, CAST(1500.00 AS Decimal(12, 2)), N'30aff779a3b025edd9cf2955f884d3b01b44f7aa1373aade4b1dd4154b72c78d')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (130, 130, 1, 3, CAST(1500.00 AS Decimal(12, 2)), N'4a59a92fdecad6c681fd2b9da4d1117ab7f9c535cd959f1cd53ebe5e04d958eb')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (131, 131, 2, 4, CAST(2800.00 AS Decimal(12, 2)), N'3742a56df409cda9068f909ded16eff834ed91b82ba7065ac9514c71acb36373')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (132, 132, 2, 1, CAST(2800.00 AS Decimal(12, 2)), N'435ea05a7184e110813960f70952f574cc9c339d6e577d3b29bd2bce9fff1024')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (133, 133, 3, 2, CAST(1600.00 AS Decimal(12, 2)), N'c2dde49fae9b1124c62d136ebd0e0ee5494b8c7c2b9cf4c3700530cf8e03bdba')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (134, 134, 4, 3, CAST(2900.00 AS Decimal(12, 2)), N'd64111127a46c3f4d2c384dc894d70fb9ddc2a285a86a00cfa54dc81164e0f00')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (135, 135, 5, 4, CAST(2900.00 AS Decimal(12, 2)), N'fe3cf1bfacadfd9f82ecf3847009379e02e3e1ac86ff6a5abc9b411cb5e4e8ba')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (136, 136, 5, 1, CAST(2900.00 AS Decimal(12, 2)), N'efc842b0bc5e7fae8dc58e9affbfc131d8a09a8c990755762d62660f83fdd034')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (137, 137, 6, 2, CAST(2850.00 AS Decimal(12, 2)), N'5ffe01d5d3966f05fd4e52d384ee2f20a139dfa368623a0d20f698885ade4ae4')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (138, 138, 7, 3, CAST(3600.00 AS Decimal(12, 2)), N'17e0350558025ceac0a4bb24e64dbb3c5856fba9173991887c1cfc7b01f59546')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (139, 139, 8, 4, CAST(3600.00 AS Decimal(12, 2)), N'385a061a777537c8c76e1ed5ecbf52bbb34d3a9dec52e5cdd0a3fc0cf664c2c3')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (140, 140, 9, 1, CAST(2600.00 AS Decimal(12, 2)), N'c48edb9e34322dc791a34859bf3e15de9c54a11f219f30b12642b451e0f263f1')
INSERT [dbo].[DetalleFactura] ([IdDetalleFactura], [IdFactura], [CodigoProducto], [Cantidad], [PrecioUnitario], [DVH]) VALUES (141, 141, 3, 1, CAST(1600.00 AS Decimal(12, 2)), N'510f033506532ca02dfec498548f8a6745320a73276efe902604d2cee146b736')
SET IDENTITY_INSERT [dbo].[DetalleFactura] OFF
GO
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cancha', N'91129657cbb5230efd409301669fd05b677f7331542c2192547bd4da1b7bd1bd')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cliente', N'c2abf3dc402f97f1e70b519b1372cbc084318ce8860ff52cb1f2ded4ce83e0c2')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'DetalleFactura', N'4bf5bac896142072ba70a93bfbbd4ee65b4437cd0310751009f0681ae8c7ae31')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Equipamiento', N'1fb47453d7dee2f1e004895302f0abd6dcaa3a456f677af737b2669d1fe365f5')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Factura', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'FacturaBuffet', N'a92b13faf5e2cb31cd3d33d12383f0c17ece448b310b55076030fe1389bc6490')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia', N'fdd294e73b3531a63b8e55aff07cf3c5d17090230a00fa065860d52642a31e97')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia_Familia', N'18c42e63ad2b66da1312f3417e15d5d971f8a253fbfb4897fbeb267656f4be79')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Idioma', N'b8206deae9aaca74a25998ec9d26c6b2b41fdf430ccc455979a8aed7a298a234')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple', N'af6008fbd952c323fa30abdd2116fa5565f0feed87b967afc09efe275e28ee13')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple_Familia', N'f1cf327dc70b0c1b359b6acbf39448b57883f5ec01ef7bb011d86214cdb56711')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Producto', N'f50c0f3d19196e16ad5e1e72f3b094a3288912fcd4850ad461d96fba0b40accf')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Reserva', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'RespaldoBD', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol', N'd1cb232072612a6fabd52d2c409c777956f9c95be7668a971e4dcd13c97ec2ea')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_Familia', N'32b5356d0241e9fa61aec0d6b60e4ce854b7b877378c67761cb47c05d0cae856')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_PermisoSimple', N'8269a31763bc4f2080e492cb816d67802190cc2bca0335d19114a8f60dde3bd4')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Tarifa', N'9cc41aa9db8a17e67681d251c2d21eb70e08af85b8f741319d72704670345d13')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Usuario', N'520a02749a026cf74181912bc4b5927a9d80ee7fe15e7000b407db5766146820')
GO
SET IDENTITY_INSERT [dbo].[Equipamiento] ON 

INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (1, N'Paleta', CAST(8000.00 AS Decimal(12, 2)), 8, 1, N'345646ab25ac506c73da5a5ecdcd9dd10858d9a5d96d33c46ab6ccf715f6a6f1')
INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (2, N'Pelota', CAST(1500.00 AS Decimal(12, 2)), 12, 1, N'27f8a517152ae46590b19ac12c70883c73339a1f60d04dc994ed76d8b754c9c6')
SET IDENTITY_INSERT [dbo].[Equipamiento] OFF
GO
SET IDENTITY_INSERT [dbo].[FacturaBuffet] ON 

INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (1, N'DEMO-0001', N'41000001', 1, CAST(N'2026-11-26T11:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-26T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-1', N'cce751b864b873e2540d52d525f8495132c8a4c14c14ed55caeb962e81973355')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (2, N'DEMO-0002', N'41000002', 1, CAST(N'2026-11-25T12:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-25T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-2', N'32994e2ade569e1029aa97c66e30131a0ecd080b9520a71a77e620ebb514aaaf')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (3, N'DEMO-0003', N'41000003', 1, CAST(N'2026-11-24T13:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-24T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-3', N'b6b268089a9c0324be16e6aebe5cb2d0eb453b5679bca0fc2e5439ff4fef1993')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (4, N'DEMO-0004', N'41000004', 1, CAST(N'2026-11-23T14:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-23T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-4', N'bf5b55a4bcd8bf660a466e6c68f624ad88ea9ef2a416e3cb2ba6f6385e75546a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (5, N'DEMO-0005', N'41000005', 1, CAST(N'2026-11-22T15:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-22T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-5', N'88495a5bd45d3294f6731eba4fc3f06adbed790180cf29f36c47fbf8ab6db98b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (6, N'DEMO-0006', N'41000006', 1, CAST(N'2026-11-21T16:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-21T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-6', N'a91bf7aac8b80a186c979a23443ab60227bb4d9944ca81022528e37e47235828')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (7, N'DEMO-0007', N'41000007', 1, CAST(N'2026-11-20T17:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-20T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-7', N'ef2c14b47c0b1fff01b133c0d9ddd9e072810ffb697b92108dd5c23955a77e2e')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (8, N'DEMO-0008', N'41000008', 1, CAST(N'2026-11-19T18:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-19T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-8', N'58c7eb3b6fb9d967afad67773ff4f8fd15dd7e2469cc71cc598e86d384f08eb8')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (9, N'DEMO-0009', N'41000009', 1, CAST(N'2026-11-18T19:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-18T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-9', N'64bb0a6895767551a5be6cf6b5dd48d899d19143c45d507e8a298600f8d33d05')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (10, N'DEMO-0010', N'41000010', 1, CAST(N'2026-11-17T10:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-17T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-10', N'3129e9f454105a37f53a0721004702e3a890f0cc33b4c075875f57dd9303b9f8')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (11, N'DEMO-0011', N'41000002', 1, CAST(N'2026-11-16T11:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-16T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-11', N'b2f2079a7e8cc3e05f8fdcc20c97bfe51a11d028ed43dd0a927fc4ca86a5fd97')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (12, N'DEMO-0012', N'41000003', 1, CAST(N'2026-11-15T12:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-15T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-12', N'd54727064e01364c436643612d6ba2216afd2b1ef9a712946f86bee951aa235a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (13, N'DEMO-0013', N'41000004', 1, CAST(N'2026-11-14T13:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-14T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-13', N'119c03dfc135d9096713785c04571cbcd15da37e8050ca50b00fba5e3de5045c')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (14, N'DEMO-0014', N'41000005', 1, CAST(N'2026-11-13T14:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-13T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-14', N'36f31bd809eb549be5c664e86ba58f91db55fcfc47d0f08ae8f724714bae0069')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (15, N'DEMO-0015', N'41000006', 1, CAST(N'2026-11-12T15:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-12T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-15', N'4588882abded46cea32f5964238f83d61bbbd7c3a7d4dbd14de47e6d301777d5')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (16, N'DEMO-0016', N'41000007', 1, CAST(N'2026-11-11T16:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-11T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-16', N'd3962d1bfd9282eb013e1be546b0bdd8e9dbc497d97c15cf6c19ba022f69105b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (17, N'DEMO-0017', N'41000008', 1, CAST(N'2026-11-10T17:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-10T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-17', N'ccae486e4d503ad50585db37abb89297d8d1fe33703e937562eaeacbcd0bd399')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (18, N'DEMO-0018', N'41000009', 1, CAST(N'2026-11-09T18:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-09T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-18', N'f89c0edf37d6573323bf0b8ad0d5513af81c502365bc2ac7aa3d4491d8ab930f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (19, N'DEMO-0019', N'41000001', 1, CAST(N'2026-11-08T19:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-08T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-19', N'8528714b44c544f29800c99e451cc076012634a7477f370ea795ac956bcc55d0')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (20, N'DEMO-0020', N'41000010', 1, CAST(N'2026-11-07T10:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-07T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-20', N'8c588e4db338c325607a0139ef9dd678c6424baaa5af0a01b1866c742270c94c')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (21, N'DEMO-0021', N'41000003', 1, CAST(N'2026-11-06T11:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-06T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-21', N'89822a589b046806d5300fb53467a2f3a5834ee12b832080d27f20453cf24316')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (22, N'DEMO-0022', N'41000004', 1, CAST(N'2026-11-05T12:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-05T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-22', N'3ec16717d578a47851f8122f368d0d381ad9b516251d4a26f7b4f9acb1da4e5b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (23, N'DEMO-0023', N'41000005', 1, CAST(N'2026-11-04T13:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-04T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-23', N'99d3e3853cdbf1a9fe8ac06095c077b965b9edd068365f745039a0cfc97ddb9a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (24, N'DEMO-0024', N'41000006', 1, CAST(N'2026-11-03T14:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-03T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-24', N'f7ed445bb06f9870f6ca1c83cf0b39f938a1ba3e0d5bc16ba18c8470f3af0d9f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (25, N'DEMO-0025', N'41000007', 1, CAST(N'2026-11-02T15:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-02T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-25', N'fb2cbfb2094f37bb0b0db51151a1473111082b74b1a76b225fddde986ea82a9d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (26, N'DEMO-0026', N'41000008', 1, CAST(N'2026-11-01T16:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-01T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-26', N'c45f2b511a16ffeef2b473f82df959a69ed9b849af72bc2e1637e131cfe9b6c1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (27, N'DEMO-0027', N'41000009', 1, CAST(N'2026-10-31T17:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-31T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-27', N'f415fc2633d85a68f27f403583f450e65861f2f8dc8c52aa6bc661e64c5c570b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (28, N'DEMO-0028', N'41000001', 1, CAST(N'2026-10-30T18:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-30T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-28', N'14083cd54e371406d292bb648b621bc67c7cd7a68f660699d4377485cbd7c41f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (29, N'DEMO-0029', N'41000002', 1, CAST(N'2026-11-26T19:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-26T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-29', N'76f3f1d3b8f6c5fed56ef1ac1444623fdf770f5edc4b308c7a3e368c1b1c0e7a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (30, N'DEMO-0030', N'41000010', 1, CAST(N'2026-11-25T10:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-25T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-30', N'fd8e4d49f03c29cc1bd8a5a058492669a899317cfb5728f559e64bc292e068f7')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (31, N'DEMO-0031', N'41000004', 1, CAST(N'2026-11-24T11:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-24T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-31', N'60bc54c88de0743d6eae4c0332f064c8cac774753ce968faad23ae7413fef222')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (32, N'DEMO-0032', N'41000005', 1, CAST(N'2026-11-23T12:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-23T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-32', N'475a2ba3e69cb177888a62dd13e1c84fa22f940c92c75c6d7363dbfef8730f54')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (33, N'DEMO-0033', N'41000006', 1, CAST(N'2026-11-22T13:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-22T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-33', N'89df41baafd491c904af9ae00eb4a20c35143ba4de0e328f52898f8e45869bfe')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (34, N'DEMO-0034', N'41000007', 1, CAST(N'2026-11-21T14:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-21T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-34', N'2f500a8d261b3def1a704ef8fe21314eb2bef95dff241ed5fcb2df441bd57903')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (35, N'DEMO-0035', N'41000008', 1, CAST(N'2026-11-20T15:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-20T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-35', N'2251be4bd17101d6f4361df52d3ce5d37dfb56cb7e32183c2ad4c8379a1ab525')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (36, N'DEMO-0036', N'41000009', 1, CAST(N'2026-11-19T16:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-19T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-36', N'b471313a39be3cdb47f0e81ffaf2df64506048689fc1bb52f4a401bbe01d4c80')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (37, N'DEMO-0037', N'41000001', 1, CAST(N'2026-11-18T17:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-18T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-37', N'61233c4c1e59263f35c4f6653d007dd76a72e9123e5f5848ae43e50d0b360115')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (38, N'DEMO-0038', N'41000002', 1, CAST(N'2026-11-17T18:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-17T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-38', N'0223d7b834cd2472cbef616d23e8836810dd5a34614718ae2fdf96bef785eea6')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (39, N'DEMO-0039', N'41000003', 1, CAST(N'2026-11-16T19:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-16T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-39', N'4fa6d9b0a06190df261d604e81c73b8ee17d15792ce7c01aa9406a69b774a6cd')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (40, N'DEMO-0040', N'41000010', 1, CAST(N'2026-11-15T10:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-15T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-40', N'675a621d75d695be4cd662217fe333bde34c44c47d1f81436db1fe8a3dc137ba')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (41, N'DEMO-0041', N'41000005', 1, CAST(N'2026-11-14T11:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-14T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-41', N'3481593d61a7ceecd51ede7ecf92fa1d656e72aada994754e7a8fa1a16ec8474')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (42, N'DEMO-0042', N'41000006', 1, CAST(N'2026-11-13T12:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-13T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-42', N'2972a58ee38255d9428275c6f1d651d1f429eb53e58e63d2252f6b6f43324b1f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (43, N'DEMO-0043', N'41000007', 1, CAST(N'2026-11-12T13:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-12T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-43', N'30b54d6c857dff77ee22d39b5cfc8787678a1aae9a246c02af444c5fbdce1740')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (44, N'DEMO-0044', N'41000008', 1, CAST(N'2026-11-11T14:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-11T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-44', N'77c21177e1eb4531d7b0a475562bf84d683ad19d03c9523f43719d623543c3f8')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (45, N'DEMO-0045', N'41000009', 1, CAST(N'2026-11-10T15:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-10T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-45', N'c5b9a5af4e1b36060286fd616c65757d041cf3d8152a83253e96654d22a6fe7d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (46, N'DEMO-0046', N'41000001', 1, CAST(N'2026-11-09T16:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-09T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-46', N'fe7bbab9b3249ed2013368759dea149868563676876df632a577712946903026')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (47, N'DEMO-0047', N'41000002', 1, CAST(N'2026-11-08T17:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-08T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-47', N'45d3291806c073123b75f82a209a1d49ed540e95adceddd9bdf55b7e4a64ed31')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (48, N'DEMO-0048', N'41000003', 1, CAST(N'2026-11-07T18:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-07T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-48', N'6acc0084a8dba58f4e40680a9008e80a170ac5405676138d6424cf6c5d884e0d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (49, N'DEMO-0049', N'41000004', 1, CAST(N'2026-11-06T19:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-06T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-49', N'2968b5a2f1287492c4f592150258aa063de678387610718c59036e6e5aa21053')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (50, N'DEMO-0050', N'41000010', 1, CAST(N'2026-11-05T10:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-05T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-50', N'0a040f16bcac502ac9cc6b1db27588713779be6c56667749ae3ae7f5f8642ff4')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (51, N'DEMO-0051', N'41000006', 1, CAST(N'2026-11-04T11:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-04T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-51', N'9777444eb11490c3c9e9609acc1dd91cfec09246dce2b238af09628a109a3b08')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (52, N'DEMO-0052', N'41000007', 1, CAST(N'2026-11-03T12:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-03T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-52', N'b1f4e7d2163568e9ba8455b51257027033febe2aa055b0b70daa2a1d26a2e80b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (53, N'DEMO-0053', N'41000008', 1, CAST(N'2026-11-02T13:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-02T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-53', N'df8e33a9b9c513b508a2bf6491984f305e6a75945dad502fde676f350de3d1ce')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (54, N'DEMO-0054', N'41000009', 1, CAST(N'2026-11-01T14:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-01T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-54', N'b61c8653c9ef2f76dffd73449d4daf2323cfb4cf933818864e36e5e7fa467f88')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (55, N'DEMO-0055', N'41000001', 1, CAST(N'2026-10-31T15:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-31T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-55', N'6c61c1eacb27fbbb9dd27ccd23b356a61b175f4b9860f5aa7145c59d7142887d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (56, N'DEMO-0056', N'41000002', 1, CAST(N'2026-10-30T16:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-30T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-56', N'6dade9b60f8f12e6837d0ce1e601d395da324c6088b96c2dc8cdb88124e0cf87')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (57, N'DEMO-0057', N'41000003', 1, CAST(N'2026-11-26T17:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-26T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-57', N'2d53eab07c53bbe77433489bdd772ac8436e208c52bc6fa18bb30b7d497ee030')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (58, N'DEMO-0058', N'41000004', 1, CAST(N'2026-11-25T18:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-25T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-58', N'fa1ba0db12c57fed3e244062845490e99ed3148764b639a1cbfe3ca918e81a4d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (59, N'DEMO-0059', N'41000005', 1, CAST(N'2026-11-24T19:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-24T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-59', N'527e90b435e40ac7a392c6638c29dc1e3969edcc9d869a3f144670e90d2b9ede')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (60, N'DEMO-0060', N'41000010', 1, CAST(N'2026-11-23T10:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-23T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-60', N'59abe28cfe61fd03383fdae9a00ee7bd1bdbabbd65218b053501247b75fbe528')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (61, N'DEMO-0061', N'41000007', 1, CAST(N'2026-11-22T11:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-22T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-61', N'2205a39744764d2b3550f59a55b5f2de0e3845d42be1b87e3e43814e07257cd1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (62, N'DEMO-0062', N'41000008', 1, CAST(N'2026-11-21T12:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-21T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-62', N'306a23ac8057a81c0ca434a4314ae747ed77fe56d210702ccbe9fb7e86f8caf5')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (63, N'DEMO-0063', N'41000009', 1, CAST(N'2026-11-20T13:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-20T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-63', N'ae60215792605a5c93f256992e56eaeb95997dca992ecaa0a8bd68b42050638c')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (64, N'DEMO-0064', N'41000001', 1, CAST(N'2026-11-19T14:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-19T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-64', N'fd7974750bb327a5c47008131b0fdefcddfecd001bc7483f97bc65bc9106edb8')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (65, N'DEMO-0065', N'41000002', 1, CAST(N'2026-11-18T15:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-18T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-65', N'6a749598056faacba375fd01bb4560045006559ac2871997861ad4d0a809c80d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (66, N'DEMO-0066', N'41000003', 1, CAST(N'2026-11-17T16:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-17T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-66', N'842c1fa5ce3b2dc161d6ae8254609519f71fe2c2cac351357f227672253c0273')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (67, N'DEMO-0067', N'41000004', 1, CAST(N'2026-11-16T17:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-16T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-67', N'5a3cb94917724b9e0c73918ca006b1520149393b9c212210640e149698eb02cc')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (68, N'DEMO-0068', N'41000005', 1, CAST(N'2026-11-15T18:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-15T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-68', N'c2a8b767714e04ad4a7dd08d1b38ec412f7dd741369a5559445f5b1bc15ada0b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (69, N'DEMO-0069', N'41000006', 1, CAST(N'2026-11-14T19:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-14T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-69', N'd438456ad640154d3a47a80c17c806c52ba0cec469d2eb1bbb6c5c69adb8625a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (70, N'DEMO-0070', N'41000010', 1, CAST(N'2026-11-13T10:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-13T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-70', N'4bbe3d7bb5eb06fb9f99ba64941fef23699efb0c0b0eec8e87d01b1e5a8cf4a1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (71, N'DEMO-0071', N'41000008', 1, CAST(N'2026-11-12T11:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-12T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-71', N'0f93818fbe8a67b216e649a14a5b03bc91d27941d77b9974f97dfd1e069e8ac9')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (72, N'DEMO-0072', N'41000009', 1, CAST(N'2026-11-11T12:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-11T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-72', N'1c0e7be50b3752b970c455821fa475523c05a3cdd4efe2483a88acafe9440192')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (73, N'DEMO-0073', N'41000001', 1, CAST(N'2026-11-10T13:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-10T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-73', N'58065c2a1751656e696d9e4f818e4ee23e577281c36023bd3538b4e9f368f397')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (74, N'DEMO-0074', N'41000002', 1, CAST(N'2026-11-09T14:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-09T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-74', N'eae9a83ee52427751b09c29caa9b5fb84344f427da3427a9986c1b4847502dfe')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (75, N'DEMO-0075', N'41000003', 1, CAST(N'2026-11-08T15:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-08T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-75', N'0ccd211083599190eb7ef3c5c20c10d09ac4780b865772f2114dc6ff02d11cfb')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (76, N'DEMO-0076', N'41000004', 1, CAST(N'2026-11-07T16:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-07T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-76', N'1faf8d936eebf2dc0105a0db33f058c9a3479f842f7a6539d17a8c126e56cd60')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (77, N'DEMO-0077', N'41000005', 1, CAST(N'2026-11-06T17:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-06T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-77', N'8294a31faff7005c7003f6ab93f99aaadf6514681a5739067333e94abf81d548')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (78, N'DEMO-0078', N'41000006', 1, CAST(N'2026-11-05T18:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-05T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-78', N'0da1e2a3d148742de812a549e11f28b4813e9f37d4600db302858b7b11cdf422')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (79, N'DEMO-0079', N'41000007', 1, CAST(N'2026-11-04T19:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-04T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-79', N'953657e66ddeaaa40cb96557ec10d0e45dc96f52a2cc356cb76c1b5406cbe093')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (80, N'DEMO-0080', N'41000010', 1, CAST(N'2026-11-03T10:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-03T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-80', N'c3d30c30376b45ccf14b682537212cb12da6554e696d6881ff59cc895536b481')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (81, N'DEMO-0081', N'41000009', 1, CAST(N'2026-11-02T11:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-02T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-81', N'63f0c63df943127d27709266a7b0ec0c48355bc8eb90fe3c703963b34b79a3d1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (82, N'DEMO-0082', N'41000001', 1, CAST(N'2026-11-01T12:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-01T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-82', N'c4f976ccf5748a68332d55da5adc22e2fefea59af78ef03e18c7e5a091ceb2c8')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (83, N'DEMO-0083', N'41000002', 1, CAST(N'2026-10-31T13:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-31T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-83', N'14452360c89cd8ec1f56ebaadecbbce473a86bab1f8cc2ddbc4946ea3c542ac2')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (84, N'DEMO-0084', N'41000003', 1, CAST(N'2026-10-30T14:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-30T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-84', N'b5d4b8abc1d68f54b6593a08173a0b3bc0a667f25e8c51f287940304ba9b7258')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (85, N'DEMO-0085', N'41000004', 1, CAST(N'2026-11-26T15:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-26T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-85', N'fea4b3b6f717210ab5236856abc4b67b69be3249855fb6cf456fa348467d0e85')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (86, N'DEMO-0086', N'41000005', 1, CAST(N'2026-11-25T16:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-25T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-86', N'99e80cd0a202feb699f0c626565bfb38fe1635c159d69a1030e196e98c7feb71')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (87, N'DEMO-0087', N'41000006', 1, CAST(N'2026-11-24T17:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-24T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-87', N'439fdfa42482a914dc60de3ebcbb98c1c8689225d2dc0f6a33f7576c8db632a7')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (88, N'DEMO-0088', N'41000007', 1, CAST(N'2026-11-23T18:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-23T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-88', N'063b5114cedc9b00300ee486d8d18a4774b7386dc51f356ab8b91efbaccad771')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (89, N'DEMO-0089', N'41000008', 1, CAST(N'2026-11-22T19:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-22T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-89', N'd4d376a0b971b73daa5a330f28d650ac6dcd9cd9f87b9e2c1e02269abee8f1db')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (90, N'DEMO-0090', N'41000010', 1, CAST(N'2026-11-21T10:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-21T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-90', N'1aa56624d2d470c8b7766ff2cbe13513884779e036812435ef2dc2017c366011')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (91, N'DEMO-0091', N'41000001', 1, CAST(N'2026-11-20T11:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-20T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-91', N'5700c15fb3e7aefb0fe5fdfa1d34171c4e7dea674616aade4cf553eebdb390c1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (92, N'DEMO-0092', N'41000002', 1, CAST(N'2026-11-19T12:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-19T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-92', N'0c587cc8a08816d3701574aafc4f57e810826d81dc4b1a1fdd26d550f28fd850')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (93, N'DEMO-0093', N'41000003', 1, CAST(N'2026-11-18T13:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-18T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-93', N'99ff8574325623644f9a9be43b3b417fa5c0dcdf11c69807948200ddd4868d9d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (94, N'DEMO-0094', N'41000004', 1, CAST(N'2026-11-17T14:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-17T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-94', N'ef1a6369cf0e112babcf0fae12786b2234fed0eb013f0da20ccbcfea332dc8e0')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (95, N'DEMO-0095', N'41000005', 1, CAST(N'2026-11-16T15:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-16T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-95', N'759604b933c31fdf2145d1ed94dbf3f9b31c96cb184254a322f4268de864de08')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (96, N'DEMO-0096', N'41000006', 1, CAST(N'2026-11-15T16:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-15T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-96', N'a2237e6768c77bd3129d870421ed08d9e885f8b8ce543a2a76b5ab7febc6a34d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (97, N'DEMO-0097', N'41000007', 1, CAST(N'2026-11-14T17:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-14T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-97', N'4233ea312249e0a3be5c072367e922a8a1041d73fd498d7979fb678757e7d20b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (98, N'DEMO-0098', N'41000008', 1, CAST(N'2026-11-13T18:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-13T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-98', N'b49272c5a819bbc7c147518a98ae87acb72322647a5b7f57ca46f8f220e861b6')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (99, N'DEMO-0099', N'41000009', 1, CAST(N'2026-11-12T19:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-12T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-99', N'd9dbce008429e699cbf0d16ece4ff9c8d18d749416bd5a44df8d08895ca3a181')
GO
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (100, N'DEMO-0100', N'41000010', 1, CAST(N'2026-11-11T10:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-11T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-100', N'38ffd8094cee0493ec43f05cbec88524506d71e16b7b4b024bdfb77f093eb089')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (101, N'DEMO-0101', N'41000002', 1, CAST(N'2026-11-10T11:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-10T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-101', N'b1e9c53ec9ed19a846e5a8fd264f25e3160c1b6ab03790719c04cea7d130bc0c')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (102, N'DEMO-0102', N'41000003', 1, CAST(N'2026-11-09T12:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-09T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-102', N'63ff11aa12736ec7e9edb736ae2d3032309efaeff2db3b3cca53a39596a170e6')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (103, N'DEMO-0103', N'41000004', 1, CAST(N'2026-11-08T13:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-08T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-103', N'01a5b374042596f7e88596d8143410dfebd0d20a3d9335feca0f7648704b1063')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (104, N'DEMO-0104', N'41000005', 1, CAST(N'2026-11-07T14:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-07T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-104', N'6791fdcd64868f10412c962601ddeda4465215aa583f636c30885c04fe6bb71a')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (105, N'DEMO-0105', N'41000006', 1, CAST(N'2026-11-06T15:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-06T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-105', N'd05133d1cb19b17a61afd81d643d09e6c823aa6d0bec3cb98f0ec773ebb29735')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (106, N'DEMO-0106', N'41000007', 1, CAST(N'2026-11-05T16:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-05T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-106', N'9449bc6d45018cca6b11dd6b302985fd543672eee88eff1e8075d7e7334b2755')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (107, N'DEMO-0107', N'41000008', 1, CAST(N'2026-11-04T17:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-04T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-107', N'c8ff9ceb216fcc395b63ea0002dbb70ea3484ef076b5410fd9d31440a20b903d')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (108, N'DEMO-0108', N'41000009', 1, CAST(N'2026-11-03T18:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-03T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-108', N'2e1d670cb1e16e73a27ae50cdbabea588a8774cfec3475cfa7aa61f2725a6e18')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (109, N'DEMO-0109', N'41000001', 1, CAST(N'2026-11-02T19:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-02T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-109', N'37015654c75ffb5d30aad012009e05ff8fb71cca76b50b91d190751688e34caa')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (110, N'DEMO-0110', N'41000010', 1, CAST(N'2026-11-01T10:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-01T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-110', N'5715e9dcf9e8e493d830dea91871a866b860e7b3d410a47789deea7c4ba8ebcd')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (111, N'DEMO-0111', N'41000003', 1, CAST(N'2026-10-31T11:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-31T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-111', N'b586d434f9a2d86856142040b10c2067bb85f7b919342c666abea8727219b7b2')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (112, N'DEMO-0112', N'41000004', 1, CAST(N'2026-10-30T12:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-30T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-112', N'5f4b4ecac33067a4dc22bc5c92e8f991762f134bb13f14d991ad4745f444eb7f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (113, N'DEMO-0113', N'41000005', 1, CAST(N'2026-11-26T13:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-26T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-113', N'5a6ef5cd331f197dcd5d55376a6d851b4e961ccd275fa483f5324c8e73726f89')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (114, N'DEMO-0114', N'41000006', 1, CAST(N'2026-11-25T14:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-25T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-114', N'45f5cd55cb6a2de19cb094e1d4410c7ffb58e0ca36b972041e5455d39c30cfc1')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (115, N'DEMO-0115', N'41000007', 1, CAST(N'2026-11-24T15:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-24T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-115', N'768131a26138489b31b70faf643003aca5f656530e06c853138ec55a86673570')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (116, N'DEMO-0116', N'41000008', 1, CAST(N'2026-11-23T16:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-23T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-116', N'd6c3a81a2d8c7d5fcc9d2d7ba915cd4cae6c24ba835c7bacac1f740f9ce4a311')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (117, N'DEMO-0117', N'41000009', 1, CAST(N'2026-11-22T17:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-22T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-117', N'0be64f18e1b9ac479464e2816c73356ea2c636a79a07325f1f277cc7ba843591')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (118, N'DEMO-0118', N'41000001', 1, CAST(N'2026-11-21T18:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-21T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-118', N'9e987813d2b7637da2246b68adc2fb0cd9c5f17e20d316ebd38e6f046528e3f3')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (119, N'DEMO-0119', N'41000002', 1, CAST(N'2026-11-20T19:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-20T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-119', N'56dc8f53e50cbd0694dda69c142068e874f940c243918d41f1bd6258ac3ca351')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (120, N'DEMO-0120', N'41000010', 1, CAST(N'2026-11-19T10:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-19T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-120', N'37d6cefd6178cc4dae72d176188d69ea886bd5f211b36630a370275868a82e25')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (121, N'DEMO-0121', N'41000004', 1, CAST(N'2026-11-18T11:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-18T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-121', N'0ba411cd7afc079aac8e9a31cd746b1f12335a2f883103acfe127eab510232ca')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (122, N'DEMO-0122', N'41000005', 1, CAST(N'2026-11-17T12:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-17T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-122', N'351291b9cf4d56af929d5730bd5757ab463d354833bdbc4bf793c54ded63dfb3')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (123, N'DEMO-0123', N'41000006', 1, CAST(N'2026-11-16T13:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-16T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-123', N'cd9d29b7612da5dfe42be7d6ce6ba1d6345d6465659e7029b9859a60ba933384')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (124, N'DEMO-0124', N'41000007', 1, CAST(N'2026-11-15T14:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-15T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-124', N'8f437a86a574de88fcf3c26846ffe49a283748138daa532d0b6f6be48e878df2')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (125, N'DEMO-0125', N'41000008', 1, CAST(N'2026-11-14T15:00:00.0000000' AS DateTime2), CAST(10400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-14T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-125', N'adf4f0c2f198378c0b16fdfac2f0f427c76e3c6a8f77e9934ee21484580fd740')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (126, N'DEMO-0126', N'41000009', 1, CAST(N'2026-11-13T16:00:00.0000000' AS DateTime2), CAST(7500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-13T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-126', N'53f6686546740a148a4fd2584d2052b1a75f900d80fa40081475267947e5be9f')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (127, N'DEMO-0127', N'41000001', 1, CAST(N'2026-11-12T17:00:00.0000000' AS DateTime2), CAST(12800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-12T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-127', N'90b499a7d486fd653d8f34f88b8dcf86d8218214ab87834ec9cac8677483f083')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (128, N'DEMO-0128', N'41000002', 1, CAST(N'2026-11-11T18:00:00.0000000' AS DateTime2), CAST(1500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-11T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-128', N'fa5e71904f1acdec4f6ad682120c667fe6cb0e356132c500b2c9a1bef95d7c99')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (129, N'DEMO-0129', N'41000003', 1, CAST(N'2026-11-10T19:00:00.0000000' AS DateTime2), CAST(3000.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-10T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-129', N'337d62155fe3777f53e6d800f95186c63fdce28f7551a876c0e8540c6a656eb7')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (130, N'DEMO-0130', N'41000010', 1, CAST(N'2026-11-09T10:00:00.0000000' AS DateTime2), CAST(4500.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-09T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-130', N'11b70c3db31e72b68fe63c5ea59955d1ed19f4c074f7d0ce0c287eebc2745a31')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (131, N'DEMO-0131', N'41000005', 1, CAST(N'2026-11-08T11:00:00.0000000' AS DateTime2), CAST(11200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-08T11:00:00.0000000' AS DateTime2), N'AUT-DEMO-131', N'28e55712babfeb15b44d3d61c7486aee0d9a84136fda74d4cd49ed5a46daf25b')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (132, N'DEMO-0132', N'41000006', 1, CAST(N'2026-11-07T12:00:00.0000000' AS DateTime2), CAST(2800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-07T12:00:00.0000000' AS DateTime2), N'AUT-DEMO-132', N'26c94ecc9fa6401d22984933ca15d383d504c75db51c3eeb562cda266def4739')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (133, N'DEMO-0133', N'41000007', 1, CAST(N'2026-11-06T13:00:00.0000000' AS DateTime2), CAST(3200.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-06T13:00:00.0000000' AS DateTime2), N'AUT-DEMO-133', N'e89339f62d01bd7d42b9e8fc2923fa636748cd4a825661bb99cab04d0252b623')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (134, N'DEMO-0134', N'41000008', 1, CAST(N'2026-11-05T14:00:00.0000000' AS DateTime2), CAST(8700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-05T14:00:00.0000000' AS DateTime2), N'AUT-DEMO-134', N'6d23e561a52aade9c1ab3d10a1b7a73a8889a226d205e54a8c67050e025eccd0')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (135, N'DEMO-0135', N'41000009', 1, CAST(N'2026-11-04T15:00:00.0000000' AS DateTime2), CAST(11600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-04T15:00:00.0000000' AS DateTime2), N'AUT-DEMO-135', N'cb572e29249dc0f82f733d4cbe0c5fc7f21eeeed0d758c8defb3f0f31b48c497')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (136, N'DEMO-0136', N'41000001', 1, CAST(N'2026-11-03T16:00:00.0000000' AS DateTime2), CAST(2900.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-03T16:00:00.0000000' AS DateTime2), N'AUT-DEMO-136', N'7cae41186bf7d703128a9ce7f436f1ad6dc043293a4fc5a7f02cee28888952cc')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (137, N'DEMO-0137', N'41000002', 1, CAST(N'2026-11-02T17:00:00.0000000' AS DateTime2), CAST(5700.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-02T17:00:00.0000000' AS DateTime2), N'AUT-DEMO-137', N'5a29bb63eadbe11eb900938762c782b50757e6cefde7fbb6a52319eb4b237dac')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (138, N'DEMO-0138', N'41000003', 1, CAST(N'2026-11-01T18:00:00.0000000' AS DateTime2), CAST(10800.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-11-01T18:00:00.0000000' AS DateTime2), N'AUT-DEMO-138', N'66bb1a1b808fcb24e659bbdbe456aaafccc1f81aa14e947bd2b0d0db209f6eea')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (139, N'DEMO-0139', N'41000004', 1, CAST(N'2026-10-31T19:00:00.0000000' AS DateTime2), CAST(14400.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-31T19:00:00.0000000' AS DateTime2), N'AUT-DEMO-139', N'c56af32456e92c16ad740283682b3e2540772da31f0f4593a6cd786d2bcaeb24')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (140, N'DEMO-0140', N'41000010', 1, CAST(N'2026-10-30T10:00:00.0000000' AS DateTime2), CAST(2600.00 AS Decimal(12, 2)), N'Pagada', N'Banco Demo', N'1234', CAST(N'2026-10-30T10:00:00.0000000' AS DateTime2), N'AUT-DEMO-140', N'1a3f60ff0ed5c82661328ff58fe65057db776242605fc15f4487e9bfdce8c316')
INSERT [dbo].[FacturaBuffet] ([IdFactura], [NumeroFactura], [DNI], [IdVendedor], [FechaHoraEmision], [ImporteTotal], [Estado], [Banco], [Ultimos4Tarjeta], [FechaHoraPago], [CodigoAutorizacion], [DVH]) VALUES (141, N'FB-20261109-6C87736DFB', N'11111111', 2, CAST(N'2026-11-26T12:43:21.5929686' AS DateTime2), CAST(1600.00 AS Decimal(12, 2)), N'Pagada', N'adads', N'2134', CAST(N'2026-11-26T12:43:21.5306832' AS DateTime2), N'FB-1ECC9073E211', N'02f38e571bac699e8501c509682c18ca8c16ead53dc8bfd763c4a96ee77e71d3')
SET IDENTITY_INSERT [dbo].[FacturaBuffet] OFF
GO
SET IDENTITY_INSERT [dbo].[Familia] ON 

INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (1, N'Sesión', N'8e7a9630253803d9fabd34ed9f974b1478805cac0f83f50375d7111f1ed2a39f')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (2, N'Gestión de Usuarios', N'88cb8da76653546918837cb79f7e5ae674e369c099953018b3b5eaccd3bba807')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (3, N'Bitácora', N'0ccc7005cfabb92cbac4fadf23553a1a26953dbf6b1cb5b0e33d614591ee8be5')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (4, N'RBAC', N'b7e2e53022e5e83073fc6e73189d6119493fafbd592b3de0163b3aaaf9c4db7a')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (5, N'Reservas', N'4f86698cec518bf106fdc37b75cd21f66c9476a6d8c7112e323b79491217f96e')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (6, N'Clientes', N'1b6cf98dffb4309a85c5504ba4439f5e746f8ee06b1bc1301ec352f6f772e2f4')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (7, N'Buffet', N'c3dada9b953d968c56dc27ea8d503f3988caf4db6fda9473b672a49c4e265ea9')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (8, N'Administración Total', N'2bcfc06f48977987ce2108e3e6b623735737b1976191816c53ed8ba93b5ab278')
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [DVH]) VALUES (13, N'Canchas', N'e9d02c0a64e7727f9380cb3a264ceb60fedfa9918d25e562c735da7d2648f63e')
SET IDENTITY_INSERT [dbo].[Familia] OFF
GO
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 2, N'a9e264cb4c953f958e10a18334af28d3229562f48c1ec33c0a5f94df26dcb5d9')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 3, N'29694ce63ca04a055a1f6284f59acd208be2cb74edc443689f231def0f55c18e')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 4, N'9ccad5595eaedbf07fc1ada91dcd2805c6c544430424ce91c69f709c02a52103')
GO
SET IDENTITY_INSERT [dbo].[Idioma] ON 

INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (1, N'ES', N'Español', N'1f83a4462935948663a6223389e13dab529e0db28bbf75ad071f08d161654e73')
INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (2, N'EN', N'English', N'b98cc3676414c0928ab8a26a2e721b1400a585267b969cd4b35b87a2b6d88b9f')
SET IDENTITY_INSERT [dbo].[Idioma] OFF
GO
SET IDENTITY_INSERT [dbo].[PermisoSimple] ON 

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
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre], [DVH]) VALUES (34, N'PRO_AUDITAR_CAMBIOS', N'Auditar y restaurar versiones de productos', N'8a2775d6e7a733c14a9157c3b50ad3c50085882bd991b48a3642938160606b8a')
SET IDENTITY_INSERT [dbo].[PermisoSimple] OFF
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
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (13, 28, N'90f395f0b6f1de9c74af4eab4a3a4aac2927f091fe479344113ba1c9355a976b')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (8, 34, N'041d41a229f1cd12788911d22580fe3cf17fa0a293f168cbd630c1c0fddf60c1')
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (1, N'Agua mineral sin gas 500 ml', N'Bebidas', CAST(1500.00 AS Decimal(12, 2)), 26, 10, 1, N'663db81774fd1a80e68d8106c6f8252541abbf30f295854b8244f34f87991226')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (2, N'Agua mineral 1.5 L', N'Bebidas', CAST(2800.00 AS Decimal(12, 2)), 10, 4, 1, N'68c33413583bc27c382f84876858548fbee2ea08147d9f6803c6308ad867a0a7')
INSERT [dbo].[Producto] ([CodigoProducto], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [Activo], [DVH]) VALUES (3, N'Agua con gas 500 ml', N'Bebidas', CAST(1600.00 AS Decimal(12, 2)), 10, 4, 1, N'6b012c4dc750b76337ed5a89b328b89bc5d05ddfa597fa4be2fbc85cdb93fff1')
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
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto_C] ON 

INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (1, 1, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Agua mineral sin gas 500 ml', N'Bebidas', CAST(1500.00 AS Decimal(12, 2)), 26, 10, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (2, 2, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Agua mineral 1.5 L', N'Bebidas', CAST(2800.00 AS Decimal(12, 2)), 10, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (4, 4, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Gaseosa cola 500 ml', N'Bebidas', CAST(2900.00 AS Decimal(12, 2)), 23, 8, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (5, 5, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Gaseosa lima limon 500 ml', N'Bebidas', CAST(2900.00 AS Decimal(12, 2)), 17, 6, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (6, 6, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Gaseosa naranja 500 ml', N'Bebidas', CAST(2850.00 AS Decimal(12, 2)), 5, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (7, 7, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Bebida isotonica naranja 500 ml', N'Bebidas', CAST(3600.00 AS Decimal(12, 2)), 15, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (8, 8, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Bebida isotonica limon 500 ml', N'Bebidas', CAST(3600.00 AS Decimal(12, 2)), 2, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (9, 9, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Jugo de naranja 500 ml', N'Bebidas', CAST(2600.00 AS Decimal(12, 2)), 8, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (10, 10, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Energizante lata 250 ml', N'Bebidas', CAST(5200.00 AS Decimal(12, 2)), 7, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (11, 11, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Cafe expreso', N'Cafeteria', CAST(2500.00 AS Decimal(12, 2)), 39, 10, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (12, 12, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Cafe con leche', N'Cafeteria', CAST(3200.00 AS Decimal(12, 2)), 16, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (13, 13, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Te', N'Cafeteria', CAST(2000.00 AS Decimal(12, 2)), 15, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (14, 14, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Chocolate caliente', N'Cafeteria', CAST(3400.00 AS Decimal(12, 2)), 8, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (15, 15, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Alfajor de chocolate', N'Snacks', CAST(2000.00 AS Decimal(12, 2)), 19, 6, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (16, 16, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Alfajor de maicena', N'Snacks', CAST(2100.00 AS Decimal(12, 2)), 6, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (17, 17, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Barra de cereal', N'Snacks', CAST(1800.00 AS Decimal(12, 2)), 12, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (18, 18, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Galletitas dulces', N'Snacks', CAST(2700.00 AS Decimal(12, 2)), 13, 4, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (19, 19, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Galletitas saladas', N'Snacks', CAST(2600.00 AS Decimal(12, 2)), 7, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (20, 20, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Papas fritas paquete', N'Snacks', CAST(3400.00 AS Decimal(12, 2)), 8, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (21, 21, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Mani salado', N'Snacks', CAST(2600.00 AS Decimal(12, 2)), 5, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (22, 22, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Tostado jamon y queso', N'Comidas', CAST(6500.00 AS Decimal(12, 2)), 10, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (23, 23, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Sandwich de pollo', N'Comidas', CAST(7900.00 AS Decimal(12, 2)), 6, 2, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (24, 24, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Sandwich vegetariano', N'Comidas', CAST(7300.00 AS Decimal(12, 2)), 4, 2, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (25, 25, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Medialuna', N'Comidas', CAST(1700.00 AS Decimal(12, 2)), 15, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (26, 26, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Porcion de pizza', N'Comidas', CAST(5800.00 AS Decimal(12, 2)), 9, 3, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (27, 27, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Caramelos unidad', N'Golosinas', CAST(500.00 AS Decimal(12, 2)), 50, 16, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (28, 28, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Chicle unidad', N'Golosinas', CAST(900.00 AS Decimal(12, 2)), 18, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (29, 29, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Galletitas Toddy', N'Snacks', CAST(2400.00 AS Decimal(12, 2)), 6, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (30, 30, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Coca Cola', N'Bebidas', CAST(2100.00 AS Decimal(12, 2)), 10, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (31, 33, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Gaseosa Pepsi', N'Bebidas', CAST(3100.00 AS Decimal(12, 2)), 9, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (32, 34, CAST(N'2026-11-09T12:35:34.1520000' AS DateTime2), N'Prueba', N'Bebidas', CAST(2000.00 AS Decimal(12, 2)), 20, 5, 1, 1)
INSERT [dbo].[Producto_C] ([IdCambio], [CodigoProducto], [FechaHora], [Nombre], [Categoria], [PrecioUnitario], [StockDisponible], [StockMinimo], [ActivoProducto], [Act]) VALUES (33, 3, CAST(N'2026-11-09T12:43:21.5900000' AS DateTime2), N'Agua con gas 500 ml', N'Bebidas', CAST(1600.00 AS Decimal(12, 2)), 10, 4, 1, 1)
SET IDENTITY_INSERT [dbo].[Producto_C] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (1, N'Administrador', N'e968303d5a0735207eb00fd12bf270e9d83f5e269fd9bab80a26424e3954441f')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (2, N'Vendedor de Buffet', N'8fcd2473f9162ea7f574ab6fda26376b775efd5a2a7188d53648104f8d5ff67b')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (3, N'Recepcionista', N'9d65b912fda8c9906289143693ab2b3999de7c6430c4a6e001e7f57188e24b8c')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (4, N'Supervisor', N'77f52042ee0c9f4eafc80b63fe08383efe3cdf607d53c9046b873415d4af4538')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (5, N'Encargado de Canchas', N'714816c1e538b87ea88b9540cce7d1f1afaf7eb6e5888ca34bf904630261ba56')
SET IDENTITY_INSERT [dbo].[Rol] OFF
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
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (1, 29, N'32c556fb186927442698fd68ca98c72da34ae2c3a8a5ddf4d05e0dca9f4da5b9')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (1, 30, N'b10f6285b7a31102d191a7f4ea6812baab6f313d257a92154487a5988ac05cae')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 30, N'96866a4ce8b4f0299246ba15185965f942ab653dd00f06c743b4dc521f759266')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 32, N'e40dbfc826024b8cfad402a6d4b21b1817f6a8ea535fa239d14cb2e1c9556c26')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (4, 33, N'5ac69ddc4dc620d0fa43b9c6ebcc7e34c18b18a99a3d9fed74acd7c609ca1842')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (2, 30, N'f3cd24ffdc1062aea60502bb1528190b2314b91a0415eb0db7f232f1a214bd6b')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (2, 25, N'd0147af625798484c4e263f6efa3c3c1e886fc774832aa65dabee1b3a150d85f')
INSERT [dbo].[Rol_PermisoSimple] ([IdRol], [IdPermisoSimple], [DVH]) VALUES (3, 25, N'c28d843778d6313eca5e15ea77ffbb3e05cc745a5b6eb4172c338f19069c1c26')
GO
SET IDENTITY_INSERT [dbo].[Tarifa] ON 

INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (1, N'Diurna', CAST(30000.00 AS Decimal(12, 2)), CAST(N'08:30:00' AS Time), CAST(N'17:30:00' AS Time), 1, N'01d171f677f4fac8f86c9e8a44e8f8903a9ffaff7adf787d72c19f2bafdb3642')
INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (2, N'Nocturna', CAST(45000.00 AS Decimal(12, 2)), CAST(N'17:30:00' AS Time), CAST(N'22:00:00' AS Time), 1, N'dc1b62070c4361943e0b14a1c93dd2f52d3b09fb7532d1a091871ad7a4bcda3a')
SET IDENTITY_INSERT [dbo].[Tarifa] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (1, N'Administrador', N'Rodriguez', N'46631736', N'admin1@padelgest.com', N'admin1', N'5456cc85efead5b7762b131e99389aa4b861199686c6a38f1f2bd4154c61b409', 1, 0, 0, 0, 1, 1, N'a41ee631585a381fadea84b7fe2d32db3e765bb08879a1a1cc2d26e580459102')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (2, N'Leonel', N'Mariolani', N'11113333', N'supervisor@padelgest.com', N'dueño11113333', N'9d3c8c6731e909c1d2319294c0466d66ffd0e21d04532f5c8a2776b5d9c33acf', 1, 0, 0, 0, 1, 4, N'2cf54c4e8ec6a0c28d345a125767ae23beaa6fac2c071e9454d1cd9531b7dfd2')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (3, N'Lucas', N'Pereyra', N'22224444', N'Vendedor@padelgest.com', N'vendedor22224444', N'3f941ccc5c5773b0a3b0e831aabb9ead199e7b9d001e56178d7495d3413e1f44', 1, 0, 0, 0, 1, 2, N'89cb4e221d852f16f00afa63ce6447868b38083e119b4176c1b5c02ca6eab7b3')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Cancha_Nombre]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Cancha] ADD  CONSTRAINT [UQ_Cancha_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_DetalleFactura_FacturaProducto]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[DetalleFactura] ADD  CONSTRAINT [UQ_DetalleFactura_FacturaProducto] UNIQUE NONCLUSTERED 
(
	[IdFactura] ASC,
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetalleFactura_Producto]    Script Date: 9/11/2026 12:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_DetalleFactura_Producto] ON [dbo].[DetalleFactura]
(
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Equipamiento_Tipo]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Equipamiento] ADD  CONSTRAINT [UQ_Equipamiento_Tipo] UNIQUE NONCLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_FacturaBuffet_CodigoAutorizacion]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [UQ_FacturaBuffet_CodigoAutorizacion] UNIQUE NONCLUSTERED 
(
	[CodigoAutorizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_FacturaBuffet_NumeroFactura]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[FacturaBuffet] ADD  CONSTRAINT [UQ_FacturaBuffet_NumeroFactura] UNIQUE NONCLUSTERED 
(
	[NumeroFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FacturaBuffet_DNI_Fecha]    Script Date: 9/11/2026 12:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FacturaBuffet_DNI_Fecha] ON [dbo].[FacturaBuffet]
(
	[DNI] ASC,
	[FechaHoraEmision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FacturaBuffet_Vendedor_Fecha]    Script Date: 9/11/2026 12:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_FacturaBuffet_Vendedor_Fecha] ON [dbo].[FacturaBuffet]
(
	[IdVendedor] ASC,
	[FechaHoraEmision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Familia_Nombre]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [UQ_Familia_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Idioma_Codigo]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Idioma] ADD  CONSTRAINT [UQ_Idioma_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_PermisoSimple_Codigo]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[PermisoSimple] ADD  CONSTRAINT [UQ_PermisoSimple_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Producto_ActivoCategoriaNombre]    Script Date: 9/11/2026 12:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_Producto_ActivoCategoriaNombre] ON [dbo].[Producto]
(
	[Activo] ASC,
	[Categoria] ASC,
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductoC_Consulta]    Script Date: 9/11/2026 12:48:40 ******/
CREATE NONCLUSTERED INDEX [IX_ProductoC_Consulta] ON [dbo].[Producto_C]
(
	[CodigoProducto] ASC,
	[FechaHora] DESC,
	[IdCambio] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UX_ProductoC_VersionVigente]    Script Date: 9/11/2026 12:48:40 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_ProductoC_VersionVigente] ON [dbo].[Producto_C]
(
	[CodigoProducto] ASC
)
WHERE ([Act]=(1))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Reserva_Disponibilidad]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Index [UQ_Reserva_Codigo]    Script Date: 9/11/2026 12:48:40 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Reserva_Codigo] ON [dbo].[Reserva]
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Reserva_IdFactura]    Script Date: 9/11/2026 12:48:40 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Reserva_IdFactura] ON [dbo].[Reserva]
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Rol_Nombre]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [UQ_Rol_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_DNI]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_Email]    Script Date: 9/11/2026 12:48:40 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_NombreUsuario]    Script Date: 9/11/2026 12:48:40 ******/
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
/****** Object:  Trigger [dbo].[TR_Producto_RegistrarCambio]    Script Date: 9/11/2026 12:48:40 ******/
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
    WHERE
    (
        d.CodigoProducto IS NULL
        OR EXISTS
        (
            SELECT i.Nombre, i.Categoria, i.PrecioUnitario,
                   i.StockDisponible, i.StockMinimo, i.Activo
            EXCEPT
            SELECT d.Nombre, d.Categoria, d.PrecioUnitario,
                   d.StockDisponible, d.StockMinimo, d.Activo
        )
    )
    -- Si la version historica ya fue marcada como vigente y sus valores
    -- coinciden con Producto, se trata de una restauracion: no insertar
    -- otra fila ni alterar la fecha original de Producto_C.
    AND NOT EXISTS
    (
        SELECT 1
        FROM dbo.Producto_C AS h
        WHERE h.CodigoProducto = i.CodigoProducto
          AND h.Act = 1
          AND h.Nombre = i.Nombre
          AND h.Categoria = i.Categoria
          AND h.PrecioUnitario = i.PrecioUnitario
          AND h.StockDisponible = i.StockDisponible
          AND h.StockMinimo = i.StockMinimo
          AND h.ActivoProducto = i.Activo
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
        StockDisponible, StockMinimo, ActivoProducto, Act
    )
    SELECT
        i.CodigoProducto, SYSDATETIME(),
        i.Nombre, i.Categoria, i.PrecioUnitario,
        i.StockDisponible, i.StockMinimo, i.Activo, 1
    FROM inserted i
    INNER JOIN @Cambios c ON c.CodigoProducto = i.CodigoProducto
    LEFT JOIN deleted d ON d.CodigoProducto = i.CodigoProducto;
END;
GO
ALTER TABLE [dbo].[Producto] ENABLE TRIGGER [TR_Producto_RegistrarCambio]
GO
/****** Object:  Trigger [dbo].[TR_ProductoC_RestaurarVersion]    Script Date: 9/11/2026 12:48:40 ******/
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

    -- Actualiza los datos actuales del producto con la version elegida.
    -- TR_Producto_RegistrarCambio detecta que ya existe esa version vigente
    -- y no crea otra fila en Producto_C: se conserva su fecha historica.
    -- Todo ocurre dentro de la misma transaccion.
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
ALTER TABLE [dbo].[Producto_C] ENABLE TRIGGER [TR_ProductoC_RestaurarVersion]
GO

-- Comprobaciones basicas para la demostracion
SELECT N'Usuario' AS Tabla, COUNT(*) AS Registros FROM dbo.Usuario
UNION ALL SELECT N'Cliente', COUNT(*) FROM dbo.Cliente
UNION ALL SELECT N'Producto', COUNT(*) FROM dbo.Producto
UNION ALL SELECT N'Producto_C', COUNT(*) FROM dbo.Producto_C
UNION ALL SELECT N'FacturaBuffet', COUNT(*) FROM dbo.FacturaBuffet
UNION ALL SELECT N'DetalleFactura', COUNT(*) FROM dbo.DetalleFactura;
GO