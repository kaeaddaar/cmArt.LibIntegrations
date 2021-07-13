using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public class S5QtyPair
    {
        public S5QtyPair(short department, decimal qty)
        {
            Department = department;
            Qty = qty;
        }
        public short Department { get; set; }
        public decimal Qty { get; set; }

    }
}
