USE [master]
GO

/****** Object:  Database [SpecialOlympics]    Script Date: 31/12/2020 14:35:26 ******/
CREATE DATABASE [SpecialOlympics]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SpecialOlympics', FILENAME = N'C:\Users\jpascual\SpecialOlympics.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SpecialOlympics_log', FILENAME = N'C:\Users\jpascual\SpecialOlympics_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SpecialOlympics].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SpecialOlympics] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SpecialOlympics] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SpecialOlympics] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SpecialOlympics] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SpecialOlympics] SET ARITHABORT OFF 
GO

ALTER DATABASE [SpecialOlympics] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SpecialOlympics] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SpecialOlympics] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SpecialOlympics] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SpecialOlympics] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SpecialOlympics] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SpecialOlympics] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SpecialOlympics] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SpecialOlympics] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SpecialOlympics] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SpecialOlympics] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SpecialOlympics] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SpecialOlympics] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SpecialOlympics] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SpecialOlympics] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SpecialOlympics] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SpecialOlympics] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SpecialOlympics] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SpecialOlympics] SET  MULTI_USER 
GO

ALTER DATABASE [SpecialOlympics] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SpecialOlympics] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SpecialOlympics] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SpecialOlympics] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SpecialOlympics] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SpecialOlympics] SET QUERY_STORE = OFF
GO

USE [SpecialOlympics]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [SpecialOlympics] SET  READ_WRITE 
GO


USE [SpecialOlympics]
GO

/****** Object:  Table [dbo].[Campeonatos]    Script Date: 31/12/2020 14:36:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Campeonatos](
	[IdCampeonato] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](80) NOT NULL,
	[FechaInicio] [date] NULL,
	[FechaFin] [date] NULL,
 CONSTRAINT [PK_Campeonatos] PRIMARY KEY CLUSTERED 
(
	[IdCampeonato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [SpecialOlympics]
GO

/****** Object:  Table [dbo].[Entrenamientos]    Script Date: 31/12/2020 14:38:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Entrenamientos](
	[IdEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](40) NOT NULL,
	[Ubicacion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entrenamientos] PRIMARY KEY CLUSTERED 
(
	[IdEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [SpecialOlympics]
GO

/****** Object:  Table [dbo].[Voluntarios]    Script Date: 31/12/2020 14:38:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Voluntarios](
	[IdVoluntario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](20) NULL,
	[Apellidos] [nvarchar](40) NULL,
	[Email] [nvarchar](80) NULL,
	[IsActivo] [bit] NULL,
	[Foto] [image] NULL,
 CONSTRAINT [PK_Voluntarios] PRIMARY KEY CLUSTERED 
(
	[IdVoluntario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Voluntarios] ADD  CONSTRAINT [DF_Voluntarios_IsActivo]  DEFAULT ((0)) FOR [IsActivo]
GO


USE [SpecialOlympics]
GO

/****** Object:  Table [dbo].[VoluntariosCampeonatos]    Script Date: 31/12/2020 14:38:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VoluntariosCampeonatos](
	[IdVoluntarioCampeonato] [int] IDENTITY(1,1) NOT NULL,
	[IdVoluntario] [int] NOT NULL,
	[IdCampeonato] [int] NOT NULL,
 CONSTRAINT [PK_VoluntariosCampeonatos] PRIMARY KEY CLUSTERED 
(
	[IdVoluntarioCampeonato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VoluntariosCampeonatos]  WITH CHECK ADD  CONSTRAINT [FK_VoluntariosCampeonato_Voluntarios] FOREIGN KEY([IdVoluntario])
REFERENCES [dbo].[Voluntarios] ([IdVoluntario])
GO

ALTER TABLE [dbo].[VoluntariosCampeonatos] CHECK CONSTRAINT [FK_VoluntariosCampeonato_Voluntarios]
GO

ALTER TABLE [dbo].[VoluntariosCampeonatos]  WITH CHECK ADD  CONSTRAINT [FK_VoluntariosCampeonatos_Campeonatos] FOREIGN KEY([IdCampeonato])
REFERENCES [dbo].[Campeonatos] ([IdCampeonato])
GO

ALTER TABLE [dbo].[VoluntariosCampeonatos] CHECK CONSTRAINT [FK_VoluntariosCampeonatos_Campeonatos]
GO


USE [SpecialOlympics]
GO

/****** Object:  Table [dbo].[VoluntariosEntrenamientos]    Script Date: 31/12/2020 14:38:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VoluntariosEntrenamientos](
	[IdVoluntarioEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdVoluntario] [int] NOT NULL,
	[IdEntrenamiento] [int] NOT NULL,
 CONSTRAINT [PK_VoluntariosEntrenamientos] PRIMARY KEY CLUSTERED 
(
	[IdVoluntarioEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VoluntariosEntrenamientos]  WITH CHECK ADD  CONSTRAINT [FK_VoluntariosEntrenamientos_Entrenamientos] FOREIGN KEY([IdEntrenamiento])
REFERENCES [dbo].[Entrenamientos] ([IdEntrenamiento])
GO

ALTER TABLE [dbo].[VoluntariosEntrenamientos] CHECK CONSTRAINT [FK_VoluntariosEntrenamientos_Entrenamientos]
GO

ALTER TABLE [dbo].[VoluntariosEntrenamientos]  WITH CHECK ADD  CONSTRAINT [FK_VoluntariosEntrenamientos_Voluntarios] FOREIGN KEY([IdVoluntario])
REFERENCES [dbo].[Voluntarios] ([IdVoluntario])
GO

ALTER TABLE [dbo].[VoluntariosEntrenamientos] CHECK CONSTRAINT [FK_VoluntariosEntrenamientos_Voluntarios]
GO




