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
        [FieldHidden]
        private ICommonFields _CommonFields;
        public DataLoadFormat()
        {
            _CommonFields = new CommonFields();
        }
        public void init(ICommonFields CommonFields)
        {
            _CommonFields = CommonFields ?? new CommonFields();
        }
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
        public string SupplierName { get => _CommonFields.SupplierName; set => _CommonFields.SupplierName = value; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string SupplierPartNumber { get => _CommonFields.SupplierPartNumber; set => _CommonFields.SupplierPartNumber = value; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string SupplierCode { get => _CommonFields.SupplierCode; set => _CommonFields.SupplierCode = value; }
        public decimal WholesaleCost { get => _CommonFields.WholesaleCost; set => _CommonFields.WholesaleCost = value; }
        public decimal PriceSchedule1_MSRP { get => _CommonFields.PriceSchedule1_MSRP; set => _CommonFields.PriceSchedule1_MSRP = value; }
        public decimal PriceSchedule2_MinPrice { get => _CommonFields.PriceSchedule2_MinPrice; set => _CommonFields.PriceSchedule2_MinPrice = value; }
    }
}
