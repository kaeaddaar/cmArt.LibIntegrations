using cmArt.Shopify.Connector.Data;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;

namespace cmArt.Shopify.Connector
{
    public class cmShopify
    {
        // Get all records from shopify
        // Store records from shopify

        public static IEnumerable<Product_Product>GetAllShopifyRecords()
        {
            // count of records
            string strCount = ApiCalls.MakeApiGetCall("/admin/api/2021-07/products/count.json");
            Product_Count objCount = (Product_Count)JsonSerializer.Deserialize(strCount, typeof(Product_Count));
            int count = objCount.count;

            int pageSize = 100; // max 250
            int pages = count / pageSize;
            bool IsPartialPage = (count % pageSize) > 0;
            if(IsPartialPage)
            {
                pages = pages + 1;
            }
            string lastId = string.Empty;

            Product_Root products = new Product_Root();
            Product_Product lastProduct = new Product_Product();
            List<List<Product_Product>> PagesOfProduct = new List<List<Product_Product>>();
            for ( int i = 0; i < pages; i++)
            {
                string tmp = string.Empty;
                if (i == 0)
                {
                    tmp = ApiCalls.MakeApiGetCall("/admin/api/2021-07/products.json" + $"?limit={pageSize}");
                }
                else
                {
                    tmp = ApiCalls.MakeApiGetCall("/admin/api/2021-07/products.json" + $"?limit={pageSize}&since_id={lastProduct.id}");
                }
                products = (Product_Root)JsonSerializer.Deserialize(tmp, typeof(Product_Root));
                lastProduct = products.products.LastOrDefault();
                PagesOfProduct.Add(products.products);
            }
            IEnumerable<Product_Product> AllProducts = PagesOfProduct.SelectMany(prod => prod);
            return AllProducts;
        }
        public static IEnumerable<Product_Product>Product_Add()
        {
            throw new NotImplementedException();
        }
    }
}
