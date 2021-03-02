using System;
using System.Collections.Generic;
using System.Text;
using cmArt.LibIntegrations;
using FileHelpers;


namespace cmArt.BevNet
{
    [DelimitedRecord(",")]
    public class DataLoadFormat : ICommonFields, IDataLoadFormat, ICopyable<IDataLoadFormat>
    {
        public IDataLoadFormat CopyFrom(IDataLoadFormat IFrom)
        {
            this.Cat = IFrom.Cat;
            this.InvUnique = IFrom.InvUnique;
            this.PartNumber = IFrom.PartNumber;
            this.PriceSchedule1_MSRP = IFrom.PriceSchedule1_MSRP;
            this.PriceSchedule2_MinPrice = IFrom.PriceSchedule2_MinPrice;
            this.SupplierCode = IFrom.SupplierCode;
            this.SupplierName = IFrom.SupplierName;
            this.SupplierPartNumber = IFrom.SupplierPartNumber;
            this.WholesaleCost = IFrom.WholesaleCost;
            return this;
        }

        public int InvUnique { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Cat { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string PartNumber { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string SupplierName { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string SupplierPartNumber { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string SupplierCode { get; set; }
        public decimal WholesaleCost { get; set; }
        public decimal PriceSchedule1_MSRP { get; set; }
        public decimal PriceSchedule2_MinPrice { get; set; }
        public decimal S5Orig_WholesaleCost { get; set; }
        public decimal S5Orig_ListPrice { get; set; }
        public decimal S5Orig_MinPrice { get; set; }
        public decimal Change_WholesaleCost { get; set; }
        public decimal Change_ListPrice { get; set; }
        public decimal Change_MinPrice { get; set; }
    }
}
