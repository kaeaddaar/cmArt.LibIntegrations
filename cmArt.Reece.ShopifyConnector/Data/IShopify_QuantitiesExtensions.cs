using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopify_QuantitiesExtensions
    {
        public static Shopify_Quantities AsShopify_Quantities(this IShopify_Quantities data)
        {
            Shopify_Quantities _quantities = new Shopify_Quantities();
            _quantities.Cat = data.Cat;
            _quantities.InvUnique = data.InvUnique;
            _quantities.PartNumber = data.PartNumber;
            _quantities.Quantities = data.Quantities;
            return _quantities;
        }
        public static IShopify_Quantities CopyFrom(this IShopify_Quantities To, IShopify_Quantities From)
        {
            To.Cat = From.Cat.TrimEnd();
            To.InvUnique = From.InvUnique;
            To.PartNumber = From.PartNumber.TrimEnd();
            To.Quantities = From.Quantities;

            return To;
        }
        public static bool Equals(this IShopify_Quantities compareFrom, IShopify_Quantities compareTo)
        {
            try
            {
                if (compareTo.InvUnique == compareFrom.InvUnique)
                {
                    if (compareFrom.Quantities == null && compareTo.Quantities != null) { return false; }
                    if (compareFrom.Quantities != null && compareTo.Quantities == null) { return false; }
                    if (compareFrom.Quantities == null && compareTo.Quantities == null) { return true; }

                    IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                        .FullOuterJoin(LeftRecords: compareFrom.Quantities, RightRecords: compareTo.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
                    foreach (var QtyPair in QtyPairs)
                    {
                        if (QtyPair.Item1 == null || QtyPair.Item2 == null)
                        {
                            return false;
                        }
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
