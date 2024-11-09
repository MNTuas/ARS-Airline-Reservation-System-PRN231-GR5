USE [master]
GO
/****** Object:  Database [AirlinesReservationSystem]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE DATABASE [AirlinesReservationSystem]
GO
ALTER DATABASE [AirlinesReservationSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AirlinesReservationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AirlinesReservationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AirlinesReservationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AirlinesReservationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AirlinesReservationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AirlinesReservationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AirlinesReservationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AirlinesReservationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [AirlinesReservationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AirlinesReservationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AirlinesReservationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AirlinesReservationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AirlinesReservationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AirlinesReservationSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AirlinesReservationSystem] SET QUERY_STORE = OFF
GO
USE [AirlinesReservationSystem]
GO
/****** Object:  Table [dbo].[Airlines]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airlines](
	[Id] [char](36) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airplane]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airplane](
	[Id] [char](36) NOT NULL,
	[CodeNumber] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[AirlinesId] [char](36) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AirplaneSeat]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AirplaneSeat](
	[Id] [char](36) NOT NULL,
	[AirplaneId] [char](36) NOT NULL,
	[SeatClassId] [char](36) NOT NULL,
	[SeatCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[Id] [char](36) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingInformation]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingInformation](
	[Id] [char](36) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UserId] [char](36) NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[IsRefund] [bit] NULL,
	[CancelDate] [datetime] NULL,
 CONSTRAINT [PK__BookingI__3214EC07A2E4AB37] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[Id] [char](36) NOT NULL,
	[FlightNumber] [nvarchar](50) NOT NULL,
	[AirplaneId] [char](36) NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
	[Duration] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[From] [char](36) NOT NULL,
	[To] [char](36) NOT NULL,
 CONSTRAINT [PK__Flight__3214EC07A0A2C2BE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Passenger]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passenger](
	[Id] [char](36) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Dob] [date] NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[UserId] [char](36) NOT NULL,
 CONSTRAINT [PK__Passenge__3214EC074FD50069] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rank]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rank](
	[Id] [char](36) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Discount] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefundBankAccount]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefundBankAccount](
	[Id] [char](36) NOT NULL,
	[AccountName] [varchar](50) NOT NULL,
	[AccountNumber] [varchar](50) NOT NULL,
	[BankName] [varchar](50) NOT NULL,
	[BookingId] [char](36) NOT NULL,
 CONSTRAINT [PK_RefundBankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatClass]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatClass](
	[Id] [char](36) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [char](36) NOT NULL,
	[TicketClassId] [char](36) NOT NULL,
	[BookingId] [char](36) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Dob] [date] NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Ticket__3214EC0723F9FC1C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketClass]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketClass](
	[Id] [char](36) NOT NULL,
	[FlightId] [char](36) NOT NULL,
	[SeatClassId] [char](36) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[TotalSeat] [int] NOT NULL,
	[RemainSeat] [int] NOT NULL,
 CONSTRAINT [PK__TicketCl__3214EC07F0F9CC3D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [char](36) NOT NULL,
	[BookingId] [char](36) NOT NULL,
	[UserId] [char](36) NOT NULL,
	[FinalPrice] [decimal](18, 2) NOT NULL,
	[PayDate] [datetime] NULL,
	[Status] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Transact__3214EC072F6C7077] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/9/2024 1:19:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [char](36) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Point] [int] NULL,
	[RankId] [char](36) NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Status] [varchar](10) NOT NULL,
 CONSTRAINT [PK__User__3214EC07FFD92900] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Airlines] ([Id], [Name], [Code], [Status]) VALUES (N'53AAA70C-6185-4468-98E1-65ABAFC690B3', N'Vietnam Airlines', N'VN', 1)
INSERT [dbo].[Airlines] ([Id], [Name], [Code], [Status]) VALUES (N'f29d5d08-6ba8-4542-a277-020767ccf729', N'Vietjet Airs', N'VJ', 1)
GO
INSERT [dbo].[Airplane] ([Id], [CodeNumber], [Status], [AirlinesId]) VALUES (N'1ac5282e-bfef-4a18-ab73-d0748d99b396', N'VN1234', 1, N'53AAA70C-6185-4468-98E1-65ABAFC690B3')
INSERT [dbo].[Airplane] ([Id], [CodeNumber], [Status], [AirlinesId]) VALUES (N'2800e5df-ff60-4014-867d-d80e5ce45b75', N'VJ3679', 1, N'f29d5d08-6ba8-4542-a277-020767ccf729')
INSERT [dbo].[Airplane] ([Id], [CodeNumber], [Status], [AirlinesId]) VALUES (N'b4a8f9d9-610f-4b6b-8164-f84a8bce58cc', N'VN4567', 1, N'53AAA70C-6185-4468-98E1-65ABAFC690B3')
INSERT [dbo].[Airplane] ([Id], [CodeNumber], [Status], [AirlinesId]) VALUES (N'd0f3027f-61eb-482c-bb55-e73ccaff3ed7', N'VJ2345', 1, N'f29d5d08-6ba8-4542-a277-020767ccf729')
GO
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'0498bc27-3709-4310-b27f-a0480c2e5939', N'2800e5df-ff60-4014-867d-d80e5ce45b75', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', 50)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'19441262-4f8d-492a-8ad4-2c8c95b8787d', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', 100)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'29200cc0-e554-405a-b94d-bf9932b867fa', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', N'977A3036-5375-44EB-9A62-411F3861F767', 25)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'3951a8bb-ef9e-4451-8c4f-69b3940833e5', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', 50)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'3fa48048-1900-4115-bae4-3e49b232bb1b', N'b4a8f9d9-610f-4b6b-8164-f84a8bce58cc', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', 50)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'58796b56-41d5-4895-82d3-ea19b3dcecc7', N'2800e5df-ff60-4014-867d-d80e5ce45b75', N'977A3036-5375-44EB-9A62-411F3861F767', 25)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'749c7e56-b9e5-473e-a460-84c7bfc8cdfc', N'b4a8f9d9-610f-4b6b-8164-f84a8bce58cc', N'977A3036-5375-44EB-9A62-411F3861F767', 25)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'765c3262-387d-40eb-8b18-6209ecafc942', N'2800e5df-ff60-4014-867d-d80e5ce45b75', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', 100)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'a89181df-f58a-4dc1-9bfc-028d8b55f9d7', N'b4a8f9d9-610f-4b6b-8164-f84a8bce58cc', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', 100)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'ce275997-1936-4c7d-a70f-443f3323185d', N'd0f3027f-61eb-482c-bb55-e73ccaff3ed7', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', 100)
INSERT [dbo].[AirplaneSeat] ([Id], [AirplaneId], [SeatClassId], [SeatCount]) VALUES (N'e8a567fe-cbe0-478f-9845-00754e9915c7', N'd0f3027f-61eb-482c-bb55-e73ccaff3ed7', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', 50)
GO
INSERT [dbo].[Airport] ([Id], [Name], [City], [Country], [Status]) VALUES (N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'Noi Bai International Airport', N'Hanoi', N'Vietnam', 1)
INSERT [dbo].[Airport] ([Id], [Name], [City], [Country], [Status]) VALUES (N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496', N'Tan Son Nhat International Airport', N'Ho Chi Minh City', N'Vietnam', 1)
GO
INSERT [dbo].[BookingInformation] ([Id], [CreatedDate], [Quantity], [UserId], [Status], [IsRefund], [CancelDate]) VALUES (N'18833aa6-92f3-46ca-be7d-9da1be77580b', CAST(N'2024-11-09T08:50:48.763' AS DateTime), 1, N'bdcb9801-1377-470f-8dc0-77c13b718af0', N'Cancelled', 0, CAST(N'2024-11-09T11:00:05.893' AS DateTime))
INSERT [dbo].[BookingInformation] ([Id], [CreatedDate], [Quantity], [UserId], [Status], [IsRefund], [CancelDate]) VALUES (N'9a6eaef5-a2c6-458b-84fc-6c1024404ef7', CAST(N'2024-11-09T08:57:09.317' AS DateTime), 2, N'bdcb9801-1377-470f-8dc0-77c13b718af0', N'Cancelled', NULL, NULL)
GO
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'075ab0b2-140d-4608-868e-7aa790e75f41', N'VN809', N'b4a8f9d9-610f-4b6b-8164-f84a8bce58cc', CAST(N'2024-11-08T14:17:00.000' AS DateTime), CAST(N'2024-11-08T16:17:00.000' AS DateTime), 120, N'Arrived', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'11670398-ceae-4ee1-9657-188781686440', N'VN120', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', CAST(N'2024-11-14T21:50:00.000' AS DateTime), CAST(N'2024-11-15T01:10:00.000' AS DateTime), 200, N'Scheduled', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'6238bc67-1fb2-463e-9fd0-aeb014f18ef7', N'VN809', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', CAST(N'2024-11-09T06:00:00.000' AS DateTime), CAST(N'2024-11-09T08:00:00.000' AS DateTime), 120, N'Arrived', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'aea967c6-8161-4bb1-9961-7759df3c583a', N'VN505', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', CAST(N'2024-11-13T22:00:00.000' AS DateTime), CAST(N'2024-11-14T00:00:00.000' AS DateTime), 120, N'Scheduled', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'bd065be8-03d7-4674-898b-f4e7488c503d', N'VN878', N'1ac5282e-bfef-4a18-ab73-d0748d99b396', CAST(N'2024-11-15T22:00:00.000' AS DateTime), CAST(N'2024-11-16T00:00:00.000' AS DateTime), 120, N'Scheduled', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'd96f5943-2bae-43ba-88d4-784e7d753491', N'VJ678', N'd0f3027f-61eb-482c-bb55-e73ccaff3ed7', CAST(N'2024-11-10T21:30:00.000' AS DateTime), CAST(N'2024-11-10T23:30:00.000' AS DateTime), 120, N'Scheduled', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496')
INSERT [dbo].[Flight] ([Id], [FlightNumber], [AirplaneId], [DepartureTime], [ArrivalTime], [Duration], [Status], [From], [To]) VALUES (N'e3fbd3b9-089a-433a-9954-f1068a368ea3', N'VJ320', N'2800e5df-ff60-4014-867d-d80e5ce45b75', CAST(N'2024-11-14T22:30:00.000' AS DateTime), CAST(N'2024-11-15T00:30:00.000' AS DateTime), 120, N'Scheduled', N'ACE506C4-0489-4B6D-ADD0-5F5D24796E0F', N'F4E862F3-AA6C-4E90-9DD4-52F7703B9496')
GO
INSERT [dbo].[Passenger] ([Id], [FirstName], [LastName], [Gender], [Dob], [Country], [Type], [UserId]) VALUES (N'27a3608d-d0a1-4b26-ba4e-88d938355c77', N'test', N'3', N'Male', CAST(N'2000-01-05' AS Date), N'Việt Nam', N'Adult', N'bdcb9801-1377-470f-8dc0-77c13b718af0')
INSERT [dbo].[Passenger] ([Id], [FirstName], [LastName], [Gender], [Dob], [Country], [Type], [UserId]) VALUES (N'e8dfce5b-ceb9-4a42-8950-5c7d9a25d61b', N'Minh', N'Nhat', N'Male', CAST(N'2000-11-05' AS Date), N'Thái Lan', N'Adult', N'bdcb9801-1377-470f-8dc0-77c13b718af0')
GO
INSERT [dbo].[Rank] ([Id], [Type], [Description], [Discount]) VALUES (N'85788ACB-C23D-4F73-8E06-4E1DD4018B3C', N'Bronze', N'Bronze rank description', CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[Rank] ([Id], [Type], [Description], [Discount]) VALUES (N'A406102A-6723-4C7C-8973-5686AE87C989', N'Gold', N'Gold rank description', CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[Rank] ([Id], [Type], [Description], [Discount]) VALUES (N'F21A4D25-40CA-4002-A48C-AD2929E6C82C', N'Silver', N'Silver rank description', CAST(10.00 AS Decimal(5, 2)))
GO
INSERT [dbo].[RefundBankAccount] ([Id], [AccountName], [AccountNumber], [BankName], [BookingId]) VALUES (N'dfeb820d-a2d5-4cf9-bfce-0848ab64e849', N'test1', N'09817234', N'TPBank', N'18833aa6-92f3-46ca-be7d-9da1be77580b')
GO
INSERT [dbo].[SeatClass] ([Id], [Name], [Description], [Status]) VALUES (N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', N'Business', N'Business class seats with more amenities', 1)
INSERT [dbo].[SeatClass] ([Id], [Name], [Description], [Status]) VALUES (N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', N'Economy', N'Economy class seats', 1)
INSERT [dbo].[SeatClass] ([Id], [Name], [Description], [Status]) VALUES (N'977A3036-5375-44EB-9A62-411F3861F767', N'FirstClass', N'Premium first-class seats with luxurious amenities', 1)
GO
INSERT [dbo].[Ticket] ([Id], [TicketClassId], [BookingId], [FirstName], [LastName], [Gender], [Dob], [Country], [Status]) VALUES (N'0962ee52-c235-4975-a86c-de8ca8b7329b', N'720a4d22-2f25-42c6-ba1e-7a6749dfa9a4', N'9a6eaef5-a2c6-458b-84fc-6c1024404ef7', N'test', N'namer', N'Male', CAST(N'2000-02-03' AS Date), N'Vietnam', N'Cancelled')
INSERT [dbo].[Ticket] ([Id], [TicketClassId], [BookingId], [FirstName], [LastName], [Gender], [Dob], [Country], [Status]) VALUES (N'9dd6d08c-c304-4b8b-9f03-7f189c95a1c2', N'720a4d22-2f25-42c6-ba1e-7a6749dfa9a4', N'18833aa6-92f3-46ca-be7d-9da1be77580b', N'test', N'a', N'Male', CAST(N'2000-12-01' AS Date), N'Vietnam', N'Cancelled')
INSERT [dbo].[Ticket] ([Id], [TicketClassId], [BookingId], [FirstName], [LastName], [Gender], [Dob], [Country], [Status]) VALUES (N'd9e54714-87ca-4a40-bf9d-c6c293a4ca61', N'720a4d22-2f25-42c6-ba1e-7a6749dfa9a4', N'9a6eaef5-a2c6-458b-84fc-6c1024404ef7', N'Minh', N'Nhat', N'Male', CAST(N'2000-12-01' AS Date), N'Vietnam', N'Cancelled')
GO
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'07d0b6c6-7eb4-42ef-b9b3-324a81d9e9ee', N'e3fbd3b9-089a-433a-9954-f1068a368ea3', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(2000000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'0959d6d9-a20b-418f-9ea0-51f18e09c78b', N'075ab0b2-140d-4608-868e-7aa790e75f41', N'977A3036-5375-44EB-9A62-411F3861F767', CAST(5000000.00 AS Decimal(18, 2)), N'Available', 25, 25)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'0ccc26c4-bbd4-424a-a699-9ef56b417fea', N'bd065be8-03d7-4674-898b-f4e7488c503d', N'977A3036-5375-44EB-9A62-411F3861F767', CAST(5000000.00 AS Decimal(18, 2)), N'Available', 25, 25)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'0cd34fce-c013-4b23-bd9d-24840d7ea2de', N'd96f5943-2bae-43ba-88d4-784e7d753491', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(4000000.00 AS Decimal(18, 2)), N'Available', 50, 50)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'1ec3cff3-151c-4666-ae89-37c334102a52', N'6238bc67-1fb2-463e-9fd0-aeb014f18ef7', N'977A3036-5375-44EB-9A62-411F3861F767', CAST(3000000.00 AS Decimal(18, 2)), N'Available', 25, 25)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'3ab997b5-3f4d-4018-a803-044654d48a53', N'6238bc67-1fb2-463e-9fd0-aeb014f18ef7', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(2000000.00 AS Decimal(18, 2)), N'Available', 50, 50)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'42c35e70-1286-4ad0-80b6-28e3c38e7d84', N'11670398-ceae-4ee1-9657-188781686440', N'977A3036-5375-44EB-9A62-411F3861F767', CAST(5000000.00 AS Decimal(18, 2)), N'Available', 25, 25)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'44aa85d1-be46-4b19-9174-f2459478f5fc', N'd96f5943-2bae-43ba-88d4-784e7d753491', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(3200000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'720a4d22-2f25-42c6-ba1e-7a6749dfa9a4', N'e3fbd3b9-089a-433a-9954-f1068a368ea3', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(3000000.00 AS Decimal(18, 2)), N'Available', 50, 57)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'80349761-c515-4762-b538-0824dfeecd05', N'11670398-ceae-4ee1-9657-188781686440', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(3200000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'8a8d77fd-e61d-4873-bb6f-70e4f0ae7af2', N'bd065be8-03d7-4674-898b-f4e7488c503d', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(3200000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'8ffb59fb-75f4-40e2-8d26-56f60faaad37', N'bd065be8-03d7-4674-898b-f4e7488c503d', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(4000000.00 AS Decimal(18, 2)), N'Available', 50, 50)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'93395049-b0c4-4b2e-9943-d1de6222cc5f', N'e3fbd3b9-089a-433a-9954-f1068a368ea3', N'977A3036-5375-44EB-9A62-411F3861F767', CAST(5000000.00 AS Decimal(18, 2)), N'Available', 25, 25)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'a66d40bd-21f8-4186-9948-25b45ea2eab2', N'6238bc67-1fb2-463e-9fd0-aeb014f18ef7', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(1000000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'e5f737bf-f7cf-47a7-bb03-fd36ba4bc66a', N'075ab0b2-140d-4608-868e-7aa790e75f41', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(3200000.00 AS Decimal(18, 2)), N'Available', 50, 50)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'eb28b9b1-e74e-4475-b58a-3dee5fbe2bd6', N'075ab0b2-140d-4608-868e-7aa790e75f41', N'96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9', CAST(2000000.00 AS Decimal(18, 2)), N'Available', 100, 100)
INSERT [dbo].[TicketClass] ([Id], [FlightId], [SeatClassId], [Price], [Status], [TotalSeat], [RemainSeat]) VALUES (N'f809c824-72da-48ab-b537-f77a46642f31', N'11670398-ceae-4ee1-9657-188781686440', N'79562C9B-6B09-4CBF-B5A1-9903F2F15B67', CAST(4000000.00 AS Decimal(18, 2)), N'Available', 50, 50)
GO
INSERT [dbo].[Transaction] ([Id], [BookingId], [UserId], [FinalPrice], [PayDate], [Status], [CreatedDate]) VALUES (N'4dbcaa56-1eac-4ab0-bc54-a24801ef764c', N'18833aa6-92f3-46ca-be7d-9da1be77580b', N'bdcb9801-1377-470f-8dc0-77c13b718af0', CAST(2850000.00 AS Decimal(18, 2)), CAST(N'2024-11-09T08:51:34.337' AS DateTime), N'Paid', CAST(N'2024-11-09T08:50:57.240' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'4f0eb13e-13b1-4e60-8de7-c6ed3af5f589', N'string', N'phuhoang', N'0123456789', N'phuhdmse171637@fpt.edu.vn', N'$2a$11$haUs2f/hqhMt0qcujz953uH960HVf6c7VHogRsncZ.yqcS1PtY98u', N'string', 0, N'85788ACB-C23D-4F73-8E06-4E1DD4018B3C', N'User', N'Inactive')
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'5fb30871-0408-49e2-ab51-1d57d75f4c7c', NULL, N'abc', NULL, N'abcstaff01@gmail.com', N'$2a$11$e8sjRBe2n8GD2sJgdpC6oeuySpRoZRSpcZWut9w3zpf4Jbff97Fq6', NULL, NULL, NULL, N'Staff', N'Active')
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'70213829-9f5f-4d1a-a3d6-6401ad750ec2', NULL, N'admin', NULL, N'admin@gmail.com', N'$2a$11$tKMLlKoFNjyidYyffnAH5.BzgcUqpJ2LBOvr9jV4bt3MFMTP//Qn2', NULL, NULL, NULL, N'Admin', N'Active')
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'7bcebc4b-5e28-4c2a-82f8-013306e76350', NULL, N'abcstaff', NULL, N'abcstaff@gmail.com', N'$2a$11$4Wve8QYMQjdqXbX.kO6iie1u02hs/cW0v4cwjjp6JpRkRZ0p8Fvgm', NULL, NULL, NULL, N'Staff', N'Active')
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'bdcb9801-1377-470f-8dc0-77c13b718af0', NULL, N'phuhoang', N'0324532521', N'phu@gmail.com', N'$2a$11$6fjsBN.maR2XKiv3oyfZtOMwBAknMrZi/55S7ZOktUPWIjYG9r6ji', N'123 go vap', 0, N'85788ACB-C23D-4F73-8E06-4E1DD4018B3C', N'User', N'Active')
INSERT [dbo].[User] ([Id], [Avatar], [Name], [PhoneNumber], [Email], [Password], [Address], [Point], [RankId], [Role], [Status]) VALUES (N'ea04dcf9-ccbb-4a7b-b895-3ba3d97f707b', NULL, N'staff', NULL, N'staff@gmail.com', N'$2a$11$y6pP93RzdQCHqEUErouayO9YH4LXZZ5rtpfd31jaGTo8WLchXtBvS', NULL, NULL, NULL, N'Staff', N'Active')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Airplane_Airlines]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Airplane_Airlines] ON [dbo].[Airplane]
(
	[AirlinesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_AirplaneSeat_Airplane]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_AirplaneSeat_Airplane] ON [dbo].[AirplaneSeat]
(
	[AirplaneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_AirplaneSeat_SeatClass]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_AirplaneSeat_SeatClass] ON [dbo].[AirplaneSeat]
(
	[SeatClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_BookingInformation_User]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_BookingInformation_User] ON [dbo].[BookingInformation]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Flight_Airplane]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Flight_Airplane] ON [dbo].[Flight]
(
	[AirplaneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Flight_Airport_From]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Flight_Airport_From] ON [dbo].[Flight]
(
	[From] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Flight_Airport_To]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Flight_Airport_To] ON [dbo].[Flight]
(
	[To] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Passenger_User]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Passenger_User] ON [dbo].[Passenger]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Ticket_BookingInformation]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Ticket_BookingInformation] ON [dbo].[Ticket]
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Ticket_TicketClass]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Ticket_TicketClass] ON [dbo].[Ticket]
(
	[TicketClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_TicketClass_Flight]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_TicketClass_Flight] ON [dbo].[TicketClass]
(
	[FlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_TicketClass_SeatClass]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_TicketClass_SeatClass] ON [dbo].[TicketClass]
(
	[SeatClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Transaction_BookingInformation]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Transaction_BookingInformation] ON [dbo].[Transaction]
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_Transaction_User]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_Transaction_User] ON [dbo].[Transaction]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_FK_User_Rank]    Script Date: 11/9/2024 1:19:39 PM ******/
CREATE NONCLUSTERED INDEX [IDX_FK_User_Rank] ON [dbo].[User]
(
	[RankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Airplane]  WITH CHECK ADD  CONSTRAINT [FK_Airplane_Airlines] FOREIGN KEY([AirlinesId])
REFERENCES [dbo].[Airlines] ([Id])
GO
ALTER TABLE [dbo].[Airplane] CHECK CONSTRAINT [FK_Airplane_Airlines]
GO
ALTER TABLE [dbo].[AirplaneSeat]  WITH CHECK ADD  CONSTRAINT [FK_AirplaneSeat_Airplane] FOREIGN KEY([AirplaneId])
REFERENCES [dbo].[Airplane] ([Id])
GO
ALTER TABLE [dbo].[AirplaneSeat] CHECK CONSTRAINT [FK_AirplaneSeat_Airplane]
GO
ALTER TABLE [dbo].[AirplaneSeat]  WITH CHECK ADD  CONSTRAINT [FK_AirplaneSeat_SeatClass] FOREIGN KEY([SeatClassId])
REFERENCES [dbo].[SeatClass] ([Id])
GO
ALTER TABLE [dbo].[AirplaneSeat] CHECK CONSTRAINT [FK_AirplaneSeat_SeatClass]
GO
ALTER TABLE [dbo].[BookingInformation]  WITH CHECK ADD  CONSTRAINT [FK_BookingInformation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BookingInformation] CHECK CONSTRAINT [FK_BookingInformation_User]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Airplane] FOREIGN KEY([AirplaneId])
REFERENCES [dbo].[Airplane] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Airplane]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Airport_From] FOREIGN KEY([From])
REFERENCES [dbo].[Airport] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Airport_From]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Airport_To] FOREIGN KEY([To])
REFERENCES [dbo].[Airport] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Airport_To]
GO
ALTER TABLE [dbo].[Passenger]  WITH CHECK ADD  CONSTRAINT [FK_Passenger_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Passenger] CHECK CONSTRAINT [FK_Passenger_User]
GO
ALTER TABLE [dbo].[RefundBankAccount]  WITH CHECK ADD  CONSTRAINT [FK_RefundBankAccount_BookingInformation] FOREIGN KEY([BookingId])
REFERENCES [dbo].[BookingInformation] ([Id])
GO
ALTER TABLE [dbo].[RefundBankAccount] CHECK CONSTRAINT [FK_RefundBankAccount_BookingInformation]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_BookingInformation] FOREIGN KEY([BookingId])
REFERENCES [dbo].[BookingInformation] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_BookingInformation]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_TicketClass] FOREIGN KEY([TicketClassId])
REFERENCES [dbo].[TicketClass] ([Id])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_TicketClass]
GO
ALTER TABLE [dbo].[TicketClass]  WITH CHECK ADD  CONSTRAINT [FK_TicketClass_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([Id])
GO
ALTER TABLE [dbo].[TicketClass] CHECK CONSTRAINT [FK_TicketClass_Flight]
GO
ALTER TABLE [dbo].[TicketClass]  WITH CHECK ADD  CONSTRAINT [FK_TicketClass_SeatClass] FOREIGN KEY([SeatClassId])
REFERENCES [dbo].[SeatClass] ([Id])
GO
ALTER TABLE [dbo].[TicketClass] CHECK CONSTRAINT [FK_TicketClass_SeatClass]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_BookingInformation] FOREIGN KEY([BookingId])
REFERENCES [dbo].[BookingInformation] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_BookingInformation]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Rank] FOREIGN KEY([RankId])
REFERENCES [dbo].[Rank] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Rank]
GO
USE [master]
GO
ALTER DATABASE [AirlinesReservationSystem] SET  READ_WRITE 
GO
