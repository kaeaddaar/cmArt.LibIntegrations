using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public static class IS5_CommonFields_InWJExtensions_for_Transformation
    {
        public static bool cmQtyEquals(this IS5_CommonFields_In_WJ data, IS5_CommonFields_In_WJ compareTo)
        {

            if (data == null || compareTo == null) { return false; }

            if (data.InvUnique != compareTo.InvUnique) { return false; }

            decimal dataQty;
            decimal compareToQty;
            dataQty = data.Quantities.Sum(x => x.Qty);
            compareToQty = compareTo.Quantities.Sum(x => x.Qty);
            if (dataQty != compareToQty) { return false; }

            return true;
        }

        public static bool cmEquals(this IS5_CommonFields_In_WJ data, IS5_CommonFields_In_WJ compareTo)
        {

            if (data == null || compareTo == null) { return false; }

            Func<string, string> keyBarcodes = (x) => x;
            var BarcodePairs = GenericJoins<string, string, string>
                .FullOuterJoin(data.barcodes, compareTo.barcodes, keyBarcodes, keyBarcodes);
            foreach (var pair in BarcodePairs)
            {
                if (pair.Item1 == null || pair.Item2 == null) { return false; }
                if (pair.Item1 != pair.Item2) { return false; }
            }

            if (data.Description.Length > 0 && compareTo.Description.TrimEnd().Length > data.Description.TrimEnd().Length)
            {
                if (data.Description.TrimEnd() != compareTo.Description.Substring(0, data.Description.Length)) { return false; }
            }
            else
            {
                if (data.Description.TrimEnd() != compareTo.Description.TrimEnd()) { return false; }
            }
            if (data.InvUnique != compareTo.InvUnique) { return false; }
            if (data.PartNumber.TrimEnd() != compareTo.PartNumber.TrimEnd()) { return false; }
            if (data.WebDescription.TrimEnd() != compareTo.WebDescription.TrimEnd()) { return false; }
            if (data.weight != compareTo.weight) { return false; }
            if (data.Cat != compareTo.Cat) { return false; }
            if (data.PackSize != compareTo.PackSize) { return false; }
            if (data.Size_1 != compareTo.Size_1) { return false; }
            if (data.Size_2 != compareTo.Size_2) { return false; }
            if (data.Size_3 != compareTo.Size_3) { return false; }
            if (data.Units != compareTo.Units) { return false; }

            if (data.FF22 == compareTo.FF22) { return false; }
            if (data.PartNumber == compareTo.PartNumber) { return false; }

            Func<S5PricePair, short> keyPrices = (x) => x.Level;
            var PricesPairs = GenericJoins<S5PricePair, S5PricePair, short>
                .FullOuterJoin(data.Prices, compareTo.Prices, keyPrices, keyPrices);
            foreach (var pair in PricesPairs)
            {
                if (pair.Item1 == null || pair.Item2 == null) { return false; }
                if (pair.Item1.Price != pair.Item2.Price) { return false; }
            }

            Func<S5QtyPair, short> keyQuantities = (x) => x.Location;
            var QuantitiesPairs = GenericJoins<S5QtyPair, S5QtyPair, short>
                .FullOuterJoin(data.Quantities, compareTo.Quantities, keyQuantities, keyQuantities);
            foreach (var pair in QuantitiesPairs)
            {
                if (pair.Item1 == null || pair.Item2 == null) { return false; }
                if (pair.Item1.Qty != pair.Item2.Qty) { return false; }
            }

            return true;
        }
        public static S5_CommonFields AsS5_CommonFields(this IS5_CommonFields_In_WJ data)
        {
            IS5_CommonFields_In_WJ _data = data ?? new S5_CommonFields();
            S5_CommonFields result = new S5_CommonFields();

            result.barcodes = new List<string>(_data.barcodes);
            result.Cat = _data.Cat;
            result.Description = _data.Description;
            result.FF22 = _data.FF22;
            result.InvUnique = _data.InvUnique;
            result.PackSize = _data.PackSize;
            result.PartNumber = _data.PartNumber;
            result.Prices = _data.Prices;
            result.Quantities = _data.Quantities;
            result.Size_1 = _data.Size_1;
            result.Size_2 = _data.Size_2;
            result.Size_3 = _data.Size_3;
            result.Units = _data.Units;
            result.WebDescription = _data.WebDescription;
            result.weight = _data.weight;
            result.WholesaleCost = _data.WholesaleCost;


            return result;
        }
        public static IS5_CommonFields_In_WJ CopyFrom(this IS5_CommonFields_In_WJ to, IS5_CommonFields_In_WJ from)
        {
            to.barcodes = new List<string>(from.barcodes);
            to.Cat = from.Cat;
            to.Description = from.Description;
            to.FF22 = from.FF22;
            to.InvUnique = from.InvUnique;
            to.PackSize = from.PackSize;
            to.PartNumber = from.PartNumber;
            to.Prices = from.Prices;
            to.Quantities = from.Quantities;
            to.Size_1 = from.Size_1;
            to.Size_2 = from.Size_2;
            to.Size_3 = from.Size_3;
            to.Units = from.Units;
            to.WebDescription = from.WebDescription;
            to.weight = from.weight;
            to.WholesaleCost = from.WholesaleCost;

            return to;
        }
        public static Product_Root AsProduct_Root(this IS5_CommonFields_In_WJ data)
        {
            IS5_CommonFields_In_WJ _data = data ?? new S5_CommonFields();
            Product_Root_Common result = new Product_Root_Common();
            result.SetNotEditableDefaults();

            adapterWJ_from_S5 adapter = new adapterWJ_from_S5();
            adapter.Init(_data);
            result.CopyFrom(adapter);

            Product_Root finalResult = (Product_Root)result;
            return finalResult;
        }
    }
}
