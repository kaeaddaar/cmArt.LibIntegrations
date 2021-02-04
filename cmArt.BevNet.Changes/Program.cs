using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using cmArt.LibIntegrations.CsvFileReaderService;
using cmArt.BevNet;
using System.Linq;

namespace cmArt.BevNet.Changes
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
                Console.WriteLine(BevNetRecords.Skip(i).FirstOrDefault().PROD_ITEM);
            }

            Console.WriteLine($"Path: {config["SourceDirectory"]}");

            Console.WriteLine("Done");
            Console.ReadKey();
        }


    }
}
