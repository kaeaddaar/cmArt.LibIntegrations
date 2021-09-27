using cmArt.LibIntegrations.S5InventoryLogicService;
using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public class WJCommonFields : IS5InventoryLogic, IWJCommonFields
    {
        public string Cat { get; set; }
        public string Description { get; set; }
        public int InvUnique { get; set; }
        public string PartNumber { get; set; }
        public IEnumerable<PricePair> Prices { get; set; }
        public IEnumerable<II_S5QtyPair> Quantities { get; set; }
        public decimal WholesaleCost { get; set; }
    }
}
