using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.CommonFieldsMappingService
{
    public class CF_PricePair
    {
        public CF_PricePair()
        {

        }
        public CF_PricePair(short level, decimal price)
        {
            this.Level = level;
            this.Price = price;
        }
        public short Level { get; set; }
        public decimal Price { get; set; }
    }
}
