using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public class Shopify_Prices_Pair : Generic_Pair<Shopify_Prices>
    {
        public Shopify_Prices_Pair()
        {
        }

        public Shopify_Prices_Pair(Shopify_Prices S5In, Shopify_Prices ShopifyIn) : base(S5In, ShopifyIn)
        {
        }
    }
}
