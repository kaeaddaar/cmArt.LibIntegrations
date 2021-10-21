using cmArt.LibIntegrations.ApiCallerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;

namespace cmArt.WebJaguar.Connector
{
    public class WebJaguarConnector : ApiCallerBase
    {
        
        public WebJaguarConnector()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
        public string Products_Add(IEnumerable<Product_Root> ChangedRecords)
        {
            IEnumerable<Product_Root> _ChangedRecords = ChangedRecords ?? new List<Product_Root>();
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "";
            data.Body = "";
            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
            string results = this.MakeApiPostCall(data, logStub);
            return results;
        }
        public IEnumerable<Product_Root> GetAll_Product_Root_Records()
        {
            //this.init(_ApiConnectorData);
            ApiCallData data = new ApiCallData();
            data.UrlCommand = "/api/v1/product.jhtm?sku=cm12612-5";
            data.Body = String.Empty;
            Func<string, int> logStub = (x) => { Console.WriteLine("Logging not yet implemented"); return 0; };
            string results = this.MapApiPostCall_Unsecured(data, logStub);

            throw new NotImplementedException();
            return new List<Product_Root>();
        }
    }
}
