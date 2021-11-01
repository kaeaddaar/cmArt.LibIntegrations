using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class S5PricePairIndexes
    {
        public static short Level(S5PricePair data)
        {
            if (data == null) { return -1; }
            return data.Level;
        }
    }
}
