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

        public decimal WholesaleCost { get => (decimal)_InvAss.Inv.Wholesale_1; set => _InvAss.Inv.Wholesale_1 = (double)value; }
        public IEnumerable<pair> Prices 
        { 
            get
            {
                List<pair> prices = new List<pair>();
                var FirstRecord = _InvAss;
                foreach (var sched in FirstRecord.InvPrices_PerInventry_27)
                {
                    PriceScheduleView tmpPriceSchedule = GetPriceSchedule(sched.ScheduleLevel);
                    pair tmpPrice = new pair(tmpPriceSchedule.Level, tmpPriceSchedule.Price);
                    prices.Add(tmpPrice);
                }
                return prices;
            }

            set => throw new NotImplementedException("Doesn't yet support setting the price");
        }

        public decimal InStock
        {
            get 
            {
                _InvAss = _InvAss ?? new S5InvAssembled();
                decimal tmpQty = (decimal)
                    (
                        _InvAss.StokLines_PerInventry_27.Select(stok => stok.CostQuantity).Sum()
                        - _InvAss.StokLines_PerInventry_27.Where(stok => stok.StockStatus == 39 || stok.StockStatus == 40
                        || stok.StockStatus == 16 || stok.StockStatus == 17).Select(stok => stok.PriceQty).Sum()
                    );
                return tmpQty;
            }

                
            set => throw new NotImplementedException("You can't really load a qty into the records that make the quantity up. Possible, but does it really make sense?"); 
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
