using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class Stok_Indexes
    {
        public static int InventoryUnique(IStok record)
        {
            try
            {
                return (int)record.PartPtr;
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Part Pointer caused an OverflowException in Stok trying to " +
                    $"convert PartPtr from long to int. StUnique = {record.StUnique}, PartPt = {record.PartPtr}");
            }
        }
    }

}
