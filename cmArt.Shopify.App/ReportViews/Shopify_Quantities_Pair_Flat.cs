﻿using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.Shopify.App.ReportViews
{
    [DelimitedRecord(",")]
    public class Shopify_Quantities_Pair_Flat : IShopify_Quantities_Pair_Flat
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
        public string LeftQuantities { get; set; }

        public int RightInvUnique { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightCat { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightPartNumber { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightQuantities { get; set; }
    }
}
