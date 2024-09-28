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
    Name NVARCHAR(255) NOT NULL,
	Status VARCHAR(50) NOT NULL
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

-- Insert sample data into Airlines table
INSERT INTO Airlines (Id, Name) VALUES
('7f7c7a2a-0e35-4e25-a858-85a1f9676f01', 'Vietnam Airlines', 'Active'),
('b9f4346f-e1b6-4b7e-a5f9-348db6c752a2', 'Pacific Airlines', 'Active'),
('e65f9924-f915-4203-b8ff-8a18d75e2af3', 'Bamboo Airways', 'Active'),
('88ab9914-ec2a-4bfa-bf3f-b77bcac98711', 'VietJet Air', 'Active'),
('0a6c8ae1-17fa-41ae-82e6-c63f4fbb8db7', 'AirAsia', 'Active'),
('51e4f0de-dbbc-4e9f-81c7-d192ba2d216d', 'Singapore Airlines', 'Active'),
('950d019c-60d2-40b5-994e-e4f0a5a87b12', 'Qatar Airways', 'Active'),
('fe66f785-bae7-4e6e-ae07-87ec68815b88', 'Emirates', 'Active'),
('5c6d1b27-cb0d-49e7-9ae3-4b657f3a3566', 'Cathay Pacific', 'Active'),
('db1e3766-d994-44e7-a6a4-44a82f99b6e9', 'Thai Airways', 'Active');
GO
