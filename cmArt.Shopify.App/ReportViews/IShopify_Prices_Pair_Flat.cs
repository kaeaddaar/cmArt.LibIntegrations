namespace cmArt.Shopify.App.ReportViews
{
    public interface IShopify_Prices_Pair_Flat
    {
        string LeftCat { get; set; }
        int LeftInvUnique { get; set; }
        string LeftPartNumber { get; set; }
        string LeftPrices { get; set; }
        string RightCat { get; set; }
        int RightInvUnique { get; set; }
        string RightPartNumber { get; set; }
        string RightPrices { get; set; }
    }
}