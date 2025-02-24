-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema readingClub
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `readingClub` ;

-- -----------------------------------------------------
-- Schema readingClub
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `readingClub` DEFAULT CHARACTER SET utf8 ;
USE `readingClub` ;

-- -----------------------------------------------------
-- Table `readingClub`.`Member`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `readingClub`.`Member` ;

CREATE TABLE IF NOT EXISTS `readingClub`.`Member` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `datebirth` DATE NULL,
  `email` VARCHAR(100) NOT NULL,
  `telephone` VARCHAR(9) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `readingClub`.`Gender`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `readingClub`.`Gender` ;

CREATE TABLE IF NOT EXISTS `readingClub`.`Gender` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `readingClub`.`Book`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `readingClub`.`Book` ;

CREATE TABLE IF NOT EXISTS `readingClub`.`Book` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `title` VARCHAR(45) NOT NULL,
  `author` VARCHAR(45) NOT NULL,
  `publicationYear` INT NOT NULL,
  `Gender_ID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Book_Gender1_idx` (`Gender_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Book_Gender1`
    FOREIGN KEY (`Gender_ID`)
    REFERENCES `readingClub`.`Gender` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `readingClub`.`Loan`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `readingClub`.`Loan` ;

CREATE TABLE IF NOT EXISTS `readingClub`.`Loan` (
  `Member_ID` INT NOT NULL,
  `Book_ID` INT NOT NULL,
  `loanDate` DATE NULL,
  `returnDate` DATE NULL,
  INDEX `fk_Member_has_Book_Book1_idx` (`Book_ID` ASC) VISIBLE,
  INDEX `fk_Member_has_Book_Member_idx` (`Member_ID` ASC) VISIBLE,
  PRIMARY KEY (`Member_ID`, `Book_ID`),
  CONSTRAINT `fk_Member_has_Book_Member`
    FOREIGN KEY (`Member_ID`)
    REFERENCES `readingClub`.`Member` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_has_Book_Book1`
    FOREIGN KEY (`Book_ID`)
    REFERENCES `readingClub`.`Book` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
