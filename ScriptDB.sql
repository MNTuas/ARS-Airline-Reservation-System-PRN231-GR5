use master

-- Create the database
CREATE DATABASE AirlinesReservationSystem;
GO

-- Use the created database
USE AirlinesReservationSystem;
GO

-- Table: Airlines
CREATE TABLE Airlines (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    Name NVARCHAR(100) NOT NULL
);
GO

-- Table: Airport
CREATE TABLE Airport (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) NOT NULL
);
GO

-- Table: Route
CREATE TABLE Route (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    [From] CHAR(36) NOT NULL,
    [To] CHAR(36) NOT NULL,
    FOREIGN KEY ([From]) REFERENCES Airport(Id),
    FOREIGN KEY ([To]) REFERENCES Airport(Id)
);
GO

-- Indexes on Route table
CREATE INDEX IDX_Route_From ON Route([From]);
CREATE INDEX IDX_Route_To ON Route([To]);
GO

-- Table: Airplane
CREATE TABLE Airplane (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    AvailableSeat INT NOT NULL,
    AirlinesId CHAR(36) NOT NULL,
    FOREIGN KEY (AirlinesId) REFERENCES Airlines(Id)
);
GO

-- Index on Airplane table
CREATE INDEX IDX_Airplane_AirlinesId ON Airplane(AirlinesId);
GO

-- Table: Rank
CREATE TABLE Rank (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Discount DECIMAL(5, 2) NULL
);
GO

-- Table: User
CREATE TABLE [User] (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    Avatar NVARCHAR(MAX) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Point INT NOT NULL,
    RankId CHAR(36) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (RankId) REFERENCES Rank(Id)
);
GO

-- Indexes on User table
CREATE INDEX IDX_User_Email ON [User](Email);
CREATE INDEX IDX_User_PhoneNumber ON [User](PhoneNumber);
CREATE INDEX IDX_User_RankId ON [User](RankId);
GO

-- Table: Passenger
CREATE TABLE Passenger (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Dob DATE NOT NULL,
    Country NVARCHAR(50) NOT NULL
);
GO

-- Table: Relatives
CREATE TABLE Relatives (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    UserId CHAR(36) NOT NULL,
    PassengerId CHAR(36) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id),
    FOREIGN KEY (PassengerId) REFERENCES Passenger(Id)
);
GO

-- Indexes on Relatives table
CREATE INDEX IDX_Relatives_UserId ON Relatives(UserId);
CREATE INDEX IDX_Relatives_PassengerId ON Relatives(PassengerId);
GO

-- Table: Flight
CREATE TABLE Flight (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    AirplaneId CHAR(36) NOT NULL,
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    RouteId CHAR(36) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Class NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (AirplaneId) REFERENCES Airplane(Id),
    FOREIGN KEY (RouteId) REFERENCES Route(Id)
);
GO

-- Indexes on Flight table
CREATE INDEX IDX_Flight_AirplaneId ON Flight(AirplaneId);
CREATE INDEX IDX_Flight_RouteId ON Flight(RouteId);
GO

-- Table: BookingInformation
CREATE TABLE BookingInformation (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    CreatedDate DATETIME NOT NULL,
    FlightId CHAR(36) NOT NULL,
    UserId CHAR(36) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (FlightId) REFERENCES Flight(Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
GO

-- Indexes on BookingInformation table
CREATE INDEX IDX_BookingInformation_FlightId ON BookingInformation(FlightId);
CREATE INDEX IDX_BookingInformation_UserId ON BookingInformation(UserId);
GO

-- Table: PassengerOfBooking
CREATE TABLE PassengerOfBooking (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    PassengerId CHAR(36) NOT NULL,
    BookingId CHAR(36) NOT NULL,
    FOREIGN KEY (PassengerId) REFERENCES Passenger(Id),
    FOREIGN KEY (BookingId) REFERENCES BookingInformation(Id)
);
GO

-- Indexes on PassengerOfBooking table
CREATE INDEX IDX_PassengerOfBooking_PassengerId ON PassengerOfBooking(PassengerId);
CREATE INDEX IDX_PassengerOfBooking_BookingId ON PassengerOfBooking(BookingId);
GO

-- Table: PaymentRecord
CREATE TABLE PaymentRecord (
    Id CHAR(36) PRIMARY KEY NOT NULL,
    BookingId CHAR(36) NOT NULL,
    UserId CHAR(36) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(5, 2) NULL,
    FinalPrice DECIMAL(10, 2) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    PayDate DATETIME NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES BookingInformation(Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
GO

-- Indexes on PaymentRecord table
CREATE INDEX IDX_PaymentRecord_BookingId ON PaymentRecord(BookingId);
CREATE INDEX IDX_PaymentRecord_UserId ON PaymentRecord(UserId);
GO
