﻿using cmArt.LibIntegrations.GenericJoinsService;
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

            if (data.FF22 == compareTo.FF22) { return false; }
            if (data.PartNumber == compareTo.PartNumber) { return false; }
            if (data.WholesaleCost == compareTo.WholesaleCost) { return false; }

            throw new NotImplementedException("The Two Collections Below Aren't Implemented correctly yet as the S5QtyPair comparison needs " +
                "to happen based on level. Might have it right now but need to test with a fresh mind.");

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
            result.Description = _data.Description;
            result.InvUnique = _data.InvUnique;
            result.PartNumber = _data.PartNumber;
            result.WebDescription = _data.WebDescription;
            result.weight = _data.weight;
            result.Cat = _data.Cat;

            result.Prices = _data.Prices;
            result.FF22 = _data.FF22;
            result.PartNumber = _data.PartNumber;
            result.Quantities = _data.Quantities;
            result.WholesaleCost = _data.WholesaleCost;

            return result;
        }
        public static IS5_CommonFields_In_WJ CopyFrom(this IS5_CommonFields_In_WJ to, IS5_CommonFields_In_WJ from)
        {
            to.barcodes = new List<string>(from.barcodes);
            to.Cat = from.Cat;
            to.Description = from.Description;
            to.InvUnique = from.InvUnique;
            to.PartNumber = from.PartNumber;
            to.WebDescription = from.WebDescription;
            to.weight = from.weight;

            to.Prices = from.Prices;
            to.FF22 = from.FF22;
            to.PartNumber = from.PartNumber;
            to.Quantities = from.Quantities;
            to.WholesaleCost = from.WholesaleCost;

            return to;
        }
        public static Product_Root AsProduct_Root(this IS5_CommonFields_In_WJ data)
        {
            IS5_CommonFields_In_WJ _data = data ?? new S5_CommonFields();
            Product_Root_Common result = new Product_Root_Common();
            result.SetNotEditableDefaults();

            IWJ_CommonFields_In_S5 result_updater = (IWJ_CommonFields_In_S5)result;
            adapterS5_from_WJ adapter = new adapterS5_from_WJ();
            adapter.init(result_updater);
            adapter.CopyFrom(_data);
            Product_Root finalResult = (Product_Root)result;
            return finalResult;
        }
    }
}
