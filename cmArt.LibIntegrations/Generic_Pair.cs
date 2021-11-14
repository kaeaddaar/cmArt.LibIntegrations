using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations
{
    public class Generic_Pair<TCommon> where TCommon : new()
    {
        public TCommon S5 { get; set; }
        public TCommon External { get; set; }
        public Generic_Pair()
        {
            S5 = S5 ?? new TCommon();
            External = External ?? new TCommon();
        }
        public Generic_Pair(TCommon S5In, TCommon ExternalIn)
        {
            S5 = S5In ?? new TCommon();
            External = ExternalIn ?? new TCommon();
        }
    }

}
