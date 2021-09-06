using System;
using cmArt.Shopify.App;


namespace cmArt.Shopify.ConsoleTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("cmArt.Shopify.ConsoleTools.Program.Main has begun executing.");
            Console.WriteLine("This app is designed to wrap the code that does the sync/push of data to shopify, and give the developer control of what processes to run");
            Console.WriteLine("Calling ShopifyConsoleApp.Main_Console");
            ShopifyConsoleApp.Main_Console(args);
            Console.WriteLine("cmArt.Shopify.ConsoleTools.Program.Main ending execution.");
        }
    }
}
