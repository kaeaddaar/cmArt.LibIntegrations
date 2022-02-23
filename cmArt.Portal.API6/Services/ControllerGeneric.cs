using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Web;
using Microsoft.EntityFrameworkCore;
using cmArt.Portal.Data;

namespace cmArt.Portal.API6.Services
{
    public class ControllerGeneric<T, TIndex, TContext, IContext> where T : ICopyable<T>, ICopyableHttpRequest<T>, new() where TContext : DbContext, IContext, new()
    {
        public static int DeleteObject_Default(TContext context, T objToDelete)
        {
            context.Remove(objToDelete);
            context.SaveChanges();
            return 1;
        }

        public static async Task<IActionResult> Run
        (
            Func<IContext, TIndex, T> GetObj
            //, Func<T, Ts, Context, int> FillForUpdate
            //, Func<ILogger, HttpRequest, dynamic, int, Ts> Log_Gather_Fill
            , HttpRequest req
            , ILogger log
            , string idIn
            , Func<TContext, T, TIndex> AddObj
            , Func<TContext, T, int> DeleteObj
            , Func<string, TIndex> StringToTIndex
        )
        {
            IActionResult result;
            try
            {
                result = await WrappedRun
                (
                    GetObj: GetObj
                    //, FillForUpdate: FillForUpdate
                    //, Log_Gather_Fill: Log_Gather_Fill
                    , req: req
                    , log: log
                    , idIn: idIn
                    , AddObj: AddObj
                    , DeleteObj: DeleteObj
                    , StringToTIndex: StringToTIndex
                );
            }
            catch (Exception e)
            {
                result = new OkObjectResult(e);
            }
            return result;
        }


        private static async Task<IActionResult> WrappedRun
        (
            Func<IContext, TIndex, T> GetObj
            //, Func<T, Ts, Context, int> FillForUpdate
            //, Func<ILogger, HttpRequest, dynamic, int, Ts> Log_Gather_Fill
            , HttpRequest req
            , ILogger log
            , string idIn
            , Func<TContext, T, TIndex> AddObj
            , Func<TContext, T, int> DeleteObj
            , Func<string, TIndex> StringToTIndex
        )
        {
            log.LogInformation("ControllerGeneric<T> Entry Point");

            TIndex id = StringToTIndex(idIn);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string AllowUpdate = req.Query["AllowUpdate"];
            AllowUpdate = AllowUpdate ?? data?.AllowUpdate;

            T obj = new T();
            TContext context = new TContext();

            T newObj = new T();

            string responseMessage = string.Empty;

            try
            {
                // ----- Log_Gather_Fill -----
                newObj.CopyFrom(req, data);
                //newObj = Log_Gather_Fill(log, req, data, id);
            }
            catch (Exception e)
            {
                responseMessage = "{ \"error\": \"" + HttpUtility.HtmlEncode(e.ToString()) + "\"}";
                return new OkObjectResult(responseMessage);
            }

            string method = req.Method;


            if (method == "GET")
            {
                log.LogInformation("GET started");
                try
                {
                    // ----- GetObj -----
                    obj = GetObj(context, id); // object specific data reposotory call
                    responseMessage = JsonConvert.SerializeObject(obj);
                }
                catch (Exception e)
                {
                    responseMessage = "{ \"error\": \"" + HttpUtility.HtmlEncode(e.Message) + "\"}";
                }
            }
            else if (method == "POST")
            {
                log.LogInformation("POST started");
                bool found = false;
                try
                {
                    // ----- GetObj -----
                    obj = GetObj(context, id); // object specific data reposotory call
                    found = obj != null;
                }
                catch (Exception e)
                {
                    responseMessage = "{ \"error\": \"" + HttpUtility.HtmlEncode(e.Message) + "\"}";
                }

                bool NotFound_ShouldAdd = !found;
                bool Found_ShouldUpdate = found;

                if (Found_ShouldUpdate)
                {
                    log.LogInformation("Found_ShouldUpdate");
                    bool IsIdempotent = !(AllowUpdate == "false");

                    bool IsUpdateAllowed = IsIdempotent;

                    log.LogInformation($"IsUpdateAllowed = {IsUpdateAllowed}");
                    if (IsUpdateAllowed)
                    {
                        // ----- FillForUpdate -----
                        obj.CopyFrom(newObj);
                        context.SaveChanges();
                        //FillForUpdate(obj, newObj, context); // Field/Object Specific
                    }
                }
                if (NotFound_ShouldAdd)
                {
                    obj = new T();
                    obj.CopyFrom(newObj);
                    //FillForUpdate(obj, newObj, context);
                    TIndex UniqueIdAdded = AddObj(context, obj);
                    obj = GetObj(context, UniqueIdAdded);
                }
                responseMessage = JsonConvert.SerializeObject(obj);
            }
            else if (method == "PUT") // Update
            {
                log.LogInformation("PUT started");
                bool found = false;
                try
                {
                    // ----- GetObj -----
                    obj = GetObj(context, id); // Field/Object Specific
                    found = true;
                }
                catch (Exception e)
                {
                    responseMessage = "{ \"error\": \"" + HttpUtility.HtmlEncode(e.Message) + "\"}";
                }

                bool NotFound_Fail = !found;
                bool Found_ShouldUpdate = found;

                log.LogInformation($"Found = {found}");
                if (NotFound_Fail)
                {
                    string msg = $"{id} not found";
                    responseMessage = "{ \"error\": \"" + msg + "\"}";
                }

                log.LogInformation($"Found_ShouldUpdate = {Found_ShouldUpdate}");
                if (Found_ShouldUpdate)
                {
                    // ----- FillForUpdate -----
                    obj.CopyFrom(newObj);
                    //FillForUpdate(obj, newObj, context); // Field/Object Specific
                    await context.SaveChangesAsync();
                }
                responseMessage = JsonConvert.SerializeObject(obj);
            }
            else if (method == "DELETE")
            {
                log.LogInformation("DELETE started");
                bool found = false;
                try
                {
                    // ----- GetObj -----
                    obj = GetObj(context, id); // Field/Object specific
                    found = true;
                }
                catch (Exception e)
                {
                    responseMessage = "{ \"error\": \"" + HttpUtility.HtmlEncode(e.Message) + "\"}";
                }
                if (found)
                {
                    DeleteObj(context, obj);
                    await context.SaveChangesAsync();
                    responseMessage = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    string msg = $"{id} not found";
                    responseMessage = "{ \"error\": \"" + msg + "\"}";
                }
            }
            else
            {
                log.LogInformation("Expected method not found. method: \"" + method + "\"");
                string msg = $"Request method needs to be GET, POST, PUT, or DELETE. It was \"{req.Method} \".";
                responseMessage = "{ \"error\": \"" + msg + "\"}";
            }

            return new OkObjectResult(responseMessage);

        }



    }

}
