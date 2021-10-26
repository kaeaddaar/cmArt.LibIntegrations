using cmArt.Reece.ShopifyConnector;

namespace cmArt.Shopify.App.ReportViews
{
    public interface IShopify_Product_Pair
    {
        Shopify_Product S5 { get; set; }
        Shopify_Product Shopify { get; set; }
    }
}