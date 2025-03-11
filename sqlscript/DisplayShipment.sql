USE InventoryDb; 
GO 
Alter PROCEDURE DisplayShipment

AS 
BEGIN
Select * from Shipment where IsDeleted=0
END; 
GO