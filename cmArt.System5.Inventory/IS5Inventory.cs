using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public interface IS5Inventory
    {
        IEnumerable<IAltSuply> AltSupplies { get; }
        IEnumerable<IComments> CommentsLines { get; }
        IEnumerable<IInventry_27> Inventry_27s { get; }
        IEnumerable<IInvPrice> InvPrices { get; }
        IEnumerable<IStok> StokLines { get; }
        short Schedule_0 { get; }
        short Schedule_Cash { get; }
        short Schedule_List { get; }
        short Schedule_Sale { get; }
        //IEnumerable<InventoryBasePriceInfo> PriceInfos { get; set; }
    }

}
