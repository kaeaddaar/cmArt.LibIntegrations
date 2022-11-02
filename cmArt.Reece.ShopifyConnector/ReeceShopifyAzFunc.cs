using cmArt.Shopify.Connector.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;


namespace cmArt.Reece.ShopifyConnector
{
    /// <summary>
    /// This class is used when talking to Reece's API through a Blazor app. Essentially I proxy the calls through an azure function.
    /// WARNING: Prior to 2022-10-30 this is set up to run through localhost calls and I still haven't changed this. 
    /// </summary>
    public class ReeceShopifyAzFunc
     {
        protected static ILogger _logger;
        protected static ILogger _fileLogger;
        const int Api_Timeout_Max = 25;
        //private static StaticSettings settings;
        //private static void LoadConfigs()
        //{
        //    IConfigurationRoot config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings_connector.json", optional: false, reloadOnChange: true)
        //        .Build();

        //    settings = new StaticSettings(config);

        //    //logger_ToFile = new LogToFile();
        //    //logger_ToFile.Init(settings.LogfilePath + "\\Integration_LogFile.txt");

        //}
        private static void LogInfo(string msg)
        {
            string _msg = msg ?? string.Empty;
            bool LoggerExists = _logger != null;
            if (LoggerExists)
            {
                _logger.LogInformation(msg);
            }
            Console.WriteLine(msg);
        }
        private static void LogApiCalls(string msg)
        {
            string _msg = msg ?? string.Empty;
            bool fileLoggerExists = _fileLogger != null;
            if (fileLoggerExists)
            {
                _fileLogger.LogInformation(msg);
            }
            Console.WriteLine(msg);
        }
        public static void AddLogger(ILogger logger, ILogger fileLogger = null)
        {
            _logger = logger;
            _fileLogger = fileLogger;
        }
        private static void LogInfo_Write(string msg)
        {
            string _msg = msg ?? string.Empty;
            bool LoggerExists = _logger != null;
            if (LoggerExists)
            {
                _logger.LogInformation(msg);
            }
            Console.Write(msg);
        }

        public static string RetryAPICall(Func<string> MakeCall)
        {
            string result = string.Empty;
            int initDelay = 50;
            int delay = initDelay;
            int Seconds_30 = 1000 * 30;
            while (result == string.Empty)
            {
                try
                {
                    result = MakeCall();
                }
                catch (Exception e)
                {
                    if (delay > 2000)
                    {
                        LogInfo_Write(">2k");
                        System.Threading.Thread.Sleep(Seconds_30);
                        delay = initDelay;
                        result = string.Empty;
                    }
                    else
                    {
                        LogInfo_Write(".");
                        delay = delay * 2;
                        System.Threading.Thread.Sleep(delay);
                        result = string.Empty;
                    }
                }
            }

            return result;
        }
        public static async Task<string> MakeApiPostCall(string urlCommand, string content, string BaseUrl = "http://localhost:7071/api/MakeApiPostCall")
        {
            string _BaseUrl = BaseUrl ?? String.Empty;
            //LoadConfigs();
            //_BaseUrl = settings.PortalApiUrl ?? String.Empty;

            LogApiCalls("urlCommand(Post): " + urlCommand);
            LogApiCalls("content: " + content);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(_BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = "shopravi";
            string clientSecret = "H9pPG9yW58cMP45e";

            // Async Call
            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = new StringContent(content);

            //make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            var response = await client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            LogApiCalls("responseBody: " + responseBody);
            return responseBody;

        }
        public static string MakeApiGetCall_Unsecured(string urlCommand, string BaseUrl = "http://localhost:7071/api/MakeApiGetCall")
        {
            string _BaseUrl = BaseUrl ?? String.Empty;
            //LoadConfigs();
            //_BaseUrl = settings.PortalApiUrl ?? String.Empty;

            LogApiCalls("urlCommand(Get - Unsecured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(_BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            LogApiCalls("responseBody: " + responseBody);
            return responseBody;
        }
        public static async Task<HttpResponseMessage> MakeApiGetCallGeneric
        (
            string ApiConnectorData_Json
            , string ApiCallData_Json
            , string BaseUrl = "http://localhost:7071/api/MakeShopifyApiGetCall"
        )
        {
            LogInfo("BaseUrl: " + BaseUrl);
            string _BaseUrl = BaseUrl ?? String.Empty;
            LogInfo("_BaseUrl: " + _BaseUrl);
            LogInfo("ApiConnectorData_Json: " + ApiConnectorData_Json);
            LogInfo("ApiCallData_Json: " + ApiCallData_Json);

            //LogApiCalls("urlCommand(Get - Secured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(_BaseUrl);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = clintIdIn ?? string.Empty;
            //string clientSecret = clientSecretIn ?? string.Empty;

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Headers.Add("ApiConnectorData_Json", ApiConnectorData_Json);
            requestMessage.Headers.Add("ApiCallData_Json", ApiCallData_Json);

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(850); // just over half a second

                var requestMessage_Delay = new HttpRequestMessage(HttpMethod.Get, baseUri);
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage_Delay.Headers.Add("ApiConnectorData_Json", ApiConnectorData_Json);
                requestMessage_Delay.Headers.Add("ApiCallData_Json", ApiCallData_Json);

                client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

                baseUri = new Uri(_BaseUrl);
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                HttpResponseMessage response_Delay = await client.SendAsync(requestMessage_Delay);
                response = response_Delay;
            }
            response.EnsureSuccessStatusCode();
            return response;
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            //string PageInfo = response.Headers.Where(x => x.Key == "Link").Select(x => string.Join(',', x.Value)).FirstOrDefault() ?? string.Empty;

            //LogInfo("responseBody: " + responseBody);
            //LogApiCalls("responseBody: " + responseBody);
            //return responseBody;

        }
        public static async Task<HttpResponseMessage> MakeApiDeleteCallGeneric
        (
            string ApiConnectorData_Json
            , string ApiCallData_Json
            , string BaseUrl = "http://localhost:7071/api/MakeShopifyApiGetCall"
        )
        {
            LogInfo("BaseUrl: " + BaseUrl);
            string _BaseUrl = BaseUrl ?? String.Empty;
            LogInfo("_BaseUrl: " + _BaseUrl);
            LogInfo("ApiConnectorData_Json: " + ApiConnectorData_Json);
            LogInfo("ApiCallData_Json: " + ApiCallData_Json);

            //LogApiCalls("urlCommand(Get - Secured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(_BaseUrl);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = clintIdIn ?? string.Empty;
            //string clientSecret = clientSecretIn ?? string.Empty;

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Headers.Add("ApiConnectorData_Json", ApiConnectorData_Json);
            requestMessage.Headers.Add("ApiCallData_Json", ApiCallData_Json);

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(850); // just over half a second
                HttpRequestMessage requestMessage_Delay = new HttpRequestMessage(HttpMethod.Delete, baseUri);
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage_Delay.Headers.Add("ApiConnectorData_Json", ApiConnectorData_Json);
                requestMessage_Delay.Headers.Add("ApiCallData_Json", ApiCallData_Json);

                client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

                baseUri = new Uri(_BaseUrl);
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                HttpResponseMessage response_Delay = await client.SendAsync(requestMessage_Delay);
                response = response_Delay;
            }
            response.EnsureSuccessStatusCode();
            return response;
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            //string PageInfo = response.Headers.Where(x => x.Key == "Link").Select(x => string.Join(',', x.Value)).FirstOrDefault() ?? string.Empty;

            //LogInfo("responseBody: " + responseBody);
            //LogApiCalls("responseBody: " + responseBody);
            //return responseBody;

        }
        public static async Task<string> MakeApiGetCall
        (
            string urlCommand
            , string BaseUrl = "http://localhost:7071/api/MakeApiGetCall"
            , string clintIdIn = "shopravi"
            , string clientSecretIn = "H9pPG9yW58cMP45e"
        )
        {
            LogInfo("BaseUrl: " + BaseUrl);
            string _BaseUrl = BaseUrl ?? String.Empty;
            LogInfo("_BaseUrl: " + _BaseUrl);

            LogApiCalls("urlCommand(Get - Secured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(_BaseUrl);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = clintIdIn ?? string.Empty;
            string clientSecret = clientSecretIn ?? string.Empty;

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Headers.Add("urlCommand", urlCommand);

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            LogInfo("responseBody: " + responseBody);
            LogApiCalls("responseBody: " + responseBody);
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
                    results += MakeApiPostCall("/inventory/create", strQuantitiesList);
                }
                catch (Exception e)
                {
                    LogInfo($"Error in QUantities_Add, likely a Serialization issue or issue with call to /inventory/create for " +
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
                    results += MakeApiPostCall("/inventory/edit", strQuantitiesList);
                }
                catch (Exception e)
                {
                    LogInfo($"Error in QUantities_Edit, likely a Serialization issue or issue with call to /inventory/edit for " +
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
                    LogInfo("Failed to serialize PricesList in preparation to call to /discounts/edit. Message: " + e.Message);
                }
                try
                {
                    results += MakeApiPostCall("/discount/create", strPricesList);
                }
                catch (Exception e)
                {
                    string msg = "Error in call to /discount/add. Message: " + e.Message;
                    LogInfo(msg);
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
                    LogInfo("Failed to serialize PricesList in preparation to call to /discounts/edit. Message: " + e.Message);
                }
                results += MakeApiPostCall("/discount/edit", strPricesList);
            }
            return results;
        }
        public static Task<string> Products_Sync()
        {
            LogInfo("Products_Sync()");
            Task<string> results = MakeApiGetCall("/product/shopify");
            return results;
        }
        public static Task<string> Discounts_Sync()
        {
            LogInfo("Discounts_Sync()");
            Task<string> results = MakeApiGetCall("/discount/shopify");
            return results;
        }
        public static Task<string> Inventory_Sync()
        {
            LogInfo("Inventory_Sync()");
            Task<string> results = MakeApiGetCall("/inventory/shopify");
            return results;
        }
        public static async Task<string> Products_DeleteAll()
        {
            LogInfo("Products_DeleteAll()");
            try
            {
                //get all products
                IEnumerable<Shopify_Product> allProducts = await GetAllShopify_Products();

                //delete all of them
                string results = await Products_Delete(allProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Product_DeleteAll: " + e.ToString();
                LogInfo(msg);
                return msg;
            }
        }
        public static async Task<string> Products_Delete(IEnumerable<Shopify_Product> ProductsToDelete)
        {
            try
            { 
                List<Shopify_Product> prods = new List<Shopify_Product>(ProductsToDelete);
                string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                string results = await MakeApiPostCall("/product/delete/", strEditProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Delete: " + e.ToString();
                LogInfo(msg);
                return msg;
            }
        }
        public static async Task<string> Products_Add(IEnumerable<Shopify_Product> NewProducts)
        {
            int pageSize = 10;
            int page = 0;
            int count = NewProducts.Count();
            int StartAt = page * pageSize;
            //Task<string> results = new Task<string>(() => string.Empty);
            string results = string.Empty;
            do
            {
                try
                {
                    List<Shopify_Product> prods = NewProducts.Skip(StartAt).Take(pageSize).ToList();
                    string strNewProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                    string strTmp = await MakeApiPostCall("/product/add/", strNewProducts);
                    results += strTmp;
                }
                catch (Exception e)
                {
                    string msg = "Error in Products_Add. Messge: " + e.ToString();
                    LogInfo(msg);
                    return msg;
                }
                page++;
                StartAt = page * pageSize;
            } while (StartAt <= count);
            return await new Task<string>(() => results);
        }
        public static async Task<string> Products_Edit(IEnumerable<Shopify_Product> ProductsToEdit)
        {
            IEnumerable<Shopify_Product> _ProductsToEdit = ProductsToEdit ?? new List<Shopify_Product>();
            int total = _ProductsToEdit.Count();
            int pageSize = 10;
            int pageNum = 1;
            int pageMin = pageNum * pageSize - pageSize + 1;
            int pageMax = pageNum * pageSize;
            string results = string.Empty;
            while(pageMin <= total)
            {
                IEnumerable<Shopify_Product> PageOfProductsToEdit = _ProductsToEdit.Skip(pageMin - 1).Take(pageSize);
                try
                { 
                    List<Shopify_Product> prods = new List<Shopify_Product>(PageOfProductsToEdit);
                    string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                    string tmpResults = await MakeApiPostCall("/product/edit/", strEditProducts);
                    results += tmpResults;
                }
                catch (Exception e)
                {
                    string msg = "Error in Products_Edit:" + e.ToString();
                    LogInfo(msg);
                    return msg;
                }
                pageNum++;
                pageMin = pageNum * pageSize - pageSize + 1;
                pageMax = pageNum * pageSize;
            }
            return await new Task<string>(() => "No Records Processed");
        }
        public static async Task<IEnumerable<Shopify_Product>> GetAllShopify_Products()
        {
            LogInfo("GetAllShopify_Products()");
            //Products_Sync();
            string results = await MakeApiGetCall("/product/list");
            List<tmpShopify_Product> Data = new List<tmpShopify_Product>();
            try
            {
                Data = (List<tmpShopify_Product>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Product>));
            }
            catch (Exception e)
            {
                LogInfo(e.ToString());
            }
            return Data.Select(p => p.AsShopify_Product());
        }
        public static async Task<IEnumerable<tmpShopify_Prices>> GetAlltmpShopify_Prices()
        {
            LogInfo("Hello World");
            LogInfo("GetAlltmpShopify_Prices()-cm");
            //Discounts_Sync();
            try
            {
                string results = await MakeApiGetCall("/discount/list");

                List<tmpShopify_Prices> Data = (List<tmpShopify_Prices>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Prices>));

                return Data;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Edit: " + e.ToString();
                LogInfo(msg);
                return new List<tmpShopify_Prices>();
            }
        }
        public static async Task<IEnumerable<tmpShopify_Quantities>> GetAlltmpShopify_Quantities()
        {
            LogInfo("GetAlltmpShopify_Quantities()");
            //Inventory_Sync();
            string results = await MakeApiGetCall("/inventory/list");
            results = results.Replace("\"id\":null,", string.Empty);
            List<tmpShopify_Quantities> Data = new List<tmpShopify_Quantities>();
            try
            {
                Data = (List<tmpShopify_Quantities>)JsonSerializer.Deserialize(results, typeof(List<tmpShopify_Quantities>));
            }
            catch (Exception e)
            {
                LogInfo(e.ToString());
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
