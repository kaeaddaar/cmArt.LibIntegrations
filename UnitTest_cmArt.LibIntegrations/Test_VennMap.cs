using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using cmArt.LibIntegrations.VennMapService;
using cmArt.System5.Data;
using cmArt.System5.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_VennMap
    {
        class InfoType
        {
            public int UniqueId { get; set; }
            public string Value { get; set; }
        }
        [TestMethod]
        public void TestUsingBasicScenario()
        {
            IEnumerable<InfoType> infoRecords = GetSampleInfoTypeRecords();
            IEnumerable<IS5InvAssembled> S5Records = GetSampleS5InvAssembledRecords();
            Func<InfoType, int> fInfoIndex = (info) => info.UniqueId;
            Func<IS5InvAssembled, int> fS5Index = (S5) => S5.Inv.InvUnique;
            VennMap_InvAss<InfoType, int> map = new VennMap_InvAss<InfoType, int>(infoRecords, S5Records, fInfoIndex, fS5Index);

            Assert.AreEqual(1, map.Both_Ecomm.First().Item1.UniqueId);
            Assert.AreEqual(1, map.Both_Ecomm.First().Item2.Inv.InvUnique);

            Assert.IsNull(map.InvOnly_Ecomm.First().Item1);
            Assert.AreEqual(2, map.InvOnly_Ecomm.First().Item2.Inv.InvUnique);

            Assert.AreEqual(3, map.Both_NoEcomm.First().Item1.UniqueId);
            Assert.AreEqual(3, map.Both_NoEcomm.First().Item2.Inv.InvUnique);

            Assert.IsNull(map.InvOnly_NoEcomm.First().Item1);
            Assert.AreEqual(4, map.InvOnly_NoEcomm.First().Item2.Inv.InvUnique);

            Assert.AreEqual(5, map.TOnly.First().Item1.UniqueId);
            Assert.IsNull(map.TOnly.First().Item2);

        }
        private IEnumerable<IS5InvAssembled> GetSampleS5InvAssembledRecords()
        {
            List<IS5InvAssembled> s5InvAssembledObjs = new List<IS5InvAssembled>();

            #region Create S5InvAssembledObj Item1, Ecomm=Y Exists on Web
            IS5InvAssembled tmpItem1 = Get_Sample_S5Inventory_for_one_Inventory_Item().ToAssembled().First();
            IS5InvAssembled Item1 = new S5InvAssembled
            (
                tmpItem1.Inv
                , tmpItem1.InvPrices_PerInventry_27
                , tmpItem1.StokLines_PerInventry_27
                , tmpItem1.CommentsLines_PerInventry_27
                , tmpItem1.AltSuplies_PerInventry_27
            );
            Item1.Inv.InvUnique = 1;
            Item1.Inv.Part = "One";
            Item1.InvPrices_PerInventry_27.Select(c => { c.PartUnique = Item1.Inv.InvUnique; return c; });
            Item1.StokLines_PerInventry_27.Select(c => { c.PartPtr = Item1.Inv.InvUnique; return c; });
            Item1.CommentsLines_PerInventry_27.Select(c => { c.RecordNo = Item1.Inv.InvUnique; return c; });
            Item1.AltSuplies_PerInventry_27.Select(c => { c.RecordNo = Item1.Inv.InvUnique; return c; });
            Item1.Inv.Ecommerce = "Y";
            #endregion Create S5InvAssembledObj Item1, Ecomm=Y Exists on Web

            #region Create S5InvAssembledObj Item2, Ecomm=Y Doesn't Exist on Web
            IS5InvAssembled tmpItem2 = Get_Sample_S5Inventory_for_one_Inventory_Item().ToAssembled().First();
            IS5InvAssembled Item2 = new S5InvAssembled
            (
                tmpItem2.Inv
                , tmpItem2.InvPrices_PerInventry_27
                , tmpItem2.StokLines_PerInventry_27
                , tmpItem2.CommentsLines_PerInventry_27
                , tmpItem2.AltSuplies_PerInventry_27
            );
            Item2.Inv.InvUnique = 2;
            Item2.Inv.Part = "Two";
            Item2.InvPrices_PerInventry_27.Select(c => { c.PartUnique = Item2.Inv.InvUnique; return c; });
            Item2.StokLines_PerInventry_27.Select(c => { c.PartPtr = Item2.Inv.InvUnique; return c; });
            Item2.CommentsLines_PerInventry_27.Select(c => { c.RecordNo = Item2.Inv.InvUnique; return c; });
            Item2.AltSuplies_PerInventry_27.Select(c => { c.RecordNo = Item2.Inv.InvUnique; return c; });
            Item2.Inv.Ecommerce = "Y";
            #endregion Create S5InvAssembledObj Item2, Ecomm=Y Doesn't Exist on Web

            #region Create S5InvAssembledObj Item3, Ecomm=N Exists on Web
            IS5InvAssembled tmpItem3 = Get_Sample_S5Inventory_for_one_Inventory_Item().ToAssembled().First();
            IS5InvAssembled Item3 = new S5InvAssembled
            (
                tmpItem3.Inv
                , tmpItem3.InvPrices_PerInventry_27
                , tmpItem3.StokLines_PerInventry_27
                , tmpItem3.CommentsLines_PerInventry_27
                , tmpItem3.AltSuplies_PerInventry_27
            );
            Item3.Inv.InvUnique = 3;
            Item3.Inv.Part = "Three";
            Item3.InvPrices_PerInventry_27.Select(c => { c.PartUnique = Item3.Inv.InvUnique; return c; });
            Item3.StokLines_PerInventry_27.Select(c => { c.PartPtr = Item3.Inv.InvUnique; return c; });
            Item3.CommentsLines_PerInventry_27.Select(c => { c.RecordNo = Item3.Inv.InvUnique; return c; });
            Item3.AltSuplies_PerInventry_27.Select(c => { c.RecordNo = Item3.Inv.InvUnique; return c; });
            Item3.Inv.Ecommerce = "N";
            #endregion Create S5InvAssembledObj Item3, Ecomm=N Exists on Web

            #region Create S5InvAssembledObj Item4, Ecomm=N Doesn't Exist on Web
            IS5InvAssembled tmpItem4 = Get_Sample_S5Inventory_for_one_Inventory_Item().ToAssembled().First();
            IS5InvAssembled Item4 = new S5InvAssembled
            (
                tmpItem4.Inv
                , tmpItem4.InvPrices_PerInventry_27
                , tmpItem4.StokLines_PerInventry_27
                , tmpItem4.CommentsLines_PerInventry_27
                , tmpItem4.AltSuplies_PerInventry_27
            );
            Item4.Inv.InvUnique = 4;
            Item4.Inv.Part = "Four";
            Item4.InvPrices_PerInventry_27.Select(c => { c.PartUnique = Item4.Inv.InvUnique; return c; });
            Item4.StokLines_PerInventry_27.Select(c => { c.PartPtr = Item4.Inv.InvUnique; return c; });
            Item4.CommentsLines_PerInventry_27.Select(c => { c.RecordNo = Item4.Inv.InvUnique; return c; });
            Item4.AltSuplies_PerInventry_27.Select(c => { c.RecordNo = Item4.Inv.InvUnique; return c; });
            Item4.Inv.Ecommerce = "N";
            #endregion Create S5InvAssembledObj Item4, Ecomm=N Doesn't Exist on Web

            s5InvAssembledObjs.Add(Item1);
            s5InvAssembledObjs.Add(Item2);
            s5InvAssembledObjs.Add(Item3);
            s5InvAssembledObjs.Add(Item4);

            return s5InvAssembledObjs;
        }
        private IEnumerable<InfoType> GetSampleInfoTypeRecords()
        {
            List<InfoType> infoTypes = new List<InfoType>();
            infoTypes.Add(new InfoType() { UniqueId = 1, Value = "One" });// Exists in S5 and Ecomm=Y
            infoTypes.Add(new InfoType() { UniqueId = 3, Value = "Three" });// Exists in S5 and Ecomm=N
            infoTypes.Add(new InfoType() { UniqueId = 5, Value = "Five" });// Doesn't Exist in S5
            return infoTypes;
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
            Assert.AreEqual(12018, InvRaw.AltSuplyLines.First().Part);
            Assert.AreEqual(12018, InvRaw.CommentsLines.First().RecordNo);
            Assert.AreEqual(12018, InvRaw.InvPrices.First().PartUnique);
            Assert.AreEqual(12018, InvRaw.StokLines.First().PartPtr);

            Assert.AreEqual("211545".PadRight(50, ' '), InvRaw.Inventry_27s.First().Part);
            Assert.AreEqual("Y", InvRaw.Inventry_27s.First().Ecommerce);
            Assert.AreEqual("ff2".PadRight(60, ' '), InvRaw.CommentsLines.Last().Comment);

            Assert.AreEqual(new DateTime(2020, 10, 15), InvRaw.InvPrices.Where(p => p.InvUnique == 435900).First().StartDate);
            Assert.AreEqual("211545".PadRight(50, ' '), InvRaw.AltSuplyLines.Where(a => a.AUnique == 2).First().PartNumber);

            // additional assertions based on future scenarios go here

            return InvRaw;
        }

    }

}
