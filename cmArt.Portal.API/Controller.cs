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

namespace BlazorApp.Api
{
    public static class Controller
    {
        [FunctionName("JsonDocument_Controller")]
        public static async Task<IActionResult> Run_JsonDocument
        (
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "JsonDocument/{idIn?}")] HttpRequest req
            , ILogger log
            , string idIn
        )
        {
            //return await InventorySimple_Controller_Generic.Run(req, log, idIn);
            Func<Context, Document, Guid> funcAdd = Document_Repository.AddJsonDocument;
            Func<Context, Document, int> funcDelete = ControllerGeneric<Document, Guid, Context, IContext>.DeleteObject_Default;
            return await ControllerGeneric<Document, Guid, Context, IContext>.Run(Document_Repository.GetJsonDocument, req, log, idIn, funcAdd, funcDelete, utils.StringToGuid);
        }
    }
}
