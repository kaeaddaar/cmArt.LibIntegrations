using cmArt.LibIntegrations.ApiCallerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace cmArt.WebJaguar.Connector
{
    public class WebJaguarConnector : ApiCallerBase
    {
        
        public WebJaguarConnector()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings_connector.json", optional: false, reloadOnChange: true)
                .Build();

            StaticSettings settings = new StaticSettings(config);

            _ApiConnectorData.Url = settings.WebJaguarApiUrl;
            _ApiConnectorData.UserName = settings.WebJaguarApiUsername;
            _ApiConnectorData.Password = settings.WebJaguarApiPassword;
            
            this.init(_ApiConnectorData);
        }
        public string Products_Edit(IEnumerable<Product_Root> ChangedRecords)
        {
            IEnumerable<Product_Root> _ChangedRecords = ChangedRecords ?? new List<Product_Root>();
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "";
            data.Body = "";
            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
            string results = this.MakeApiPostCall(data, logStub);
            return results;
        }
        public string Products_Add(IEnumerable<Product_Root> NewRecords)
        {
            IEnumerable<Product_Root> _ChangedRecords = NewRecords ?? new List<Product_Root>();
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "/api/v1/createProduct.jhtm";
            string results = string.Empty;
            foreach (var newRecord in _ChangedRecords)
            {
                string strNewRecord = System.Text.Json.JsonSerializer.Serialize
                    (newRecord, typeof(Product_Root));
                data.Body = strNewRecord;
                Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
                results += this.MakeApiPostCall(data, logStub);
            }

            return results;
        }
        public IEnumerable<Product_Root> GetAll_Product_Root_Records()
        {
            //this.init(_ApiConnectorData);
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "/api/v1/productSearch.jhtm?noKeywords=true&q=keyword&searchIndexedCatId=&last_modified=7/22/2019";
            data.Body = String.Empty;
            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
            string results = this.MakeApiGetCall(data.UrlCommand);

            JsonDocument doc = JsonDocument.Parse(results);
            JsonElement root = doc.RootElement;
            JsonElement prod = root.GetProperty("productList");
            IEnumerable<Product_Root> prods = (List<Product_Root>)JsonSerializer.Deserialize(prod.GetRawText(), typeof(List<Product_Root>));
            
            throw new NotImplementedException();
            return new List<Product_Root>();
        }
        public string Product_Get(int ProductId)
        {
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "/api/v1/product.jhtm?id="+ ProductId;
            data.Body = "";
            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
            //string results = this.MakeApiPostCall(data, logStub);
            string results = this.MakeApiGetCall(data.UrlCommand);
            return results;
        }
    }
}
