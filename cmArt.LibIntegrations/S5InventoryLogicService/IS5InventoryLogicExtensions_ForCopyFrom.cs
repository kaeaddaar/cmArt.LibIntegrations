using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.S5InventoryLogicService
{
    public static class IS5InventoryLogicExtensions_ForCopyFrom
    {
        public static IS5InventoryLogic CopyFrom(this IS5InventoryLogic to, IS5InventoryLogic from)
        {
            to.Cat = from.Cat;
            to.Description = from.Description;
            to.InvUnique = from.InvUnique;
            to.PartNumber = from.PartNumber;

            to.Prices = new List<PricePair>(from.Prices ?? new List<PricePair>());
            to.Quantities = new List<II_S5QtyPair>(from.Quantities ?? new List<II_S5QtyPair>());

            to.WholesaleCost = from.WholesaleCost;

            return to;
        }
    }
}
