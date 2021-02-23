using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class DataLoadFormat : ICommonFields, IDataLoadFormat
    {
        private ICommonFields _CommonFields;
        public int InvUnique { get; set; }
        public string Cat { get; set; }
        public string PartNumber { get; set; }
        public string SupplierName { get => _CommonFields.SupplierName; set => _CommonFields.SupplierName = value; }
        public string SupplierPartNumber { get => _CommonFields.SupplierPartNumber; set => _CommonFields.SupplierPartNumber = value; }
        public string SupplierCode { get => _CommonFields.SupplierCode; set => _CommonFields.SupplierCode = value; }
        public decimal WholesaleCost { get => _CommonFields.WholesaleCost; set => _CommonFields.WholesaleCost = value; }
        public decimal PriceSchedule1_MSRP { get => _CommonFields.PriceSchedule1_MSRP; set => _CommonFields.PriceSchedule1_MSRP = value; }
        public decimal PriceSchedule2_MinPrice { get => _CommonFields.PriceSchedule2_MinPrice; set => _CommonFields.PriceSchedule2_MinPrice = value; }
    }
}
