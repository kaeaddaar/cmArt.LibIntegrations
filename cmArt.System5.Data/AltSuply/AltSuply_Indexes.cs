using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class AltSuply_Indexes
    {
        public static int InventoryUnique(IAltSuply record)
        {
            return record.Part;
        }
    }

}
