using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.LibIntegrations.ReportService
{
    [DelimitedRecord(",")]
    public class Inventory_Product_Pair_Flat : IInventory_Pair_Flat
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
    }

}
