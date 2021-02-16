using cmArt.System5.Data;
using cmArt.System5.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;


namespace UnitTestAssembleS5Inventory
{
    [TestClass]
    public class UnitTest_S5Data_ToAssembled
    {
        [TestMethod]
        public void Test_ToAssembled_empty_lists_shouldnt_fail()
        {
            S5Inventory InvRaw = new S5Inventory();
            IEnumerable<IS5InvAssembled> InvAss = InvRaw.ToAssembled();
        }

        [TestMethod]
        public void TestToAssembled_BasicScenario()
        {
            S5Inventory InvRaw = Get_Sample_S5Inventory_for_one_Inventory_Item();

            IEnumerable<IS5InvAssembled> InvAss = InvRaw.ToAssembled();
            var First = InvAss.First();

            Assert.AreEqual(12018, First.Inv.InvUnique);
            Assert.AreEqual(12018, First.AltSuplies_PerInventry_27.First().Part);
            Assert.AreEqual(12018, First.CommentsLines_PerInventry_27.First().RecordNo);
            Assert.AreEqual(12018, First.InvPrices_PerInventry_27.First().PartUnique);
            Assert.AreEqual(12018, First.StokLines_PerInventry_27.First().PartPtr);

            // right padding removed from 211545, ff2, and ANOTHERBARCODE examples below
            Assert.AreEqual("211545", First.Inv.Part);
            Assert.AreEqual("Y", First.Inv.Ecommerce);
            Assert.AreEqual("ff2", First.CommentsLines_PerInventry_27.Last().Comment);

            Assert.AreEqual(new DateTime(2020, 10, 15), First.InvPrices_PerInventry_27.Where(p => p.InvUnique == 435900).First().StartDate);
            Assert.AreEqual("ANOTHERBARCODE", First.AltSuplies_PerInventry_27.Where(a => a.AUnique == 3).First().PartNumber); Assert.AreEqual("ANOTHERBARCODE", First.AltSuplies_PerInventry_27.Where(a => a.AUnique == 3).First().PartNumber);
            Assert.AreEqual("211545", First.AltSuplies_PerInventry_27.Where(a => a.AUnique == 2).First().PartNumber);

            //435898 is the older price and should have been filtered out
            Assert.AreEqual(0, First.InvPrices_PerInventry_27.Where(p => p.InvUnique == 435898).Count());

            // additional assertions based on future scenarios for cleaned data go here

            // Filtered data like todays prices still hasn't happened
            // Calculating quantity still hasn't happened


        }

        private S5Inventory Get_Sample_S5Inventory_for_one_Inventory_Item()
        {
            List<Inventry_27> RawInventoryRecords = new List<Inventry_27>();
            List<Stok> RawStockRecords = new List<Stok>();
            List<AltSuply> RawBarcodeRecords = new List<AltSuply>();
            List<InvPrice> RawPriceHistoryRecords = new List<InvPrice>();
            List<Comments> RawFreeFormRecords = new List<Comments>();

            // Dakis not used here, but save for routines for dakis
            string DakisLine = "2764856,59f6b500 - c2e1 - 0138 - a5b8 - 00163ecd2826,Celestron,\"Omni 32mm Eyepiece - 1.25\"\"\",#93323,#211545,,Y,59.99,,N,N,N,N,\"\",\"\",0,Y,1,2,\"Temporarily out of stock or back ordered, call for availability.\",\"Temporarily out of stock or back ordered, call for availability.\",N,N,N,N,01,01,2001,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,https://cphfun.com/shop/celestron-omni-32mm-eyepiece-1-25/59f6b500-c2e1-0138-a5b8-00163ecd2826?variation=2764856,http://resources.dakiscdn.com/MBphZCHyoQtwkRf7Y3Py4A,N,N,050234933230";

            // Don't forget to escape \u0000 when pasting from raw file should read as \\u0000 when done
            string inv = "{ \"InvUnique\":12018,\"Cat\":\"100 \",\"Part\":\"211545                                            \",\"Size_1\":\"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\"Size_2\":\"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\"Size_3\":\"\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\\u0000\",\"Supplier\":0,\"Description\":\"211545 from Dakis for testing      \",\"Wholesale_1\":29,\"WholeExtra_1\":0,\"Freight_1\":0,\"Duty_1\":0,\"Foreign_1\":0,\"Country\":\" \",\"Tax\":3,\"Description2\":null,\"Item\":null,\"SuppPart\":\"211545                                            \",\"PriceQty\":0,\"MarkDeleted\":\"\\u0000\",\"Location\":null,\"Brand\":0,\"PackSize\":\"1       \",\"Units\":\"EA      \",\"Weight\":0,\"KitType\":0,\"Serial\":\"N\",\"Ecommerce\":\"Y\"}";

            string stockLineRecord1 = "{ \"StUnique\":15,\"Date\":\"2020-10-15T00:00:00\",\"Number\":\"211545                                  \",\"Description\":\"                                   \",\"PartPtr\":12018,\"ProductPtr\":0,\"PriceQty\":1,\"Department\":2,\"StockStatus\":1,\"PickQuantity\":2,\"ExpiryDate\":\"1000-01-01T00:00:00\",\"LocationPtr\":0,\"PickPriority\":0,\"Proximity\":0,\"CostQuantity\":2,\"Wholesale\":30,\"WholeExtra\":0,\"Duty\":0,\"Freight\":0,\"CurrencyCode\":0,\"CheckPtr\":0,\"Foreign\":0,\"SupplierPtr\":0,\"TrandataPtr\":0,\"Costed\":false,\"Weight\":0,\"HeaderPtr\":0,\"CostStatus\":1,\"BillPtr\":0,\"Country\":\" \"}";
            string stockLineRecord2 = "{ \"StUnique\":16,\"Date\":\"2020-10-16T00:00:00\",\"Number\":\"211545                                  \",\"Description\":\"                                   \",\"PartPtr\":12018,\"ProductPtr\":0,\"PriceQty\":1,\"Department\":2,\"StockStatus\":1,\"PickQuantity\":2,\"ExpiryDate\":\"1000-01-01T00:00:00\",\"LocationPtr\":0,\"PickPriority\":0,\"Proximity\":0,\"CostQuantity\":2,\"Wholesale\":30,\"WholeExtra\":0,\"Duty\":0,\"Freight\":0,\"CurrencyCode\":0,\"CheckPtr\":0,\"Foreign\":0,\"SupplierPtr\":0,\"TrandataPtr\":0,\"Costed\":false,\"Weight\":0,\"HeaderPtr\":0,\"CostStatus\":1,\"BillPtr\":0,\"Country\":\" \"}";
            string AltSuplyRecord1 = "{ \"AUnique\":3, \"Part\":12018, \"RecordNo\":0, \"Price\":29, \"Preferred\":\"3\", \"PartNumber\":\"ANOTHERBARCODE                                    \", \"FileNo\":3, \"Extra\":0, \"Freight\":0, \"Duty\":0, \"PackSize\":\"        \" }";
            string AltSuplyRecord2 = "{\"AUnique\":2,\"Part\":12018,\"RecordNo\":0,\"Price\":29,\"Preferred\":\"3\",\"PartNumber\":\"211545                                            \",\"FileNo\":3,\"Extra\":0,\"Freight\":0,\"Duty\":0,\"PackSize\":\"        \"}";

            string PriceSchedule0_1 = "{ \"InvUnique\":435898,\"PartUnique\":12018,\"ScheduleLevel\":0,\"RScheduleType\":\"L\",\"SScheduleType\":\"L\",\"RegularPrice\":267.5,\"SalePrice\":50,\"QuanDisc\":0,\"Department\":0,\"StartDate\":\"1000-01-01T00:00:00\",\"EndDate\":\"1000-01-01T00:00:00\"}";
            string PriceSchedule0_2 = "{\"InvUnique\":435900,\"PartUnique\":12018,\"ScheduleLevel\":0,\"RScheduleType\":\"L\",\"SScheduleType\":\"L\",\"RegularPrice\":141.3448275862069,\"SalePrice\":50,\"QuanDisc\":0,\"Department\":0,\"StartDate\":\"2020-10-15T00:00:00\",\"EndDate\":\"1000-01-01T00:00:00\"}";
            string PriceSchedule1_1 = "{ \"InvUnique\":435887,\"PartUnique\":12018,\"ScheduleLevel\":1,\"RScheduleType\":\"D\",\"SScheduleType\":\"D\",\"RegularPrice\":55,\"SalePrice\":55,\"QuanDisc\":0,\"Department\":0,\"StartDate\":\"1000-01-01T00:00:00\",\"EndDate\":\"1000-01-01T00:00:00\"}";
            string PriceSchedule1_2 = "{\"InvUnique\":435901,\"PartUnique\":12018,\"ScheduleLevel\":1,\"RScheduleType\":\"D\",\"SScheduleType\":\"D\",\"RegularPrice\":14.287755393627652,\"SalePrice\":55,\"QuanDisc\":0,\"Department\":0,\"StartDate\":\"2020-10-15T00:00:00\",\"EndDate\":\"1000-01-01T00:00:00\"}";

            string FF1 = "{ \"FileNo\":135,\"RecordNo\":12018,\"LineNo\":1,\"Comment\":\"ff1                                                         \",\"CUnique\":56304}";
            string FF2 = "{ \"FileNo\":135,\"RecordNo\":12018,\"LineNo\":2,\"Comment\":\"ff2                                                         \",\"CUnique\":56305}";


            var options = new JsonSerializerOptions // not necessary, but matches Deserializer in main project
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            RawInventoryRecords.Add(JsonSerializer.Deserialize<Inventry_27>(inv, options));
            RawStockRecords.Add(JsonSerializer.Deserialize<Stok>(stockLineRecord1, options));
            RawStockRecords.Add(JsonSerializer.Deserialize<Stok>(stockLineRecord2, options));
            RawBarcodeRecords.Add(JsonSerializer.Deserialize<AltSuply>(AltSuplyRecord1, options));
            RawBarcodeRecords.Add(JsonSerializer.Deserialize<AltSuply>(AltSuplyRecord2, options));
            RawPriceHistoryRecords.Add(JsonSerializer.Deserialize<InvPrice>(PriceSchedule0_1, options));
            RawPriceHistoryRecords.Add(JsonSerializer.Deserialize<InvPrice>(PriceSchedule0_2, options));
            RawPriceHistoryRecords.Add(JsonSerializer.Deserialize<InvPrice>(PriceSchedule1_1, options));
            RawPriceHistoryRecords.Add(JsonSerializer.Deserialize<InvPrice>(PriceSchedule1_2, options));
            RawFreeFormRecords.Add(JsonSerializer.Deserialize<Comments>(FF1, options));
            RawFreeFormRecords.Add(JsonSerializer.Deserialize<Comments>(FF2, options));

            S5Inventory InvRaw = new S5Inventory
            (
                AltSuply_Records: RawBarcodeRecords
                , Comments_Records: RawFreeFormRecords
                , Inventry_27_Records: RawInventoryRecords
                , InvPrice_Records: RawPriceHistoryRecords
                , Stok_Records: RawStockRecords
            );

            Assert.AreEqual(12018, InvRaw.Inventry_27s.First().InvUnique);
            Assert.AreEqual(12018, InvRaw.AltSupplies.First().Part);
            Assert.AreEqual(12018, InvRaw.CommentsLines.First().RecordNo);
            Assert.AreEqual(12018, InvRaw.InvPrices.First().PartUnique);
            Assert.AreEqual(12018, InvRaw.StokLines.First().PartPtr);

            Assert.AreEqual("211545".PadRight(50, ' '), InvRaw.Inventry_27s.First().Part);
            Assert.AreEqual("Y", InvRaw.Inventry_27s.First().Ecommerce);
            Assert.AreEqual("ff2".PadRight(60, ' '), InvRaw.CommentsLines.Last().Comment);

            Assert.AreEqual(new DateTime(2020, 10, 15), InvRaw.InvPrices.Where(p => p.InvUnique == 435900).First().StartDate);
            Assert.AreEqual("211545".PadRight(50, ' '), InvRaw.AltSupplies.Where(a => a.AUnique == 2).First().PartNumber);

            // additional assertions based on future scenarios go here

            return InvRaw;
        }
    }
}

