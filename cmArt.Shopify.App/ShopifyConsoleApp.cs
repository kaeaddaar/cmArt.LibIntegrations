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
        public static void Main_Console(string[] args)
        {
            Console.WriteLine("Begin");


            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            StaticSettings settings = new StaticSettings(config);

            #region Get BevNetRecords (OLD)
            //if (false)
            //{
            //    IEnumerable<IPriceFile> BevNetRecords = new List<PriceFile>(); // Existing records
            //    string PathAndFile = config["Shopifyinfo:SourceDirectory"] + config["Shopifyinfo:FileName"];
            //    const bool DontSuppressRecordErrors = false;

            //    BevNetRecords = FileReaderGeneric<PriceFile>.ReadFile
            //    (PathAndFile: PathAndFile
            //        , SupressRecordErrors: DontSuppressRecordErrors
            //    );
            //}
            #endregion Get BevNetRecords (OLD)
            
            #region Get WebData
            // Get Current Web Records
            // Connect to MySQL database, and grab a copy of all inventory and customer record data. 
            // Get records with differences for each
            // Perform updates

            

            #endregion Get WebData
            
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

            Console.WriteLine("Loading Inventory From Real Windward");

            bool PreventApiAddsNEdits = false;
            bool PreventProduct = false;
            bool PreventPrices = false;
            bool PreventQuantities = false;

            IEnumerable<IS5InvAssembled> InvAss = new List<IS5InvAssembled>();
            try
            {
                if (settings.Cachinginfo == "UseCaching" && DoWeHaveCachedFiles(config))
                {
                    InvAss = GetDataFromJson(config);
                }
                if(settings.Cachinginfo != "UseCaching" || InvAss.Count() == 0)
                {
                    InvAss = GetDataFromSystemFive(config);
                }

            }
            catch (Exception e)
            {
                throw new Exception_WhileGettingData("An error occured Getting Data From System Five.", e);
            }
            Console.WriteLine("Finished - Loading Inventory From Real Windward");

            // Get E-Commerce parts
            IEnumerable<IS5InvAssembled> ECommInvAss = InvAss.Where(prod => prod.Inv.Ecommerce == "Y").Take(5);

            // Use facade to create data load format from Assembled Inventory Data
            IEnumerable<AdaptToShopifyDataLoadFormat> adapters = ECommInvAss.Select(Inv =>
            {
                AdaptToShopifyDataLoadFormat tmp = new AdaptToShopifyDataLoadFormat();
                tmp.Init(Inv);
                return tmp;
            }
            );
            IEnumerable<IShopify_Product> prod = adapters.Select(x => (IShopify_Product)x);
            IEnumerable<IShopify_Prices> prices = adapters.Select(x => (IShopify_Prices)x);
            IEnumerable<IShopify_Quantities> quantities = adapters.Select(x => (IShopify_Quantities)x);


            Func<IShopifyDataLoadFormat, IShopifyDataLoadFormat, bool> fEquals = (from, to) => { return from.Equals(to); };
            Func<IShopify_Prices, IShopify_Prices, bool> fEquals_Prices = (from, to) => { return from.Equals(to); };
            Func<IShopify_Quantities, IShopify_Quantities, bool> fEquals_Quantities = (from, to) => { return from.Equals(to); };
            Func<IShopify_Product, IShopify_Product, bool> fEquals_Product = (from, to) => { return from.Equals(to); };

            // Direct from Shopify
            List<Product_Product> all = cmShopify.GetAllShopifyRecords().ToList();
            string strProducts = System.Text.Json.JsonSerializer.Serialize(all, typeof(List<Product_Product>));
            IEnumerable<ProductAdapter> AllProduct = all.Select(prod => { ProductAdapter pa = new ProductAdapter(); pa.Init(prod); return pa; });

            #region Reece Products
            IEnumerable<IShopify_Product> API_Products = ReeceShopify.GetAllShopify_Products();
            IEnumerable<IShopify_Product> ChangedRecords_Product = UpdateProcessPattern<IShopify_Product, Shopify_Product, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Product, adapters, API_Products);
            #endregion Reece Products

            #region Reece Prices
            //IEnumerable<IShopify_Prices> API_Prices = ReeceShopify.GetAllShopify_Prices();
            IEnumerable<tmpShopify_Prices> tmpPrices = ReeceShopify.GetAlltmpShopify_Prices();
            IEnumerable<IShopify_Prices> MissingInfoPrices = tmpPrices.Select(p => p.AsShopify_Prices());
            Func<IShopify_Product, string> fGetProductPartNumber = (sp) => { return sp.PartNumber; };
            Func<IShopify_Prices, string> fGetPricesPartNumber = (sp) => { return sp.PartNumber; };

            IEnumerable<Tuple<IShopify_Product, IShopify_Prices>> joined_prices = 
                GenericJoins<IShopify_Product, IShopify_Prices, string>.LeftJoin(API_Products, MissingInfoPrices, fGetProductPartNumber, fGetPricesPartNumber);
            
            IEnumerable<IShopify_Prices> jp = joined_prices.Where(p => p.Item2 != null).Select(p => 
            { 
                p.Item2.InvUnique = p.Item1.InvUnique;
                p.Item2.Cat = p.Item1.Cat;
                p.Item2.WholesaleCost = (decimal)100000;
                return p.Item2;
            });
            IEnumerable<IShopify_Prices> ChangedRecords_Prices = UpdateProcessPattern<IShopify_Prices, Shopify_Prices, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Prices, adapters, jp);
            #endregion Reece Prices

            #region Reece Quantities
            //IEnumerable<IShopify_Quantities> API_Quantities = ReeceShopify.GetAllShopify_Quantities();
            IEnumerable<tmpShopify_Quantities> tmpApi_Quantities = ReeceShopify.GetAlltmpShopify_Quantities();
            IEnumerable<IShopify_Quantities> MissingInfoQuantities = tmpApi_Quantities.Select(p => p.AsShopify_Quantities());
            Func<IShopify_Quantities, string> fGetQuantitiesPartNumber = (sp) => { return sp.PartNumber; };

            IEnumerable<Tuple<IShopify_Product, IShopify_Quantities>> joined_quantities =
                GenericJoins<IShopify_Product, IShopify_Quantities, string>.LeftJoin(API_Products, MissingInfoQuantities, fGetProductPartNumber, fGetQuantitiesPartNumber);
            IEnumerable<IShopify_Quantities> jq = joined_quantities.Where(p => p.Item2 != null).Select(p =>
            {
                p.Item2.InvUnique = p.Item1.InvUnique;
                p.Item2.Cat = p.Item1.Cat;
                return p.Item2;
            });
            IEnumerable<IShopify_Quantities> ChangedRecords_Quantities = UpdateProcessPattern<IShopify_Quantities, Shopify_Quantities, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Quantities, adapters, jq);
            #endregion Reece Quantities

            #region Perform Edits
            IEnumerable<Shopify_Product> changedProducts = new List<Shopify_Product>();
            IEnumerable<Shopify_Prices> changedPrices = new List<Shopify_Prices>();
            IEnumerable<Shopify_Quantities> changedQuantities = new List<Shopify_Quantities>();
            if (!PreventApiAddsNEdits)
            {
                changedProducts = ChangedRecords_Product.Select(p => p.AsShopify_Product());
                string Product_Edit_Results = string.Empty;
                if (!PreventProduct)
                { Product_Edit_Results = ReeceShopify.Products_Edit(changedProducts); }

                changedPrices = ChangedRecords_Prices.Select(p => p.AsShopify_Prices());
                string Prices_Edit_Results = string.Empty;
                if (!PreventPrices && false)
                { Prices_Edit_Results = ReeceShopify.Prices_Edit(changedPrices); }

                changedQuantities = ChangedRecords_Quantities.Select(p => p.AsShopify_Quantities());
                string Quantities_Edit_Results = string.Empty;
                if (!PreventQuantities)
                { Quantities_Edit_Results = ReeceShopify.Quantities_Edit(changedQuantities); }
            }
            else
            {
                Console.WriteLine("Perform Edits Supressed due to Settings");
            }
            #endregion Perform Edits

            #region Perform Adds
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
                { string Product_Add_Results = ReeceShopify.Products_Add(NewProducts); }
            }
            try
            {
                string content = System.Text.Json.JsonSerializer.Serialize(NewProducts.ToList(), typeof(List<Shopify_Product>));
                System.IO.File.WriteAllText("C:\\Temp\\NewProducts.txt", content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error serializing and saving new products to file. Message: " + e.Message);
            }

            NewPricesPairs = GenericJoins<IShopify_Prices, IShopify_Prices, int>
                .LeftJoin(adapters, jp, IShopifyDataLoadFormat_Indexes.UniqueId, IShopifyDataLoadFormat_Indexes.UniqueId);
            IEnumerable<Shopify_Prices> NewPrices = NewPricesPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsShopify_Prices());
            if (!PreventApiAddsNEdits)
            {
                if (!PreventPrices)
                { string Prices_Add_Results = ReeceShopify.Prices_Add(NewPrices); }
            }
            try
            {
                string content = System.Text.Json.JsonSerializer.Serialize(NewPrices.ToList(), typeof(List<Shopify_Prices>));
                System.IO.File.WriteAllText("C:\\Temp\\NewPrices.txt", content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error serializing and saving new prices to file. Message: " + e.Message);
            }

            NewQuantitiesPairs = GenericJoins<IShopify_Quantities, IShopify_Quantities, int>
                .LeftJoin(adapters, jq, IShopifyDataLoadFormat_Indexes.UniqueId, IShopifyDataLoadFormat_Indexes.UniqueId);
            IEnumerable<Shopify_Quantities> NewQuantities = NewQuantitiesPairs.Where(p => p.Item2 == null).Select(p => p.Item1.AsShopify_Quantities());
            if (!PreventApiAddsNEdits)
            {
                if (!PreventQuantities)
                { string Quantities_Add_Results = ReeceShopify.Quantities_Add(NewQuantities); }
            }
            try
            {
                string content = System.Text.Json.JsonSerializer.Serialize(NewQuantities.ToList(), typeof(List<Shopify_Quantities>));
                System.IO.File.WriteAllText("C:\\Temp\\NewQuantities.txt", content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error serializing and saving new quantities to file. Message: " + e.Message);
            }
            #endregion Perform Adds

            // serialize the results to prep them for sending
            string result2 = SerializeForExport(adapters);
            //result2 = SerializeForExport(ChangedRecords);
            string result = string.Empty;

            try
            {
                IEnumerable<Shopify_Product> _ChangedRecords_Product = ChangedRecords_Product.Select(rec => (Shopify_Product)(new Shopify_Product().CopyFrom(rec)));
                result = JsonSerializer.Serialize(_ChangedRecords_Product, typeof(IEnumerable<Shopify_Product>));
            }
            catch
            {
                Console.WriteLine("Serialize of ChangedRecordPairs_Product falied");
            }
            IEnumerable<Shopify_Product> _AllRecords_Product = adapters.Select(rec => (Shopify_Product)(new Shopify_Product().CopyFrom(rec)));
            string result3 = JsonSerializer.Serialize(_AllRecords_Product, typeof(IEnumerable<Shopify_Product>));


            //Console.WriteLine(result);
            File.WriteAllText("c:\\temp\\results.txt", result);

            #region logic and reporting from Colonial version
            //// A: Get XRef from Account for AUnique,BankInfo pairs
            //IEnumerable<QryAccount> accts = GetXRefFromSupplierRecords(config);

            //// B: Attach BankInfo / wholesaler to Assembled Inventory to make InvAss, wholesaler pairs
            ////   GenericJoins service will do this
            //IEnumerable<Tuple<IS5InvAssembled, QryAccount>> result = MakePairs(InvAss, accts);

            //// C: create a facade to go from InvAss, wholesaler pairs to a data load format
            //IEnumerable<AdaptToShopifyDataLoadFormat> adapters = result.Select(p =>
            //{
            //    AdaptToShopifyDataLoadFormat tmp = new AdaptToShopifyDataLoadFormat();
            //    tmp.Init(new ValueTuple<IS5InvAssembled, QryAccount>(p.Item1, p.Item2));
            //    return tmp;
            //}
            //);

            //// At this point I believe I can focus on putting into CSV file. So implement ICopyable<T>
            //IEnumerable<ShopifyDataLoadFormat> DataLoad = adapters.Select(a =>
            //{
            //    ShopifyDataLoadFormat tmp = new ShopifyDataLoadFormat();
            //    tmp.CopyFrom(a); // .CopyFrom is from ICopyable
            //    tmp.SupplierPartNumber = tmp.SupplierPartNumber.TrimEnd();
            //    tmp.S5Orig_ListPrice = a.PriceSchedule1_MSRP;
            //    tmp.S5Orig_MinPrice = a.PriceSchedule2_MinPrice;
            //    tmp.S5Orig_WholesaleCost = a.WholesaleCost;
            //    return tmp;
            //});

            //List<(IPriceFile IFrom, ICommonFields ITo)> pairs = new List<(IPriceFile, ICommonFields)>();
            ////  - Assemble pairs of matching records that need to be updated from each other. 
            ////      - DataLoadFormat is ICommonFields
            ////      - BevNetRecords is IPriceFile
            ////      - Join them together into pairs and perorm the update
            //Func<ICommonFields, (string SupplierCode, string SupplierPart)> keyDataLoad = (common) =>
            //{
            //    return new ValueTuple<string, string>(common.SupplierCode.ToUpper(), common.SupplierPartNumber);
            //};
            //Func<IPriceFile, ValueTuple<string, string>> keyBevNet = (priceRecord) =>
            //{
            //    return new ValueTuple<string, string>(priceRecord.WHOLESALER.ToUpper(), priceRecord.PROD_ITEM);
            //};

            //IEnumerable<Tuple<IPriceFile, ICommonFields>> updatePairs
            //    = GenericJoins<IPriceFile, ICommonFields, (string SupplierCode, string SupplierPart)>
            //    .InnerJoin(BevNetRecords, DataLoad, keyBevNet, keyDataLoad);
            //pairs = updatePairs.Select(p => new ValueTuple<IPriceFile, ICommonFields>(p.Item1, p.Item2)).ToList();

            //foreach (var pair in pairs)
            //{
            //    PriceFileAdapter adapter = new PriceFileAdapter();
            //    adapter.Init(pair.Item1);
            //    pair.Item2.CopyFrom(adapter);
            //}

            //DataLoad = pairs.Select(p => (DataLoadFormat)p.Item2);

            //// export the resulting data load object to CSV
            //var engine = new FileHelperAsyncEngine<DataLoadFormat>();
            //engine.HeaderText = "InvUnique, Cat, PartNumber, SupplierName, SupplierPartNumber, SupplierCode " +
            //    ", WholesaleCost, PriceSchedule1_MSRP, PriceSchedule2_MinPrice, S5Orig_WholesaleCost" +
            //    ", S5Orig_ListPrice, S5Orig_MinPrice, Change_WholesaleCost, Change_ListPrice, Change_MinPrice";
            //using (engine.BeginWriteFile(config["Shopifyinfo:SourceDirectory"] + "S5InventoryDataLoad.csv"))
            //{
            //    foreach (var record in DataLoad)
            //    {
            //        record.Change_ListPrice = record.PriceSchedule1_MSRP - record.S5Orig_ListPrice;
            //        record.Change_MinPrice = record.PriceSchedule2_MinPrice - record.S5Orig_MinPrice;
            //        record.Change_WholesaleCost = record.WholesaleCost - record.S5Orig_WholesaleCost;
            //        engine.WriteNext(record);
            //    }
            //}
            #endregion logic and reporting from Colonial version

            Console.WriteLine("Done");
            Console.ReadKey();
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

        //private static IEnumerable<Tuple<IS5InvAssembled, QryAccount>> MakePairs(IEnumerable<IS5InvAssembled> InvAss, IEnumerable<QryAccount> accts)
        //{
        //    Func<QryAccount, int> QryAccount_Index = (record) =>
        //    {
        //        return record.AUnique;
        //    };

        //    IEnumerable<Tuple<IS5InvAssembled, QryAccount>> result = GenericJoins<IS5InvAssembled, QryAccount, int>
        //        .InnerJoin(InvAss, accts, IS5InvAssembled_Indexes.SupplierUnique_Key, QryAccount_Index);
        //    return result;
        //}
        //private static IEnumerable<QryAccount> GetXRefFromSupplierRecords(IConfiguration config)
        //{
        //    // A: Get XRef from Account for AUnique,BankInfo pairs
        //    QryAccountContext context = new QryAccountContext();
        //    Options opt = OdbcOptions.GetOptions(config["Shopifyinfo:DSNinfo"]);
        //    context.init(opt);

        //    Func<QryAccount, QryAccount> clean = (acct) =>
        //    {
        //        acct.AName = acct.AName.TrimEnd();
        //        acct.BankInfo = acct.BankInfo.TrimEnd();
        //        return acct;
        //    };

        //    IEnumerable<QryAccount> records = context._Records.Select(r => clean(r));
        //    return records;
        //}

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
