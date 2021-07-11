using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public interface IShopify_Prices : IShopify_Identity
    {
        IEnumerable<S5PricePair> Prices { get; set; }
    }
}
