USE [master]
GO
/****** Object:  Database [InfoBretes]    Script Date: 13/8/2024 15:21:29 ******/
CREATE DATABASE [InfoBretes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InfoBretes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\InfoBretes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InfoBretes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\InfoBretes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InfoBretes] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InfoBretes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InfoBretes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InfoBretes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InfoBretes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InfoBretes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InfoBretes] SET ARITHABORT OFF 
GO
ALTER DATABASE [InfoBretes] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [InfoBretes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InfoBretes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InfoBretes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InfoBretes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InfoBretes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InfoBretes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InfoBretes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InfoBretes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InfoBretes] SET  ENABLE_BROKER 
GO
ALTER DATABASE [InfoBretes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InfoBretes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InfoBretes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InfoBretes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InfoBretes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InfoBretes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InfoBretes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InfoBretes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InfoBretes] SET  MULTI_USER 
GO
ALTER DATABASE [InfoBretes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InfoBretes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InfoBretes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InfoBretes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InfoBretes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InfoBretes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InfoBretes] SET QUERY_STORE = ON
GO
ALTER DATABASE [InfoBretes] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InfoBretes]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[idEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[linkCurriculum] [varchar](200) NULL,
	[fotoPerfil] [varchar](100) NULL,
	[telefono] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[idEmpresa] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[nombreEmpresa] [varchar](50) NULL,
	[direccion] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
	[sitioWeb] [varchar](200) NULL,
	[telefono] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Postulaciones]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Postulaciones](
	[idPostulacion] [int] IDENTITY(1,1) NOT NULL,
	[idEmpleado] [int] NULL,
	[idPuesto] [int] NULL,
	[fechaPostulacion] [datetime] NULL,
	[estadoPostulacion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPostulacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PuestosTrabajo]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PuestosTrabajo](
	[idPuesto] [int] IDENTITY(1,1) NOT NULL,
	[idEmpresa] [int] NULL,
	[titulo] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
	[requisitos] [varchar](50) NULL,
	[ubicacion] [varchar](100) NULL,
	[tipoEmpleo] [varchar](50) NULL,
	[salario] [varchar](50) NULL,
	[fechaPublicacion] [datetime] NULL,
	[fechaCierre] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposUsuarios]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposUsuarios](
	[idTipo] [int] IDENTITY(1,1) NOT NULL,
	[TipoUsuario] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[idTipo] [int] NULL,
	[fechaRegistro] [datetime] NULL,
	[direccion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Empresas]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Postulaciones]  WITH CHECK ADD FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[Empleados] ([idEmpleado])
GO
ALTER TABLE [dbo].[Postulaciones]  WITH CHECK ADD FOREIGN KEY([idPuesto])
REFERENCES [dbo].[PuestosTrabajo] ([idPuesto])
GO
ALTER TABLE [dbo].[PuestosTrabajo]  WITH CHECK ADD FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresas] ([idEmpresa])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([idTipo])
REFERENCES [dbo].[TiposUsuarios] ([idTipo])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarOferta]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarOferta]
    @idPuesto INT,
	@idEmpresa INT,
    @titulo VARCHAR(50),
    @descripcion VARCHAR(50),
    @requisitos VARCHAR(50),
	@ubicacion VARCHAR(100),
	@tipoEmpleo VARCHAR(50),
	@salario VARCHAR(50)
AS
BEGIN
	Declare
		@fechaPublicacion datetime,
		@fechaCierre datetime
	SET 
		@fechaPublicacion = CAST(GETDATE() AS date)
	SET	
		@fechaCierre = DATEADD(DAY, 30, @fechaPublicacion);

    UPDATE PuestosTrabajo
    SET 
        idEmpresa = @idEmpresa,
        titulo = @titulo,
		requisitos = @requisitos,
		descripcion = @descripcion,
		ubicacion = @ubicacion,
		tipoEmpleo = @tipoEmpleo,
		salario = @salario,
		fechaPublicacion = @fechaPublicacion,
		fechaCierre = @fechaCierre
    WHERE idPuesto = @idPuesto;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarOfertaPorId]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarOfertaPorId]
    @idPuesto int
AS
BEGIN
    SELECT * FROM PuestosTrabajo
    WHERE idPuesto = @idPuesto
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarUsuario]
	@IdUsuario varchar(50),
	@Nombre varchar(50),
	@Email varchar(50),
	@Password varchar(50),
	@FechaRegistro datetime,
	@Direccion varchar(100),
	@IdTipo int
AS
BEGIN
	UPDATE [dbo].[Usuarios]
	   SET [nombre] = @Nombre
		  ,[email] = @Email
		  ,[password] = @Password
		  ,[idTipo] = @IdTipo
		  ,[fechaRegistro] = @FechaRegistro
		  ,[direccion] = @Direccion
	 WHERE idUsuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[AprobarRechazarSoli]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AprobarRechazarSoli]
    @idPostulacion INT,
    @estadoPostulacion varchar(50)
AS
BEGIN
    UPDATE Postulaciones
    SET estadoPostulacion = @estadoPostulacion
    WHERE idPostulacion = @idPostulacion;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultarEmpresa]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ConsultarEmpresa]
AS
BEGIN
    SELECT * FROM Empresas;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultarOferta]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarOferta]
AS
BEGIN

	SELECT 
        p.idPuesto,
        p.idEmpresa,
        p.titulo,
        p.descripcion,
		p.requisitos,
        p.ubicacion,
		p.tipoEmpleo,
		p.salario,
		p.fechaPublicacion,
		p.fechaCierre,
        e.nombreEmpresa
    FROM 
        PuestosTrabajo p
    INNER JOIN 
        Empresas e ON p.idEmpresa = e.idEmpresa
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarOferta]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarOferta]
    @IdPuesto INT
AS
BEGIN
	DELETE FROM Postulaciones WHERE idPuesto = @IdPuesto;
    DELETE FROM PuestosTrabajo WHERE idPuesto = @IdPuesto;
END;

/****** Object:  StoredProcedure [dbo].[ObtenerSolicitudPorId]    Script Date: 29/7/2024 21:09:42 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[EliminarUsuario]
	@IdUsuario varchar(50)
AS
BEGIN
	DELETE FROM [dbo].[Usuarios]
		  WHERE idUsuario = @IdUsuario
END

GO
/****** Object:  StoredProcedure [dbo].[ObtenerOfertaPorId]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerOfertaPorId]
    @idEmpresa int
AS
BEGIN
    SELECT * FROM PuestosTrabajo
    WHERE idEmpresa = @idEmpresa
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerSolicitudPorId]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerSolicitudPorId]
    @IdPuesto INT
AS
BEGIN
    SELECT 
        p.estadoPostulacion,
        e.linkCurriculum,
        e.telefono,
        e.fotoPerfil,
        u.nombre,
        u.email
    FROM 
        Postulaciones p
    JOIN 
        Empleados e ON p.idEmpleado = e.idEmpleado
    JOIN 
        Usuarios u ON e.idUsuario = u.idUsuario
    WHERE 
        p.idPuesto = @IdPuesto;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuario]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuario]
	@Email varchar(50),
	@Password varchar(50)
AS
BEGIN
	SELECT * FROM dbo.Usuarios WHERE email =  @Email AND password = @Password;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorEmail]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ObtenerUsuarioPorEmail]
	@Email varchar(50)
AS
BEGIN
	SELECT * FROM [dbo].[Usuarios]
		WHERE email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarEmpresa]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarEmpresa]
	@idUsuario INT,
    @NombreEmpresa varchar(50), 
	@Direccion varchar(50),
    @Descripcion varchar(50),
    @SitioWeb varchar(200),
    @Telefono varchar(50)
 
AS
BEGIN
	declare
		@numEmpresa int;
	set
		@numEmpresa = (SELECT count(nombreEmpresa) FROM dbo.Empresas WHERE nombreEmpresa = @NombreEmpresa);
	
 
	IF(@numEmpresa < 1)
	BEGIN
		INSERT INTO [dbo].[Empresas](
					idUsuario,
				   [nombreEmpresa],
				   [direccion],
				   [descripcion],
				   [sitioWeb],
				   [telefono])
			 VALUES
				   (@idUsuario,@NombreEmpresa, @Direccion, @Descripcion, @SitioWeb, @Telefono)
	END
END

GO
/****** Object:  StoredProcedure [dbo].[RegistrarOferta]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarOferta]
    @idEmpresa INT,
    @titulo VARCHAR(50),
    @descripcion VARCHAR(50),
    @requisitos VARCHAR(50),
	@ubicacion VARCHAR(100),
	@tipoEmpleo VARCHAR(50),
	@salario VARCHAR(50)


AS
BEGIN
	Declare
		@fechaPublicacion datetime,
		@fechaCierre datetime
	SET 
		@fechaPublicacion = CAST(GETDATE() AS date)
	SET	
		@fechaCierre = DATEADD(DAY, 30, @fechaPublicacion); 

	INSERT INTO PuestosTrabajo(idEmpresa, titulo, descripcion, requisitos, ubicacion, tipoEmpleo, salario, fechaPublicacion, fechaCierre)
	VALUES (@idEmpresa, @titulo, @descripcion, @requisitos, @ubicacion, @tipoEmpleo, @salario, @fechaPublicacion, @fechaCierre);
END;

GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 13/8/2024 15:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(50),
	@Email varchar(50),
	@Password varchar(50),
	@FechaRegistro datetime,
	@IdTipo int
	
AS
BEGIN
	

	IF((SELECT count(email) FROM dbo.Usuarios WHERE email =  @Email) < 1)
	BEGIN
		INSERT INTO [dbo].[Usuarios]
				   ([nombre]
				   ,[email]
				   ,[fechaRegistro]
				   ,[password]
				   ,[idTipo])
			 VALUES
				   (@Nombre
				   ,@Email
				   ,@FechaRegistro
				   ,@Password
				   ,@IdTipo)
	END
END
GO
USE [master]
GO
ALTER DATABASE [InfoBretes] SET  READ_WRITE 
GO
