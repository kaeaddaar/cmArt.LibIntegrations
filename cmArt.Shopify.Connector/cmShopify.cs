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
        const string BaseUrl = "https://deltawaterproducts.myshopify.com";

        public static string GetPage(string lastid)
        {
            return MakeApiGetCall("/admin/api/2021-07/products.json");
        }
        private static string MakeApiGetCall(string urlCommand)
        {
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = "ed84bfc1c2687d7d6f357717fe977dd6";
            string clientSecret = "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";

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            //requestMessage.Content = content;

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
        }
        public static IEnumerable<Product_Product>GetAllShopifyRecords()
        {
            // count of records
            string strCount = MakeApiGetCall("/admin/api/2021-07/products/count.json");
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
                    tmp = MakeApiGetCall("/admin/api/2021-07/products.json" + $"?limit={pageSize}");
                }
                else
                {
                    tmp = MakeApiGetCall("/admin/api/2021-07/products.json" + $"?limit={pageSize}&since_id={lastProduct.id}");
                }
                products = (Product_Root)JsonSerializer.Deserialize(tmp, typeof(Product_Root));
                lastProduct = products.products.LastOrDefault();
                PagesOfProduct.Add(products.products);
            }
            IEnumerable<Product_Product> AllProducts = PagesOfProduct.SelectMany(prod => prod);
            return AllProducts;
        }
    }
}
