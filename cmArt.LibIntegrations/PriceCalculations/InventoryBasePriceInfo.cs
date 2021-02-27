using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.PriceCalculations
{
    public class InventoryBasePriceInfo : IInventoryBasePriceInfo
    {
        public Inventry_27 src_Inventry_27 { get; set; } // reference to original source data
        public string SKU { get; set; }
        public int PartUnique { get; set; }
        public Int16 Department { get; set; }
        public double WholesaleCost { get; set; }
        public double Freight { get; set; }
        public double Extra { get; set; }
        public double Duty { get; set; }

        public Int16 ScheduleLevel_0 { get; set; }
        public string RScheduleType_0 { get; set; }
        public double RegularPrice_0 { get; set; }
        public double RegularPrice_0_Calculated { get; set; }
        public string SScheduleType_0 { get; set; }
        public double SalePrice_0 { get; set; }
        public double SalePrice_0_Calculated { get; set; }

        public Int16 ScheduleLevel_List { get; set; }
        public string RScheduleType_List { get; set; }
        public double RegularPrice_List { get; set; }
        public double RegularPrice_List_Calculated { get; set; }
        public string SScheduleType_List { get; set; }
        public double SalePrice_List { get; set; }
        public double SalePrice_List_Calculated { get; set; }

        public Int16 ScheduleLevel_Cash { get; set; }
        public string RScheduleType_Cash { get; set; }
        public double RegularPrice_Cash { get; set; }
        public double RegularPrice_Cash_Calculated { get; set; }
        public string SScheduleType_Cash { get; set; }
        public double SalePrice_Cash { get; set; }
        public double SalePrice_Cash_Calculated { get; set; }

    }

}
