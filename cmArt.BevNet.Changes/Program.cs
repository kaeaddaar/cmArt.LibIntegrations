using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using cmArt.LibIntegrations.CsvFileReaderService;
using cmArt.BevNet;
using System.Linq;
using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;
using cmArt.System5.Inventory;
using cmArt.BevNet.System5;
using cmArt.LibIntegrations.GenericJoinsService;
using FileHelpers;
using cmArt.LibIntegrations;
using cmArt.System5.Data;

namespace cmArt.BevNet.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IEnumerable<IPriceFile> BevNetRecords = new List<PriceFile>();
            string PathAndFile = config["SourceDirectory"] + config["FileName"];
            const bool DontSuppressRecordErrors = false;

            BevNetRecords = FileReaderGeneric<PriceFile>.ReadFile
            (   PathAndFile: PathAndFile
                , SupressRecordErrors: DontSuppressRecordErrors
            );

            int MaxRecordsToDisplay = 10;
            for (int i = 0; i < MaxRecordsToDisplay; i++)
            {
                Console.WriteLine((BevNetRecords.Skip(i).FirstOrDefault() ?? new PriceFile()).PROD_ITEM);
            }

            Console.WriteLine($"Path: {config["SourceDirectory"]}");

            Console.WriteLine("Loading Inventory From Real Windward");
            
            IEnumerable<IS5InvAssembled> InvAss = GetDataFromSystemFive(config);
            Console.WriteLine("Finished - Loading Inventory From Real Windward");

            // A: Get XRef from Account for AUnique,BankInfo pairs
            IEnumerable<QryAccount> accts = GetXRefFromSupplierRecords(config);

            // B: Attach BankInfo / wholesaler to Assembled Inventory to make InvAss, wholesaler pairs
            //   GenericJoins service will do this
            IEnumerable<Tuple<IS5InvAssembled, QryAccount>> result = MakePairs(InvAss, accts);

            // C: create a facade to go from InvAss, wholesaler pairs to a data load format
            IEnumerable<AdaptToDataLoadFormat> adapters = result.Select(p =>
                {
                    AdaptToDataLoadFormat tmp = new AdaptToDataLoadFormat();
                    tmp.Init(new ValueTuple<IS5InvAssembled, QryAccount>(p.Item1, p.Item2));
                    return tmp;
                }
            );

            // At this point I believe I can focus on putting into CSV file. So implement ICopyable<T>
            IEnumerable<DataLoadFormat> DataLoad = adapters.Select(a => 
            {
                DataLoadFormat tmp = new DataLoadFormat();
                tmp.CopyFrom(a); // .CopyFrom is from ICopyable
                tmp.SupplierPartNumber = tmp.SupplierPartNumber.TrimEnd();
                tmp.S5Orig_ListPrice = a.PriceSchedule1_MSRP;
                tmp.S5Orig_MinPrice = a.PriceSchedule2_MinPrice;
                tmp.S5Orig_WholesaleCost = a.WholesaleCost;
                return tmp;
            });

            List<(IPriceFile IFrom, ICommonFields ITo)> pairs = new List<(IPriceFile, ICommonFields)>();
            //  - Assemble pairs of matching records that need to be updated from each other. 
            //      - DataLoadFormat is ICommonFields
            //      - BevNetRecords is IPriceFile
            //      - Join them together into pairs and perform the update
            Func<ICommonFields, (string SupplierCode, string SupplierPart)> keyDataLoad = (common) =>
            {
                return new ValueTuple<string, string>(common.SupplierCode.ToUpper(), common.SupplierPartNumber);
            };
            Func<IPriceFile, ValueTuple<string, string>> keyBevNet = (priceRecord) =>
            {
                return new ValueTuple<string, string>(priceRecord.WHOLESALER.ToUpper(), priceRecord.PROD_ITEM);
            };

            IEnumerable<Tuple<IPriceFile, ICommonFields>> updatePairs
                = GenericJoins<IPriceFile, ICommonFields, (string SupplierCode, string SupplierPart)>
                .InnerJoin(BevNetRecords, DataLoad, keyBevNet, keyDataLoad);
            pairs = updatePairs.Select(p => new ValueTuple<IPriceFile, ICommonFields>(p.Item1, p.Item2)).ToList();

            foreach (var pair in pairs)
            {
                PriceFileAdapter adapter = new PriceFileAdapter();
                adapter.Init(pair.Item1);
                pair.Item2.CopyFrom(adapter);
            }

            DataLoad = pairs.Select(p => (DataLoadFormat)p.Item2);

            // export the resulting data load object to CSV
            var engine = new FileHelperAsyncEngine<DataLoadFormat>();
            engine.HeaderText = "InvUnique, Cat, PartNumber, SupplierName, SupplierPartNumber, SupplierCode " +
                ", WholesaleCost, PriceSchedule1_MSRP, PriceSchedule2_MinPrice, S5Orig_WholesaleCost" +
                ", S5Orig_ListPrice, S5Orig_MinPrice, Change_WholesaleCost, Change_ListPrice, Change_MinPrice";
            using (engine.BeginWriteFile(config["SourceDirectory"] + "S5InventoryDataLoad.csv"))
            {
                foreach (var record in DataLoad)
                {
                    record.Change_ListPrice = record.PriceSchedule1_MSRP - record.S5Orig_ListPrice;
                    record.Change_MinPrice = record.PriceSchedule2_MinPrice - record.S5Orig_MinPrice;
                    record.Change_WholesaleCost = record.WholesaleCost - record.S5Orig_WholesaleCost;
                    engine.WriteNext(record);
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
        private static IEnumerable<Tuple<IS5InvAssembled, QryAccount>> MakePairs(IEnumerable<IS5InvAssembled> InvAss, IEnumerable<QryAccount> accts)
        {
            Func<QryAccount, int> QryAccount_Index = (record) =>
            {
                return record.AUnique;
            };

            IEnumerable<Tuple<IS5InvAssembled, QryAccount>> result = GenericJoins<IS5InvAssembled, QryAccount, int>
                .InnerJoin(InvAss, accts, IS5InvAssembled_Indexes.SupplierUnique_Key, QryAccount_Index);
            return result;
        }
        private static IEnumerable<QryAccount> GetXRefFromSupplierRecords(IConfiguration config)
        {
            // A: Get XRef from Account for AUnique,BankInfo pairs
            QryAccountContext context = new QryAccountContext();
            Options opt = OdbcOptions.GetOptions(config["System5DSN"]);
            context.init(opt);

            Func<QryAccount, QryAccount> clean = (acct) =>
            {
                acct.AName = acct.AName.TrimEnd();
                acct.BankInfo = acct.BankInfo.TrimEnd();
                return acct;
            };

            IEnumerable<QryAccount> records = context._Records.Select(r => clean(r));
            return records;
        }
        private static IEnumerable<IS5InvAssembled> GetDataFromJson(IConfiguration config)
        {
            string CachedFilesDirectory = config["SourceDirectory"];

            Options opt = PagedJsonOptions_S5Inventory.GetOptions(CachedFilesDirectory, new List<string>());
            OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);

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
        private static IEnumerable<IS5InvAssembled> GetDataFromSystemFive(IConfiguration config)
        {
            string DSN = config["System5DSN"];

            Options opt = OdbcOptions.GetOptions(DSN);
            OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);

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
