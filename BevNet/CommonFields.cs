using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class CommonFields : ICommonFields
    {
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public decimal WholesaleCost { get; set; }
        public decimal PriceSchedule1_MSRP { get; set; }
        public decimal PriceSchedule2_MinPrice { get; set; }
    }
}
