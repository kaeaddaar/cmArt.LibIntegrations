using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public interface IInventoryId
    {
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }

    }
}
