using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.PriceCalculations
{
    public interface IInventoryBasePriceInfo
    {
        short Department { get; set; }
        double Duty { get; set; }
        double Extra { get; set; }
        double Freight { get; set; }
        int PartUnique { get; set; }
        double RegularPrice_0 { get; set; }
        double RegularPrice_0_Calculated { get; set; }
        double RegularPrice_Cash { get; set; }
        double RegularPrice_Cash_Calculated { get; set; }
        double RegularPrice_List { get; set; }
        double RegularPrice_List_Calculated { get; set; }
        string RScheduleType_0 { get; set; }
        string RScheduleType_Cash { get; set; }
        string RScheduleType_List { get; set; }
        double SalePrice_0 { get; set; }
        double SalePrice_0_Calculated { get; set; }
        double SalePrice_Cash { get; set; }
        double SalePrice_Cash_Calculated { get; set; }
        double SalePrice_List { get; set; }
        double SalePrice_List_Calculated { get; set; }
        short ScheduleLevel_0 { get; set; }
        short ScheduleLevel_Cash { get; set; }
        short ScheduleLevel_List { get; set; }
        string SKU { get; set; }
        string SScheduleType_0 { get; set; }
        string SScheduleType_Cash { get; set; }
        string SScheduleType_List { get; set; }
        double WholesaleCost { get; set; }
    }

}
