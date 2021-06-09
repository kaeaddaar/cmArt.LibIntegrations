using System.Collections.Generic;

namespace cmArt.Shopify.App.Data
{
    public interface IShopifyDataLoadFormat
    {
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        string Description { get; set; }
        IEnumerable<pair> Prices { get; set; }
        decimal WholesaleCost { get; set; }
        decimal InStock { get; set; }

        IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom);
    }
}