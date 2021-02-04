using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class Comments_Indexes
    {
        public static int InventoryUnique_File135(IComments record)
        {
            if (record.FileNo == 135)
            {
                return record.RecordNo;
            }
            else
            {
                return 0;
            }
        }
    }

}
