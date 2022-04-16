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
using cmArt.LibIntegrations.SerializationService;
using cmArt.Portal.Data.ShopifyData;
using cmArt.Portal.Data.GenericSerialization;

namespace cmArt.Portal.API
{
    public class fGenericSerialization
    {
        [FunctionName("GenericSerialization_ReadOrDeserializeTable")]
        public static async Task<IActionResult> Run_WebInventory
        (
            [HttpTrigger(AuthorizationLevel.Function, "post"
                , Route = "ReadOrDeserializeTable")] HttpRequest req
            , ILogger log
            //, string TableNameIn
        )   
        {
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }
            GenericSerializationCallData data = System.Text.Json.JsonSerializer.Deserialize<GenericSerializationCallData>(content);

            List<shopify_price_rule> PriceRules = GenericSerialization<shopify_price_rule>.ReadOrDeserializeTable(data.TableName, data.CashedFilesDirectory, data.RecordsPerPage);

            //string CachedFilesDirectory = req.Headers.Where(x => x.Key == "CachedFilesDirectory")
            //    .Select(x => string.Join(',', x.Value)).FirstOrDefault() ?? string.Empty;
            //string strRecordsPerPage = req.Headers.Where(x => x.Key == "RecordsPerPage")
            //    .Select(x => string.Join(',', x.Value)).FirstOrDefault() ?? string.Empty;
            //int RecordsPerPage = 0;
            //int.TryParse(strRecordsPerPage, out RecordsPerPage);

            // get the table and return the results
            return new OkObjectResult("ReadOrDeserializeTable");

        }
    }
}
