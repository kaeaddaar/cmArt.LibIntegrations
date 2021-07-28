using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopifyDataLoadFormatExtensions
    {
        public static bool Equals(this IShopifyDataLoadFormat compareFrom, IShopifyDataLoadFormat compareTo)
        {
            try
            {
                if (
                    compareTo.Cat == compareFrom.Cat
                    && compareTo.Description == compareFrom.Description
                    && compareTo.InvUnique == compareFrom.InvUnique
                    && compareTo.PartNumber == compareFrom.PartNumber
                    && compareTo.WholesaleCost == compareFrom.WholesaleCost
                )
                {
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
