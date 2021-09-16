using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace cmArt.System5.Inventory
{
    public interface IS5InvAssembled
    {
        IEnumerable<IAltSuply> AltSuplies_PerInventry_27 { get; }
        IEnumerable<IComments> CommentsLines_PerInventry_27 { get; }
        IInventry_27 Inv { get; }
        IEnumerable<IInvPrice> InvPrices_PerInventry_27 { get; }
        IEnumerable<IStok> StokLines_PerInventry_27 { get; }
    }

}
