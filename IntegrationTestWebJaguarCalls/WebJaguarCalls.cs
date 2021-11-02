using cmArt.LibIntegrations.SerializationService;
using cmArt.WebJaguar.Connector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;


namespace IntegrationTestWebJaguarCalls
{
    [TestClass]
    public class WebJaguarCalls
    {
        [TestMethod]
        public void Product_Add_Getting_Id_Back_and_Accessing_Product() // Product Add doesn't show on inventory search until index ran, can only grab them by unique
                                  // (may broken as well)
        {
            //comment out the exception below when you actually want to run the test, and then uncomment it afterwards.
            //throw new NotImplementedException("This is an integration test and should not be ran normally as the WebJaguar API" +
            //    " can't clean up the added record afterwards.");

            WebJaguarConnector wj = new WebJaguarConnector();
            List<Product_Root> newRecords = new List<Product_Root>();
            Product_Root ToAdd = GetSampleProduct();
            newRecords.Add(ToAdd);
            string results = wj.Products_Add(newRecords);
            ResponseMessage msg = (ResponseMessage)System.Text.Json.JsonSerializer.Deserialize(results, typeof(ResponseMessage));
            string strNum = string.Empty;
            if (msg.message.Contains("successfully created"))
            {
                IEnumerable<char> numsOnly = msg.message.Where(x => char.IsNumber(x));
                numsOnly.ToList().ForEach(x => strNum += x);
            }
            int id;
            int.TryParse(strNum, out id);

            string GetResults = wj.Product_Get(id);
            //Product_Root AddedProd = (Product_Root)JsonSerializer.Deserialize(GetResults, typeof(Product_Root));
            JsonDocument doc = JsonDocument.Parse(GetResults);
            JsonElement root = doc.RootElement;
            JsonElement prod = root.GetProperty("product");
            JsonElement elemSku = prod.GetProperty("sku");
            string sku = elemSku.GetString();
            Assert.AreEqual(ToAdd.sku, sku);

            // Test the serialization as well
            Product_Root AddedProd = (Product_Root)JsonSerializer.Deserialize(prod.GetRawText(), typeof(Product_Root));
            Assert.AreEqual(ToAdd.sku, AddedProd.sku);

            // cleanup and delete the added product manually
            // TIP: When trying to use this code in the App we have to track the unique id until a sync happens

        }
        private Product_Root GetSampleProduct()
        {
            string json = @"{
    ""supplierIds"": [
        1,
        2
    ],
    ""name"": ""Marlboro Smooth Menthol Box"",
    ""sku"": ""cm12612-5"",
    ""note"": ""string"",
    ""masterSku"": """",
    ""slaveCount"": 1,
    ""slaveLowestPrice"": 2.2,
    ""slaveHighestPrice"": 2.2,
    ""upc"": ""02830324,028200130303,855266"",
    ""feedFreeze"": false,
    ""shortDesc"": ""Marlboro Smooth Menthol Box"",
    ""longDesc"": ""Marlboro Smooth Menthol Box"",
    ""weight"": 70,
    ""msrp"":  68,
    ""hideMsrp"": false,
    ""unitPrice"": 1,
    ""priceByCustomer"": false,
    ""catIds"": [
        1,
        2
    ],
    ""categoryIds"": ""001"",
    ""catalogPrice"": 2.2,
    ""price1"": 12.2,
    ""price2"": 12,
    ""price3"": 11.8,
    ""price4"": 1.1,
    ""price5"": 1.1,
    ""price6"": 1.1,
    ""price7"": 1.1,
    ""price8"": 1.1,
    ""price9"": 1.1,
    ""price10"": 1.1,
    ""cost1"": 1,
    ""cost2"": 1,
    ""cost3"": 1,
    ""cost4"": 1,
    ""cost5"": 1,
    ""cost6"": 1,
    ""cost7"": 1,
    ""cost8"": 1,
    ""cost9"": 1,
    ""cost10"": 1,
    ""discount1"": 1,
    ""discount2"": 1,
    ""discount3"": 1,
    ""discount4"": 1,
    ""discount5"": 1,
    ""discount6"": 1,
    ""discount7"": 1,
    ""discount8"": 1,
    ""discount9"": 1,
    ""discount10"": 1,
    ""discountPercent1"": false,
    ""discountPercent2"": false,
    ""discountPercent3"": false,
    ""discountPercent4"": false,
    ""discountPercent5"": false,
    ""discountPercent6"": false,
    ""discountPercent7"": false,
    ""discountPercent8"": false,
    ""discountPercent9"": false,
    ""discountPercent10"": false,
    ""marginPercent1"": 1.1,
    ""marginPercent2"": 1,
    ""marginPercent3"": 1,
    ""marginPercent4"": 1,
    ""marginPercent5"": 1,
    ""marginPercent6"": 1,
    ""marginPercent7"": 1,
    ""marginPercent8"": 1,
    ""marginPercent9"": 1,
    ""marginPercent10"": 1,
    ""markupPercent1"": 1.1,
    ""markupPercent2"": 1,
    ""markupPercent3"": 1,
    ""markupPercent4"": 1,
    ""markupPercent5"": 1,
    ""markupPercent6"": 1,
    ""markupPercent7"": 1,
    ""markupPercent8"": 1,
    ""markupPercent9"": 1,
    ""markupPercent10"": 1,
    ""qtyBreak1"": 4,
    ""qtyBreak2"": 7,
    ""qtyBreak3"": 1,
    ""qtyBreak4"": 1,
    ""qtyBreak5"": 1,
    ""qtyBreak6"": 1,
    ""qtyBreak7"": 1,
    ""qtyBreak8"": 1,
    ""qtyBreak9"": 1,
    ""priceTable1"": 68.29,
    ""priceTable2"": 68,
    ""priceTable3"": 70.3387,
    ""priceTable4"": 68.29,
    ""priceTable5"": 1.1,
    ""priceTable6"": 1.1,
    ""priceTable7"": 1.1,
    ""priceTable8"": 1.1,
    ""priceTable9"": 1.1,
    ""priceTable10"": 1.1,
    ""priceCasePack"": false,
    ""priceCasePackQty"": 1,
    ""priceCasePack1"": 2.2,
    ""priceCasePack2"": 1.1,
    ""priceCasePack3"": 1.1,
    ""priceCasePack4"": 1.1,
    ""priceCasePack5"": 1.1,
    ""priceCasePack6"": 1.1,
    ""priceCasePack7"": 1.1,
    ""priceCasePack8"": 1.1,
    ""priceCasePack9"": 1.1,
    ""priceCasePack10"": 1.1,
    ""quote"": false,
    ""price"": [
        {
                ""amt"": 10,
            ""discountAmt"": 1,
            ""qtyFrom"": 0,
            ""qtyTo"": 4,
            ""caseAmt"": 10,
            ""caseDiscountAmt"": 2,
            ""cost"": 10
        },
        {
                ""amt"": 10,
            ""discountAmt"": 2,
            ""qtyFrom"": 5,
            ""qtyTo"": 9,
            ""caseAmt"": 9,
            ""caseDiscountAmt"": 3,
            ""cost"": 10
        }
    ],
    ""field1"": ""sss"",
    ""field2"": ""Box"",
    ""field3"": ""w"",
    ""field4"": ""w"",
    ""field5"": ""10"",
    ""field6"": ""w"",
    ""field7"": ""100's"",
    ""field8"": ""w"",
    ""field9"": ""w"",
    ""field10"": ""w"",
    ""field11"": ""w"",
    ""field12"": ""w"",
    ""field13"": ""w"",
    ""field14"": ""w"",
    ""field15"": ""w"",
    ""field16"": ""w"",
    ""field17"": ""w"",
    ""field18"": ""w"",
    ""field19"": ""w"",
    ""field20"": ""w"",
    ""field21"": ""w"",
    ""field22"": ""w"",
    ""field23"": ""w"",
    ""field24"": ""w"",
    ""field25"": ""w"",
    ""field26"": ""w"",
    ""field27"": ""w"",
    ""field28"": ""w"",
    ""field29"": ""w"",
    ""field30"": ""w"",
    ""field31"": ""w"",
    ""field32"": ""w"",
    ""field33"": ""w"",
    ""field34"": ""w"",
    ""field35"": ""w"",
    ""field36"": ""w"",
    ""field37"": ""w"",
    ""field38"": ""w"",
    ""field39"": ""w"",
    ""field40"": ""w"",
    ""field41"": ""w"",
    ""field42"": ""w"",
    ""field43"": ""w"",
    ""field44"": ""w"",
    ""field45"": ""w"",
    ""field46"": ""w"",
    ""field47"": ""w"",
    ""field48"": ""w"",
    ""field49"": ""w"",
    ""field50"": ""w"",
    ""field51"": ""w"",
    ""field52"": ""w"",
    ""field53"": ""w"",
    ""field54"": ""w"",
    ""field55"": ""w"",
    ""field56"": ""w"",
    ""field57"": ""w"",
    ""field58"": ""w"",
    ""field59"": ""w"",
    ""field60"": ""w"",
    ""field61"": ""w"",
    ""field62"": ""w"",
    ""field63"": ""w"",
    ""field64"": ""w"",
    ""field65"": ""w"",
    ""field66"": ""w"",
    ""field67"": ""w"",
    ""field68"": ""w"",
    ""field69"": ""w"",
    ""field70"": ""w"",
    ""field71"": ""w"",
    ""field72"": ""w"",
    ""field73"": ""w"",
    ""field74"": ""w"",
    ""field75"": ""w"",
    ""field76"": ""w"",
    ""field77"": ""w"",
    ""field78"": ""w"",
    ""field79"": ""w"",
    ""field80"": ""w"",
    ""field81"": ""w"",
    ""field82"": ""w"",
    ""field83"": ""w"",
    ""field84"": ""w"",
    ""field85"": ""w"",
    ""field86"": ""w"",
    ""field87"": ""w"",
    ""field88"": ""w"",
    ""field89"": ""w"",
    ""field90"": ""w"",
    ""field91"": ""w"",
    ""field92"": ""w"",
    ""field93"": ""w"",
    ""field94"": ""w"",
    ""field95"": ""w"",
    ""field96"": ""w"",
    ""field97"": ""w"",
    ""field98"": ""w"",
    ""field99"": ""w"",
    ""field100"": ""w"",
    ""keywords"": ""product"",
    ""kitParts"": [
        {
                ""kitPartsSku"": ""sku6"",
            ""quantity"": 2
        }
    ],
    ""kitCost"": 46,
    ""salesTagId"": 1,
    ""caseContent"": 1,
    ""packing"": ""sss"",
    ""cubicSize"": 2.2,
    ""minimumQty"": 2,
    ""incrementalQty"": 2,
    ""loginRequire"": true,
    ""hidePrice"": false,
    ""altLoginRequire"": false,
    ""recommendedList"": ""sdswed"",
    ""recommendedListTitle"": ""ada"",
    ""recommendedList1"": ""asda"",
    ""recommendedListTitle1"": ""dada"",
    ""recommendedList2"": ""asda"",
    ""recommendedListTitle2"": ""asda"",
    ""recommendedListDisplay"": """",
    ""alsoConsider"": ""sku10-1"",
    ""rank"": 1,
    ""searchRank"": 1,
    ""hideHeader"": false,
    ""hideTopBar"": false,
    ""hideLeftBar"": true,
    ""hideRightBar"": false,
    ""hideFooter"": false,
    ""hideBreadCrumbs"": false,
    ""headTag"": ""asa"",
    ""htmlAddToCart"": ""asa"",
    ""productLayout"": """",
    ""addToList"": false,
    ""addToPresentation"": false,
    ""boxSize"": 1,
    ""boxExtraAmt"": 1.1,
    ""type"": """",
    ""optionIndex"": 1,
    ""temperature"": 1,
    ""compare"": false,
    ""taxable"": true,
    ""enableSpecialTax"": false,
    ""searchable"": true,
    ""optionCode"": ""as"",
    ""negInventory"": false,
    ""lowInventory"": 1,
    ""desiredInventory"": 2,
    ""showNegInventory"": false,
    ""orderPendingQty"": 1,
    ""purchasePendingQty"": 1,
    ""inventoryItem"": false,
    ""loyaltyPoint"": 3.3,
    ""customShippingEnabled"": true,
    ""numCustomLines"": 1,
    ""customLineCharacter"": 1,
    ""crossItemsCode"": ""sa"",
    ""active"": true,
    ""tab1"": """",
    ""tab1Content"": """",
    ""tab2"": """",
    ""tab2Content"": """",
    ""tab3"": """",
    ""tab3Content"": """",
    ""tab4"": """",
    ""tab4Content"": """",
    ""tab5"": """",
    ""tab5Content"": """",
    ""tab6"": """",
    ""tab6Content"": """",
    ""tab7"": """",
    ""tab7Content"": """",
    ""tab8"": """",
    ""tab8Content"": """",
    ""tab9"": """",
    ""tab9Content"": """",
    ""tab10"": """",
    ""tab10Content"": """",
    ""manufactureName"": """",
    ""enableRate"": true,
    ""feed"": ""s"",
    ""deal"": ""s"",
    ""packageL"": 222,
    ""packageW"": 222,
    ""packageH"": 222,
    ""upsMaxItemsInPackage"": 1,
    ""uspsMaxItemsInPackage"": 1,
    ""endQtyPricing"": false,
    ""retainOnCart"": true,
    ""siteMapPriority"": 0.5,
    ""defaultSupplierId"": 0,
    ""cost"": 1.1,
    ""costPercent"": false,
    ""defaultSupplierSku"": """",
    ""defaultSupplierName"": """",
    ""defaultSupplierAccountNumber"": """",
    ""eventProtection"": 0,
    ""productLevel"": 1,
    ""groupSpecialPrice"": 1,
    ""caseUnitTitle"": """",
    ""productType"": """",
    ""discountedCost"": 11,
    ""discountedPrice"": 11,
    ""limitQty"": 1,
    ""selectedFieldGroupIndex"": 1,
    ""qbIncomeAccountType"": """",
    ""qbExpenseAccountType"": """",
    ""decoSku"": """",
    ""productCustomerMarkupTitle"": """",
    ""configLabel"": ""product"",
    ""primaryLocation"": {
                ""locationName"": """",
        ""warehouse_id"": 1,
        ""warehouseZone"": """",
        ""aisle"": """",
        ""bin"": """",
        ""level"": """"
    },
    ""locationMinAmount"": 0,
    ""locationDesiredAmount"": 0,
    ""commissionable"": false,
    ""amazonCategoryId"": 2,
    ""amazonSubCategoryId"": 2,
    ""amazonCategoryId1"": 1,
    ""amazonSubCategoryId1"": 3,
    ""hideProduct"": false,
    ""aliasSku"": ""cmasa"",
    ""pushedToAccounting"": false,
    ""hazardousTier"": 2,
    ""qtyOnPO"": 3,
    ""groundShipping"": false,
    ""backOrderFlag"": false
}";
            Product_Root tmp = (Product_Root)System.Text.Json.JsonSerializer.Deserialize(json, typeof(Product_Root));
            return tmp;
        }
    }

}
