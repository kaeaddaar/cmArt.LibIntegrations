using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.S5InventoryLogicService
{
    public class QtyPair : II_S5QtyPair
    {
        public QtyPair(short Location, decimal Qty)
        {
            this.Location = Location;
            this.Qty = Qty;
        }
        public short Location { get; set; }
        public decimal Qty { get; set; }

    }
}
