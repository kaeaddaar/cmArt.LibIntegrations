using cmArt.LibIntegrations.ApiCallerService;
using cmArt.LibIntegrations.SerializationService;
using System;
using System.Collections.Generic;
using System.Text;

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

        public List<string> GetCachedFileNamesFromDirectory(string directory, string TableName)
        {
            throw new NotImplementedException();
        }

        public List<T> ReadOrDeserializeTable(string TableName, string CachedFilesDirectory, int RecordsPerPage)
        {
            Func<string, int> fLog = (x) => { Console.WriteLine(x); return 1; };
            ApiCallData callData = new ApiCallData();
            callData.UrlCommand = "/api/ReadOrDeserializeTable";
            GenericSerializationCallData args = new GenericSerializationCallData();
            args.TableName = TableName;
            args.CashedFilesDirectory = CachedFilesDirectory;
            args.RecordsPerPage = RecordsPerPage;

            string strArgs = System.Text.Json.JsonSerializer.Serialize(args, typeof(GenericSerializationCallData)) ?? String.Empty;
            callData.Body = strArgs;

            string strResults = this.MakeApiPostCall(callData, fLog);
            List<T> records = System.Text.Json.JsonSerializer.Deserialize<List<T>>(strResults);
            return records;
        }

        public IEnumerable<string> RemoveCachedFileNamesFromDirectory(string directory, string TableName)
        {
            throw new NotImplementedException();
        }

        public void SerializeToJSON(List<T> ToSerialize, string TableName, string CachedFilesDirectory, int RecordsPerPage)
        {
            throw new NotImplementedException();
        }
    }
}
