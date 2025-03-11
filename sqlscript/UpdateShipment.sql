USE InventoryDb; 
GO
CREATE PROCEDURE UpdateShipment
    @ShipmentId INT,
    @ItemName NVARCHAR(100),
    @Quantity INT,
    @Destination NVARCHAR(200),
    @ModifiedBy NVARCHAR(100)
AS
BEGIN
    BEGIN TRY
        -- Start a transaction
        BEGIN TRANSACTION;

        -- Validate input parameters
        IF @ShipmentId IS NULL OR @ShipmentId <= 0
        BEGIN
            THROW 52000, 'Invalid ShipmentId. ShipmentId must be greater than 0.', 1;
        END

        IF @ItemName IS NULL OR LEN(@ItemName) = 0
        BEGIN
            THROW 52001, 'ItemName cannot be NULL or empty.', 1;
        END

        IF @Quantity IS NULL OR @Quantity < 0
        BEGIN
            THROW 52002, 'Quantity cannot be NULL or negative.', 1;
        END

        IF @Destination IS NULL OR LEN(@Destination) = 0
        BEGIN
            THROW 52003, 'Destination cannot be NULL or empty.', 1;
        END

        IF @ModifiedBy IS NULL OR LEN(@ModifiedBy) = 0
        BEGIN
            THROW 52004, 'ModifiedBy cannot be NULL or empty.', 1;
        END

        -- Check if the record exists
        IF NOT EXISTS (SELECT 1 FROM Shipment WHERE ShipmentId = @ShipmentId AND IsDeleted = 0)
        BEGIN
            THROW 52005, 'Shipment with the given ShipmentId does not exist or is deleted.', 1;
        END

        -- Update the record
        UPDATE Shipment
        SET
            ItemName = @ItemName,
            Quantity = @Quantity,
            Destination = @Destination,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETDATE()
        WHERE ShipmentId = @ShipmentId;

        -- Commit the transaction if all is successful
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