using cmArt.Reece.ShopifyConnector;
using cmArt.Reece.ShopifyConnector.Data;


namespace cmArt.Portal.Data.OnlineInventory
{
    public interface IPortalInventory : IShopifyDataLoadFormat_POCO
    {
        string ImageLocation { get; set; }

        void Init(ShopifyDataLoadFormat data);
    }
}