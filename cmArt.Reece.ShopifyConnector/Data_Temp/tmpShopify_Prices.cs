using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class tmpShopify_Prices
    {
        public IEnumerable<S5PricePair> prices { get; set; }
        public string partNumber { get; set; }
        public Shopify_Prices AsShopify_Prices()
        {
            Shopify_Prices shopify_Prices = new Shopify_Prices();
            shopify_Prices.Cat = string.Empty;
            shopify_Prices.InvUnique = 0;
            shopify_Prices.PartNumber = this.partNumber;
            shopify_Prices.Prices = this.prices;
            shopify_Prices.WholesaleCost = 0;
            return shopify_Prices;
        }
    }
}
