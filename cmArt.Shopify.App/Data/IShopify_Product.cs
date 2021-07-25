using cmArt.LibIntegrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public interface IShopify_Product : IShopify_Identity, IEquality<IShopify_Product>, ICopyable<IShopify_Product>
    {
        string Description { get; set; }
    }
}
