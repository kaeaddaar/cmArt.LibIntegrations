using cmArt.BevNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestUpdateOfCommonFields
{
    public static class Default_PriceFile
    {
        public static IPriceFile SingleRecord()
        {
            PriceFile record = new PriceFile();
            record.UNIV_PROD = "010SK";
            record.BDESC = "LEALTANZA  RIOJA CRIANZA";
            record.DESCRIPTIO = "LEALTANZA CRIANZA";
            record.SIZE = 750;
            record.VINTAGE = "";
            record.UNIV_CAT = "645";
            record.LWBN = "W";
            record.BESTBOT = (decimal)13.4100;
            record.DATE = "01/01/2021";
            record.BOTPERCASE = 12;
            record.SECPACK = 0;
            record.WHOLESALER = "EDER";
            record.PROD_ITEM = "00018";
            record.UPC = "";
            record.CASE_PRICE = (decimal)160.9200;
            record.BOT_PRICE = (decimal)16.99;
            record.FRONT_NYC = (decimal)25.4900;
            record.POSTOFF = (decimal)18.0000;
            record.QTY1__ = 0;
            record.D_TYPE1__ = " ";
            record.DISCOUNT1_ = 0.0000F;
            record.QTY2__ = 0;
            record.D_TYPE2__ = " ";
            record.DISCOUNT2_ = 0.0000F;
            record.QTY3__ = 0;
            record.D_TYPE3__ = " ";
            record.DISCOUNT3_ = 0.0000F;
            record.QTY4__ = 0;
            record.D_TYPE4__ = " ";
            record.DISCOUNT4_ = 0.0000F;
            record.QTY5__ = 0;
            record.D_TYPE5__ = " ";
            record.DISCOUNT5_ = 0.0000F;
            record.QTY6__ = 0;
            record.D_TYPE6__ = " ";
            record.DISCOUNT6_ = 0.0000F;
            record.QTY7__ = 0;
            record.D_TYPE7__ = " ";
            record.DISCOUNT7_ = 0.0000F;
            record.QTY8__ = 0;
            record.D_TYPE8__ = " ";
            record.DISCOUNT8_ = 0.0000F;
            record.QTY9__ = 0;
            record.D_TYPE9__ = " ";
            record.DISCOUNT9_ = 0.0000F;
            record.DIV1___ = "";
            record.DIV2___ = "";
            record.DIV3___ = "";
            record.DIV4___ = "";
            record.DIV5___ = "";
            record.DIV6___ = "";
            record.DIV7___ = "";
            record.DIV8___ = "";
            record.DIV9___ = "";
            record.DIV10___ = "";
            record.DIV11___ = "";
            record.DIV12___ = "";
            record.ASST_SIZE = "";
            record.CALE_SHELF = (decimal)25.49;
            record.TRUEVINT = "";
            record.FULLCASE = "F";
            record.TYPE_DESC = "Spanish Still Wine";
            record.DEPOSIT = (decimal)0.00;
            record.WHOLE_NAME = "Eder Brothers";

            return record;
        }
    }
}
