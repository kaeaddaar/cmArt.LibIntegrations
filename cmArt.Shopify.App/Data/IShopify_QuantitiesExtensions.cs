using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public static class IShopify_QuantitiesExtensions
    {
        public static bool Equals(this IShopify_Quantities compareFrom, IShopify_Quantities compareTo)
        {
            try
            {
                if (compareTo.InvUnique == compareFrom.InvUnique)
                {
                    IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                        .FullOuterJoin(LeftRecords: compareTo.Quantities, RightRecords: compareTo.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
                    foreach (var QtyPair in QtyPairs)
                    {
                        if (QtyPair.Item1.Qty != QtyPair.Item2.Qty)
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
