USE master
-- Create Database
CREATE DATABASE AirlinesReservationSystem;
GO

-- Use the database
USE AirlinesReservationSystem;
GO

-- Create Tables
CREATE TABLE Airlines (
    Id CHAR(36) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
	Status NVARCHAR(50) NOT NULL
);

CREATE TABLE Airport (
    Id CHAR(36) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
    Country NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50) NOT NULL
);

CREATE TABLE Rank (
    Id CHAR(36) PRIMARY KEY,
    Type NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Discount DECIMAL(5, 2) NOT NULL
);

CREATE TABLE [User] (
    Id CHAR(36) PRIMARY KEY,
    Avatar NVARCHAR(255),
    Name NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(50),
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255),
    Point INT NOT NULL,
    RankId CHAR(36) FOREIGN KEY REFERENCES Rank(Id),
    Role NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL
);

CREATE TABLE Airplane (
    Id CHAR(36) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    AvailableSeat INT NOT NULL,
    AirlinesId CHAR(36) FOREIGN KEY REFERENCES Airlines(Id),
	Status NVARCHAR(50) 
);

CREATE TABLE Flight (
    Id CHAR(36) PRIMARY KEY,
    AirplaneId CHAR(36) FOREIGN KEY REFERENCES Airplane(Id),
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    [From] CHAR(36) FOREIGN KEY REFERENCES Airport(Id),
    [To] CHAR(36) FOREIGN KEY REFERENCES Airport(Id)
);

CREATE TABLE FlightClass (
    Id CHAR(36) PRIMARY KEY,
    FlightId CHAR(36) FOREIGN KEY REFERENCES Flight(Id),
    Class NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Passenger (
    Id CHAR(36) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(50) NOT NULL,
    Dob DATE NOT NULL,
    Country NVARCHAR(255) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    UserId CHAR(36) FOREIGN KEY REFERENCES [User](Id)
);

CREATE TABLE BookingInformation (
    Id CHAR(36) PRIMARY KEY,
    FlightId CHAR(36) FOREIGN KEY REFERENCES Flight(Id),
    CreatedDate DATETIME NOT NULL,
    FlightClassId CHAR(36) FOREIGN KEY REFERENCES FlightClass(Id),
    UserId CHAR(36) FOREIGN KEY REFERENCES [User](Id),
    Status NVARCHAR(50) NOT NULL
);

CREATE TABLE PassengerOfBooking (
    Id CHAR(36) PRIMARY KEY,
    PassengerId CHAR(36) FOREIGN KEY REFERENCES Passenger(Id),
    BookingId CHAR(36) FOREIGN KEY REFERENCES BookingInformation(Id)
);

CREATE TABLE PaymentRecord (
    Id CHAR(36) PRIMARY KEY,
    BookingId CHAR(36) FOREIGN KEY REFERENCES BookingInformation(Id),
    UserId CHAR(36) FOREIGN KEY REFERENCES [User](Id),
    Price DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(5, 2) NOT NULL,
    FinalPrice DECIMAL(10, 2) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    PayDate DATETIME,
    Status NVARCHAR(50) NOT NULL
);

-- Create Indexes
CREATE INDEX IDX_Flight_DepartureTime ON Flight(DepartureTime);
CREATE INDEX IDX_Flight_ArrivalTime ON Flight(ArrivalTime);
CREATE INDEX IDX_Flight_Status ON Flight(Status);
CREATE INDEX IDX_Airplane_Code ON Airplane(Code);
CREATE INDEX IDX_User_Email ON [User](Email);

-- Insert Sample Data (Vietnam)
INSERT INTO Airlines (Id, Name, Status) VALUES (NEWID(), 'Vietnam Airlines', 'Active');
INSERT INTO Airport (Id, Name, City, Country, Status) VALUES (NEWID(), 'Noi Bai International Airport', 'Hanoi', 'Vietnam', 'Active');
INSERT INTO Airport (Id, Name, City, Country, Status) VALUES (NEWID(), 'Tan Son Nhat International Airport', 'Ho Chi Minh City', 'Vietnam', 'Active');

INSERT INTO Rank (Id, Type, Description, Discount) VALUES (NEWID(), 'Gold', 'Gold member discount', 10.00);

INSERT INTO Airplane (Id, Code, Type, AvailableSeat, AirlinesId) 
VALUES (NEWID(), 'VN-A123', 'Boeing 787', 250, (SELECT Id FROM Airlines WHERE Name = 'Vietnam Airlines'));

INSERT INTO Flight (Id, AirplaneId, DepartureTime, ArrivalTime, Status, [From], [To])
VALUES (NEWID(), 
(SELECT Id FROM Airplane WHERE Code = 'VN-A123'), 
'2024-10-10 08:00:00', '2024-10-10 10:00:00', 'Scheduled',
(SELECT Id FROM Airport WHERE Name = 'Noi Bai International Airport'), 
(SELECT Id FROM Airport WHERE Name = 'Tan Son Nhat International Airport'));

INSERT INTO FlightClass (Id, FlightId, Class, Quantity, Price) 
VALUES (NEWID(), 
(SELECT Id FROM Flight WHERE Status = 'Scheduled'), 'Economy', 200, 150.00);

--INSERT INTO Passenger (Id, FirstName, LastName, Gender, Dob, Country, Type, UserId)
--VALUES (NEWID(), 'Tran', 'Thi B', 'Female', '1995-01-01', 'Vietnam', 'Adult', 
--(SELECT Id FROM User WHERE Name = 'Nguyen Van A'));

--INSERT INTO BookingInformation (Id, FlightId, CreatedDate, FlightClassId, UserId, Status) 
--VALUES (NEWID(), 
--(SELECT Id FROM Flight WHERE Status = 'Scheduled'), 
--GETDATE(), 
--(SELECT Id FROM FlightClass WHERE Class = 'Economy'), 
--(SELECT Id FROM User WHERE Name = 'Nguyen Van A'), 'Booked');

--INSERT INTO PaymentRecord (Id, BookingId, UserId, Price, Discount, FinalPrice, CreatedDate, PayDate, Status) 
--VALUES (NEWID(), 
--(SELECT Id FROM BookingInformation WHERE Status = 'Booked'), 
--(SELECT Id FROM User WHERE Name = 'Nguyen Van A'), 150.00, 10.00, 135.00, GETDATE(), GETDATE(), 'Paid');
