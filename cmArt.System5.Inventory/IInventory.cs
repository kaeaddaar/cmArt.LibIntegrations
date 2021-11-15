using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public interface IInventory : IInventoryId, IEquality<IInventory>
    {
        string Description { get; set; }
        
        IInventory CopyFrom(IInventory IFrom);

    }

}
