CREATE DATABASE `specialolympics` /*!40100 DEFAULT CHARACTER SET latin1 */;

/************
*			*
*   TABLAS	*
*			*
*************/
CREATE TABLE `campeonatos` (
  `IdCampeonato` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(80) CHARACTER SET utf8 NOT NULL,
  `FechaInicio` date DEFAULT NULL,
  `FechaFin` date DEFAULT NULL,
  `Ubicacion` varchar(400) DEFAULT NULL,
  PRIMARY KEY (`IdCampeonato`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

CREATE TABLE `entrenamientos` (
  `IdEntrenamiento` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT NULL,
  `FechaInicio` datetime DEFAULT NULL,
  `Ubicacion` varchar(400) DEFAULT NULL,  
  `Observaciones` varchar(400) DEFAULT NULL,
  PRIMARY KEY (`IdEntrenamiento`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

CREATE TABLE `voluntarios` (
  `IdVoluntario` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(20) DEFAULT NULL,
  `Apellido1` varchar(30) DEFAULT NULL,
  `Apellido2` varchar(30) DEFAULT NULL,
  `DNI` varchar(12) DEFAULT NULL,
  `Telefono1` varchar(15) DEFAULT NULL,
  `Email` varchar(80) DEFAULT NULL,  
  `DireccionCompleta` varchar(200) DEFAULT NULL,  
  `Poblacion` varchar(50) DEFAULT NULL,
  `Provincia` varchar(50) DEFAULT NULL,
  `CodigoPostal` varchar(10) DEFAULT NULL,
  `FechaNacimiento` datetime DEFAULT NULL,
  `FechaAlta` datetime DEFAULT NULL,  
  `Telefono2` varchar(15) DEFAULT NULL,
  `TelefonoEmergencia` varchar(15) DEFAULT NULL,
  `IsAlergico` tinyint(1) DEFAULT NULL,
  `DescripcionAlergias` varchar(200) DEFAULT NULL,  
  `IsActivo` tinyint(1) DEFAULT NULL,
  `Observaciones` varchar(400) DEFAULT NULL,
  PRIMARY KEY (`IdVoluntario`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

CREATE TABLE `voluntarioscampeonatos` (
  `IdVoluntarioCampeonato` int(11) NOT NULL AUTO_INCREMENT,
  `IdVoluntario` int(11) NOT NULL,
  `IdCampeonato` int(11) NOT NULL,
  `Funcion` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdVoluntarioCampeonato`),
  KEY `FK_Voluntario` (`IdVoluntario`),
  KEY `FK_Campeonato` (`IdCampeonato`),
  CONSTRAINT `FK_Campeonato` FOREIGN KEY (`IdCampeonato`) REFERENCES `campeonatos` (`IdCampeonato`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Voluntario` FOREIGN KEY (`IdVoluntario`) REFERENCES `voluntarios` (`IdVoluntario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

CREATE TABLE `voluntariosentrenamientos` (
  `IdVoluntarioEntrenamiento` int(11) NOT NULL AUTO_INCREMENT,
  `IdVoluntario` int(11) NOT NULL,
  `IdEntrenamiento` int(11) NOT NULL,
  `Funcion` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdVoluntarioEntrenamiento`),
  KEY `FK_Voluntario2` (`IdVoluntario`),
  KEY `FK_Entrenamiento` (`IdEntrenamiento`),
  CONSTRAINT `FK_Entrenamiento` FOREIGN KEY (`IdEntrenamiento`) REFERENCES `entrenamientos` (`IdEntrenamiento`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Voluntario2` FOREIGN KEY (`IdVoluntario`) REFERENCES `voluntarios` (`IdVoluntario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

/************
*			*
*   VISTAS	*
*			*
*************/
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vistavoluntarioscampeonatos` AS select `voluntarioscampeonatos`.`IdVoluntarioCampeonato` AS `IdVoluntarioCampeonato`,`campeonatos`.`IdCampeonato` AS `IdCampeonato`,`campeonatos`.`Nombre` AS `Nombre`,`voluntarios`.`IdVoluntario` AS `IdVoluntario`,`voluntarios`.`Nombre` AS `NombreVoluntario`,`voluntarios`.`Apellido1` AS `Apellido1` from ((`campeonatos` join `voluntarioscampeonatos` on(`campeonatos`.`IdCampeonato` = `voluntarioscampeonatos`.`IdCampeonato`)) join `voluntarios` on(`voluntarioscampeonatos`.`IdVoluntario` = `voluntarios`.`IdVoluntario`));

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vistavoluntariosentrenamientos` AS select `voluntariosentrenamientos`.`IdVoluntarioEntrenamiento` AS `IdVoluntarioEntrenamiento`,`voluntariosentrenamientos`.`IdEntrenamiento` AS `IdEntrenamiento`,`entrenamientos`.`Nombre` AS `Nombre`,`voluntariosentrenamientos`.`IdVoluntario` AS `IdVoluntario`,`voluntarios`.`Nombre` AS `NombreVoluntario`,`voluntarios`.`Apellido1` AS `Apellido1` from ((`entrenamientos` join `voluntariosentrenamientos` on(`entrenamientos`.`IdEntrenamiento` = `voluntariosentrenamientos`.`IdEntrenamiento`)) join `voluntarios` on(`voluntariosentrenamientos`.`IdVoluntario` = `voluntarios`.`IdVoluntario`));

/********************
*					*
*   PROCEDIMIENTOS  *
*					*
*********************/

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetCampeonatosFromVoluntarioByID`(
	IN IDVol int
)
BEGIN
	SET IDVol = IFNULL(IDVol, 0); /*MySql no deja poner valores default a los parametros*/
    SELECT C.IdCampeonato, Nombre, FechaInicio, FechaFin, Funcion, Ubicacion
	FROM VoluntariosCampeonatos VC INNER JOIN Campeonatos C ON VC.IdCampeonato = C.IdCampeonato
	WHERE IDVoluntario = IDVol;
END$$
DELIMITER ;


DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEntrenamientosFromVoluntarioByID`(
	IN IDVol int
)
BEGIN
	SET IDVol = IFNULL(IDVol, 0); /*MySql no deja poner valores default a los parametros*/
    SELECT E.IdEntrenamiento, Nombre, Funcion, Ubicacion
	FROM VoluntariosEntrenamientos VE INNER JOIN Entrenamientos E ON VE.IdEntrenamiento = E.IdEntrenamiento
	WHERE IDVoluntario = IDVol;
END$$
DELIMITER ;

/*SET SQL_SAFE_UPDATES = 0;
delete from voluntarioscampeonatos;
delete from voluntariosentrenamientos;
delete from voluntarios;
delete from campeonatos;
delete from entrenamientos;
ALTER TABLE voluntarios AUTO_INCREMENT = 1;
ALTER TABLE campeonatos AUTO_INCREMENT = 1;
ALTER TABLE entrenamientos AUTO_INCREMENT = 1;
ALTER TABLE voluntariosentrenamientos AUTO_INCREMENT = 1;
ALTER TABLE voluntarioscampeonatos AUTO_INCREMENT = 1;
SET SQL_SAFE_UPDATES = 1;*/
