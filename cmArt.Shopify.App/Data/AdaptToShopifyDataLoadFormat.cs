using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cmArt.LibIntegrations.PriceCalculations;
#nullable enable

namespace cmArt.Shopify.App.Data
{
    public class AdaptToShopifyDataLoadFormat : IShopifyDataLoadFormat
    {
        protected IS5InvAssembled _InvAss { get; set; }

        public AdaptToShopifyDataLoadFormat()
        {
            _InvAss = new S5InvAssembled();
        }
        public void Init(IS5InvAssembled inv)
        {
            _InvAss = inv;
        }
        public string Cat { get => _InvAss.Inv.Cat; set => _InvAss.Inv.Cat = value ?? string.Empty; }
        public int InvUnique { get => _InvAss.Inv.InvUnique; set => _InvAss.Inv.InvUnique = value; }
        public string PartNumber { get => _InvAss.Inv.Part; set => _InvAss.Inv.Part = value ?? string.Empty; }
        public string Description { get => _InvAss.Inv.Description; set => _InvAss.Inv.Description = value ?? string.Empty; }
        public decimal PriceSchedule1_MSRP
        {
            get => (decimal)GetPriceSchedule(1).Price;
            set => throw new NotImplementedException("Doesn't yet support converting a price back into percentage " +
                "based on a formula");
        }
        public decimal PriceSchedule2_MinPrice
        {
            get => (decimal)GetPriceSchedule(0).Price;
            set => throw new NotImplementedException("Doesn't yet support converting a price back into percentage " +
                "based on a formula");
        }
        public decimal WholesaleCost { get => (decimal)_InvAss.Inv.Wholesale_1; set => _InvAss.Inv.Wholesale_1 = (double)value; }
        public IEnumerable<decimal> Prices 
        { 
            get
            {
                List<decimal> prices = new List<decimal>();
                var FirstRecord = _InvAss;
                foreach (var sched in FirstRecord.InvPrices_PerInventry_27)
                {
                    decimal tmpPrice = GetPriceSchedule(sched.ScheduleLevel).Price;
                    prices.Add(tmpPrice);
                }
                return prices;
            }

            set => throw new NotImplementedException("Doesn't yet support setting the price");
        }

        protected PriceScheduleView GetPriceSchedule(short ScheduleNum)
        {
            var FirstRecord = _InvAss;

            var Bases = FirstRecord.InvPrices_PerInventry_27.PopulateBasePriceSchedInfo_NoPrice(FirstRecord.Inv, 0, 0, 0);
            var prices = Bases.CalculatePrices(FirstRecord.InvPrices_PerInventry_27);

            var price = prices.Where(p => p.Level == ScheduleNum).FirstOrDefault();

            return price;
        }

        public IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom)
        {
            throw new NotImplementedException();
        }
    }

}
