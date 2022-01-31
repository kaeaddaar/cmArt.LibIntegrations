using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class Shopify_Quantities : IShopify_Quantities
    {
        public IEnumerable<S5QtyPair> Quantities { get; set; }
        public string Cat { get; set; }
        public int InvUnique { get; set; }
        public string PartNumber { get; set; }

        public bool cmEquals(IShopify_Quantities compareTo)
        {
            return IShopify_Quantities.Equals(this, compareTo);
        }

        public IShopify_Quantities CopyFrom(IShopify_Quantities IFrom)
        {
            return IShopify_QuantitiesExtensions.CopyFrom(this, IFrom);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Quantities CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }
    }
}
