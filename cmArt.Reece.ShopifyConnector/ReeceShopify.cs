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
using Newtonsoft.Json.Linq;
using System.Threading;
using cmArt.LibIntegrations.ApiCallerService;

namespace cmArt.Reece.ShopifyConnector
{
    public class ReeceShopify
    {
        const string BaseUrl = "https://aquadragonservices.com/pcr/apitest/index.php";
        protected static ILogger _logger;
        protected static ILogger _fileLogger;
        const int Api_Timeout_Max = 25;

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

        private static string RetryAPICall(Func<string> MakeCall)
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

        private static string MakeApiPostCall(string urlCommand, string content, string SessionId = "")
        {
            LogApiCalls("urlCommand(Post): " + urlCommand);
            LogApiCalls("content: " + content);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(BaseUrl + urlCommand);
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
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            LogApiCalls("responseBody: " + responseBody);
            return responseBody;
        }
        public static string MakeApiPostCall(ApiCallFormData data, Func<string, int> MakeLogEntry, string ContentType = "", string fileNameAndPath = "")
        {
            string urlCommand = data.UrlCommand;
            Dictionary<string, string> content = data.Body;
            try
            {
                MakeLogEntry("urlCommand: " + urlCommand);
                foreach (var item in content) { MakeLogEntry("content." + item.Key + ": \"" + item.Value + "\""); }
            }
            catch (Exception e)
            {
                return "Error in MapApiPostCall_Unsecured while trying to MakeLogEntry. Message: " + e.Message;
            }

            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = "shopravi";
            string clientSecret = "H9pPG9yW58cMP45e";

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            var formContent = new MultipartFormDataContent();

            foreach (var item in content)
            {
                formContent.Add(new StringContent(item.Value), item.Key);
            }
            if (File.Exists(fileNameAndPath))
            {
                formContent.Add(new StreamContent(File.OpenRead(fileNameAndPath)), "file");
            }

            requestMessage.Content = formContent;
            string responseBody = MakeApiCall(client, requestMessage, 10);

            return responseBody;
        }
        private static string MakeApiCall(HttpClient client, HttpRequestMessage requestMessage, int maxAttempts)
        {
            int attempts = 0;
            int _maxAttempts = maxAttempts;
            string messages = string.Empty;
            while (attempts < _maxAttempts)
            {
                try
                {
                    attempts++;
                    var task = client.SendAsync(requestMessage);
                    var response = task.Result;
                    //response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result ?? string.Empty;
                    return responseBody;
                }
                catch (Exception e)
                {
                    System.Threading.Thread.Sleep(100 * attempts);
                    // try again. but maybe do some logging
                    messages += e.Message + Environment.NewLine;
                }
            }
            return $"Quitting after {_maxAttempts} tries to get MakeApiCall. Messages: {messages}. " +
                $"RequestUri: {requestMessage.RequestUri}. Content: {requestMessage.Content}. Method: {requestMessage.Method}";
        }

        private static string MakeApiGetCall_Unsecured(string urlCommand)
        {
            LogApiCalls("urlCommand(Get - Unsecured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(BaseUrl + urlCommand);
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
        private static string MakeApiGetCall(string urlCommand)
        {
            LogApiCalls("urlCommand(Get - Secured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(Api_Timeout_Max);

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = "shopravi";
            string clientSecret = "H9pPG9yW58cMP45e";

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

            LogApiCalls("responseBody: " + responseBody);
            return responseBody;
        }
        public static string Quantities_Add(IEnumerable<Shopify_Quantities> QuantitiesList)
        {
            LogInfo("Begin Quantities_Add(IEnumerable<Shopify_Quantities> QuantitiesList)");
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
        public static string CloseOldSession()
        {
            throw new NotImplementedException();
        }
        public static string OpenSession()
        {
            string NewSessionId = Guid.NewGuid().ToString();
            string SessionIdJson = "{ \"SessionId\": \"" + NewSessionId + "\"}";

            ApiCallFormData data = new ApiCallFormData();
            data.UrlCommand = "/product/list";
            data.Body = new Dictionary<string, string>();
            data.Body.Add("SESSIONID", Guid.NewGuid().ToString());
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };

            var strResults = MakeApiPostCall(data, fLog);
            var results = JObject.Parse(strResults);
            var message = results["message"] ?? string.Empty;
            if (message.ToString() == "Update Successful")
            { return strResults.ToString(); }

            int count = 0;
            int delayTicks = 10000;
            int maxCount = 90;
            string FirstTime;
            DateTime LastRanTime;
            DateTime FirstRanTime = new DateTime();
            DateTime PrevRanTime = new DateTime();
            bool firstPass = true;
            while (count < 90)
            {
                Thread.Sleep(delayTicks);
                strResults = MakeApiPostCall("/sessions/open", SessionIdJson);
                results = JObject.Parse(strResults);
                message = results["message"] ?? string.Empty;
                var LastRan = results["Session"]["LastCallTime"];
                DateTime.TryParse(LastRan.ToString(), out LastRanTime);
                if (firstPass)
                {
                    firstPass = false;
                    DateTime.TryParse(LastRan.ToString(), out FirstRanTime);
                }

                if (message.ToString() == "Update Successful")
                {
                    var tmpSession = results["Session"];
                    if (tmpSession == null)
                    {
                        return "Update Successful";
                    }
                    else
                    {
                        string SessionId = (results["Session"]["SessionId"] ?? string.Empty).ToString() ?? string.Empty;
                        return SessionId;
                    }
                }
                bool TimeHasPassed;
                if (message.ToString() == "Error Message: Session is open and Session IDs are not the same")
                {
                    bool MoreThan15MinutesHasPassed = DateTime.UtcNow - LastRanTime > TimeSpan.FromMinutes(15);
                    if (MoreThan15MinutesHasPassed)
                    {
                        // close existing session so we can re-open the new session.
                        // 01 - Get current SessionId
                        // 02 - Close the SessionId
                        return "Error Message: Session failed to be released by the API. The API should return \"Update Successful\" and an updated session";
                    }
                }


            }

            return $"Session failed to be opened within {delayTicks/1000/60* maxCount} minutes";

        }
        public static string Quantities_Edit(IEnumerable<Shopify_Quantities> QuantitiesList)
        {
            LogInfo("Begin Quantities_Edit(IEnumerable<Shopify_Quantities> QuantitiesList)");
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
            LogInfo("Begin Prices_Add(IEnumerable<Shopify_Prices> PricesList)");
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
                    string msg = "Error in call to /discount/add: " + e.ToString();
                    LogInfo(msg);
                }
            }
            return results;
        }
        public static string Prices_Edit(IEnumerable<Shopify_Prices> PricesList)
        {
            LogInfo("Begin Prices_Edit(IEnumerable<Shopify_Prices> PricesList)");
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
        public static string Products_Sync()
        {
            LogInfo("Products_Sync()");
            string results = MakeApiGetCall("/product/shopify") ?? string.Empty;
            return results;
        }
        public static string Discounts_Sync()
        {
            LogInfo("Discounts_Sync()");
            string results = MakeApiGetCall("/discount/shopify") ?? string.Empty;
            return results;
        }
        public static string Inventory_Sync()
        {
            LogInfo("Inventory_Sync()");
            string results = MakeApiGetCall("/inventory/shopify") ?? string.Empty;
            return results;
        }
        public static string Products_DeleteAll()
        {
            LogInfo("Products_DeleteAll()");
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
                string msg = "Error in Product_DeleteAll: " + e.ToString();
                LogInfo(msg);
                return msg;
            }
        }
        public static string Products_Delete(IEnumerable<Shopify_Product> ProductsToDelete)
        {
            LogInfo("Begin Products_Delete(IEnumerable<Shopify_Product> ProductsToDelete)");
            try
            { 
                List<Shopify_Product> prods = new List<Shopify_Product>(ProductsToDelete);
                string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                string results = MakeApiPostCall("/product/delete/", strEditProducts);
                return results;
            }
            catch (Exception e)
            {
                string msg = "Error in Products_Delete: " + e.ToString();
                LogInfo(msg);
                return msg;
            }
        }
        public static string Products_Add(IEnumerable<Shopify_Product> NewProducts)
        {
            LogInfo("Begin Products_Add(IEnumerable<Shopify_Product> NewProducts)");
            int pageSize = 10;
            int page = 0;
            int count = NewProducts.Count();
            int StartAt = page * pageSize;
            string results = string.Empty;
            LogInfo_Write("Page: ");
            do
            {
                try
                {
                    List<Shopify_Product> prods = NewProducts.Skip(StartAt).Take(pageSize).ToList();
                    string strNewProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                    results += MakeApiPostCall("/product/add/", strNewProducts) ?? string.Empty;
                }
                catch (Exception e)
                {
                    string msg = "Error in Products_Add. Messge: " + e.ToString();
                    LogInfo(msg);
                    return msg;
                }
                page++;
                LogInfo_Write(page + ",");
                StartAt = page * pageSize;
            } while (StartAt <= count);
            LogInfo("(" + page + ")");
            return results;
        }
        public static string Products_Edit(IEnumerable<Shopify_Product> ProductsToEdit)
        {
            LogInfo("Begin Products_Edit(IEnumerable<Shopify_Product> ProductsToEdit)");
            IEnumerable<Shopify_Product> _ProductsToEdit = ProductsToEdit ?? new List<Shopify_Product>();
            int total = _ProductsToEdit.Count();
            int pageSize = 10;
            int pageNum = 1;
            int pageMin = pageNum * pageSize - pageSize + 1;
            int pageMax = pageNum * pageSize;
            string results = string.Empty;
            LogInfo_Write("Page: ");
            while (pageMin <= total)
            {
                IEnumerable<Shopify_Product> PageOfProductsToEdit = _ProductsToEdit.Skip(pageMin - 1).Take(pageSize);
                try
                { 
                    List<Shopify_Product> prods = new List<Shopify_Product>(PageOfProductsToEdit);
                    string strEditProducts = JsonSerializer.Serialize(prods, typeof(List<Shopify_Product>));
                    string tmpResults = MakeApiPostCall("/product/edit/", strEditProducts);
                    results += tmpResults;
                }
                catch (Exception e)
                {
                    string msg = "Error in Products_Edit:" + e.ToString();
                    LogInfo(msg);
                    return msg;
                }
                pageNum++;
                LogInfo_Write(pageNum + ",");
                pageMin = pageNum * pageSize - pageSize + 1;
                pageMax = pageNum * pageSize;
            }
            LogInfo("(" + pageNum + ")");
            return "No Records Processed";
        }
        public static IEnumerable<Shopify_Product> GetAllShopify_Products()
        {
            LogInfo("GetAllShopify_Products()");
            Products_Sync();
            string results = MakeApiGetCall("/product/list");
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
        public static IEnumerable<tmpShopify_Prices> GetAlltmpShopify_Prices()
        {
            LogInfo("GetAlltmpShopify_Prices()");
            Discounts_Sync();
            try
            {
                string results = MakeApiGetCall("/discount/list");

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
        public static IEnumerable<tmpShopify_Quantities> GetAlltmpShopify_Quantities()
        {
            LogInfo("GetAlltmpShopify_Quantities()");
            Inventory_Sync();
            string results = MakeApiGetCall("/inventory/list");
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
