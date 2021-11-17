using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.LibIntegrations.ReportService;
using cmArt.Reece.ShopifyConnector;
using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.ReportViews
{
    public static class IS5_CommonFields_In_WJExtensions_for_differences
    {
        public static IEnumerable<Changes_View> Diff(this IS5_CommonFields_In_WJ compareFrom, IS5_CommonFields_In_WJ compareTo)
        {
            List<Changes_View> changes = new List<Changes_View>();

            Func<string, string> fBarcodeIndex = (x) => x;
            IEnumerable<Tuple<string, string>> barcodePairs = GenericJoins<string, string, string>
                .FullOuterJoin(LeftRecords: compareTo.barcodes, RightRecords: compareTo.barcodes
                    , LeftKey: fBarcodeIndex, RightKey: fBarcodeIndex);
            foreach (var barcodePair in barcodePairs)
            {
                if (barcodePair.Item1 != barcodePair.Item2)
                {
                    Changes_View tmp = new Changes_View();
                    tmp.InvUnique = compareFrom.InvUnique;
                    tmp.Cat = compareFrom.Cat;
                    tmp.PartNumber = compareFrom.PartNumber;
                    tmp.FieldName = "barcode";
                    tmp.S5ValueToSendToExternal = barcodePair.Item2;
                    tmp.ExternalValueBeforeUpdate = barcodePair.Item1;
                    changes.Add(tmp);
                }
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

            if (compareTo.FF22 != compareFrom.FF22)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "FF22 (WJ Cat IDs)";
                tmp.S5ValueToSendToExternal = compareFrom.FF22;
                tmp.ExternalValueBeforeUpdate = compareTo.FF22;
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

            if (compareTo.WebDescription != compareFrom.WebDescription)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "WebDescription";
                tmp.S5ValueToSendToExternal = compareFrom.WebDescription;
                tmp.ExternalValueBeforeUpdate = compareTo.WebDescription;
                changes.Add(tmp);
            }

            if (compareTo.weight != compareFrom.weight)
            {
                Changes_View tmp = new Changes_View();
                tmp.InvUnique = compareFrom.InvUnique;
                tmp.Cat = compareFrom.Cat;
                tmp.PartNumber = compareFrom.PartNumber;
                tmp.FieldName = "weight";
                tmp.S5ValueToSendToExternal = compareFrom.weight.ToString();
                tmp.ExternalValueBeforeUpdate = compareTo.weight.ToString();
                changes.Add(tmp);
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

    }

}
