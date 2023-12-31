USE [master]
GO
/****** Object:  Database [MiniForum]    Script Date: 5/20/2023 11:18:12 AM ******/
CREATE DATABASE [MiniForum]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiniForum', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.LOCNGO\MSSQL\DATA\MiniForum.mdf' , SIZE = 8384KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiniForum_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.LOCNGO\MSSQL\DATA\MiniForum_log.ldf' , SIZE = 4288KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MiniForum] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniForum].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniForum] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiniForum] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiniForum] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiniForum] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiniForum] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiniForum] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MiniForum] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiniForum] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiniForum] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiniForum] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiniForum] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiniForum] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiniForum] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiniForum] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiniForum] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MiniForum] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiniForum] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiniForum] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiniForum] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiniForum] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiniForum] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiniForum] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiniForum] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MiniForum] SET  MULTI_USER 
GO
ALTER DATABASE [MiniForum] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiniForum] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiniForum] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiniForum] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MiniForum] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MiniForum]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 5/20/2023 11:18:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comment](
	[CmtID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
	[UserID] [char](5) NULL,
	[PostID] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CmtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FriendFollow]    Script Date: 5/20/2023 11:18:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FriendFollow](
	[UserID1] [char](5) NOT NULL,
	[UserID2] [char](5) NOT NULL,
 CONSTRAINT [PK_Friend] PRIMARY KEY CLUSTERED 
(
	[UserID1] ASC,
	[UserID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Like_Detail]    Script Date: 5/20/2023 11:18:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Like_Detail](
	[PostID] [int] NOT NULL,
	[UserID] [char](5) NOT NULL,
 CONSTRAINT [PK_Like_Detail] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Post]    Script Date: 5/20/2023 11:18:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](500) NULL,
	[Photo] [image] NULL,
	[UserID] [char](5) NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/20/2023 11:18:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [char](5) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Name] [varchar](10) NOT NULL,
	[PassWord] [varchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[Post] ([PostID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[FriendFollow]  WITH CHECK ADD  CONSTRAINT [FK_Table_User1] FOREIGN KEY([UserID2])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[FriendFollow] CHECK CONSTRAINT [FK_Table_User1]
GO
ALTER TABLE [dbo].[FriendFollow]  WITH CHECK ADD  CONSTRAINT [FK_Table_User2] FOREIGN KEY([UserID1])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[FriendFollow] CHECK CONSTRAINT [FK_Table_User2]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
USE [master]
GO
ALTER DATABASE [MiniForum] SET  READ_WRITE 
GO
