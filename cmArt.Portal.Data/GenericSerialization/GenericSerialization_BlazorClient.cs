using cmArt.LibIntegrations.ApiCallerService;
using cmArt.LibIntegrations.SerializationService;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace cmArt.Portal.Data.GenericSerialization
{
    public class GenericSerialization_BlazorClient<T> : ApiCallerBase, IGenericSerialization<T>
    {
        public GenericSerialization_BlazorClient()
        {
        }

        public bool Exists(string CachedFilesDirectory, string TableName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetCachedFileNamesFromDirectoryAsync(string CachedFilesDirectory, string TableName)
        {
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/GetCachedFileNamesFromDirectory";
            callData.Args = GetArgs_Json(TableName: TableName, CachedFilesDirectory: CachedFilesDirectory, RecordsPerPage: 0);
            callData.Body = string.Empty;

            string strResults = await this.MakeApiPostCallAsync(callData, fLog);
            List<string> results = new List<string>();
            try
            {
                results = System.Text.Json.JsonSerializer.Deserialize<List<string>>(strResults);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return results ?? new List<string>();
        }
        public List<string> GetCachedFileNamesFromDirectory(string CachedFilesDirectory, string TableName)
        {
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/GetCachedFileNamesFromDirectory";
            callData.Body = GetArgs_Json(TableName: TableName, CachedFilesDirectory: CachedFilesDirectory, RecordsPerPage: 0);

            string strResults = this.MakeApiPostCall(callData, fLog);
            List<string> results = new List<string>();
            try
            {
                results = System.Text.Json.JsonSerializer.Deserialize<List<string>>(strResults);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return results ?? new List<string>();
        }
        public async Task<List<T>> ReadOrDeserializeTablePageAsync(string PathAndFile)
        {
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            dynamic args = new System.Dynamic.ExpandoObject();
            args.PathAndFile = PathAndFile;

            callData.UrlCommand = "/api/ReadFromFile";
            callData.Body = string.Empty;
            callData.Args = Newtonsoft.Json.JsonConvert.SerializeObject(args) ?? string.Empty;

            string strResults = await this.MakeApiPostCallAsync(callData, fLog);
            List<T> records = new List<T>();
            try
            {
                //records = System.Text.Json.JsonSerializer.Deserialize<List<T>>(strResults);
                records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(strResults);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return records ?? new List<T>();
        }
        public async Task<List<T>> ReadOrDeserializeTableAsync(string TableName, string CachedFilesDirectory, int RecordsPerPage)
        {
            //Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            //ApiCallData callData = new ApiCallData();
            //callData.UrlCommand = "/api/ReadOrDeserializeTable";
            //callData.Body = GetArgs_Json(TableName: TableName, CachedFilesDirectory: CachedFilesDirectory, RecordsPerPage: RecordsPerPage);

            //string strResults = await this.MakeApiPostCallAsync(callData, fLog);
            //List<T> records = new List<T>();
            //try
            //{
            //    records = System.Text.Json.JsonSerializer.Deserialize<List<T>>(strResults);
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e.ToString());
            //}
            //return records ?? new List<T>();

            //-----
            bool cachedDataExists = false;

            string directory = $"{CachedFilesDirectory}";
            IEnumerable<string> Cachedfiles = await GetCachedFileNamesFromDirectoryAsync(CachedFilesDirectory, TableName);

            cachedDataExists = Cachedfiles.Count() > 0;
            string filePathAndName = string.Empty;
            string fileContents = string.Empty;
            List<T> rows = new List<T>();

            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            if (cachedDataExists)
            {
                Console.Write($"Loading {TableName} from Cache: ");
                Cachedfiles = Cachedfiles.Select(x => x.ToUpper());
                // load from cache
                for (int pageNum = 1; pageNum <= Cachedfiles.Count(); pageNum++)
                {
                    Console.Write($" - Load Pg {pageNum}");
                    filePathAndName = $"{CachedFilesDirectory}tbl{TableName}_page{pageNum}.json";
                    filePathAndName = filePathAndName.ToUpper();
                    if (Cachedfiles.Contains(filePathAndName))
                    {
                        //fileContents = await ReadOrDeserializeTablePageAsync(filePathAndName);
                        //List<T> TableRecords = new List<T>();
                        List<T> TableRecords = await ReadOrDeserializeTablePageAsync(filePathAndName);
                        foreach (T row in TableRecords)
                        {
                            rows.Add(row);
                        }
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"ReadOrSerializeTable for {TableName} Complete");
                return rows;
            }
            else
            {
                Console.WriteLine($"Cached Data Doesn't Exist for \"{TableName}\"");
                //Console.Write($"Loading {TableName} from Database.");
                //List<T> TableRecords = Load_Table_FromDatabase();
                //GenericSerialization<T>.SerializeToJSON(TableRecords, TableName, CachedFilesDirectory, RecordsPerPage);
                //Console.Write($"Finished Loading {TableName} from Database.");
                //return TableRecords;
                return new List<T>();
            }


        }

        public List<T> ReadOrDeserializeTable(string TableName, string CachedFilesDirectory, int RecordsPerPage)
        {
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/ReadOrDeserializeTable";
            callData.Body = GetArgs_Json(TableName: TableName, CachedFilesDirectory: CachedFilesDirectory, RecordsPerPage: RecordsPerPage);

            string strResults = this.MakeApiPostCall(callData, fLog);
            List<T> records = System.Text.Json.JsonSerializer.Deserialize<List<T>>(strResults);
            return records;
        }

        public IEnumerable<string> RemoveCachedFileNamesFromDirectory(string directory, string TableName)
        {
            throw new NotImplementedException();
        }

        public void SerializeToJSON(List<T> ToSerialize, string TableName, string CachedFilesDirectory, int RecordsPerPage)
        { // Client needs to handle the paging the API just needs to write a page.
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/SerializeToJSON";
            callData.Args = GetArgs_Json(TableName: TableName, CachedFilesDirectory: CachedFilesDirectory, RecordsPerPage: RecordsPerPage);

            List<T> _FailedToSerialize;
            List<T> _ToSerialize = GetSerializableRows(ToSerialize, out _FailedToSerialize);
            //callData.Body = JsonSerializer.Serialize(_ToSerialize, typeof(List<T>));
            callData.Body = Newtonsoft.Json.JsonConvert.SerializeObject(ToSerialize);

            List<List<T>> ToSerialize_Paged = GenericAggregateByPage<T>.ToPages(ToSerialize, RecordsPerPage);
            string messages = string.Empty;
            int PageNum = 1;
            foreach (var page in ToSerialize_Paged)
            {
                string PathAndFile = $"C:\\temp\\test\\tbl{TableName}_page{PageNum}.json";
                string tmpJson = string.Empty;
                try
                {
                    //tmpJson = JsonSerializer.Serialize(page, typeof(List<T>));
                    tmpJson = Newtonsoft.Json.JsonConvert.SerializeObject(page);
                }
                catch (Exception e)
                {
                    messages += e.ToString();
                }
                string strResults = SaveToFile(PathAndFile, tmpJson);
                messages += strResults + Environment.NewLine;
                PageNum++;
            }
            //return messages;
        }
        private List<T> GetSerializableRows(List<T> ToSerialize, out List<T> FailedToSerialize)
        {
            List<T> _ToSerialize = ToSerialize ?? new List<T>();
            List<T> _FailedToSerialize = new List<T>();
            List<T> _GoodToSerialize = new List<T>();

            foreach (T row in _ToSerialize)
            {
                try
                {
                    string tmp = JsonSerializer.Serialize(row);
                    _GoodToSerialize.Add(row);
                }
                catch (Exception e)
                {
                    _FailedToSerialize.Add(row);
                }
            }
            FailedToSerialize = _FailedToSerialize;
            return _GoodToSerialize;
        }
        private string GetArgs_Json(string TableName, string CachedFilesDirectory, int RecordsPerPage)
        {
            GenericSerializationCallData args = new GenericSerializationCallData();
            args.TableName = TableName;
            args.CashedFilesDirectory = CachedFilesDirectory;
            args.RecordsPerPage = RecordsPerPage;
            
            string strArgs = string.Empty;
            try
            {
               strArgs = System.Text.Json.JsonSerializer.Serialize(args, typeof(GenericSerializationCallData)) ?? String.Empty;
            }
            catch (Exception ex)
            {
                string strEmptyArgs = "{ \"TableMame\": \"\", \"CashedFilesDirectory\": \"\", \"RecordsPerPage\": 0 }";
                strArgs = string.Empty;
            }
            return strArgs;
        }
        private string SaveToFile(string PathAndFile, string content)
        {
            dynamic args = new System.Dynamic.ExpandoObject();
            args.PathAndFile = PathAndFile;

            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/SaveToFile";
            callData.Args = System.Text.Json.JsonSerializer.Serialize<dynamic>(args) ?? String.Empty;
            callData.Body = content;

            string strResults = this.MakeApiPostCall(callData, fLog);
            return strResults;
        }
    }
}
