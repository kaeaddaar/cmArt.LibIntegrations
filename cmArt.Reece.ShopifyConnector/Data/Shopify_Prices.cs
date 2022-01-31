using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class Shopify_Prices : IShopify_Prices
    {
        public IEnumerable<S5PricePair> Prices { get; set; }
        public string Cat { get; set; }
        public int InvUnique { get; set; }
        public string PartNumber { get; set; }
        //public decimal WholesaleCost { get; set; }

        public bool cmEquals(IShopify_Prices compareTo)
        {
            return IShopify_PricesExtensions.Equals(this, compareTo);
        }

        public IShopify_Prices CopyFrom(IShopify_Prices IFrom)
        {
            return IShopify_PricesExtensions.CopyFrom(this, IFrom);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Prices CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }
    }
}
