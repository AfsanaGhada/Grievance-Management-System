-- 1. Create Database
CREATE DATABASE [GrievanceManagementSystem];
GO

USE [GrievanceManagementSystem];
GO

-- 2. Country Table
CREATE TABLE Country (
    CountryId INT PRIMARY KEY IDENTITY(1,1),
    CountryName NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
);

-- 3. State Table
CREATE TABLE State (
    StateId INT PRIMARY KEY IDENTITY(1,1),
    StateName NVARCHAR(100) NOT NULL,
    StateCode NVARCHAR(10),
    CountryId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
);

-- 4. City Table
CREATE TABLE City (
    CityId INT PRIMARY KEY IDENTITY(1,1),
    CityName NVARCHAR(100) NOT NULL,
    CityCode NVARCHAR(10),
    StateId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (StateId) REFERENCES State(StateId)
);

-- 5. Users Table
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    [Password] NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50) NOT NULL, -- 'Admin' or 'Citizen'
    Phone NVARCHAR(15),
    CityId INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);

-- 6. FacilityType Table
CREATE TABLE FacilityType (
    FacilityTypeId INT PRIMARY KEY IDENTITY(1,1),
    FacilityName NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
);

-- 7. ComplaintType Table
CREATE TABLE ComplaintType (
    ComplaintTypeId INT PRIMARY KEY IDENTITY(1,1),
    ComplaintName NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
);

-- 8. Complaint Table
CREATE TABLE Complaint (
    ComplaintId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ComplaintDate DATETIME NOT NULL DEFAULT GETDATE(),
    [Status] NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    UserId INT NOT NULL,
    ComplaintTypeId INT NOT NULL,
    FacilityTypeId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (ComplaintTypeId) REFERENCES ComplaintType(ComplaintTypeId),
    FOREIGN KEY (FacilityTypeId) REFERENCES FacilityType(FacilityTypeId)
);

-- 9. (Optional) Complaint Status History Table
CREATE TABLE ComplaintStatusHistory (
    HistoryId INT PRIMARY KEY IDENTITY(1,1),
    ComplaintId INT NOT NULL,
    [Status] NVARCHAR(50) NOT NULL,
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    Remarks NVARCHAR(500),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (ComplaintId) REFERENCES Complaint(ComplaintId)
);
CREATE TABLE Citizen (
    CitizenId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FullName NVARCHAR(150) NOT NULL,
    Phone NVARCHAR(15),
    Address NVARCHAR(250),
    Gender NVARCHAR(10),
    DOB DATE,
    CityId INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);
CREATE TABLE Admin (
    AdminId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FullName NVARCHAR(150),
    Phone NVARCHAR(15),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE TABLE ComplaintAttachment (
    AttachmentId INT PRIMARY KEY IDENTITY(1,1),
    ComplaintId INT NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    UploadedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ComplaintId) REFERENCES Complaint(ComplaintId)
);

CREATE TABLE Feedback (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    ComplaintId INT NOT NULL,
    UserId INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comments NVARCHAR(500),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ComplaintId) REFERENCES Complaint(ComplaintId),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
USE GrievanceManagementSystem;
GO

CREATE TABLE Facility (
    FacilityId INT PRIMARY KEY IDENTITY(1,1),
    FacilityName NVARCHAR(100) NOT NULL,
    FacilityTypeId INT NOT NULL,
    Location NVARCHAR(200),
    CityId INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (FacilityTypeId) REFERENCES FacilityType(FacilityTypeId),
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);




INSERT INTO Country (CountryName) VALUES ('India');
INSERT INTO State (StateName, StateCode, CountryId) VALUES
('Gujarat', 'GJ', 1),
('Maharashtra', 'MH', 1);
INSERT INTO City (CityName, CityCode, StateId) VALUES
('Rajkot', 'RAJ', 1),
('Ahmedabad', 'AMD', 1),
('Mumbai', 'MUM', 2);
INSERT INTO Admin (UserId, FullName, Phone) 
VALUES (1, 'System Admin', '9999999999');
INSERT INTO [User] (FullName, Email, [Password], Role, Phone, CityId)
VALUES ('Afsana', 'afsana@example.com', 'afsana123', 'Citizen', '9998887770', 1);

INSERT INTO [User] (FullName, Email, [Password], Role, Phone, CityId)
VALUES ('Ruchi', 'ruchi@example.com', 'ruchi123', 'Citizen', '9998887771', 2);

INSERT INTO [User] (FullName, Email, [Password], Role, Phone, CityId)
VALUES ('Karan', 'karan@example.com', 'karan123', 'Citizen', '9998887772', 2);

INSERT INTO [User] (FullName, Email, [Password], Role, Phone, CityId)
VALUES ('Faizan', 'faizan@example.com', 'faizan123', 'Citizen', '9998887773', 3);

INSERT INTO [User] (FullName, Email, [Password], Role, Phone, CityId)
VALUES ('Admin', 'admin@gms.com', 'admin123', 'Admin', '9998887779', 1);
-- Assuming Admin UserId is 5 (as per insert order above)
INSERT INTO Admin (UserId, FullName, Phone)
VALUES (5, 'Admin', '9998887779');
CREATE TABLE Admin (
    AdminId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FullName NVARCHAR(150),
    Phone NVARCHAR(15),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Admin';
select * from Citizen
INSERT INTO Citizen (UserId, FullName, Phone, Address, Gender, DOB, CityId)
VALUES 
(1, 'Afsana', '9998887770', 'Block A, Rajkot', 'Female', '2000-05-10', 1),
(2, 'Ruchi', '9998887771', 'Street 4, Ahmedabad', 'Female', '1999-08-15', 2);
INSERT INTO FacilityType (FacilityName) VALUES
('Water Supply'),
('Garbage Collection'),
('Street Lights'),
('Drainage'),
('Road Maintenance');
INSERT INTO ComplaintType (ComplaintName) VALUES
('Water Leakage'),
('Garbage Not Collected'),
('Light Not Working'),
('Blocked Drainage'),
('Potholes');
INSERT INTO Complaint (Title, Description, UserId, ComplaintTypeId, FacilityTypeId)
VALUES
('Leaking Pipe in Street', 'There is a continuous water leak outside our house.', 2, 1, 1),
('Overflowing Garbage Bin', 'The garbage has not been collected for 4 days.', 3, 2, 2);
INSERT INTO ComplaintAttachment (ComplaintId, FilePath)
VALUES 
(1, 'uploads/leak_pipe.jpg'),
(2, 'uploads/garbage_overflow.png');
INSERT INTO ComplaintStatusHistory (ComplaintId, [Status], Remarks)
VALUES
(1, 'Pending', 'Received and assigned to engineer'),
(2, 'In Progress', 'Cleaning team dispatched');
INSERT INTO Feedback (ComplaintId, UserId, Rating, Comments)
VALUES
(1, 2, 4, 'Resolved quickly by the municipality.'),
(2, 3, 3, 'Took a bit longer than expected.');
SELECT * FROM Complaint;
SELECT * FROM Citizen;
SELECT * FROM Admin;
SELECT * FROM Feedback;
-- Citizen entries for Karan and Faizan
INSERT INTO Citizen (UserId, FullName, Phone, Address, Gender, DOB, CityId)
VALUES 
(3, 'Karan', '9998887772', 'Road 2, Ahmedabad', 'Male', '1998-01-20', 2),
(4, 'Faizan', '9998887773', 'Gali 7, Mumbai', 'Male', '1997-09-05', 3);
INSERT INTO Facility (FacilityName, FacilityTypeId, Location, CityId)
VALUES
('Main Water Pump - Ward 1', 1, 'Near School Road, Rajkot', 1),
('Garbage Bin - Street 5', 2, 'Street 5, Ahmedabad', 2),
('Street Light Pole #17', 3, 'Sector 12, Rajkot', 1),
('Drainage Outlet - Block B', 4, 'Block B, Mumbai', 3),
('Main Road Sector 8', 5, 'Sector 8, Mumbai', 3);

SELECT COUNT(*) FROM Country;
SELECT COUNT(*) FROM State;
SELECT COUNT(*) FROM City;
SELECT COUNT(*) FROM [User];
SELECT COUNT(*) FROM Admin;
SELECT COUNT(*) FROM Citizen;
SELECT COUNT(*) FROM Complaint;
SELECT COUNT(*) FROM Feedback;
SELECT COUNT(*) FROM ComplaintStatusHistory;
SELECT COUNT(*) FROM ComplaintType;
SELECT COUNT(*) FROM FacilityType;
SELECT COUNT(*) FROM ComplaintAttachment;
