-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: d3hiringdb
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `tbl_class`
--

DROP TABLE IF EXISTS `tbl_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_class` (
  `classID` int NOT NULL AUTO_INCREMENT,
  `teacherID` int NOT NULL,
  `studentID` int NOT NULL,
  `dt_created` datetime NOT NULL,
  `dt_last_update` datetime DEFAULT NULL,
  PRIMARY KEY (`classID`),
  KEY `teacherID_idx` (`teacherID`),
  KEY `studentID_idx` (`studentID`),
  CONSTRAINT `studentID` FOREIGN KEY (`studentID`) REFERENCES `tbl_student` (`studentID`),
  CONSTRAINT `teacherID` FOREIGN KEY (`teacherID`) REFERENCES `tbl_teacher` (`teacherID`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_class`
--

LOCK TABLES `tbl_class` WRITE;
/*!40000 ALTER TABLE `tbl_class` DISABLE KEYS */;
INSERT INTO `tbl_class` VALUES (23,1,1,'2020-02-28 18:00:15',NULL),(24,1,2,'2020-02-28 18:00:15',NULL),(25,1,3,'2020-02-28 18:00:15',NULL),(26,1,4,'2020-02-28 18:00:15',NULL),(27,2,1,'2020-02-28 18:00:44',NULL),(28,2,2,'2020-02-28 18:00:44',NULL),(29,3,3,'2020-02-29 18:03:52',NULL);
/*!40000 ALTER TABLE `tbl_class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_notification`
--

DROP TABLE IF EXISTS `tbl_notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_notification` (
  `notificationID` int NOT NULL AUTO_INCREMENT,
  `teacherID` int NOT NULL,
  `notification` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `recipients` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `dt_created` datetime NOT NULL,
  `dt_last_update` datetime DEFAULT NULL,
  PRIMARY KEY (`notificationID`),
  KEY `teacherID_idx` (`teacherID`),
  CONSTRAINT `FK_teacherID` FOREIGN KEY (`teacherID`) REFERENCES `tbl_teacher` (`teacherID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_notification`
--

LOCK TABLES `tbl_notification` WRITE;
/*!40000 ALTER TABLE `tbl_notification` DISABLE KEYS */;
INSERT INTO `tbl_notification` VALUES (2,3,'Hello students!','studentmiche@gmail.com','2020-03-01 19:26:12',NULL),(3,3,'Hello students! @studentagnes@gmail.com @studentbob@gmail.com @studentmiche@gmail.com','studentmiche@gmail.com,studentbob@gmail.com,studentagnes@gmail.com','2020-03-01 19:30:13',NULL),(4,3,'Hello students! @studentagnes@gmail.com @studentbob@gmail.com @studentmiche@gmail.com','studentmiche@gmail.com,studentbob@gmail.com,studentagnes@gmail.com','2020-03-01 20:45:49',NULL);
/*!40000 ALTER TABLE `tbl_notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_student`
--

DROP TABLE IF EXISTS `tbl_student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_student` (
  `studentID` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `suspended` tinyint NOT NULL DEFAULT '0',
  `dt_created` datetime NOT NULL,
  `dt_last_update` datetime DEFAULT NULL,
  PRIMARY KEY (`studentID`),
  UNIQUE KEY `studentID_UNIQUE` (`studentID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_student`
--

LOCK TABLES `tbl_student` WRITE;
/*!40000 ALTER TABLE `tbl_student` DISABLE KEYS */;
INSERT INTO `tbl_student` VALUES (1,'Bob','studentbob@gmail.com',0,'2020-02-28 11:40:22',NULL),(2,'Agnes','studentagnes@gmail.com',0,'2020-02-28 11:40:22','2020-03-01 17:59:13'),(3,'Miche','studentmiche@gmail.com',0,'2020-02-28 11:40:22',NULL),(4,'Jeff','studentjeff@gmail.com',0,'2020-02-28 11:40:22',NULL),(5,'Mary','studentmary@gmail.com',0,'2020-02-28 11:40:22',NULL),(6,'Jimmy','studentjimmy@gmail.com',0,'2020-02-28 17:42:26','2020-03-01 20:47:05'),(7,'Clai','studentclai@gmail.com',0,'2020-02-28 17:42:26',NULL);
/*!40000 ALTER TABLE `tbl_student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_teacher`
--

DROP TABLE IF EXISTS `tbl_teacher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_teacher` (
  `teacherID` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `email` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `dt_created` datetime NOT NULL,
  `dt_last_update` datetime DEFAULT NULL,
  PRIMARY KEY (`teacherID`),
  UNIQUE KEY `teacherID_UNIQUE` (`teacherID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_teacher`
--

LOCK TABLES `tbl_teacher` WRITE;
/*!40000 ALTER TABLE `tbl_teacher` DISABLE KEYS */;
INSERT INTO `tbl_teacher` VALUES (1,'Ken','teacherken@gmail.com','2020-02-28 11:38:52',NULL),(2,'Joe','teacherjoe@gmail.com','2020-02-28 11:38:52',NULL),(3,'Peter','teacherpeter@gmail.com','2020-02-28 11:38:52',NULL),(4,'James','teacherjames@gmail.com','2020-02-28 11:38:52',NULL);
/*!40000 ALTER TABLE `tbl_teacher` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-01 21:36:10
