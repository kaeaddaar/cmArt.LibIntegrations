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
        public static int InvUnique(IS5InvAssembled record)
        {
            if (record == null) { return 0; }
            if (record.Inv == null) { return 0; }
            return record.Inv.InvUnique;
        }

    }
}
