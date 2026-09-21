-- =====================================================
-- Flower Shop Database
-- =====================================================

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- =====================================================
-- Database
-- =====================================================

CREATE DATABASE IF NOT EXISTS `FlowerShop`
    DEFAULT CHARACTER SET utf8mb4
    COLLATE utf8mb4_0900_ai_ci;

USE `FlowerShop`;


-- =====================================================
-- Table: Categories
-- =====================================================

CREATE TABLE IF NOT EXISTS `Categories` (
    `idCategories` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(100) NOT NULL,
    `Description` VARCHAR(300) NOT NULL,

    PRIMARY KEY (`idCategories`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: Suppliers
-- =====================================================

CREATE TABLE IF NOT EXISTS `Suppliers` (
    `idSuppliers` INT NOT NULL AUTO_INCREMENT,
    `Address` VARCHAR(255) NOT NULL,
    `ContactPerson` VARCHAR(255) NOT NULL,
    `NumberPhone` VARCHAR(45) NOT NULL,
    `Email` VARCHAR(45) NOT NULL,

    PRIMARY KEY (`idSuppliers`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: Role
-- =====================================================

CREATE TABLE IF NOT EXISTS `Role` (
    `idRole` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(100) NOT NULL,
    `Description` VARCHAR(255) NOT NULL,

    PRIMARY KEY (`idRole`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: Product
-- =====================================================

CREATE TABLE IF NOT EXISTS `Product` (
    `idProduct` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(100) NOT NULL,
    `Price` FLOAT NOT NULL,
    `idCategory` INT NOT NULL,
    `Description` VARCHAR(255) NOT NULL,
    `Image` VARCHAR(255) NOT NULL,

    PRIMARY KEY (`idProduct`),

    INDEX `idCategory_idx` (`idCategory` ASC),

    CONSTRAINT `Product_Category`
        FOREIGN KEY (`idCategory`)
        REFERENCES `Categories` (`idCategories`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: Storehouse
-- =====================================================

CREATE TABLE IF NOT EXISTS `Storehouse` (
    `idStorehouse` INT NOT NULL AUTO_INCREMENT,
    `QuantityInStock` INT NOT NULL,
    `NumberOfShippedItems` INT NOT NULL,
    `DeliveryDate` DATETIME NOT NULL,
    `Product_idProduct` INT NOT NULL,
    `Suppliers_idSuppliers` INT NOT NULL,

    PRIMARY KEY (`idStorehouse`),

    INDEX `Product_idProduct_idx` (`Product_idProduct` ASC),
    INDEX `Suppliers_idSuppliers_idx` (`Suppliers_idSuppliers` ASC),

    CONSTRAINT `Storehouse_Product`
        FOREIGN KEY (`Product_idProduct`)
        REFERENCES `Product` (`idProduct`),

    CONSTRAINT `Storehouse_Suppliers`
        FOREIGN KEY (`Suppliers_idSuppliers`)
        REFERENCES `Suppliers` (`idSuppliers`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: user
-- =====================================================

CREATE TABLE IF NOT EXISTS `user` (
    `idUser` INT NOT NULL AUTO_INCREMENT,
    `Surname` VARCHAR(255) NOT NULL,
    `Name` VARCHAR(255) NOT NULL,
    `Patronymic` VARCHAR(255) NOT NULL,
    `PhoneNumber` VARCHAR(25) NOT NULL,
    `Login` VARCHAR(100) NOT NULL,
    `Password` VARCHAR(25) NOT NULL,
    `idRole` INT NOT NULL,

    PRIMARY KEY (`idUser`),

    INDEX `idRole_idx` (`idRole` ASC),

    CONSTRAINT `User_Role`
        FOREIGN KEY (`idRole`)
        REFERENCES `Role` (`idRole`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: Sale
-- =====================================================

CREATE TABLE IF NOT EXISTS `Sale` (
    `idSale` INT NOT NULL AUTO_INCREMENT,
    `SaleDate` DATETIME NOT NULL,
    `TotalAmount` FLOAT NOT NULL,
    `PaymentType` ENUM('Cash', 'Card') NOT NULL,
    `user_idUser` INT NOT NULL,

    PRIMARY KEY (`idSale`),

    INDEX `user_idUser_idx` (`user_idUser` ASC),

    CONSTRAINT `Sale_User`
        FOREIGN KEY (`user_idUser`)
        REFERENCES `user` (`idUser`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Table: SaleItem
-- =====================================================

CREATE TABLE IF NOT EXISTS `SaleItem` (
    `idSales` INT NOT NULL AUTO_INCREMENT,
    `PriceAtTime` FLOAT NOT NULL,
    `Product_idProduct` INT NOT NULL,
    `Quantity` INT NOT NULL,
    `Sale_idSale` INT NOT NULL,

    PRIMARY KEY (`idSales`),

    INDEX `Product_idProduct_idx` (`Product_idProduct` ASC),
    INDEX `Sale_idSale_idx` (`Sale_idSale` ASC),

    CONSTRAINT `SaleItem_Product`
        FOREIGN KEY (`Product_idProduct`)
        REFERENCES `Product` (`idProduct`),

    CONSTRAINT `SaleItem_Sale`
        FOREIGN KEY (`Sale_idSale`)
        REFERENCES `Sale` (`idSale`)
)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- =====================================================
-- Restore settings
-- =====================================================

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;