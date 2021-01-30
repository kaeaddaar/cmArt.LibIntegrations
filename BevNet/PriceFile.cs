using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.BevNet
{
    [DelimitedRecord(",")]
    public class PriceFile : IPriceFile
    {
        //[FieldQuoted]
        //[FieldOptional]
        //[FieldNullValue(typeof(string), "")]

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string UNIV_PROD { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string BDESC { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DESCRIPTIO { get; set; }

        public int SIZE { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string VINTAGE { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string UNIV_CAT { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string LWBN { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(decimal), "0.0")]
        public decimal BESTBOT { get; set; }

        [FieldNullValue(typeof(string), "")]
        public string DATE { get; set; }

        public int BOTPERCASE { get; set; }

        [FieldNullValue(typeof(int), "0")]
        public int SECPACK { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string WHOLESALER { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string PROD_ITEM { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string UPC { get; set; }
        public decimal CASE_PRICE { get; set; }
        public decimal BOT_PRICE { get; set; }
        public decimal FRONT_NYC { get; set; }
        public decimal POSTOFF { get; set; }

        //[FieldQuoted]
        //[FieldNullValue(typeof(string), "")]
        //public string SPEC_PRICE { get; set; }

        //[FieldQuoted]
        //[FieldNullValue(typeof(string), "")]
        //public string RIPCODE { get; set; }

        #region --  qty 1 --
        public float QTY1__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE1__ { get; set; }

        public float DISCOUNT1_ { get; set; }
        #endregion --  qty 1 --

        #region --  qty 2 --
        public float QTY2__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE2__ { get; set; }

        public float DISCOUNT2_ { get; set; }
        #endregion --  qty 2 --

        #region --  qty 3 --
        public float QTY3__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE3__ { get; set; }

        public float DISCOUNT3_ { get; set; }
        #endregion --  qty 3 --

        #region --  qty 4 --
        public float QTY4__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE4__ { get; set; }

        public float DISCOUNT4_ { get; set; }
        #endregion --  qty 4 --

        #region --  qty 5 --
        public float QTY5__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE5__ { get; set; }

        public float DISCOUNT5_ { get; set; }
        #endregion --  qty 5 --

        #region --  qty 6 --
        public float QTY6__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE6__ { get; set; }

        public float DISCOUNT6_ { get; set; }
        #endregion --  qty 6 --

        #region --  qty 7 --
        public float QTY7__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE7__ { get; set; }

        public float DISCOUNT7_ { get; set; }
        #endregion --  qty 7 --

        #region --  qty 8 --
        public float QTY8__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE8__ { get; set; }

        public float DISCOUNT8_ { get; set; }
        #endregion --  qty 8 --

        #region --  qty 9 --
        public float QTY9__ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string D_TYPE9__ { get; set; }

        public float DISCOUNT9_ { get; set; }
        #endregion --  qty 9 --

        //[FieldQuoted]
        //[FieldNullValue(typeof(string), "")]
        //public string RIPS { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV1___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV2___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV3___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV4___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV5___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV6___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV7___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV8___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV9___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV10___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV11___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string DIV12___ { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string ASST_SIZE { get; set; }

        public decimal CALE_SHELF { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string TRUEVINT { get; set; }

        [FieldNullValue(typeof(string), "")]
        public string FULLCASE { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string TYPE_DESC { get; set; }

        public decimal DEPOSIT { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string WHOLE_NAME { get; set; }


    }
}
