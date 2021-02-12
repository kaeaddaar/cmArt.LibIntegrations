using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class IStokExtensions_ForCopyFrom
    {
        public static void CopyFrom(this IStok to, IStok from)
        {
            to.BillPtr = from.BillPtr;
            to.CheckPtr = from.CheckPtr;
            to.Costed = from.Costed;
            to.CostQuantity = from.CostQuantity;
            to.CostStatus = from.CostStatus;
            to.Country = from.Country;
            to.CurrencyCode = from.CurrencyCode;
            to.Date = from.Date;
            to.Department = from.Department;
            to.Description = from.Description;
            to.Duty = from.Duty;
            to.ExpiryDate = from.ExpiryDate;
            to.Foreign = from.Foreign;
            to.Freight = from.Freight;
            to.HeaderPtr = from.HeaderPtr;
            to.LocationPtr = from.LocationPtr;
            to.Number = from.Number;
            to.PartPtr = from.PartPtr;
            to.PickPriority = from.PickPriority;
            to.PickQuantity = from.PickQuantity;
            to.PriceQty = from.PriceQty;
            to.ProductPtr = from.ProductPtr;
            to.Proximity = from.Proximity;
            to.StockStatus = from.StockStatus;
            to.StUnique = from.StUnique;
            to.SupplierPtr = from.SupplierPtr;
            to.TrandataPtr = from.TrandataPtr;
            to.Weight = from.Weight;
            to.WholeExtra = from.WholeExtra;
            to.Wholesale = from.Wholesale;
        }
    }
}
