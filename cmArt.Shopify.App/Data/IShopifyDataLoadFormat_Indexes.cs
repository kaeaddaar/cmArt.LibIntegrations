using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public static class IShopifyDataLoadFormat_Indexes
    {
        public static int UniqueId(IShopifyDataLoadFormat data)
        {
            return data.InvUnique;
        }
    }
}
