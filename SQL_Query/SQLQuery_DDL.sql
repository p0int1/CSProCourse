-- DROP DATABASE Logistics

-- создание базы данных
CREATE DATABASE Logistics

USE Logistics

-- справочник тип транспорта
-- Car, Ship, Plane, Train
CREATE TABLE VehicleType (
    Id INT PRIMARY KEY,
    VehicleName VARCHAR(20) UNIQUE
);

-- таблица транспортов
CREATE TABLE Vehicle (
    Id INT PRIMARY KEY IDENTITY,
    VehicleType INT,
    VehicleNumber VARCHAR(20) UNIQUE,
    MaxCargoWeightKg INT,
    MaxCargoWeightPnd FLOAT,
    MaxCargoVolume FLOAT,
    FOREIGN KEY (VehicleType) REFERENCES VehicleType(Id)
);

-- таблица складов
CREATE TABLE Warehouse (
    Id INT PRIMARY KEY IDENTITY,
    WarehouseName VARCHAR(20)
);

-- накладная
CREATE TABLE Invoice (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    RecipientAddress VARCHAR(100),
    SenderAddress VARCHAR(100),
    RecipientPhoneNumber VARCHAR(20),
    SenderPhoneNumber VARCHAR(20)
);

-- таблица грузов
CREATE TABLE Cargo (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    InvoiceId UNIQUEIDENTIFIER,
    CargoVolume FLOAT,
    CargoWeight INT,
    CargoCode VARCHAR(20) UNIQUE FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id)
);

-- таблица связи VehicleCargo
CREATE TABLE VehicleCargo (
    VehicleId INT,
    CargoId UNIQUEIDENTIFIER,
    FOREIGN KEY (VehicleId) REFERENCES Vehicle(Id),
    FOREIGN KEY (CargoId) REFERENCES Cargo(Id),
);

-- таблица связи WarehouseCargo
CREATE TABLE WarehouseCargo (
    WarehouseId INT,
    CargoId UNIQUEIDENTIFIER,
    FOREIGN KEY (WarehouseId) REFERENCES Warehouse(Id),
    FOREIGN KEY (CargoId) REFERENCES Cargo(Id),
);
