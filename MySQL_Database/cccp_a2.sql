-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 13, 2024 at 03:58 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cccp_a2`
--
CREATE DATABASE IF NOT EXISTS `cccp_a2` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `cccp_a2`;

-- --------------------------------------------------------

--
-- Table structure for table `sdam_book`
--

DROP TABLE IF EXISTS `sdam_book`;
CREATE TABLE `sdam_book` (
  `id` int(11) NOT NULL,
  `author` varchar(20) NOT NULL,
  `title` varchar(20) NOT NULL,
  `isbn` varchar(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sdam_book`
--

INSERT INTO `sdam_book` (`id`, `author`, `title`, `isbn`) VALUES
(1, 'Author 1', 'Title 1', '1234567890'),
(2, 'Author 2', 'Title 2', '2345678901'),
(3, 'Author 3', 'Title 3', '3456789012'),
(4, 'Author 4', 'Title 4', '4567890123');

-- --------------------------------------------------------

--
-- Table structure for table `sdam_loan`
--

DROP TABLE IF EXISTS `sdam_loan`;
CREATE TABLE `sdam_loan` (
  `id` int(11) NOT NULL,
  `memberId` int(11) NOT NULL,
  `bookId` int(11) NOT NULL,
  `loanDate` timestamp NULL DEFAULT NULL,
  `dueDate` timestamp NULL DEFAULT NULL,
  `returnDate` timestamp NULL DEFAULT NULL,
  `numberOfRenewals` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sdam_loan`
--

INSERT INTO `sdam_loan` (`id`, `memberId`, `bookId`, `loanDate`, `dueDate`, `returnDate`, `numberOfRenewals`) VALUES
(1, 1, 1, '2024-04-13 13:32:40', '2024-05-11 13:32:40', '2024-04-13 13:35:14', 1),
(2, 2, 2, '2024-04-13 13:33:06', '2024-05-25 13:33:06', '2024-04-13 13:35:23', 2),
(3, 3, 3, '2024-04-13 13:33:40', '2024-04-27 13:33:40', '2024-04-13 13:35:31', 0);

-- --------------------------------------------------------

--
-- Table structure for table `sdam_member`
--

DROP TABLE IF EXISTS `sdam_member`;
CREATE TABLE `sdam_member` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sdam_member`
--

INSERT INTO `sdam_member` (`id`, `name`) VALUES
(1, 'Graham'),
(2, 'Phil'),
(3, 'Fiona');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `sdam_book`
--
ALTER TABLE `sdam_book`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sdam_loan`
--
ALTER TABLE `sdam_loan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sdam_member`
--
ALTER TABLE `sdam_member`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `sdam_book`
--
ALTER TABLE `sdam_book`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `sdam_loan`
--
ALTER TABLE `sdam_loan`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `sdam_member`
--
ALTER TABLE `sdam_member`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
