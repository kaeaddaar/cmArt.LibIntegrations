using System;
using System.Collections.Generic;
using cmArt.LibIntegrations.CsvFileReaderService;
using System.Linq;
using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;
using cmArt.System5.Inventory;
using cmArt.LibIntegrations.GenericJoinsService;
using FileHelpers;
using cmArt.LibIntegrations;
using cmArt.System5.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Threading.Tasks;
using System.Text.Json;
using cmArt.LibIntegrations.SerializationService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace cmArt.SystemFive.Cache.Inventory
{
    [Serializable]
    public class Exception_WhileGettingData : Exception
    {
        public Exception_WhileGettingData() : base() { }
        public Exception_WhileGettingData(string message) : base(message) { }
        public Exception_WhileGettingData(string message, Exception inner) : base(message, inner) { }
        protected Exception_WhileGettingData(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class WebJaguarApp
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddTransient<WebJaguarApp>();
        }
        #region variables
        // SetupLogging()
        private static ServiceCollection serviceCollection;
        private static ServiceProvider serviceProvider;
        private static ILogger<WebJaguarApp> logger;
        // SetupArgs()
        private static bool PreventApiAddsNEdits;
        private static bool PreventProduct;
        private static bool PreventPrices;
        private static bool PreventQuantities;
        private static string[] _args;
        private static bool RunAsSelfCompare;
        //SetupAndDisplaySettings()
        private static IConfiguration config;
        //GetSystem5Data()
        private static IEnumerable<IS5InvAssembled> InvAss;

        #endregion variables

        private static void SetupLogging()
        {
            serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
            logger = serviceProvider.GetService<ILogger<WebJaguarApp>>();

        }
        private static void SetupAndDisplaySettings()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            logger.LogInformation($"CachedFiles: {config["S5CachingInfo:CachedFiles"]}");
            logger.LogInformation($"DSNinfo: {config["S5CachingInfo:DSNinfo"]}");
        }
        private static void SetupArgs(string[] args)
        {
            _args = args ?? new string[] { };
            _args = args.Select(c => c.ToUpper()).ToArray();
        }
        private static void GetSystem5Data()
        {
            InvAss = new List<IS5InvAssembled>();
            try
            {
                logger.LogInformation("Caching System Five Data to file via ODBC");
                InvAss = GetDataFromSystemFive(config);
            }
            catch (Exception e)
            {
                logger.LogInformation("Error occurred while trying to Load Data From System Five (via ODBC)");
                throw new Exception_WhileGettingData("An error occured Getting Data From System Five.", e);
            }
            logger.LogInformation("Finished - Caching Inventory From Real Windward");
        }
        public static void Main(string[] args)
        {
            SetupLogging();
            logger.LogInformation("Begin");
            SetupAndDisplaySettings();
            SetupArgs(args);
            logger.LogInformation("Loading Inventory From System Five");
            GetSystem5Data();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
        private static IEnumerable<IS5InvAssembled> GetDataFromSystemFive(IConfiguration config)
        {
            string DSN = config["S5CachingInfo:DSNinfo"];

            Options opt = OdbcOptions.GetOptions(DSN);
            OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);

            // Cache data here
            string CachedFilesDirectory = config["S5CachingInfo:CachedFiles"];

            Options opt2 = PagedJsonOptions_S5Inventory.GetOptions(CachedFilesDirectory, new List<string>());
            PagedJsonContext JSONContext = new PagedJsonContext();
            JSONContext.CopyFrom(context);
            JSONContext.SaveToPagedFiles(opt2);

            // Assemble Inventory
            S5Inventory InvRaw = new S5Inventory
            (
                AltSuply_Records: context.AltSuplyLines
                , Comments_Records: context.CommentsLines
                , Inventry_27_Records: context.Inventry_27s
                , InvPrice_Records: context.InvPrices
                , Stok_Records: context.StokLines
            );
            IEnumerable<IS5InvAssembled> InvAss = InvRaw.ToAssembled();
            return InvAss;
        }


    }
}

