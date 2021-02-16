using cmArt.LibIntegrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using cmArt.BevNet;


namespace UnitTestUpdateOfCommonFields
{
    [TestClass]
    public class UnitTest_Rehydrater_Features
    {
        [TestMethod]
        public void Test_simple_rehydration_scenario()
        {
            //Rehydrater<IPriceFile, ICommonFields, (string SupplierCode, string SupplierPart), PriceFileAdapter, CommonFields, IAdapter<PriceFile, ICommonFields, IPriceFile>, PriceFile> rehydrater =
            //    new Rehydrater<IPriceFile, ICommonFields, (string SupplierCode, string SupplierPart), PriceFileAdapter, CommonFields, IAdapter<PriceFile, ICommonFields, IPriceFile>, PriceFile>();
            Rehydrater
            <
                PriceFile_Clean
                , IPriceFile
                , CommonFields
                , ICommonFields
                , PriceFileAdapter
                , (string SupplierCode, string SupplierPart)
            > rehydrater = new Rehydrater
                <
                    PriceFile_Clean
                    , IPriceFile
                    , CommonFields
                    , ICommonFields
                    , PriceFileAdapter
                    , (string SupplierCode, string SupplierPart)
                >()
            ;

            // Move most of below into Rehydrater code

            const decimal NewPrice0 = (decimal)9.0;
            const decimal NewPrice1 = (decimal)9.1;
            const decimal NewPrice2 = (decimal)9.2;

            // get price records
            IPriceFile record0 = Default_PriceFile.SingleRecord();
            IPriceFile record1 = Default_PriceFile.SingleRecord();
            record1.PROD_ITEM = record1.PROD_ITEM + "_1";

            IPriceFile record2 = Default_PriceFile.SingleRecord();
            record2.PROD_ITEM = record2.PROD_ITEM + "_2";

            List<IPriceFile> PriceFileRecords = new List<IPriceFile>();
            PriceFileRecords.Add(record0);
            PriceFileRecords.Add(record1);
            PriceFileRecords.Add(record2);

            rehydrater.IntegrationRecords = PriceFileRecords;

            // get common fields with updated info
            PriceFileAdapter adapter0 = new PriceFileAdapter();
            adapter0.Init(record0);
            PriceFileAdapter adapter1 = new PriceFileAdapter();
            adapter1.Init(record1);
            PriceFileAdapter adapter2 = new PriceFileAdapter();
            adapter2.Init(record2);


            ICommonFields cf0 = new CommonFields();
            ICommonFields cf1 = new CommonFields();
            ICommonFields cf2 = new CommonFields();

            cf0.CopyFrom(adapter0);
            cf1.CopyFrom(adapter1);
            cf2.CopyFrom(adapter2);

            //// perform in rehydrater
            //// update price info
            //cf0.PriceSchedule2_MinPrice = NewPrice0;
            //cf1.PriceSchedule2_MinPrice = NewPrice1;
            //cf2.PriceSchedule2_MinPrice = NewPrice2;

            List<ICommonFields> commonFields = new List<ICommonFields>();
            commonFields.Add(cf0);
            commonFields.Add(cf1);
            commonFields.Add(cf2);

            rehydrater.CommonFieldRecords = commonFields;
            rehydrater.IntegrationRecords = PriceFileRecords;

            //// perform in rehydrater
            //adapter0.CopyFrom(cf0);
            //adapter1.CopyFrom(cf1);
            //adapter2.CopyFrom(cf2);


            Assert.AreEqual(NewPrice0, record0.BOT_PRICE);
            Assert.AreEqual(NewPrice1, record1.BOT_PRICE);
            Assert.AreEqual(NewPrice2, record2.BOT_PRICE);

        }
        [TestMethod]
        public void Test_simple_rehydration_scenario_without_Rehydrator()
        {
            const decimal NewPrice0 = (decimal)9.0;
            const decimal NewPrice1 = (decimal)9.1;
            const decimal NewPrice2 = (decimal)9.2;

            // get price records
            IPriceFile record0 = Default_PriceFile.SingleRecord();
            IPriceFile record1 = Default_PriceFile.SingleRecord();
            record1.PROD_ITEM = record1.PROD_ITEM + "_1";

            IPriceFile record2 = Default_PriceFile.SingleRecord();
            record2.PROD_ITEM = record2.PROD_ITEM + "_2";

            List<IPriceFile> PriceFileRecords = new List<IPriceFile>();
            PriceFileRecords.Add(record0);
            PriceFileRecords.Add(record1);
            PriceFileRecords.Add(record2);

            // get common fields with updated info
            PriceFileAdapter adapter0 = new PriceFileAdapter();
            adapter0.Init(record0);
            PriceFileAdapter adapter1 = new PriceFileAdapter();
            adapter1.Init(record1);
            PriceFileAdapter adapter2 = new PriceFileAdapter();
            adapter2.Init(record2);

            ICommonFields cf0 = new CommonFields();
            ICommonFields cf1 = new CommonFields();
            ICommonFields cf2 = new CommonFields();

            cf0.CopyFrom(adapter0);
            cf1.CopyFrom(adapter1);
            cf2.CopyFrom(adapter2);

            // update price info
            cf0.PriceSchedule2_MinPrice = NewPrice0;
            cf1.PriceSchedule2_MinPrice = NewPrice1;
            cf2.PriceSchedule2_MinPrice = NewPrice2;

            adapter0.CopyFrom(cf0);
            adapter1.CopyFrom(cf1);
            adapter2.CopyFrom(cf2);

            Assert.AreEqual(NewPrice0, record0.BOT_PRICE);
            Assert.AreEqual(NewPrice1, record1.BOT_PRICE);
            Assert.AreEqual(NewPrice2, record2.BOT_PRICE);
        }
        [TestMethod]
        public void Test_records_can_only_be_initialized()
        {

            Assert.IsTrue(false);
        }
    }
}
