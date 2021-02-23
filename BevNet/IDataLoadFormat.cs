namespace cmArt.BevNet
{
    public interface IDataLoadFormat
    {
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }
        decimal PriceSchedule1_MSRP { get; set; }
        decimal PriceSchedule2_MinPrice { get; set; }
        string SupplierCode { get; set; }
        string SupplierName { get; set; }
        string SupplierPartNumber { get; set; }
        decimal WholesaleCost { get; set; }
    }
}