using cmArt.Reece.ShopifyConnector;
using cmArt.Reece.ShopifyConnector.Data;


namespace cmArt.Portal.Data.OnlineInventory
{
    public interface IWebInventory : IShopifyDataLoadFormat_POCO
    {
        string ImageLocation { get; set; }

        void Init(ShopifyDataLoadFormat data);
    }
}