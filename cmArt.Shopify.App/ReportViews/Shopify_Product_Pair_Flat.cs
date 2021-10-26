using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.Shopify.App.ReportViews
{

    public class Shopify_Product_Pair_Flat : IShopify_Product_Pair_Flat
    {
        public string LeftCat { get; set; }
        public string LeftDescription { get; set; }
        public int LeftInvUnique { get; set; }
        public string LeftPartNumber { get; set; }
        public string RightCat { get; set; }
        public string RightDescription { get; set; }
        public int RightInvUnique { get; set; }
        public string RightPartNumber { get; set; }
    }
}
