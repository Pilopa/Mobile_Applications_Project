SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- The following statements require a database 'marblemaze' to exist!

-- -----------------------------------------------------
-- Table `marblemaze`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `marblemaze`.`User` ;

CREATE TABLE IF NOT EXISTS `marblemaze`.`User` (
  `idUser` INT NOT NULL,
  `username` VARCHAR(50) NULL,
  `passwordHash` VARCHAR(255) NULL,
  `authToken` VARCHAR(255) NULL,
  `lastRequestTimestamp` DATETIME NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC));


-- -----------------------------------------------------
-- Table `marblemaze`.`Level`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `marblemaze`.`Level` ;

CREATE TABLE IF NOT EXISTS `marblemaze`.`Level` (
  `idLevel` INT NOT NULL,
  PRIMARY KEY (`idLevel`));


-- -----------------------------------------------------
-- Table `marblemaze`.`Highscore`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `marblemaze`.`Highscore` ;

CREATE TABLE IF NOT EXISTS `marblemaze`.`Highscore` (
  `User_idUser` INT NOT NULL,
  `Level_idLevel` INT NOT NULL,
  `value` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`User_idUser`, `Level_idLevel`),
  INDEX `fk_User_has_Level_Level1_idx` (`Level_idLevel` ASC),
  INDEX `fk_User_has_Level_User_idx` (`User_idUser` ASC),
  CONSTRAINT `fk_User_has_Level_User`
    FOREIGN KEY (`User_idUser`)
    REFERENCES `marblemaze`.`User` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_has_Level_Level1`
    FOREIGN KEY (`Level_idLevel`)
    REFERENCES `marblemaze`.`Level` (`idLevel`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
