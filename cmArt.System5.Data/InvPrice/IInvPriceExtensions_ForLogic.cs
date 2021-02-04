using System;
using System.Collections.Generic;
using System.Linq;

namespace cmArt.System5.Data
{
    public static class IInvPriceExtensions_ForLogic
    {
        public static IEnumerable<IInvPrice> GetPricesAsOf
        (
            this IEnumerable<IInvPrice> unfilteredPrices
            , DateTime AsOfDate
        )
        {
            IEnumerable<IInvPrice> _prices;
            _prices = unfilteredPrices;

            _prices = unfilteredPrices.Where(p => p.StartDate < AsOfDate);

            var RecentPriceKey =
                from InvPrice in _prices
                group InvPrice by new { InvPrice.PartUnique, InvPrice.ScheduleLevel, InvPrice.Department }
                into newPrices
                let maxDate = newPrices.Max(p => p.StartDate)
                select new
                {
                    PartUnique = newPrices.Key.PartUnique
                    , ScheduleLevel = newPrices.Key.ScheduleLevel
                    , Department = newPrices.Key.Department
                    , MaxDate = maxDate
                };

            // Assemble the key back into a full table of information
            var RecentPrices =
                from InvPriceKey in RecentPriceKey
                join InvPrice1 in _prices on
                new
                {
                    PartUnique = InvPriceKey.PartUnique
                    , StartDate = InvPriceKey.MaxDate
                    , ScheduleLevel = InvPriceKey.ScheduleLevel
                    , Department = InvPriceKey.Department
                }
                equals
                new
                {
                    PartUnique = InvPrice1.PartUnique
                    , StartDate = InvPrice1.StartDate
                    , ScheduleLevel = InvPrice1.ScheduleLevel
                    , Department = InvPrice1.Department
                }
                select new InvPrice
                {
                    InvUnique = InvPrice1.InvUnique
                    , Department = InvPrice1.Department
                    , EndDate = InvPrice1.EndDate
                    , PartUnique = InvPrice1.PartUnique
                    , QuanDisc = InvPrice1.QuanDisc
                    , RegularPrice = InvPrice1.RegularPrice
                    , RScheduleType = InvPrice1.RScheduleType
                    , SalePrice = InvPrice1.SalePrice
                    , ScheduleLevel = InvPrice1.ScheduleLevel
                    , SScheduleType = InvPrice1.SScheduleType
                    , StartDate = InvPrice1.StartDate
                };

            return RecentPrices;
        }
    }

}
