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

            List<S5PricePair> first = new List<S5PricePair>();
            first.Add(new S5PricePair(0, 200));
            first.Add(new S5PricePair(1, 180));
            first.Add(new S5PricePair(2, 160));

            List<S5PricePair> second = new List<S5PricePair>();
            second.Add(new S5PricePair(0, 2000));
            second.Add(new S5PricePair(1, 1800));
            second.Add(new S5PricePair(2, 1600));

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
