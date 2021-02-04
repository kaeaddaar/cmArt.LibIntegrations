using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class IAltSuplyExtensions_ForCopyFrom
    {
        public static void CopyFrom(this IAltSuply to, IAltSuply from)
        {
            to.AUnique = from.AUnique;
            to.Duty = from.Duty;
            to.Extra = from.Extra;
            to.FileNo = from.FileNo;
            to.Freight = from.Freight;
            to.PackSize = from.PackSize;
            to.Part = from.Part;
            to.PartNumber = from.PartNumber;
            to.Preferred = from.Preferred;
            to.Price = from.Price;
            to.RecordNo = from.RecordNo;
        }
    }

}
