using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interface;


namespace DataAccessLayer.Models 
{
    public class Shipment : IShipmentModel
    {
        private int shipmentId;
        private string itemName;
        private int quantity;
        private string destination;
      
        public int ShipmentId { get;  set; }  
        public string ItemName
        {
            get => itemName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)  && ShipmentId !=0)
                    throw new ArgumentException("Item Name cannot be null or empty.");
                itemName = value;
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity cannot be negative.");
                quantity = value;
            }
        }


        public string Destination
        {
            get => destination;
            set
            {
                if (string.IsNullOrWhiteSpace(value) && ShipmentId !=0)
                    throw new ArgumentException("Destination cannot be null or empty.");
                destination = value;
            }
        }

        public string CurrentUser => Environment.UserName.ToString();

        public Shipment()
        {
        }

       
    }
}
