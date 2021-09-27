using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory.InfoInterfaces.Pricing
{
    public interface II_PricePair
    {
        short Level { get; set; }
        decimal Price { get; set; }
    }
}
