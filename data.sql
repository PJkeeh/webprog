USE [master]
GO
/****** Object:  Database [webprog]    Script Date: 4/04/2016 23:51:51 ******/
CREATE DATABASE [webprog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'webprog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\webprog.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'webprog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\webprog_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [webprog] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [webprog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [webprog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [webprog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [webprog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [webprog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [webprog] SET ARITHABORT OFF 
GO
ALTER DATABASE [webprog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [webprog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [webprog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [webprog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [webprog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [webprog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [webprog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [webprog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [webprog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [webprog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [webprog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [webprog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [webprog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [webprog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [webprog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [webprog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [webprog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [webprog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [webprog] SET  MULTI_USER 
GO
ALTER DATABASE [webprog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [webprog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [webprog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [webprog] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [webprog] SET DELAYED_DURABILITY = DISABLED 
GO
USE [webprog]
GO
/****** Object:  Table [dbo].[login]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[login](
	[login] [nchar](50) NOT NULL,
	[password] [nchar](255) NOT NULL,
 CONSTRAINT [PK_login] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[match]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[match](
	[match_id] [numeric](18, 0) NOT NULL,
	[match_hometeam_id] [numeric](18, 0) NOT NULL,
	[match_date] [date] NOT NULL,
	[match_awayteam_id] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_match] PRIMARY KEY CLUSTERED 
(
	[match_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stadion]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stadion](
	[stadion_id] [numeric](18, 0) NOT NULL,
	[stadion_name] [nchar](50) NOT NULL,
	[stadion_description] [text] NULL,
 CONSTRAINT [PK_stadion] PRIMARY KEY CLUSTERED 
(
	[stadion_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[team]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[team](
	[team_id] [numeric](18, 0) NOT NULL,
	[team_name] [nchar](50) NOT NULL,
	[team_description] [text] NULL,
	[team_stadion_id] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_club] PRIMARY KEY CLUSTERED 
(
	[team_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticket]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticket](
	[ticket_id] [numeric](18, 0) NOT NULL,
	[ticket_type] [numeric](18, 0) NOT NULL,
	[match_id] [numeric](18, 0) NOT NULL,
	[login] [nchar](50) NOT NULL,
 CONSTRAINT [PK_ticket] PRIMARY KEY CLUSTERED 
(
	[ticket_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticket_team]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticket_team](
	[ticket_type_id] [numeric](18, 0) NOT NULL,
	[team_id] [numeric](18, 0) NOT NULL,
	[ticket_price] [real] NOT NULL,
	[ticket_amount] [numeric](18, 0) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticket_type]    Script Date: 4/04/2016 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticket_type](
	[ticket_type_id] [numeric](18, 0) NOT NULL,
	[ticket_type_name] [nchar](255) NOT NULL,
	[ticket_type_hometeam] [bit] NOT NULL,
 CONSTRAINT [PK_ticket_type] PRIMARY KEY CLUSTERED 
(
	[ticket_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[login] ([login], [password]) VALUES (N'Pieter                                            ', N'40BD001563085FC35165329EA1FF5C5ECBDBBEEF                                                                                                                                                                                                                       ')
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(N'2016-03-19' AS Date), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(N'2016-03-20' AS Date), CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(N'2016-03-19' AS Date), CAST(3 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(N'2016-03-26' AS Date), CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(N'2016-03-27' AS Date), CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), CAST(N'2016-04-02' AS Date), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[match] ([match_id], [match_hometeam_id], [match_date], [match_awayteam_id]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(N'2016-04-03' AS Date), CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[stadion] ([stadion_id], [stadion_name], [stadion_description]) VALUES (CAST(0 AS Numeric(18, 0)), N'Stadion Name 0                                    ', N'Stadion Description 0')
INSERT [dbo].[stadion] ([stadion_id], [stadion_name], [stadion_description]) VALUES (CAST(1 AS Numeric(18, 0)), N'Stadion Name 1                                    ', N'Stadion Description 1')
INSERT [dbo].[stadion] ([stadion_id], [stadion_name], [stadion_description]) VALUES (CAST(2 AS Numeric(18, 0)), N'Stadion Name 2                                    ', N'Stadion Description  2')
INSERT [dbo].[team] ([team_id], [team_name], [team_description], [team_stadion_id]) VALUES (CAST(0 AS Numeric(18, 0)), N'Ploeg 0                                           ', N'Ploeg 0 description', CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[team] ([team_id], [team_name], [team_description], [team_stadion_id]) VALUES (CAST(1 AS Numeric(18, 0)), N'Ploeg 1                                           ', N'Ploeg 1 description', CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[team] ([team_id], [team_name], [team_description], [team_stadion_id]) VALUES (CAST(2 AS Numeric(18, 0)), N'Ploeg 2                                           ', N'Ploeg 2 description', CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[team] ([team_id], [team_name], [team_description], [team_stadion_id]) VALUES (CAST(3 AS Numeric(18, 0)), N'Ploeg 3                                           ', N'Ploeg 3 description', CAST(0 AS Numeric(18, 0)))
INSERT [dbo].[team] ([team_id], [team_name], [team_description], [team_stadion_id]) VALUES (CAST(4 AS Numeric(18, 0)), N'Ploeg 4                                           ', N'Ploeg 4 description', CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[ticket] ([ticket_id], [ticket_type], [match_id], [login]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'pieter                                            ')
INSERT [dbo].[ticket] ([ticket_id], [ticket_type], [match_id], [login]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'pieter                                            ')
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 9.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 9.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 9.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 9.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 19.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 19.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 19.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(0 AS Numeric(18, 0)), 19.99, CAST(1000 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 8.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 8.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 8.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 8.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 18.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 18.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 18.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), 18.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 5.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 5.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 5.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 5.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 15.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 15.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 15.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), 15.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 9.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 9.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 9.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 9.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 19.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 19.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 19.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), 19.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(0 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 10.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 10.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 10.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 10.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 29.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 29.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 29.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_team] ([ticket_type_id], [team_id], [ticket_price], [ticket_amount]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), 29.99, CAST(500 AS Numeric(18, 0)))
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(0 AS Numeric(18, 0)), N'Onderste ring achter het doel                                                                                                                                                                                                                                  ', 1)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(1 AS Numeric(18, 0)), N'Onderste ring achter het doel                                                                                                                                                                                                                                  ', 0)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(2 AS Numeric(18, 0)), N'Onderste ring zijlijn Oost                                                                                                                                                                                                                                     ', 1)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(3 AS Numeric(18, 0)), N'Onderste ring zijlijn West                                                                                                                                                                                                                                     ', 1)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(4 AS Numeric(18, 0)), N'Bovenste ring achter het doel                                                                                                                                                                                                                                  ', 1)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(5 AS Numeric(18, 0)), N'Bovenste ring achter het doel                                                                                                                                                                                                                                  ', 0)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(6 AS Numeric(18, 0)), N'Bovenste ring zijlijn Oost                                                                                                                                                                                                                                     ', 1)
INSERT [dbo].[ticket_type] ([ticket_type_id], [ticket_type_name], [ticket_type_hometeam]) VALUES (CAST(7 AS Numeric(18, 0)), N'Bovenste ring zijlijn West                                                                                                                                                                                                                                     ', 1)
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_away_team] FOREIGN KEY([match_hometeam_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_away_team]
GO
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_home_team] FOREIGN KEY([match_awayteam_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_home_team]
GO
ALTER TABLE [dbo].[team]  WITH CHECK ADD  CONSTRAINT [FK_team_stadion] FOREIGN KEY([team_stadion_id])
REFERENCES [dbo].[stadion] ([stadion_id])
GO
ALTER TABLE [dbo].[team] CHECK CONSTRAINT [FK_team_stadion]
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD  CONSTRAINT [FK_ticket_match] FOREIGN KEY([match_id])
REFERENCES [dbo].[match] ([match_id])
GO
ALTER TABLE [dbo].[ticket] CHECK CONSTRAINT [FK_ticket_match]
GO
ALTER TABLE [dbo].[ticket]  WITH CHECK ADD  CONSTRAINT [FK_ticket_ticket] FOREIGN KEY([ticket_id])
REFERENCES [dbo].[ticket] ([ticket_id])
GO
ALTER TABLE [dbo].[ticket] CHECK CONSTRAINT [FK_ticket_ticket]
GO
ALTER TABLE [dbo].[ticket_team]  WITH CHECK ADD  CONSTRAINT [FK_ticket_team_team] FOREIGN KEY([team_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[ticket_team] CHECK CONSTRAINT [FK_ticket_team_team]
GO
ALTER TABLE [dbo].[ticket_team]  WITH CHECK ADD  CONSTRAINT [FK_ticket_team_ticket_type] FOREIGN KEY([ticket_type_id])
REFERENCES [dbo].[ticket_type] ([ticket_type_id])
GO
ALTER TABLE [dbo].[ticket_team] CHECK CONSTRAINT [FK_ticket_team_ticket_type]
GO
USE [master]
GO
ALTER DATABASE [webprog] SET  READ_WRITE 
GO
