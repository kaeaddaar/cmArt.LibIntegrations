using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.LibIntegrations.ReportService
{
    [DelimitedRecord(",")]
    public class Inventory_Pair_Flat : IInventory_Pair_Flat
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
        public string LeftDescription { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftPrices { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftQuantities { get; set; }
        public string LeftBarcodes { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftFF22 { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftWebDescription { get; set; }
        public float LeftWeight { get; set; }

        public int RightInvUnique { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightCat { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightPartNumber { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightDescription { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightPrices { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightQuantities { get; set; }
        public string RightBarcodes { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightFF22 { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightWebDescription { get; set; }
        public float RightWeight { get; set; }
    }

}
