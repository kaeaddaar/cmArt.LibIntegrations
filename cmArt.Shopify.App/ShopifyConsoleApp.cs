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
using cmArt.Shopify.App.Data;
using System.Text.Json;
using cmArt.LibIntegrations.SerializationService;
using cmArt.Shopify.Connector.Data;
using cmArt.Shopify.Connector;
using cmArt.Reece.ShopifyConnector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using cmArt.Shopify.App.Services;
using cmArt.LibIntegrations.VennMapService;
using cmArt.Shopify.App.ReportViews;
using cmArt.LibIntegrations.ReportService;
using cmArt.LibIntegrations.LoggingServices;

using cmArt.Portal.API;
using cmArt.Portal.Data;
using cmArt.Portal.API.Data;
using cmArt.Portal.API.Repositories;

namespace cmArt.Shopify.App
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
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddTransient<ShopifyConsoleApp>();
        }
        #region variables
        // SetupLogging()
        private static ServiceCollection serviceCollection;
        private static ServiceProvider serviceProvider;
        private static ILogger logger;
        private static ILogger logger_ApiCalls;
        private static ILogger logger_AtOrAfter;
        // SetupArgs()
        private static bool PreventApiAddsNEdits;
        private static bool PreventProduct;
        private static bool PreventPrices;
        private static bool PreventQuantities;
        private static string[] _args;
        private static bool RunAsSelfCompare;
        private static bool RunSyncOnce;
        private static bool RunAsSyncService;
        //SetupAndDisplaySettings()
        private static IConfiguration config;
        private static StaticSettings settings;
        //GetSystem5Data()
        private static IEnumerable<IS5InvAssembled> InvAss;
        //FilterForECommAndSave()
        private static IEnumerable<IS5InvAssembled> ECommInvAss;
        //CreateDataLoadLists()
        private static IEnumerable<AdaptToShopifyDataLoadFormat> adapters;
        private static IEnumerable<Shopify_Product> PocoProductsAdapted;
        private static IEnumerable<Shopify_Prices> PocoPricesAdapted;
        private static IEnumerable<Shopify_Quantities> PocoQuantitiesAdapted;
        private static IEnumerable<IShopify_Product> prods;
        private static IEnumerable<IShopify_Prices> prices;
        private static IEnumerable<IShopify_Quantities> quantities;
        //GetPrevDataLoadLists()
        private static IEnumerable<Shopify_Product> PrevDataLoad_Product;
        private static IEnumerable<Shopify_Quantities> PrevDataLoad_Quantities;
        private static IEnumerable<Shopify_Prices> PrevDataLoad_Prices;
        //GetShopifyData()
        private static List<Changes_View> changes = new List<Changes_View>();
        private static Func<IShopify_Prices, IShopify_Prices, bool> fEquals_Prices;
        private static Func<IShopify_Quantities, IShopify_Quantities, bool> fEquals_Quantities;
        private static Func<IShopify_Product, IShopify_Product, bool> fEquals_Product;
        //GetShopifyData_Reece_Products()
        private static IEnumerable<Shopify_Product> API_Products;
        private static IEnumerable<Shopify_Prices> API_Prices;
        private static IEnumerable<Shopify_Quantities> API_Quantities;
        //GetShopifyData_Reece_Prices()
        private static Func<IShopify_Product, string> fGetProductPartNumber;
        private static Func<IShopify_Prices, string> fGetPricesPartNumber;
        //GetChangedRecords()
        private static IEnumerable<IShopify_Product> ChangedRecords_Product;
        private static IEnumerable<IShopify_Prices> ChangedRecords_Prices;
        private static IEnumerable<IShopify_Quantities> ChangedRecords_Quantities;
        //ProduceVennMap()
        private static VennMap_InvAss<Shopify_Product, int> map_Product;
        private static VennMap_InvAss<Shopify_Prices, int> map_Prices;
        private static VennMap_InvAss<Shopify_Quantities, int> map_Quantities;
        //
        private static DateTime dtLastRun;
        private static string dtLastRunPathAndFile;


        #endregion variables
        public static void Main_Console(string[] args)
        {
            SetupSettings();
            SetupLogging();
            SetupArgs(args);

            dtLastRunPathAndFile = settings.LogfilePath + "\\lastrun.txt";
            bool IsAlreadyRanToday = AlreadyRanToday();
            int ThirtySeconds = 1000 * 30;

            if (RunAsSelfCompare)
            {
                SetupLogging_ForDoWork();
                DoWork(args);
            }
            if (RunSyncOnce)
            {
                SetupLogging_ForDoWork();
                DoWork(args);
            }
            if (RunAsSyncService)
            {
                if (!IsAlreadyRanToday)
                {
                    if (AtOrAfterTimeToRun())
                    {
                        SetupLogging_ForDoWork();
                        DoWork(args);
                    }
                }
            }
            System.Threading.Thread.Sleep(ThirtySeconds);//always wait thirty seconds after any check or action
        }
        public static void DoWork(string[] args)
        {
            logger.LogInformation("Begin");

            DisplaySettings();

            SetupArgs(args);

            logger.LogInformation("Loading Inventory From System Five");

            GetSystem5Data();

            DemoPrep_TurnOnEcommFlagForTop3InventoryItemsOfEachCategory();

            FilterForECommAndSave();

            Transform_Assembled_Inventory_To_Products_Prices_And_Quantities();


            GetEqualityFunctions();

            if (RunAsSelfCompare)
            {
                Cache_And_Overwrite_Products_Prices_And_Quantities();
            }
            else
            {
                Cache_Products_Prices_And_Quantities_From_SystemFive();
                GetShopifyData();
                CacheShopifyData();
                map_Product = ProduceVennMap(map_Product);
                map_Prices = ProduceVennMap(map_Prices);
                map_Quantities = ProduceVennMap(map_Quantities);
                CheckForDuplicates();
            }

            GetChangedRecords();

            GetDetailedDifferences_And_Create_Reports();

            ProduceReportsBeforeProcessing();

            // Direct from Shopify
            if (false)
            {
                logger.LogInformation("Get Products directly from Shopify");
                List<Product_Product> all = cmShopify.GetAllShopifyRecords().ToList();
                string strProducts = System.Text.Json.JsonSerializer.Serialize(all, typeof(List<Product_Product>));
                IEnumerable<ProductAdapter> AllProduct = all.Select(prod => { ProductAdapter pa = new ProductAdapter(); pa.Init(prod); return pa; });
            }

            #region Perform Edits
            logger.LogInformation("Begin Performing Edits");
            IEnumerable<Shopify_Product> changedProducts = new List<Shopify_Product>();
            IEnumerable<Shopify_Prices> changedPrices = new List<Shopify_Prices>();
            IEnumerable<Shopify_Quantities> changedQuantities = new List<Shopify_Quantities>();
            if (!PreventApiAddsNEdits)
            {
                changedProducts = ChangedRecords_Product.Select(p => p.AsShopify_Product());
                string Product_Edit_Results = string.Empty;
                if (!PreventProduct)
                {
                    logger.LogInformation("Performing Edits on Changed Products");
                    logger.LogInformation($"Number of Changed Products: {changedProducts.Count()}");
                    Product_Edit_Results = ReeceShopify.Products_Edit(changedProducts);

                    string FileNameChangedProducts = settings.OutputDirectory + "\\changedProducts.json.txt";
                    string content = System.Text.Json.JsonSerializer.Serialize(changedProducts.ToList(), typeof(List<Shopify_Product>));
                    System.IO.File.WriteAllText(FileNameChangedProducts, content);

                    var iNotUn = changedProducts.Where(x => x.WebCategory != "Uncategorized" && x.WebCategory != string.Empty).Count();
                    var NotUn = changedProducts.Where(x => x.WebCategory != "Uncategorized" && x.WebCategory != string.Empty);
                    content = System.Text.Json.JsonSerializer.Serialize(NotUn.ToList(), typeof(List<Shopify_Product>));
                    System.IO.File.WriteAllText(FileNameChangedProducts, content);
                }
                else { logger.LogInformation("Preventing edits on changed products"); }

                changedPrices = ChangedRecords_Prices.Select(p => p.AsShopify_Prices());
                string Prices_Edit_Results = string.Empty;
                if (!PreventPrices)
                {
                    logger.LogInformation("Performing Edits on Changed Prices");
                    logger.LogInformation($"Number of Changed Prices {changedPrices.Count()}");
                    Prices_Edit_Results = ReeceShopify.Prices_Edit(changedPrices);
                    string FileNameChangedPrices = settings.OutputDirectory + "\\changedPrices.json.txt";
                    string content = System.Text.Json.JsonSerializer.Serialize(changedPrices.ToList(), typeof(List<Shopify_Prices>));
                    System.IO.File.WriteAllText(FileNameChangedPrices, content);
                }
                else { logger.LogInformation("Preventing edits on changed prices"); }

                changedQuantities = ChangedRecords_Quantities.Select(p => p.AsShopify_Quantities());
                string Quantities_Edit_Results = string.Empty;
                if (!PreventQuantities)
                {
                    logger.LogInformation("Performing Edits on Changed Quantities");
                    logger.LogInformation($"Number of Changed Quantities: {changedQuantities.Count()}");
                    Quantities_Edit_Results = ReeceShopify.Quantities_Edit(changedQuantities);
                    string FileNameChangedQuantities = settings.OutputDirectory + "\\changedQuantities.json.txt";
                    string content = System.Text.Json.JsonSerializer.Serialize(changedQuantities.ToList(), typeof(List<Shopify_Quantities>));
                    System.IO.File.WriteAllText(FileNameChangedQuantities, content);
                }
                else { logger.LogInformation("Preventing edits on changed quantities"); }

            }
            else
            {
                logger.LogInformation("All Perform Edits Supressed due to Settings");
            }
            #endregion Perform Edits

            #region Perform Adds
            logger.LogInformation("Begin Perform Adds");
            IEnumerable<Tuple<IShopify_Product, IShopify_Product>> NewProductsPairs = new List<Tuple<IShopify_Product, IShopify_Product>>();
            IEnumerable<Tuple<IShopify_Prices, IShopify_Prices>> NewPricesPairs = new List<Tuple<IShopify_Prices, IShopify_Prices>>();
            IEnumerable<Tuple<IShopify_Quantities, IShopify_Quantities>> NewQuantitiesPairs = new List<Tuple<IShopify_Quantities, IShopify_Quantities>>();

            //API_Products, jp, jq
            NewProductsPairs = GenericJoins<IShopify_Product, IShopify_Product, int>
            .LeftJoin(adapters, API_Products, IShopifyDataLoadFormat_Indexes.UniqueId, IShopifyDataLoadFormat_Indexes.UniqueId);
            IEnumerable<Shopify_Product> NewProducts = NewProductsPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsShopify_Product());
            if (!PreventApiAddsNEdits)
            {
                if (!PreventProduct)
                {
                    logger.LogInformation("Performing Products_Add on NewProducts");
                    logger.LogInformation($"Number of records in NewProducts: {NewProducts.Count()}");
                    string Product_Add_Results = ReeceShopify.Products_Add(NewProducts);
                }
                else { logger.LogInformation("Prevented adding of NewProducts"); }
            }
            try
            {
                string FileNameNewProducts = settings.OutputDirectory + "\\NewProducts.json.txt";
                logger.LogInformation($"Saving NewProducts to file: {FileNameNewProducts}");
                string content = System.Text.Json.JsonSerializer.Serialize(NewProducts.ToList(), typeof(List<Shopify_Product>));
                System.IO.File.WriteAllText(FileNameNewProducts, content);
            }
            catch (Exception e)
            {
                logger.LogInformation("Error serializing and saving new products to file. Message: " + e.Message);
            }

            PauseToGiveSomeTimeForNewProductsToLoad(NewProducts.Count());

            NewPricesPairs = GenericJoins<IShopify_Prices, IShopify_Prices, int>
                .LeftJoin(adapters, API_Prices, IShopifyDataLoadFormat_Indexes.UniqueId, IShopifyDataLoadFormat_Indexes.UniqueId);
            IEnumerable<Shopify_Prices> NewPrices = NewPricesPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsShopify_Prices());

            List<List<Shopify_Prices>> Pages_Prices = GenericAggregateByPage<Shopify_Prices>.ToPages(NewPrices, 10);
            string Prices_Add_Results = string.Empty;

            if (!PreventApiAddsNEdits)
            {
                if (!PreventPrices)
                {
                    logger.LogInformation("Performing Prices_Add on NewPrices");
                    logger.LogInformation($"Number of records in NewPrices: {NewPrices.Count()}");

                    foreach (var page in Pages_Prices)
                    {
                        string strTempPrice_Add_Results = ReeceShopify.Prices_Add(NewPrices);
                        Prices_Add_Results += strTempPrice_Add_Results;
                    }
                }
                else { logger.LogInformation("Prevented adding of NewPrices"); }
            }
            try
            {
                string FileNameNewPrices = settings.OutputDirectory + "\\NewPrices.json.txt";
                logger.LogInformation($"Saving NewPrices to file: {FileNameNewPrices}");
                string content = System.Text.Json.JsonSerializer.Serialize(NewPrices.ToList(), typeof(List<Shopify_Prices>));
                System.IO.File.WriteAllText(FileNameNewPrices, content);
            }
            catch (Exception e)
            {
                logger.LogInformation("Error serializing and saving new prices to file. Message: " + e.Message);
            }

            NewQuantitiesPairs = GenericJoins<IShopify_Quantities, IShopify_Quantities, int>
                .LeftJoin(adapters, API_Quantities, IShopifyDataLoadFormat_Indexes.UniqueId, IShopifyDataLoadFormat_Indexes.UniqueId);
            IEnumerable<Shopify_Quantities> NewQuantities = NewQuantitiesPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsShopify_Quantities());

            List<List<Shopify_Quantities>> Pages_Quantities = GenericAggregateByPage<Shopify_Quantities>.ToPages(NewQuantities, 10);
            string Quantities_Add_Results = string.Empty;

            if (!PreventApiAddsNEdits)
            {
                if (!PreventQuantities)
                {
                    logger.LogInformation("Performing Quantities_Add on NewQuantities");
                    logger.LogInformation($"Number of records in NewQuantities: {NewQuantities.Count()}");
                    foreach (var page in Pages_Quantities)
                    {
                        string tmpString = ReeceShopify.Quantities_Add(page);
                        Quantities_Add_Results += tmpString;
                    }
                }
                else { logger.LogInformation("Prevented addinf of NewQuantities"); }
            }
            try
            {
                string FileNameNewQuantities = settings.OutputDirectory + "\\NewQuantities.json.txt";
                logger.LogInformation($"Saving NewQuantities to file: {FileNameNewQuantities}");
                string content = System.Text.Json.JsonSerializer.Serialize(NewQuantities.ToList(), typeof(List<Shopify_Quantities>));
                System.IO.File.WriteAllText(FileNameNewQuantities, content);
            }
            catch (Exception e)
            {
                logger.LogInformation("Error serializing and saving new quantities to file. Message: " + e.Message);
            }
            #endregion Perform Adds

            #region Perform Deletes
            PerformDeletes();
            #endregion Perform Deletes
            // ----- Reporting goes here -----

            string result2 = SerializeForExport(adapters);
            string result = string.Empty;

            try
            {
                logger.LogInformation("Attempting to serialize ChangedRecords_Product");
                IEnumerable<Shopify_Product> _ChangedRecords_Product = ChangedRecords_Product.Select(rec => (Shopify_Product)(new Shopify_Product().CopyFrom(rec)));
                result = JsonSerializer.Serialize(_ChangedRecords_Product, typeof(IEnumerable<Shopify_Product>));
            }
            catch
            {
                logger.LogInformation("Serialize of ChangedRecordPairs_Product falied");
            }
            IEnumerable<Shopify_Product> _AllRecords_Product = adapters.Select(rec => (Shopify_Product)(new Shopify_Product().CopyFrom(rec)));
            string result3 = JsonSerializer.Serialize(_AllRecords_Product, typeof(IEnumerable<Shopify_Product>));

            string FileName = settings.OutputDirectory + "\\_ChangedRecords_Product.json.txt";
            logger.LogInformation($"Saving ChangedRecords_Product to file: {FileName}");
            File.WriteAllText(FileName, result);

            Save_dtLastRun_ToFile();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
        private static bool AlreadyRanToday()
        {
            Get_dtLastRun_FromFile();
            DateTime dtNow = DateTime.Now;
            DateTime dtStartOfDay = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            bool IsToday = dtStartOfDay <= dtLastRun;
            LogInfo_AtOrAfter($"AlreadyRanToday Checks - Now: {dtNow.ToString()}, LastRun: {dtLastRun.ToString()}, AlreadyRanToday: {IsToday}");
            return IsToday;
        }
        private static void Get_dtLastRun_FromFile()
        {
            string strLastRun = string.Empty;
            try
            {
                bool fileExists = File.Exists(dtLastRunPathAndFile);
                if (!fileExists)
                {
                    DateTime tmp = new DateTime();
                    File.WriteAllText(dtLastRunPathAndFile, tmp.ToString());
                }
                strLastRun = File.ReadAllText(dtLastRunPathAndFile);
            }
            catch (Exception e)
            {
                LogInfo_AtOrAfter("Error reading lastrun time from file: " + e.ToString());
                strLastRun = DateTime.Now.ToString();
            }
            DateTime.TryParse(strLastRun, out dtLastRun);
        }
        private static void Save_dtLastRun_ToFile()
        {
            dtLastRun = DateTime.Now;
            try
            {
                File.WriteAllText(dtLastRunPathAndFile, dtLastRun.ToString());
            }
            catch (Exception e)
            {
                logger.LogInformation("Error saving time of last run to file: " + e.ToString());
            }
        }
        private static void LogInfo_AtOrAfter(string msg)
        {
            try
            {
                Console.WriteLine(msg);
                logger_AtOrAfter.LogInformation(msg);
            }
            catch (Exception e)
            {
                string tmp = $"Error attempting to LogAMessage. message: {e.Message}";
                Console.WriteLine(tmp);
            }
        }
        private static bool AtOrAfterTimeToRun()
        {
            DateTime dtNow = DateTime.Now;
            int hrs = 0;
            int.TryParse(settings.Hours, out hrs);
            int mins = 0;
            int.TryParse(settings.Minutes, out mins);

            bool IsAtOrAfterTimeToRun = false;
            if (dtNow.Hour == hrs)
            {
                if (dtNow.Minute >= mins)
                {
                    IsAtOrAfterTimeToRun = true;
                }
            }
            if (dtNow.Hour > hrs)
            {
                IsAtOrAfterTimeToRun = true;
            }
            LogInfo_AtOrAfter($"AtOrAfterTimeToRun - Hour: {hrs}, Minutes: {mins}, AtOrAfter: {IsAtOrAfterTimeToRun}");
            return IsAtOrAfterTimeToRun;

        }

        private static void SetupLogging()
        {
            LogToFile logger_ToFile = new LogToFile();
            logger_ToFile.Init(settings.LogfilePath + "\\Integration_LogFile.txt");
            logger = logger_ToFile;

            LogToFile logger_ToFile_AtOrAfter = new LogToFile();
            logger_ToFile_AtOrAfter.Init(settings.LogfilePath + "\\AtOrAfter_Sync.txt", false);
            logger_AtOrAfter = logger_ToFile_AtOrAfter;
        }
        private static void SetupLogging_ForDoWork()
        {
            LogToFile logger_ToFile_ApiCalls = new LogToFile();
            logger_ToFile_ApiCalls.Init(settings.LogfilePath + "\\ApiCalls.txt", false);
            logger_ApiCalls = logger_ToFile_ApiCalls;
        }
        private static void SetupSettings()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            settings = new StaticSettings(config);
        }
        private static void DisplaySettings()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            settings = new StaticSettings(config);

            logger.LogInformation($"CachedFiles: {settings.CachedFiles}");
            logger.LogInformation($"CSVFiles: {settings.CSVFiles}");
            logger.LogInformation($"OutputDirectory: {settings.OutputDirectory}");
            logger.LogInformation($"DSNinfo: {settings.DSNinfo}");
            logger.LogInformation($"Cachinginfo: {settings.Cachinginfo}");
            logger.LogInformation($"SupressUpload: {settings.SupressUpload}");
            logger.LogInformation($"LogfilePath: {settings.LogfilePath}");
            logger.LogInformation($"Hours: {settings.Hours}");
            logger.LogInformation($"Minutes: {settings.Minutes}");
            logger.LogInformation($"Seconds: {settings.Seconds}");
            logger.LogInformation($"errormail: {settings.errormail}");
            logger.LogInformation($"smtpaddress: {settings.smtpaddress}");
            logger.LogInformation($"smtpport: {settings.smtpport}");
            logger.LogInformation($"enableSSL: {settings.enableSSL}");
            logger.LogInformation($"fromemailaddress: {settings.fromemailaddress}");
            logger.LogInformation($"fromemailpassword: {settings.fromemailpassword}");
            logger.LogInformation($"RunAs: {settings.RunAs}");
        }
        private static void SetupArgs(string[] args)
        {
            _args = args ?? new string[] { };
            _args = args.Select(c => c.ToUpper()).ToArray();

            PreventApiAddsNEdits = _args.Contains("PREVENTADDSANDEDITS") || _args.Contains("PREVENTADDSNEDITS") || settings.SupressUpload.ToUpper() == "SUPRESS";
            PreventProduct = !(_args.Contains("PRODUCTS") || _args.Contains("PRODUCT")) && _args.Count() > 0;
            PreventPrices = !(_args.Contains("DISCOUNTS") || _args.Contains("DISCOUNT")) && _args.Count() > 0;
            PreventQuantities = !(_args.Contains("INVENTORY") || _args.Contains("INVENTORYITEMS") || _args.Contains("INVENTORYITEM")) && _args.Count() > 0;

            if (PreventApiAddsNEdits) { logger.LogInformation("PREVENTADDSANDEDITS or PREVENTADDSNEDITS found in arguments, adds and edits will be prevented"); }
            if (PreventProduct) { logger.LogInformation("PRODUCTS or PRODUCT not found in arguments so we will prevent them from being sent to Shopify"); }
            if (PreventPrices) { logger.LogInformation("DISCOUNTS or DISCOUNT not found in arguments so we will prevent them from being sent to Shopify"); }
            if (PreventQuantities)
            {
                logger.LogInformation("INVENTORY or INVENTORYITEMS or INVENTORYITEM not found in arguments so we will prevent them from "
                    + "being sent to Shopify");
            }
            RunAsSelfCompare = (settings.RunAs.ToUpper() == "SELFCOMPARE".ToUpper());
            RunSyncOnce = (settings.RunAs.ToUpper() == "RunSyncOnce".ToUpper());
            RunAsSyncService = (settings.RunAs.ToUpper() == "SYNC".ToUpper());
        }
        private static void GetSystem5Data()
        {
            InvAss = new List<IS5InvAssembled>();
            try
            {
                if (settings.Cachinginfo == "UseCaching" && DoWeHaveCachedFiles(config))
                {
                    logger.LogInformation("Loading Cached System Five Data");
                    InvAss = GetDataFromJson(config);
                }
                if (settings.Cachinginfo != "UseCaching" || InvAss.Count() == 0)
                {
                    logger.LogInformation("Loading System Five Data via ODBC");
                    InvAss = GetDataFromSystemFive(config);
                }

            }
            catch (Exception e)
            {
                logger.LogInformation("Error occurred while trying to Load Data From System Five (Cache or ODBC)");
                throw new Exception_WhileGettingData("An error occured Getting Data From System Five.", e);
            }
            logger.LogInformation("Finished - Loading Inventory From Real Windward");
        }
        private static void DemoPrep_TurnOnEcommFlagForTop3InventoryItemsOfEachCategory()
        {
            logger.LogInformation("Performing Demo Prep step - Setting Ecomm to Y for top 3 items of each category.");
            IEnumerable<IGrouping<string, IS5InvAssembled>> grouped = InvAss.OrderBy(x => x.Inv.Cat).GroupBy(x => x.Inv.Cat);
            foreach(var group in grouped)
            {
                IEnumerable<IS5InvAssembled> tmpGroup = group.Take(3);
                foreach(var item in tmpGroup)
                {
                    item.Inv.Ecommerce = "Y";
                }
            }
            logger.LogInformation("Finished Performing Demo Prep step");
        }
        private static void FilterForECommAndSave()
        {
            logger.LogInformation("Filtering for Ecommerce equals Y");
            ECommInvAss = InvAss.Where(prod => prod.Inv.Ecommerce == "Y");
            Reports.SaveReport(ECommInvAss, "EcommProduct_From_SystemFive", settings, logger);
            //string strECommInvAss = SerializeForExport(ECommInvAss);
            //File.WriteAllText(settings.OutputDirectory + "\\strEcommInvAss.txt", strECommInvAss);
        }
        private static void Transform_Assembled_Inventory_To_Products_Prices_And_Quantities()
        {
            // Use facade to create data load format from Assembled Inventory Data
            logger.LogInformation("Begin converting Assembled Inventory Records to the Shopify Data Load Format via an adapter.");
            adapters = ECommInvAss.Select(Inv =>
            {
                AdaptToShopifyDataLoadFormat tmp = new AdaptToShopifyDataLoadFormat();
                tmp.Init(Inv);
                return tmp;
            }
            );

            logger.LogInformation(" -- Get products from adapter");
            PocoProductsAdapted = adapters.Select(x => x.AsShopify_Product());
            prods = PocoProductsAdapted;
            Reports.SaveReport(prods, "FromSystem5_Products", settings, logger);

            logger.LogInformation(" -- Get prices from adapter");
            PocoPricesAdapted = adapters.Select(x => x.AsShopify_Prices());
            prices = PocoPricesAdapted;
            Reports.SaveReport(prices, "FromSystem5_Prices", settings, logger);

            logger.LogInformation(" -- Get quantities from adapter");
            PocoQuantitiesAdapted = adapters.Select(x => x.AsShopify_Quantities());
            quantities = PocoQuantitiesAdapted;
            Reports.SaveReport(quantities, "FromSystem5_Quantities", settings, logger);

            // The old way - DNU unless you need to convert back to the adapter later on
            //prods = adapters.Select(x => (IShopify_Product)x);
            //prices = adapters.Select(x => (IShopify_Prices)x);
            //quantities = adapters.Select(x => (IShopify_Quantities)x);
        }
        private static void Cache_And_Overwrite_Products_Prices_And_Quantities()
        {
            CachingPattern_Shopify_Product cacheShopify_Product = new CachingPattern_Shopify_Product("PocoProductsAdapted", settings);
            PrevDataLoad_Product = cacheShopify_Product._01_GetPrev();
            cacheShopify_Product._02_SaveNewestToCache(PocoProductsAdapted);

            CachingPattern_Shopify_Quantities cacheShopify_Quantities = new CachingPattern_Shopify_Quantities("PocoQuantitiesAdapted", settings);
            PrevDataLoad_Quantities = cacheShopify_Quantities._01_GetPrev();
            cacheShopify_Quantities._02_SaveNewestToCache(PocoQuantitiesAdapted);

            CachingPattern_Shopify_Prices cacheShopify_Prices = new CachingPattern_Shopify_Prices("PocoPricesAdapted", settings);
            PrevDataLoad_Prices = cacheShopify_Prices._01_GetPrev();
            cacheShopify_Prices._02_SaveNewestToCache(PocoPricesAdapted);

            API_Products = PrevDataLoad_Product;
            API_Prices = PrevDataLoad_Prices;
            API_Quantities = PrevDataLoad_Quantities;
        }
        private static void Cache_Products_Prices_And_Quantities_From_SystemFive()
        {
            CachingPattern_Shopify_Product cacheShopify_Product = new CachingPattern_Shopify_Product("PocoProductsAdapted", settings);
            cacheShopify_Product._02_SaveNewestToCache(PocoProductsAdapted);

            CachingPattern_Shopify_Prices cacheShopify_Prices = new CachingPattern_Shopify_Prices("PocoPricesAdapted", settings);
            cacheShopify_Prices._02_SaveNewestToCache(PocoPricesAdapted);

            CachingPattern_Shopify_Quantities cacheShopify_Quantities = new CachingPattern_Shopify_Quantities("PocoQuantitiesAdapted", settings);
            cacheShopify_Quantities._02_SaveNewestToCache(PocoQuantitiesAdapted);

            Context context = new Context();

            Document S5_Product_Document = new Document();
            S5_Product_Document.Id = new Guid("74e5b8d9-f264-4029-af3a-f2c44c906511");
            S5_Product_Document.DocumentName = "S5_Product";
            S5_Product_Document.CustomerId = Guid.Empty;
            S5_Product_Document.ProjectId = Guid.Empty;
            S5_Product_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(PocoProductsAdapted, typeof(IEnumerable<Shopify_Product>));
            Document_Repository.AddJsonDocument(context, S5_Product_Document);

            Document S5_Prices_Document = new Document();
            S5_Prices_Document.Id = new Guid("7fb824a1-6b6a-46eb-bb0c-e4a315ccb6ec");
            S5_Prices_Document.DocumentName = "S5_Prices";
            S5_Prices_Document.CustomerId = Guid.Empty;
            S5_Prices_Document.ProjectId = Guid.Empty;
            S5_Prices_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(PocoPricesAdapted, typeof(IEnumerable<Shopify_Prices>));
            Document_Repository.AddJsonDocument(context, S5_Prices_Document);

            Document S5_Quantities_Document = new Document();
            S5_Quantities_Document.Id = new Guid("c0aff18b-a32a-4b0b-bcc7-5808ddace171");
            S5_Quantities_Document.DocumentName = "S5_Quantities";
            S5_Quantities_Document.CustomerId = Guid.Empty;
            S5_Quantities_Document.ProjectId = Guid.Empty;
            S5_Quantities_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(PocoQuantitiesAdapted, typeof(IEnumerable<Shopify_Quantities>));
            Document_Repository.AddJsonDocument(context, S5_Quantities_Document);

        }
        private static void GetShopifyData_Reece_Products()
        {
            logger.LogInformation("Get Products using Reece's API");
            API_Products = new List<Shopify_Product>();
            try
            {
                API_Products = ReeceShopify.GetAllShopify_Products();
                logger.LogInformation($"Saving Shopify Products for ShopifyAPI_Products");
                try
                {
                    GenericSerialization<Shopify_Product>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, "ShopifyAPI_Products");
                    GenericSerialization<Shopify_Product>.SerializeToJSON(API_Products.ToList(), "ShopifyAPI_Products", settings.OutputDirectory, 50000);
                }
                catch
                {
                    logger.LogInformation($"Failed to serialize API_Products. API_Products file(s) will not be written.");
                }
            }
            catch (Exception e)
            {
                logger.LogInformation($"Failed to get All shopify products from Custom API. Message = \"{e.Message}\"");
                Console.ReadKey();
            }

            Reports.SaveReport(API_Products, "FromShopify_Products", settings, logger);
        }
        private static void GetShopifyData_Reece_Prices()
        {
            logger.LogInformation("Get Prices using Reece's API");
            IEnumerable<tmpShopify_Prices> tmpPrices = new List<tmpShopify_Prices>();
            try
            {
                tmpPrices = ReeceShopify.GetAlltmpShopify_Prices();
                string FileOrTableName = "ShopifyAPI_Prices";
                logger.LogInformation($"Saving Shopify Prices to file: {FileOrTableName}");
                try
                {
                    GenericSerialization<tmpShopify_Prices>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, FileOrTableName);
                    GenericSerialization<tmpShopify_Prices>.SerializeToJSON(tmpPrices.ToList(), FileOrTableName, settings.OutputDirectory, 50000);
                }
                catch
                {
                    logger.LogInformation($"Failed to serialize {FileOrTableName}. {FileOrTableName} file(s) will be missing");
                }
            }
            catch (Exception e)
            {
                logger.LogInformation($"Failed to get All shopify prices from Custom API. Message = \"{e.Message}\"");
                Console.ReadKey();
            }
            IEnumerable<Shopify_Prices> MissingInfoPrices = tmpPrices.Select(p => p.AsShopify_Prices());
            fGetProductPartNumber = (sp) => { return sp.PartNumber.TrimEnd(); };
            fGetPricesPartNumber = (sp) => { return sp.PartNumber.TrimEnd(); };

            IEnumerable<Tuple<Shopify_Product, Shopify_Prices>> joined_prices =
                GenericJoins<Shopify_Product, Shopify_Prices, string>.LeftJoin(API_Products, MissingInfoPrices, fGetProductPartNumber, fGetPricesPartNumber);

            API_Prices = joined_prices.Where(p => p.Item2 != null).Select(p =>
            {
                p.Item2.InvUnique = p.Item1.InvUnique;
                p.Item2.Cat = p.Item1.Cat;
                //p.Item2.WholesaleCost = Math.Round(p.Item1.WholesaleCost, 6);
                return p.Item2;
            });

            Reports.SaveReport(API_Prices, "FromShopify_Prices", settings, logger);
        }
        private static void GetShopifyData_Reece_Quantities()
        {
            logger.LogInformation("Get Quantities from Reece's API");
            //IEnumerable<IShopify_Quantities> API_Quantities = ReeceShopify.GetAllShopify_Quantities();
            IEnumerable<tmpShopify_Quantities> tmpApi_Quantities = new List<tmpShopify_Quantities>();
            try
            {
                tmpApi_Quantities = ReeceShopify.GetAlltmpShopify_Quantities();
                string FileOrTableName = "ShopifyAPI_Quantities";
                logger.LogInformation($"Saving Shopify Quantities to file: {FileOrTableName}");
                try
                {
                    GenericSerialization<tmpShopify_Quantities>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, FileOrTableName);
                    GenericSerialization<tmpShopify_Quantities>.SerializeToJSON(tmpApi_Quantities.ToList(), FileOrTableName, settings.OutputDirectory, 50000);
                }
                catch
                {
                    logger.LogInformation($"Failed to serialize {FileOrTableName}. {FileOrTableName} file(s) will be missing");
                }
            }
            catch (Exception e)
            {
                logger.LogInformation($"Failed to get All shopify quantities from Custom API. Message = \"{e.Message}\"");
                Console.ReadKey();
            }
            IEnumerable<Shopify_Quantities> MissingInfoQuantities = tmpApi_Quantities.Select(p => p.AsShopify_Quantities());
            Func<IShopify_Quantities, string> fGetQuantitiesPartNumber = (sp) => { return sp.PartNumber; };

            IEnumerable<Tuple<IShopify_Product, Shopify_Quantities>> joined_quantities =
                GenericJoins<IShopify_Product, Shopify_Quantities, string>.LeftJoin(API_Products, MissingInfoQuantities, fGetProductPartNumber, fGetQuantitiesPartNumber);
            API_Quantities = joined_quantities.Where(p => p.Item2 != null).Select(p =>
            {
                p.Item2.InvUnique = p.Item1.InvUnique;
                p.Item2.Cat = p.Item1.Cat;
                return p.Item2;
            });

            Reports.SaveReport(API_Quantities, "FromShopify_Quantities", settings, logger);
        }
        private static void GetShopifyData()
        {
            changes = changes ?? new List<Changes_View>();

            ReeceShopify.AddLogger(logger, logger_ApiCalls);
            GetShopifyData_Reece_Products();
            GetShopifyData_Reece_Prices();
            GetShopifyData_Reece_Quantities();
        }
        private static void CacheShopifyData()
        {
            CachingPattern_Shopify_Product cacheShopify_Product = new CachingPattern_Shopify_Product("ShopifyProducts_ReecesAPI", settings);
            cacheShopify_Product._02_SaveNewestToCache(API_Products);

            CachingPattern_Shopify_Prices cacheShopify_Prices = new CachingPattern_Shopify_Prices("ShopifyPrices_ReecesAPI", settings);
            cacheShopify_Prices._02_SaveNewestToCache(API_Prices);

            CachingPattern_Shopify_Quantities cacheShopify_Quantities = new CachingPattern_Shopify_Quantities("ShopifyQuantities_ReecesAPI", settings);
            cacheShopify_Quantities._02_SaveNewestToCache(API_Quantities);

            Context context = new Context();

            Document Shopify_Product_Document = new Document();
            Shopify_Product_Document.Id = new Guid("17cf9358-b783-46a2-97ee-ef69388f3904");
            Shopify_Product_Document.DocumentName = "Shopify_Product";
            Shopify_Product_Document.CustomerId = Guid.Empty;
            Shopify_Product_Document.ProjectId = Guid.Empty;
            Shopify_Product_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(API_Products, typeof(IEnumerable<Shopify_Product>));
            Document_Repository.AddJsonDocument(context, Shopify_Product_Document);

            Document Shopify_Prices_Document = new Document();
            Shopify_Prices_Document.Id = new Guid("b9ae61e7-4248-4c95-832d-acbb079a68df");
            Shopify_Prices_Document.DocumentName = "Shopify_Prices";
            Shopify_Prices_Document.CustomerId = Guid.Empty;
            Shopify_Prices_Document.ProjectId = Guid.Empty;
            Shopify_Prices_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(API_Prices, typeof(IEnumerable<Shopify_Prices>));
            Document_Repository.AddJsonDocument(context, Shopify_Prices_Document);

            Document Shopify_Quantities_Document = new Document();
            Shopify_Quantities_Document.Id = new Guid("c1f7d63e-1f1a-42cd-8c00-254f3b77354d");
            Shopify_Quantities_Document.DocumentName = "Shopify_Quantities";
            Shopify_Quantities_Document.CustomerId = Guid.Empty;
            Shopify_Quantities_Document.ProjectId = Guid.Empty;
            Shopify_Quantities_Document.DocumentValue = System.Text.Json.JsonSerializer.Serialize(API_Quantities, typeof(IEnumerable<Shopify_Quantities>));
            Document_Repository.AddJsonDocument(context, Shopify_Quantities_Document);
        }
        private static void CheckForDuplicates()
        {
            var Duplicates_Products = API_Products.GroupBy(x => x.PartNumber).Where(x => x.Count() > 1).Select(x => x.Key);
            var Duplicates_Prices = API_Prices.GroupBy(x => x.PartNumber).Where(x => x.Count() > 1).Select(x => x.Key);
            var Duplicates_Quantities = API_Quantities.GroupBy(x => x.PartNumber).Where(x => x.Count() > 1).Select(x => x.Key);

            string Products = string.Join(Environment.NewLine, Duplicates_Products);
            string Prices = string.Join(Environment.NewLine, Duplicates_Prices);
            string Quantities = string.Join(Environment.NewLine, Duplicates_Quantities);

            File.WriteAllText(settings.OutputDirectory + "\\DuplicateProducts.txt", Products);
            File.WriteAllText(settings.OutputDirectory + "\\DuplicatePrices.txt", Prices);
            File.WriteAllText(settings.OutputDirectory + "\\DuplicateQuantities.txt", Quantities);

            var Duplicates_S5InvAss = InvAss.GroupBy(x => x.Inv.Part).Where(x => x.Count() > 1).Select(x => x.Key);
            string Inv = string.Join(Environment.NewLine, Duplicates_S5InvAss);
            File.WriteAllText(settings.OutputDirectory + "\\DuplicateS5Inventory.txt", Inv);
        }
        private static void GetEqualityFunctions()
        {
            //fEquals = (from, to) => 
            //{
            //    IEnumerable<Changes_View> tmp = from.Diff(to);
            //    tmp.Select(x =>
            //    {
            //        changes.Add(x);
            //        return x;
            //    }); 
            //    return from.Equals(to); 
            //};
            logger.LogInformation("GetEqualityFunctions()");
            fEquals_Prices = (from, to) => { return from.Equals(to); };
            fEquals_Quantities = (from, to) => { return from.Equals(to); };
            fEquals_Product = (from, to) => { return from.Equals(to); };
        }
        private static void GetChangedRecords()
        {
            logger.LogInformation("GetChangedRecords()");
            ChangedRecords_Product = UpdateProcessPattern<IShopify_Product, Shopify_Product, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Product, adapters, API_Products);

            ChangedRecords_Prices = UpdateProcessPattern<IShopify_Prices, Shopify_Prices, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Prices, adapters, API_Prices);

            ChangedRecords_Quantities = UpdateProcessPattern<IShopify_Quantities, Shopify_Quantities, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Quantities, adapters, API_Quantities);

        }
        private static void GetDetailedDifferences_And_Create_Reports()
        {
            logger.LogInformation("GetDetailedDifferences_And_Create_Reports()");
            var ChangedRecords_Product_Pairs = UpdateProcessPattern<IShopify_Product, Shopify_Product, int>
                .GetChangedRecordPairs(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Product, adapters, API_Products);

            var ChangedRecords_Prices_Pairs = UpdateProcessPattern<IShopify_Prices, Shopify_Prices, int>
                .GetChangedRecordPairs(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Prices, adapters, API_Prices);

            var ChangedRecords_Quantities_Pairs = UpdateProcessPattern<IShopify_Quantities, Shopify_Quantities, int>
                .GetChangedRecordPairs(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Quantities, adapters, API_Quantities);

            IEnumerable<IEnumerable<Changes_View>> tmpProd = ChangedRecords_Product_Pairs.Select(x => x.Item1.Diff(x.Item2));
            IEnumerable<Changes_View> tmpDetailProd = tmpProd.SelectMany(x => x);
            foreach (var x in tmpDetailProd) { changes.Add(x); }

            IEnumerable<IEnumerable<Changes_View>> tmpPrices = ChangedRecords_Prices_Pairs.Select(x => x.Item1.Diff(x.Item2));
            IEnumerable<Changes_View> tmpDetailPrices = tmpPrices.SelectMany(x => x);
            foreach (var x in tmpDetailPrices) { changes.Add(x); }

            IEnumerable<IEnumerable<Changes_View>> tmpQuantities = ChangedRecords_Quantities_Pairs.Select(x => x.Item1.Diff(x.Item2));
            IEnumerable<Changes_View> tmpDetailQuantities = tmpQuantities.SelectMany(x => x);
            foreach (var x in tmpDetailQuantities) { changes.Add(x); }

            Reports.SaveReport(ChangedRecords_Product_Pairs, "Changed_Product", settings, logger);
            Reports.SaveReport(ChangedRecords_Prices_Pairs, "Changed_Prices", settings, logger);
            Reports.SaveReport(ChangedRecords_Quantities_Pairs, "Changed_Quantities", settings, logger);
        }
        private static VennMap_InvAss<Shopify_Product, int> ProduceVennMap(VennMap_InvAss<Shopify_Product, int> map)
        {
            logger.LogInformation("ProduceVennMap(VennMap_InvAss<Shopify_Product, int> map)");
            Func<IS5InvAssembled, IS5InvAssembled> As_S5InvAssembled = (x) =>
            {
                return new S5InvAssembled
                (
                    x.Inv
                    , x.InvPrices_PerInventry_27 ?? new List<IInvPrice>()
                    , x.StokLines_PerInventry_27 ?? new List<IStok>()
                    , x.CommentsLines_PerInventry_27 ?? new List<IComments>()
                    , x.AltSuplies_PerInventry_27 ?? new List<IAltSuply>()
                );
            };

            map = new VennMap_InvAss<Shopify_Product, int>
            (
                API_Products
                , InvAss.Select(x => As_S5InvAssembled(x))
                , IShopifyDataLoadFormat_Indexes.UniqueId
                , IS5InvAssembled_Indexes.InvUnique
            );
            return map;
        }
        private static VennMap_InvAss<Shopify_Prices, int> ProduceVennMap(VennMap_InvAss<Shopify_Prices, int> map)
        {
            logger.LogInformation("ProduceVennMap(VennMap_InvAss<Shopify_Prices, int> map)");
            Func<IS5InvAssembled, IS5InvAssembled> As_S5InvAssembled = (x) =>
            {
                return new S5InvAssembled
                (
                    x.Inv
                    , x.InvPrices_PerInventry_27 ?? new List<IInvPrice>()
                    , x.StokLines_PerInventry_27 ?? new List<IStok>()
                    , x.CommentsLines_PerInventry_27 ?? new List<IComments>()
                    , x.AltSuplies_PerInventry_27 ?? new List<IAltSuply>()
                );
            };

            map = new VennMap_InvAss<Shopify_Prices, int>
            (
                API_Prices
                , InvAss.Select(x => As_S5InvAssembled(x))
                , IShopifyDataLoadFormat_Indexes.UniqueId
                , IS5InvAssembled_Indexes.InvUnique
            );
            return map;
        }
        private static VennMap_InvAss<Shopify_Quantities, int> ProduceVennMap(VennMap_InvAss<Shopify_Quantities, int> map)
        {
            logger.LogInformation("ProduceVennMap(VennMap_InvAss<Shopify_Quantities, int> map)");
            Func<IS5InvAssembled, IS5InvAssembled> As_S5InvAssembled = (x) =>
            {
                return new S5InvAssembled
                (
                    x.Inv
                    , x.InvPrices_PerInventry_27 ?? new List<IInvPrice>()
                    , x.StokLines_PerInventry_27 ?? new List<IStok>()
                    , x.CommentsLines_PerInventry_27 ?? new List<IComments>()
                    , x.AltSuplies_PerInventry_27 ?? new List<IAltSuply>()
                );
            };

            map = new VennMap_InvAss<Shopify_Quantities, int>
            (
                API_Quantities
                , InvAss.Select(x => As_S5InvAssembled(x))
                , IShopifyDataLoadFormat_Indexes.UniqueId
                , IS5InvAssembled_Indexes.InvUnique
            );
            return map;
        }
        private static void ProduceReportsBeforeProcessing()
        {
            logger.LogInformation("ProduceReportsBeforeProcessing()");
            Reports.SaveReport(changes, "Differences_Detail", settings, logger);
            ProduceReportsBeforeProcessing_Product();
            ProduceReportsBeforeProcessing_Prices();
            ProduceReportsBeforeProcessing_Quantities();
        }
        private static void ProduceReportsBeforeProcessing_Product()
        {
            logger.LogInformation("ProduceReportsBeforeProcessing_Product()");
            // Venn Reports are relevant for the Sync process only because it compares System Five and the Web Site we're syncing to.
            bool UsingSyncProcess = (map_Product != null);
            if (UsingSyncProcess)
            {
                //Reports.SaveReport(map, settings, logger);
                Func<(Shopify_Product, IS5InvAssembled), Shopify_Product_Pair_Flat> Transform =
                    (m =>
                    {
                        AdaptToShopifyDataLoadFormat tmpAdapter = new AdaptToShopifyDataLoadFormat();
                        tmpAdapter.Init(m.Item2);
                        Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(m.Item1, tmpAdapter.AsShopify_Product());
                        Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
                        return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
                    });

                IEnumerable<(Shopify_Product, IS5InvAssembled)> Map_Both_Ecomm = map_Product.Both_Ecomm;
                IEnumerable<Shopify_Product_Pair_Flat> Both_Ecomm = Map_Both_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(Both_Ecomm, "Venn_Product_Both_Ecomm", settings, logger);

                IEnumerable<(Shopify_Product, IS5InvAssembled)> Map_Both_NoEcomm = map_Product.Both_NoEcomm;
                IEnumerable<Shopify_Product_Pair_Flat> Both_NoEcomm = Map_Both_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(Both_NoEcomm, "Venn_Product_Both_NoEcomm", settings, logger);

                IEnumerable<(Shopify_Product, IS5InvAssembled)> Map_InvOnly_Ecomm = map_Product.InvOnly_Ecomm;
                IEnumerable<Shopify_Product_Pair_Flat> InvOnly_Ecomm = Map_InvOnly_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_Ecomm, "Venn_Product_InvOnly_Ecomm", settings, logger);

                IEnumerable<(Shopify_Product, IS5InvAssembled)> Map_InvOnly_NoEcomm = map_Product.InvOnly_NoEcomm;
                IEnumerable<Shopify_Product_Pair_Flat> InvOnly_NoEcomm = Map_InvOnly_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_NoEcomm, "Venn_Product_InvOnly_NoEcomm", settings, logger);

            }
        }
        private static void ProduceReportsBeforeProcessing_Prices()
        {
            logger.LogInformation("ProduceReportsBeforeProcessing_Prices()");
            // Venn Reports are relevant for the Sync process only because it compares System Five and the Web Site we're syncing to.
            bool UsingSyncProcess = (map_Prices != null);
            if (UsingSyncProcess)
            {
                //Reports.SaveReport(map, settings, logger);
                Func<(Shopify_Prices, IS5InvAssembled), Shopify_Prices_Pair_Flat> Transform =
                    (m =>
                    {
                        AdaptToShopifyDataLoadFormat tmpAdapter = new AdaptToShopifyDataLoadFormat();
                        tmpAdapter.Init(m.Item2);
                        Generic_Pair<Shopify_Prices> tmpSP = new Generic_Pair<Shopify_Prices>(m.Item1, tmpAdapter.AsShopify_Prices());
                        Shopify_Prices_Pair_Adapter tmpFlatAdapter = new Shopify_Prices_Pair_Adapter(tmpSP);
                        return tmpFlatAdapter.AsShopify_Prices_Pair_Flat();
                    });

                string DataName = "Prices";
                IEnumerable<(Shopify_Prices, IS5InvAssembled)> Map_Both_Ecomm = map_Prices.Both_Ecomm;
                IEnumerable<Shopify_Prices_Pair_Flat> Both_Ecomm = Map_Both_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(Both_Ecomm, $"Venn_{DataName}_Both_Ecomm", settings, logger);

                IEnumerable<(Shopify_Prices, IS5InvAssembled)> Map_Both_NoEcomm = map_Prices.Both_NoEcomm;
                IEnumerable<Shopify_Prices_Pair_Flat> Both_NoEcomm = Map_Both_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(Both_NoEcomm, $"Venn_{DataName}_Both_NoEcomm", settings, logger);

                IEnumerable<(Shopify_Prices, IS5InvAssembled)> Map_InvOnly_Ecomm = map_Prices.InvOnly_Ecomm;
                IEnumerable<Shopify_Prices_Pair_Flat> InvOnly_Ecomm = Map_InvOnly_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_Ecomm, $"Venn_{DataName}_InvOnly_Ecomm", settings, logger);

                IEnumerable<(Shopify_Prices, IS5InvAssembled)> Map_InvOnly_NoEcomm = map_Prices.InvOnly_NoEcomm;
                IEnumerable<Shopify_Prices_Pair_Flat> InvOnly_NoEcomm = Map_InvOnly_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_NoEcomm, $"Venn_{DataName}_InvOnly_NoEcomm", settings, logger);
            }
        }
        private static void ProduceReportsBeforeProcessing_Quantities()
        {
            logger.LogInformation("ProduceReportsBeforeProcessing_Quantities()");
            // Venn Reports are relevant for the Sync process only because it compares System Five and the Web Site we're syncing to.
            bool UsingSyncProcess = (map_Quantities != null);
            if (UsingSyncProcess)
            {
                //Reports.SaveReport(map, settings, logger);
                Func<(Shopify_Quantities, IS5InvAssembled), Shopify_Quantities_Pair_Flat> Transform =
                    (m =>
                    {
                        AdaptToShopifyDataLoadFormat tmpAdapter = new AdaptToShopifyDataLoadFormat();
                        tmpAdapter.Init(m.Item2);
                        Generic_Pair<Shopify_Quantities> tmpSP = new Generic_Pair<Shopify_Quantities>(m.Item1, tmpAdapter.AsShopify_Quantities());
                        Shopify_Quantities_Pair_Adapter tmpFlatAdapter = new Shopify_Quantities_Pair_Adapter(tmpSP);
                        return tmpFlatAdapter.AsShopify_Quantities_Pair_Flat();
                    });

                string DataName = "Quantities";
                IEnumerable<(Shopify_Quantities, IS5InvAssembled)> Map_Both_Ecomm = map_Quantities.Both_Ecomm;
                IEnumerable<Shopify_Quantities_Pair_Flat> Both_Ecomm = Map_Both_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(Both_Ecomm, $"Venn_{DataName}_Both_Ecomm", settings, logger);

                IEnumerable<(Shopify_Quantities, IS5InvAssembled)> Map_Both_NoEcomm = map_Quantities.Both_NoEcomm;
                IEnumerable<Shopify_Quantities_Pair_Flat> Both_NoEcomm = Map_Both_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(Both_NoEcomm, $"Venn_{DataName}_Both_NoEcomm", settings, logger);

                IEnumerable<(Shopify_Quantities, IS5InvAssembled)> Map_InvOnly_Ecomm = map_Quantities.InvOnly_Ecomm;
                IEnumerable<Shopify_Quantities_Pair_Flat> InvOnly_Ecomm = Map_InvOnly_Ecomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_Ecomm, $"Venn_{DataName}_InvOnly_Ecomm", settings, logger);

                IEnumerable<(Shopify_Quantities, IS5InvAssembled)> Map_InvOnly_NoEcomm = map_Quantities.InvOnly_NoEcomm;
                IEnumerable<Shopify_Quantities_Pair_Flat> InvOnly_NoEcomm = Map_InvOnly_NoEcomm.Select(m => Transform(m));
                Reports.SaveReport(InvOnly_NoEcomm, $"Venn_{DataName}_InvOnly_NoEcomm", settings, logger);
            }
        }
        private static void PerformDeletes()
        {
            logger.LogInformation("PerformDeletes()");
            IEnumerable<Shopify_Product> ToDelete = map_Product.TOnly.Select(x => x.Item1);
            foreach(var record in ToDelete)
            {
                ReeceShopify.Products_Delete(ToDelete);
            }
        }
        private static void PauseToGiveSomeTimeForNewProductsToLoad(int numNewProducts)
        {
            logger.LogInformation("PauseToGiveSomeTimeForNewProductsToLoad(int numNewProducts)");
            int ticsPerMinute = 60 * 1000;
            int ticsToWait = ticsPerMinute * 5 + numNewProducts * ticsPerMinute / 4;
            int tics = 0;
            int count = 0;
            int minutesToWait = ticsToWait / ticsPerMinute;
            logger.LogInformation($"Begining {minutesToWait} minute wait cycle to give Shopify time to finish the work.");
            while (tics < ticsToWait)
            {
                System.Threading.Thread.Sleep(ticsPerMinute);
                count++;
                tics = ticsPerMinute * count;
                logger.LogInformation((tics / ticsPerMinute).ToString() + " minutes passed, " 
                    + ((ticsToWait - tics) / ticsPerMinute).ToString() + " minutes to go.");
            }
        }

        private static string SerializeForExport(IEnumerable<IS5InvAssembled> ListToSerialize)
        {
            // serialize the results to prep them for sending
            string result = string.Empty;
            try
            {
                result = JsonSerializer.Serialize(ListToSerialize, typeof(IEnumerable<IS5InvAssembled>));
            }
            catch (Exception e)
            {
                List<IS5InvAssembled> FromAdapters = new List<IS5InvAssembled>();
                List<IS5InvAssembled> AdapterRecordsNotSerializable = new List<IS5InvAssembled>();

                // only copy serializable records and try again
                string tmp = string.Empty;
                int i = 0;

                foreach (var adapter in ListToSerialize)
                {
                    try
                    {
                        tmp = JsonSerializer.Serialize(adapter, typeof(S5InvAssembled));
                        FromAdapters.Add(adapter);
                    }
                    catch (Exception Ex)
                    {
                        // Count bad records
                        i++;
                        IS5InvAssembled dataLoadFormat = new S5InvAssembled();
                        try
                        {
                            AdapterRecordsNotSerializable.Add(adapter);
                        }
                        catch
                        {
                            throw new Exception("Failed to .CopyFrom the adapter record into a data load format object for a single record "
                                + "that also failed to serialize. See Inner Exception.", e);
                        }
                    }
                }

                try
                {
                    result = JsonSerializer.Serialize(FromAdapters, typeof(IEnumerable<AdaptToShopifyDataLoadFormat>));
                }
                catch
                {
                    throw new Exception($"Failed to serialize sanitized records from the Shopify Data Load File. See Inner Exception.", e);
                }
            }
            return result;
        }

        private static string SerializeForExport(IEnumerable<IShopifyDataLoadFormat> ChangedRecords)
        {
            // serialize the results to prep them for sending
            string result = string.Empty;
            try
            {
                IEnumerable<ShopifyDataLoadFormat> _ChangedRecords = ChangedRecords.Select(rec => (ShopifyDataLoadFormat)(new ShopifyDataLoadFormat().CopyFrom(rec)));
                result = JsonSerializer.Serialize(_ChangedRecords, typeof(IEnumerable<ShopifyDataLoadFormat>));
            }
            catch (Exception e)
            {
                List<IShopifyDataLoadFormat> FromAdapters = new List<IShopifyDataLoadFormat>();
                List<IShopifyDataLoadFormat> AdapterRecordsNotSerializable = new List<IShopifyDataLoadFormat>();

                // only copy serializable records and try again
                string tmp = string.Empty;
                int i = 0;

                foreach (var ChangedRecord in ChangedRecords)
                {
                    try
                    {
                        tmp = JsonSerializer.Serialize(ChangedRecord, typeof(IShopifyDataLoadFormat));
                        FromAdapters.Add(ChangedRecord);
                    }
                    catch (Exception Ex)
                    {
                        // Count bad records
                        i++;
                        IShopifyDataLoadFormat dataLoadFormat = new ShopifyDataLoadFormat();
                        try
                        {
                            dataLoadFormat.CopyFrom(ChangedRecord);
                            AdapterRecordsNotSerializable.Add(dataLoadFormat);
                        }
                        catch
                        {
                            throw new Exception("Failed to .CopyFrom the adapter record into a data load format object for a single record "
                                + "that also failed to serialize. See Inner Exception.", e);
                        }
                    }
                }

                try
                {
                    result = JsonSerializer.Serialize(FromAdapters, typeof(IEnumerable<IShopifyDataLoadFormat>));
                }
                catch
                {
                    throw new Exception($"Failed to serialize sanitized records from the Shopify Data Load File. See Inner Exception.", e);
                }
            }
            return result;
        }

        private static string SerializeForExport(IEnumerable<AdaptToShopifyDataLoadFormat> adapters)
        {
            // serialize the results to prep them for sending
            string result = string.Empty;
            try
            {
                result = JsonSerializer.Serialize(adapters, typeof(IEnumerable<AdaptToShopifyDataLoadFormat>));
            }
            catch (Exception e)
            {
                List<IShopifyDataLoadFormat> FromAdapters = new List<IShopifyDataLoadFormat>();
                List<IShopifyDataLoadFormat> AdapterRecordsNotSerializable = new List<IShopifyDataLoadFormat>();

                // only copy serializable records and try again
                string tmp = string.Empty;
                int i = 0;

                foreach (var adapter in adapters)
                {
                    try
                    {
                        tmp = JsonSerializer.Serialize(adapter, typeof(AdaptToShopifyDataLoadFormat));
                        FromAdapters.Add(adapter);
                    }
                    catch (Exception Ex)
                    {
                        // Count bad records
                        i++;
                        IShopifyDataLoadFormat dataLoadFormat = new ShopifyDataLoadFormat();
                        try
                        {
                            dataLoadFormat.CopyFrom(adapter);
                            AdapterRecordsNotSerializable.Add(dataLoadFormat);
                        }
                        catch
                        {
                            throw new Exception("Failed to .CopyFrom the adapter record into a data load format object for a single record "
                                + "that also failed to serialize. See Inner Exception.", e);
                        }
                    }
                }

                try
                {
                    result = JsonSerializer.Serialize(FromAdapters, typeof(IEnumerable<AdaptToShopifyDataLoadFormat>));
                }
                catch
                {
                    throw new Exception($"Failed to serialize sanitized records from the Shopify Data Load File. See Inner Exception.", e);
                }
            }
            return result;
        }
        private static List<ShopifyDataLoadFormat> FromShopify_LocalTestData()
        {
            string FilePathAndName = "C:\\Temp\\results-Shopify.txt";
            List<ShopifyDataLoadFormat> ShopifyRecords = new List<ShopifyDataLoadFormat>();
            if (!File.Exists(FilePathAndName))
            {
                return ShopifyRecords;
            }

            string contents = File.ReadAllText(FilePathAndName);
            List<ShopifyDataLoadFormat> data = (List<ShopifyDataLoadFormat>)JsonSerializer.Deserialize(contents, typeof(List<ShopifyDataLoadFormat>));
            return data;
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
