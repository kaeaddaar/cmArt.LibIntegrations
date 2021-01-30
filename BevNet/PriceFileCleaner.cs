using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    // When we start needing to clean up data in PriceFile we can wrap it with PriceFileCleaner
    //  If we also write an extension method on IPriceFile of .CopyFrom(IPriceFile priceFileToCopy) 
    //  then it's easy to apply the clean up routine. The CopyFrom can do a MemberwiseClone()
    public class PriceFileCleaner : IPriceFile
    {
        private IPriceFile _priceFile;
        public PriceFileCleaner(IPriceFile priceFile)
        {
            _priceFile = priceFile;
        }

        public string ASST_SIZE { get => _priceFile.ASST_SIZE; set => _priceFile.ASST_SIZE = value; }
        public string BDESC { get => _priceFile.BDESC; set => _priceFile.BDESC = value; }
        public decimal BESTBOT { get => _priceFile.BESTBOT; set => _priceFile.BESTBOT = value; }
        public decimal BOT_PRICE { get => _priceFile.BOT_PRICE; set => _priceFile.BOT_PRICE = value; }
        public int BOTPERCASE { get => _priceFile.BOTPERCASE; set => _priceFile.BOTPERCASE = value; }
        public decimal CALE_SHELF { get => _priceFile.CALE_SHELF; set => _priceFile.CALE_SHELF = value; }
        public decimal CASE_PRICE { get => _priceFile.CASE_PRICE; set => _priceFile.CASE_PRICE = value; }
        public string D_TYPE1__ { get => _priceFile.D_TYPE1__; set => _priceFile.D_TYPE1__ = value; }
        public string D_TYPE2__ { get => _priceFile.D_TYPE2__; set => _priceFile.D_TYPE2__ = value; }
        public string D_TYPE3__ { get => _priceFile.D_TYPE3__; set => _priceFile.D_TYPE3__ = value; }
        public string D_TYPE4__ { get => _priceFile.D_TYPE4__; set => _priceFile.D_TYPE4__ = value; }
        public string D_TYPE5__ { get => _priceFile.D_TYPE5__; set => _priceFile.D_TYPE5__ = value; }
        public string D_TYPE6__ { get => _priceFile.D_TYPE6__; set => _priceFile.D_TYPE6__ = value; }
        public string D_TYPE7__ { get => _priceFile.D_TYPE7__; set => _priceFile.D_TYPE7__ = value; }
        public string D_TYPE8__ { get => _priceFile.D_TYPE8__; set => _priceFile.D_TYPE8__ = value; }
        public string D_TYPE9__ { get => _priceFile.D_TYPE9__; set => _priceFile.D_TYPE9__ = value; }
        public string DATE { get => _priceFile.DATE; set => _priceFile.DATE = value; }
        public decimal DEPOSIT { get => _priceFile.DEPOSIT; set => _priceFile.DEPOSIT = value; }
        public string DESCRIPTIO { get => _priceFile.DESCRIPTIO; set => _priceFile.DESCRIPTIO = value; }
        public float DISCOUNT1_ { get => _priceFile.DISCOUNT1_; set => _priceFile.DISCOUNT1_ = value; }
        public float DISCOUNT2_ { get => _priceFile.DISCOUNT2_; set => _priceFile.DISCOUNT2_ = value; }
        public float DISCOUNT3_ { get => _priceFile.DISCOUNT3_; set => _priceFile.DISCOUNT3_ = value; }
        public float DISCOUNT4_ { get => _priceFile.DISCOUNT4_; set => _priceFile.DISCOUNT4_ = value; }
        public float DISCOUNT5_ { get => _priceFile.DISCOUNT5_; set => _priceFile.DISCOUNT5_ = value; }
        public float DISCOUNT6_ { get => _priceFile.DISCOUNT6_; set => _priceFile.DISCOUNT6_ = value; }
        public float DISCOUNT7_ { get => _priceFile.DISCOUNT7_; set => _priceFile.DISCOUNT7_ = value; }
        public float DISCOUNT8_ { get => _priceFile.DISCOUNT8_; set => _priceFile.DISCOUNT8_ = value; }
        public float DISCOUNT9_ { get => _priceFile.DISCOUNT9_; set => _priceFile.DISCOUNT9_ = value; }
        public string DIV1___ { get => _priceFile.DIV1___; set => _priceFile.DIV1___ = value; }
        public string DIV10___ { get => _priceFile.DIV10___; set => _priceFile.DIV10___ = value; }
        public string DIV11___ { get => _priceFile.DIV11___; set => _priceFile.DIV11___ = value; }
        public string DIV12___ { get => _priceFile.DIV12___; set => _priceFile.DIV12___ = value; }
        public string DIV2___ { get => _priceFile.DIV2___; set => _priceFile.DIV2___ = value; }
        public string DIV3___ { get => _priceFile.DIV3___; set => _priceFile.DIV3___ = value; }
        public string DIV4___ { get => _priceFile.DIV4___; set => _priceFile.DIV4___ = value; }
        public string DIV5___ { get => _priceFile.DIV5___; set => _priceFile.DIV5___ = value; }
        public string DIV6___ { get => _priceFile.DIV6___; set => _priceFile.DIV6___ = value; }
        public string DIV7___ { get => _priceFile.DIV7___; set => _priceFile.DIV7___ = value; }
        public string DIV8___ { get => _priceFile.DIV8___; set => _priceFile.DIV8___ = value; }
        public string DIV9___ { get => _priceFile.DIV9___; set => _priceFile.DIV9___ = value; }
        public decimal FRONT_NYC { get => _priceFile.FRONT_NYC; set => _priceFile.FRONT_NYC = value; }
        public string FULLCASE { get => _priceFile.FULLCASE; set => _priceFile.FULLCASE = value; }
        public string LWBN { get => _priceFile.LWBN; set => _priceFile.LWBN = value; }
        public decimal POSTOFF { get => _priceFile.POSTOFF; set => _priceFile.POSTOFF = value; }
        public string PROD_ITEM { get => _priceFile.PROD_ITEM; set => _priceFile.PROD_ITEM = value; }
        public float QTY1__ { get => _priceFile.QTY1__; set => _priceFile.QTY1__ = value; }
        public float QTY2__ { get => _priceFile.QTY2__; set => _priceFile.QTY2__ = value; }
        public float QTY3__ { get => _priceFile.QTY3__; set => _priceFile.QTY3__ = value; }
        public float QTY4__ { get => _priceFile.QTY4__; set => _priceFile.QTY4__ = value; }
        public float QTY5__ { get => _priceFile.QTY5__; set => _priceFile.QTY5__ = value; }
        public float QTY6__ { get => _priceFile.QTY6__; set => _priceFile.QTY6__ = value; }
        public float QTY7__ { get => _priceFile.QTY7__; set => _priceFile.QTY7__ = value; }
        public float QTY8__ { get => _priceFile.QTY8__; set => _priceFile.QTY8__ = value; }
        public float QTY9__ { get => _priceFile.QTY9__; set => _priceFile.QTY9__ = value; }
        public int SECPACK { get => _priceFile.SECPACK; set => _priceFile.SECPACK = value; }
        public int SIZE { get => _priceFile.SIZE; set => _priceFile.SIZE = value; }
        public string TRUEVINT { get => _priceFile.TRUEVINT; set => _priceFile.TRUEVINT = value; }
        public string TYPE_DESC { get => _priceFile.TYPE_DESC; set => _priceFile.TYPE_DESC = value; }
        public string UNIV_CAT { get => _priceFile.UNIV_CAT; set => _priceFile.UNIV_CAT = value; }
        public string UNIV_PROD { get => _priceFile.UNIV_PROD; set => _priceFile.UNIV_PROD = value; }
        public string UPC { get => _priceFile.UPC; set => _priceFile.UPC = value; }
        public string VINTAGE { get => _priceFile.VINTAGE; set => _priceFile.VINTAGE = value; }
        public string WHOLE_NAME { get => _priceFile.WHOLE_NAME; set => _priceFile.WHOLE_NAME = value; }
        public string WHOLESALER { get => _priceFile.WHOLESALER; set => _priceFile.WHOLESALER = value; }
    }
}
