IF DB_ID('Logistics') IS NOT NULL
   DROP DATABASE Logistics;

CREATE DATABASE Logistics;
USE Logistics;

GO
CREATE TABLE VehicleType (
    Id INT PRIMARY KEY,
    VehicleName VARCHAR(20) UNIQUE
);

GO
CREATE TABLE Vehicle (
    Id INT PRIMARY KEY IDENTITY,
    VehicleTypeId INT,
    VehicleNumber VARCHAR(20) UNIQUE,
    MaxCargoWeightKg INT,
    MaxCargoWeightPnd FLOAT,
    MaxCargoVolume FLOAT,
    FOREIGN KEY (VehicleTypeId) REFERENCES VehicleType(Id)
);

GO
CREATE TABLE Warehouse (
    Id INT PRIMARY KEY IDENTITY,
    WarehouseName VARCHAR(20)
);

GO
CREATE TABLE Invoice (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    RecipientAddress VARCHAR(100),
    SenderAddress VARCHAR(100),
    RecipientPhoneNumber VARCHAR(20),
    SenderPhoneNumber VARCHAR(20)
);

GO
CREATE TABLE Cargo (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    InvoiceId UNIQUEIDENTIFIER,
    CargoVolume FLOAT,
    CargoWeight INT,
    CargoCode VARCHAR(20) UNIQUE,
    VehicleId INT NULL,
    WarehouseId INT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id),
    FOREIGN KEY (VehicleId) REFERENCES Vehicle(Id),
    FOREIGN KEY (WarehouseId) REFERENCES Warehouse(Id)
);

USE master;
