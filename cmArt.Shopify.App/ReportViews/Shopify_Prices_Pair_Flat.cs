using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.Shopify.App.ReportViews
{
    [DelimitedRecord(",")]
    public class Shopify_Prices_Pair_Flat : IShopify_Prices_Pair_Flat
    {
        public int LeftInvUnique { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftCat { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftPartNumber { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftPrices { get; set; }
        //decimal LeftWholesaleCost { get; set; }

        public int RightInvUnique { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightCat { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightPartNumber { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightPrices { get; set; }
        //decimal RightWholesaleCost { get; set; }
    }
}
