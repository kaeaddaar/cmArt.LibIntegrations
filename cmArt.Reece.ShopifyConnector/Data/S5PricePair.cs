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
            this.level = level;
            this.price = price;
        }
        public short level { get; set; }
        public decimal price { get; set; }
        public double pricing
        {
            get
            {
                return ((float)price);
            }
            set
            {
                price = ((decimal)value);
            }
        }

    }
}
