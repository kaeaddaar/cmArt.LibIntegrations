using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector.Data
{
    public static class IProduct_Root_Indexes
    {
        public static int UniqueId (this IProduct_Root data)
        {
            IProduct_Root _data = data ?? new Product_Root();
            int unique = 0;
            int.TryParse(_data.sku, out unique);
            return unique;
        }

    }
}
