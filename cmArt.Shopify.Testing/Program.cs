using System;
using System.Collections.Generic;
using cmArt.Shopify.App.Data;
using System.Text.Json;


namespace cmArt.Shopify.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin Test");

            List<ShopifyDataLoadFormat> Records = new List<ShopifyDataLoadFormat>();

            List<pair> first = new List<pair>();
            first.Add(new pair(0, 200));
            first.Add(new pair(1, 180));
            first.Add(new pair(2, 160));

            List<pair> second = new List<pair>();
            second.Add(new pair(0, 2000));
            second.Add(new pair(1, 1800));
            second.Add(new pair(2, 1600));

            Records.Add
            (
                new ShopifyDataLoadFormat()
                {
                    Cat = "250"
                    , Description = "Description"
                    , InvUnique = 1000
                    , PartNumber = "12345"
                    , WholesaleCost = (decimal)100
                    , Prices = first
                }
            );
            Records.Add
            (
                new ShopifyDataLoadFormat()
                {
                    Cat = "250"
                    , Description = "Description2"
                    , InvUnique = 2000
                    , PartNumber = "ABCDE"
                    , WholesaleCost = (decimal)1000
                    , Prices = second
                }
            );

            string result = JsonSerializer.Serialize(Records, typeof(IEnumerable<ShopifyDataLoadFormat>));
            Console.WriteLine(result);

            Console.WriteLine("End Test");
            Console.ReadKey();
        }
        
    }
}
