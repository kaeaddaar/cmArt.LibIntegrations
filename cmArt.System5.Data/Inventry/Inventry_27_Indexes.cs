using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class Inventry_27_Indexes
    {
        public static int InventoryUnique(IInventry_27 record)
        {
            return record.InvUnique;
        }

        public static string PartNumber(IInventry_27 record)
        {
            return record.Part;
        }
    }

}
