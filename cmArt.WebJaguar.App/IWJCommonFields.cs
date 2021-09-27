using cmArt.LibIntegrations.S5InventoryLogicService;
using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System.Collections.Generic;

namespace cmArt.WebJaguar.App
{
    public interface IWJCommonFields : IS5InventoryLogic
    {
        string Cat { get; set; }
        string Description { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        IEnumerable<PricePair> Prices { get; set; }
        IEnumerable<QtyPair> Quantities { get; set; }
        decimal WholesaleCost { get; set; }
    }
}