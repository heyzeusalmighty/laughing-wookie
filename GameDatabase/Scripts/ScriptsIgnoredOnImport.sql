
USE [master]
GO

/****** Object:  Database [Eclipse]    Script Date: 3/18/2015 11:20:38 AM ******/
CREATE DATABASE [Eclipse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Eclipse', FILENAME = N'C:\Users\thomas.lombardi\Eclipse.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Eclipse_log', FILENAME = N'C:\Users\thomas.lombardi\Eclipse_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Eclipse] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Eclipse].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Eclipse] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [Eclipse] SET ANSI_NULLS OFF
GO

ALTER DATABASE [Eclipse] SET ANSI_PADDING OFF
GO

ALTER DATABASE [Eclipse] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [Eclipse] SET ARITHABORT OFF
GO

ALTER DATABASE [Eclipse] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [Eclipse] SET AUTO_CREATE_STATISTICS ON
GO

ALTER DATABASE [Eclipse] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [Eclipse] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [Eclipse] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [Eclipse] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [Eclipse] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [Eclipse] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [Eclipse] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [Eclipse] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [Eclipse] SET  DISABLE_BROKER
GO

ALTER DATABASE [Eclipse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [Eclipse] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [Eclipse] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [Eclipse] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [Eclipse] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [Eclipse] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [Eclipse] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [Eclipse] SET RECOVERY SIMPLE
GO

ALTER DATABASE [Eclipse] SET  MULTI_USER
GO

ALTER DATABASE [Eclipse] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [Eclipse] SET DB_CHAINING OFF
GO

ALTER DATABASE [Eclipse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO

ALTER DATABASE [Eclipse] SET TARGET_RECOVERY_TIME = 0 SECONDS
GO

USE [Eclipse]
GO

/****** Object:  Table [dbo].[Game]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[GameUsers]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[MapDeck]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Player]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[PlayerShipModel]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[PlayerTrack]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ScienceTrack]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[ShipModelComponents]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Tiles]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Turns]    Script Date: 3/18/2015 11:20:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[Game] ON
GO

INSERT [dbo].[Game] ([GameId], [Round], [Status], [CurrentPlayer], [GameIdentifier]) VALUES (1, 0, N'CREATED', NULL, N'35646b3e-d359-44c2-bd79-1b2f55e4c549')
GO

INSERT [dbo].[Game] ([GameId], [Round], [Status], [CurrentPlayer], [GameIdentifier]) VALUES (2, 0, N'CREATED', NULL, N'2b617c88-a33b-472b-9448-6c2735a36240')
GO

INSERT [dbo].[Game] ([GameId], [Round], [Status], [CurrentPlayer], [GameIdentifier]) VALUES (1002, 0, N'CREATED', NULL, N'239d4ec4-1cd8-4beb-af88-6dbd9f361824')
GO

SET IDENTITY_INSERT [dbo].[Game] OFF
GO

SET IDENTITY_INSERT [dbo].[GameUsers] ON
GO

INSERT [dbo].[GameUsers] ([UserId], [UserName], [EmailAddress]) VALUES (1, N'AbeLincoln', N'x@x.com')
GO

INSERT [dbo].[GameUsers] ([UserId], [UserName], [EmailAddress]) VALUES (2, N'TommyJefferson', N'x@x.com')
GO

INSERT [dbo].[GameUsers] ([UserId], [UserName], [EmailAddress]) VALUES (3, N'TeddyRoooo', N'x@x.com')
GO

INSERT [dbo].[GameUsers] ([UserId], [UserName], [EmailAddress]) VALUES (4, N'GeorgieWashington', N'x@x.com')
GO

SET IDENTITY_INSERT [dbo].[GameUsers] OFF
GO

SET IDENTITY_INSERT [dbo].[MapDeck] ON
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (1, 5, 1, 1, 1, 6, 4, N'Aliens', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (2, 6, 1, 2, 1, 8, 4, N'Aliens', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (3, 1, 1, 3, 0, NULL, NULL, N'Aliens', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (4, 2, 1, 4, 0, NULL, NULL, N'Aliens', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (5, 8, 1, 5, 0, NULL, NULL, N'', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (6, 7, 1, 6, 0, NULL, NULL, N'', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (7, 4, 1, 7, 0, NULL, NULL, N'', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (8, 3, 1, 8, 0, NULL, NULL, N'', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (9, 666, 2, 0, 1, 7, 3, N'Black', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (10, 666, 2, 0, 1, 8, 5, N'Green', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (11, 666, 2, 0, 1, 4, 5, N'White', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (12, 666, 2, 0, 1, 7, 6, N'Blue', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (13, 9, 2, 1, 1, 6, 7, N'', 1002)
GO

INSERT [dbo].[MapDeck] ([MapDeckId], [MapId], [Division], [SortOrder], [Revealed], [XCoords], [YCoords], [Occupied], [GameId]) VALUES (14, 10, 2, 2, 0, NULL, NULL, N'Aliens', 1002)
GO

SET IDENTITY_INSERT [dbo].[MapDeck] OFF
GO

SET IDENTITY_INSERT [dbo].[Player] ON
GO

INSERT [dbo].[Player] ([PlayerId], [Username], [GameId], [DiscColor], [CurrentOrange], [CurrentBrown], [CurrentPink], [OrangeIncome], [BrownIncome], [PinkIncome], [Pass], [TurnOrder], [TotalDiscs], [AvailableDiscs], [UserId]) VALUES (1, N'AbeLincoln', 1002, N'Black', 2, 3, 3, 0, 0, 0, 0, 0, 8, 6, 1)
GO

INSERT [dbo].[Player] ([PlayerId], [Username], [GameId], [DiscColor], [CurrentOrange], [CurrentBrown], [CurrentPink], [OrangeIncome], [BrownIncome], [PinkIncome], [Pass], [TurnOrder], [TotalDiscs], [AvailableDiscs], [UserId]) VALUES (2, N'TommyJefferson', 1002, N'Green', 2, 3, 3, 0, 0, 0, 0, 0, 8, 6, 2)
GO

INSERT [dbo].[Player] ([PlayerId], [Username], [GameId], [DiscColor], [CurrentOrange], [CurrentBrown], [CurrentPink], [OrangeIncome], [BrownIncome], [PinkIncome], [Pass], [TurnOrder], [TotalDiscs], [AvailableDiscs], [UserId]) VALUES (3, N'TeddyRoooo', 1002, N'White', 2, 3, 3, 0, 0, 0, 0, 0, 8, 6, 3)
GO

INSERT [dbo].[Player] ([PlayerId], [Username], [GameId], [DiscColor], [CurrentOrange], [CurrentBrown], [CurrentPink], [OrangeIncome], [BrownIncome], [PinkIncome], [Pass], [TurnOrder], [TotalDiscs], [AvailableDiscs], [UserId]) VALUES (4, N'GeorgieWashington', 1002, N'Blue', 2, 3, 3, 0, 0, 0, 0, 0, 8, 6, 4)
GO

SET IDENTITY_INSERT [dbo].[Player] OFF
GO

SET IDENTITY_INSERT [dbo].[PlayerShipModel] ON
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (1, N'interceptor', 7)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (2, N'interceptor', 8)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (3, N'interceptor', 9)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (4, N'interceptor', 10)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (5, N'interceptor', 1)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (6, N'interceptor', 2)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (7, N'interceptor', 3)
GO

INSERT [dbo].[PlayerShipModel] ([ModelId], [ModelName], [PlayerId]) VALUES (8, N'interceptor', 4)
GO

SET IDENTITY_INSERT [dbo].[PlayerShipModel] OFF
GO

SET IDENTITY_INSERT [dbo].[PlayerTrack] ON
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (1, 7, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (2, 8, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (3, 9, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (4, 10, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (5, 1, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (6, 2, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (7, 3, N'Star', 1, 1)
GO

INSERT [dbo].[PlayerTrack] ([TrackId], [PlayerId], [Track], [TileId], [Position]) VALUES (8, 4, N'Star', 1, 1)
GO

SET IDENTITY_INSERT [dbo].[PlayerTrack] OFF
GO

SET IDENTITY_INSERT [dbo].[ShipModelComponents] ON
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (1, 4, 1)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (2, 5, 1)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (3, 1, 1)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (4, 4, 2)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (5, 5, 2)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (6, 1, 2)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (7, 4, 3)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (8, 5, 3)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (9, 1, 3)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (10, 4, 4)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (11, 5, 4)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (12, 1, 4)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (13, 4, 5)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (14, 5, 5)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (15, 1, 5)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (16, 4, 6)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (17, 5, 6)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (18, 1, 6)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (19, 4, 7)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (20, 5, 7)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (21, 1, 7)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (22, 4, 8)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (23, 5, 8)
GO

INSERT [dbo].[ShipModelComponents] ([ComId], [ComponentId], [ShipId]) VALUES (24, 1, 8)
GO

SET IDENTITY_INSERT [dbo].[ShipModelComponents] OFF
GO

USE [master]
GO

ALTER DATABASE [Eclipse] SET  READ_WRITE
GO
