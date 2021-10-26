using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public static class IShopify_Product_PairExtensionsForTransformation
    {
        public static Shopify_Product_Pair_Flat AsShopify_Product_Pair_Flat(this IShopify_Product_Pair_Flat data)
        {
            IShopify_Product_Pair_Flat _data = data ?? new Shopify_Product_Pair_Flat();
            Shopify_Product_Pair_Flat result = new Shopify_Product_Pair_Flat();
            
            result.LeftCat = _data.LeftCat;
            result.LeftDescription = _data.LeftDescription;
            result.LeftInvUnique = _data.LeftInvUnique;
            result.LeftPartNumber = _data.LeftPartNumber;

            result.RightCat = _data.RightCat;
            result.RightDescription = _data.RightDescription;
            result.RightInvUnique = _data.RightInvUnique;
            result.RightPartNumber = _data.RightPartNumber;

            return result;
        }
    }
}
