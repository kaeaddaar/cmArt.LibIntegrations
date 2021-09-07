﻿using System;
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
        public decimal WholesaleCost { get; set; }

        public bool Equals(IShopify_Prices compareTo)
        {
            return IShopify_PricesExtensions.Equals(this, compareTo);
        }
    }
}