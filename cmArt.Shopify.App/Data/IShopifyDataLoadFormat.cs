using System.Collections.Generic;

namespace cmArt.Shopify.App.Data
{
    public interface IShopifyDataLoadFormat : IShopify_Identity, IShopify_Product, IShopify_Prices
    {
        IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom);
        bool Equals(IShopifyDataLoadFormat compareTo);
        
    }
}