using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector.Data
{
    public interface IShopifyDataLoadFormat_POCO
    {
        string Cat { get; set; }
        string Description { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        IEnumerable<S5PricePair> Prices { get; set; }
        IEnumerable<S5QtyPair> Quantities { get; set; }
        string WebCategory { get; set; }

    }
}
