using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class S5PricePair
    {
        public S5PricePair()
        {

        }
        public S5PricePair(short level, decimal price)
        {
            this.Level = level;
            this.Price = price;
        }
        public short Level { get; set; }
        public decimal Price { get; set; }
        //public double pricing
        //{
        //    get
        //    {
        //        return ((float)Price);
        //    }
        //    set
        //    {
        //        Price = ((decimal)value);
        //    }
        //}

    }
}
