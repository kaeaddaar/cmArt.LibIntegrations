namespace cmArt.Shopify.App.ReportViews
{
    public interface IShopify_Quantities_Pair_Flat1
    {
        string LeftCat { get; set; }
        int LeftInvUnique { get; set; }
        string LeftPartNumber { get; set; }
        string LeftQuantities { get; set; }
        string RightCat { get; set; }
        int RightInvUnique { get; set; }
        string RightPartNumber { get; set; }
        string RightQuantities { get; set; }
    }
}