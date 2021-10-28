namespace cmArt.Shopify.App.ReportViews
{
    public interface IShopify_Quantities_Pair_Flat
    {
        string LeftCat { get; set; }
        string LeftQuantities { get; set; }
        int LeftInvUnique { get; set; }
        string LeftPartNumber { get; set; }
        string RightCat { get; set; }
        string RightQuantities { get; set; }
        int RightInvUnique { get; set; }
        string RightPartNumber { get; set; }
    }
}