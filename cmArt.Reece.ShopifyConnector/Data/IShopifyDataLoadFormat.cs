using System.Collections.Generic;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopifyDataLoadFormat : IShopify_Identity, IShopify_Product, IShopify_Prices, IShopify_Quantities, IEquality<IShopifyDataLoadFormat>
    {
        IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom);
        
    }
}