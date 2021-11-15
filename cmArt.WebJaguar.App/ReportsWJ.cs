using cmArt.LibIntegrations.ReportService;
using cmArt.WebJaguar.App.ReportViews;
using cmArt.WebJaguar.Data;
using FileHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public class ReportsWJ
    {
        //public static void SaveReport
        //(
        //    IEnumerable<Tuple<S5_CommonFields, S5_CommonFields>> data
        //    , string TableName
        //    , StaticSettings settings
        //    , ILogger logger
        //)
        //{
        //    IEnumerable<Tuple<IShopify_Product, IShopify_Product>> _data = data ?? new List<Tuple<IShopify_Product, IShopify_Product>>();

        //    Func<Tuple<IShopify_Product, IShopify_Product>, Shopify_Product_Pair_Flat> Transform =
        //    (IShopProd) =>
        //    {
        //        Shopify_Product tmp = IShopProd.Item2.AsShopify_Product();
        //        Shopify_Product tmp1 = IShopProd.Item1.AsShopify_Product();
        //        Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(tmp1, tmp);
        //        Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
        //        return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
        //    };

        //    IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
        //    {
        //        return Transform(d);
        //    });
        //    SaveReport(flatData, TableName, settings, logger);
        //}
        public static void SaveReport(IEnumerable<Changes_View> data, string TableName, String OutputDirectory, ILogger logger)
        {
            IEnumerable<Changes_View> _data = data ?? new List<Changes_View>();
            var engine = new FileHelperAsyncEngine<Changes_View>();
            engine.HeaderText = "InvUnique, Cat, PartNumber, FieldName, S5ValueToSendToShopify, ShopifyValueBeforeUpdate";

            using (engine.BeginWriteFile(OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        public static void SaveReport(IEnumerable<S5_CommonFields_Pairs_Flat> data, string TableName, string OutputDirectory, ILogger logger)
        {
            IEnumerable<S5_CommonFields_Pairs_Flat> _data = data ?? new List<S5_CommonFields_Pairs_Flat>();
            var engine = new FileHelperAsyncEngine<S5_CommonFields_Pairs_Flat>();
            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftPrices, LeftQuantities, RightInvUnique, RightCat, " +
                "RightPartNumber, RightPrices, RightQuantities";
            using (engine.BeginWriteFile(OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
    }

}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace cmArt.WebJaguar.App
//{
//    public static class ReportsWJ
//    {
//        public static void SaveReport
//        (
//            IEnumerable<Tuple<IShopify_Product, IShopify_Product>> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<Tuple<IShopify_Product, IShopify_Product>> _data = data ?? new List<Tuple<IShopify_Product, IShopify_Product>>();

//            Func<Tuple<IShopify_Product, IShopify_Product>, Shopify_Product_Pair_Flat> Transform =
//            (IShopProd) =>
//            {
//                Shopify_Product tmp = IShopProd.Item2.AsShopify_Product();
//                Shopify_Product tmp1 = IShopProd.Item1.AsShopify_Product();
//                Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(tmp1, tmp);
//                Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
//            };

//            IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<Tuple<IShopify_Prices, IShopify_Prices>> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<Tuple<IShopify_Prices, IShopify_Prices>> _data = data ?? new List<Tuple<IShopify_Prices, IShopify_Prices>>();

//            Func<Tuple<IShopify_Prices, IShopify_Prices>, Shopify_Prices_Pair_Flat> Transform =
//            (IShopProd) =>
//            {
//                Shopify_Prices tmp = IShopProd.Item2.AsShopify_Prices();
//                Shopify_Prices tmp1 = IShopProd.Item1.AsShopify_Prices();
//                Generic_Pair<Shopify_Prices> tmpSP = new Generic_Pair<Shopify_Prices>(tmp1, tmp);
//                Shopify_Prices_Pair_Adapter tmpFlatAdapter = new Shopify_Prices_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Prices_Pair_Flat();
//            };

//            IEnumerable<Shopify_Prices_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<Tuple<IShopify_Quantities, IShopify_Quantities>> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<Tuple<IShopify_Quantities, IShopify_Quantities>> _data = data ?? new List<Tuple<IShopify_Quantities, IShopify_Quantities>>();

//            Func<Tuple<IShopify_Quantities, IShopify_Quantities>, Shopify_Quantities_Pair_Flat> Transform =
//            (IShopProd) =>
//            {
//                Shopify_Quantities tmp = IShopProd.Item2.AsShopify_Quantities();
//                Shopify_Quantities tmp1 = IShopProd.Item1.AsShopify_Quantities();
//                Generic_Pair<Shopify_Quantities> tmpSP = new Generic_Pair<Shopify_Quantities>(tmp1, tmp);
//                Shopify_Quantities_Pair_Adapter tmpFlatAdapter = new Shopify_Quantities_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Quantities_Pair_Flat();
//            };

//            IEnumerable<Shopify_Quantities_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<IShopify_Quantities> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<IShopify_Quantities> _data = data ?? new List<IShopify_Quantities>();

//            Func<IShopify_Quantities, Shopify_Quantities_Pair_Flat> Transform =
//            (IShopData) =>
//            {
//                ; Shopify_Quantities tmp = IShopData.AsShopify_Quantities();
//                Generic_Pair<Shopify_Quantities> tmpSP = new Generic_Pair<Shopify_Quantities>(new Shopify_Quantities(), tmp);
//                Shopify_Quantities_Pair_Adapter tmpFlatAdapter = new Shopify_Quantities_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Quantities_Pair_Flat();
//            };

//            IEnumerable<Shopify_Quantities_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<IShopify_Prices> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<IShopify_Prices> _data = data ?? new List<IShopify_Prices>();

//            Func<IShopify_Prices, Shopify_Prices_Pair_Flat> Transform =
//            (IShopData) =>
//            {
//                ; Shopify_Prices tmp = IShopData.AsShopify_Prices();
//                Generic_Pair<Shopify_Prices> tmpSP = new Generic_Pair<Shopify_Prices>(new Shopify_Prices(), tmp);
//                Shopify_Prices_Pair_Adapter tmpFlatAdapter = new Shopify_Prices_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Prices_Pair_Flat();
//            };

//            IEnumerable<Shopify_Prices_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<IShopify_Product> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<IShopify_Product> _data = data ?? new List<IShopify_Product>();

//            Func<IShopify_Product, Shopify_Product_Pair_Flat> Transform =
//            (IShopProd) =>
//            {
//                ; Shopify_Product tmp = IShopProd.AsShopify_Product();
//                Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(new Shopify_Product(), tmp);
//                Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
//            };

//            IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport
//        (
//            IEnumerable<IS5InvAssembled> data
//            , string TableName
//            , StaticSettings settings
//            , ILogger logger
//        )
//        {
//            IEnumerable<IS5InvAssembled> _data = data ?? new List<IS5InvAssembled>();
//            Func<IS5InvAssembled, Shopify_Product_Pair_Flat> Transform =
//            (IS5InvAss) =>
//            {
//                AdaptToShopifyDataLoadFormat tmpAdapter = new AdaptToShopifyDataLoadFormat();
//                tmpAdapter.Init(IS5InvAss);
//                Generic_Pair<Shopify_Product> tmpSP = new Generic_Pair<Shopify_Product>(new Shopify_Product(), tmpAdapter.AsShopify_Product());
//                Shopify_Product_Pair_Adapter tmpFlatAdapter = new Shopify_Product_Pair_Adapter(tmpSP);
//                return tmpFlatAdapter.AsShopify_Product_Pair_Flat();
//            };

//            IEnumerable<Shopify_Product_Pair_Flat> flatData = _data.Select(d =>
//            {
//                return Transform(d);
//            });
//            SaveReport(flatData, TableName, settings, logger);
//        }
//        public static void SaveReport(IEnumerable<Changes_View> data, string TableName, StaticSettings settings, ILogger logger)
//        {
//            IEnumerable<Changes_View> _data = data ?? new List<Changes_View>();
//            var engine = new FileHelperAsyncEngine<Changes_View>();
//            engine.HeaderText = "InvUnique, Cat, PartNumber, FieldName, S5ValueToSendToShopify, ShopifyValueBeforeUpdate";

//            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
//            {
//                foreach (var record in _data)
//                {
//                    engine.WriteNext(record);
//                }
//            }
//        }
//        public static void SaveReport(IEnumerable<Shopify_Quantities_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
//        {
//            IEnumerable<Shopify_Quantities_Pair_Flat> _data = data ?? new List<Shopify_Quantities_Pair_Flat>();
//            var engine = new FileHelperAsyncEngine<Shopify_Quantities_Pair_Flat>();
//            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftQuantities, RightInvUnique, RightCat, " +
//                "RightPartNumber, RightQuantities";
//            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
//            {
//                foreach (var record in _data)
//                {
//                    engine.WriteNext(record);
//                }
//            }
//        }
//        public static void SaveReport(IEnumerable<Shopify_Prices_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
//        {
//            IEnumerable<Shopify_Prices_Pair_Flat> _data = data ?? new List<Shopify_Prices_Pair_Flat>();
//            var engine = new FileHelperAsyncEngine<Shopify_Prices_Pair_Flat>();
//            //engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftPrices, LeftWholesaleCost, RightInvUnique, RightCat, " + 
//            //    "RightPartNumber, RightPrices, RightWholesaleCost";
//            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftPrices, RightInvUnique, RightCat, " +
//                "RightPartNumber, RightPrices";
//            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
//            {
//                foreach (var record in _data)
//                {
//                    engine.WriteNext(record);
//                }
//            }
//        }
//        public static void SaveReport(IEnumerable<Shopify_Product_Pair_Flat> data, string TableName, StaticSettings settings, ILogger logger)
//        {
//            IEnumerable<Shopify_Product_Pair_Flat> _data = data ?? new List<Shopify_Product_Pair_Flat>();
//            var engine = new FileHelperAsyncEngine<Shopify_Product_Pair_Flat>();
//            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftDescription, RightInvUnique, RightCat, RightPartNumber, " +
//                "RightDescription";
//            using (engine.BeginWriteFile(settings.OutputDirectory + $"\\{TableName}.csv"))
//            {
//                foreach (var record in _data)
//                {
//                    engine.WriteNext(record);
//                }
//            }
//        }
//    }

//}
