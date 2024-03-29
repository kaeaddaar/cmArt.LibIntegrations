﻿using System;
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
//using cmArt.Shopify.App.Data;
using System.Text.Json;
using cmArt.LibIntegrations.SerializationService;
//using cmArt.Shopify.Connector.Data;
//using cmArt.Shopify.Connector;
//using cmArt.Reece.ShopifyConnector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
//using cmArt.Shopify.App.Services;
using cmArt.WebJaguar.Data;
using cmArt.WebJaguar.App.Services;
using cmArt.WebJaguar.Connector;
using cmArt.WebJaguar.App.ReportViews;
using cmArt.LibIntegrations.VennMapService;

namespace cmArt.WebJaguar.App
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
        private static StaticSettings settings;
        //GetSystem5Data()
        private static IEnumerable<IS5InvAssembled> InvAss;
        //FilterForECommAndSave()
        private static IEnumerable<IS5InvAssembled> ECommInvAss;
        //CreateDataLoadLists()
        private static IEnumerable<adapterS5_from_InvAss> adaptersS5;
        private static IEnumerable<adapterWJ_from_S5> adaptersWJ;
        private static IEnumerable<S5_CommonFields> S5CommonFieldsAdapted;
        private static IEnumerable<IS5_CommonFields_In_WJ> S5CommonFields;
        //GetPrevDataLoadListsAndOverwrite()
        private static IEnumerable<S5_CommonFields> PrevDataLoad_Product;
        private static CachingPattern cacheShopify_Product;
        //Get_ChangedQuantities()
        private static IEnumerable<IS5_CommonFields_In_WJ> ChangedRecords_Product_QtyOnly;
        //ReportOn_ChangedQuantities()
        //PerformEditsOn_ChangedQuantities()
        //GetShopifyData()
        private static Func<IS5_CommonFields_In_WJ, IS5_CommonFields_In_WJ, bool> fEquals = (from, to) => { return from.cmEquals(to); };
        //GetShopifyData_Reece_Products()
        private static IEnumerable<S5_CommonFields> API_Products;
        //GetShopifyData_Reece_Prices()
        private static Func<IProduct_Root, string> fGetProductPartNumber;
        //GetChangedRecords()
        private static IEnumerable<IS5_CommonFields_In_WJ> ChangedRecords_Product;
        //BuildReports()
        private static VennMap<S5_CommonFields, adapterS5_from_InvAss, int> map;
        //ProduceVennMap()

        //PerformEdits()
        private static IEnumerable<S5_CommonFields> changedProducts;
        private static IEnumerable<Product_Root> WJ_WithChanges;
        //GetNewRecords()
        private static IEnumerable<Tuple<IS5_CommonFields_In_WJ, IS5_CommonFields_In_WJ>> NewProductsPairs;
        private static IEnumerable<S5_CommonFields> NewProducts;

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

            PreventApiAddsNEdits = _args.Contains("PREVENTADDSANDEDITS") || _args.Contains("PREVENTADDSNEDITS") 
                || settings.PreventAddsAndEdits == "PREVENTADDSANDEDITS";
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
            RunAsSelfCompare = (settings.RunAs == "SELFCOMPARE");
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
        private static void FilterForECommAndSave()
        {
            logger.LogInformation("Filtering for Ecommerce equals Y");
            ECommInvAss = InvAss.Where(prod => prod.Inv.Ecommerce == "Y" && prod.Inv.Part.TrimEnd() != "DEFAULT");
        }
        private static void CreateDataLoadLists()
        {
            // Use facade to create data load format from Assembled Inventory Data
            logger.LogInformation("Begin converting Assembled Inventory Records to the External Data Load Format via an adapter.");
            adaptersS5 = ECommInvAss.Select(Inv =>
            {
                adapterS5_from_InvAss tmpS5 = new adapterS5_from_InvAss();
                tmpS5.init(Inv);

                return tmpS5;
            }
            );

            logger.LogInformation(" -- Get products from adapter");
            S5CommonFieldsAdapted = adaptersS5.Select(x => x.AsS5_CommonFields());
            S5CommonFields = S5CommonFieldsAdapted;
        }
        private static void GetPrevDataLoadLists()
        {
            cacheShopify_Product = new CachingPattern("PocoProductsAdapted", settings);
            PrevDataLoad_Product = cacheShopify_Product._01_GetPrev();
            //cacheShopify_Product._02_SaveNewestToCache(S5CommonFieldsAdapted);

            API_Products = PrevDataLoad_Product;
        }
        private static void Get_ChangedQuantities()
        {
            Func<IS5_CommonFields_In_WJ, IS5_CommonFields_In_WJ, bool> fQtyEquals = (from, to) => { return from.cmQtyEquals(to); };

            ChangedRecords_Product_QtyOnly = UpdateProcessPattern<IS5_CommonFields_In_WJ, S5_CommonFields, int>
                .GetChangedRecords(IS5_CommonFields_In_WJ_Indexes.UniqueId, fQtyEquals, adaptersS5, API_Products);
        }
        private static void ReportOn_ChangedQuantities()
        {
            // skipping
        }
        private static void PerformEditsOn_ChangedQuantities_AndOverwriteCache()
        {
            WebJaguarConnector api = new WebJaguarConnector();
            IEnumerable<S5_CommonFields> cf = ChangedRecords_Product_QtyOnly.Select(x => x.AsS5_CommonFields());
            api.Quantities_Edit(cf);

            cacheShopify_Product._02_SaveNewestToCache(S5CommonFieldsAdapted);
        }
        private static void CleanUpMemory_ChangedQuantities()
        {
            ChangedRecords_Product_QtyOnly = null;
            GC.Collect(); // if this really slows things down then we can remove it. Might save us from overdoing the memory allocation though
        }

        private static void GetWebJaguarData_Product_Root()
        {
            logger.LogInformation("Get Products using the WebJaguar API");
            API_Products = new List<S5_CommonFields>();
            try
            {
                WebJaguarConnector api = new WebJaguarConnector();
                List<Product_Root> API_Products_WJ = new List<Product_Root>();
                List<Product_Root> API_Products_WJ_Less_Than_10 = api.GetAll_Product_Root_Records((decimal)0.00, (decimal)9.99);
                List<Product_Root> API_Products_WJ_10_to_100000 = api.GetAll_Product_Root_Records((decimal)10.00, (decimal)100000);
                API_Products_WJ_Less_Than_10.ForEach(x => API_Products_WJ.Add(x));
                API_Products_WJ_10_to_100000.ForEach(x => API_Products_WJ.Add(x));
                ExportWebJaguarData(API_Products_WJ);

                IEnumerable<WJ_CommonFields> wJ_CommonFields = API_Products_WJ.Select(prod => prod.AsWJ_CommonFields());
                List<WJ_CommonFields> test = API_Products_WJ.Select(prod => prod.AsWJ_CommonFields()).ToList();
                API_Products = wJ_CommonFields.Select(cf =>
                {
                    adapterS5_from_WJ tmp = new adapterS5_from_WJ();
                    tmp.init(cf);
                    return tmp.AsS5_CommonFields();
                });
                logger.LogInformation($"Saving WebJaguar Products for API_Products");
                try
                {
                    GenericSerialization<S5_CommonFields>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, "API_Products");
                    GenericSerialization<S5_CommonFields>.SerializeToJSON(API_Products.ToList(), "API_Products", settings.OutputDirectory, 50000);
                }
                catch
                {
                    logger.LogInformation($"Failed to serialize API_Products. API_Products file(s) will not be written.");
                }
            }
            catch (Exception e)
            {
                logger.LogInformation($"Failed to get All external products from Custom API. Message = \"{e.Message}\"");
                Console.ReadKey();
            }
        }
        private static void ExportWebJaguarData(IEnumerable<Product_Root> API_Products_WJ)
        {
            IEnumerable<WJ_Data_Export> report = API_Products_WJ.Select(x => x.AsWJ_Data_Export());
            ReportsWJ.SaveReport(report, "WJ_Data_Export", settings.OutputDirectory, logger);
        }
        private static void GetEqualityFunctions()
        {
            fEquals = (from, to) => { return from.Equals(to); };
        }
        private static void GetChangedRecords()
        {
            ChangedRecords_Product = UpdateProcessPattern<IS5_CommonFields_In_WJ, S5_CommonFields, int>
                .GetChangedRecords(IS5_CommonFields_In_WJ_Indexes.UniqueId, fEquals, adaptersS5, API_Products);
        }
        private static void GetNewRecords()
        {
            NewProductsPairs = new List<Tuple<IS5_CommonFields_In_WJ, IS5_CommonFields_In_WJ>>();

            NewProductsPairs = GenericJoins<IS5_CommonFields_In_WJ, IS5_CommonFields_In_WJ, int>
            .LeftJoin(adaptersS5, API_Products, IS5_CommonFields_In_WJ_Indexes.UniqueId, IS5_CommonFields_In_WJ_Indexes.UniqueId);
            NewProducts = NewProductsPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsS5_CommonFields());
            // convert CommonFields to Product_Root filling using default content

        }
        private static void SeeIfNewRecordsExistByS5InvUnique_and_append_to_API_Products()//works around bug in WJ productSearch missing items
        {
            IEnumerable<int> NewProductIDs = NewProducts.Select(x => x.InvUnique);
            WebJaguarConnector apiWJ = new WebJaguarConnector();
            List<string> WJProductsFound = new List<string>();
            int count = 0;
            foreach (int id in NewProductIDs)
            {
                count++;
                string strProd = apiWJ.Product_Get(id.ToString());
                if (!strProd.Contains("No product found for given Id"))
                {
                    WJProductsFound.Add(strProd);
                }
            }
            IEnumerable<Product_Response> WJProdResponseFound = WJProductsFound
                .Where(x => x.Substring(0,8) != "Quitting")
                .Select(x => (Product_Response)System.Text.Json.JsonSerializer.Deserialize(x, typeof(Product_Response)));
            IEnumerable<Product_Root> WJProdsFound = WJProdResponseFound.Select(x => x.product ?? new Product_Root());
            IEnumerable<WJ_Data_Export> WJProdsFound_Export = WJProdsFound.Select(x => x.AsWJ_Data_Export());
            ReportsWJ.SaveReport(WJProdsFound_Export, "WJ_Data_Export_MissingProductsFound", settings.OutputDirectory, logger);

            IEnumerable<WJ_CommonFields> wJ_CommonFields = WJProdsFound.Select(prod => prod.AsWJ_CommonFields());
            IEnumerable<S5_CommonFields> API_ProductsMissing = wJ_CommonFields.Select(cf =>
            {
                adapterS5_from_WJ tmp = new adapterS5_from_WJ();
                tmp.init(cf);
                return tmp.AsS5_CommonFields();
            });
            List<S5_CommonFields> lstAPI_Products = API_Products.ToList();
            foreach (var prod in API_ProductsMissing)
            {
                lstAPI_Products.Add(prod);
            }
            API_Products = lstAPI_Products;
        }
        private static void BuildReports()
        {
            var ChangedRecords_Product_Pairs = UpdateProcessPattern<IS5_CommonFields_In_WJ, S5_CommonFields, int>
                .GetChangedRecordPairs(IS5_CommonFields_In_WJ_Indexes.UniqueId, fEquals, adaptersS5, API_Products);
            var changes = ChangedRecords_Product_Pairs.Select(x => x.Item1.Diff(x.Item2)).SelectMany(x => x);
            ReportsWJ.SaveReport(changes, "ChangesView", settings.OutputDirectory, logger);

            Func<(S5_CommonFields, adapterS5_from_InvAss), S5_CommonFields_Pairs_Flat> Transform =
                 (m =>
                 {
                     Generic_Pair<S5_CommonFields> tmpSP = new Generic_Pair<S5_CommonFields>(m.Item1, m.Item2.AsS5_CommonFields());
                     Inventory_Pair_Adapter tmpFlatAdapter = new Inventory_Pair_Adapter(tmpSP);
                     return tmpFlatAdapter.AsS5_CommonFields_Pairs_Flat();
                 });

            ProduceVennMap();

            IEnumerable<S5_CommonFields_Pairs_Flat> Both_Ecomm_Pairs = map.Both_Ecomm.Select(x => Transform(x));
            ReportsWJ.SaveReport(Both_Ecomm_Pairs, "Both_Ecomm", settings.OutputDirectory, logger);

            IEnumerable<S5_CommonFields_Pairs_Flat> InvOnly_Ecomm_Pairs = map.InvOnly_Ecomm.Select(x => Transform(x));
            ReportsWJ.SaveReport(InvOnly_Ecomm_Pairs, "InvOnly_Ecomm", settings.OutputDirectory, logger);

            IEnumerable<S5_CommonFields_Pairs_Flat> TOnly = map.TOnly.Select(x => Transform(x));
            ReportsWJ.SaveReport(TOnly, "WebJaguar_Only", settings.OutputDirectory, logger);

        }
        private static void ProduceVennMap()
        {
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

            Func<adapterS5_from_InvAss, bool> EcommEqualsY = (x) => x.IsEcomm;
            map = new VennMap<S5_CommonFields, adapterS5_from_InvAss, int>
            (
                API_Products
                , adaptersS5
                , IS5_CommonFields_In_WJ_Indexes.UniqueId
                , IS5_CommonFields_In_WJ_Indexes.UniqueId
                , EcommEqualsY
            );
        }

        private static void PerformEdits()
        {
            logger.LogInformation("Begin Performing Edits");
            changedProducts = new List<S5_CommonFields>();

            WebJaguarConnector apiWJ = new WebJaguarConnector();
            if (!PreventApiAddsNEdits)
            {
                changedProducts = ChangedRecords_Product.Select(p => p.AsS5_CommonFields());
                string Product_Edit_Results = string.Empty;
                if (!PreventProduct)
                {
                    logger.LogInformation("Performing Edits on Changed Products");
                    logger.LogInformation($"Number of Changed Products: {changedProducts.Count()}");
                    Product_Edit_Results = apiWJ.Products_Edit(changedProducts); // uses changes converted back to Product_Root (if it works)
                    string FileNameChangedProducts = settings.OutputDirectory + "\\changedProducts.json.txt";
                    string content = System.Text.Json.JsonSerializer.Serialize(changedProducts.ToList(), typeof(List<S5_CommonFields>));
                    System.IO.File.WriteAllText(FileNameChangedProducts, content);
                }
                else { logger.LogInformation("Preventing edits on changed products"); }
            }
            else
            {
                logger.LogInformation("All Perform Edits Supressed due to Settings");
            }
        }
        private static void PerformAdds()
        {
            logger.LogInformation("Begin Perform Adds");
            if (!PreventApiAddsNEdits)
            {
                if (!PreventProduct)
                {
                    logger.LogInformation("Performing Products_Add on NewProducts");
                    logger.LogInformation($"Number of records in NewProducts: {NewProducts.Count()}");
                    WebJaguarConnector apiWJ = new WebJaguarConnector();
                    IEnumerable<Product_Root> NewProduct_Root = NewProducts.Select(p => p.AsProduct_Root());
                    string Product_Add_Results = apiWJ.Products_Add(NewProduct_Root);
                }
                else { logger.LogInformation("Prevented adding of NewProducts"); }
            }
            try
            {
                string FileNameNewProducts = settings.OutputDirectory + "\\NewProducts.json.txt";
                logger.LogInformation($"Saving NewProducts to file: {FileNameNewProducts}");
                string content = System.Text.Json.JsonSerializer.Serialize(NewProducts.ToList(), typeof(List<S5_CommonFields>));
                System.IO.File.WriteAllText(FileNameNewProducts, content);
            }
            catch (Exception e)
            {
                logger.LogInformation("Error serializing and saving new products to file. Message: " + e.Message);
            }
        }
        public static void Main_Console(string[] args)
        {
            SetupLogging();
            logger.LogInformation("Begin");
            SetupAndDisplaySettings();
            SetupArgs(args);
            logger.LogInformation("Loading Inventory From System Five");
            GetSystem5Data();

            FilterForECommAndSave();
            CreateDataLoadLists();
            GetEqualityFunctions();
            if (RunAsSelfCompare)
            {
                GetPrevDataLoadLists();

                Get_ChangedQuantities();
                ReportOn_ChangedQuantities();
                PerformEditsOn_ChangedQuantities_AndOverwriteCache();
                CleanUpMemory_ChangedQuantities();
            }
            else
            {
                GetWebJaguarData_Product_Root();
            }
            GetChangedRecords();
            GetNewRecords();
            //SeeIfNewRecordsExistByS5InvUnique_and_append_to_API_Products();
            BuildReports();
            PerformEdits();
            PerformAdds();

            Console.WriteLine("Done");
            Console.ReadKey();
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
                    result = JsonSerializer.Serialize(FromAdapters, typeof(IEnumerable<adapterS5_from_InvAss>));
                }
                catch
                {
                    throw new Exception($"Failed to serialize sanitized records from the Shopify Data Load File. See Inner Exception.", e);
                }
            }
            return result;
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

