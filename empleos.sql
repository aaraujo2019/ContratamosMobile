USE [decasa_empleos]
GO
/****** Object:  User [decasa_admin]    Script Date: 7/07/2019 6:40:52 p. m. ******/
CREATE USER [decasa_admin] FOR LOGIN [decasa_admin] WITH DEFAULT_SCHEMA=[decasa_admin]
GO
/****** Object:  User [decasa_admin_avaluos]    Script Date: 7/07/2019 6:40:52 p. m. ******/
CREATE USER [decasa_admin_avaluos] FOR LOGIN [decasa_admin_avaluos] WITH DEFAULT_SCHEMA=[decasa_admin_avaluos]
GO
/****** Object:  User [decasa_empleos]    Script Date: 7/07/2019 6:40:52 p. m. ******/
CREATE USER [decasa_empleos] FOR LOGIN [decasa_empleos] WITH DEFAULT_SCHEMA=[decasa_empleos]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [decasa_admin]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [decasa_admin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [decasa_admin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [decasa_admin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [decasa_admin_avaluos]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [decasa_admin_avaluos]
GO
ALTER ROLE [db_datareader] ADD MEMBER [decasa_admin_avaluos]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [decasa_admin_avaluos]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [decasa_empleos]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [decasa_empleos]
GO
ALTER ROLE [db_datareader] ADD MEMBER [decasa_empleos]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [decasa_empleos]
GO
/****** Object:  Schema [decasa_admin]    Script Date: 7/07/2019 6:40:53 p. m. ******/
CREATE SCHEMA [decasa_admin]
GO
/****** Object:  Schema [decasa_admin_avaluos]    Script Date: 7/07/2019 6:40:53 p. m. ******/
CREATE SCHEMA [decasa_admin_avaluos]
GO
/****** Object:  Schema [decasa_empleos]    Script Date: 7/07/2019 6:40:54 p. m. ******/
CREATE SCHEMA [decasa_empleos]
GO
/****** Object:  Table [decasa_admin].[Aplicaciones]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[Aplicaciones](
	[IdAplicacion] [int] IDENTITY(1,1) NOT NULL,
	[IdOferta] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaAplicacion] [datetime] NOT NULL,
	[IdDispositivo] [varchar](50) NULL,
 CONSTRAINT [PK_Aplicaciones] PRIMARY KEY CLUSTERED 
(
	[IdAplicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [decasa_admin].[Estados]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[Estados](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [decasa_admin].[Ofertas]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[Ofertas](
	[IdOferta] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[DescripcionOferta] [varchar](max) NOT NULL,
	[Salario] [money] NOT NULL,
	[OfertaDesde] [date] NOT NULL,
	[OfertaHasta] [date] NOT NULL,
	[IdProfesion] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdEstado] [int] NOT NULL,
	[FechaOferta] [datetime] NOT NULL CONSTRAINT [DF_Ofertas_FechaOferta]  DEFAULT (getdate()),
	[IdDispositivo] [varchar](50) NULL,
 CONSTRAINT [PK_Ofertas] PRIMARY KEY CLUSTERED 
(
	[IdOferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [decasa_admin].[Profesiones]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[Profesiones](
	[IdProfesion] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Profesiones] PRIMARY KEY CLUSTERED 
(
	[IdProfesion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [decasa_admin].[TipoUsuario]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[TipoUsuario](
	[IdTipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [decasa_admin].[Usuarios]    Script Date: 7/07/2019 6:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [decasa_admin].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Contraseña] [varchar](200) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[IdTipoUsuario] [int] NOT NULL,
	[ArchivoCv] [varbinary](max) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [decasa_admin].[Estados] ON 

INSERT [decasa_admin].[Estados] ([IdEstado], [Descripcion]) VALUES (1, N'Activo')
INSERT [decasa_admin].[Estados] ([IdEstado], [Descripcion]) VALUES (2, N'Inactivo')
SET IDENTITY_INSERT [decasa_admin].[Estados] OFF
SET IDENTITY_INSERT [decasa_admin].[Ofertas] ON 

INSERT [decasa_admin].[Ofertas] ([IdOferta], [Titulo], [DescripcionOferta], [Salario], [OfertaDesde], [OfertaHasta], [IdProfesion], [IdUsuario], [IdEstado], [FechaOferta], [IdDispositivo]) VALUES (4, N'Auxiliar Contable', N'Auxiliar de contabilidad', 1500000.0000, CAST(N'2019-06-02' AS Date), CAST(N'2019-06-25' AS Date), 4, 2, 1, CAST(N'2019-06-02 17:37:58.843' AS DateTime), N'0001')
INSERT [decasa_admin].[Ofertas] ([IdOferta], [Titulo], [DescripcionOferta], [Salario], [OfertaDesde], [OfertaHasta], [IdProfesion], [IdUsuario], [IdEstado], [FechaOferta], [IdDispositivo]) VALUES (5, N'Practicante Desarrolador', N'Contenido Oferta.', 1500000.0000, CAST(N'2019-07-07' AS Date), CAST(N'2019-08-14' AS Date), 2, 2, 1, CAST(N'2019-07-07 17:16:21.270' AS DateTime), N'9c9faa55')
SET IDENTITY_INSERT [decasa_admin].[Ofertas] OFF
SET IDENTITY_INSERT [decasa_admin].[Profesiones] ON 

INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (1, N'Administración de Empresas')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (2, N'Ingeniería Industrial')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (3, N'Ingeniería de Sistemas')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (4, N'Contaduría')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (5, N'Administración de Negocios')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (6, N'Economía')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (7, N'Administración Financiera')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (8, N'Ingeniería Electrónica')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (9, N'Ingeniería de Telecomunicaciones')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (10, N'Técnico en gestión empresarial')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (11, N'Antropología')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (12, N'Arquitectura')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (13, N'Biología')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (14, N'Bibliotecología')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (15, N'Comunicación Social')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (16, N'Derecho')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (17, N'Diseño Industrial')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (18, N'Enfermería')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (19, N'Filosofía')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (20, N'Ingeniería Civil')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (21, N'Medicina')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (22, N'Medicina Veterinaria')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (23, N'Psicología')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (24, N'Terapia Ocupacional')
INSERT [decasa_admin].[Profesiones] ([IdProfesion], [Descripcion]) VALUES (25, N'Psitioterapia')
SET IDENTITY_INSERT [decasa_admin].[Profesiones] OFF
SET IDENTITY_INSERT [decasa_admin].[TipoUsuario] ON 

INSERT [decasa_admin].[TipoUsuario] ([IdTipoUsuario], [Descripcion]) VALUES (1, N'Administrador')
INSERT [decasa_admin].[TipoUsuario] ([IdTipoUsuario], [Descripcion]) VALUES (2, N'Propietario')
INSERT [decasa_admin].[TipoUsuario] ([IdTipoUsuario], [Descripcion]) VALUES (3, N'Empleador')
INSERT [decasa_admin].[TipoUsuario] ([IdTipoUsuario], [Descripcion]) VALUES (4, N'Usuario')
SET IDENTITY_INSERT [decasa_admin].[TipoUsuario] OFF
SET IDENTITY_INSERT [decasa_admin].[Usuarios] ON 

INSERT [decasa_admin].[Usuarios] ([IdUsuario], [Nombre], [Apellido], [Usuario], [Contraseña], [Email], [IdTipoUsuario], [ArchivoCv]) VALUES (2, N'Alvaro Javier', N'Araujo Arrieta', N'aaraujo', N'ingAlv4r0', N'alvaroaraujo3@gmail.com', 1, 0x00000000000000000000000000)
SET IDENTITY_INSERT [decasa_admin].[Usuarios] OFF
ALTER TABLE [decasa_admin].[Aplicaciones] ADD  CONSTRAINT [DF_Aplicaciones_FechaAplicacion]  DEFAULT (getdate()) FOR [FechaAplicacion]
GO
ALTER TABLE [decasa_admin].[Ofertas]  WITH CHECK ADD  CONSTRAINT [FK_Ofertas_Estados] FOREIGN KEY([IdEstado])
REFERENCES [decasa_admin].[Estados] ([IdEstado])
GO
ALTER TABLE [decasa_admin].[Ofertas] CHECK CONSTRAINT [FK_Ofertas_Estados]
GO
ALTER TABLE [decasa_admin].[Ofertas]  WITH CHECK ADD  CONSTRAINT [FK_Ofertas_Profesiones] FOREIGN KEY([IdProfesion])
REFERENCES [decasa_admin].[Profesiones] ([IdProfesion])
GO
ALTER TABLE [decasa_admin].[Ofertas] CHECK CONSTRAINT [FK_Ofertas_Profesiones]
GO
ALTER TABLE [decasa_admin].[Ofertas]  WITH CHECK ADD  CONSTRAINT [FK_Ofertas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [decasa_admin].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [decasa_admin].[Ofertas] CHECK CONSTRAINT [FK_Ofertas_Usuarios]
GO
ALTER TABLE [decasa_admin].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_TipoUsuarios] FOREIGN KEY([IdTipoUsuario])
REFERENCES [decasa_admin].[TipoUsuario] ([IdTipoUsuario])
GO
ALTER TABLE [decasa_admin].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_TipoUsuarios]
GO
/****** Object:  StoredProcedure [dbo].[ValidarUsuario]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 /*=============================================
 Author:		Alvaro Araujo Arrieta
 Create date: 2/06/2019
 Description:	<Description,,>
 =============================================*/
CREATE PROCEDURE [dbo].[ValidarUsuario] 
	
	@pUsuario varchar(10),
	@pClave varchar(100)
	
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT * 
    FROM [decasa_admin].[Usuarios]
    WHERE Usuario = @pUsuario
		AND	Contraseña = @pClave
    
    
END



GO
/****** Object:  StoredProcedure [decasa_admin].[ActualizarOferta]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[ActualizarOferta]
	@pTitulo varchar(50),
	@pDescripcionOferta varchar(max),
	@pSalario money,
	@pOfertaDesde date,
	@pOfertaHasta date,
	@pIdProfesion int,
	@pIdUsuario int,
	@pIdEstado int,
	@pIdDispositivo varchar(50),
	@pIdOferta INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [decasa_admin].[Ofertas]
	   SET [Titulo] = @pTitulo
		  ,[DescripcionOferta] = @pDescripcionOferta
		  ,[Salario] = @pSalario
		  ,[OfertaDesde] = @pOfertaDesde
		  ,[OfertaHasta] = @pOfertaHasta
		  ,[IdProfesion] = @pIdProfesion
		  ,[IdUsuario] = @pIdUsuario
		  ,[IdEstado] = @pIdEstado
		  ,[IdDispositivo] = @pIdDispositivo
	 WHERE [IdOferta] = @pIdOferta
END

GO
/****** Object:  StoredProcedure [decasa_admin].[ActualizarOfertas]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[ActualizarOfertas]
	@pTitulo varchar(50),
	@pDescripcionOferta varchar(max),
	@pSalario money,
	@pOfertaDesde date,
	@pOfertaHasta date,
	@pIdProfesion int,
	@pIdUsuario int,
	@pIdEstado int,
	@pIdDispositivo varchar(50),
	@pIdOferta INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [decasa_admin].[Ofertas]
	   SET [Titulo] = @pTitulo
		  ,[DescripcionOferta] = @pDescripcionOferta
		  ,[Salario] = @pSalario
		  ,[OfertaDesde] = @pOfertaDesde
		  ,[OfertaHasta] = @pOfertaHasta
		  ,[IdProfesion] = @pIdProfesion
		  ,[IdUsuario] = @pIdUsuario
		  ,[IdEstado] = @pIdEstado
		  ,[IdDispositivo] = @pIdDispositivo
	 WHERE [IdOferta] = @pIdOferta
END

GO
/****** Object:  StoredProcedure [decasa_admin].[ActualizarUsuario]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[ActualizarUsuario] 
	@pNombre varchar(100),
	@pApellido varchar(100),
	@pUsuario varchar(50),
	@pContraseña varchar(200),
	@pEmail varchar(100),
	@pIdTipoUsuario int,
	@pArchivoCv varbinary(max),
	@pIdUsuario int

AS
BEGIN
	
	SET NOCOUNT ON;

    UPDATE [decasa_admin].[Usuarios]
	   SET [Nombre] = @pNombre
		  ,[Apellido] = @pApellido
		  ,[Usuario] = @pUsuario
		  ,[Contraseña] = @pContraseña
		  ,[Email] = @pEmail
		  ,[IdTipoUsuario] = @pIdTipoUsuario
		  ,[ArchivoCv] = @pArchivoCv
	 WHERE IdUsuario = @pIdUsuario
END

GO
/****** Object:  StoredProcedure [decasa_admin].[BuscarOfertasPorId]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[BuscarOfertasPorId]  
	@pIdOferta INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM [decasa_admin].[Ofertas]
	WHERE IdOferta = @pIdOferta

END

GO
/****** Object:  StoredProcedure [decasa_admin].[CargarOfertas]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[CargarOfertas] 
	
AS
BEGIN

	SET NOCOUNT ON;

    select * 
	from [decasa_admin].[Ofertas]
	where IdEstado = 1
END

GO
/****** Object:  StoredProcedure [decasa_admin].[ConsultarTipoUsuarioId]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [decasa_admin].[ConsultarTipoUsuarioId] 
	@pIdTipoUsuario INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT IdTipoUsuario, Descripcion
	 FROM [decasa_admin].[TipoUsuario]
	WHERE IdTipoUsuario = @pIdTipoUsuario
END

GO
/****** Object:  StoredProcedure [decasa_admin].[InsertarOfertas]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[InsertarOfertas] 
	@pTitulo varchar(50),
	@pDescripcionOferta varchar(max),
	@pSalario money,
	@pOfertaDesde date,
	@pOfertaHasta date,
	@pIdProfesion int,
	@pIdUsuario int,
	@pIdEstado int,
	@pIdDispositivo varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [decasa_admin].[Ofertas]
           ([Titulo]
           ,[DescripcionOferta]
           ,[Salario]
           ,[OfertaDesde]
           ,[OfertaHasta]
           ,[IdProfesion]
           ,[IdUsuario]
           ,[IdEstado]
           ,[IdDispositivo])
     VALUES
           (@pTitulo
           ,@pDescripcionOferta
           ,@pSalario
           ,@pOfertaDesde
           ,@pOfertaHasta
           ,@pIdProfesion
           ,@pIdUsuario
           ,@pIdEstado
           ,@pIdDispositivo)


	SELECT @@IDENTITY
END

GO
/****** Object:  StoredProcedure [decasa_admin].[InsertarProfesion]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[InsertarProfesion] 
	@pDescripcion varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [decasa_admin].[Profesiones]
           ([Descripcion])
     VALUES
           (@pDescripcion)
END

GO
/****** Object:  StoredProcedure [decasa_admin].[InsertarUsuario]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[InsertarUsuario]  
	@pNombre varchar(100),
	@pApellido varchar(100),
	@pUsuario varchar(50),
	@pContraseña varchar(200),
	@pEmail varchar(100),
	@pIdTipoUsuario int,
	@pArchivoCv varbinary(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [decasa_admin].[Usuarios]
           ([Nombre]
           ,[Apellido]
           ,[Usuario]
           ,[Contraseña]
           ,[Email]
           ,[IdTipoUsuario]
           ,[ArchivoCv])
     VALUES
           (@pNombre
           ,@pApellido
           ,@pUsuario
           ,@pContraseña
           ,@pEmail
           ,@pIdTipoUsuario
           ,@pArchivoCv)

END

GO
/****** Object:  StoredProcedure [decasa_admin].[InsertarUsuarioAplicaOferta]    Script Date: 7/07/2019 6:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [decasa_admin].[InsertarUsuarioAplicaOferta] 
	@pIdOferta int,
	@pIdUsuario int,
	@pFechaAplicacion date,
	@pIdDispositivo varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   
   INSERT INTO [decasa_admin].[Aplicaciones]
           ([IdOferta]
           ,[IdUsuario]
           ,[FechaAplicacion]
           ,[IdDispositivo])
    VALUES
        (@pIdOferta,
         @pIdUsuario,
         @pFechaAplicacion,
         @pIdDispositivo)

END

GO
