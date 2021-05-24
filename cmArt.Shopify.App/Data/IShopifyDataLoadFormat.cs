using System.Collections.Generic;

namespace cmArt.Shopify.App.Data
{
    public interface IShopifyDataLoadFormat
    {
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        string Description { get; set; }
        IEnumerable<decimal> Prices { get; set; }
        decimal WholesaleCost { get; set; }

        IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom);
    }
}