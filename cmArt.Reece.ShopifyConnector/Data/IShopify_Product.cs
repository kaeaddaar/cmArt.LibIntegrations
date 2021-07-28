using cmArt.LibIntegrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Product : IShopify_Identity, IEquality<IShopify_Product>, ICopyable<IShopify_Product>
    {
        string Description { get; set; }
    }
}
