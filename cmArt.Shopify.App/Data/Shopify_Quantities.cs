using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public class Shopify_Quantities : IShopify_Quantities
    {
        public IEnumerable<S5QtyPair> Quantities { get; set; }
        public string Cat { get; set; }
        public int InvUnique { get; set; }
        public string PartNumber { get; set; }

        public bool Equals(IShopify_Quantities compareTo)
        {
            return IShopify_Quantities.Equals(this, compareTo);
        }
    }
}
