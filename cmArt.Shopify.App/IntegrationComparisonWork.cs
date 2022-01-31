using cmArt.LibIntegrations;
using cmArt.LibIntegrations.VennMapService;
using cmArt.Reece.ShopifyConnector;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App
{
    public class IntegrationComparisonWork
    {
        public IEnumerable<IShopify_Product> ChangedRecords_Product { get; set; }
        public IEnumerable<IShopify_Prices> ChangedRecords_Prices { get; set; }
        public IEnumerable<IShopify_Quantities> ChangedRecords_Quantities { get; set; }

        public Func<IShopify_Prices, IShopify_Prices, bool> fEquals_Prices;
        public Func<IShopify_Quantities, IShopify_Quantities, bool> fEquals_Quantities;
        public Func<IShopify_Product, IShopify_Product, bool> fEquals_Product;

        public IntegrationComparisonWork()
        {
            fEquals_Product = (from, to) => { return IShopify_ProductExtensions.Equals(from, to); };
            fEquals_Prices = (from, to) => { return IShopify_PricesExtensions.Equals(from, to); };
            fEquals_Quantities = (from, to) => { return IShopify_QuantitiesExtensions.Equals(from, to); };
        }
        public void GetChangedRecords
        (
            IEnumerable<IShopify_Product> S5Products
            , IEnumerable<IShopify_Product> ShopifyProducts
            , IEnumerable<IShopify_Prices> S5Prices
            , IEnumerable<IShopify_Prices> ShopifyPrices
            , IEnumerable<IShopify_Quantities> S5Quantities
            , IEnumerable<IShopify_Quantities> ShopifyQuantities
            , ILogger logger
        )
        {
            logger.LogInformation("GetChangedRecords()");

            IEnumerable<IShopify_Product> ChangedRecords_Product = UpdateProcessPattern<IShopify_Product, Shopify_Product, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Product, S5Products, ShopifyProducts);

            IEnumerable<IShopify_Prices> ChangedRecords_Prices = UpdateProcessPattern<IShopify_Prices, Shopify_Prices, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Prices, S5Prices, ShopifyPrices);

            IEnumerable<IShopify_Quantities> ChangedRecords_Quantities = UpdateProcessPattern<IShopify_Quantities, Shopify_Quantities, int>
                .GetChangedRecords(IShopifyDataLoadFormat_Indexes.UniqueId, fEquals_Quantities, S5Quantities, ShopifyQuantities);
        }
    }
}
