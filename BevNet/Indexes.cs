using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public static class Indexes
    {
        public static ValueTuple<string,string> SupplierCode_SupplierPartNum_Key(IPriceFile priceFile)
        {
            return new ValueTuple<string, string>(priceFile.WHOLESALER, priceFile.PROD_ITEM);
        }


    }

}
