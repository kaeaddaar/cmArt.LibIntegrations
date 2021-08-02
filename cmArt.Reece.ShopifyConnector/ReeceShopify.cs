using cmArt.Shopify.Connector.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;


namespace cmArt.Reece.ShopifyConnector
{
    public class ReeceShopify
    {
        // Get all records from shopify
        // Store records from shopify
        const string BaseUrl = "https://aquadragonservices.com/pcr/apitest/index.php";

        private static string MakeApiPostCall_Unsecured(string urlCommand, string content)
        {
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = "ed84bfc1c2687d7d6f357717fe977dd6";
            //string clientSecret = "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";

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = new StringContent(content);

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
        }

        private static string MakeApiGetCall_Unsecured(string urlCommand)
        {
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = "ed84bfc1c2687d7d6f357717fe977dd6";
            //string clientSecret = "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";

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            ////requestMessage.Content = content;

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
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
        public static string Products_Sync()
        {
            string results = MakeApiGetCall_Unsecured("/product/shopify") ?? string.Empty;
            return results;
        }
        public static string Products_DeleteAll()
        {
            //get all products
            IEnumerable<Shopify_Product> allProducts = GetAllShopify_Products();

            //delete all of them
            string results = Products_Delete(allProducts);
            return results;
        }
        public static string Products_Delete(IEnumerable<Shopify_Product> NewProducts)
        {
            List<Shopify_Product> prods = new List<Shopify_Product>(NewProducts);
            string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
            string results = MakeApiPostCall_Unsecured("/product/delete/", strEditProducts);
            return results;
        }
        public static string Products_Add(IEnumerable<Shopify_Product> NewProducts)
        {
            List<Shopify_Product> prods = new List<Shopify_Product>(NewProducts);
            string strNewProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
            string results = MakeApiPostCall_Unsecured("/product/add/", strNewProducts) ?? string.Empty;
            return results;
        }
        public static string Products_Edit(IEnumerable<Shopify_Product> NewProducts)
        {
            List<Shopify_Product> prods = new List<Shopify_Product>(NewProducts);
            string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
            string results = MakeApiPostCall_Unsecured("/product/edit/", strEditProducts);
            return results;
        }
        public static IEnumerable<Shopify_Product> GetAllShopify_Products()
        {
            Products_Sync();
            string results = MakeApiGetCall_Unsecured("/product/list");
            List<tmpShopify_Product> Data = new List<tmpShopify_Product>();
            try
            {
                Data = (List<tmpShopify_Product>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Product>));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Data.Select(p => p.AsShopify_Product());
        }
        public static IEnumerable<Shopify_Prices> GetAllShopify_Prices()
        {
            string results = MakeApiGetCall_Unsecured("/discount/list");

            List<Shopify_Prices> Data = (List<Shopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<Shopify_Prices>));

            return Data;
        }
        public static IEnumerable<tmpShopify_Prices> GetAlltmpShopify_Prices()
        {
            string results = MakeApiGetCall_Unsecured("/discount/list");

            List<tmpShopify_Prices> Data = (List<tmpShopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Prices>));

            return Data;
        }
        public static IEnumerable<Shopify_Quantities> GetAllShopify_Quantities()
        {
            string results = MakeApiGetCall_Unsecured("/inventory/list");
            List<Shopify_Quantities> Data = new List<Shopify_Quantities>();
            try
            {
                Data = (List<Shopify_Quantities>)JsonSerializer.Deserialize(results, typeof(List<Shopify_Quantities>));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Data = Data ?? new List<Shopify_Quantities>();
            return Data;
        }
        public static IEnumerable<tmpShopify_Quantities> GetAlltmpShopify_Quantities()
        {
            string results = MakeApiGetCall_Unsecured("/inventory/list");
            results = results.Replace("\"id\":null,", string.Empty);
            List<tmpShopify_Quantities> Data = new List<tmpShopify_Quantities>();
            try
            {
                Data = (List<tmpShopify_Quantities>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Quantities>));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Data = Data ?? new List<tmpShopify_Quantities>();
            foreach(tmpShopify_Quantities qtys in Data)
            {
                qtys.FixShopifyLocations();
            }
            return Data;
        }
    }

}
