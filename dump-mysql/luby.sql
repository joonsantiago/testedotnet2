-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 16-Jan-2021 às 03:07
-- Versão do servidor: 10.4.17-MariaDB
-- versão do PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `luby`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `notification`
--

CREATE TABLE `notification` (
  `Id` int(11) NOT NULL,
  `WorkHourId` int(11) NOT NULL,
  `Status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `notification`
--

INSERT INTO `notification` (`Id`, `WorkHourId`, `Status`) VALUES
(1, 4, 1),
(2, 5, 1),
(3, 6, 2),
(4, 7, 1),
(5, 8, 1),
(6, 8, 1),
(7, 9, 1),
(8, 10, 1),
(9, 11, 1),
(10, 12, 1),
(11, 13, 1),
(12, 14, 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `project`
--

CREATE TABLE `project` (
  `Id` int(11) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `project`
--

INSERT INTO `project` (`Id`, `Name`, `Description`) VALUES
(2, 'First project database', NULL),
(3, 'Second project database - up', NULL),
(4, 'Third project database- up', NULL),
(7, 'Project 1', ''),
(8, 'Project 2', ''),
(9, 'Project 3', ''),
(10, 'Project 4', ''),
(11, 'Project 5', ''),
(12, 'Project 6', ''),
(13, 'Project 7', ''),
(14, 'Project 8', ''),
(15, 'Project 9', ''),
(16, 'Project 10', ''),
(17, 'Project 11', ''),
(18, 'Project 12', ''),
(19, 'Project 13', ''),
(20, 'Project 14', ''),
(21, 'Project 15', ''),
(22, 'Project 16', '');

-- --------------------------------------------------------

--
-- Estrutura da tabela `projectuser`
--

CREATE TABLE `projectuser` (
  `Id` int(11) NOT NULL,
  `ProjectId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `projectuser`
--

INSERT INTO `projectuser` (`Id`, `ProjectId`, `UserId`) VALUES
(2, 2, 2),
(4, 4, 2),
(9, 4, 7),
(10, 14, 2),
(11, 14, 7),
(12, 14, 5),
(13, 13, 7),
(14, 13, 2),
(15, 2, 7);

-- --------------------------------------------------------

--
-- Estrutura da tabela `user`
--

CREATE TABLE `user` (
  `Id` int(11) NOT NULL,
  `Cpf` varchar(11) NOT NULL,
  `Login` varchar(100) DEFAULT NULL,
  `Password` varchar(200) DEFAULT NULL,
  `Name` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `user`
--

INSERT INTO `user` (`Id`, `Cpf`, `Login`, `Password`, `Name`, `Email`, `Role`) VALUES
(2, '11111111122', 'luby', '$2y$10$eCifsQ9Co5CPSnbnZqs61.Gy4GqEzSKV3GQWwd.vSmZlCZCjmVU9u', 'Luby - DEV 1', 'luby@luby.com', 2),
(5, '11111111123', 'lubyadm', '$2a$10$RkpIT66bKlNvCUI/nOHczezb7PLWW2AyTBQ50N.lb16jNUYVCwLdy', 'LUBY - DEV 2', 'luby@luby.com', 2),
(7, '113', 'luby3', '$2a$10$I24BjsLSubRmQ0yAuYVZI.N9joFwTAxAg0TSGD.6y2uNa6KrtYu3e', 'LUBY2', 'lubu@luby2', 2);

-- --------------------------------------------------------

--
-- Estrutura da tabela `workhour`
--

CREATE TABLE `workhour` (
  `Id` int(11) NOT NULL,
  `ProjectId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `TotalTime` decimal(10,2) DEFAULT NULL,
  `CreatedAt` datetime NOT NULL,
  `FinishedAt` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `workhour`
--

INSERT INTO `workhour` (`Id`, `ProjectId`, `UserId`, `TotalTime`, `CreatedAt`, `FinishedAt`) VALUES
(8, 2, 7, '4.00', '2021-01-13 09:00:16', '2021-01-13 13:00:36'),
(9, 2, 7, '4.17', '2021-01-13 09:00:16', '2021-01-13 13:10:36'),
(10, 2, 2, '0.00', '2021-01-13 09:00:16', '2021-01-13 13:00:36'),
(11, 2, 2, '4.50', '2021-01-13 09:30:24', '2021-01-13 14:00:24'),
(12, 14, 7, '7.33', '2021-01-15 13:00:00', '2021-01-15 20:20:00'),
(14, 14, 2, '15.00', '2021-01-15 08:23:00', '2021-01-15 23:23:00');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `notification`
--
ALTER TABLE `notification`
  ADD PRIMARY KEY (`Id`);

--
-- Índices para tabela `project`
--
ALTER TABLE `project`
  ADD PRIMARY KEY (`Id`);

--
-- Índices para tabela `projectuser`
--
ALTER TABLE `projectuser`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FK_ PROJECTUSER_PROJECT` (`ProjectId`),
  ADD KEY `FK_PROJECTUSER_USERID` (`UserId`);

--
-- Índices para tabela `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`Id`);

--
-- Índices para tabela `workhour`
--
ALTER TABLE `workhour`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FK_WORKHOUR_PROJECT` (`ProjectId`),
  ADD KEY `FK_WORKHOUR_USER` (`UserId`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `notification`
--
ALTER TABLE `notification`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de tabela `project`
--
ALTER TABLE `project`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT de tabela `projectuser`
--
ALTER TABLE `projectuser`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de tabela `user`
--
ALTER TABLE `user`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de tabela `workhour`
--
ALTER TABLE `workhour`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `projectuser`
--
ALTER TABLE `projectuser`
  ADD CONSTRAINT `FK_ PROJECTUSER_PROJECT` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_PROJECTUSER_USERID` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `workhour`
--
ALTER TABLE `workhour`
  ADD CONSTRAINT `FK_WORKHOUR_PROJECT` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_WORKHOUR_USER` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
