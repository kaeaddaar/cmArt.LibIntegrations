using cmArt.BevNet;
using cmArt.LibIntegrations.CsvFileReaderService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_FileReaderGeneric_by_validating_BevNet_file
    {
        [TestMethod]
        public void Test_Read_BevNet_File_With_Header_And_Three_Records()
        {
            IEnumerable<PriceFile> priceFile;
            priceFile = FileReaderGeneric<PriceFile>.ReadFile("C:\\_P\\Tools\\cmArt.LibIntegrations\\" +
                "IntegrationTest_cmArt.LibIntegrations\\BevNet_Small.csv");
            Assert.AreEqual(3, priceFile.Count());
        }

        [TestMethod]
        public void ValidateContents_Of_BevNet_File_With_Header_And_First_Of_Three_Records()
        {
            IEnumerable<PriceFile> priceFile;
            priceFile = FileReaderGeneric<PriceFile>.ReadFile("C:\\_P\\Tools\\cmArt.LibIntegrations\\" +
                "IntegrationTest_cmArt.LibIntegrations\\BevNet_Small.csv");
            Assert.AreEqual(3, priceFile.Count());

            // validate contents of record 1
            PriceFile record = priceFile.FirstOrDefault();
            Assert.AreEqual("010SK", record.UNIV_PROD);
            Assert.AreEqual("LEALTANZA  RIOJA CRIANZA", record.BDESC);
            Assert.AreEqual("LEALTANZA CRIANZA", record.DESCRIPTIO);
            Assert.AreEqual(750, record.SIZE);
            Assert.AreEqual("", record.VINTAGE);
            Assert.AreEqual("645", record.UNIV_CAT);
            Assert.AreEqual("W", record.LWBN);
            Assert.AreEqual((decimal)13.4100, record.BESTBOT);
            Assert.AreEqual("01/01/2021", record.DATE);
            Assert.AreEqual(12, record.BOTPERCASE);
            Assert.AreEqual(0, record.SECPACK);
            Assert.AreEqual("EDER", record.WHOLESALER);
            Assert.AreEqual("00018", record.PROD_ITEM);
            Assert.AreEqual("", record.UPC);
            Assert.AreEqual((decimal)160.9200, record.CASE_PRICE);
            Assert.AreEqual((decimal)16.99, record.BOT_PRICE);
            Assert.AreEqual((decimal)25.4900, record.FRONT_NYC);
            Assert.AreEqual((decimal)18.0000, record.POSTOFF);
            Assert.AreEqual(0, record.QTY1__);
            Assert.AreEqual(" ", record.D_TYPE1__);
            Assert.AreEqual(0.0000, record.DISCOUNT1_);
            Assert.AreEqual(0, record.QTY2__);
            Assert.AreEqual(" ", record.D_TYPE2__);
            Assert.AreEqual(0.0000, record.DISCOUNT2_);
            Assert.AreEqual(0, record.QTY3__);
            Assert.AreEqual(" ", record.D_TYPE3__);
            Assert.AreEqual(0.0000, record.DISCOUNT3_);
            Assert.AreEqual(0, record.QTY4__);
            Assert.AreEqual(" ", record.D_TYPE4__);
            Assert.AreEqual(0.0000, record.DISCOUNT4_);
            Assert.AreEqual(0, record.QTY5__);
            Assert.AreEqual(" ", record.D_TYPE5__);
            Assert.AreEqual(0.0000, record.DISCOUNT5_);
            Assert.AreEqual(0, record.QTY6__);
            Assert.AreEqual(" ", record.D_TYPE6__);
            Assert.AreEqual(0.0000, record.DISCOUNT6_);
            Assert.AreEqual(0, record.QTY7__);
            Assert.AreEqual(" ", record.D_TYPE7__);
            Assert.AreEqual(0.0000, record.DISCOUNT7_);
            Assert.AreEqual(0, record.QTY8__);
            Assert.AreEqual(" ", record.D_TYPE8__);
            Assert.AreEqual(0.0000, record.DISCOUNT8_);
            Assert.AreEqual(0, record.QTY9__);
            Assert.AreEqual(" ", record.D_TYPE9__);
            Assert.AreEqual(0.0000, record.DISCOUNT9_);
            Assert.AreEqual("", record.DIV1___);
            Assert.AreEqual("", record.DIV2___);
            Assert.AreEqual("", record.DIV3___);
            Assert.AreEqual("", record.DIV4___);
            Assert.AreEqual("", record.DIV5___);
            Assert.AreEqual("", record.DIV6___);
            Assert.AreEqual("", record.DIV7___);
            Assert.AreEqual("", record.DIV8___);
            Assert.AreEqual("", record.DIV9___);
            Assert.AreEqual("", record.DIV10___);
            Assert.AreEqual("", record.DIV11___);
            Assert.AreEqual("", record.DIV12___);
            Assert.AreEqual("", record.ASST_SIZE);
            Assert.AreEqual((decimal)25.49, record.CALE_SHELF);
            Assert.AreEqual("", record.TRUEVINT);
            Assert.AreEqual("F", record.FULLCASE);
            Assert.AreEqual("Spanish Still Wine", record.TYPE_DESC);
            Assert.AreEqual((decimal)0.00, record.DEPOSIT);
            Assert.AreEqual("Eder Brothers", record.WHOLE_NAME);

            // validate contents of record 2
            //record = priceFile.Skip(1).FirstOrDefault();
            //Assert.AreEqual("", record.UNIV_PROD);
            //Assert.AreEqual("", record.BDESC);
            //Assert.AreEqual("", record.DESCRIPTIO);
            //Assert.AreEqual("", record.SIZE);
            //Assert.AreEqual("", record.VINTAGE);
            //Assert.AreEqual("", record.UNIV_CAT);
            //Assert.AreEqual("", record.LWBN);
            //Assert.AreEqual("", record.BESTBOT);
            //Assert.AreEqual("", record.DATE);
            //Assert.AreEqual("", record.BOTPERCASE);
            //Assert.AreEqual("", record.SECPACK);
            //Assert.AreEqual("", record.WHOLESALER);
            //Assert.AreEqual("", record.PROD_ITEM);
            //Assert.AreEqual("", record.UPC);
            //Assert.AreEqual("", record.CASE_PRICE);
            //Assert.AreEqual("", record.BOT_PRICE);
            //Assert.AreEqual("", record.FRONT_NYC);
            //Assert.AreEqual("", record.POSTOFF);
            //Assert.AreEqual("", record.QTY1__);
            //Assert.AreEqual("", record.D_TYPE1__);
            //Assert.AreEqual("", record.DISCOUNT1_);
            //Assert.AreEqual("", record.QTY2__);
            //Assert.AreEqual("", record.D_TYPE2__);
            //Assert.AreEqual("", record.DISCOUNT2_);
            //Assert.AreEqual("", record.QTY3__);
            //Assert.AreEqual("", record.D_TYPE3__);
            //Assert.AreEqual("", record.DISCOUNT3_);
            //Assert.AreEqual("", record.QTY4__);
            //Assert.AreEqual("", record.D_TYPE4__);
            //Assert.AreEqual("", record.DISCOUNT4_);
            //Assert.AreEqual("", record.QTY5__);
            //Assert.AreEqual("", record.D_TYPE5__);
            //Assert.AreEqual("", record.DISCOUNT5_);
            //Assert.AreEqual("", record.QTY6__);
            //Assert.AreEqual("", record.D_TYPE6__);
            //Assert.AreEqual("", record.DISCOUNT6_);
            //Assert.AreEqual("", record.QTY7__);
            //Assert.AreEqual("", record.D_TYPE7__);
            //Assert.AreEqual("", record.DISCOUNT7_);
            //Assert.AreEqual("", record.QTY8__);
            //Assert.AreEqual("", record.D_TYPE8__);
            //Assert.AreEqual("", record.DISCOUNT8_);
            //Assert.AreEqual("", record.QTY9__);
            //Assert.AreEqual("", record.D_TYPE9__);
            //Assert.AreEqual("", record.DISCOUNT9_);
            //Assert.AreEqual("", record.DIV1___);
            //Assert.AreEqual("", record.DIV2___);
            //Assert.AreEqual("", record.DIV3___);
            //Assert.AreEqual("", record.DIV4___);
            //Assert.AreEqual("", record.DIV5___);
            //Assert.AreEqual("", record.DIV6___);
            //Assert.AreEqual("", record.DIV7___);
            //Assert.AreEqual("", record.DIV8___);
            //Assert.AreEqual("", record.DIV9___);
            //Assert.AreEqual("", record.DIV10___);
            //Assert.AreEqual("", record.DIV11___);
            //Assert.AreEqual("", record.DIV12___);
            //Assert.AreEqual("", record.ASST_SIZE);
            //Assert.AreEqual("", record.CALE_SHELF);
            //Assert.AreEqual("", record.TRUEVINT);
            //Assert.AreEqual("", record.FULLCASE);
            //Assert.AreEqual("", record.TYPE_DESC);
            //Assert.AreEqual("", record.DEPOSIT);
            //Assert.AreEqual("", record.WHOLE_NAME);

            // validate contents of record 3
            //record = priceFile.Skip(2).FirstOrDefault();
            //Assert.AreEqual("", record.UNIV_PROD);
            //Assert.AreEqual("", record.BDESC);
            //Assert.AreEqual("", record.DESCRIPTIO);
            //Assert.AreEqual("", record.SIZE);
            //Assert.AreEqual("", record.VINTAGE);
            //Assert.AreEqual("", record.UNIV_CAT);
            //Assert.AreEqual("", record.LWBN);
            //Assert.AreEqual("", record.BESTBOT);
            //Assert.AreEqual("", record.DATE);
            //Assert.AreEqual("", record.BOTPERCASE);
            //Assert.AreEqual("", record.SECPACK);
            //Assert.AreEqual("", record.WHOLESALER);
            //Assert.AreEqual("", record.PROD_ITEM);
            //Assert.AreEqual("", record.UPC);
            //Assert.AreEqual("", record.CASE_PRICE);
            //Assert.AreEqual("", record.BOT_PRICE);
            //Assert.AreEqual("", record.FRONT_NYC);
            //Assert.AreEqual("", record.POSTOFF);
            //Assert.AreEqual("", record.QTY1__);
            //Assert.AreEqual("", record.D_TYPE1__);
            //Assert.AreEqual("", record.DISCOUNT1_);
            //Assert.AreEqual("", record.QTY2__);
            //Assert.AreEqual("", record.D_TYPE2__);
            //Assert.AreEqual("", record.DISCOUNT2_);
            //Assert.AreEqual("", record.QTY3__);
            //Assert.AreEqual("", record.D_TYPE3__);
            //Assert.AreEqual("", record.DISCOUNT3_);
            //Assert.AreEqual("", record.QTY4__);
            //Assert.AreEqual("", record.D_TYPE4__);
            //Assert.AreEqual("", record.DISCOUNT4_);
            //Assert.AreEqual("", record.QTY5__);
            //Assert.AreEqual("", record.D_TYPE5__);
            //Assert.AreEqual("", record.DISCOUNT5_);
            //Assert.AreEqual("", record.QTY6__);
            //Assert.AreEqual("", record.D_TYPE6__);
            //Assert.AreEqual("", record.DISCOUNT6_);
            //Assert.AreEqual("", record.QTY7__);
            //Assert.AreEqual("", record.D_TYPE7__);
            //Assert.AreEqual("", record.DISCOUNT7_);
            //Assert.AreEqual("", record.QTY8__);
            //Assert.AreEqual("", record.D_TYPE8__);
            //Assert.AreEqual("", record.DISCOUNT8_);
            //Assert.AreEqual("", record.QTY9__);
            //Assert.AreEqual("", record.D_TYPE9__);
            //Assert.AreEqual("", record.DISCOUNT9_);
            //Assert.AreEqual("", record.DIV1___);
            //Assert.AreEqual("", record.DIV2___);
            //Assert.AreEqual("", record.DIV3___);
            //Assert.AreEqual("", record.DIV4___);
            //Assert.AreEqual("", record.DIV5___);
            //Assert.AreEqual("", record.DIV6___);
            //Assert.AreEqual("", record.DIV7___);
            //Assert.AreEqual("", record.DIV8___);
            //Assert.AreEqual("", record.DIV9___);
            //Assert.AreEqual("", record.DIV10___);
            //Assert.AreEqual("", record.DIV11___);
            //Assert.AreEqual("", record.DIV12___);
            //Assert.AreEqual("", record.ASST_SIZE);
            //Assert.AreEqual("", record.CALE_SHELF);
            //Assert.AreEqual("", record.TRUEVINT);
            //Assert.AreEqual("", record.FULLCASE);
            //Assert.AreEqual("", record.TYPE_DESC);
            //Assert.AreEqual("", record.DEPOSIT);
            //Assert.AreEqual("", record.WHOLE_NAME);

        }
    }
}
