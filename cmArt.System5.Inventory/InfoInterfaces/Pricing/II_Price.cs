using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory.Pricing
{
    public class II_Price
    {
        short Schedule { get; set; }
        decimal Value { get; set; }
        PriceSchedFormula Formula { get; set; }
        decimal PercentageOrAmount{ get; set; }
    }
}
