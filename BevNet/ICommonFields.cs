using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public interface ICommonFields
    {
        string SupplierName { get; set; }
        string SupplierPartNumber { get; set; }
        string SupplierCode { get; set; }
        decimal WholesaleCost { get; set; }
        decimal PriceSchedule1_MSRP { get; set; }
        decimal PriceSchedule2_MinPrice { get; set; }
    }
}
