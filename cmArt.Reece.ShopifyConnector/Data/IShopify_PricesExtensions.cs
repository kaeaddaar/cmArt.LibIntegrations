using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopify_PricesExtensions
    {
        public static Shopify_Prices AsShopify_Prices(this IShopify_Prices data)
        {
            Shopify_Prices _prices = new Shopify_Prices();
            _prices.Cat = data.Cat;
            _prices.InvUnique = data.InvUnique;
            _prices.PartNumber = data.PartNumber;
            _prices.Prices = data.Prices;
            //_prices.WholesaleCost = data.WholesaleCost;
            return _prices;
        }
        public static IShopify_Prices CopyFrom(this IShopify_Prices To, IShopify_Prices From)
        {
            To.Cat = From.Cat.TrimEnd();
            To.InvUnique = From.InvUnique;
            To.PartNumber = From.PartNumber.TrimEnd();
            To.Prices = From.Prices;

            return To;
        }
        public static bool Equals(this IShopify_Prices compareFrom, IShopify_Prices compareTo)
        {
            try
            {
                if (compareTo.InvUnique == compareFrom.InvUnique)
                {
                    //if (compareTo.WholesaleCost != compareFrom.WholesaleCost) { return false; }

                    if (compareFrom.Prices == null && compareTo.Prices != null) { return false; }
                    if (compareFrom.Prices != null && compareTo.Prices == null) { return false; }
                    if (compareFrom.Prices == null && compareTo.Prices == null) { return true; }

                    IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                        .FullOuterJoin(LeftRecords: compareTo.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
                    foreach (var PricePair in PricePairs)
                    {
                        if (PricePair.Item1 == null || PricePair.Item2 == null) { return false; }
                        if (Math.Round(PricePair.Item1.Price,2) != Math.Round(PricePair.Item2.Price,2))
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
