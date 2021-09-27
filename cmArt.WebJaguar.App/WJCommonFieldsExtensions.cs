using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.LibIntegrations.S5InventoryLogicService;
using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public static class WJCommonFieldsExtensions
    {
        public static IWJCommonFields CopyFrom(this IWJCommonFields to, IWJCommonFields from)
        {
            to.Cat = from.Cat;
            to.Description = from.Description;
            to.InvUnique = from.InvUnique;
            to.PartNumber = from.PartNumber;

            to.Prices = new List<PricePair>(from.Prices ?? new List<PricePair>());
            to.Quantities = new List<QtyPair>(from.Quantities ?? new List<QtyPair>());

            to.WholesaleCost = from.WholesaleCost;

            return to;
        }
        public static bool Equals(this IWJCommonFields to, IWJCommonFields from)
        {
            bool TheyDontMatch = false;
            if (to.InvUnique != from.InvUnique) { return TheyDontMatch; }
            if (to.Cat != from.Cat) { return TheyDontMatch; }
            if (to.Description != from.Description) { return TheyDontMatch; }
            if (to.PartNumber != from.PartNumber) { return TheyDontMatch; }
            if (to.WholesaleCost != from.WholesaleCost) { return TheyDontMatch; }
            
            if (to.Prices.Count() != from.Prices.Count()) { return TheyDontMatch; }
            if (to.Quantities.Count() != from.Quantities.Count()) { return TheyDontMatch; }

            IEnumerable<Tuple<PricePair, PricePair>> ComparePricePairs = GenericJoins<PricePair, PricePair, short>
                .FullOuterJoin(to.Prices, from.Prices, PricePair_Indexes.level, PricePair_Indexes.level);
            foreach (var cpp in ComparePricePairs)
            {
                bool NotAPair = (cpp.Item1 is null) || (cpp.Item2 is null);
                if (NotAPair) { return TheyDontMatch; }
                bool NotEqual = (cpp.Item1.Equals(cpp.Item2)) == false;
                if (NotEqual) { return TheyDontMatch; }
            }

            IEnumerable<Tuple<QtyPair, QtyPair>> CompareQuantityPairs = GenericJoins<QtyPair, QtyPair, short>
                .FullOuterJoin(to.Quantities, from.Quantities, QtyPair_Indexes.location, QtyPair_Indexes.location);
            foreach (var cqp in CompareQuantityPairs)
            {
                bool NotAPair = (cqp.Item1 is null) || (cqp.Item2 is null);
                if (NotAPair) { return TheyDontMatch; }
                bool NotEqual = (cqp.Item1.Equals(cqp.Item2)) == false;
                if (NotEqual) { return TheyDontMatch; }
            }

            return true;

        }
    }
}
