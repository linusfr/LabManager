-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema se02team06
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema se02team06
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `se02team06` DEFAULT CHARACTER SET utf8 ;
USE `se02team06` ;

-- -----------------------------------------------------
-- Table `se02team06`.`course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`course` (
  `courseID` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `semester` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`courseID`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`lab`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`lab` (
  `labID` INT(11) NOT NULL AUTO_INCREMENT,
  `labNumber` INT(11) NOT NULL,
  `course_courseID` INT(11) NOT NULL,
  PRIMARY KEY (`labID`),
  CONSTRAINT `fk_lab_course1`
    FOREIGN KEY (`course_courseID`)
    REFERENCES `se02team06`.`course` (`courseID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 17
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`labdate`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`labdate` (
  `labdateID` INT(11) NOT NULL AUTO_INCREMENT,
  `date` DATETIME NOT NULL,
  `lab_labID` INT(11) NOT NULL,
  PRIMARY KEY (`labdateID`),
  CONSTRAINT `fk_labdate_lab1`
    FOREIGN KEY (`lab_labID`)
    REFERENCES `se02team06`.`lab` (`labID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 25
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`lecturer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`lecturer` (
  `lecturerID` INT(11) NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(45) NOT NULL,
  `lastName` VARCHAR(45) NOT NULL,
  `salutation` VARCHAR(45) NULL DEFAULT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`lecturerID`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`lecturer_has_course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`lecturer_has_course` (
  `lecturer_lecturerID` INT(11) NOT NULL,
  `course_courseID` INT(11) NOT NULL,
  PRIMARY KEY (`lecturer_lecturerID`, `course_courseID`),
  CONSTRAINT `fk_lecturer_has_course_course1`
    FOREIGN KEY (`course_courseID`)
    REFERENCES `se02team06`.`course` (`courseID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_lecturer_has_course_lecturer1`
    FOREIGN KEY (`lecturer_lecturerID`)
    REFERENCES `se02team06`.`lecturer` (`lecturerID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`student`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`student` (
  `studentID` INT(11) NOT NULL AUTO_INCREMENT,
  `matricelNumber` INT(11) NOT NULL,
  `firstName` VARCHAR(45) NOT NULL,
  `lastName` VARCHAR(45) NOT NULL,
  `studyCourse` VARCHAR(45) NOT NULL,
  `semester` INT(11) NOT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `salutation` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`studentID`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`present`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`present` (
  `presentID` INT(11) NOT NULL AUTO_INCREMENT,
  `wasPresent` TINYINT(4) NOT NULL DEFAULT 0,
  `note` VARCHAR(200) NOT NULL,
  `labdate_labdateID` INT(11) NOT NULL,
  `student_studentID` INT(11) NOT NULL,
  PRIMARY KEY (`presentID`),
  CONSTRAINT `fk_present_labdate1`
    FOREIGN KEY (`labdate_labdateID`)
    REFERENCES `se02team06`.`labdate` (`labdateID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_present_student1`
    FOREIGN KEY (`student_studentID`)
    REFERENCES `se02team06`.`student` (`studentID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`student_has_lab`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`student_has_lab` (
  `student_studentID` INT(11) NOT NULL,
  `lab_labID` INT(11) NOT NULL,
  PRIMARY KEY (`student_studentID`, `lab_labID`),
  CONSTRAINT `fk_student_has_lab_lab1`
    FOREIGN KEY (`lab_labID`)
    REFERENCES `se02team06`.`lab` (`labID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_student_has_lab_student1`
    FOREIGN KEY (`student_studentID`)
    REFERENCES `se02team06`.`student` (`studentID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`task`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`task` (
  `taskID` INT(11) NOT NULL AUTO_INCREMENT,
  `taskNumber` INT(11) NOT NULL,
  `dueDate` DATE NOT NULL,
  `lab_labID` INT(11) NOT NULL,
  PRIMARY KEY (`taskID`),
  INDEX `fk_task_lab1` (`lab_labID` ASC) VISIBLE,
  CONSTRAINT `fk_task_lab1`
    FOREIGN KEY (`lab_labID`)
    REFERENCES `se02team06`.`lab` (`labID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 25
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `se02team06`.`taskDone`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `se02team06`.`taskdone` (
  `taskDoneID` INT(11) NOT NULL AUTO_INCREMENT,
  `isDone` TINYINT(4) NOT NULL DEFAULT 0,
  `student_studentID` INT(11) NOT NULL,
  `task_taskID` INT(11) NOT NULL,
  PRIMARY KEY (`taskDoneID`),
  CONSTRAINT `fk_taskDone_student1`
    FOREIGN KEY (`student_studentID`)
    REFERENCES `se02team06`.`student` (`studentID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_taskDone_task1`
    FOREIGN KEY (`task_taskID`)
    REFERENCES `se02team06`.`task` (`taskID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
