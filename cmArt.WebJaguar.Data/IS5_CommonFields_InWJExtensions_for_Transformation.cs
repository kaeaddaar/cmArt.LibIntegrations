using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.WebJaguar.Connector;
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
            
            Func<string, string> key = (x) => x;
            var BarcodePairs = GenericJoins<string, string, string>
                .FullOuterJoin(data.barcodes, compareTo.barcodes, key, key);
            foreach (var pair in BarcodePairs)
            {
                if (pair.Item1 == null || pair.Item2 == null) { return false; }
                if (pair.Item1 != pair.Item2) { return false; }
            }

            if (data.Description.TrimEnd() != compareTo.Description.TrimEnd()) { return false; }
            if (data.InvUnique != compareTo.InvUnique) { return false; }
            if (data.PartNumber.TrimEnd() != compareTo.PartNumber.TrimEnd()) { return false; }
            if (data.WebDescription.TrimEnd() != compareTo.WebDescription.TrimEnd()) { return false; }
            if (data.weight != compareTo.weight) { return false; }

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

            return result;
        }
        public static IS5_CommonFields_In_WJ CopyFrom(this IS5_CommonFields_In_WJ to, IS5_CommonFields_In_WJ from)
        {
            to.barcodes = new List<string>(from.barcodes);
            to.Description = from.Description;
            to.InvUnique = from.InvUnique;
            to.PartNumber = from.PartNumber;
            to.WebDescription = from.WebDescription;
            to.weight = from.weight;
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
