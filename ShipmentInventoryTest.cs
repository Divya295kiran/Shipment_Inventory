using NUnit.Framework;
using InventorySystem.ViewModels;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System.Collections.ObjectModel;
using Moq;
using DataAccessLayer.Interface;
using System.Configuration;
using System.Data.SqlClient;

namespace InventoryTest
{
    public class ShipmentInventoryTest
    {
        private ShipmentViewModel viewModel;
        private ShipmentRepository shipmentRepository;
        

        [SetUp]
        public void SetUp()
        {
          
            shipmentRepository = new ShipmentRepository();
            viewModel = new ShipmentViewModel();
          
        }

        [Test]
        public void AddShipment_ValidShipment_ShouldAddToRepository()
        {
            // Arrange
            viewModel.Shipment.ItemName = "Laptop";
            viewModel.Shipment.Quantity = 5;
            viewModel.Shipment.Destination = "New York";

            // Act
            viewModel.AddShipment();

            // Assert
            var shipments = shipmentRepository.GetAllShipments();
            Assert.That( shipments.Count , Is.EqualTo(1));
            Assert.That( shipments[0].ItemName , Is.EqualTo("Laptop"));
            Assert.That(shipments[0].Quantity, Is.EqualTo(5));
            Assert.That( shipments[0].Destination , Is.EqualTo("New York"));
        }

        
        [Test]
        public void UpdateShipment_ValidShipment_ShouldUpdateInRepository()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = 4,
                ItemName = "Tablet",
                Quantity = 3,
                Destination = "London"
            };
            shipmentRepository.AddShipment(shipment);

            viewModel.Shipment = new Shipment
            {
                ShipmentId = 4,
                ItemName = "Updated Tablet",
                Quantity = 6,
                Destination = "Paris"
            };

            // Act
            viewModel.UpdateShipment();

            // Assert
            var shipments = shipmentRepository.GetAllShipments();
            Assert.That( shipments.Count , Is.EqualTo(3));
            Assert.That( shipments[2].ItemName , Is.EqualTo("Updated Tablet"));
            Assert.That( shipments[2].Quantity, Is.EqualTo(6));
            Assert.That( shipments[2].Destination , Is.EqualTo("Paris"));
        }

        [Test]
        public void DeleteShipment_ValidId_ShouldRemoveFromRepository()
        {
            // Arrange
            var shipment = new Shipment
            {
                ShipmentId = 1,
                ItemName = "Headphones",
                Quantity = 10,
                Destination = "Berlin"
            };
            shipmentRepository.AddShipment(shipment);

            viewModel.Shipment = shipment;

            // Act
            viewModel.DeleteShipment();

            // Assert
            var shipments = shipmentRepository.GetAllShipments();
            Assert.That(shipments.Count , Is.EqualTo(1));
        }


        [Test]
        public void LoadData_ShouldPopulateShipmentList()
        {
            // Arrange
            shipmentRepository.AddShipment(new Shipment { ItemName = "Phone", Quantity = 10, Destination = "Tokyo" });
           

            // Act
            viewModel.LoadData();

            // Assert
            Assert.That( viewModel.ShipmentList.Count , Is.EqualTo(2));
            Assert.That( viewModel.ShipmentList[1].ItemName , Is.EqualTo("Phone"));
          
        }




    }
}