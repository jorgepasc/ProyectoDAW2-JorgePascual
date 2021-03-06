USE [master]
GO
/****** Object:  Database [SpecialOlympics]    Script Date: 29/04/2021 15:24:42 ******/
CREATE DATABASE [SpecialOlympics]

USE [SpecialOlympics]
GO
/****** Object:  Table [dbo].[Voluntarios]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voluntarios](
	[IdVoluntario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
	[Apellido1] [varchar](30) NULL,
	[Apellido2] [varchar](30) NULL,
	[DNI] [varchar](12) NULL,
	[Telefono1] [varchar](15) NULL,
	[Email] [varchar](80) NULL,
	[DireccionCompleta] [varchar](200) NULL,
	[Poblacion] [varchar](50) NULL,
	[Provincia] [varchar](50) NULL,
	[CodigoPostal] [varchar](10) NULL,
	[FechaNacimiento] [datetime2](0) NULL,
	[FechaAlta] [datetime2](0) NULL,
	[Telefono2] [varchar](15) NULL,
	[TelefonoEmergencia] [varchar](15) NULL,
	[IsAlergico] [bit] NULL,
	[DescripcionAlergias] [varchar](200) NULL,
	[IsActivo] [bit] NULL,
	[Observaciones] [varchar](400) NULL,
	[ProfileImagePath] [varchar](max) NULL,
	[RutaDocumento1] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVoluntario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VoluntariosCampeonatos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoluntariosCampeonatos](
	[IdVoluntarioCampeonato] [int] IDENTITY(1,1) NOT NULL,
	[IdVoluntario] [int] NOT NULL,
	[IdCampeonato] [int] NOT NULL,
	[Funcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVoluntarioCampeonato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Campeonatos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campeonatos](
	[IdCampeonato] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NOT NULL,
	[FechaInicio] [date] NULL,
	[FechaFin] [date] NULL,
	[Ubicacion] [varchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCampeonato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VoluntariosEntrenamientos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoluntariosEntrenamientos](
	[IdVoluntarioEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[IdVoluntario] [int] NOT NULL,
	[IdEntrenamiento] [int] NOT NULL,
	[Funcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVoluntarioEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Entrenamientos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrenamientos](
	[IdEntrenamiento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[FechaInicio] [datetime2](0) NULL,
	[Ubicacion] [varchar](400) NULL,
	[Observaciones] [varchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEntrenamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  View [dbo].[VistaVoluntariosEntrenamientos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VistaVoluntariosEntrenamientos]
AS
SELECT dbo.VoluntariosEntrenamientos.IdVoluntarioEntrenamiento, dbo.VoluntariosEntrenamientos.IdEntrenamiento, dbo.Entrenamientos.Nombre AS NombreEntrenamiento, dbo.VoluntariosEntrenamientos.IdVoluntario, dbo.Voluntarios.Nombre AS NombreVoluntario, 
             dbo.Voluntarios.Apellido1, dbo.VoluntariosEntrenamientos.Funcion
FROM   dbo.Entrenamientos INNER JOIN
             dbo.VoluntariosEntrenamientos ON dbo.Entrenamientos.IdEntrenamiento = dbo.VoluntariosEntrenamientos.IdEntrenamiento INNER JOIN
             dbo.Voluntarios ON dbo.VoluntariosEntrenamientos.IdVoluntario = dbo.Voluntarios.IdVoluntario
GO

/****** Object:  View [dbo].[VistaVoluntariosCampeonatos]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VistaVoluntariosCampeonatos]
AS
SELECT dbo.VoluntariosCampeonatos.IdVoluntarioCampeonato, dbo.Campeonatos.IdCampeonato, dbo.Campeonatos.Nombre AS NombreCampeonato, dbo.Voluntarios.IdVoluntario, dbo.Voluntarios.Nombre AS NombreVoluntario, dbo.Voluntarios.Apellido1, 
             dbo.VoluntariosCampeonatos.Funcion
FROM   dbo.Campeonatos INNER JOIN
             dbo.VoluntariosCampeonatos ON dbo.Campeonatos.IdCampeonato = dbo.VoluntariosCampeonatos.IdCampeonato INNER JOIN
             dbo.Voluntarios ON dbo.VoluntariosCampeonatos.IdVoluntario = dbo.Voluntarios.IdVoluntario
GO

/********************
*					*
*   PROCEDIMIENTOS  *
*					*
*********************/

/****** Object:  StoredProcedure [dbo].[GetCampeonatosFromVoluntarioByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Pascual
-- Create date: 05/01/2021
-- Description:	Obtiene los campeonatos en los que ha participado el voluntario dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetCampeonatosFromVoluntarioByID] 
	-- Add the parameters for the stored procedure here
	@IDVoluntario int = 0
AS
BEGIN

    SELECT C.IdCampeonato, Nombre, FechaInicio, FechaFin, Funcion, Ubicacion
	FROM VoluntariosCampeonatos VC INNER JOIN Campeonatos C ON VC.IdCampeonato = C.IdCampeonato
	WHERE IdVoluntario = @IDVoluntario

END
GO

/****** Object:  StoredProcedure [dbo].[GetEntrenamientosFromVoluntarioByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Pascual
-- Create date: 04/01/2021
-- Description:	Obtiene los entrenamientos en los que participa el voluntario dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetEntrenamientosFromVoluntarioByID] 
	-- Add the parameters for the stored procedure here
	@IDVoluntario int = 0
AS
BEGIN
	SELECT E.IdEntrenamiento, Nombre, FechaInicio, Funcion, Ubicacion
	FROM VoluntariosEntrenamientos VE INNER JOIN Entrenamientos E ON E.IDEntrenamiento = VE.IdEntrenamiento
	WHERE IdVoluntario = @IDVoluntario

END
GO

/****** Object:  StoredProcedure [dbo].[GetVoluntariosDisponiblesForCampeonatoByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Pascual
-- Create date: 15/02/2021
-- Description:	Obtiene los voluntarios en activo que podrían participar en el campeonato dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetVoluntariosDisponiblesForCampeonatoByID] 
	-- Add the parameters for the stored procedure here
	@IDCampeonato int = 0
AS
BEGIN
	SELECT IdVoluntario, 0 as IdVoluntarioActividad, Nombre, Apellido1, Apellido2, Telefono1, Email, '' as Funcion
	FROM Voluntarios
	WHERE IdVoluntario NOT IN (
		SELECT VC.IdVoluntario
		FROM VoluntariosCampeonatos VC JOIN Voluntarios V ON V.IdVoluntario = VC.IdVoluntario
		WHERE IdCampeonato = @IDCampeonato
	) AND IsActivo = 1

END
GO

/****** Object:  StoredProcedure [dbo].[GetVoluntariosDisponiblesForEntrenamientoByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Pascual
-- Create date: 15/02/2021
-- Description:	Obtiene los voluntarios en activo que podrían participar en el entrenamiento dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetVoluntariosDisponiblesForEntrenamientoByID] 
	-- Add the parameters for the stored procedure here
	@IDEntrenamiento int = 0
AS
BEGIN
	SELECT IdVoluntario, 0 as IdVoluntarioActividad, Nombre, Apellido1, Apellido2, Telefono1, Email, '' as Funcion
	FROM Voluntarios
	WHERE IdVoluntario NOT IN (
		SELECT VE.IdVoluntario
		FROM VoluntariosEntrenamientos VE JOIN Voluntarios V ON V.IdVoluntario = VE.IdVoluntario
		WHERE IdEntrenamiento = @IDEntrenamiento
	) AND IsActivo = 1

END
GO

/****** Object:  StoredProcedure [dbo].[GetVoluntariosFromCampeonatoByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jorge Pascual
-- Create date: 15/02/2021
-- Description:	Obtiene los voluntarios que participan en el campeonato dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetVoluntariosFromCampeonatoByID] 
	-- Add the parameters for the stored procedure here
	@IDCampeonato int = 0
AS
BEGIN
	SELECT V.IdVoluntario, IdVoluntarioCampeonato as IdVoluntarioActividad, Nombre, Apellido1, Apellido2, Telefono1, Email, Funcion
	FROM VoluntariosCampeonatos VC INNER JOIN Voluntarios V ON V.IdVoluntario = VC.IdVoluntario
	WHERE IdCampeonato = @IDCampeonato

END
GO

/****** Object:  StoredProcedure [dbo].[GetVoluntariosFromEntrenamientoByID]    Script Date: 29/04/2021 15:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Jorge Pascual
-- Create date: 15/02/2021
-- Description:	Obtiene los voluntarios que participan en el entrenamiento dado por ID
-- =============================================
CREATE PROCEDURE [dbo].[GetVoluntariosFromEntrenamientoByID] 
	-- Add the parameters for the stored procedure here
	@IDEntrenamiento int = 0
AS
BEGIN
	SELECT V.IdVoluntario, IdVoluntarioEntrenamiento as IdVoluntarioActividad, Nombre, Apellido1, Apellido2, Telefono1, Email, Funcion
	FROM VoluntariosEntrenamientos VE INNER JOIN Voluntarios V ON V.IdVoluntario = VE.IdVoluntario
	WHERE IdEntrenamiento = @IDEntrenamiento

END
GO
USE [master]
GO
ALTER DATABASE [SpecialOlympics] SET  READ_WRITE 
GO
