USE [PadelGestDB]
/****** Object:  Table [dbo].[BitacoraEvento]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Cancha]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Cliente]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Cliente](
	[DNI] [nvarchar](8) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](30) NOT NULL,
	[CorreoElectronico] [nvarchar](150) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[DigitoVerificador]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[DigitoVerificador](
	[NombreTabla] [nvarchar](100) NOT NULL,
	[DVV] [varchar](64) NULL,
 CONSTRAINT [PK_DigitoVerificador] PRIMARY KEY CLUSTERED 
(
	[NombreTabla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[Equipamiento]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Factura]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Familia]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Familia](
	[IdFamilia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[Familia_Familia]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Idioma]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
SET QUOTED_IDENTIFIER ON
/****** Object:  Table [dbo].[PermisoSimple]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[PermisoSimple_Familia]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Producto]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Reserva]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[RespaldoBD]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Rol]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DVH] [varchar](64) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[Rol_Familia]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Rol_PermisoSimple]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Tarifa]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 19/10/2026 15:17:06 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
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
/****** PN2 NUEVO: FacturaBuffet y DetalleFactura (Factura de PN1 intacta) ******/
CREATE TABLE [dbo].[FacturaBuffet] (
 [IdFactura] int IDENTITY(1,1) NOT NULL,
 [NumeroFactura] nvarchar(30) NOT NULL,
 [DNI] nvarchar(8) NOT NULL,
 [IdVendedor] int NOT NULL,
 [FechaHoraEmision] datetime2(7) NOT NULL CONSTRAINT [DF_FacturaBuffet_FechaHoraEmision] DEFAULT (SYSDATETIME()),
 [ImporteTotal] decimal(12,2) NOT NULL,
 [Estado] nvarchar(20) NOT NULL CONSTRAINT [DF_FacturaBuffet_Estado] DEFAULT (N'Pagada'),
 [Banco] nvarchar(80) NOT NULL,
 [Ultimos4Tarjeta] char(4) NOT NULL,
 [FechaHoraPago] datetime2(7) NOT NULL,
 [CodigoAutorizacion] nvarchar(50) NOT NULL,
 [DVH] varchar(64) NULL,
 CONSTRAINT [PK_FacturaBuffet] PRIMARY KEY ([IdFactura]),
 CONSTRAINT [UQ_FacturaBuffet_NumeroFactura] UNIQUE ([NumeroFactura]),
 CONSTRAINT [UQ_FacturaBuffet_CodigoAutorizacion] UNIQUE ([CodigoAutorizacion]),
 CONSTRAINT [FK_FacturaBuffet_Cliente] FOREIGN KEY ([DNI]) REFERENCES [dbo].[Cliente]([DNI]),
 CONSTRAINT [FK_FacturaBuffet_Vendedor] FOREIGN KEY ([IdVendedor]) REFERENCES [dbo].[Usuario]([IdUsuario]),
 CONSTRAINT [CK_FacturaBuffet_Importe] CHECK ([ImporteTotal] > 0),
 CONSTRAINT [CK_FacturaBuffet_Estado] CHECK ([Estado] IN (N'Pagada', N'Anulada')),
 CONSTRAINT [CK_FacturaBuffet_Numero] CHECK (LEN(LTRIM(RTRIM([NumeroFactura]))) > 0),
 CONSTRAINT [CK_FacturaBuffet_Banco] CHECK (LEN(LTRIM(RTRIM([Banco]))) > 0),
 CONSTRAINT [CK_FacturaBuffet_Tarjeta] CHECK (LEN([Ultimos4Tarjeta])=4 AND [Ultimos4Tarjeta] NOT LIKE '%[^0-9]%'),
 CONSTRAINT [CK_FacturaBuffet_Autorizacion] CHECK (LEN(LTRIM(RTRIM([CodigoAutorizacion]))) > 0)
);
CREATE TABLE [dbo].[DetalleFactura] (
 [IdDetalleFactura] int IDENTITY(1,1) NOT NULL,
 [IdFactura] int NOT NULL,
 [CodigoProducto] int NOT NULL,
 [Cantidad] int NOT NULL,
 [PrecioUnitario] decimal(12,2) NOT NULL,
 [Subtotal] AS CONVERT(decimal(18,2), CONVERT(decimal(18,2), [Cantidad]) * [PrecioUnitario]) PERSISTED,
 [DVH] varchar(64) NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY ([IdDetalleFactura]),
 CONSTRAINT [UQ_DetalleFactura_FacturaProducto] UNIQUE ([IdFactura],[CodigoProducto]),
 CONSTRAINT [FK_DetalleFactura_FacturaBuffet] FOREIGN KEY ([IdFactura]) REFERENCES [dbo].[FacturaBuffet]([IdFactura]),
 CONSTRAINT [FK_DetalleFactura_Producto] FOREIGN KEY ([CodigoProducto]) REFERENCES [dbo].[Producto]([CodigoProducto]),
 CONSTRAINT [CK_DetalleFactura_Cantidad] CHECK ([Cantidad] > 0),
 CONSTRAINT [CK_DetalleFactura_Precio] CHECK ([PrecioUnitario] > 0)
);
CREATE INDEX [IX_FacturaBuffet_DNI_Fecha] ON [dbo].[FacturaBuffet]([DNI],[FechaHoraEmision]);
CREATE INDEX [IX_FacturaBuffet_Vendedor_Fecha] ON [dbo].[FacturaBuffet]([IdVendedor],[FechaHoraEmision]);
CREATE INDEX [IX_DetalleFactura_Producto] ON [dbo].[DetalleFactura]([CodigoProducto]);

SET IDENTITY_INSERT [dbo].[BitacoraEvento] ON 

INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (1, NULL, N'recepcionista@padelgest.com', CAST(N'2026-10-04T14:44:51.8551037' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla RespaldoBD: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (2, NULL, N'recepcionista@padelgest.com', CAST(N'2026-10-04T14:44:51.8743094' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Reserva: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (3, NULL, N'recepcionista@padelgest.com', CAST(N'2026-10-04T14:44:51.8750155' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Cliente: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (4, NULL, N'recepcionista@padelgest.com', CAST(N'2026-10-04T14:44:51.8752942' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Factura: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (5, NULL, N'recepcionista@padelgest.com', CAST(N'2026-10-04T14:44:51.8755895' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Pago: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (6, NULL, N'admin1@cinegest.com', CAST(N'2026-10-04T14:45:03.4699321' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla RespaldoBD: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (7, NULL, N'admin1@cinegest.com', CAST(N'2026-10-04T14:45:03.4705100' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Reserva: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (8, NULL, N'admin1@cinegest.com', CAST(N'2026-10-04T14:45:03.4708423' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Cliente: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (9, NULL, N'admin1@cinegest.com', CAST(N'2026-10-04T14:45:03.4711789' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Factura: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (10, NULL, N'admin1@cinegest.com', CAST(N'2026-10-04T14:45:03.4714605' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Pago: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (11, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:08.9183893' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla RespaldoBD: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (12, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:08.9190223' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Reserva: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (13, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:08.9193116' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Cliente: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (14, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:08.9195863' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Factura: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (15, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:08.9201010' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Pago: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (16, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:21.8933583' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla RespaldoBD: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (17, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:21.8937942' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Reserva: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (18, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:21.8941620' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Cliente: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (19, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:21.8946014' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Factura: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (20, NULL, N'admin1@padelgest.com', CAST(N'2026-10-04T14:45:21.8948569' AS DateTime2), N'Integridad', N'Verificacion de integridad', N'Alta', N'Fallido', N'Tabla Pago: se detecto al menos un registro eliminado fuera del sistema.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (21, 4, N'Carlos12344444', CAST(N'2026-10-04T14:48:12.7571361' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (22, 4, N'Carlos12344444', CAST(N'2026-10-04T14:49:50.3264218' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (23, 4, N'Carlos12344444', CAST(N'2026-10-04T14:57:33.3052016' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: Español.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (24, 4, N'Carlos12344444', CAST(N'2026-10-04T14:57:33.3117041' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (25, 4, N'Carlos12344444', CAST(N'2026-10-04T15:00:13.2200707' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (26, 4, N'Carlos12344444', CAST(N'2026-10-04T15:00:42.4379914' AS DateTime2), N'Cliente', N'Registrar cliente', N'Media', N'Exitoso', N'Se registró el cliente DNI 12345678 con IdCliente 1.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (27, 4, N'Carlos12344444', CAST(N'2026-10-04T15:01:17.4456170' AS DateTime2), N'Reserva', N'Registrar reserva', N'Alta', N'Exitoso', N'Reserva RES-20261004-313D60 registrada para el Cliente DNI 12345678. Cancha: Cancha 1. Fecha: 04/10/2026. Horario: 16:00. Paletas: 0. Pelotas: 0. Importe abonado: $ 30.000,00.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (28, 4, N'Carlos12344444', CAST(N'2026-10-04T15:01:29.2503804' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (29, 1, N'admin1', CAST(N'2026-10-12T13:41:07.4762759' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (30, 1, N'admin1', CAST(N'2026-10-12T13:41:20.5909750' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (31, 4, N'Carlos12344444', CAST(N'2026-10-12T13:41:33.9178938' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (32, 4, N'Carlos12344444', CAST(N'2026-10-12T13:42:13.7206651' AS DateTime2), N'Cliente', N'Registrar cliente', N'Media', N'Exitoso', N'Se registró el cliente DNI 11111111.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (33, 4, N'Carlos12344444', CAST(N'2026-10-12T13:42:38.4238923' AS DateTime2), N'Reserva', N'Registrar reserva', N'Alta', N'Exitoso', N'Reserva RES-20261012-306A72 registrada para el Cliente DNI 11111111. Cancha: Cancha 1. Fecha: 12/10/2026. Horario: 14:30. Paletas: 0. Pelotas: 0. Importe abonado: $ 30.000,00.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (34, 4, N'Carlos12344444', CAST(N'2026-10-12T13:43:01.0739390' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (35, 1, N'admin1', CAST(N'2026-10-12T14:53:33.3929537' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (36, 1, N'admin1', CAST(N'2026-10-12T14:53:55.4966648' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (37, 1, N'admin1', CAST(N'2026-10-19T13:59:37.9904487' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (38, 1, N'admin1', CAST(N'2026-10-19T13:59:49.1533414' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (39, 4, N'Carlos12344444', CAST(N'2026-10-19T14:00:06.7409122' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (40, 4, N'Carlos12344444', CAST(N'2026-10-19T14:00:16.3242451' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
SET IDENTITY_INSERT [dbo].[BitacoraEvento] OFF
SET IDENTITY_INSERT [dbo].[Cancha] ON 

INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (1, N'Cancha 1', N'Disponible', N'e6e8bcf57788150e413e016d20f8de662b731665f5b7e1071025cb1919d07ce3')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (2, N'Cancha 2', N'Disponible', N'0aaac307006e492dabf3cfdbe1bc09849eaf4a9947d3753ec0a26bc94a3cc329')
INSERT [dbo].[Cancha] ([IdCancha], [Nombre], [Estado], [DVH]) VALUES (3, N'Cancha 3', N'Disponible', N'e76a1e12447a651bd0aa24debb03ca1a4df00165df4fa548c51fa6f4fcd9d128')
SET IDENTITY_INSERT [dbo].[Cancha] OFF
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'11111111', N'Prueba', N'Prueba', N'1234567890', N'Prueba@gmail.com', N'ec3be973343c5f3d7c22cf3e662ac0b6847054920461a7ba3f3b731f14703035')
INSERT [dbo].[Cliente] ([DNI], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [DVH]) VALUES (N'12345678', N'Leo', N'Rod', N'11223344', N'leo@gmail.com', N'cdcc6763b1c89b568b83c035d6e8b6d99eede4e64213704242e3e85717a187e9')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cancha', N'91129657cbb5230efd409301669fd05b677f7331542c2192547bd4da1b7bd1bd')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Cliente', N'c03015b3d9ef698e79b624cdec9770b67232579d5c09d00cfabf3f9c474d69bb')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Equipamiento', N'1fb47453d7dee2f1e004895302f0abd6dcaa3a456f677af737b2669d1fe365f5')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Factura', N'1c7f1fe929094cfca32ee9dc21322ede6cad2728b8a5839f817bf869499bba6a')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'FacturaBuffet', NULL)
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'DetalleFactura', NULL)
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia', N'fdd294e73b3531a63b8e55aff07cf3c5d17090230a00fa065860d52642a31e97')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Familia_Familia', N'18c42e63ad2b66da1312f3417e15d5d971f8a253fbfb4897fbeb267656f4be79')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Idioma', N'b8206deae9aaca74a25998ec9d26c6b2b41fdf430ccc455979a8aed7a298a234')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple', N'bdd33eb30c868e8dea0a3268656a388942cf038d457db6a596486e066931a614')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'PermisoSimple_Familia', N'e24973fe7c5a5b5c5d929cabb8f8f3f36f6b314a62bb488c51f1d145ac5c9467')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Producto', NULL)
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Reserva', N'428b6771c8a716b8ad77fee5cdd902a57bb4a9077fa37eff1db853d2764f17d4')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'RespaldoBD', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol', N'700ba87ff88d54c32d59eac310a40e63e20df37bb09a5a3f7191354a747a30d3')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_Familia', N'a1b7ac1cedd808c373f78ba8a157a7d1c823d56cd6a447d89dce682ddb323b22')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Rol_PermisoSimple', N'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Tarifa', N'9cc41aa9db8a17e67681d251c2d21eb70e08af85b8f741319d72704670345d13')
INSERT [dbo].[DigitoVerificador] ([NombreTabla], [DVV]) VALUES (N'Usuario', N'161a8edb1f52eabd4bda9294b36ace3cec0c41c79b92b2e34627034f59f780a9')
SET IDENTITY_INSERT [dbo].[Equipamiento] ON 

INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (1, N'Paleta', CAST(8000.00 AS Decimal(12, 2)), 8, 1, N'345646ab25ac506c73da5a5ecdcd9dd10858d9a5d96d33c46ab6ccf715f6a6f1')
INSERT [dbo].[Equipamiento] ([IdEquipamiento], [Tipo], [Importe], [StockDisponible], [Activo], [DVH]) VALUES (2, N'Pelota', CAST(1500.00 AS Decimal(12, 2)), 12, 1, N'27f8a517152ae46590b19ac12c70883c73339a1f60d04dc994ed76d8b754c9c6')
SET IDENTITY_INSERT [dbo].[Equipamiento] OFF
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([IdFactura], [IdCancha], [IdTarifa], [FechaHoraEmision], [FechaReserva], [Horario], [CantidadPaletas], [CantidadPelotas], [ImporteTarifa], [ImporteEquipamiento], [ImporteTotal], [Estado], [DVH], [DNI]) VALUES (1, 1, 1, CAST(N'2026-10-04T15:01:17.4393736' AS DateTime2), CAST(N'2026-10-04' AS Date), CAST(N'16:00:00' AS Time), 0, 0, CAST(30000.00 AS Decimal(12, 2)), CAST(0.00 AS Decimal(12, 2)), CAST(30000.00 AS Decimal(12, 2)), N'Pagada', N'89fd7e3336346cca437c79c873d00f5ace7e0b89f51ab71ec74d0e2da0dee44c', N'12345678')
INSERT [dbo].[Factura] ([IdFactura], [IdCancha], [IdTarifa], [FechaHoraEmision], [FechaReserva], [Horario], [CantidadPaletas], [CantidadPelotas], [ImporteTarifa], [ImporteEquipamiento], [ImporteTotal], [Estado], [DVH], [DNI]) VALUES (2, 1, 1, CAST(N'2026-10-12T13:42:38.4218183' AS DateTime2), CAST(N'2026-10-12' AS Date), CAST(N'14:30:00' AS Time), 0, 0, CAST(30000.00 AS Decimal(12, 2)), CAST(0.00 AS Decimal(12, 2)), CAST(30000.00 AS Decimal(12, 2)), N'Pagada', N'0b7a4ff38ff7b651b65b950519980cbbe8f2c6039ef465e06ce3b2e8277eaf8a', N'11111111')
SET IDENTITY_INSERT [dbo].[Factura] OFF
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
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 2, N'a9e264cb4c953f958e10a18334af28d3229562f48c1ec33c0a5f94df26dcb5d9')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 3, N'29694ce63ca04a055a1f6284f59acd208be2cb74edc443689f231def0f55c18e')
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija], [DVH]) VALUES (8, 4, N'9ccad5595eaedbf07fc1ada91dcd2805c6c544430424ce91c69f709c02a52103')
SET IDENTITY_INSERT [dbo].[Idioma] ON 

INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (1, N'ES', N'Español', N'1f83a4462935948663a6223389e13dab529e0db28bbf75ad071f08d161654e73')
INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre], [DVH]) VALUES (2, N'EN', N'English', N'b98cc3676414c0928ab8a26a2e721b1400a585267b969cd4b35b87a2b6d88b9f')
SET IDENTITY_INSERT [dbo].[Idioma] OFF
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
SET IDENTITY_INSERT [dbo].[PermisoSimple] OFF
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
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (7, 26, N'7f155abfca97e134f158f3c6b4d04d6707ff181ae78c16cde5732d96186d022e')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (7, 27, N'43751bc31fa90e39010c41ca3956b248442525c24376a578e8584b6faabb978d')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (8, 29, N'e3c65737f4c3a978fdab7ffb65ff727596a48ef2233745b1c863acfe4ebe1deb')
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple], [DVH]) VALUES (13, 28, N'90f395f0b6f1de9c74af4eab4a3a4aac2927f091fe479344113ba1c9355a976b')
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([IdReserva], [IdCancha], [IdTarifa], [Fecha], [Horario], [Estado], [DVH], [Codigo], [IdFactura], [CantidadPaletas], [CantidadPelotas], [DNI], [Banco], [Ultimos4Tarjeta], [ImportePago], [FechaHoraPago], [EstadoPago], [CodigoAutorizacion]) VALUES (1, 1, 1, CAST(N'2026-10-04' AS Date), CAST(N'16:00:00' AS Time), N'Reservada', N'8d615750faa723df1ad859773d9ecb6fa5c2b1ec1a9b4bf5958deeba74f2fd40', N'RES-20261004-313D60', 1, 0, 0, N'12345678', N'banco prov', N'5678', CAST(30000.00 AS Decimal(12, 2)), CAST(N'2026-10-04T15:01:16.0544578' AS DateTime2), N'Aprobado', N'RES-4377B783E263')
INSERT [dbo].[Reserva] ([IdReserva], [IdCancha], [IdTarifa], [Fecha], [Horario], [Estado], [DVH], [Codigo], [IdFactura], [CantidadPaletas], [CantidadPelotas], [DNI], [Banco], [Ultimos4Tarjeta], [ImportePago], [FechaHoraPago], [EstadoPago], [CodigoAutorizacion]) VALUES (2, 1, 1, CAST(N'2026-10-12' AS Date), CAST(N'14:30:00' AS Time), N'Reservada', N'0006a5c1f8d4c50735b7f3058b624e3980e3156195bcaddf121fd00f8cbd069e', N'RES-20261012-306A72', 2, 0, 0, N'11111111', N'Banco Galicia', N'1234', CAST(30000.00 AS Decimal(12, 2)), CAST(N'2026-10-12T13:42:37.6062670' AS DateTime2), N'Aprobado', N'RES-30290091F080')
SET IDENTITY_INSERT [dbo].[Reserva] OFF
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (1, N'Administrador', N'e968303d5a0735207eb00fd12bf270e9d83f5e269fd9bab80a26424e3954441f')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (2, N'Vendedor de Buffet', N'8fcd2473f9162ea7f574ab6fda26376b775efd5a2a7188d53648104f8d5ff67b')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (3, N'Recepcionista', N'9d65b912fda8c9906289143693ab2b3999de7c6430c4a6e001e7f57188e24b8c')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (4, N'Dueño', N'08f785674ea0f3e5871d825ef0977cf95f484245afc3195e7b5f82a014031d92')
INSERT [dbo].[Rol] ([IdRol], [Nombre], [DVH]) VALUES (5, N'Encargado de Canchas', N'714816c1e538b87ea88b9540cce7d1f1afaf7eb6e5888ca34bf904630261ba56')
SET IDENTITY_INSERT [dbo].[Rol] OFF
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (1, 1, N'3f22d6d875f4e1635c6b9f02ac6ef85c76fa33ca7a7593ea6bed1ef2c9997414')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (1, 8, N'444abbca8de8dc89ae7445fe27afc743f74ddfd2fae5be74c464b587e10198cc')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (2, 1, N'ab8acbe9b03fadaf050f68d7ca94737fff7eed9c76bc50c5d53f9ee0ff779beb')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (2, 7, N'61bc29ecfce8757bde6fbb33b61ecd759f744bc1c1f53db4ea5f2ae0059838ae')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (3, 1, N'81e96b53ef6b10d23617ec3286caaefa081820565aae60315ccab78733478d71')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (3, 5, N'a6dad5d245abd5061945ce56b487542e3233969b36a260e85a4c09556aab60de')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (3, 6, N'1c0852ab9781b5dacfab79eac19733d9e86f1cb6c359fd1657de29797d547c18')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 1, N'b6d71685bd9ab5b3cd6aa85bc38ee89f8d4c6d8c9d7afb87b0a4ff1b0915027f')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 5, N'cebc03007ff90caa5f14b0d24681820822c75b97db9a3658fd2c06b796794c97')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 6, N'13ab4ed36916168913964f52b4b98215893947f7540ef25ad80ed34593f97b1f')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 7, N'a7c9457857e60130f01a26bed4d4d64249afbbf8f2e041ca4d8d74fd6098d0b7')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (4, 13, N'8c35684bf37f290ed4e35e4eb8e7fd2a5ec035e188191f443d0c11c6c969c9b8')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (5, 1, N'cac33d2c18b2084313c957a5641e259f69b9f0d5afa2cc4b825b585d6534ccdc')
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia], [DVH]) VALUES (5, 13, N'8ec99bdedaf4cc509dabae02553a61d2e49c4969309086f082fcc956568aa859')
SET IDENTITY_INSERT [dbo].[Tarifa] ON 

INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (1, N'Diurna', CAST(30000.00 AS Decimal(12, 2)), CAST(N'08:30:00' AS Time), CAST(N'17:30:00' AS Time), 1, N'01d171f677f4fac8f86c9e8a44e8f8903a9ffaff7adf787d72c19f2bafdb3642')
INSERT [dbo].[Tarifa] ([IdTarifa], [TipoTarifa], [Importe], [HoraDesde], [HoraHasta], [Activo], [DVH]) VALUES (2, N'Nocturna', CAST(45000.00 AS Decimal(12, 2)), CAST(N'17:30:00' AS Time), CAST(N'22:00:00' AS Time), 1, N'dc1b62070c4361943e0b14a1c93dd2f52d3b09fb7532d1a091871ad7a4bcda3a')
SET IDENTITY_INSERT [dbo].[Tarifa] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (1, N'Leonel', N'Rodriguez', N'46631736', N'admin1@padelgest.com', N'admin1', N'5456cc85efead5b7762b131e99389aa4b861199686c6a38f1f2bd4154c61b409', 1, 0, 0, 0, 1, 1, N'1b3f2113a555df030e928e242671cf13e21859f3a7acf1aba2b9ddb31f8c9a87')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (3, N'Seba', N'Mariolani', N'11223344', N'dueño@padelgest.com', N'Seba11223344', N'27ef5a3bc40eb5df6cb01d9d911c4ade3e247fa4d68d1b2ab6db2d2fd9f71d8a', 1, 0, 0, 1, 1, 4, N'2f0d1ed945b023e60d84f14394c2f3b77f376871cac1c8439b0d05803beb583b')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (4, N'Carlos', N'Gonzalez', N'12344444', N'recepcionista@padelgest.com', N'Carlos12344444', N'a55a9ea338452b2b5d9eee49633700eb451bf9b855a7de816d0fbf40b4b993d7', 1, 0, 0, 0, 1, 3, N'7dc44470973d91703ffecb8ef1c349235971d19669d2ff4232c7045588d3b8dd')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (5, N'Jorge', N'Pereyra', N'22224444', N'vendedor@padelgest.com', N'Jorge22224444', N'3f941ccc5c5773b0a3b0e831aabb9ead199e7b9d001e56178d7495d3413e1f44', 1, 0, 0, 1, 1, 2, N'23c84b299a579145a69ace570ddf67ff833a88df0c5525d8424f411bbc127552')
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol], [DVH]) VALUES (6, N'Bruno', N'Roca', N'10101010', N'canchero@padelgest.com', N'Bruno10101010', N'e5dc9e54502940bac6a9b65c1de3129fdc35ff4a4b4cc7f7bc968edec973fcd7', 1, 0, 0, 1, 1, 5, N'd4754c4d1d212358e9b6051c86c8ad3e2b84ee3766c742b80f01e42520d4285a')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Cancha_Nombre]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Cancha] ADD  CONSTRAINT [UQ_Cancha_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Equipamiento_Tipo]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Equipamiento] ADD  CONSTRAINT [UQ_Equipamiento_Tipo] UNIQUE NONCLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Familia_Nombre]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [UQ_Familia_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Idioma_Codigo]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Idioma] ADD  CONSTRAINT [UQ_Idioma_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
SET ANSI_PADDING ON
/****** Object:  Index [UQ_PermisoSimple_Codigo]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[PermisoSimple] ADD  CONSTRAINT [UQ_PermisoSimple_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Rol_Nombre]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [UQ_Rol_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Usuario_DNI]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Usuario_Email]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
/****** Object:  Index [UQ_Usuario_NombreUsuario]    Script Date: 19/10/2026 15:17:06 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[BitacoraEvento] ADD  CONSTRAINT [DF_BitacoraEvento_FechaHora]  DEFAULT (sysdatetime()) FOR [FechaHora]
ALTER TABLE [dbo].[Equipamiento] ADD  CONSTRAINT [DF_Equipamiento_Activo]  DEFAULT ((1)) FOR [Activo]
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_Stock]  DEFAULT ((0)) FOR [StockDisponible]
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_StockMinimo]  DEFAULT ((0)) FOR [StockMinimo]
ALTER TABLE [dbo].[Producto] ADD  CONSTRAINT [DF_Producto_Activo]  DEFAULT ((1)) FOR [Activo]
ALTER TABLE [dbo].[Tarifa] ADD  CONSTRAINT [DF_Tarifa_Activo]  DEFAULT ((1)) FOR [Activo]
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Activo]  DEFAULT ((1)) FOR [Activo]
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Bloqueado]  DEFAULT ((0)) FOR [Bloqueado]
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_IntentosFallidos]  DEFAULT ((0)) FOR [IntentosFallidos]
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_DebeCambiarClave]  DEFAULT ((1)) FOR [DebeCambiarClave]
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_IdIdioma]  DEFAULT ((1)) FOR [IdIdioma]
ALTER TABLE [dbo].[BitacoraEvento]  WITH CHECK ADD  CONSTRAINT [FK_BitacoraEvento_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ALTER TABLE [dbo].[BitacoraEvento] CHECK CONSTRAINT [FK_BitacoraEvento_Usuario]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cancha] FOREIGN KEY([IdCancha])
REFERENCES [dbo].[Cancha] ([IdCancha])
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cancha]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
ON UPDATE CASCADE
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cliente]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Tarifa] FOREIGN KEY([IdTarifa])
REFERENCES [dbo].[Tarifa] ([IdTarifa])
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Tarifa]
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK_FamFam_Hija] FOREIGN KEY([IdFamiliaHija])
REFERENCES [dbo].[Familia] ([IdFamilia])
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK_FamFam_Hija]
ALTER TABLE [dbo].[Familia_Familia]  WITH CHECK ADD  CONSTRAINT [FK_FamFam_Padre] FOREIGN KEY([IdFamiliaPadre])
REFERENCES [dbo].[Familia] ([IdFamilia])
ALTER TABLE [dbo].[Familia_Familia] CHECK CONSTRAINT [FK_FamFam_Padre]
ALTER TABLE [dbo].[PermisoSimple_Familia]  WITH CHECK ADD  CONSTRAINT [FK_PsFam_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([IdFamilia])
ALTER TABLE [dbo].[PermisoSimple_Familia] CHECK CONSTRAINT [FK_PsFam_Familia]
ALTER TABLE [dbo].[PermisoSimple_Familia]  WITH CHECK ADD  CONSTRAINT [FK_PsFam_PermisoSimple] FOREIGN KEY([IdPermisoSimple])
REFERENCES [dbo].[PermisoSimple] ([IdPermisoSimple])
ALTER TABLE [dbo].[PermisoSimple_Familia] CHECK CONSTRAINT [FK_PsFam_PermisoSimple]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cancha] FOREIGN KEY([IdCancha])
REFERENCES [dbo].[Cancha] ([IdCancha])
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cancha]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cliente] FOREIGN KEY([DNI])
REFERENCES [dbo].[Cliente] ([DNI])
ON UPDATE CASCADE
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Cliente]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Factura] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[Factura] ([IdFactura])
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Factura]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Tarifa] FOREIGN KEY([IdTarifa])
REFERENCES [dbo].[Tarifa] ([IdTarifa])
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Tarifa]
ALTER TABLE [dbo].[RespaldoBD]  WITH CHECK ADD  CONSTRAINT [FK_RespaldoBD_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ALTER TABLE [dbo].[RespaldoBD] CHECK CONSTRAINT [FK_RespaldoBD_Usuario]
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_RolFam_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([IdFamilia])
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_RolFam_Familia]
ALTER TABLE [dbo].[Rol_Familia]  WITH CHECK ADD  CONSTRAINT [FK_RolFam_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ALTER TABLE [dbo].[Rol_Familia] CHECK CONSTRAINT [FK_RolFam_Rol]
ALTER TABLE [dbo].[Rol_PermisoSimple]  WITH CHECK ADD  CONSTRAINT [FK_RolPs_PermisoSimple] FOREIGN KEY([IdPermisoSimple])
REFERENCES [dbo].[PermisoSimple] ([IdPermisoSimple])
ALTER TABLE [dbo].[Rol_PermisoSimple] CHECK CONSTRAINT [FK_RolPs_PermisoSimple]
ALTER TABLE [dbo].[Rol_PermisoSimple]  WITH CHECK ADD  CONSTRAINT [FK_RolPs_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ALTER TABLE [dbo].[Rol_PermisoSimple] CHECK CONSTRAINT [FK_RolPs_Rol]
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Idioma] FOREIGN KEY([IdIdioma])
REFERENCES [dbo].[Idioma] ([IdIdioma])
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Idioma]
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [CK_Cliente_DNI] CHECK  ((len([DNI])>=(7) AND len([DNI])<=(8) AND NOT [DNI] like '%[^0-9]%'))
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [CK_Cliente_DNI]
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Importe] CHECK  (([Importe]>=(0)))
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Importe]
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Stock] CHECK  (([StockDisponible]>=(0)))
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Stock]
ALTER TABLE [dbo].[Equipamiento]  WITH CHECK ADD  CONSTRAINT [CK_Equipamiento_Tipo] CHECK  (([Tipo]=N'Pelota' OR [Tipo]=N'Paleta'))
ALTER TABLE [dbo].[Equipamiento] CHECK CONSTRAINT [CK_Equipamiento_Tipo]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_CantidadPaletas] CHECK  (([CantidadPaletas]>=(0)))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_CantidadPaletas]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_CantidadPelotas] CHECK  (([CantidadPelotas]>=(0)))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_CantidadPelotas]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_Estado] CHECK  (([Estado]=N'Cancelada' OR [Estado]=N'Pagada' OR [Estado]=N'Pendiente'))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_Estado]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteEquipamiento] CHECK  (([ImporteEquipamiento]>=(0)))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteEquipamiento]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteTarifa] CHECK  (([ImporteTarifa]>=(0)))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteTarifa]
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [CK_Factura_ImporteTotal] CHECK  (([ImporteTotal]>(0)))
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_ImporteTotal]
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Categoria] CHECK  ((len(ltrim(rtrim([Categoria])))>(0)))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Categoria]
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Nombre] CHECK  ((len(ltrim(rtrim([Nombre])))>(0)))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Nombre]
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Precio] CHECK  (([PrecioUnitario]>(0)))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Precio]
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_Stock] CHECK  (([StockDisponible]>=(0)))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_Stock]
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [CK_Producto_StockMinimo] CHECK  (([StockMinimo]>=(0)))
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [CK_Producto_StockMinimo]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_CantidadPaletas] CHECK  (([CantidadPaletas]>=(0)))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_CantidadPaletas]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_CantidadPelotas] CHECK  (([CantidadPelotas]>=(0)))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_CantidadPelotas]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_EstadoPago] CHECK  (([EstadoPago]=N'Aprobado'))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_EstadoPago]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_ImportePago] CHECK  (([ImportePago]>(0)))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_ImportePago]
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [CK_Reserva_Ultimos4Tarjeta] CHECK  ((len([Ultimos4Tarjeta])=(4) AND NOT [Ultimos4Tarjeta] like '%[^0-9]%'))
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [CK_Reserva_Ultimos4Tarjeta]
ALTER TABLE [dbo].[Tarifa]  WITH CHECK ADD  CONSTRAINT [CK_Tarifa_Horario] CHECK  (([HoraDesde]<[HoraHasta]))
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [CK_Tarifa_Horario]
ALTER TABLE [dbo].[Tarifa]  WITH CHECK ADD  CONSTRAINT [CK_Tarifa_Importe] CHECK  (([Importe]>=(0)))
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [CK_Tarifa_Importe]
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_IntentosFallidos] CHECK  (([IntentosFallidos]>=(0)))
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_IntentosFallidos]
GO
GO
GO
GO
GO
GO
GO
GO
GO
GO
