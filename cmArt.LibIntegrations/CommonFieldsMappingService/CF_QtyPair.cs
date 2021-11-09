using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.CommonFieldsMappingService
{

    public class CF_QtyPair
    {
        public CF_QtyPair(short Location, decimal Qty)
        {
            this.Location = Location;
            this.Qty = Qty;
        }
        public short Location { get; set; }
        public decimal Qty { get; set; }

    }
}
