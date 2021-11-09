using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ReportService
{
    public interface IInventory_Pair_Flat
    {
        string LeftCat { get; set; }
        string LeftDescription { get; set; }
        int LeftInvUnique { get; set; }
        string LeftPartNumber { get; set; }
        string RightCat { get; set; }
        string RightDescription { get; set; }
        int RightInvUnique { get; set; }
        string RightPartNumber { get; set; }
        string LeftPrices { get; set; }
        string RightPrices { get; set; }
        string LeftQuantities { get; set; }
        string RightQuantities { get; set; }

    }
}
