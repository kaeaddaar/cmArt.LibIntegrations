using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class S5PricePair
    {
        public S5PricePair(short level, decimal price)
        {
            Level = level;
            Price = price;
        }
        public short Level { get; set; }
        public decimal Price { get; set; }
    }
}
