using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class IInventry_27Extensions_ForCopyFrom
    {
        public static void CopyFrom(this IInventry_27 to, IInventry_27 from)
        {
            to.Brand = from.Brand;
            to.Cat = from.Cat;
            to.Country = from.Country;
            to.Description = from.Description;
            to.Description2 = from.Description2;
            to.Duty_1 = from.Duty_1;
            to.Ecommerce = from.Ecommerce;
            to.Foreign_1 = from.Foreign_1;
            to.Freight_1 = from.Freight_1;
            to.InvUnique = from.InvUnique;
            to.Item = from.Item;
            to.KitType = from.KitType;
            to.Location = from.Location;
            to.MarkDeleted = from.MarkDeleted;
            to.PackSize = from.PackSize;
            to.Part = from.Part;
            to.PriceQty = from.PriceQty;
            to.Serial = from.Serial;
            to.Size_1 = from.Size_1;
            to.Size_2 = from.Size_2;
            to.Size_3 = from.Size_3;
            to.Supplier = from.Supplier;
            to.SuppPart = from.SuppPart;
            to.Tax = from.Tax;
            to.Units = from.Units;
            to.Weight = from.Weight;
            to.WholeExtra_1 = from.WholeExtra_1;
            to.Wholesale_1 = from.Wholesale_1;
        }
    }
}
