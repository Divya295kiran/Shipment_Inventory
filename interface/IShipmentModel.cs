using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IShipmentModel
    {
        int ShipmentId { get;  set; }
        string ItemName { get; set; }
        int Quantity { get; set; }
        string Destination { get; set; }
    }
}
