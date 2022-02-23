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
using cmArt.Portal.API6.Services;
using cmArt.Portal.API6.Data;
using cmArt.Portal.API6.Repositories;
using cmArt.Portal.Data6;
// -----
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace cmArt.Portal.API6
{
    public static class Controller
    {
        //[FunctionName("WebInventory_Controller")]
        //public static async Task<IActionResult> Run_WebInventory
        //(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "WebInventory/{idIn?}")] HttpRequest req
        //    , ILogger log
        //    , string idIn
        //)
        //{
        //    //return await InventorySimple_Controller_Generic.Run(req, log, idIn);
        //    Func<Context_WebInventory, WebInventory, int> funcAdd = WebInventory_Repository.AddWebInventoryRecord;
        //    Func<Context_WebInventory, WebInventory, int> funcDelete = ControllerGeneric<WebInventory, int, Context_WebInventory, IContext_WebInventory>.DeleteObject_Default;
        //    return await ControllerGeneric<WebInventory, int, Context_WebInventory, IContext_WebInventory>.Run(WebInventory_Repository.GetWebInventoryRecord, req, log, idIn, funcAdd, funcDelete, utils.StringToInt);
        //}
        [FunctionName("JsonDocument_Controller")]
        public static async Task<IActionResult> Run_JsonDocument
        (
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "JsonDocument/{idIn?}")] HttpRequest req
            , ILogger log
            , string idIn
        )
        {
            //return await InventorySimple_Controller_Generic.Run(req, log, idIn);
            Func<Context_Documents, Document, Guid> funcAdd = Document_Repository.AddJsonDocument;
            Func<Context_Documents, Document, int> funcDelete = ControllerGeneric<Document, DocumentClient, Guid, Context_Documents, IContext_Documents>.DeleteObject_Default;
            return await ControllerGeneric<Document, DocumentClient, Guid, Context_Documents, IContext_Documents>.Run(Document_Repository.GetJsonDocument, req, log, idIn, funcAdd, funcDelete, utils.StringToGuid);
        }

        [FunctionName("MakeApiPostCall_Controller")]
        public static async Task<IActionResult> RunMakeApiPostCall
        (
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MakeApiPostCall")] HttpRequest req,
            ILogger log
        )
        {

            string urlCommand = req.Headers["urlCommand"];
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            //return new OkObjectResult("strUrlCommand"+ strUrlCommand);

            // -----
            //LogApiCalls("urlCommand(Post): " + urlCommand);
            //LogApiCalls("content: " + content);

            string BaseUrl = "https://aquadragonservices.com/pcr/apitest/index.php";

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(15);

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

            return new OkObjectResult(responseBody);
        }
        [FunctionName("MakeApiGetCall_Controller")]
        public static async Task<IActionResult> RunMakeApiGetCall
        (
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "MakeApiGetCall")] HttpRequest req,
            ILogger log
        )
        {
            string BaseUrl = "https://aquadragonservices.com/pcr/apitest/index.php";

            string urlCommand = req.Headers["urlCommand"];
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            //LogApiCalls("urlCommand(Get - Secured): " + urlCommand);
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);

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

            //LogApiCalls("responseBody: " + responseBody);
            return new OkObjectResult(responseBody);
        }


    }
}
