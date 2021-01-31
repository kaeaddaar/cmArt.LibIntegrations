using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


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

            Console.WriteLine($"Path: {config["SourceDirectory"]}");

            Console.WriteLine("Done");
            Console.ReadKey();
        }


    }
}
