USE [master]
GO
/****** Object:  Database [DES_Restaurant]    Script Date: 22/05/2023 7:07:51 CH ******/
CREATE DATABASE [DES_Restaurant]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DES_Restaurant', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DES_Restaurant.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DES_Restaurant_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DES_Restaurant_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DES_Restaurant] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DES_Restaurant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DES_Restaurant] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DES_Restaurant] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DES_Restaurant] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DES_Restaurant] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DES_Restaurant] SET ARITHABORT OFF 
GO
ALTER DATABASE [DES_Restaurant] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DES_Restaurant] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DES_Restaurant] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DES_Restaurant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DES_Restaurant] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DES_Restaurant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DES_Restaurant] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DES_Restaurant] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DES_Restaurant] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DES_Restaurant] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DES_Restaurant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DES_Restaurant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DES_Restaurant] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DES_Restaurant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DES_Restaurant] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DES_Restaurant] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DES_Restaurant] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DES_Restaurant] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DES_Restaurant] SET  MULTI_USER 
GO
ALTER DATABASE [DES_Restaurant] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DES_Restaurant] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DES_Restaurant] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DES_Restaurant] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DES_Restaurant] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DES_Restaurant] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DES_Restaurant] SET QUERY_STORE = OFF
GO
USE [DES_Restaurant]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Category_ID] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Category_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Criteria]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Criteria](
	[Criteria_ID] [int] IDENTITY(1,1) NOT NULL,
	[Criteria_Name] [nvarchar](255) NULL,
	[Criteria_Weights] [float] NULL,
	[Criteria_Rank] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Criteria_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_ID] [varchar](10) NOT NULL,
	[Customer_Name] [nvarchar](50) NOT NULL,
	[Customer_Phone] [varchar](12) NOT NULL,
	[Customer_Password] [varchar](max) NULL,
	[Customer_Avatar] [varbinary](max) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Menu_ID] [int] IDENTITY(1,1) NOT NULL,
	[Menu_Time] [date] NULL,
	[Menu_Customer] [varchar](10) NOT NULL,
	[Menu_Name] [nvarchar](255) NULL,
	[Menu_CustomeCount] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Menu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuDetail]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuDetail](
	[MenuDetail_Item] [int] NOT NULL,
	[MenuDetail_MenuID] [int] NOT NULL,
 CONSTRAINT [PK_MenuDetail] PRIMARY KEY CLUSTERED 
(
	[MenuDetail_Item] ASC,
	[MenuDetail_MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[MenuItem_ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuItem_Name] [nvarchar](50) NOT NULL,
	[MenuItem_Image] [varbinary](max) NULL,
	[MenuItem_Price] [bigint] NOT NULL,
	[MenuItem_Calo] [float] NOT NULL,
	[MenuItem_Rating] [float] NOT NULL,
	[MenuItem_Speed] [int] NOT NULL,
	[MenuItem_Category] [int] NOT NULL,
	[MenuItem_Unit] [nvarchar](10) NULL,
	[MenuItem_isVegetarian] [bit] NULL,
	[MenuItems_Recipe] [nvarchar](max) NULL,
 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
(
	[MenuItem_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priority_criteria]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority_criteria](
	[Priority_criteria_1] [int] NOT NULL,
	[Priority_criteria_2] [int] NOT NULL,
	[Priority_criteria_Score] [float] NULL,
 CONSTRAINT [PK_Priority_criteria] PRIMARY KEY CLUSTERED 
(
	[Priority_criteria_1] ASC,
	[Priority_criteria_2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priority_criteria_forMenu]    Script Date: 22/05/2023 7:07:51 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority_criteria_forMenu](
	[Priority_criteria_ID] [int] NOT NULL,
	[Priority_criteria_forMenu_1] [int] NOT NULL,
	[Priority_criteria_forMenu_2] [int] NOT NULL,
	[Priority_criteria_forMenu_Score] [float] NULL,
 CONSTRAINT [PK_Priority_criteria_forMenu] PRIMARY KEY CLUSTERED 
(
	[Priority_criteria_ID] ASC,
	[Priority_criteria_forMenu_1] ASC,
	[Priority_criteria_forMenu_2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Menu_Time]  DEFAULT (getdate()) FOR [Menu_Time]
GO
ALTER TABLE [dbo].[MenuItems] ADD  CONSTRAINT [DF_MenuItems_MenuItem_Rating]  DEFAULT ((0)) FOR [MenuItem_Rating]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_MenuCustomer] FOREIGN KEY([Menu_Customer])
REFERENCES [dbo].[Customer] ([Customer_ID])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_MenuCustomer]
GO
ALTER TABLE [dbo].[MenuDetail]  WITH CHECK ADD  CONSTRAINT [FK_MenuDetail_Item] FOREIGN KEY([MenuDetail_Item])
REFERENCES [dbo].[MenuItems] ([MenuItem_ID])
GO
ALTER TABLE [dbo].[MenuDetail] CHECK CONSTRAINT [FK_MenuDetail_Item]
GO
ALTER TABLE [dbo].[MenuDetail]  WITH CHECK ADD  CONSTRAINT [FK_MenuDetail_Menu] FOREIGN KEY([MenuDetail_MenuID])
REFERENCES [dbo].[Menu] ([Menu_ID])
GO
ALTER TABLE [dbo].[MenuDetail] CHECK CONSTRAINT [FK_MenuDetail_Menu]
GO
ALTER TABLE [dbo].[MenuItems]  WITH CHECK ADD  CONSTRAINT [FK_Category] FOREIGN KEY([MenuItem_Category])
REFERENCES [dbo].[Category] ([Category_ID])
GO
ALTER TABLE [dbo].[MenuItems] CHECK CONSTRAINT [FK_Category]
GO
ALTER TABLE [dbo].[Priority_criteria]  WITH CHECK ADD  CONSTRAINT [FK_Priority_criteria_1] FOREIGN KEY([Priority_criteria_1])
REFERENCES [dbo].[Criteria] ([Criteria_ID])
GO
ALTER TABLE [dbo].[Priority_criteria] CHECK CONSTRAINT [FK_Priority_criteria_1]
GO
ALTER TABLE [dbo].[Priority_criteria]  WITH CHECK ADD  CONSTRAINT [FK_Priority_criteria_2] FOREIGN KEY([Priority_criteria_2])
REFERENCES [dbo].[Criteria] ([Criteria_ID])
GO
ALTER TABLE [dbo].[Priority_criteria] CHECK CONSTRAINT [FK_Priority_criteria_2]
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu]  WITH CHECK ADD  CONSTRAINT [FK_Priority_criteria_forMenu_1] FOREIGN KEY([Priority_criteria_forMenu_1])
REFERENCES [dbo].[MenuItems] ([MenuItem_ID])
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu] CHECK CONSTRAINT [FK_Priority_criteria_forMenu_1]
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu]  WITH CHECK ADD  CONSTRAINT [FK_Priority_criteria_forMenu_2] FOREIGN KEY([Priority_criteria_forMenu_2])
REFERENCES [dbo].[MenuItems] ([MenuItem_ID])
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu] CHECK CONSTRAINT [FK_Priority_criteria_forMenu_2]
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu]  WITH CHECK ADD  CONSTRAINT [FK_Priority_criteria_ID] FOREIGN KEY([Priority_criteria_ID])
REFERENCES [dbo].[Criteria] ([Criteria_ID])
GO
ALTER TABLE [dbo].[Priority_criteria_forMenu] CHECK CONSTRAINT [FK_Priority_criteria_ID]
GO
USE [master]
GO
ALTER DATABASE [DES_Restaurant] SET  READ_WRITE 
GO
