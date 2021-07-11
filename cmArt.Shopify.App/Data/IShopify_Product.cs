using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public interface IShopify_Product : IShopify_Identity
    {
        string Description { get; set; }
        decimal WholesaleCost { get; set; }
    }
}
