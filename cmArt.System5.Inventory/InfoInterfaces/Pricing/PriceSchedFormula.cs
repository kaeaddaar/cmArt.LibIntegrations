using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory.Pricing
{
    public enum PriceSchedFormula
    {
        Not_Set
        , W_MarkupFromCost
        , L_MarkupFromLanded
        , D_DiscountFromList
        , F_Fixed
        , H_MarginFromCost
        , M_MarginFromLanded
        , S_DiscountFromSale
        , DS_DollarMarkupFromLanded
        , I_DiscountFromCashPriceSchedule
        , J_MarkupFromCashPriceSchedule
        , K_DollarMarkupFromCashPriceSchedule
    }
}
