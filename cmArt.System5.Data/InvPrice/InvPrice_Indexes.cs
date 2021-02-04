using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class InvPrice_Indexes
    {
        public static int InventoryUnique(IInvPrice record)
        {
            return record.PartUnique;
        }
    }
}
