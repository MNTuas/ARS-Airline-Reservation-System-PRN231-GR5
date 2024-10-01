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
INSERT INTO Airlines (Id, Name, Status) VALUES
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

-- Insert sample data into Airplane table
INSERT INTO Airplane (Id, Code, Type, AvailableSeat, AirlinesId) VALUES
('0fd1d134-4f53-4456-8f26-6f76879c6077', 'VN-A888', 'Boeing 787-9 Dreamliner', 300, '7f7c7a2a-0e35-4e25-a858-85a1f9676f01'), -- Vietnam Airlines
('8b32d6a1-e76d-4c47-8190-68cde438cda8', 'VN-A896', 'Airbus A350-900', 305, '7f7c7a2a-0e35-4e25-a858-85a1f9676f01'), -- Vietnam Airlines
('c0d2e582-f3e0-4d7f-a3b2-62d604c8a192', 'PA-3201', 'Airbus A320', 180, 'b9f4346f-e1b6-4b7e-a5f9-348db6c752a2'), -- Pacific Airlines
('96f6797d-b40d-4ac9-bb8e-5e4c865ce413', 'BQA-789', 'Boeing 787-9 Dreamliner', 294, 'e65f9924-f915-4203-b8ff-8a18d75e2af3'), -- Bamboo Airways
('53c72eae-89b3-4be7-bc89-d3992e4c6e3f', 'VJ-A321', 'Airbus A321neo', 230, '88ab9914-ec2a-4bfa-bf3f-b77bcac98711'), -- VietJet Air
('97c6276d-9a30-4293-b621-b7dd05660c2f', 'AK-330', 'Airbus A330-300', 285, '0a6c8ae1-17fa-41ae-82e6-c63f4fbb8db7'), -- AirAsia
('11e4fb50-5e3c-48c3-bf7d-01b476dd9bb1', 'SQ-A380', 'Airbus A380', 471, '51e4f0de-dbbc-4e9f-81c7-d192ba2d216d'), -- Singapore Airlines
('7a1d6ff1-dff4-40f2-8f98-d82f3746f1da', 'QR-777', 'Boeing 777-300ER', 354, '950d019c-60d2-40b5-994e-e4f0a5a87b12'), -- Qatar Airways
('2d3bf8c8-bb26-46de-9855-2294f027a5b5', 'EK-380', 'Airbus A380', 517, 'fe66f785-bae7-4e6e-ae07-87ec68815b88'), -- Emirates
('dc5bc99d-14f2-48ae-93b4-2fb20ed94a0d', 'CX-747', 'Boeing 747-400', 375, '5c6d1b27-cb0d-49e7-9ae3-4b657f3a3566'); -- Cathay Pacific
GO
