using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interface
{
   public  interface IShipmentRepository
    {
        int AddShipment(Shipment shipment);
        int UpdateShipment(Shipment shipment);
        int DeleteShipment(int shipmentId);
        ObservableCollection<Shipment> GetAllShipments();
    }
}
