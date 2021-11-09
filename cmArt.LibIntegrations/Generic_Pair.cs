using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations
{
    public class Generic_Pair<TCommon> where TCommon : new()
    {
        public TCommon S5 { get; set; }
        public TCommon Shopify { get; set; }
        public Generic_Pair()
        {
            S5 = S5 ?? new TCommon();
            Shopify = Shopify ?? new TCommon();
        }
        public Generic_Pair(TCommon S5In, TCommon ShopifyIn)
        {
            S5 = S5In ?? new TCommon();
            Shopify = ShopifyIn ?? new TCommon();
        }
    }

}
