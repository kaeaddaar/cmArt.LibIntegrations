using cmArt.LibIntegrations.SerializationService;
using cmArt.Shopify.App.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using cmArt.LibIntegrations.VennMapService;
using cmArt.Reece.ShopifyConnector;
using cmArt.System5.Inventory;

namespace cmArt.Shopify.App
{
    public static class Reports
    {
        public static void SaveReport(IEnumerable<AdaptToShopifyDataLoadFormat> adapters, StaticSettings settings, ILogger logger)
        {
            string TableName = "AdaptToShopifyDataLoadFormat";
            logger.LogInformation($"Saving {TableName}");
            try
            {
                GenericSerialization<AdaptToShopifyDataLoadFormat>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, TableName);
                GenericSerialization<AdaptToShopifyDataLoadFormat>.SerializeToJSON(adapters.ToList(), TableName, settings.OutputDirectory, 50000);
            }
            catch
            {
                logger.LogInformation($"Failed to serialize {TableName}. {TableName} file(s) will not be written.");
            }

        }
        public static void SaveReport(VennMap<Shopify_Product, int> map, StaticSettings settings, ILogger logger)
        {
            if (map == null) { throw new ArgumentNullException("the map argument in the method call to SaveReport must not be null"); }
            VennMap<Shopify_Product, int> _map = map;

            SerializeVennMapResult("VenMapp_InvOnly_Ecomm", map.InvOnly_Ecomm, settings, logger);
            SerializeVennMapResult("VenMapp_InvOnly_NoEcomm", map.InvOnly_NoEcomm, settings, logger);
            SerializeVennMapResult("VenMapp_Both_Ecomm", map.Both_Ecomm, settings, logger);
            SerializeVennMapResult("VenMapp_Both_NoEcomm", map.Both_NoEcomm, settings, logger);
            SerializeVennMapResult("VenMapp_TOnly", map.TOnly, settings, logger);

        }
        private static void SerializeVennMapResult
        (
            string TableName
            , IEnumerable<(Shopify_Product, System5.Inventory.S5InvAssembledObj)> VennMapResult
            , StaticSettings settings
            , ILogger logger
        )
        {
            string tmpTableName = TableName;
            logger.LogInformation($"Saving {tmpTableName}");
            try
            {
                GenericSerialization<(Shopify_Product, S5InvAssembledObj)>
                    .RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, tmpTableName);
                GenericSerialization<(Shopify_Product, S5InvAssembledObj)>
                    .SerializeToJSON(VennMapResult.ToList(), tmpTableName, settings.OutputDirectory, 50000);
            }
            catch
            {
                logger.LogInformation($"Failed to serialize {tmpTableName}. {tmpTableName} file(s) will not be written.");
            }
        }
    }
}
