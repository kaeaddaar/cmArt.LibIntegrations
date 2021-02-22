using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using cmArt.LibIntegrations.CsvFileReaderService;
using cmArt.BevNet;
using System.Linq;
using cmArt.LibIntegrations.OdbcService;
using cmArt.System5.Inventory;

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
            GetXRefFromSupplierRecords();

            // B: Attach BankInfo / wholesaler to Assembled Inventory to make InvAss, wholesaler pairs
            //   GenericJoins service will do this

            // C: create a facade to go from InvAss, wholesaler pairs to a data load format

            // D: create an adapter to go from the data load format to Common fields

            // E: get common fields form BevNets PriceFileAdapter

            // F: apply rehydrator to step Ds adapter and step Es common fields

            // G: export the resulting data load object to CSV

            Console.WriteLine("Done");
            Console.ReadKey();
        }
        private static void GetXRefFromSupplierRecords()
        {
            // A: Get XRef from Account for AUnique,BankInfo pairs

        }
        private static IEnumerable<IS5InvAssembled> GetDataFromSystemFive(IConfiguration config)
        {
            string DSN = config["System5DSN"];

            Options opt = OdbcOptions.GetOptions(DSN);
            OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);

            // Assemble Inventory
            S5Inventory InvRaw = new S5Inventory
            (
                AltSuply_Records: context.AltSuplies
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
