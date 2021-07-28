using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class S5QtyPairIndexes
    {
        public static short Department(S5QtyPair pair)
        {
            return pair.Location;
        }
    }
}
