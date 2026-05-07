/*
SmartWholesale Database Script
Normalized to 3NF
Project: SmartWholesale Management System
Course: CSC2210 OOP2
*/

CREATE DATABASE SmartWholesaleDB;
GO

USE SmartWholesaleDB;
GO

-- 1. Users Table (Role-based access)
CREATE TABLE Users (
    UId INT PRIMARY KEY IDENTITY(1,1),
    UName NVARCHAR(100) NOT NULL,
    UPassword NVARCHAR(100) NOT NULL,
    UPhoneNo NVARCHAR(20),
    Email NVARCHAR(100) UNIQUE,
    UAddress NVARCHAR(200),
    Role NVARCHAR(50) NOT NULL, -- SuperAdmin, Admin, Manager, Customer
    JoiningDate DATETIME DEFAULT GETDATE(),
    Salary DECIMAL(18, 2) DEFAULT 0
);

-- 2. Items Table (Inventory)
CREATE TABLE Items (
    IId INT PRIMARY KEY IDENTITY(1,1),
    IType NVARCHAR(50) NOT NULL,
    IBrand NVARCHAR(50),
    IModelNo NVARCHAR(50),
    IPrice DECIMAL(18, 2) NOT NULL,
    IStockStatus INT NOT NULL DEFAULT 0, -- Quantity
    IMinimumStock INT NOT NULL DEFAULT 5,
    OwnerId INT, -- Admin/ShopOwner who owns this product
    CONSTRAINT FK_ItemOwner FOREIGN KEY (OwnerId) REFERENCES Users(UId)
);

-- 3. Bills Table
CREATE TABLE Bills (
    BillId INT PRIMARY KEY IDENTITY(1,1),
    BillDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    CustomerId INT,
    CONSTRAINT FK_BillCustomer FOREIGN KEY (CustomerId) REFERENCES Users(UId)
);

-- 4. Transactions Table
CREATE TABLE Transactions (
    TId INT PRIMARY KEY IDENTITY(1,1),
    BillId INT,
    UId INT, -- The user who processed the transaction (e.g. Manager/Salesman)
    TotalAmount DECIMAL(18, 2) NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_TransactionBill FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_TransactionUser FOREIGN KEY (UId) REFERENCES Users(UId)
);

-- 5. BillItems Table (Junction table for Many-to-Many between Bill and Item)
CREATE TABLE BillItems (
    BillItemId INT PRIMARY KEY IDENTITY(1,1),
    BillId INT,
    IId INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_BillItems_Bill FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_BillItems_Item FOREIGN KEY (IId) REFERENCES Items(IId)
);

-- 6. Cart Table (Temporary storage for customer)
CREATE TABLE Cart (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UId INT,
    IId INT,
    Quantity INT NOT NULL,
    AddedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_CartUser FOREIGN KEY (UId) REFERENCES Users(UId),
    CONSTRAINT FK_CartItem FOREIGN KEY (IId) REFERENCES Items(IId)
);

-- 7. Reviews Table
CREATE TABLE Reviews (
    ReviewId INT PRIMARY KEY IDENTITY(1,1),
    UId INT, -- Customer
    IId INT, -- Product
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    ReviewDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ReviewUser FOREIGN KEY (UId) REFERENCES Users(UId),
    CONSTRAINT FK_ReviewItem FOREIGN KEY (IId) REFERENCES Items(IId)
);

-- 8. Offers Table
CREATE TABLE Offers (
    OfferId INT PRIMARY KEY IDENTITY(1,1),
    IId INT,
    DiscountPercent FLOAT,
    StartDate DATETIME,
    EndDate DATETIME,
    CONSTRAINT FK_OfferItem FOREIGN KEY (IId) REFERENCES Items(IId)
);

-- Seed Data
INSERT INTO Users (UName, UPassword, UPhoneNo, Email, UAddress, Role, Salary)
VALUES 
('Super Admin', 'admin123', '01700000000', 'superadmin@wholesale.com', 'Dhaka', 'SuperAdmin', 100000),
('Shop Owner 1', 'owner123', '01800000000', 'owner1@shop.com', 'Chittagong', 'Admin', 50000),
('Manager 1', 'manager123', '01900000000', 'manager1@shop.com', 'Sylhet', 'Manager', 30000),
('Customer 1', 'cust123', '01500000000', 'customer1@gmail.com', 'Rajshahi', 'Customer', 0);

INSERT INTO Items (IType, IBrand, IModelNo, IPrice, IStockStatus, OwnerId)
VALUES 
('Processor', 'AMD', 'Ryzen 7 5800X', 22000, 14, 2),
('SSD', 'Samsung', 'EVO 225GB', 2000, 11, 2),
('RAM', 'GIGABYTE', 'REVO 8GB', 1500, 22, 2);
