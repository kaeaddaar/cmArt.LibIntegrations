using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace cmArt.Reece.ShopifyConnector
{
    public class tmpS5QtyPair
    {
        public tmpS5QtyPair(string Location, decimal Qty)
        {
            this.Location = Location;
            this.Qty = Qty;
        }
        public string Location { get; set; }
        public decimal Qty { get; set; }
        public S5QtyPair AsS5QtyPair()
        {
            short tmpLocation = 0;
            if (Location == "62675222726") { tmpLocation = 0; }
            else if (Location == "63449497798") { tmpLocation = 2; }
            else if (Location == "63449530566") { tmpLocation = 3; }
            else if (Location == "63449563334") { tmpLocation = 4; }
            else 
            {
                short.TryParse(Location, out tmpLocation);
            }
            S5QtyPair s5QtyPair = new S5QtyPair(tmpLocation, Qty);
            return s5QtyPair;
        }

    }
}
