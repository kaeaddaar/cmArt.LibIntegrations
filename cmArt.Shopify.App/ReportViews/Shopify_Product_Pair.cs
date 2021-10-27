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
        public Shopify_Product_Pair(Shopify_Product S5In, Shopify_Product ShopifyIn)
        {
            S5 = S5In ?? new Shopify_Product();
            Shopify = ShopifyIn ?? new Shopify_Product();
        }
    }
}
