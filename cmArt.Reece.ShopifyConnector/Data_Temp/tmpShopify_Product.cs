using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class tmpShopify_Product
    {
        public string Description { get; set; }
        public decimal WholesaleCost { get; set; }
        public string Cat { get; set; }
        public string InvUnique { get; set; }
        public string PartNumber { get; set; }
        public Shopify_Product AsShopify_Product()
        {
            Shopify_Product shopify_Product = new Shopify_Product();
            shopify_Product.Cat = this.Cat;
            shopify_Product.Description = this.Description;
            
            int tmpInvUnique = 0;
            int.TryParse(this.InvUnique, out tmpInvUnique);
            shopify_Product.InvUnique = tmpInvUnique;

            shopify_Product.PartNumber = this.PartNumber;
            shopify_Product.WholesaleCost = this.WholesaleCost;
            return shopify_Product;
        }
    }
}
