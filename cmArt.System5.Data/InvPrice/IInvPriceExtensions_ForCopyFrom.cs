using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class IInvPriceExtensions_ForCopyFrom
    {
        public static void CopyFrom(this IInvPrice to, IInvPrice from)
        {
            to.Department = from.Department;
            to.EndDate = from.EndDate;
            to.InvUnique = from.InvUnique;
            to.PartUnique = from.PartUnique;
            to.QuanDisc = from.QuanDisc;
            to.RegularPrice = from.RegularPrice;
            to.RScheduleType = from.RScheduleType;
            to.SalePrice = from.SalePrice;
            to.ScheduleLevel = from.ScheduleLevel;
            to.SScheduleType = from.SScheduleType;
            to.StartDate = from.StartDate;
        }
    }

}
