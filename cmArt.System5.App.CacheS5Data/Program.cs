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
//using cmArt.BevNet;
//using cmArt.BevNet.System5;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Threading.Tasks;
using System.Text.Json;
using cmArt.LibIntegrations.SerializationService;


namespace cmArt.System5.App.CacheData
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

    public class ShopifyConsoleApp
    {
        class StaticSettings
        {
            public StaticSettings(IConfiguration config)
            {
                // Feature idea: Add ability validate existense of the keys used below
                string prefix = "Shopifyinfo:";
                CachedFiles = config[$"{prefix}CachedFiles"] ?? string.Empty;
                CSVFiles = config[$"{prefix}CSVFiles"] ?? string.Empty;
                OutputDirectory = config[$"{prefix}OutputDirectory"] ?? string.Empty;
                DSNinfo = config[$"{prefix}DSNinfo"] ?? string.Empty;
                Cachinginfo = config[$"{prefix}Cachinginfo"] ?? string.Empty;
                SupressUpload = config[$"{prefix}SupressUpload"] ?? string.Empty;
                LogfilePath = config[$"{prefix}LogfilePath"] ?? string.Empty;
                Hours = config[$"{prefix}Hours"] ?? string.Empty;
                Minutes = config[$"{prefix}Minutes"] ?? string.Empty;
                Seconds = config[$"{prefix}Seconds"] ?? string.Empty;
                errormail = config[$"{prefix}errormail"] ?? string.Empty;
                smtpaddress = config[$"{prefix}smtpaddress"] ?? string.Empty;
                smtpport = config[$"{prefix}smtpport"] ?? string.Empty;
                enableSSL = config[$"{prefix}enableSSL"] ?? string.Empty;
                fromemailaddress = config[$"{prefix}fromemailaddress"] ?? string.Empty;
                fromemailpassword = config[$"{prefix}fromemailpassword"] ?? string.Empty;
            }
            public string CachedFiles { get; }
            public string CSVFiles { get; }
            public string OutputDirectory { get; }
            public string DSNinfo { get; }
            public string Cachinginfo { get; }
            public string SupressUpload { get; }
            public string LogfilePath { get; }
            public string Hours { get; }
            public string Minutes { get; }
            public string Seconds { get; }
            public string errormail { get; }
            public string smtpaddress { get; }
            public string smtpport { get; }
            public string enableSSL { get; }
            public string fromemailaddress { get; }
            public string fromemailpassword { get; }

        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin");

            string[] _args = args ?? new string[] { };
            _args = args.Select(c => c.ToUpper()).ToArray();

            IConfiguration config = null;
            try
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine("A configuration file is required, Exiting. Error Message: " + e.Message);
                Environment.Exit(0);
            }

            StaticSettings settings = new StaticSettings(config);

            Console.WriteLine($"CachedFiles: {settings.CachedFiles}");
            Console.WriteLine($"CSVFiles: {settings.CSVFiles}");
            Console.WriteLine($"OutputDirectory: {settings.OutputDirectory}");
            Console.WriteLine($"DSNinfo: {settings.DSNinfo}");
            Console.WriteLine($"Cachinginfo: {settings.Cachinginfo}");
            Console.WriteLine($"SupressUpload: {settings.SupressUpload}");
            Console.WriteLine($"LogfilePath: {settings.LogfilePath}");
            Console.WriteLine($"Hours: {settings.Hours}");
            Console.WriteLine($"Minutes: {settings.Minutes}");
            Console.WriteLine($"Seconds: {settings.Seconds}");
            Console.WriteLine($"errormail: {settings.errormail}");
            Console.WriteLine($"smtpaddress: {settings.smtpaddress}");
            Console.WriteLine($"smtpport: {settings.smtpport}");
            Console.WriteLine($"enableSSL: {settings.enableSSL}");
            Console.WriteLine($"fromemailaddress: {settings.fromemailaddress}");
            Console.WriteLine($"fromemailpassword: {settings.fromemailpassword}");

            Console.WriteLine("Loading Inventory From System Five");

            IEnumerable<IS5InvAssembled> InvAss = new List<IS5InvAssembled>();
            try
            {
                Console.WriteLine("Loading System Five Data via ODBC");
                InvAss = GetDataFromSystemFive(config);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while trying to Load Data From System Five via ODBC");
                throw new Exception_WhileGettingData("An error occured Getting Data From System Five.", e);
            }
            Console.WriteLine("Finished - Loading Inventory From Real Windward");

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static IEnumerable<IS5InvAssembled> GetDataFromJson(IConfiguration config)
        {
            string CachedFilesDirectory = config["Shopifyinfo:CachedFiles"];

            Options opt = PagedJsonOptions_S5Inventory.GetOptions(CachedFilesDirectory, new List<string>());
            PagedJsonContext context = new PagedJsonContext(opt);

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
        private static bool DoWeHaveCachedFiles(IConfiguration config)
        {
            string CachedField = config["Shopifyinfo:CachedFiles"];
            int foundCount = 0;
            if (GenericSerialization<AltSuply>.Exists(CachedField, "AltSuply")) { foundCount++; }
            if (GenericSerialization<Comments>.Exists(CachedField, "Comments")) { foundCount++; }
            if (GenericSerialization<Inventry_27>.Exists(CachedField, "Inventry")) { foundCount++; }
            if (GenericSerialization<InvPrice>.Exists(CachedField, "InvPrice")) { foundCount++; }
            if (GenericSerialization<Stok>.Exists(CachedField, "Stok")) { foundCount++; }

            bool foundAll = foundCount == 5;
            return foundAll;
        }
        private static IEnumerable<IS5InvAssembled> GetDataFromSystemFive(IConfiguration config)
        {
            string DSN = config["Shopifyinfo:DSNinfo"];

            Options opt = OdbcOptions.GetOptions(DSN);
            OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);

            // Cache data here
            string CachedFilesDirectory = config["Shopifyinfo:CachedFiles"];

            Options opt2 = PagedJsonOptions_S5Inventory.GetOptions(CachedFilesDirectory, new List<string>());
            PagedJsonContext JSONContext = new PagedJsonContext();
            JSONContext.CopyFrom(context);
            JSONContext.SaveToPagedFiles(opt2);

            // Assemble Inventory - This app doesn't use the the assembled inventory
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
