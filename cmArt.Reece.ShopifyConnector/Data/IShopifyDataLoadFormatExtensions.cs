using cmArt.LibIntegrations.GenericJoinsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopifyDataLoadFormatExtensions
    {
        // Need Product, Prices, and Quantities separate so breaking it out into 3 methods plus the identity (avoids duplication)
        public static IEnumerable<Changes_View> Diff(this IShopifyDataLoadFormat compareFrom, IShopifyDataLoadFormat compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();
            if (compareTo.Cat != compareFrom.Cat)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "Cat";
                tmp.S5ValueToSendToExternal = compareFrom.Cat;
                tmp.ExternalValueBeforeUpdate = compareTo.Cat;
                changes.Add(tmp);
            }
            if (compareTo.Description != compareFrom.Description)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "Description";
                tmp.S5ValueToSendToExternal = compareFrom.Description;
                tmp.ExternalValueBeforeUpdate = compareTo.Description;
                changes.Add(tmp);
            }
            if (compareTo.PartNumber != compareFrom.PartNumber)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "PartNumber";
                tmp.S5ValueToSendToExternal = compareFrom.PartNumber;
                tmp.ExternalValueBeforeUpdate = compareTo.PartNumber;
                changes.Add(tmp);
            }
            
            IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                .FullOuterJoin(LeftRecords: compareTo.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
            foreach (var PricePair in PricePairs)
            {
                if (PricePair.Item1.Price != PricePair.Item2.Price)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "Prices(Level " + PricePair.Item1.Level.ToString() + ")";
                    tmp.S5ValueToSendToExternal = PricePair.Item2.Price.ToString();
                    tmp.ExternalValueBeforeUpdate = PricePair.Item1.Price.ToString();
                    changes.Add(tmp);
                }
            }

            IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                .FullOuterJoin(LeftRecords: compareTo.Quantities, RightRecords: compareTo.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
            foreach (var QtyPair in QtyPairs)
            {
                if (QtyPair.Item1.Qty != QtyPair.Item2.Qty)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "Quantities(Dept " + QtyPair.Item1.Location.ToString() + ")";
                    tmp.S5ValueToSendToExternal = QtyPair.Item2.Location.ToString();
                    tmp.ExternalValueBeforeUpdate = QtyPair.Item1.Location.ToString();
                    changes.Add(tmp);
                }
            }

            if (compareTo.WholesaleCost != compareFrom.WholesaleCost)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "WholesaleCost";
                tmp.S5ValueToSendToExternal = compareFrom.WholesaleCost.ToString();
                tmp.ExternalValueBeforeUpdate = compareTo.WholesaleCost.ToString();
                changes.Add(tmp);
            }

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Quantities compareFrom, IShopify_Quantities compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                .FullOuterJoin(LeftRecords: compareTo.Quantities, RightRecords: compareTo.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
            foreach (var QtyPair in QtyPairs)
            {
                if (QtyPair.Item1.Qty != QtyPair.Item2.Qty)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "Quantities(Dept " + QtyPair.Item1.Location.ToString() + ")";
                    tmp.S5ValueToSendToExternal = QtyPair.Item2.Location.ToString();
                    tmp.ExternalValueBeforeUpdate = QtyPair.Item1.Location.ToString();
                    changes.Add(tmp);
                }
            }

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Prices compareFrom, IShopify_Prices compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                .FullOuterJoin(LeftRecords: compareTo.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
            foreach (var PricePair in PricePairs)
            {
                if (PricePair.Item1 == null || PricePair.Item2 == null)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;

                    if (PricePair.Item1 == null)
                    {
                        tmp.FieldName = "Prices(Level " + "LeftSide(S5) was null" + ")";
                        tmp.ExternalValueBeforeUpdate = "null";
                    }
                    else
                    {
                        tmp.FieldName = "Prices(Level " + PricePair.Item1.Level.ToString() + ")";
                        tmp.ExternalValueBeforeUpdate = PricePair.Item1.Price.ToString();
                    }

                    if (PricePair.Item2 == null)
                    {
                        tmp.S5ValueToSendToExternal = "null";
                    }
                    else
                    {
                        tmp.S5ValueToSendToExternal = PricePair.Item2.Price.ToString();
                    }

                    changes.Add(tmp);
                }
                else
                {
                    if (PricePair.Item1.Price != PricePair.Item2.Price)
                    {
                        Changes_View tmp = new Changes_View();
                        tmp.InvUnique = compareFrom.InvUnique;
                        tmp.Cat = compareFrom.Cat;
                        tmp.PartNumber = compareFrom.PartNumber;
                        tmp.FieldName = "Prices(Level " + PricePair.Item1.Level.ToString() + ")";
                        tmp.S5ValueToSendToExternal = PricePair.Item2.Price.ToString();
                        tmp.ExternalValueBeforeUpdate = PricePair.Item1.Price.ToString();
                        changes.Add(tmp);
                    }
                }
            }
            //if (compareTo.WholesaleCost != compareFrom.WholesaleCost)
            //{
            //    Changes_View tmp = new Changes_View();
            //    tmp.InvUnique = compareFrom.InvUnique;
            //    tmp.Cat = compareFrom.Cat;
            //    tmp.PartNumber = compareFrom.PartNumber;
            //    tmp.FieldName = "WholesaleCost";
            //    tmp.S5ValueToSendToExternal = compareFrom.WholesaleCost.ToString();
            //    tmp.ExternalValueBeforeUpdate = compareTo.WholesaleCost.ToString();
            //    changes.Add(tmp);
            //}

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Product compareFrom, IShopify_Product compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            if (compareTo.Description != compareFrom.Description)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "Description";
                tmp.S5ValueToSendToExternal = compareFrom.Description;
                tmp.ExternalValueBeforeUpdate = compareTo.Description;
                changes.Add(tmp);
            }

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Identity compareFrom, IShopify_Identity compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();
            if (compareTo.InvUnique != compareFrom.InvUnique)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "InvUnique";
                tmp.S5ValueToSendToExternal = compareFrom.InvUnique.ToString();
                tmp.ExternalValueBeforeUpdate = compareTo.InvUnique.ToString();
                changes.Add(tmp);
            }
            if (compareTo.Cat != compareFrom.Cat)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "Cat";
                tmp.S5ValueToSendToExternal = compareFrom.Cat;
                tmp.ExternalValueBeforeUpdate = compareTo.Cat;
                changes.Add(tmp);
            }
            if (compareTo.PartNumber != compareFrom.PartNumber)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "PartNumber";
                tmp.S5ValueToSendToExternal = compareFrom.PartNumber;
                tmp.ExternalValueBeforeUpdate = compareTo.PartNumber;
                changes.Add(tmp);
            }

            return changes;
        }

        public static bool Equals(this IShopifyDataLoadFormat compareFrom, IShopifyDataLoadFormat compareTo)
        {
            try
            {
                if (
                    compareTo.Cat == compareFrom.Cat
                    && compareTo.Description == compareFrom.Description
                    && compareTo.InvUnique == compareFrom.InvUnique
                    && compareTo.PartNumber == compareFrom.PartNumber
                    //&& compareTo.WholesaleCost == compareFrom.WholesaleCost
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
