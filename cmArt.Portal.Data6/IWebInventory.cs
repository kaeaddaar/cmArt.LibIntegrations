using cmArt.LibIntegrations;
using cmArt.Reece.ShopifyConnector;
using cmArt.Reece.ShopifyConnector.Data;
using cmArt.Portal;

namespace cmArt.Portal.Data6
{
    public interface IWebInventory : IShopifyDataLoadFormat_POCO
    {
        string ImageUrl { get; set; }

        void Init(ShopifyDataLoadFormat data);

    }
}