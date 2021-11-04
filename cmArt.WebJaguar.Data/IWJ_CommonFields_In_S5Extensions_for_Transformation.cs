using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public static class IWJ_CommonFields_In_S5Extensions_for_Transformation
    {
        public static IWJ_CommonFields_In_S5 CopyFrom(this IWJ_CommonFields_In_S5 to, IWJ_CommonFields_In_S5 from)
        {
            IWJ_CommonFields_In_S5 _from = from ?? new WJ_CommonFields();

            to.longDesc = _from.longDesc;
            to.name = _from.name;
            to.shortDesc = _from.shortDesc;
            to.sku = _from.sku;
            to.upc = _from.upc;
            to.weight = _from.weight;

            return to;
        }
    }
}
