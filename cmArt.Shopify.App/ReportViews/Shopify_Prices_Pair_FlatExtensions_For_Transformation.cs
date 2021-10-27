using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public static class Shopify_Prices_Pair_FlatExtensions_For_Transformation
    {
        public static Shopify_Prices_Pair_Flat AsShopify_Prices_Pair_Flat(this IShopify_Prices_Pair_Flat data)
        {
            IShopify_Prices_Pair_Flat _data = data ?? new Shopify_Prices_Pair_Flat();
            Shopify_Prices_Pair_Flat result = new Shopify_Prices_Pair_Flat();

            result.LeftCat = _data.LeftCat;
            result.LeftPrices = _data.LeftPrices;
            result.LeftInvUnique = _data.LeftInvUnique;
            result.LeftPartNumber = _data.LeftPartNumber;

            result.RightCat = _data.RightCat;
            result.RightPrices = _data.RightPrices;
            result.RightInvUnique = _data.RightInvUnique;
            result.RightPartNumber = _data.RightPartNumber;

            return result;
        }
    }
}
