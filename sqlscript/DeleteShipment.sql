USE InventoryDb; 
GO
CREATE PROCEDURE DeleteShipment
    @ShipmentId INT
AS
BEGIN
    BEGIN TRY
        -- Start a transaction
        BEGIN TRANSACTION;

        -- Validation: Check if the Shipment ID exists
        IF NOT EXISTS (SELECT 1 FROM Shipment WHERE ShipmentId = @ShipmentId AND IsDeleted = 0)
        BEGIN
            THROW 55000, 'Shipment ID does not exist or has already been deleted.', 1;
        END

        -- Soft delete the record by updating IsDeleted flag
        UPDATE Shipment
        SET 
            IsDeleted = 1, 
            ModifiedBy = 'System', -- This can be replaced dynamically with the user
            ModifiedDate = GETDATE() -- Record the deletion date
        WHERE ShipmentId = @ShipmentId;

        -- Commit the transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of error
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
    END CATCH
END;
GO