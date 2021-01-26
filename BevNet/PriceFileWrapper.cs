using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class PriceFileWrapper : ICommonFields
    {
        private IPriceFile _PriceFile;
        public PriceFileWrapper(IPriceFile PriceFile)
        {
            _PriceFile = PriceFile ?? (IPriceFile)(new PriceFile());
        }
    }
}
