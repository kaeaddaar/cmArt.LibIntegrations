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
        public static IEnumerable<Shopify_Product> GetAllShopify_Products()
        {
            string results = MakeApiGetCall_Unsecured("/product/list");

            List<Shopify_Product> Data = (List<Shopify_Product>)JsonSerializer.Deserialize(results, typeof(List<Shopify_Product>));

            return Data;
        }
        public static IEnumerable<Shopify_Prices> GetAllShopify_Prices()
        {
            string results = MakeApiGetCall_Unsecured("/discount/list");

            List<Shopify_Prices> Data = (List<Shopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<Shopify_Prices>));

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
    }

}
