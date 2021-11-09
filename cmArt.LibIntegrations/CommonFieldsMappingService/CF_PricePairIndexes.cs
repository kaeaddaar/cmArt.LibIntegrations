using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.CommonFieldsMappingService
{
    public static class CF_PricePairIndexes
    {
        public static short Level(CF_PricePair data)
        {
            if (data == null) { return -1; }
            return data.Level;
        }
    }

}
