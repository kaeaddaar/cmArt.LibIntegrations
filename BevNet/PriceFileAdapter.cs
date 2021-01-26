using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class PriceFileAdapter : ICommonFields
    {
        private IPriceFile _PriceFile;
        public PriceFileAdapter(IPriceFile PriceFile)
        {
            _PriceFile = PriceFile ?? (IPriceFile)(new PriceFile());
        }
    }
}
