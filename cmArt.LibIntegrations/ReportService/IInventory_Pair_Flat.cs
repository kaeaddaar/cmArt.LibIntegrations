using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ReportService
{
    public interface IInventory_Pair_Flat
    {
        int LeftInvUnique { get; set; }
        string LeftCat { get; set; }
        string LeftPartNumber { get; set; }
        string LeftDescription { get; set; }
        string LeftPrices { get; set; }
        string LeftQuantities { get; set; }
        string LeftBarcodes { get; set; }
        string LeftFF22 { get; set; }
        string LeftWebDescription { get; set; }
        float LeftWeight { get; set; }

        int RightInvUnique { get; set; }
        string RightCat { get; set; }
        string RightPartNumber { get; set; }
        string RightDescription { get; set; }
        string RightPrices { get; set; }
        string RightQuantities { get; set; }
        string RightBarcodes { get; set; }
        string RightFF22 { get; set; }
        string RightWebDescription { get; set; }
        float RightWeight { get; set; }

    }
}
