using cmArt.Reece.ShopifyConnector;
using cmArt.Reece.ShopifyConnector.Data;


namespace cmArt.Portal.Data6
{
    public interface IWebInventory : IShopifyDataLoadFormat_POCO
    {
        string ImageUrl { get; set; }

        void Init(ShopifyDataLoadFormat data);

    }
}