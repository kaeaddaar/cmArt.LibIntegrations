using cmArt.System5.Inventory.InfoInterfaces.Pricing;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.S5InventoryLogicService
{
    public class PricePair : II_PricePair
    {
        public PricePair(short level, decimal price)
        {
            Level = level;
            Price = price;
        }
        public short Level { get; set; }
        public decimal Price { get; set; }
        public bool Equals(PricePair CompareTo)
        {
            bool TheyDontMatch = false;
            if (this.Level != CompareTo.Level) { return TheyDontMatch; }
            if (this.Price != CompareTo.Price) { return TheyDontMatch; }
            return true;
        }
    }
}
