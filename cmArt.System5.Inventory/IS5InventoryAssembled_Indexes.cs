using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public static class IS5InvAssembled_Indexes
    {
        public static int SupplierUnique_Key(IS5InvAssembled record)
        {
            return (int)record.Inv.Supplier;
        }

    }
}
