using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public static class IS5InventoryExtensions_ForCopyFrom
    {
        public static void CopyFrom(this IS5Inventory to, IS5Inventory from)
        {
            to.AltSuplyLines = from.AltSuplyLines;
            to.CommentsLines = from.CommentsLines;
            to.Inventry_27s = from.Inventry_27s;
            to.InvPrices = from.InvPrices;
            to.Schedule_0 = from.Schedule_0;
            to.Schedule_Cash = from.Schedule_Cash;
            to.Schedule_List = from.Schedule_List;
            to.Schedule_Sale = from.Schedule_Sale;
            to.StokLines = from.StokLines;
        }
    }
}
