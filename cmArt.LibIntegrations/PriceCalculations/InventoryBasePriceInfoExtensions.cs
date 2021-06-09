using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace cmArt.LibIntegrations.PriceCalculations
{
    public static class InventoryBasePriceInfoExtensions
    {
        public static double LandedCost(this IInventoryBasePriceInfo info)
        {
            double _LandedCost;
            _LandedCost = info.WholesaleCost + info.Freight + info.Extra + info.Duty;
            return _LandedCost;
        }

        public static PriceScheduleView CalculatePrice(this IInventoryBasePriceInfo info, IInvPrice price)
        {
            double WholesaleCost = info.WholesaleCost;
            double LandedCost = info.LandedCost();

            double? Price0 = null;
            double? ListPrice = null;
            double? CashPrice = null;
            double? SalePrice = null;
            double? PriceToCalculate = null;

            Price0 = info.RegularPrice_0_Calculated;
            ListPrice = info.RegularPrice_List_Calculated;
            CashPrice = info.RegularPrice_Cash_Calculated;
            SalePrice = info.SalePrice_0_Calculated;

            string info_json = string.Empty;
            string nl = Environment.NewLine;

            try
            {
                PriceToCalculate = CalcPrice
                (
                    price.RScheduleType
                    , price.RegularPrice
                    , WholesaleCost
                    , LandedCost
                    , CashPrice
                    , SalePrice
                    , ListPrice
                );
            }
            catch (ArgumentNullException e)
            {
                info_json = GetContents(info);
                Console.WriteLine($"Error: failed to calculate Web Price" +
                    $"({info.ScheduleLevel_List}). " + "Details: " + e.Message + nl + info_json);
            }

            PriceScheduleView view = new PriceScheduleView();
            view.Level = price.ScheduleLevel;
            view.Percentage = price.RegularPrice;
            view.Price = (decimal)PriceToCalculate;
            view.Code = price.RScheduleType;

            return view;
        }

        public static IEnumerable<PriceScheduleView> CalculatePrices
        (
            this IInventoryBasePriceInfo info
            , IEnumerable<IInvPrice> invPrices
        )
        {
            double WholesaleCost = info.WholesaleCost;
            double LandedCost = info.LandedCost();

            double? Price0 = null;
            double? ListPrice = null;
            double? CashPrice = null;
            double? SalePrice = null;

            info.CalculateBasePrices();
            Price0 = info.RegularPrice_0_Calculated;
            ListPrice = info.RegularPrice_List_Calculated;
            CashPrice = info.RegularPrice_Cash_Calculated;
            SalePrice = info.SalePrice_0_Calculated;

            string info_json = string.Empty;
            string nl = Environment.NewLine;

            Func<IInvPrice, double> calc = (x) =>
            {
                try
                {
                    return (double)(decimal)CalcPrice
                    (
                        x.RScheduleType
                        , x.RegularPrice
                        , WholesaleCost
                        , LandedCost
                        , CashPrice
                        , SalePrice
                        , ListPrice
                    );
                }
                catch (ArgumentNullException e)
                {
                    info_json = GetContents(info);
                    Console.WriteLine($"Error: failed to calculate Web Price" +
                        $"({info.ScheduleLevel_List}). " + "Details: " + e.Message + nl + info_json);
                    return 0;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine($"Error: Overflow exception. x.RegularPrice={x.RegularPrice}, WholesaleCost={WholesaleCost}" +
                        $", LandedCost={LandedCost}, SalePrice={SalePrice}, ListPrice={ListPrice}");
                    return 100000;
                }

            };

            var views = invPrices.Select
            (
                price => new PriceScheduleView()
                {
                    Code = price.RScheduleType
                    , InventoryUnique = price.PartUnique
                    , Level = price.ScheduleLevel
                    , Percentage = price.RegularPrice
                    , Price = (decimal)calc(price)
                }
            );

            #region linq above turned into foreach
            if (false)
            {
                List<PriceScheduleView> Vs = new List<PriceScheduleView>();

                foreach (var price in invPrices)
                {
                    Vs.Add
                    (
                        new PriceScheduleView()
                        {
                            Code = price.RScheduleType
                            , InventoryUnique = price.PartUnique
                            , Level = price.ScheduleLevel
                            , Percentage = price.RegularPrice
                            , Price = (decimal)calc(price)
                        }
                    );
                }
            }
            #endregion linq above turned into foreach

            return views;
        }

        public static IInventoryBasePriceInfo CalculateBasePrices(this IInventoryBasePriceInfo info)
        {
            double WholesaleCost = info.WholesaleCost;
            double LandedCost = info.LandedCost();

            double? Price0 = null;
            double? ListPrice = null;
            double? CashPrice = null;
            double? SalePrice = null;

            string info_json = string.Empty;
            string nl = Environment.NewLine;
            if (info.SKU == "8702418")
            {
                string x = info.SKU;
            }

            try
            {
                Price0 = CalcPrice
                (
                    info.RScheduleType_0
                    , info.RegularPrice_0
                    , WholesaleCost
                    , LandedCost
                    , CashPrice
                    , SalePrice
                    , ListPrice
                );
            }
            catch (ArgumentNullException e)
            {
                info_json = GetContents(info);
                Console.WriteLine($"Error: failed to calculate retail for price schedule 0" +
                    $"({info.ScheduleLevel_0}). " + "Details: " + e.Message + nl + info_json);
            }

            try
            {
                ListPrice = CalcPrice
                (
                    info.RScheduleType_List
                    , info.RegularPrice_List
                    , WholesaleCost
                    , LandedCost
                    , CashPrice
                    , SalePrice
                    , ListPrice
                );
            }
            catch (ArgumentNullException e)
            {
                info_json = GetContents(info);
                Console.WriteLine($"Error: failed to calculate retail for List Price" +
                    $"({info.ScheduleLevel_List}). " + "Details: " + e.Message + nl + info_json);
            }

            try
            {
                CashPrice = CalcPrice
                (
                    info.RScheduleType_Cash
                    , info.RegularPrice_Cash
                    , WholesaleCost
                    , LandedCost
                    , CashPrice
                    , SalePrice
                    , ListPrice
                );
            }
            catch (ArgumentNullException e)
            {
                info_json = GetContents(info);
                Console.WriteLine($"Error: failed to calculate retail for Cash Price" +
                    $"({info.ScheduleLevel_List}). " + "Details: " + e.Message + nl + info_json);
            }

            try
            {
                SalePrice = CalcPrice
                (
                    info.SScheduleType_0
                    , info.SalePrice_0
                    , WholesaleCost
                    , LandedCost
                    , CashPrice
                    , SalePrice
                    , ListPrice
                );
            }
            catch (ArgumentNullException e)
            {
                info_json = GetContents(info);
                Console.WriteLine($"Error: failed to calculate Sale Price" +
                    $"({info.ScheduleLevel_List}). " + "Details: " + e.Message + nl + info_json);
            }

            info.RegularPrice_0_Calculated = CNull(Price0);
            info.RegularPrice_List_Calculated = CNull(ListPrice);
            info.RegularPrice_Cash_Calculated = CNull(CashPrice);
            info.SalePrice_0_Calculated = CNull(SalePrice);

            return info;
        }

        public static string GetContents(this IInventoryBasePriceInfo info)
        {
            string json = string.Empty;
            try
            {
                json = JsonSerializer.Serialize(info);
            }
            catch
            {
                json = "{ error: \"Failed to serialize\"}";
            }
            return json;
        }

        private static double CNull(double? value, double DefaultIfNull = 0)
        {
            if (value is null)
            {
                value = DefaultIfNull;
            }
            return (double)value;
        }

        public static double CalcPrice
        (
            string RScheduleType
            , double BaseValue
            , double WholesaleCost
            , double LandedCost
            , double? CashPrice
            , double? SalePrice
            , double? ListPrice
        )
        {
            double price = 0;
            const string msgListRqd = "List Price is required";
            const string msgSaleRqd = "Sale Price is required";
            const string msgCashRqd = "Cash Price is required";

            switch (RScheduleType)
            {
                case "W":
                    price = PriceScheduleFormulas.MarkupFromCost(BaseValue, WholesaleCost);
                    break;
                case "L":
                    price = PriceScheduleFormulas.MarkupFromLanded(BaseValue, LandedCost);
                    break;
                case "D":
                    if (ListPrice is null)
                    { throw new ArgumentNullException(msgListRqd + $"({RScheduleType})"); }
                    price = PriceScheduleFormulas.DiscountFromList
                        (BaseValue, (double)ListPrice);
                    break;
                case "F":
                    price = PriceScheduleFormulas.GetFixedPrice(BaseValue);
                    break;
                case "H":
                    price = PriceScheduleFormulas.GetMarginFromCost
                        (BaseValue, WholesaleCost);
                    break;
                case "M":
                    price = PriceScheduleFormulas.GetMarginFromLanded(BaseValue, LandedCost);
                    break;
                case "S":
                    if (SalePrice is null)
                    { throw new ArgumentNullException(msgSaleRqd + $"({RScheduleType})"); }
                    price = PriceScheduleFormulas.GetDiscountFromSale(BaseValue, (double)SalePrice);
                    break;
                case "$":
                    price = PriceScheduleFormulas.GetDollarMarkupFromLanded
                        (BaseValue, LandedCost);
                    break;
                case "I":
                    if (CashPrice is null)
                    { throw new ArgumentNullException(msgCashRqd + $"({RScheduleType})"); }
                    price = PriceScheduleFormulas.GetDiscountFromCashPriceSchedule
                        (BaseValue, (double)CashPrice);
                    break;
                case "J":
                    if (CashPrice is null)
                    { throw new ArgumentNullException(msgCashRqd + $"({RScheduleType})"); }
                    price = PriceScheduleFormulas.GetMarkupFromCashPriceSchedule
                        (BaseValue, (double)CashPrice);
                    break;
                case "K":
                    if (CashPrice is null)
                    { throw new ArgumentNullException(msgCashRqd + $"({RScheduleType})"); }
                    price = PriceScheduleFormulas.GetDollarMarkupFromCashPriceSchedule
                        (BaseValue, (double)CashPrice);
                    break;
            }
            return price;
        }
    }

}
