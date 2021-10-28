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
using cmArt.Shopify.App.ReportViews;
using FileHelpers;

namespace cmArt.Shopify.App
{
    public static class Reports
    {
        public static void SaveReport
        (
            IEnumerable<IShopify_Quantities> data
            , string TableName
            , StaticSettings settings
            , ILogger logger
        )
        {
            IEnumerable<IShopify_Quantities> _data = data ?? new List<IShopify_Quantities>();

            Func<IShopify_Quantities, Shopify_Quantities_Pair_Flat> Transform =
            (IShopData) =>
            {
                ; Shopify_Quantities tmp = IShopData.AsShopify_Quantities();
                Generic_Pair<Shopify_Quantities> tmpSP = new Generic_Pair<Shopify_Quantities>(new Shopify_Quantities(), tmp);
                Shopify_Quantities_Pair_Adapter tmpFlatAdapter = new Shopify_Quantities_Pair_Adapter(tmpSP);
                return tmpFlatAdapter.AsShopify_Quantities_Pair_Flat();
            };

            IEnumerable<Shopify_Quantities_Pair_Flat> flatData = _data.Select(d =>
            {
                return Transform(d);
            });
            SaveReport(flatData, TableName, settings, logger);
        }
        public static void SaveReport
        (
            IEnumerable<IShopify_Prices> data
            , string TableName
            , StaticSettings settings
            , ILogger logger
        )
        {
            IEnumerable<IShopify_Prices> _data = data ?? new List<IShopify_Prices>();

            Func<IShopify_Prices, Shopify_Prices_Pair_Flat> Transform =
            (IShopData) =>
            {
                ; Shopify_Prices tmp = IShopData.AsShopify_Prices();
                Generic_Pair<Shopify_Prices> tmpSP = new Generic_Pair<Shopify_Prices>(new Shopify_Prices(), tmp);
                Shopify_Prices_Pair_Adapter tmpFlatAdapter = new Shopify_Prices_Pair_Adapter(tmpSP);
                return tmpFlatAdapter.AsShopify_Prices_Pair_Flat();
            };

            IEnumerable<Shopify_Prices_Pair_Flat> flatData = _data.Select(d =>
            {
                return Transform(d);
            });
            SaveReport(flatData, TableName, settings, logger);
        }
        public static void SaveReport
        (
            IEnumerable<IShopify_Product> data
            , string TableName
            , StaticSettings settings
            , ILogger logger
        )
        {
            IEnumerable<IShopify_Product> _data = data ?? new List<IShopify_Product>();

            Func<IShopify_Product, Shopify_Product_Pair_Flat> Transform =
            (IShopProd) =>
            {
;               Shopify_Product tmp = IShopProd.AsShopify_Product();
                Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(new Shopify_Product(), tmp);
                Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
                return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
            };

            IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
            {
                return Transform(d);
            });
            SaveReport(flatData, TableName, settings, logger);
        }
        public static void SaveReport
        (
            IEnumerable<IS5InvAssembled> data
            , string TableName
            , StaticSettings settings
            , ILogger logger
        )
        {
            IEnumerable<IS5InvAssembled> _data = data ?? new List<IS5InvAssembled>();
            Func<IS5InvAssembled, Shopify_Product_Pair_Flat> Transform =
            (IS5InvAss) =>
            {
                AdaptToShopifyDataLoadFormat tmpAdapter = new AdaptToShopifyDataLoadFormat();
                tmpAdapter.Init(IS5InvAss);
                Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(new Shopify_Product(), tmpAdapter.AsShopify_Product());
                Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
                return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
            };

            IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
            {
                return Transform(d);
            });
            SaveReport(flatData, TableName, settings, logger);
        }
        public static void SaveReport(IEnumerable<Shopify_Quantities_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
        {
            IEnumerable<Shopify_Quantities_Pair_Flat> _data = data ?? new List<Shopify_Quantities_Pair_Flat>();
            var engine = new FileHelperAsyncEngine<Shopify_Quantities_Pair_Flat>();
            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftQuantities, RightInvUnique, RightCat, " +
                "RightPartNumber, RightQuantities";
            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        public static void SaveReport(IEnumerable<Shopify_Prices_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
        {
            IEnumerable<Shopify_Prices_Pair_Flat> _data = data ?? new List<Shopify_Prices_Pair_Flat>();
            var engine = new FileHelperAsyncEngine<Shopify_Prices_Pair_Flat>();
            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftPrices, LeftWholesaleCost, RightInvUnique, RightCat, " + 
                "RightPartNumber, RightPrices, RightWholesaleCost";
            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        public static void SaveReport(IEnumerable<Shopify_Product_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
        {
            IEnumerable<Shopify_Product_Pair_Flat> _data = data ?? new List<Shopify_Product_Pair_Flat>();
            var engine = new FileHelperAsyncEngine<Shopify_Product_Pair_Flat>();
            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftDescription, RightInvUnique, RightCat, RightPartNumber, " +
                "RightDescription";
            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        #region don't need any more, remove
        //public static void SaveReport(IEnumerable<AdaptToShopifyDataLoadFormat> adapters, StaticSettings settings, ILogger logger)
        //{
        //    string TableName = "AdaptToShopifyDataLoadFormat";
        //    logger.LogInformation($"Saving {TableName}");
        //    try
        //    {
        //        GenericSerialization<AdaptToShopifyDataLoadFormat>.RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, TableName);
        //        GenericSerialization<AdaptToShopifyDataLoadFormat>.SerializeToJSON(adapters.ToList(), TableName, settings.OutputDirectory, 50000);
        //    }
        //    catch
        //    {
        //        logger.LogInformation($"Failed to serialize {TableName}. {TableName} file(s) will not be written.");
        //    }

        //}
        //public static void SaveReport(VennMap<Shopify_Product, int> map, StaticSettings settings, ILogger logger)
        //{
        //    if (map == null) { throw new ArgumentNullException("the map argument in the method call to SaveReport must not be null"); }
        //    VennMap<Shopify_Product, int> _map = map;

        //    throw new NotImplementedException("SerializeVennMapResult functionality should be replaced by CSV report, so I commented it out" +
        //        " instead of changing the type info. Just Skip this routine.");
        //    //SerializeVennMapResult("VenMapp_InvOnly_Ecomm", map.InvOnly_Ecomm, settings, logger);
        //    //SerializeVennMapResult("VenMapp_InvOnly_NoEcomm", map.InvOnly_NoEcomm, settings, logger);
        //    //SerializeVennMapResult("VenMapp_Both_Ecomm", map.Both_Ecomm, settings, logger);
        //    //SerializeVennMapResult("VenMapp_Both_NoEcomm", map.Both_NoEcomm, settings, logger);
        //    //SerializeVennMapResult("VenMapp_TOnly", map.TOnly, settings, logger);

        //}
        //private static void SerializeVennMapResult
        //(
        //    string TableName
        //    , IEnumerable<(Shopify_Product, System5.Inventory.S5InvAssembledObj)> VennMapResult
        //    , StaticSettings settings
        //    , ILogger logger
        //)
        //{
        //    string tmpTableName = TableName;
        //    logger.LogInformation($"Saving {tmpTableName}");
        //    try
        //    {
        //        GenericSerialization<(Shopify_Product, S5InvAssembledObj)>
        //            .RemoveCachedFileNamesFromDirectory(settings.OutputDirectory, tmpTableName);
        //        GenericSerialization<(Shopify_Product, S5InvAssembledObj)>
        //            .SerializeToJSON(VennMapResult.ToList(), tmpTableName, settings.OutputDirectory, 50000);
        //    }
        //    catch
        //    {
        //        logger.LogInformation($"Failed to serialize {tmpTableName}. {tmpTableName} file(s) will not be written.");
        //    }
        //}
        #endregion don't need any more, remove
    }
}
