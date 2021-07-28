using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopify_PricesExtensions
    {
        public static bool Equals(this IShopify_Prices compareFrom, IShopify_Prices compareTo)
        {
            try
            {
                if (compareTo.InvUnique == compareFrom.InvUnique)
                {
                    if (compareTo.WholesaleCost != compareFrom.WholesaleCost) { return false; }

                    if (compareFrom.Prices == null && compareTo.Prices != null) { return false; }
                    if (compareFrom.Prices != null && compareTo.Prices == null) { return false; }
                    if (compareFrom.Prices == null && compareTo.Prices == null) { return true; }

                    IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                        .FullOuterJoin(LeftRecords: compareTo.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
                    foreach (var PricePair in PricePairs)
                    {
                        if (PricePair.Item1.Price != PricePair.Item2.Price)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
