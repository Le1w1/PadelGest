USE [PadelGestDB]
GO
/****** Object:  Table [dbo].[BitacoraEvento]    Script Date: 19/6/2026 23:42:19 ******/
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
/****** Object:  Table [dbo].[Familia]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[IdFamilia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Familia_Familia]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia_Familia](
	[IdFamiliaPadre] [int] NOT NULL,
	[IdFamiliaHija] [int] NOT NULL,
 CONSTRAINT [PK_Familia_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamiliaPadre] ASC,
	[IdFamiliaHija] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Idioma]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idioma](
	[IdIdioma] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](10) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED 
(
	[IdIdioma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermisoSimple]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermisoSimple](
	[IdPermisoSimple] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_PermisoSimple] PRIMARY KEY CLUSTERED 
(
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermisoSimple_Familia]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermisoSimple_Familia](
	[IdFamilia] [int] NOT NULL,
	[IdPermisoSimple] [int] NOT NULL,
 CONSTRAINT [PK_PermisoSimple_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC,
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_Familia]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_Familia](
	[IdRol] [int] NOT NULL,
	[IdFamilia] [int] NOT NULL,
 CONSTRAINT [PK_Rol_Familia] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol_PermisoSimple]    Script Date: 19/6/2026 23:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol_PermisoSimple](
	[IdRol] [int] NOT NULL,
	[IdPermisoSimple] [int] NOT NULL,
 CONSTRAINT [PK_Rol_PermisoSimple] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdPermisoSimple] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 19/6/2026 23:42:19 ******/
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
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BitacoraEvento] ON 

INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (1, 1, N'admin1', CAST(N'2026-06-02T21:59:06.4109072' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (2, 1, N'admin1', CAST(N'2026-06-02T21:59:35.2640772' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (3, 1, N'admin1', CAST(N'2026-06-02T22:00:00.9927079' AS DateTime2), N'Administrador', N'Desactivar Usuario', N'Alta', N'Exitoso', N'El administrador desactivó el usuario: Seba05')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (4, 1, N'admin1', CAST(N'2026-06-02T22:00:13.4175958' AS DateTime2), N'Administrador', N'Desactivar Usuario', N'Alta', N'Exitoso', N'El administrador desactivó el usuario: carlitos')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (5, 1, N'admin1', CAST(N'2026-06-02T22:00:19.0638240' AS DateTime2), N'Administrador', N'Activar Usuario', N'Alta', N'Exitoso', N'El administrador activó el usuario: Seba05')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (6, 1, N'admin1', CAST(N'2026-06-02T22:00:24.5538346' AS DateTime2), N'Administrador', N'Activar Usuario', N'Alta', N'Exitoso', N'El administrador activó el usuario: carlitos')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (8, 3, N'Seba05', CAST(N'2026-06-02T22:02:17.9719575' AS DateTime2), N'Usuario', N'Bloqueo de cuenta', N'Alta', N'Exitoso', N'La cuenta fue bloqueada por superar la cantidad de intentos permitidos durante el Re-Login.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (11, 4, N'carlitos', CAST(N'2026-06-02T22:03:07.4865971' AS DateTime2), N'Administrador', N'Desbloquear Usuario', N'Alta', N'Exitoso', N'El administrador desbloqueó el usuario: Seba05')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (12, 3, N'Seba05', CAST(N'2026-06-02T22:08:55.2313268' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (13, 3, N'Seba05', CAST(N'2026-06-02T22:09:04.4259229' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (14, 3, N'Seba05', CAST(N'2026-06-02T22:09:23.3395066' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (15, 3, N'Seba05', CAST(N'2026-06-02T22:09:23.3414363' AS DateTime2), N'Login', N'Bloqueo de cuenta', N'Alta', N'Exitoso', N'La cuenta fue bloqueada por superar la cantidad de intentos permitidos.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (16, 1, N'admin1', CAST(N'2026-06-02T22:09:44.1223553' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (17, 1, N'admin1', CAST(N'2026-06-02T22:09:48.8007135' AS DateTime2), N'Administrador', N'Desbloquear Usuario', N'Alta', N'Exitoso', N'El administrador desbloqueó el usuario: Seba05')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (18, 1, N'admin1', CAST(N'2026-06-02T22:11:52.5532977' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (19, 1, N'admin1', CAST(N'2026-06-02T22:11:57.9251379' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (20, 1, N'admin1', CAST(N'2026-06-02T22:15:09.7235257' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (21, 1, N'admin1', CAST(N'2026-06-02T22:15:17.7164926' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (22, 1, N'admin1', CAST(N'2026-06-02T22:16:04.5244567' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (23, 1, N'admin1', CAST(N'2026-06-02T22:16:29.5377409' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: carlitos')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (24, 1, N'admin1', CAST(N'2026-06-02T22:17:52.7821817' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: carlitos')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (25, 3, N'Seba05', CAST(N'2026-06-02T22:20:39.9959473' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (26, 3, N'Seba05', CAST(N'2026-06-02T22:20:41.2345884' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (27, 3, N'Seba05', CAST(N'2026-06-02T22:20:42.1521472' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (28, 3, N'Seba05', CAST(N'2026-06-02T22:20:42.1540051' AS DateTime2), N'Login', N'Bloqueo de cuenta', N'Alta', N'Exitoso', N'La cuenta fue bloqueada por superar la cantidad de intentos permitidos.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (29, 1, N'admin1', CAST(N'2026-06-02T22:21:03.9490343' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (30, 1, N'admin1', CAST(N'2026-06-02T22:21:16.6260053' AS DateTime2), N'Administrador', N'Desbloquear Usuario', N'Alta', N'Exitoso', N'El administrador desbloqueó el usuario: Seba05')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (31, 1, N'admin1', CAST(N'2026-06-02T22:22:24.1837101' AS DateTime2), N'Administrador', N'Crear Usuario', N'Alta', N'Exitoso', N'El administrador creó el usuario: Jorge22224444')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (32, NULL, N'pereyra@uai.edu', CAST(N'2026-06-02T22:33:21.3501403' AS DateTime2), N'Login', N'Inicio de sesión', N'Media', N'Fallido', N'Intento de inicio de sesión con un email inexistente.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (33, 5, N'Jorge22224444', CAST(N'2026-06-02T22:33:50.8402439' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (37, 1, N'admin1', CAST(N'2026-06-02T22:36:32.2242257' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (39, 1, N'admin1', CAST(N'2026-06-02T23:03:48.5659652' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (40, 1, N'admin1', CAST(N'2026-06-02T23:07:50.5958256' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (41, 1, N'admin1', CAST(N'2026-06-02T23:10:04.7593042' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (42, 1, N'admin1', CAST(N'2026-06-02T23:10:11.7935084' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (43, 1, N'admin1', CAST(N'2026-06-04T12:46:47.2913378' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (44, 1, N'admin1', CAST(N'2026-06-04T12:57:30.3646321' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (45, 1, N'admin1', CAST(N'2026-06-04T13:01:58.5233772' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (46, 1, N'admin1', CAST(N'2026-06-04T13:02:07.8495080' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (47, 1, N'admin1', CAST(N'2026-06-04T13:09:03.8211079' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (48, 1, N'admin1', CAST(N'2026-06-04T13:35:59.7985431' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (49, 1, N'admin1', CAST(N'2026-06-09T17:45:01.9724718' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (50, 1, N'admin1', CAST(N'2026-06-09T17:45:51.9691320' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (51, 1, N'admin1', CAST(N'2026-06-09T17:49:53.2972618' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (52, 1, N'admin1', CAST(N'2026-06-09T18:05:29.2058442' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (53, 1, N'admin1', CAST(N'2026-06-09T18:05:57.8147304' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (54, 1, N'admin1', CAST(N'2026-06-09T18:11:54.7823073' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (55, 1, N'admin1', CAST(N'2026-06-09T18:13:22.0037927' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (56, 1, N'admin1', CAST(N'2026-06-09T18:17:27.7764855' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (57, 1, N'admin1', CAST(N'2026-06-09T18:17:40.9197361' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (58, 1, N'admin1', CAST(N'2026-06-09T18:24:13.4425062' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (59, 1, N'admin1', CAST(N'2026-06-09T18:31:40.5603739' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (60, 1, N'admin1', CAST(N'2026-06-09T18:31:49.6436459' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (61, 1, N'admin1', CAST(N'2026-06-09T18:32:52.2645744' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: Seba11223344')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (62, 1, N'admin1', CAST(N'2026-06-09T18:33:07.8560099' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: Carlos12344444')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (63, 1, N'admin1', CAST(N'2026-06-09T18:33:41.5545304' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (64, 4, N'Carlos12344444', CAST(N'2026-06-09T18:34:38.0455045' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (65, 4, N'Carlos12344444', CAST(N'2026-06-09T18:35:07.4795435' AS DateTime2), N'Usuario', N'Cambio de clave', N'Alta', N'Exitoso', N'El usuario modificó su contraseña correctamente.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (66, 4, N'Carlos12344444', CAST(N'2026-06-09T18:35:20.1610207' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (67, 4, N'Carlos12344444', CAST(N'2026-06-09T18:35:40.5433892' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (68, 4, N'Carlos12344444', CAST(N'2026-06-09T18:35:46.7881084' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (69, 4, N'Carlos12344444', CAST(N'2026-06-09T18:36:27.1981080' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (70, 4, N'Carlos12344444', CAST(N'2026-06-09T18:36:53.3512770' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (71, 4, N'Carlos12344444', CAST(N'2026-06-09T18:36:53.3533375' AS DateTime2), N'Login', N'Bloqueo de cuenta', N'Alta', N'Exitoso', N'La cuenta fue bloqueada por superar la cantidad de intentos permitidos.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (72, 1, N'admin1', CAST(N'2026-06-09T18:37:09.1812685' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (73, 1, N'admin1', CAST(N'2026-06-09T18:37:19.2630355' AS DateTime2), N'Administrador', N'Desbloquear Usuario', N'Alta', N'Exitoso', N'El administrador desbloqueó el usuario: Carlos12344444')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (74, 1, N'admin1', CAST(N'2026-06-09T18:39:34.7301808' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (75, 4, N'Carlos12344444', CAST(N'2026-06-09T18:39:48.3359105' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (76, 1, N'admin1', CAST(N'2026-06-09T20:43:58.0586582' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (77, 1, N'admin1', CAST(N'2026-06-09T20:49:52.8465997' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (78, 1, N'admin1', CAST(N'2026-06-09T20:50:14.3121829' AS DateTime2), N'Administrador', N'Desactivar Usuario', N'Alta', N'Exitoso', N'El administrador desactivó el usuario: Carlos12344444')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (79, 1, N'admin1', CAST(N'2026-06-09T20:50:18.8581673' AS DateTime2), N'Administrador', N'Activar Usuario', N'Alta', N'Exitoso', N'El administrador activó el usuario: Carlos12344444')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (80, 1, N'admin1', CAST(N'2026-06-09T21:00:19.1433655' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (81, 1, N'admin1', CAST(N'2026-06-09T21:00:26.8322256' AS DateTime2), N'Administrador', N'Imprimir PDF', N'Media', N'Exitoso', N'El usuario imprimió/exportó a PDF la auditoría de eventos. Eventos exportados: 32')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (82, 1, N'admin1', CAST(N'2026-06-18T13:08:57.0041326' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (83, 1, N'admin1', CAST(N'2026-06-18T13:09:03.7567432' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (84, 1, N'admin1', CAST(N'2026-06-18T13:09:26.9741105' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (85, 1, N'admin1', CAST(N'2026-06-18T13:18:04.6611238' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (86, 1, N'admin1', CAST(N'2026-06-18T13:18:13.7129906' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (87, 1, N'admin1', CAST(N'2026-06-18T13:20:16.3906318' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: English.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (88, 1, N'admin1', CAST(N'2026-06-18T13:20:16.3920645' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (89, 1, N'admin1', CAST(N'2026-06-18T13:20:49.8226006' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (90, 1, N'admin1', CAST(N'2026-06-18T13:20:53.8869859' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (91, 1, N'admin1', CAST(N'2026-06-18T13:21:03.4003049' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (92, 1, N'admin1', CAST(N'2026-06-18T13:21:13.6174822' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (93, 1, N'admin1', CAST(N'2026-06-18T13:21:20.6373635' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (94, 1, N'admin1', CAST(N'2026-06-18T13:21:39.1629567' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (95, 1, N'admin1', CAST(N'2026-06-18T13:24:05.4164264' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (96, 1, N'admin1', CAST(N'2026-06-18T13:24:22.3123726' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (97, 1, N'admin1', CAST(N'2026-06-18T13:25:27.5431563' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: Español.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (98, 1, N'admin1', CAST(N'2026-06-18T13:25:27.5444128' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (99, 1, N'admin1', CAST(N'2026-06-18T13:27:12.1012824' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (100, 1, N'admin1', CAST(N'2026-06-18T13:27:24.7927274' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (101, 1, N'admin1', CAST(N'2026-06-18T13:27:33.7084924' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (102, 1, N'admin1', CAST(N'2026-06-18T13:27:50.1421881' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (103, 1, N'admin1', CAST(N'2026-06-18T13:28:03.1870537' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (104, 1, N'admin1', CAST(N'2026-06-18T13:28:12.4919375' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (105, 1, N'admin1', CAST(N'2026-06-18T13:31:20.8344509' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (106, 1, N'admin1', CAST(N'2026-06-18T13:31:24.5635975' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
GO
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (107, 1, N'admin1', CAST(N'2026-06-18T13:41:45.3621600' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (108, 1, N'admin1', CAST(N'2026-06-18T13:41:49.2994781' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (109, 1, N'admin1', CAST(N'2026-06-18T13:42:05.4919782' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (110, 1, N'admin1', CAST(N'2026-06-18T13:52:53.8995284' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (111, 1, N'admin1', CAST(N'2026-06-18T13:53:15.6134433' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (112, 1, N'admin1', CAST(N'2026-06-18T13:54:26.9003005' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (113, 1, N'admin1', CAST(N'2026-06-18T13:54:36.8324528' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (114, 1, N'admin1', CAST(N'2026-06-18T13:57:15.3201070' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (115, 1, N'admin1', CAST(N'2026-06-18T13:57:18.8623937' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (116, 1, N'admin1', CAST(N'2026-06-18T13:57:27.7467770' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (117, 1, N'admin1', CAST(N'2026-06-18T13:57:38.3900156' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (118, 1, N'admin1', CAST(N'2026-06-18T13:57:56.8384815' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (119, 1, N'admin1', CAST(N'2026-06-18T13:58:03.6608299' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (120, 1, N'admin1', CAST(N'2026-06-18T14:04:06.1349335' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (121, 1, N'admin1', CAST(N'2026-06-18T14:04:11.3773602' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (122, 1, N'admin1', CAST(N'2026-06-18T14:04:34.5738370' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: English.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (123, 1, N'admin1', CAST(N'2026-06-18T14:04:34.5755606' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (124, 1, N'admin1', CAST(N'2026-06-18T14:07:31.5077495' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (125, 1, N'admin1', CAST(N'2026-06-18T14:07:38.1858940' AS DateTime2), N'Administrador', N'Imprimir PDF', N'Media', N'Exitoso', N'El usuario imprimió/exportó a PDF la auditoría de eventos. Eventos exportados: 43')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (126, 1, N'admin1', CAST(N'2026-06-18T14:09:16.0272271' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (127, 1, N'admin1', CAST(N'2026-06-18T14:09:18.7559810' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: Español.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (128, 1, N'admin1', CAST(N'2026-06-18T14:09:18.7565420' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (129, 1, N'admin1', CAST(N'2026-06-18T14:09:26.7573568' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (130, 1, N'admin1', CAST(N'2026-06-18T14:09:33.8790048' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (131, 1, N'admin1', CAST(N'2026-06-18T14:09:41.1624176' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: English.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (132, 1, N'admin1', CAST(N'2026-06-18T14:09:41.1629364' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (133, 1, N'admin1', CAST(N'2026-06-18T14:09:52.6903774' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (134, 1, N'admin1', CAST(N'2026-06-18T14:09:56.6406416' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (135, 1, N'admin1', CAST(N'2026-06-18T14:29:07.2524703' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (136, 1, N'admin1', CAST(N'2026-06-18T14:29:20.0989056' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (137, 1, N'admin1', CAST(N'2026-06-18T14:29:24.5577611' AS DateTime2), N'Idioma', N'Persistir idioma', N'Baja', N'Exitoso', N'Se persistió el idioma del usuario al cerrar sesión: Español.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (138, 1, N'admin1', CAST(N'2026-06-18T14:29:24.5582294' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (139, 1, N'admin1', CAST(N'2026-06-18T14:32:08.5615682' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (140, 1, N'admin1', CAST(N'2026-06-18T14:32:21.5406824' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (141, 1, N'admin1', CAST(N'2026-06-18T22:38:20.0852699' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (142, 1, N'admin1', CAST(N'2026-06-18T22:42:49.4066442' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (143, 1, N'admin1', CAST(N'2026-06-18T22:43:41.3721464' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (144, 1, N'admin1', CAST(N'2026-06-18T22:44:13.2305472' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (145, 1, N'admin1', CAST(N'2026-06-18T22:44:19.4665305' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (146, 1, N'admin1', CAST(N'2026-06-18T22:47:52.7685760' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (147, 1, N'admin1', CAST(N'2026-06-18T22:47:58.0266576' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (148, 1, N'admin1', CAST(N'2026-06-18T22:52:53.2055073' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (149, 1, N'admin1', CAST(N'2026-06-18T22:55:16.1134794' AS DateTime2), N'Administrador', N'Crear Familia', N'Alta', N'Exitoso', N'El administrador creó la Familia: Prueba 1')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (150, 1, N'admin1', CAST(N'2026-06-18T22:55:52.6900534' AS DateTime2), N'Administrador', N'Desactivar Familia', N'Alta', N'Exitoso', N'El administrador desactivó la Familia: Prueba 1')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (151, 1, N'admin1', CAST(N'2026-06-18T22:56:16.6550931' AS DateTime2), N'Administrador', N'Crear Familia', N'Alta', N'Exitoso', N'El administrador creó la Familia: Hola')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (152, 1, N'admin1', CAST(N'2026-06-18T22:56:24.4190694' AS DateTime2), N'Administrador', N'Desactivar Familia', N'Alta', N'Exitoso', N'El administrador desactivó la Familia: Hola')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (153, 1, N'admin1', CAST(N'2026-06-18T23:05:09.8311418' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (154, 1, N'admin1', CAST(N'2026-06-19T19:24:20.5542739' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (155, 1, N'admin1', CAST(N'2026-06-19T19:24:55.1011106' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (156, 1, N'admin1', CAST(N'2026-06-19T19:25:09.2856227' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (157, 1, N'admin1', CAST(N'2026-06-19T19:25:13.2280783' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (158, 1, N'admin1', CAST(N'2026-06-19T19:26:53.7556921' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (159, 1, N'admin1', CAST(N'2026-06-19T19:27:09.4104054' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (160, 1, N'admin1', CAST(N'2026-06-19T19:35:07.3358014' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (161, 1, N'admin1', CAST(N'2026-06-19T20:14:13.7306895' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (162, 1, N'admin1', CAST(N'2026-06-19T20:14:51.0025519' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: Jorge22224444 (Rol: Empleado de Boletería)')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (163, 1, N'admin1', CAST(N'2026-06-19T20:14:59.6037409' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: Carlos12344444 (Rol: Empleado de Cartelera)')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (164, 1, N'admin1', CAST(N'2026-06-19T20:15:10.9111467' AS DateTime2), N'Administrador', N'Modificar Usuario', N'Alta', N'Exitoso', N'El administrador modificó el usuario: Seba11223344 (Rol: Gerente)')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (165, 1, N'admin1', CAST(N'2026-06-19T20:16:23.0594113' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (166, 3, N'Seba11223344', CAST(N'2026-06-19T20:16:45.3438388' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (167, 3, N'Seba11223344', CAST(N'2026-06-19T20:16:55.2373225' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (168, 4, N'Carlos12344444', CAST(N'2026-06-19T20:17:07.8427039' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (169, 4, N'Carlos12344444', CAST(N'2026-06-19T20:17:34.6392808' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (170, 4, N'Carlos12344444', CAST(N'2026-06-19T20:17:54.7357174' AS DateTime2), N'Login', N'Inicio de sesión', N'Alta', N'Fallido', N'El usuario ingresó una contraseña incorrecta.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (171, 4, N'Carlos12344444', CAST(N'2026-06-19T20:17:54.7379002' AS DateTime2), N'Login', N'Bloqueo de cuenta', N'Alta', N'Exitoso', N'La cuenta fue bloqueada por superar la cantidad de intentos permitidos.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (172, 1, N'admin1', CAST(N'2026-06-19T20:27:53.5034256' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (173, 1, N'admin1', CAST(N'2026-06-19T20:33:40.4621243' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (174, 4, N'Carlos12344444', CAST(N'2026-06-19T20:33:58.6335816' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (175, 4, N'Carlos12344444', CAST(N'2026-06-19T20:34:53.3885888' AS DateTime2), N'Usuario', N'Re-Login', N'Media', N'Fallido', N'Intento de Re-Login rechazado porque ya existe una sesión activa.')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (176, 4, N'Carlos12344444', CAST(N'2026-06-19T20:35:00.1559097' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (177, 1, N'admin1', CAST(N'2026-06-19T20:35:14.8209892' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (178, 1, N'admin1', CAST(N'2026-06-19T20:36:03.2056745' AS DateTime2), N'Administrador', N'Crear Usuario', N'Alta', N'Exitoso', N'El administrador creó el usuario: Bruno10101010 con el Rol: Administrador')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (179, 1, N'admin1', CAST(N'2026-06-19T20:36:20.2222545' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (180, 6, N'Bruno10101010', CAST(N'2026-06-19T20:36:30.6962137' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (181, 6, N'Bruno10101010', CAST(N'2026-06-19T20:37:38.9786807' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (182, 1, N'admin1', CAST(N'2026-06-19T20:38:06.9783947' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (183, 1, N'admin1', CAST(N'2026-06-19T20:38:59.5952223' AS DateTime2), N'Administrador', N'Crear Familia', N'Alta', N'Exitoso', N'El administrador creó la Familia: prueba')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (184, 1, N'admin1', CAST(N'2026-06-19T20:47:20.7767745' AS DateTime2), N'Administrador', N'Modificar Rol', N'Alta', N'Exitoso', N'El administrador modificó el Rol: Gerente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (185, 1, N'admin1', CAST(N'2026-06-19T20:48:00.6431158' AS DateTime2), N'Administrador', N'Crear Familia', N'Alta', N'Exitoso', N'El administrador creó la Familia: Gerente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (186, 1, N'admin1', CAST(N'2026-06-19T20:48:40.0277490' AS DateTime2), N'Administrador', N'Desactivar Familia', N'Alta', N'Exitoso', N'El administrador desactivó la Familia: Gerente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (187, 1, N'admin1', CAST(N'2026-06-19T20:53:19.3137510' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (188, 1, N'admin1', CAST(N'2026-06-19T20:55:23.9213735' AS DateTime2), N'Administrador', N'Desactivar Familia', N'Alta', N'Exitoso', N'El administrador desactivó la Familia: prueba')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (189, 1, N'admin1', CAST(N'2026-06-19T20:55:38.1627067' AS DateTime2), N'Usuario', N'Cierre de sesión', N'Baja', N'Exitoso', N'El usuario cerró sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (190, 1, N'admin1', CAST(N'2026-06-19T22:41:15.0378510' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (191, 1, N'admin1', CAST(N'2026-06-19T22:41:24.0467453' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (192, 1, N'admin1', CAST(N'2026-06-19T22:41:41.8928743' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (193, 1, N'admin1', CAST(N'2026-06-19T22:42:52.0109550' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (194, 1, N'admin1', CAST(N'2026-06-19T22:42:56.8092432' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (195, 1, N'admin1', CAST(N'2026-06-19T22:43:02.5003493' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (196, 1, N'admin1', CAST(N'2026-06-19T22:43:08.6626088' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a English (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (197, 1, N'admin1', CAST(N'2026-06-19T22:43:14.1343157' AS DateTime2), N'Idioma', N'Cambio de idioma', N'Baja', N'Exitoso', N'El usuario cambió el idioma a Español (en memoria, se persiste al cerrar sesión).')
INSERT [dbo].[BitacoraEvento] ([IdEvento], [IdUsuario], [Usuario], [FechaHora], [Modulo], [Accion], [Criticidad], [Resultado], [Descripcion]) VALUES (198, 1, N'admin1', CAST(N'2026-06-19T23:15:23.7247344' AS DateTime2), N'Usuario', N'Inicio de sesión', N'Alta', N'Exitoso', N'El usuario inició sesión correctamente')
SET IDENTITY_INSERT [dbo].[BitacoraEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[Familia] ON 

INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (1, N'Sesión', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (2, N'Gestión de Usuarios', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (3, N'Bitácora', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (4, N'RBAC', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (5, N'Boletería', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (6, N'Cartelera', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (7, N'Gerencia', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (8, N'Administración Total', 1)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (9, N'Prueba 1', 0)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (10, N'Hola', 0)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (11, N'prueba', 0)
INSERT [dbo].[Familia] ([IdFamilia], [Nombre], [Activo]) VALUES (12, N'Gerente', 0)
SET IDENTITY_INSERT [dbo].[Familia] OFF
GO
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija]) VALUES (8, 2)
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija]) VALUES (8, 3)
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija]) VALUES (8, 4)
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija]) VALUES (9, 1)
INSERT [dbo].[Familia_Familia] ([IdFamiliaPadre], [IdFamiliaHija]) VALUES (9, 5)
GO
SET IDENTITY_INSERT [dbo].[Idioma] ON 

INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre]) VALUES (1, N'ES', N'Español')
INSERT [dbo].[Idioma] ([IdIdioma], [Codigo], [Nombre]) VALUES (2, N'EN', N'English')
SET IDENTITY_INSERT [dbo].[Idioma] OFF
GO
SET IDENTITY_INSERT [dbo].[PermisoSimple] ON 

INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (1, N'USR_LISTAR', N'Listar Usuarios')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (2, N'USR_CREAR', N'Crear Usuario')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (3, N'USR_MODIFICAR', N'Modificar Usuario')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (4, N'USR_ACTIVAR_DESACTIVAR', N'Activar / Desactivar Usuario')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (5, N'USR_DESBLOQUEAR', N'Desbloquear Usuario')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (6, N'USR_ASIGNAR_ROL', N'Asignar Rol a Usuario')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (7, N'BIT_AUDITAR', N'Auditar Bitácora de Eventos')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (8, N'BIT_IMPRIMIR_PDF', N'Imprimir Bitácora a PDF')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (9, N'ROL_LISTAR', N'Listar Roles')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (10, N'ROL_GESTIONAR', N'Gestionar Roles (crear/modificar/eliminar)')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (11, N'FAM_LISTAR', N'Listar Familias')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (12, N'FAM_GESTIONAR', N'Gestionar Familias (crear/modificar/eliminar)')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (13, N'PS_LISTAR', N'Listar Permisos Simples')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (14, N'SES_RELOGIN', N'Re-Login en sesión activa')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (15, N'SES_CAMBIAR_CLAVE', N'Cambiar Clave propia')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (16, N'SES_CAMBIAR_IDIOMA', N'Cambiar Idioma')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (17, N'SES_CERRAR', N'Cerrar Sesión')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (18, N'BOL_VENDER', N'Vender Entrada')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (19, N'BOL_DEVOLVER', N'Devolver / Anular Venta')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (20, N'BOL_CONSULTAR', N'Consultar Funciones Disponibles')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (21, N'CART_VER', N'Ver Cartelera')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (22, N'CART_CREAR_FUNCION', N'Crear Función')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (23, N'CART_MODIFICAR_FUNCION', N'Modificar Función')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (24, N'CART_ELIMINAR_FUNCION', N'Eliminar Función')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (25, N'CART_GESTIONAR_PELICULAS', N'Gestionar Películas')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (26, N'GER_REPORTES', N'Ver Reportes Gerenciales')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (27, N'GER_DASHBOARD', N'Ver Dashboard Gerencial')
INSERT [dbo].[PermisoSimple] ([IdPermisoSimple], [Codigo], [Nombre]) VALUES (28, N'GER_EXPORTAR_DATOS', N'Exportar Datos a Excel/PDF')
SET IDENTITY_INSERT [dbo].[PermisoSimple] OFF
GO
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (1, 14)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (1, 15)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (1, 16)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (1, 17)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 1)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 2)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 3)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 4)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 5)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (2, 6)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (3, 7)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (3, 8)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (4, 9)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (4, 10)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (4, 11)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (4, 12)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (4, 13)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (5, 18)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (5, 19)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (5, 20)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (6, 21)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (6, 22)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (6, 23)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (6, 24)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (6, 25)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (7, 26)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (7, 27)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (7, 28)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (10, 21)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (11, 7)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (11, 8)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (11, 18)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (11, 19)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (11, 20)
INSERT [dbo].[PermisoSimple_Familia] ([IdFamilia], [IdPermisoSimple]) VALUES (12, 11)
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Nombre], [Activo]) VALUES (1, N'Administrador', 1)
INSERT [dbo].[Rol] ([IdRol], [Nombre], [Activo]) VALUES (2, N'Empleado de Boletería', 1)
INSERT [dbo].[Rol] ([IdRol], [Nombre], [Activo]) VALUES (3, N'Empleado de Cartelera', 1)
INSERT [dbo].[Rol] ([IdRol], [Nombre], [Activo]) VALUES (4, N'Gerente', 1)
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (1, 1)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (1, 5)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (1, 6)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (1, 7)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (1, 8)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (2, 1)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (2, 5)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (3, 1)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (3, 6)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (4, 1)
INSERT [dbo].[Rol_Familia] ([IdRol], [IdFamilia]) VALUES (4, 7)
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol]) VALUES (1, N'Leonel', N'Rodriguez', N'46631736', N'admin1@cinegest.com', N'admin1', N'5456cc85efead5b7762b131e99389aa4b861199686c6a38f1f2bd4154c61b409', 1, 0, 0, 0, 1, 1)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol]) VALUES (3, N'Seba', N'Riccio', N'11223344', N'Riccio@gmail.com', N'Seba11223344', N'27ef5a3bc40eb5df6cb01d9d911c4ade3e247fa4d68d1b2ab6db2d2fd9f71d8a', 1, 0, 0, 1, 1, 4)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol]) VALUES (4, N'Carlos', N'Gonzalez', N'12344444', N'carlitos@gmail.com', N'Carlos12344444', N'a55a9ea338452b2b5d9eee49633700eb451bf9b855a7de816d0fbf40b4b993d7', 1, 0, 0, 0, 1, 3)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol]) VALUES (5, N'Jorge', N'Pereyra', N'22224444', N'Pereyra@uai.edu.ar', N'Jorge22224444', N'3f941ccc5c5773b0a3b0e831aabb9ead199e7b9d001e56178d7495d3413e1f44', 1, 0, 0, 1, 1, 2)
INSERT [dbo].[Usuario] ([IdUsuario], [Nombre], [Apellido], [DNI], [Email], [NombreUsuario], [PasswordHash], [Activo], [Bloqueado], [IntentosFallidos], [DebeCambiarClave], [IdIdioma], [IdRol]) VALUES (6, N'Bruno', N'Roca', N'10101010', N'Roca@gmail.com', N'Bruno10101010', N'e5dc9e54502940bac6a9b65c1de3129fdc35ff4a4b4cc7f7bc968edec973fcd7', 1, 0, 0, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Familia_Nombre]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [UQ_Familia_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Idioma_Codigo]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Idioma] ADD  CONSTRAINT [UQ_Idioma_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_PermisoSimple_Codigo]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[PermisoSimple] ADD  CONSTRAINT [UQ_PermisoSimple_Codigo] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Rol_Nombre]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [UQ_Rol_Nombre] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_DNI]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_DNI] UNIQUE NONCLUSTERED 
(
	[DNI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_Email]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario_NombreUsuario]    Script Date: 19/6/2026 23:42:19 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BitacoraEvento] ADD  CONSTRAINT [DF_BitacoraEvento_FechaHora]  DEFAULT (sysdatetime()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [DF_Familia_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Rol] ADD  CONSTRAINT [DF_Rol_Activo]  DEFAULT ((1)) FOR [Activo]
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
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [CK_Usuario_IntentosFallidos] CHECK  (([IntentosFallidos]>=(0)))
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [CK_Usuario_IntentosFallidos]
GO
