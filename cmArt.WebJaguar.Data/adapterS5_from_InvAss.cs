using cmArt.Reece.ShopifyConnector;
using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cmArt.LibIntegrations.PriceCalculations;


namespace cmArt.WebJaguar.Data
{
    public class adapterS5_from_InvAss : IS5_CommonFields_In_WJ
    {
        private IS5InvAssembled _InvAss;
        public adapterS5_from_InvAss()
        {
            _InvAss = _InvAss ?? new S5InvAssembled();
        }

        public bool IsEcomm
        {
            get
            {
                return (_InvAss.Inv ?? new Inventry_27()).Ecommerce == "Y";
            }
            set
            {
                if (value)
                {
                    (_InvAss.Inv ?? new Inventry_27()).Ecommerce = "Y";
                }
                else
                {
                    (_InvAss.Inv ?? new Inventry_27()).Ecommerce = "N";
                }
            }
        }

        public IEnumerable<string> barcodes
        {
            get
            {
                IEnumerable<string> result;
                result = _InvAss.AltSuplies_PerInventry_27.Select(alt => alt.PartNumber.TrimEnd());
                return result;
            }
            set
            {
                throw new NotImplementedException("need more than just a string to build these records, not sure if it can be done.");
            }
        }
        public string Description
        {
            get
            {
                return (_InvAss.Inv.Description ?? string.Empty).TrimEnd();
            }
            set
            {
                _InvAss.Inv.Description = (value ?? string.Empty).TrimEnd();
            }
        }
        public int InvUnique
        {
            get
            {
                return _InvAss.Inv.InvUnique;
            }
            set
            {
                _InvAss.Inv.InvUnique = value;
            }
        }
        public string PartNumber
        {
            get
            {
                return _InvAss.Inv.Part;
            }
            set
            {
                _InvAss.Inv.Part = value ?? string.Empty;
            }
        }
        public string WebDescription 
        {
            get => string.Empty; 
            set => throw new NotImplementedException(); 
        }
        public float weight
        {
            get
            {
                return _InvAss.Inv.Weight;
            }
            set
            {
                _InvAss.Inv.Weight = value;
            }
        }

        public string Cat
        {
            get { return _InvAss.Inv.Cat.TrimEnd() ?? "000"; }
            set { _InvAss.Inv.Cat = (value ?? "000").TrimEnd(); }
        }
        public string FF22
        {
            get 
            {
                IComments tmp = _InvAss.CommentsLines_PerInventry_27.Where(x => x.LineNo == 22).FirstOrDefault() ?? new Comments();
                return tmp.Comment ?? string.Empty; 
            }
            set 
            {
                IComments tmp = _InvAss.CommentsLines_PerInventry_27.Where(x => x.LineNo == 22).FirstOrDefault() ?? new Comments();
                tmp.Comment = value ?? string.Empty;
            }
        }

        public decimal WholesaleCost
        {
            get
            {
                return (decimal)_InvAss.Inv.Wholesale_1;
            }
            set
            {
                _InvAss.Inv.Wholesale_1 = (double)value;
            }
        }
        public IEnumerable<S5PricePair> Prices
        {
            get
            {
                List<S5PricePair> tmpPrices = new List<S5PricePair>();
                for (short i = 0; i < 10; i++)
                {
                    tmpPrices.Add(new S5PricePair(i, (decimal)GetPriceSchedule(i).Price));
                }
                return tmpPrices;
            }
            set
            {
                throw new NotImplementedException("Doesn't yet support converting a price back into percentage " +
                "based on a formula");
            }
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
                if (_InvAss.StokLines_PerInventry_27 is null) { return new List<S5QtyPair>(); }
                IEnumerable<short> StokDepts = _InvAss.StokLines_PerInventry_27.Select(inv => inv.Department).GroupBy(x => x).Select(y => y.Key);
                List<S5QtyPair> pairs = new List<S5QtyPair>();
                foreach (var dept in StokDepts)
                {
                    S5QtyPair tmp = GetInStock(dept);
                    pairs.Add(tmp);
                }
                return pairs;
            }
            set => throw new NotImplementedException("You can't really load a qty into the records that make the quantity up. Possible, but does it really make sense?");
        }

        public string Units
        {
            get { return (_InvAss.Inv.Units ?? string.Empty).TrimEnd(); }
            set { _InvAss.Inv.Units = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_1
        {
            get { return (_InvAss.Inv.Size_1 ?? string.Empty).TrimEnd(); }
            set { _InvAss.Inv.Size_1 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_2
        {
            get { return (_InvAss.Inv.Size_2 ?? string.Empty).TrimEnd(); }
            set { _InvAss.Inv.Size_2 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_3
        {
            get { return (_InvAss.Inv.Size_3 ?? string.Empty).TrimEnd(); }
            set { _InvAss.Inv.Size_3 = (value ?? string.Empty).TrimEnd(); }
        }
        public string PackSize
        {
            get { return (_InvAss.Inv.PackSize ?? string.Empty).TrimEnd(); }
            set { _InvAss.Inv.PackSize = (value ?? string.Empty).TrimEnd(); }
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

        public void init(IS5InvAssembled data)
        {
            _InvAss = data ?? new S5InvAssembled();
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
