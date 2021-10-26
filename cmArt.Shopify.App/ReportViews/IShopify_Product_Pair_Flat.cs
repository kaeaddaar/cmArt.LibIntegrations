namespace cmArt.Shopify.App.ReportViews
{
    public interface IShopify_Product_Pair_Flat
    {
        string LeftCat { get; set; }
        string LeftDescription { get; set; }
        int LeftInvUnique { get; set; }
        string LeftPartNumber { get; set; }
        string RightCat { get; set; }
        string RightDescription { get; set; }
        int RightInvUnique { get; set; }
        string RightPartNumber { get; set; }
    }
}