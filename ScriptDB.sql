USE master
-- Create the AirlinesReservationSystem database
CREATE DATABASE AirlinesReservationSystem;
GO

-- Use the AirlinesReservationSystem database
USE AirlinesReservationSystem;
GO

-- Create Airlines table
CREATE TABLE Airlines (
    Id CHAR(36) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);
GO

-- Create Airport table
CREATE TABLE Airport (
    Id CHAR(36) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
    Country NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50) NOT NULL
);
GO

-- Create Route table
CREATE TABLE Route (
    Id CHAR(36) PRIMARY KEY,
    [From] CHAR(36) NOT NULL, -- Foreign key to Airport.Id
    [To] CHAR(36) NOT NULL,   -- Foreign key to Airport.Id
    FOREIGN KEY ([From]) REFERENCES Airport(Id),
    FOREIGN KEY ([To]) REFERENCES Airport(Id)
);
GO

-- Create Airplane table
CREATE TABLE Airplane (
    Id CHAR(36) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL,
    Type NVARCHAR(255) NOT NULL,
    AvailableSeat INT NOT NULL,
    AirlinesId CHAR(36) NOT NULL, -- Foreign key to Airlines.Id
    FOREIGN KEY (AirlinesId) REFERENCES Airlines(Id)
);
GO

-- Create Rank table
CREATE TABLE Rank (
    Id CHAR(36) PRIMARY KEY,
    Type NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    Discount FLOAT
);
GO

-- Create User table
CREATE TABLE [User] (
    Id CHAR(36) PRIMARY KEY,
    Avatar NVARCHAR(255),
    Name NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Point INT NOT NULL,
    RankId CHAR(36) NOT NULL, -- Foreign key to Rank.Id
    Role NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (RankId) REFERENCES Rank(Id)
);
GO

-- Create Passenger table
CREATE TABLE Passenger (
    Id CHAR(36) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(50) NOT NULL,
    Dob DATE NOT NULL,
    Country NVARCHAR(255) NOT NULL,
    Type NVARCHAR(50) NOT NULL
);
GO

-- Create Relatives table
CREATE TABLE Relatives (
    Id CHAR(36) PRIMARY KEY,
    UserId CHAR(36) NOT NULL, -- Foreign key to User.Id
    PassengerId CHAR(36) NOT NULL, -- Foreign key to Passenger.Id
    FOREIGN KEY (UserId) REFERENCES [User](Id),
    FOREIGN KEY (PassengerId) REFERENCES Passenger(Id)
);
GO

-- Create Flight table
CREATE TABLE Flight (
    Id CHAR(36) PRIMARY KEY,
    AirplaneId CHAR(36) NOT NULL, -- Foreign key to Airplane.Id
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    RouteId CHAR(36) NOT NULL, -- Foreign key to Route.Id
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (AirplaneId) REFERENCES Airplane(Id),
    FOREIGN KEY (RouteId) REFERENCES Route(Id)
);
GO

-- Create FlightClass table
CREATE TABLE FlightClass (
    Id CHAR(36) PRIMARY KEY,
    FlightId CHAR(36) NOT NULL, -- Foreign key to Flight.Id
    Class NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (FlightId) REFERENCES Flight(Id)
);
GO

-- Create BookingInformation table
CREATE TABLE BookingInformation (
    Id CHAR(36) PRIMARY KEY,
    FlightId CHAR(36) NOT NULL, -- Foreign key to Flight.Id
    CreatedDate DATE NOT NULL,
    FlightClassId CHAR(36) NOT NULL, -- Foreign key to FlightClass.Id
    UserId CHAR(36) NOT NULL, -- Foreign key to User.Id
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (FlightId) REFERENCES Flight(Id),
    FOREIGN KEY (FlightClassId) REFERENCES FlightClass(Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
GO

-- Create PassengerOfBooking table
CREATE TABLE PassengerOfBooking (
    Id CHAR(36) PRIMARY KEY,
    PassengerId CHAR(36) NOT NULL, -- Foreign key to Passenger.Id
    BookingId CHAR(36) NOT NULL, -- Foreign key to BookingInformation.Id
    FOREIGN KEY (PassengerId) REFERENCES Passenger(Id),
    FOREIGN KEY (BookingId) REFERENCES BookingInformation(Id)
);
GO

-- Create PaymentRecord table
CREATE TABLE PaymentRecord (
    Id CHAR(36) PRIMARY KEY,
    BookingId CHAR(36) NOT NULL, -- Foreign key to BookingInformation.Id
    UserId CHAR(36) NOT NULL, -- Foreign key to User.Id
    Price DECIMAL(18,2) NOT NULL,
    Discount FLOAT,
    FinalPrice DECIMAL(18,2) NOT NULL,
    CreatedDate DATE NOT NULL,
    PayDate DATE,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES BookingInformation(Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
GO

-- Add appropriate indexes for performance

-- Index on Email in User for faster lookup
CREATE INDEX IDX_User_Email ON [User] (Email);
GO

-- Index on PhoneNumber in User for fast retrieval
CREATE INDEX IDX_User_PhoneNumber ON [User] (PhoneNumber);
GO

-- Index on Route From and To
CREATE INDEX IDX_Route_From_To ON Route ([From], [To]);
GO

-- Index on DepartureTime in Flight
CREATE INDEX IDX_Flight_DepartureTime ON Flight (DepartureTime);
GO

-- Index on ArrivalTime in Flight
CREATE INDEX IDX_Flight_ArrivalTime ON Flight (ArrivalTime);
GO

-- Index on FlightId and FlightClassId in BookingInformation
CREATE INDEX IDX_BookingInformation_FlightId_FlightClassId ON BookingInformation (FlightId, FlightClassId);
GO

-- Index on PassengerId in PassengerOfBooking
CREATE INDEX IDX_PassengerOfBooking_PassengerId ON PassengerOfBooking (PassengerId);
GO

-- Index on BookingId in PassengerOfBooking
CREATE INDEX IDX_PassengerOfBooking_BookingId ON PassengerOfBooking (BookingId);
GO

-- Index on CreatedDate in PaymentRecord
CREATE INDEX IDX_PaymentRecord_CreatedDate ON PaymentRecord (CreatedDate);
GO

-- Index on PayDate in PaymentRecord
CREATE INDEX IDX_PaymentRecord_PayDate ON PaymentRecord (PayDate);
GO
