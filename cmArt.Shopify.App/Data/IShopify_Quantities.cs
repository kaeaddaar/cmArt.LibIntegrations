using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public interface IShopify_Quantities : IShopify_Identity
    {
        IEnumerable<S5QtyPair> Quantities { get; set; }
    }
}
