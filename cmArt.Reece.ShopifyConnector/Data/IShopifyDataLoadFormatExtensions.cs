using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class IShopifyDataLoadFormatExtensions
    {
        private static Changes_View GetNewChanges_View_WithDefaults(IShopifyDataLoadFormat compareFrom)
        {
            Changes_View tmp = new Changes_View();
            tmp.InvUnique = compareFrom.InvUnique;
            tmp.Cat = compareFrom.Cat;
            tmp.PartNumber = compareFrom.PartNumber;

            return tmp;
        }
        private static Changes_View GetNewChanges_View_WithDefaults(IShopify_Product compareFrom)
        {
            Changes_View tmp = new Changes_View();
            tmp.InvUnique = compareFrom.InvUnique;
            tmp.Cat = compareFrom.Cat;
            tmp.PartNumber = compareFrom.PartNumber;

            return tmp;
        }
        private static Changes_View GetNewChanges_View_WithDefaults(IShopify_Prices compareFrom)
        {
            Changes_View tmp = new Changes_View();
            tmp.InvUnique = compareFrom.InvUnique;
            tmp.Cat = compareFrom.Cat;
            tmp.PartNumber = compareFrom.PartNumber;

            return tmp;
        }
        private static Changes_View GetNewChanges_View_WithDefaults(IShopify_Quantities compareFrom)
        {
            Changes_View tmp = new Changes_View();
            tmp.InvUnique = compareFrom.InvUnique;
            tmp.Cat = compareFrom.Cat;
            tmp.PartNumber = compareFrom.PartNumber;

            return tmp;
        }
        private static Changes_View GetNewChanges_View_WithDefaults(IShopify_Identity compareFrom)
        {
            Changes_View tmp = new Changes_View();
            tmp.InvUnique = compareFrom.InvUnique;
            tmp.Cat = compareFrom.Cat;
            tmp.PartNumber = compareFrom.PartNumber;

            return tmp;
        }
        // Need Product, Prices, and Quantities separate so breaking it out into 3 methods plus the identity (avoids duplication)
        public static IEnumerable<Changes_View> Diff(this IShopifyDataLoadFormat compareFrom, IShopifyDataLoadFormat compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();
            if (compareTo.Cat != compareFrom.Cat)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "Cat";
                tmp.S5ValueToSendToExternal = compareFrom.Cat;
                tmp.ExternalValueBeforeUpdate = compareTo.Cat;
                changes.Add(tmp);
            }
            if (compareTo.Description != compareFrom.Description)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "Description";
                tmp.S5ValueToSendToExternal = compareFrom.Description;
                tmp.ExternalValueBeforeUpdate = compareTo.Description;
                changes.Add(tmp);
            }
            if (compareTo.PartNumber != compareFrom.PartNumber)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "PartNumber";
                tmp.S5ValueToSendToExternal = compareFrom.PartNumber;
                tmp.ExternalValueBeforeUpdate = compareTo.PartNumber;
                changes.Add(tmp);
            }
            
            IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                .FullOuterJoin(LeftRecords: compareFrom.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
            foreach (var PricePair in PricePairs)
            {
                if (PricePair.Item1.Price != PricePair.Item2.Price)
                {
                    Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                    tmp.FieldName = "Prices(Level " + PricePair.Item1.Level.ToString() + ")";
                    tmp.S5ValueToSendToExternal = PricePair.Item2.Price.ToString();
                    tmp.ExternalValueBeforeUpdate = PricePair.Item1.Price.ToString();
                    changes.Add(tmp);
                }
            }

            IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                .FullOuterJoin(LeftRecords: compareFrom.Quantities, RightRecords: compareTo.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
            foreach (var QtyPair in QtyPairs)
            {
                Tuple<S5QtyPair, S5QtyPair> tmpPair = FillMissing(QtyPair);
                if (tmpPair.Item1.Qty != tmpPair.Item2.Qty)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "Quantities(Dept " + tmpPair.Item1.Location.ToString() + ")";
                    tmp.S5ValueToSendToExternal = tmpPair.Item2.Qty.ToString();
                    tmp.ExternalValueBeforeUpdate = tmpPair.Item1.Qty.ToString();
                    changes.Add(tmp);
                }
            }

            //if (compareTo.WholesaleCost != compareFrom.WholesaleCost)
            //{
            //    Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
            //    tmp.FieldName = "WholesaleCost";
            //    tmp.S5ValueToSendToExternal = compareFrom.WholesaleCost.ToString();
            //    tmp.ExternalValueBeforeUpdate = compareTo.WholesaleCost.ToString();
            //    changes.Add(tmp);
            //}

            if (compareTo.WebCategory != compareFrom.WebCategory)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "WebCategory";
                tmp.S5ValueToSendToExternal = compareFrom.WebCategory;
                tmp.ExternalValueBeforeUpdate = compareTo.WebCategory;
                changes.Add(tmp);
            }

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Quantities compareFrom, IShopify_Quantities compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            IEnumerable<Tuple<S5QtyPair, S5QtyPair>> QtyPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                .FullOuterJoin(LeftRecords: compareTo.Quantities, RightRecords: compareFrom.Quantities, LeftKey: S5QtyPairIndexes.Department, RightKey: S5QtyPairIndexes.Department);
            int countInfo = QtyPairs.Count();
            foreach (Tuple<S5QtyPair, S5QtyPair> QtyPair in QtyPairs)
            {
                Tuple<S5QtyPair, S5QtyPair> tmpPair = FillMissing(QtyPair);
                if (tmpPair.Item1.Qty != tmpPair.Item2.Qty)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "Quantities(Dept " + tmpPair.Item1.Location.ToString() + ")";
                    tmp.S5ValueToSendToExternal = tmpPair.Item2.Qty.ToString();
                    tmp.ExternalValueBeforeUpdate = tmpPair.Item1.Qty.ToString();
                    changes.Add(tmp);
                }
                
            }

            return changes;
        }
        private static Tuple<S5PricePair, S5PricePair> FillMissing(Tuple<S5PricePair, S5PricePair> data)
        {
            if (data.Item1 == null && data.Item2 == null)
            {
                Tuple<S5PricePair, S5PricePair> tmpFillEmpty = new Tuple<S5PricePair, S5PricePair>(S5PricePair.Empty(0), S5PricePair.Empty(0));
                return tmpFillEmpty;
            }
            S5PricePair Left = data.Item1;
            S5PricePair Right = data.Item2;
            if (Left == null)
            {
                Left = S5PricePair.Empty(Right.Level);
            }
            if (Right == null)
            {
                Right = S5PricePair.Empty(Left.Level);
            }
            Tuple<S5PricePair, S5PricePair> tmpFill = new Tuple<S5PricePair, S5PricePair>(Left, Right);

            return tmpFill;
        }
        private static Tuple<S5QtyPair, S5QtyPair> FillMissing(Tuple<S5QtyPair, S5QtyPair> data)
        {
            if(data.Item1 == null && data.Item2 == null)
            {
                Tuple<S5QtyPair, S5QtyPair> tmpFillEmpty = new Tuple<S5QtyPair, S5QtyPair>(new S5QtyPair(1, 0), new S5QtyPair(1, 0));
                return tmpFillEmpty;
            }
            S5QtyPair Left = data.Item1;
            S5QtyPair Right = data.Item2;
            if (Left == null)
            {
                Left = S5QtyPair.Empty(Right.Location);
            } 
            if (Right == null)
            {
                Right = S5QtyPair.Empty(Left.Location);
            }
            Tuple<S5QtyPair, S5QtyPair> tmpFill = new Tuple<S5QtyPair, S5QtyPair>(Left, Right);

            return tmpFill;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Prices compareFrom, IShopify_Prices compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                .FullOuterJoin(LeftRecords: compareFrom.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
            foreach (var PricePair in PricePairs)
            {
                if (PricePair.Item1 == null || PricePair.Item2 == null)
                {
                    Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);

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
                        Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
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
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "Description";
                tmp.S5ValueToSendToExternal = compareFrom.Description;
                tmp.ExternalValueBeforeUpdate = compareTo.Description;
                changes.Add(tmp);
            }

            if (compareTo.WebCategory != compareFrom.WebCategory)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "WebCategory";
                tmp.S5ValueToSendToExternal = compareFrom.WebCategory;
                tmp.ExternalValueBeforeUpdate = compareTo.WebCategory;
                changes.Add(tmp);
            }

            return changes;
        }
        public static IEnumerable<Changes_View> Diff(this IShopify_Identity compareFrom, IShopify_Identity compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();
            if (compareTo.InvUnique != compareFrom.InvUnique)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "InvUnique";
                tmp.S5ValueToSendToExternal = compareFrom.InvUnique.ToString();
                tmp.ExternalValueBeforeUpdate = compareTo.InvUnique.ToString();
                changes.Add(tmp);
            }
            if (compareTo.Cat != compareFrom.Cat)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
                tmp.FieldName = "Cat";
                tmp.S5ValueToSendToExternal = compareFrom.Cat;
                tmp.ExternalValueBeforeUpdate = compareTo.Cat;
                changes.Add(tmp);
            }
            if (compareTo.PartNumber != compareFrom.PartNumber)
            {
                Changes_View tmp = GetNewChanges_View_WithDefaults(compareFrom);
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
                    && compareTo.WebCategory == compareFrom.WebCategory
                    //&& compareTo.WholesaleCost == compareFrom.WholesaleCost
                )
                {
                    IEnumerable<Tuple<S5PricePair, S5PricePair>> PricePairs = GenericJoins<S5PricePair, S5PricePair, short>
                        .FullOuterJoin(LeftRecords: compareFrom.Prices, RightRecords: compareTo.Prices, LeftKey: S5PricePairIndexes.Level, RightKey: S5PricePairIndexes.Level);
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
