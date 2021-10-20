using cmArt.WebJaguar.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.Data
{
    public static class IProduct_RootExtensions_ForTransformation
    {
        public static WJ_CommonFields AsWJ_CommonFields(this IProduct_Root data)
        {
            IProduct_Root _data = data ?? new Product_Root();
            WJ_CommonFields result = new WJ_CommonFields();
            IWJ_CommonFields_In_S5 tmp = (IWJ_CommonFields_In_S5)_data;
            result.CopyFrom(tmp);
            return result;
        }
    }
}
