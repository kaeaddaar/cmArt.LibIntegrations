using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cmArt.LibIntegrations.PriceCalculations;
using cmArt.Reece.ShopifyConnector;
using cmArt.LibIntegrations.ReportService;
#nullable enable

namespace cmArt.Shopify.App.Data
{
    /// <summary>
    /// Adapt to a common IShopifyDataLoadFormat comprised of IShopify_Product, IShopify_Prices, IShopify_Quantities, and more. The listed interfaces
    /// match up to the 3 separate API data structures sent & received from Reece's Shopify API.
    /// </summary>
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
        public string Cat { get => _InvAss.Inv.Cat.TrimEnd(); set => _InvAss.Inv.Cat = value.TrimEnd() ?? string.Empty; }
        public int InvUnique { get => _InvAss.Inv.InvUnique; set => _InvAss.Inv.InvUnique = value; }
        public string PartNumber { get => _InvAss.Inv.Part.TrimEnd(); set => _InvAss.Inv.Part = value.TrimEnd() ?? string.Empty; }
        public string Description { get => _InvAss.Inv.Description.TrimEnd(); set => _InvAss.Inv.Description = value.TrimEnd() ?? string.Empty; }

        public decimal WholesaleCost { get => (decimal)_InvAss.Inv.Wholesale_1; set => _InvAss.Inv.Wholesale_1 = (double)value; }
        public IEnumerable<S5PricePair> Prices 
        { 
            get
            {
                List<S5PricePair> prices = new List<S5PricePair>();
                var FirstRecord = _InvAss;
                foreach (var sched in FirstRecord.InvPrices_PerInventry_27)
                {
                    PriceScheduleView tmpPriceSchedule = GetPriceSchedule(sched.ScheduleLevel);
                    S5PricePair tmpPrice = new S5PricePair(tmpPriceSchedule.Level, tmpPriceSchedule.Price);
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
                decimal tmpQty = (decimal)0;
                double dblQty = 0;
                try
                {
                    dblQty =
                    (
                        _InvAss.StokLines_PerInventry_27.Select(stok => stok.CostQuantity).Sum()
                        - _InvAss.StokLines_PerInventry_27.Where(stok => stok.StockStatus == 39 || stok.StockStatus == 40
                        || stok.StockStatus == 16 || stok.StockStatus == 17).Select(stok => stok.PriceQty).Sum()
                    );

                }
                catch
                {
                    dblQty = 0;
                }

                try
                {
                    tmpQty = (decimal)dblQty;
                }
                catch
                {
                    tmpQty = 0;
                }

                return tmpQty;
            }

                
            set => throw new NotImplementedException("You can't really load a qty into the records that make the quantity up. Possible, but does it really make sense?"); 
        }

        public IEnumerable<S5QtyPair> Quantities 
        {
            get 
            {
                // build quantities for each department. Consider an All Department Quantities as well
                if(_InvAss.StokLines_PerInventry_27 is null) { return new List<S5QtyPair>(); }
                IEnumerable<short> StokDepts = _InvAss.StokLines_PerInventry_27.Select(inv => inv.Department).GroupBy(x => x).Select(y => y.Key);
                List<S5QtyPair> pairs = new List<S5QtyPair>();
                foreach(var dept in StokDepts)
                {
                    S5QtyPair tmp = GetInStock(dept);
                    pairs.Add(tmp);
                }
                return pairs;
            }
            set => throw new NotImplementedException("You can't really load a qty into the records that make the quantity up. Possible, but does it really make sense?");
        }

        public string WebCategory 
        { 
            get
            {
                IComments? tmp = _InvAss.CommentsLines_PerInventry_27.Where(x => x.LineNo == 2).FirstOrDefault();
                bool IsEmpty = tmp == null;
                if (IsEmpty) { tmp = new Comments(); }
                return tmp.Comment ?? string.Empty;
            }
            set => throw new NotImplementedException(); 
        }

        private S5QtyPair GetInStock(short Dept)
        {
            _InvAss = _InvAss ?? new S5InvAssembled();
            decimal tmpQty = (decimal)0;
            double dblQty = 0;
            IEnumerable<IStok> StokForDept = _InvAss.StokLines_PerInventry_27.Where(stk => stk.Department == Dept);
            try
            {
                dblQty =
                (
                    StokForDept.Select(stok => stok.CostQuantity).Sum()
                    - StokForDept.Where(stok => stok.StockStatus == 39 || stok.StockStatus == 40 || stok.StockStatus == 16 || stok.StockStatus == 17)
                        .Select(stok => stok.PriceQty)
                        .Sum()
                );

            }
            catch
            {
                dblQty = 0;
            }

            try
            {
                tmpQty = (decimal)dblQty;
            }
            catch
            {
                tmpQty = 0;
            }

            S5QtyPair pair = new S5QtyPair(Dept, tmpQty);
            return pair;
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
            this.Cat = IFrom.Cat;
            this.Description = IFrom.Description;
            this.InvUnique = IFrom.InvUnique;
            this.PartNumber = IFrom.PartNumber;
            this.Prices = IFrom.Prices;
            //this.WholesaleCost = IFrom.WholesaleCost;
            return this;
        }

        public bool cmEquals(IShopifyDataLoadFormat compareTo)
        {
            return IShopifyDataLoadFormatExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Product compareTo)
        {
            return IShopify_ProductExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Prices compareTo)
        {
            return IShopify_PricesExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Quantities compareTo)
        {
            return IShopify_QuantitiesExtensions.Equals(this, compareTo);
        }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return IShopify_ProductExtensions.CopyFrom(this, IFrom);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Product CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Prices CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Quantities CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopifyDataLoadFormat CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }
    }

}
