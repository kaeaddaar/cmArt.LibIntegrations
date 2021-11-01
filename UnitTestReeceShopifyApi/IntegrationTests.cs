using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.Reece.ShopifyConnector;
using System.Collections.Generic;
using System.Linq;


namespace UnitTestReeceShopifyApi
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void TestSampleParts()
        {
            string SamplePartOne = "SamplePartOne";
            ReeceShopify.Products_Sync();
            IEnumerable<Shopify_Product> ShopifyProducts = ReeceShopify.GetAllShopify_Products();
            Shopify_Product Sample1 = ShopifyProducts.Where(p => p.PartNumber == SamplePartOne).FirstOrDefault();
            if (Sample1 == null)
            {
                Sample1 = new Shopify_Product();
                Sample1.Cat = "000";
                Sample1.Description = "Sample1 Description";
                Sample1.InvUnique = 1;
                Sample1.PartNumber = "Sample1";
                Sample1.WholesaleCost = 100;
                List<Shopify_Product> tmp = new List<Shopify_Product>();
                tmp.Add(Sample1);
                ReeceShopify.Products_Add(tmp);
            }
            else
            {
                List<Shopify_Product> ProductsToDelete = new List<Shopify_Product>();
                ProductsToDelete.Add(Sample1);
                ReeceShopify.Products_Delete(ProductsToDelete);
                throw new System.Exception($"The Part \"{SamplePartOne}\" already existed, but shouldn't exist.");
            }

            ReeceShopify.Products_Sync();
            IEnumerable<Shopify_Product> UpdatedProducts = ReeceShopify.GetAllShopify_Products();

        }
    }
}
