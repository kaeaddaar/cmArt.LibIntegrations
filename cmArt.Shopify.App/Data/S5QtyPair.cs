using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public class S5QtyPair
    {
        public S5QtyPair(short level, decimal qty)
        {
            Level = level;
            Qty = qty;
        }
        public short Level { get; set; }
        public decimal Qty { get; set; }

    }
}
