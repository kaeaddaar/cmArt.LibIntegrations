using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;


namespace cmArt.WebJaguar.App.ReportViews
{
    [DelimitedRecord(",")]
    public class S5_CommonFields_Pairs_Flat
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

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftQuantities { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Leftbarcodes { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftDescription { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftWebDescription { get; set; }
        public float Leftweight { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LeftFF22 { get; set; }

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

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightQuantities { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Rightbarcodes { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightDescription { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightWebDescription { get; set; }
        public float Rightweight { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string RightFF22 { get; set; }
    }
}
