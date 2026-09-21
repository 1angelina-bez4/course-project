CREATE DATABASE  IF NOT EXISTS `flowershop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `flowershop`;
-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: flowershop
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `idCategories` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(300) NOT NULL,
  PRIMARY KEY (`idCategories`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Цветы','Свежие срезанные цветы различных видов для букетов и оформления помещений.'),(2,'Букеты','Готовые букеты из свежих цветов для подарков, праздников и особых мероприятий.'),(3,'Комнатные растения','Декоративные растения для выращивания в домашних условиях и украшения интерьера.'),(4,'Вазы','Вазы различных форм, размеров и материалов для размещения и оформления цветов.'),(5,'Горшки и кашпо','Горшки и декоративные кашпо для комнатных растений и оформления интерьера.'),(6,'Удобрения и уход','Товары для ухода за растениями, включая удобрения, грунт и средства для защиты растений.'),(7,'Игрушки','Мягкие игрушки и другие небольшие подарки, которые можно дополнить цветочной композицией.'),(8,'Воздушные шары','Воздушные шары различных цветов и форм для оформления праздников и подарков.'),(9,'Открытки','Поздравительные открытки для различных праздников, памятных событий и особых случаев.'),(10,'Подарки и аксессуары','Дополнительные подарки и аксессуары для оформления букетов и праздничных композиций.');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `idProduct` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Price` float NOT NULL,
  `idCategory` int NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Image` varchar(255) NOT NULL,
  PRIMARY KEY (`idProduct`),
  KEY `idCategory_idx` (`idCategory`),
  CONSTRAINT `Product_Category` FOREIGN KEY (`idCategory`) REFERENCES `categories` (`idCategories`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Роза красная',350,1,'Свежая красная роза с крупным бутоном, подходящая для букетов и оформления подарков.','rose_red.jpg'),(2,'Тюльпан розовый',200,1,'Свежий розовый тюльпан с ярким бутоном для создания букетов и цветочных композиций.','tulip_pink.jpg'),(3,'Букет Нежность',2500,2,'Элегантный букет из розовых роз и нежных декоративных цветов для подарка и праздничного оформления.','bouquet_tenderness.jpg'),(4,'Букет Весенний',2200,2,'Яркий весенний букет из тюльпанов различных оттенков с декоративной зеленью.','bouquet_spring.jpg'),(5,'Фикус Бенджамина',1800,3,'Декоративное комнатное растение с густой зеленой листвой, подходящее для украшения интерьера.','ficus.jpg'),(6,'Орхидея Фаленопсис',2500,3,'Популярное комнатное растение с крупными цветами, подходящее для дома и офиса.','orchid.jpg'),(7,'Ваза стеклянная',1200,4,'Прозрачная стеклянная ваза классической формы для размещения свежих цветов и букетов.','glass_vase.jpg'),(8,'Ваза керамическая',1800,4,'Декоративная керамическая ваза для цветов, подходящая для оформления интерьера.','ceramic_vase.jpg'),(9,'Горшок керамический',850,5,'Керамический горшок для комнатных растений, подходящий для выращивания небольших цветов.','ceramic_pot.jpg'),(10,'Кашпо декоративное',1400,5,'Декоративное кашпо для комнатных растений, предназначенное для украшения интерьера.','decorative_planter.jpg'),(11,'Удобрение для комнатных растений',650,6,'Комплексное удобрение для подкормки комнатных растений и поддержания их здорового роста.','plant_fertilizer.jpg'),(12,'Грунт универсальный',450,6,'Универсальный грунт для посадки и пересадки комнатных декоративных растений.','universal_soil.jpg'),(13,'Мягкий медвежонок',1500,7,'Мягкая игрушка в виде медвежонка, которая подходит в качестве дополнения к цветочному подарку.','teddy_bear.jpg'),(14,'Мягкий зайчик',1300,7,'Мягкая игрушка в виде зайчика для подарка вместе с букетом или цветочной композицией.','bunny.jpg'),(15,'Воздушный шар Сердце',350,8,'Декоративный воздушный шар в форме сердца для оформления праздников и подарочных композиций.','heart_balloon.jpg'),(16,'Набор воздушных шаров',900,8,'Набор разноцветных воздушных шаров для праздничного оформления помещений и мероприятий.','balloons_set.jpg'),(17,'Открытка С днем рождения',250,9,'Поздравительная открытка с праздничным дизайном для подарка на день рождения.','birthday_card.jpg'),(18,'Открытка С любовью',250,9,'Романтическая поздравительная открытка для выражения теплых чувств и дополнения цветочного подарка.','love_card.jpg'),(19,'Подарочная коробка',700,10,'Декоративная подарочная коробка для упаковки небольших подарков и цветочных композиций.','gift_box.jpg'),(20,'Лента декоративная',300,10,'Декоративная лента для оформления букетов, подарочных коробок и праздничных композиций.','decorative_ribbon.jpg');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `idRole` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idRole`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Директор','Просматривает данные о продажах, состоянии склада и поставщиках, а также формирует отчеты о продажах и складских остатках в формате Excel.'),(2,'Администратор','Управляет информацией о пользователях, поставщиках, категориях и товарах: добавляет, изменяет, удаляет и просматривает данные.'),(3,'Товаровед','Управляет товарами и складскими запасами, выполняет добавление, изменение, удаление и просмотр данных, а также формирует отчет об остатках в формате Excel.'),(4,'Продавец','Просматривает товары, формирует чеки и оформляет продажи с указанием товара, количества, цены, скидки и номера чека, а также создает чек в формате MS Word.');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sale`
--

DROP TABLE IF EXISTS `sale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sale` (
  `idSale` int NOT NULL AUTO_INCREMENT,
  `SaleDate` datetime NOT NULL,
  `TotalAmount` float NOT NULL,
  `PaymentType` enum('Cash','Card') NOT NULL,
  `user_idUser` int NOT NULL,
  PRIMARY KEY (`idSale`),
  KEY `user_idUser_idx` (`user_idUser`),
  CONSTRAINT `Sale_User` FOREIGN KEY (`user_idUser`) REFERENCES `user` (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sale`
--

LOCK TABLES `sale` WRITE;
/*!40000 ALTER TABLE `sale` DISABLE KEYS */;
INSERT INTO `sale` VALUES (1,'2026-09-12 10:15:00',3800,'Cash',7),(2,'2026-09-12 11:40:00',3700,'Card',8),(3,'2026-09-13 12:20:00',4150,'Card',7),(4,'2026-09-13 15:10:00',4250,'Cash',8),(5,'2026-09-14 13:35:00',2800,'Card',7),(6,'2026-09-14 17:20:00',3700,'Card',8);
/*!40000 ALTER TABLE `sale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `saleitem`
--

DROP TABLE IF EXISTS `saleitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `saleitem` (
  `idSales` int NOT NULL AUTO_INCREMENT,
  `PriceAtTime` float NOT NULL,
  `Product_idProduct` int NOT NULL,
  `Quantity` int NOT NULL,
  `Sale_idSale` int NOT NULL,
  PRIMARY KEY (`idSales`),
  KEY `Product_idProduct_idx` (`Product_idProduct`),
  KEY `Sale_idSale_idx` (`Sale_idSale`),
  CONSTRAINT `SaleItem_Product` FOREIGN KEY (`Product_idProduct`) REFERENCES `product` (`idProduct`),
  CONSTRAINT `SaleItem_Sale` FOREIGN KEY (`Sale_idSale`) REFERENCES `sale` (`idSale`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `saleitem`
--

LOCK TABLES `saleitem` WRITE;
/*!40000 ALTER TABLE `saleitem` DISABLE KEYS */;
INSERT INTO `saleitem` VALUES (1,350,1,2,1),(2,200,2,3,1),(3,2500,3,1,1),(4,2500,6,1,2),(5,1200,7,1,2),(6,1800,5,1,3),(7,850,9,2,3),(8,650,11,1,3),(9,2200,4,1,4),(10,900,16,2,4),(11,250,17,1,4),(12,1500,13,1,5),(13,700,19,1,5),(14,300,20,2,5),(15,1800,8,1,6),(16,1400,10,1,6),(17,250,18,2,6);
/*!40000 ALTER TABLE `saleitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storehouse`
--

DROP TABLE IF EXISTS `storehouse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `storehouse` (
  `idStorehouse` int NOT NULL AUTO_INCREMENT,
  `QuantityInStock` int NOT NULL,
  `NumberOfShippedItems` int NOT NULL,
  `DeliveryDate` datetime NOT NULL,
  `Product_idProduct` int NOT NULL,
  `Suppliers_idSuppliers` int NOT NULL,
  PRIMARY KEY (`idStorehouse`),
  KEY `Product_idProduct_idx` (`Product_idProduct`),
  KEY `Suppliers_idSuppliers_idx` (`Suppliers_idSuppliers`),
  CONSTRAINT `Storehouse_Product` FOREIGN KEY (`Product_idProduct`) REFERENCES `product` (`idProduct`),
  CONSTRAINT `Storehouse_Suppliers` FOREIGN KEY (`Suppliers_idSuppliers`) REFERENCES `suppliers` (`idSuppliers`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storehouse`
--

LOCK TABLES `storehouse` WRITE;
/*!40000 ALTER TABLE `storehouse` DISABLE KEYS */;
INSERT INTO `storehouse` VALUES (1,45,120,'2026-09-01 09:30:00',1,1),(2,60,150,'2026-09-02 10:00:00',2,2),(3,18,55,'2026-09-03 11:30:00',3,1),(4,22,70,'2026-09-03 12:00:00',4,2),(5,15,35,'2026-09-04 09:00:00',5,3),(6,12,30,'2026-09-04 10:30:00',6,4),(7,25,60,'2026-09-05 13:00:00',7,5),(8,20,45,'2026-09-05 13:30:00',8,5),(9,35,80,'2026-09-06 09:30:00',9,5),(10,28,65,'2026-09-06 10:00:00',10,5),(11,40,100,'2026-09-07 11:00:00',11,7),(12,50,120,'2026-09-07 11:30:00',12,7),(13,16,40,'2026-09-08 14:00:00',13,6),(14,14,35,'2026-09-08 14:30:00',14,6),(15,55,140,'2026-09-09 09:00:00',15,8),(16,40,100,'2026-09-09 09:30:00',16,8),(17,70,180,'2026-09-10 12:00:00',17,8),(18,65,160,'2026-09-10 12:30:00',18,8),(19,30,75,'2026-09-11 10:00:00',19,6),(20,50,110,'2026-09-11 10:30:00',20,6);
/*!40000 ALTER TABLE `storehouse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `idSuppliers` int NOT NULL AUTO_INCREMENT,
  `Address` varchar(255) NOT NULL,
  `ContactPerson` varchar(255) NOT NULL,
  `NumberPhone` varchar(45) NOT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `SupplierCompany` varchar(100) NOT NULL,
  PRIMARY KEY (`idSuppliers`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `suppliers`
--

LOCK TABLES `suppliers` WRITE;
/*!40000 ALTER TABLE `suppliers` DISABLE KEYS */;
INSERT INTO `suppliers` VALUES (1,'г. Москва, ул. Цветочная, д. 15','Иванов Сергей Петрович','+7-495-111-22-33','ivanov.flowers@mail.ru','ИП Иванов'),(2,'г. Москва, ул. Садовая, д. 28','Петрова Анна Сергеевна','+7-495-222-33-44','petrova.flowers@mail.ru','ООО «Флора»'),(3,'г. Санкт-Петербург, пр. Цветочный, д. 10','Смирнов Алексей Викторович','+7-812-333-44-55','smirnov.plants@mail.ru','ООО «Грин Плант»'),(4,'г. Санкт-Петербург, ул. Садовников, д. 7','Кузнецова Елена Андреевна','+7-812-444-55-66','kuznetsova.plants@mail.ru','ООО «Растения»'),(5,'г. Москва, ул. Производственная, д. 42','Соколов Дмитрий Олегович','+7-495-555-66-77','sokolov.pots@mail.ru','ООО «ГоршкиОпт»'),(6,'г. Тула, ул. Центральная, д. 19','Морозова Мария Игоревна','+7-487-666-77-88','morozova.decor@mail.ru','ИП Морозова'),(7,'г. Калуга, ул. Лесная, д. 31','Волков Николай Александрович','+7-484-777-88-99','volkov.fertilizer@mail.ru','ООО «АгроХим»'),(8,'г. Москва, ул. Праздничная, д. 5','Орлова Наталья Владимировна','+7-495-888-99-00','orlova.gifts@mail.ru','ООО «Праздник»');
/*!40000 ALTER TABLE `suppliers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `idUser` int NOT NULL AUTO_INCREMENT,
  `Surname` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Patronymic` varchar(255) NOT NULL,
  `PhoneNumber` varchar(25) NOT NULL,
  `Login` varchar(100) NOT NULL,
  `Password` varchar(25) NOT NULL,
  `idRole` int NOT NULL,
  PRIMARY KEY (`idUser`),
  KEY `idRole_idx` (`idRole`),
  CONSTRAINT `User_Role` FOREIGN KEY (`idRole`) REFERENCES `role` (`idRole`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Иванов','Александр','Сергеевич','+7-900-111-22-33','director','Director123',1),(2,'Петрова','Елена','Викторовна','+7-900-222-33-44','director2','Director456',1),(3,'Сидорова','Анна','Дмитриевна','+7-900-333-44-55','admin','Admin123',2),(4,'Кузнецов','Максим','Андреевич','+7-900-444-55-66','admin2','Admin456',2),(5,'Смирнов','Дмитрий','Александрович','+7-900-555-66-77','merchandiser','Merch123',3),(6,'Волкова','Мария','Сергеевна','+7-900-666-77-88','merchandiser2','Merch456',3),(7,'Морозов','Иван','Петрович','+7-900-777-88-99','seller','Seller123',4),(8,'Орлова','Наталья','Игоревна','+7-900-888-99-00','seller2','Seller456',4);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-09-17 16:20:50
