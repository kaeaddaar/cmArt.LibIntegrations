using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class Shopify_Product : IShopify_Product
    {
        public string Description { get; set; }
        public decimal WholesaleCost { get; set; }
        public string Cat { get; set; }
        public int InvUnique { get; set; }
        public string PartNumber { get; set; }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return IShopify_ProductExtensions.CopyFrom(this, IFrom);
        }

        public bool Equals(IShopify_Product compareTo)
        {
            return IShopify_Product.Equals(this, compareTo);
        }
    }
}
