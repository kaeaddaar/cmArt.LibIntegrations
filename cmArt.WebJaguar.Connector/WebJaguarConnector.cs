using cmArt.LibIntegrations.ApiCallerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using cmArt.WebJaguar.Data;


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
        public string Products_Edit(IEnumerable<S5_CommonFields> ChangedRecords)
        {
            IEnumerable<S5_CommonFields> _ChangedRecords = ChangedRecords ?? new List<S5_CommonFields>();
            IEnumerable<WJ_CommonFields> WJ_ChangedRecords = _ChangedRecords.Select(x =>
            {
                adapterWJ_from_S5 tmpAdapter = new adapterWJ_from_S5();
                tmpAdapter.Init(x);
                IWJ_CommonFields_In_S5 iCF = tmpAdapter;
                WJ_CommonFields CF = new WJ_CommonFields();
                CF.CopyFrom(iCF);
                return CF;
            });

            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };

            ApiCallData data = new ApiCallData();
            data.UrlCommand = "/api/v1/updateProduct.jhtm";
            //data.Body = JsonSerializer.Serialize(WJ_ChangedRecords, typeof(IEnumerable<WJ_CommonFields>));

            string results = string.Empty;
            foreach (var record in WJ_ChangedRecords)
            {
                data.Body = JsonSerializer.Serialize(record, typeof(WJ_CommonFields));
                string tmpResults = this.MakeApiPostCall(data, logStub);
                results += tmpResults;
            }

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
            int count = root.GetProperty("count").GetInt32();
            int page = root.GetProperty("page").GetInt32();
            int pageSize = root.GetProperty("pageSize").GetInt32();

            List<Product_Root> AllProducts = new List<Product_Root>();
            prods.ToList().ForEach(p => AllProducts.Add(p));

            bool ThereAreMorePages = pageSize * page <= count;
            if (ThereAreMorePages)
            {
                Console.WriteLine($"There are approximately {count / pageSize + 1} pages to process with GetAll_Product_Root_Records()");
                while (ThereAreMorePages)
                {
                    page++;
                    Console.Write($"{page} ");
                    ThereAreMorePages = pageSize * page <= count;
                    if (page > 5) { ThereAreMorePages = false; } // remove on release

                    data.UrlCommand = $"/api/v1/productSearch.jhtm?page={page}&noKeywords=true&q=keyword&searchIndexedCatId=&last_modified=7/22/2019";
                    data.Body = String.Empty;
                    results = this.MakeApiGetCall(data.UrlCommand);

                    doc = JsonDocument.Parse(results);
                    root = doc.RootElement;
                    prod = root.GetProperty("productList");
                    prods = (List<Product_Root>)JsonSerializer.Deserialize(prod.GetRawText(), typeof(List<Product_Root>));
                    int count_info = root.GetProperty("count").GetInt32();
                    int page_info = root.GetProperty("page").GetInt32();
                    int pageSize_info = root.GetProperty("pageSize").GetInt32();

                    prods.ToList().ForEach(p => AllProducts.Add(p));
                }
            }
            return AllProducts;
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
