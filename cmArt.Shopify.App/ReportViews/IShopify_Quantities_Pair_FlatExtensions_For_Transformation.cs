using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public static class IShopify_Quantities_Pair_FlatExtensions_For_Transformation
    {
        public static Shopify_Quantities_Pair_Flat AsShopify_Quantities_Pair_Flat(this IShopify_Quantities_Pair_Flat data)
        {
            IShopify_Quantities_Pair_Flat _data = data ?? new Shopify_Quantities_Pair_Flat();
            Shopify_Quantities_Pair_Flat result = new Shopify_Quantities_Pair_Flat();

            result.LeftCat = _data.LeftCat;
            result.LeftQuantities = _data.LeftQuantities;
            result.LeftInvUnique = _data.LeftInvUnique;
            result.LeftPartNumber = _data.LeftPartNumber;

            result.RightCat = _data.RightCat;
            result.RightQuantities = _data.RightQuantities;
            result.RightInvUnique = _data.RightInvUnique;
            result.RightPartNumber = _data.RightPartNumber;

            return result;
        }
    }
}
