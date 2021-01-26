using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class PriceFileUpdater : IPriceFile
    {
        private IPriceFile _PriceFile;
        public PriceFileUpdater(IPriceFile PriceFile)
        {
            _PriceFile = PriceFile ?? (IPriceFile)(new PriceFile());
        }
        public void UpdateUsing(ICommonFields CommonFields)
        {
            // for each field set value of _PriceFile from CommonFields

        }

    }
}
