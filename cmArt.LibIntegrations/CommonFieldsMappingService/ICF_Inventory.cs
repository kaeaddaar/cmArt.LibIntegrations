using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.CommonFieldsMappingService
{
    public interface ICF_Inventory
    {
        #region Identity
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        #endregion Identity
        string Description { get; set; }
        decimal WholesaleCost { get; set; }
        IEnumerable<CF_PricePair> Prices { get; set; }
        IEnumerable<CF_QtyPair> Quantities { get; set; }


    }
}
