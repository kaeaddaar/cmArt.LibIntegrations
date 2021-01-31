using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public static class Indexes
    {
        public static (string SupplierCode, string SupplierPart) SupplierCode_SupplierPartNum_Key(IPriceFile priceFile)
        {
            return new ValueTuple<string, string>(priceFile.WHOLESALER, priceFile.PROD_ITEM);
        }


    }

}
