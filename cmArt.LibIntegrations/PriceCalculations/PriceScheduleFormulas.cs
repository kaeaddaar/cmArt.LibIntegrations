using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.PriceCalculations
{
    public static class PriceScheduleFormulas
    {
        // Markup from Cost: W
        public static double MarkupFromCost(double MarkupPercentage, double WholesaleCost)
        {
            return GetMarkup(MarkupPercentage, WholesaleCost);
        }
        // Markup from Landed: L
        public static double MarkupFromLanded(double MarkupPercentage, double LandedCost)
        {
            return GetMarkup(MarkupPercentage, LandedCost);
        }
        // Discount from List: D
        public static double DiscountFromList(double DiscountPercentage, double ListPrice)
        {
            return GetDiscount(DiscountPercentage, ListPrice);
        }
        // Fixed: F
        public static double GetFixedPrice(double FixedColumnValue)
        {
            return FixedColumnValue;
        }
        // Margin from Cost: H
        public static double GetMarginFromCost(double MarginPercentage, double WholesaleCost)
        {
            return GetMargin(MarginPercentage, WholesaleCost);
        }
        // Margin from Landed: M
        public static double GetMarginFromLanded(double MarginPercentage, double LandedCost)
        {
            return GetMargin(MarginPercentage, LandedCost);
        }
        // Discount from Sale: S
        public static double GetDiscountFromSale(double DiscountPercentage, double SalePrice)
        {
            return GetDiscount(DiscountPercentage, SalePrice);
        }
        // Dollar Markup from Landed: $
        public static double GetDollarMarkupFromLanded(double DollarMarkup, double LandedCost)
        {
            return GetMarkupByDollar(DollarMarkup, LandedCost);
        }
        // Discount from Cash Price Schedule: I
        public static double GetDiscountFromCashPriceSchedule(double DiscountPercentage, double CashPrice)
        {
            return GetDiscount(DiscountPercentage, CashPrice);
        }
        // Markup from Cash Price Schedule: J
        public static double GetMarkupFromCashPriceSchedule(double MarkupPercentage, double CashPrice)
        {
            return GetMarkup(MarkupPercentage, CashPrice);
        }
        // Dollar Markup from Cash Price Schedule: K
        public static double GetDollarMarkupFromCashPriceSchedule(double MarkupPercentage, double CashPrice)
        {
            return GetMarkup(MarkupPercentage, CashPrice);
        }

        // Base formulas
        public static double GetDiscount(double Percentage, double FromValue)
        {
            double _price;
            _price = FromValue - (FromValue * Percentage / 100);
            return _price;
        }

        public static double GetMargin(double Percentage, double FromCostValue)
        {
            double _price;
            _price = FromCostValue / (1 - Percentage / 100);
            return _price;
        }

        public static double GetMarkup(double Percentage, double FromCostvalue)
        {
            double _price;
            _price = FromCostvalue + (FromCostvalue * Percentage / 100);
            return _price;
        }

        public static double GetMarkupByDollar(double DollarValue, double FromCostValue)
        {
            double _price;
            _price = FromCostValue + DollarValue;
            return _price;
        }
    }

}
