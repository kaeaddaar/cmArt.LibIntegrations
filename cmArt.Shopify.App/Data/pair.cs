using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public struct pair
    {
        public pair(short level, decimal price)
        {
            Level = level;
            Price = price;
        }
        public short Level { get; }
        public decimal Price { get; }
    }
}
