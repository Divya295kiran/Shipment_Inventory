USE InventoryDb; 
GO
CREATE PROCEDURE InsertShipment
    @ItemName NVARCHAR(100),
    @Quantity INT,
    @Destination NVARCHAR(200),
    @CreatedBy NVARCHAR(100)
AS
BEGIN
    BEGIN TRY
        -- Start a transaction
        BEGIN TRANSACTION;

        -- Validate input parameters
        IF @ItemName IS NULL OR LEN(@ItemName) = 0
        BEGIN
            THROW 51000, 'ItemName cannot be NULL or empty.', 1;
        END

        IF @Quantity IS NULL OR @Quantity < 0
        BEGIN
            THROW 51001, 'Quantity cannot be NULL or negative.', 1;
        END

        IF @Destination IS NULL OR LEN(@Destination) = 0
        BEGIN
            THROW 51002, 'Destination cannot be NULL or empty.', 1;
        END

        IF @CreatedBy IS NULL OR LEN(@CreatedBy) = 0
        BEGIN
            THROW 51003, 'CreatedBy cannot be NULL or empty.', 1;
        END

        -- Insert data into ShipmentTable
        INSERT INTO Shipment (ItemName, Quantity, Destination, CreatedBy, CreatedDate, IsDeleted)
        VALUES (@ItemName, @Quantity, @Destination, @CreatedBy, GETDATE(), 0);

        -- Commit transaction if successful
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction on error
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        
    END CATCH
END;
GO