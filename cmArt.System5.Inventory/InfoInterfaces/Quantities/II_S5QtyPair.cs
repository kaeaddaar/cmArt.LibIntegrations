using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory.InfoInterfaces.Quantities
{
    public interface II_S5QtyPair
    {
        short Location { get; set; }
        decimal Qty { get; set; }

    }
}
