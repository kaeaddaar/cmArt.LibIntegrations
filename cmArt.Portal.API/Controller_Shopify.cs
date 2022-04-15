using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using cmArt.Portal.API.Services;
using cmArt.Portal.API.Data;
using cmArt.Portal.API.Repositories;
using cmArt.Portal.Data;
// -----
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using cmArt.Portal.Data.InventoryData;
using cmArt.Portal.API.Models;
//using Microsoft.Extensions.Configuration;
using cmArt.LibIntegrations.ApiCallerService;
using System.Text;


namespace cmArt.Portal.API
{

    public static class Controller_Shopify
    {
        //private static IConfigurationRoot config;
        //private static StaticSettings settings;

        //private static void SetupSettings()
        //{
        //    config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings_portalapi.json", optional: false, reloadOnChange: true)
        //        .Build();

        //    settings = new StaticSettings(config, "Controller_Shopify");
        //}

        [FunctionName("MakeApiPostCall_Shopify")]
        public static async Task<IActionResult> RunMakeApiPostCall
        (
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MakeShopifyApiPostCall")] HttpRequest req,
            ILogger log
        )
        {
            //return new OkObjectResult("strUrlCommand"+ strUrlCommand);

            //SetupSettings();
            string ApiConnectorData_Json = req.Headers["ApiConnectorData_Json"];
            string ApiCallData_Json = req.Headers["ApiCallData_Json"];
            ApiConnectorData connData = (ApiConnectorData)System.Text.Json.JsonSerializer.Deserialize(ApiConnectorData_Json, typeof(ApiConnectorData));
            ApiCallerBase caller = new ApiCallerBase();
            caller.init(connData);
            ApiCallData data = (ApiCallData)System.Text.Json.JsonSerializer.Deserialize(ApiCallData_Json, typeof(ApiCallData));

            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            //string BaseUrl = settings.PortalApiUrl + urlCommand;
            string BaseUrl = caller._ApiConnectorData.Url;
            string urlCommand = data.UrlCommand;

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(15);

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = connData.UserName ?? string.Empty;
            string clientSecret = connData.Password ?? string.Empty;

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

            return new OkObjectResult(responseBody);
        }
        [FunctionName("MakeApiGetCall_Shopify")]
        public static async Task<IActionResult> RunMakeApiGetCall
        (
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "MakeShopifyApiGetCall")] HttpRequest req,
            ILogger log
        )
        {
            //return new OkObjectResult("strUrlCommand"+ strUrlCommand);

            //SetupSettings();
            string ApiConnectorData_Json = req.Headers["ApiConnectorData_Json"];
            string ApiCallData_Json = req.Headers["ApiCallData_Json"];
            ApiConnectorData connData = (ApiConnectorData)System.Text.Json.JsonSerializer.Deserialize(ApiConnectorData_Json, typeof(ApiConnectorData));
            ApiCallerBase caller = new ApiCallerBase();
            caller.init(connData);
            ApiCallData data = (ApiCallData)System.Text.Json.JsonSerializer.Deserialize(ApiCallData_Json, typeof(ApiCallData));
            string ApiCallResults = caller.MakeApiGetCall(data.UrlCommand);

            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            //string BaseUrl = settings.PortalApiUrl + urlCommand;
            string BaseUrl = caller._ApiConnectorData.Url;
            string urlCommand = data.UrlCommand;

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(15);

            Uri baseUri = new Uri(BaseUrl + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = connData.UserName ?? string.Empty;
            string clientSecret = connData.Password ?? string.Empty;

            // Async Call
            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            req.HttpContext.Response.Headers.Add("cmArtTry100000", "Test123");
            requestMessage.Content = new StringContent(content);

            Task<HttpResponseMessage> task = client.SendAsync(requestMessage);
            HttpResponseMessage response = task.Result;

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                return new ContentResult() { StatusCode = (int)System.Net.HttpStatusCode.TooManyRequests };
            }
            string strLink = response.Headers.Where(x => x.Key == "Link").Select(x => string.Join(',', x.Value)).FirstOrDefault() ?? string.Empty;
            req.HttpContext.Response.Headers.Add("Link", strLink);

            string ExternalContent = response.Content.ReadAsStringAsync().Result;

            //ContentResult returned = new ContentResult();
            //returned.Content = ApiCallResults;
            //returned.ContentType = "application/json";
            //foreach (var item in response.Headers)
            //{
            //    string key = item.Key;
            //    string value = item.Value.FirstOrDefault();
            //    req.HttpContext.Response.Headers.Add(key, value);
            //}
            return new OkObjectResult(ExternalContent);
            //return returned;

            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            //response.EnsureSuccessStatusCode();

            //string responseBody = response.Content.ReadAsStringAsync().Result;

            //// --
            //foreach(var item in response.Headers)
            //{
            //    string key = item.Key;
            //    string value = item.Value.FirstOrDefault();
            //}
            //// --
            //req.HttpContext.Response.Headers.Add("ink", "Test123");
            //return new OkObjectResult(responseBody);
            ////return (IActionResult) new customActionResult(responseBody);
        }
        //class customActionResult : ActionResult
        //{
        //    string _responseBody;
        //    public customActionResult(string responseBody)
        //    {
        //        _responseBody = responseBody ?? string.Empty;
        //    }
        //    public void ExecuteResult(ActionContext context)
        //    {
        //        context.HttpContext.Response.Headers.Add("testSync", "test2");
        //        context.HttpContext.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(_responseBody));
        //        //base.ExecuteResult(context);
        //    }
        //    public Task ExecuteResultAsync(ActionContext context)
        //    {
        //        context.HttpContext.Response.Headers.Add("test", "test1");
        //        context.HttpContext.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(_responseBody));

        //        //return base.ExecuteResultAsync(context);
        //    }
        //}

    }
    
}
