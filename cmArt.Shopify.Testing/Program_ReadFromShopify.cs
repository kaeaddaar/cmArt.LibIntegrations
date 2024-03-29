﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cmArt.Shopify.Connector;
using cmArt.Shopify.Connector.Data;

namespace cmArt.Shopify.Testing
{
    class Program_ReadFromShopify
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetPage(null));
            List<Product_Product> all = cmShopify.GetAllShopifyProductRecords().ToList();
            string strProducts = System.Text.Json.JsonSerializer.Serialize(all, typeof(List<Product_Product>));
            System.IO.File.WriteAllText("C:\\Temp\\allShopify.txt", strProducts);
            Console.ReadKey();
        }
        public static string GetPage(string lastid)
        {
            return ApiCalls.MakeApiGetCall("/admin/api/2021-07/products.json");
        }

    }
}
