using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public static class IShopify_ProductExtensions
    {
        public static bool Equals(this IShopify_Product compareFrom, IShopify_Product compareTo)
        {
            try
            {
                if (
                    compareTo.Cat == compareFrom.Cat
                    && compareTo.Description == compareFrom.Description
                    && compareTo.InvUnique == compareFrom.InvUnique
                    && compareTo.PartNumber == compareFrom.PartNumber
                    && compareTo.WholesaleCost == compareFrom.WholesaleCost
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
    }
}
