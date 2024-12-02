-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema gestpro
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema gestpro
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `gestpro` DEFAULT CHARACTER SET utf8 ;
USE `gestpro` ;

-- -----------------------------------------------------
-- Table `gestpro`.`Proyecto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`Proyecto` (
  `IDPROYECTO` INT NOT NULL AUTO_INCREMENT,
  `CODIGOPROY` VARCHAR(32) NOT NULL,
  `NOMBREPROY` VARCHAR(32) NOT NULL,
  `DESCPROY` VARCHAR(1024) NOT NULL,
  `PRESUPUESTO` FLOAT NULL,
  `FECHAINICIO` DATE NULL,
  `FECHAFIN` DATE NULL,
  PRIMARY KEY (`IDPROYECTO`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `gestpro`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`Usuario` (
  `IDUSUARIO` INT NOT NULL AUTO_INCREMENT,
  `NOMBREUSUARIO` VARCHAR(32) NOT NULL,
  `PASSUSUARIO` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`IDUSUARIO`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `gestpro`.`Rol`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`Rol` (
  `IDROL` INT NOT NULL AUTO_INCREMENT,
  `NOMBREROL` VARCHAR(64) NOT NULL,
  `DESCROL` VARCHAR(1024) NOT NULL,
  PRIMARY KEY (`IDROL`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `gestpro`.`Empleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`Empleado` (
  `IDEMPLEADO` INT NOT NULL AUTO_INCREMENT,
  `NOMBREEMP` VARCHAR(64) NOT NULL,
  `APELLIDOSEMP` VARCHAR(128) NOT NULL,
  `CSR` FLOAT NOT NULL,
  `IDUSUARIO` INT NOT NULL,
  `IDROL` INT NOT NULL,
  PRIMARY KEY (`IDEMPLEADO`),
  INDEX `fk_Empleado_Usuario1_idx` (`IDUSUARIO` ASC) VISIBLE,
  INDEX `fk_Empleado_Rol1_idx` (`IDROL` ASC) VISIBLE,
  CONSTRAINT `fk_Empleado_Usuario1`
    FOREIGN KEY (`IDUSUARIO`)
    REFERENCES `gestpro`.`Usuario` (`IDUSUARIO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Empleado_Rol1`
    FOREIGN KEY (`IDROL`)
    REFERENCES `gestpro`.`Rol` (`IDROL`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `gestpro`.`Factura`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`Factura` (
  `IDFACTURA` INT NOT NULL AUTO_INCREMENT,
  `NUMFACTURA` VARCHAR(16) NOT NULL,
  `DESCFACTURA` VARCHAR(1024) NOT NULL,
  `IMPORTE` FLOAT NOT NULL,
  `IDPROYECTO` INT NOT NULL,
  PRIMARY KEY (`IDFACTURA`),
  INDEX `fk_Factura_Proyecto1_idx` (`IDPROYECTO` ASC) VISIBLE,
  CONSTRAINT `fk_Factura_Proyecto1`
    FOREIGN KEY (`IDPROYECTO`)
    REFERENCES `gestpro`.`Proyecto` (`IDPROYECTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `gestpro`.`ProyectoEmpleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gestpro`.`ProyectoEmpleado` (
  `HORAS` INT NOT NULL,
  `COSTES` FLOAT NOT NULL,
  `FECHA` DATE NOT NULL,
  `IDPROYECTO` INT NOT NULL,
  `IDEMPLEADO` INT NOT NULL,
  PRIMARY KEY (`FECHA`, `IDPROYECTO`, `IDEMPLEADO`),
  INDEX `fk_PROYECTOEMPLEADO_Proyecto1_idx` (`IDPROYECTO` ASC) VISIBLE,
  INDEX `fk_PROYECTOEMPLEADO_Empleado1_idx` (`IDEMPLEADO` ASC) VISIBLE,
  CONSTRAINT `fk_PROYECTOEMPLEADO_Proyecto1`
    FOREIGN KEY (`IDPROYECTO`)
    REFERENCES `gestpro`.`Proyecto` (`IDPROYECTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PROYECTOEMPLEADO_Empleado1`
    FOREIGN KEY (`IDEMPLEADO`)
    REFERENCES `gestpro`.`Empleado` (`IDEMPLEADO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;