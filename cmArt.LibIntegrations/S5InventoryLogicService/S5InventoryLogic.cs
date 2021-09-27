using cmArt.LibIntegrations.PriceCalculations;
using cmArt.System5.Data;
using cmArt.System5.Inventory;
using cmArt.System5.Inventory.InfoInterfaces.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cmArt.LibIntegrations.S5InventoryLogicService
{
    public class S5InventoryLogic : IS5InventoryLogic
    {
        private IS5InvAssembled _InvAss;

        public S5InventoryLogic()
        {

        }
        public S5InventoryLogic(IS5InvAssembled data)
        {
            init(data);
        }
        public void init(IS5InvAssembled s5InventoryAssembled)
        {
            if (_InvAss != null) { throw new Exception("StockLogic already initialized, make sure you don't call init a second time."); }
            _InvAss = s5InventoryAssembled ?? new S5InvAssembled();

        }
        private void CheckInit()
        {
            if (_InvAss == null)
            {
                throw new NullReferenceException("StockLogic not initialized, make sure you call init or pass in data via the constructor before using any of the logic.");
            }

        }
        public string Cat
        {
            get
            {
                CheckInit();
                IInventry_27 inv = _InvAss.Inv ?? new Inventry_27();
                string cat = inv.Cat ?? string.Empty;
                return cat.TrimEnd();
            }
            set
            {
                string strValue = value ?? string.Empty;
                _InvAss.Inv.Cat = value.TrimEnd();
            }
        }
        public int InvUnique { get => _InvAss.Inv.InvUnique; set => _InvAss.Inv.InvUnique = value; }
        public string PartNumber { get => _InvAss.Inv.Part.TrimEnd(); set => _InvAss.Inv.Part = value.TrimEnd() ?? string.Empty; }
        public string Description { get => _InvAss.Inv.Description.TrimEnd(); set => _InvAss.Inv.Description = value.TrimEnd() ?? string.Empty; }

        public decimal WholesaleCost { get => (decimal)_InvAss.Inv.Wholesale_1; set => _InvAss.Inv.Wholesale_1 = (double)value; }

        public IEnumerable<II_S5QtyPair> Quantities
        {
            get
            {
                CheckInit();
                // build quantities for each department. Consider an All Department Quantities as well
                if (_InvAss.StokLines_PerInventry_27 is null) { return new List<QtyPair>(); }
                IEnumerable<short> StokDepts = _InvAss.StokLines_PerInventry_27.Select(inv => inv.Department).GroupBy(x => x).Select(y => y.Key);
                List<QtyPair> pairs = new List<QtyPair>();
                foreach (var dept in StokDepts)
                {
                    QtyPair tmp = GetInStock(dept);
                    pairs.Add(tmp);
                }
                return pairs;
            }
            set => throw new NotImplementedException("You can't really load a qty into the records that make the quantity up. Possible, but does it really make sense?");
        }
        private QtyPair GetInStock(short Dept)
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

            QtyPair pair = new QtyPair(Dept, tmpQty);
            return pair;
        }

        public IEnumerable<PricePair> Prices
        {
            get
            {
                CheckInit();
                List<PricePair> prices = new List<PricePair>();
                var FirstRecord = _InvAss;
                foreach (var sched in FirstRecord.InvPrices_PerInventry_27)
                {
                    PriceScheduleView tmpPriceSchedule = GetPriceSchedule(sched.ScheduleLevel);
                    PricePair tmpPrice = new PricePair(tmpPriceSchedule.Level, tmpPriceSchedule.Price);
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

    }
}
