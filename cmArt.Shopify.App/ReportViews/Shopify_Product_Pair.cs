using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public class Shopify_Product_Pair : IShopify_Product_Pair
    {
        public Shopify_Product S5 { get; set; }
        public Shopify_Product Shopify { get; set; }
        public Shopify_Product_Pair()
        {
            S5 = S5 ?? new Shopify_Product();
            Shopify = Shopify ?? new Shopify_Product();
        }
        public Shopify_Product_Pair(ValueTuple<Shopify_Product, Shopify_Product> S5_Shopify_Pair)
        {
            S5 = S5_Shopify_Pair.Item1 ?? new Shopify_Product();
            Shopify = S5_Shopify_Pair.Item2 ?? new Shopify_Product();
        }
    }
}
