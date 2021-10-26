using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public static class S5InvAssembledObj_Indexes
    {
        public static int SupplierUnique_Key(S5InvAssembledObj record)
        {
            return (int)record.Inv.Supplier;
        }
        public static int InvUnique(S5InvAssembledObj record)
        {
            if (record == null) { return 0; }
            if (record.Inv == null) { return 0; }
            return record.Inv.InvUnique;
        }

    }
}
