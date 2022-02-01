using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace cmArt.Reece.ShopifyConnector
{
    public class S5QtyPair
    {
        public S5QtyPair(short Location, decimal Qty)
        {
            this.Location = Location;
            this.Qty = Qty;
        }
        public short Location { get; set; }
        public decimal Qty { get; set; }
        public static S5QtyPair Empty(short department) => new S5QtyPair(department, 0);

    }
}
