using cmArt.LibIntegrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    // When we start needing to clean up data in PriceFile we can wrap it with PriceFileCleaner
    //  If we also write an extension method on IPriceFile of .CopyFrom(IPriceFile priceFileToCopy) 
    //  then it's easy to apply the clean up routine. The CopyFrom can do a MemberwiseClone()
    public class PriceFile_Clean : State<IPriceFile>, IPriceFile, ICopyable<IPriceFile>
    {
        public PriceFile_Clean()
        {
        }

        public IPriceFile CopyFrom(IPriceFile From)
        {
            this._state = From;
            return From;
        }

        public string ASST_SIZE { get => _state.ASST_SIZE; set => _state.ASST_SIZE = value; }
        public string BDESC { get => _state.BDESC; set => _state.BDESC = value; }
        public decimal BESTBOT { get => _state.BESTBOT; set => _state.BESTBOT = value; }
        public decimal BOT_PRICE { get => _state.BOT_PRICE; set => _state.BOT_PRICE = value; }
        public int BOTPERCASE { get => _state.BOTPERCASE; set => _state.BOTPERCASE = value; }
        public decimal CALE_SHELF { get => _state.CALE_SHELF; set => _state.CALE_SHELF = value; }
        public decimal CASE_PRICE { get => _state.CASE_PRICE; set => _state.CASE_PRICE = value; }
        public string D_TYPE1__ { get => _state.D_TYPE1__; set => _state.D_TYPE1__ = value; }
        public string D_TYPE2__ { get => _state.D_TYPE2__; set => _state.D_TYPE2__ = value; }
        public string D_TYPE3__ { get => _state.D_TYPE3__; set => _state.D_TYPE3__ = value; }
        public string D_TYPE4__ { get => _state.D_TYPE4__; set => _state.D_TYPE4__ = value; }
        public string D_TYPE5__ { get => _state.D_TYPE5__; set => _state.D_TYPE5__ = value; }
        public string D_TYPE6__ { get => _state.D_TYPE6__; set => _state.D_TYPE6__ = value; }
        public string D_TYPE7__ { get => _state.D_TYPE7__; set => _state.D_TYPE7__ = value; }
        public string D_TYPE8__ { get => _state.D_TYPE8__; set => _state.D_TYPE8__ = value; }
        public string D_TYPE9__ { get => _state.D_TYPE9__; set => _state.D_TYPE9__ = value; }
        public string DATE { get => _state.DATE; set => _state.DATE = value; }
        public decimal DEPOSIT { get => _state.DEPOSIT; set => _state.DEPOSIT = value; }
        public string DESCRIPTIO { get => _state.DESCRIPTIO; set => _state.DESCRIPTIO = value; }
        public float DISCOUNT1_ { get => _state.DISCOUNT1_; set => _state.DISCOUNT1_ = value; }
        public float DISCOUNT2_ { get => _state.DISCOUNT2_; set => _state.DISCOUNT2_ = value; }
        public float DISCOUNT3_ { get => _state.DISCOUNT3_; set => _state.DISCOUNT3_ = value; }
        public float DISCOUNT4_ { get => _state.DISCOUNT4_; set => _state.DISCOUNT4_ = value; }
        public float DISCOUNT5_ { get => _state.DISCOUNT5_; set => _state.DISCOUNT5_ = value; }
        public float DISCOUNT6_ { get => _state.DISCOUNT6_; set => _state.DISCOUNT6_ = value; }
        public float DISCOUNT7_ { get => _state.DISCOUNT7_; set => _state.DISCOUNT7_ = value; }
        public float DISCOUNT8_ { get => _state.DISCOUNT8_; set => _state.DISCOUNT8_ = value; }
        public float DISCOUNT9_ { get => _state.DISCOUNT9_; set => _state.DISCOUNT9_ = value; }
        public string DIV1___ { get => _state.DIV1___; set => _state.DIV1___ = value; }
        public string DIV10___ { get => _state.DIV10___; set => _state.DIV10___ = value; }
        public string DIV11___ { get => _state.DIV11___; set => _state.DIV11___ = value; }
        public string DIV12___ { get => _state.DIV12___; set => _state.DIV12___ = value; }
        public string DIV2___ { get => _state.DIV2___; set => _state.DIV2___ = value; }
        public string DIV3___ { get => _state.DIV3___; set => _state.DIV3___ = value; }
        public string DIV4___ { get => _state.DIV4___; set => _state.DIV4___ = value; }
        public string DIV5___ { get => _state.DIV5___; set => _state.DIV5___ = value; }
        public string DIV6___ { get => _state.DIV6___; set => _state.DIV6___ = value; }
        public string DIV7___ { get => _state.DIV7___; set => _state.DIV7___ = value; }
        public string DIV8___ { get => _state.DIV8___; set => _state.DIV8___ = value; }
        public string DIV9___ { get => _state.DIV9___; set => _state.DIV9___ = value; }
        public decimal FRONT_NYC { get => _state.FRONT_NYC; set => _state.FRONT_NYC = value; }
        public string FULLCASE { get => _state.FULLCASE; set => _state.FULLCASE = value; }
        public string LWBN { get => _state.LWBN; set => _state.LWBN = value; }
        public decimal POSTOFF { get => _state.POSTOFF; set => _state.POSTOFF = value; }
        public string PROD_ITEM { get => _state.PROD_ITEM; set => _state.PROD_ITEM = value; }
        public float QTY1__ { get => _state.QTY1__; set => _state.QTY1__ = value; }
        public float QTY2__ { get => _state.QTY2__; set => _state.QTY2__ = value; }
        public float QTY3__ { get => _state.QTY3__; set => _state.QTY3__ = value; }
        public float QTY4__ { get => _state.QTY4__; set => _state.QTY4__ = value; }
        public float QTY5__ { get => _state.QTY5__; set => _state.QTY5__ = value; }
        public float QTY6__ { get => _state.QTY6__; set => _state.QTY6__ = value; }
        public float QTY7__ { get => _state.QTY7__; set => _state.QTY7__ = value; }
        public float QTY8__ { get => _state.QTY8__; set => _state.QTY8__ = value; }
        public float QTY9__ { get => _state.QTY9__; set => _state.QTY9__ = value; }
        public int SECPACK { get => _state.SECPACK; set => _state.SECPACK = value; }
        public int SIZE { get => _state.SIZE; set => _state.SIZE = value; }
        public string TRUEVINT { get => _state.TRUEVINT; set => _state.TRUEVINT = value; }
        public string TYPE_DESC { get => _state.TYPE_DESC; set => _state.TYPE_DESC = value; }
        public string UNIV_CAT { get => _state.UNIV_CAT; set => _state.UNIV_CAT = value; }
        public string UNIV_PROD { get => _state.UNIV_PROD; set => _state.UNIV_PROD = value; }
        public string UPC { get => _state.UPC; set => _state.UPC = value; }
        public string VINTAGE { get => _state.VINTAGE; set => _state.VINTAGE = value; }
        public string WHOLE_NAME { get => _state.WHOLE_NAME; set => _state.WHOLE_NAME = value; }
        public string WHOLESALER { get => _state.WHOLESALER; set => _state.WHOLESALER = value; }

    }
}
