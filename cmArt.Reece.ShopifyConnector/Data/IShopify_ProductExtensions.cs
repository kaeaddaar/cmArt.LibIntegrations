using cmArt.LibIntegrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopify_ProductExtensions
    {

        public static Shopify_Product AsShopify_Product(this Shopify_Product data)
        {
            Shopify_Product _prod = new Shopify_Product();

            _prod.Cat = data.Cat;
            _prod.Description = data.Description;
            _prod.InvUnique = data.InvUnique;
            _prod.PartNumber = data.PartNumber;
            _prod.WholesaleCost = data.WholesaleCost;

            return _prod;
        }

        public static Shopify_Product AsShopify_Product(this IShopify_Product data)
        {
            Shopify_Product _prod = new Shopify_Product();
            _prod.CopyFrom(data);
            return _prod;
        }
        public static bool Equals(this IShopify_Product compareFrom, IShopify_Product compareTo)
        {
            try
            {
                if (
                    compareTo.Cat == compareFrom.Cat
                    && compareTo.Description.TrimEnd() == compareFrom.Description.TrimEnd()
                    && compareTo.InvUnique == compareFrom.InvUnique
                    && compareTo.PartNumber.TrimEnd() == compareFrom.PartNumber.TrimEnd()
                )
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static IShopify_Product CopyFrom(this IShopify_Product to, IShopify_Product from)
        {
            to.Cat = from.Cat;
            to.Description = from.Description.TrimEnd();
            to.InvUnique = from.InvUnique;
            to.PartNumber = from.PartNumber.TrimEnd();
            return to;
        }

    }
}
