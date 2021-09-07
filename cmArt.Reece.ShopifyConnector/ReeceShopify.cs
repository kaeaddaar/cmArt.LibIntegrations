﻿using cmArt.Shopify.Connector.Data;
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
        public static string Quantities_Add(IEnumerable<Shopify_Quantities> QuantitiesList)
        {
            string strQuantitiesList = string.Empty;
            string results = string.Empty;
            foreach (var qtys in QuantitiesList)
            {

                try
                {
                    List<Shopify_Quantities> tmpQuantitiesList = new List<Shopify_Quantities>();
                    tmpQuantitiesList.Add(qtys);
                    strQuantitiesList = System.Text.Json.JsonSerializer.Serialize(tmpQuantitiesList.ToList(), typeof(List<Shopify_Quantities>));
                    results += MakeApiPostCall_Unsecured("/inventory/create", strQuantitiesList);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error in QUantities_Add, likely a Serialization issue or issue with call to /inventory/create for " +
                        "InvUnique: {qtys.InvUnique}. Message: " + e.Message);
                }

            }
            return results;
        }
        public static string Quantities_Edit(IEnumerable<Shopify_Quantities> QuantitiesList)
        {
            string strQuantitiesList = string.Empty;
            string results = string.Empty;
            foreach (var qtys in QuantitiesList)
            {

                try
                {
                    List<Shopify_Quantities> tmpQuantitiesList = new List<Shopify_Quantities>();
                    tmpQuantitiesList.Add(qtys);
                    strQuantitiesList = System.Text.Json.JsonSerializer.Serialize(tmpQuantitiesList.ToList(), typeof(List<Shopify_Quantities>));
                    results += MakeApiPostCall_Unsecured("/inventory/edit", strQuantitiesList);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error in QUantities_Edit, likely a Serialization issue or issue with call to /inventory/edit for " +
                        "InvUnique: {qtys.InvUnique}. Message: " + e.Message);
                }

            }
            return results;
        }
        public static string Prices_Add(IEnumerable<Shopify_Prices> PricesList)
        {
            IEnumerable<Shopify_Prices> _PricesList = PricesList ?? new List<Shopify_Prices>();
            string results = string.Empty;
            foreach (Shopify_Prices prices in PricesList)
            {
                string strPricesList = string.Empty;
                try
                {
                    List<Shopify_Prices> tmpPrices = new List<Shopify_Prices>();
                    tmpPrices.Add(prices);
                    strPricesList = System.Text.Json.JsonSerializer.Serialize(tmpPrices, typeof(List<Shopify_Prices>));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to serialize PricesList in preparation to call to /discounts/edit. Message: " + e.Message);
                }
                try
                {
                    results += MakeApiPostCall_Unsecured("/discount/create", strPricesList);
                }
                catch (Exception e)
                {
                    string msg = "Error in call to /discount/add. Message: " + e.Message;
                    Console.WriteLine(msg);
                }
            }
            return results;
        }
        public static string Prices_Edit(IEnumerable<Shopify_Prices> PricesList)
        {
            IEnumerable<Shopify_Prices> _PricesList = PricesList ?? new List<Shopify_Prices>();
            string results = string.Empty;
            foreach (Shopify_Prices prices in PricesList)
            {
                string strPricesList = string.Empty;
                try
                {
                    List<Shopify_Prices> tmpPrices = new List<Shopify_Prices>();
                    tmpPrices.Add(prices);
                    strPricesList = System.Text.Json.JsonSerializer.Serialize(tmpPrices, typeof(List<Shopify_Prices>));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to serialize PricesList in preparation to call to /discounts/edit. Message: " + e.Message);
                }
                results += MakeApiPostCall_Unsecured("/discount/edit", strPricesList);
            }
            return results;
        }
        public static string Products_Sync()
        {
            string results = MakeApiGetCall_Unsecured("/product/shopify") ?? string.Empty;
            return results;
        }
        public static string Discounts_Sync()
        {
            string results = MakeApiGetCall_Unsecured("/discount/shopify") ?? string.Empty;
            return results;
        }
        public static string Inventory_Sync()
        {
            string results = MakeApiGetCall_Unsecured("/inventory/shopify") ?? string.Empty;
            return results;
        }
        public static string Products_DeleteAll()
        {
            try
            {
                //get all products
                IEnumerable<Shopify_Product> allProducts = GetAllShopify_Products();

                //delete all of them
                string results = Products_Delete(allProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Product_DeleteAll. Messge: " + e.Message;
                Console.WriteLine(msg);
                return msg;
            }
        }
        public static string Products_Delete(IEnumerable<Shopify_Product> NewProducts)
        {
            try
            { 
                List<Shopify_Product> prods = new List<Shopify_Product>(NewProducts);
                string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                string results = MakeApiPostCall_Unsecured("/product/delete/", strEditProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Delete. Messge: " + e.Message;
                Console.WriteLine(msg);
                return msg;
            }
        }
        public static string Products_Add(IEnumerable<Shopify_Product> NewProducts)
        {
            int pageSize = 10;
            int page = 0;
            int count = NewProducts.Count();
            int StartAt = page * pageSize;
            string results = string.Empty;
            do
            {
                try
                {
                    List<Shopify_Product> prods = NewProducts.Skip(StartAt).Take(pageSize).ToList();
                    string strNewProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                    results += MakeApiPostCall_Unsecured("/product/add/", strNewProducts) ?? string.Empty;
                }
                catch (Exception e)
                {
                    string msg = "Error in Products_Add. Messge: " + e.Message;
                    Console.WriteLine(msg);
                    return msg;
                }
                page++;
                StartAt = page * pageSize;
            } while (StartAt <= count);
            return results;
        }
        public static string Products_Edit(IEnumerable<Shopify_Product> NewProducts)
        {
            try
            { 
                List<Shopify_Product> prods = new List<Shopify_Product>(NewProducts);
                string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                string results = MakeApiPostCall_Unsecured("/product/edit/", strEditProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Edit. Messge: " + e.Message;
                Console.WriteLine(msg);
                return msg;
            }
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
            Discounts_Sync();
            try
            {
                string results = MakeApiGetCall_Unsecured("/discount/list");

                List<Shopify_Prices> Data = (List<Shopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<Shopify_Prices>));

                return Data;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Edit. Messge: " + e.Message;
                Console.WriteLine(msg);
                return new List<Shopify_Prices>();
            }
        }
        public static IEnumerable<tmpShopify_Prices> GetAlltmpShopify_Prices()
        {
            Discounts_Sync();
            try
            {
                string results = MakeApiGetCall_Unsecured("/discount/list");

                List<tmpShopify_Prices> Data = (List<tmpShopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Prices>));

                return Data;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Edit. Messge: " + e.Message;
                Console.WriteLine(msg);
                return new List<tmpShopify_Prices>();
            }
        }
        public static IEnumerable<Shopify_Quantities> GetAllShopify_Quantities()
        {
            Inventory_Sync();
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
            Inventory_Sync();
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