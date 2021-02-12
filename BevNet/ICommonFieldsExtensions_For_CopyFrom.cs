using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public static class ICommonFieldsExtensions_For_CopyFrom
    {
        public static void CopyFrom(this ICommonFields to, ICommonFields from)
        {
            to.PriceSchedule1_MSRP = from.PriceSchedule1_MSRP;
            to.PriceSchedule2_MinPrice = from.PriceSchedule2_MinPrice;
            to.SupplierCode = from.SupplierName;
            to.SupplierName = from.SupplierName;
            to.WholesaleCost = from.WholesaleCost;
        }

    }
}
