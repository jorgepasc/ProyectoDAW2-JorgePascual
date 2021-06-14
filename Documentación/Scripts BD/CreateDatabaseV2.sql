CREATE DATABASE specialolympics /* SQLINES DEMO *** RACTER SET latin1 */;

/* SQLINES DEMO *** *   TABLAS	*
*			*
*************/
CREATE TABLE Campeonatos (
  [IdCampeonato] int NOT NULL IDENTITY,
  [Nombre] varchar(80) NOT NULL,
  [FechaInicio] date DEFAULT NULL,
  [FechaFin] date DEFAULT NULL,
  [Ubicacion] varchar(400) DEFAULT NULL,
  PRIMARY KEY ([IdCampeonato])
)  ;

CREATE TABLE Entrenamientos (
  [IdEntrenamiento] int NOT NULL IDENTITY,
  [Nombre] varchar(50) DEFAULT NULL,
  [FechaInicio] datetime2(0) DEFAULT NULL,
  [Ubicacion] varchar(400) DEFAULT NULL,  
  [Observaciones] varchar(400) DEFAULT NULL,
  PRIMARY KEY ([IdEntrenamiento])
)  ;

CREATE TABLE Voluntarios (
  [IdVoluntario] int NOT NULL IDENTITY,
  [Nombre] varchar(20) DEFAULT NULL,
  [Apellido1] varchar(30) DEFAULT NULL,
  [Apellido2] varchar(30) DEFAULT NULL,
  [DNI] varchar(12) DEFAULT NULL,
  [Telefono1] varchar(15) DEFAULT NULL,
  [Email] varchar(80) DEFAULT NULL,  
  [DireccionCompleta] varchar(200) DEFAULT NULL,  
  [Poblacion] varchar(50) DEFAULT NULL,
  [Provincia] varchar(50) DEFAULT NULL,
  [CodigoPostal] varchar(10) DEFAULT NULL,
  [FechaNacimiento] datetime2(0) DEFAULT NULL,
  [FechaAlta] datetime2(0) DEFAULT NULL,  
  [Telefono2] varchar(15) DEFAULT NULL,
  [TelefonoEmergencia] varchar(15) DEFAULT NULL,
  [IsAlergico] bit DEFAULT NULL,
  [DescripcionAlergias] varchar(200) DEFAULT NULL,  
  [IsActivo] bit DEFAULT NULL,
  [Observaciones] varchar(400) DEFAULT NULL,
  PRIMARY KEY ([IdVoluntario])
)  ;

CREATE TABLE VoluntariosCampeonatos (
  [IdVoluntarioCampeonato] int NOT NULL IDENTITY,
  [IdVoluntario] int NOT NULL,
  [IdCampeonato] int NOT NULL,
  [Funcion] varchar(50) DEFAULT NULL,
  PRIMARY KEY ([IdVoluntarioCampeonato])
 ,
  CONSTRAINT [FK_Campeonato] FOREIGN KEY ([IdCampeonato]) REFERENCES campeonatos ([IdCampeonato]) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FK_Voluntario] FOREIGN KEY ([IdVoluntario]) REFERENCES voluntarios ([IdVoluntario]) ON DELETE CASCADE ON UPDATE CASCADE
)  ;

CREATE INDEX [FK_Voluntario] ON voluntarioscampeonatos ([IdVoluntario]);
CREATE INDEX [FK_Campeonato] ON voluntarioscampeonatos ([IdCampeonato]);

CREATE TABLE VoluntariosEntrenamientos (
  [IdVoluntarioEntrenamiento] int NOT NULL IDENTITY,
  [IdVoluntario] int NOT NULL,
  [IdEntrenamiento] int NOT NULL,
  [Funcion] varchar(50) DEFAULT NULL,
  PRIMARY KEY ([IdVoluntarioEntrenamiento])
 ,
  CONSTRAINT [FK_Entrenamiento] FOREIGN KEY ([IdEntrenamiento]) REFERENCES entrenamientos ([IdEntrenamiento]) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FK_Voluntario2] FOREIGN KEY ([IdVoluntario]) REFERENCES voluntarios ([IdVoluntario]) ON DELETE CASCADE ON UPDATE CASCADE
)  ;

CREATE INDEX [FK_Voluntario2] ON voluntariosentrenamientos ([IdVoluntario]);
CREATE INDEX [FK_Entrenamiento] ON voluntariosentrenamientos ([IdEntrenamiento]);

/************
*			*
*   VISTAS	*
*			*
*************/
CREATE VIEW [dbo].[VistaVoluntariosCampeonatos]
AS
SELECT dbo.VoluntariosCampeonatos.IdVoluntarioCampeonato, dbo.Campeonatos.IdCampeonato, dbo.Campeonatos.Nombre AS NombreCampeonato, dbo.Voluntarios.IdVoluntario, dbo.Voluntarios.Nombre AS NombreVoluntario, dbo.Voluntarios.Apellido1, 
             dbo.VoluntariosCampeonatos.Funcion
FROM   dbo.Campeonatos INNER JOIN
             dbo.VoluntariosCampeonatos ON dbo.Campeonatos.IdCampeonato = dbo.VoluntariosCampeonatos.IdCampeonato INNER JOIN
             dbo.Voluntarios ON dbo.VoluntariosCampeonatos.IdVoluntario = dbo.Voluntarios.IdVoluntario
GO


CREATE VIEW [dbo].[VistaVoluntariosEntrenamientos]
AS
SELECT dbo.VoluntariosEntrenamientos.IdVoluntarioEntrenamiento, dbo.VoluntariosEntrenamientos.IdEntrenamiento, dbo.Entrenamientos.Nombre AS NombreEntrenamiento, dbo.VoluntariosEntrenamientos.IdVoluntario, dbo.Voluntarios.Nombre AS NombreVoluntario, 
             dbo.Voluntarios.Apellido1, dbo.VoluntariosEntrenamientos.Funcion
FROM   dbo.Entrenamientos INNER JOIN
             dbo.VoluntariosEntrenamientos ON dbo.Entrenamientos.IdEntrenamiento = dbo.VoluntariosEntrenamientos.IdEntrenamiento INNER JOIN
             dbo.Voluntarios ON dbo.VoluntariosEntrenamientos.IdVoluntario = dbo.Voluntarios.IdVoluntario
GO

/********************
*					*
*   PROCEDIMIENTOS  *
*					*
*********************/

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
	SELECT E.IdEntrenamiento, Nombre, Funcion, Ubicacion
	FROM VoluntariosEntrenamientos VE INNER JOIN Entrenamientos E ON E.IDEntrenamiento = VE.IdEntrenamiento
	WHERE IdVoluntario = @IDVoluntario

END
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


/*SET SQL_SAFE_UPDATES = 0;
delete from voluntarioscampeonatos;
delete from voluntariosentrenamientos;
delete from voluntarios;
delete from campeonatos;
delete from entrenamientos;
dbcc checkident(Voluntarios, RESEED, 1)
dbcc checkident(Entrenamientos, RESEED, 1)
dbcc checkident(Campeonatos, RESEED, 1)
dbcc checkident(VoluntariosCampeonatos, RESEED, 1)
dbcc checkident(VoluntariosEntrenamientos, RESEED, 1)

SET SQL_SAFE_UPDATES = 1;*/




