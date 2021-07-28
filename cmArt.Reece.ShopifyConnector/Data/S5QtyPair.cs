using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class S5QtyPair
    {
        public S5QtyPair(short department, decimal qty)
        {
            Location = department;
            Qty = qty;
        }
        public short Location { get; set; }
        public decimal Qty { get; set; }

    }
}
