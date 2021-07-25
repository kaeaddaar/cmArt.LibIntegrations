using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector
{
    public class Product_PrimaryLocation
    {
        public string locationName { get; set; }
        public int warehouse_id { get; set; }
        public string warehouseZone { get; set; }
        public string aisle { get; set; }
        public string bin { get; set; }
        public string level { get; set; }
    }

}
