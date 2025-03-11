Use InventoryDb
GO
CREATE TABLE Shipment (
    ShipmentId INT PRIMARY KEY IDENTITY(1,1), -- Primary Key with Identity for auto-increment
    ItemName NVARCHAR(100) NOT NULL, -- NOT NULL constraint for item name
    Quantity INT NOT NULL CHECK (Quantity >= 0), -- NOT NULL and Quantity cannot be negative
    Destination NVARCHAR(200) NOT NULL, -- NOT NULL constraint for destination
    CreatedBy NVARCHAR(100) NOT NULL DEFAULT 'System', -- Tracks the user who created the record, default 'System'
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Default value: current date/time
    ModifiedBy NVARCHAR(100) NULL, -- Tracks the user who last modified the record
    ModifiedDate DATETIME NULL, -- Tracks the last modification date
    IsDeleted BIT NOT NULL DEFAULT 0 -- Logical deletion flag with default value 0
);

--  index on the ItemName column 
CREATE INDEX IX_Shipment_ItemName
ON Shipment(ItemName);

