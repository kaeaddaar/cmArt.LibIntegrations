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
using cmArt.LibIntegrations.FileNamesService;
using cmArt.LibIntegrations.ApiCallerService;

namespace cmArt.Portal.API
{
    public class fGenericSerialization
    {
        //[FunctionName("GenericSerialization_ReadOrDeserializeTable")]
        //public static async Task<IActionResult> Run_ReadOrDeserializeTable
        //(
        //    [HttpTrigger(AuthorizationLevel.Function, "post"
        //        , Route = "ReadOrDeserializeTable")] HttpRequest req
        //    , ILogger log
        //    //, string TableNameIn
        //)   
        //{
        //    // This endpoint should only grab a page of results and pass them on
        //    string content = string.Empty;
        //    using (var sr = new StreamReader(req.Body))
        //    {
        //        content = await sr.ReadToEndAsync();
        //    }
        //    GenericSerializationCallData data = System.Text.Json.JsonSerializer.Deserialize<GenericSerializationCallData>(content);

        //    //List<shopify_price_rule> PriceRules = GenericSerialization<shopify_price_rule>.ReadOrDeserializeTable(data.TableName, data.CashedFilesDirectory, data.RecordsPerPage);
        //    List<dynamic> PriceRules = GenericSerialization<dynamic>.ReadOrDeserializeTable(data.TableName, data.CashedFilesDirectory, data.RecordsPerPage);

        //    // get the table and return the results
        //    return new OkObjectResult("ReadOrDeserializeTable");

        //}
        [FunctionName("GenericSerialization_GetCachedFileNamesFromDirectory")]
        public static async Task<IActionResult> Run_GetCachedFileNamesFromDirectory
        (
            [HttpTrigger(AuthorizationLevel.Function, "post"
                , Route = "GetCachedFileNamesFromDirectory")] HttpRequest req
            , ILogger log
        //, string TableNameIn
        )
        {
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            string args = req.Headers["args"];
            GenericSerializationCallData data = (GenericSerializationCallData)JsonConvert.DeserializeObject<GenericSerializationCallData>(args);
            
            string folder = data.CashedFilesDirectory ?? string.Empty;

            bool folderExists = Directory.Exists(folder);
            if (!folderExists)
            { return new BadRequestObjectResult("Folder Doesn't Exist: " + folder); }

            List<string> FileNames = GenericSerialization<dynamic>.GetCachedFileNamesFromDirectory(folder, "shopify_price_rule");
            string strFileNames = string.Empty;
            try
            {
                strFileNames = Newtonsoft.Json.JsonConvert.SerializeObject(FileNames);
            } 
            catch (Exception e)
            {
                return new OkObjectResult(e);
            }

            string results = strFileNames;
            return new OkObjectResult(results);
        }
            [FunctionName("GenericSerialization_ReadFromFile")]
        public static async Task<IActionResult> Run_ReadFromFile
        (
            [HttpTrigger(AuthorizationLevel.Function, "post"
                , Route = "ReadFromFile")] HttpRequest req
            , ILogger log
        //, string TableNameIn
        )
        {
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            string args = req.Headers["args"];
            dynamic Arguments = (dynamic)JsonConvert.DeserializeObject<dynamic>(args);
            string PathAndFile = Arguments.PathAndFile ?? string.Empty;
           
            bool fileExists = File.Exists(PathAndFile ?? string.Empty);
            FileNameService fpn = new FileNameService(PathAndFile);
            string folder = fpn.GetPath();

            if (!fileExists)
            { return new OkObjectResult("File Doesn't Exist: " + PathAndFile); }

            bool folderExists = Directory.Exists(folder);
            if (!folderExists)
            { return new BadRequestObjectResult("Folder Doesn't Exist: " + folder); }
            
            string results = File.ReadAllText(PathAndFile);
            return new OkObjectResult(results);
        }
        [FunctionName("GenericSerialization_SaveToFile")]
        public static async Task<IActionResult> Run_SaveToFile
        (
            [HttpTrigger(AuthorizationLevel.Function, "post"
                , Route = "SaveToFile")] HttpRequest req
            , ILogger log
        //, string TableNameIn
        )
        {
            string content = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                content = await sr.ReadToEndAsync();
            }

            string args = req.Headers["args"];
            dynamic Arguments = (dynamic)JsonConvert.DeserializeObject<dynamic>(args);
            string PathAndFile = Arguments.PathAndFile ?? string.Empty;
            bool fileExists = File.Exists(PathAndFile ?? string.Empty);
            FileNameService fpn = new FileNameService(PathAndFile);
            string folder = fpn.GetPath();

            bool folderExists = Directory.Exists(folder);
            if (!folderExists)
            { return new BadRequestObjectResult("Folder Doesn't Exist: " + folder); }
            File.WriteAllText(PathAndFile, content);
            return new OkObjectResult("Contents written to disk in file: " + PathAndFile);
        }
    }
}
