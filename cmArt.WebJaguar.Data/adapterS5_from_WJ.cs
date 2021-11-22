using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class adapterS5_from_WJ : IS5_CommonFields_In_WJ
    {
        private IWJ_CommonFields_In_S5 _WJ;
        public adapterS5_from_WJ()
        {
            _WJ = new WJ_CommonFields();
        }
        public void init(IWJ_CommonFields_In_S5 data)
        {
            _WJ = data ?? new WJ_CommonFields();
        }
        public IEnumerable<string> barcodes
        {
            get
            {
                string _upc = _WJ.upc ?? string.Empty;
                return _upc.Split(',');
            }
            set
            {
                IEnumerable<string> _value = value ?? new List<string>();
                _WJ.upc = string.Join(",", _value);
            }
        }
        public string Description
        {
            get
            {
                return _WJ.name;
            }
            set
            {
                _WJ.name = value ?? string.Empty;
            }
        }
        public int InvUnique
        {
            get
            {
                int result;
                int.TryParse(_WJ.sku, out result);
                return result;
            }
            set
            {
                _WJ.sku = value.ToString();
            }
        }
        public string PartNumber
        {
            get
            {
                return _WJ.shortDesc;
            }
            set
            {
                _WJ.shortDesc = value ?? string.Empty;
            }
        }
        public string WebDescription
        {
            get
            {
                return _WJ.longDesc;
            }
            set
            {
                _WJ.longDesc = value ?? string.Empty;
            }
        }
        public float weight
        {
            get
            {
                return _WJ.weight;
            }
            set
            {
                _WJ.weight = value;
            }
        }

        public string Cat
        {
            get
            {
                return _WJ.field12 ?? string.Empty;
            }
            set
            {
                _WJ.field12 = value ?? "000";
            }
        }
        public string FF22
        {
            get
            {
                IEnumerable<int> tmpIds = _WJ.catIds ?? new List<int>();
                IEnumerable<string> tmpStrIds = tmpIds.Select(x => x.ToString());
                string results = string.Join(",", tmpStrIds);
                return results;
            }
            set
            {
                string strIds = value ?? string.Empty;
                IEnumerable<string> tmpStrIds = strIds.Split(',');
                List<int> tmpIds = new List<int>();
                foreach (var StrId in tmpStrIds)
                {
                    int tmp;
                    int.TryParse(StrId, out tmp);
                    tmpIds.Add(tmp);
                }
                _WJ.catIds = tmpIds;
            }
        }

        public decimal WholesaleCost
        { 
            get
            {
                return (decimal)_WJ.cost;
            }
            set
            {
                _WJ.cost = (double)value;
            }
        }
        public IEnumerable<S5PricePair> Prices
        {
            get
            {
                List<S5PricePair> tmpPrices = new List<S5PricePair>();
                tmpPrices.Add(new S5PricePair(0, (decimal)_WJ.priceTable1));
                tmpPrices.Add(new S5PricePair(1, (decimal)_WJ.priceTable2));
                tmpPrices.Add(new S5PricePair(2, (decimal)_WJ.priceTable3));
                tmpPrices.Add(new S5PricePair(3, (decimal)_WJ.priceTable4));
                tmpPrices.Add(new S5PricePair(4, (decimal)_WJ.priceTable5));
                tmpPrices.Add(new S5PricePair(5, (decimal)_WJ.priceTable6));
                tmpPrices.Add(new S5PricePair(6, (decimal)_WJ.priceTable7));
                tmpPrices.Add(new S5PricePair(7, (decimal)_WJ.priceTable8));
                tmpPrices.Add(new S5PricePair(8, (decimal)_WJ.priceTable9));
                tmpPrices.Add(new S5PricePair(9, (decimal)_WJ.priceTable10));
                return tmpPrices;
            }
            set
            {
                IEnumerable<S5PricePair> tmpPairs = value ?? new List<S5PricePair>();

                double P0 = (double)((tmpPairs.Where(x => x.Level == 0).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P1 = (double)((tmpPairs.Where(x => x.Level == 1).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P2 = (double)((tmpPairs.Where(x => x.Level == 2).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P3 = (double)((tmpPairs.Where(x => x.Level == 3).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P4 = (double)((tmpPairs.Where(x => x.Level == 4).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P5 = (double)((tmpPairs.Where(x => x.Level == 5).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P6 = (double)((tmpPairs.Where(x => x.Level == 6).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P7 = (double)((tmpPairs.Where(x => x.Level == 7).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P8 = (double)((tmpPairs.Where(x => x.Level == 8).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P9 = (double)((tmpPairs.Where(x => x.Level == 9).FirstOrDefault()) ?? new S5PricePair()).Price;
                double P10 = (double)((tmpPairs.Where(x => x.Level == 10).FirstOrDefault()) ?? new S5PricePair()).Price;

            }
        }
        public IEnumerable<S5QtyPair> Quantities
        {
            get
            {
                List<S5QtyPair> tmpQuantities = new List<S5QtyPair>();
                tmpQuantities.Add(new S5QtyPair(0, _WJ.inventoryAFS));
                return tmpQuantities;
            }
            set
            {
                IEnumerable<S5QtyPair> tmpQuantities = value ?? new List<S5QtyPair>();
                decimal total = tmpQuantities.Sum(x => x.Qty);
                _WJ.inventoryAFS = total;
                _WJ.inventory = total;
            }
        }

        public string Units
        {
            get { return (_WJ.field2 ?? string.Empty).TrimEnd(); }
            set { _WJ.field2 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_1
        {
            get { return (_WJ.field8 ?? string.Empty).TrimEnd(); }
            set { _WJ.field8 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_2
        {
            get { return (_WJ.field7 ?? string.Empty).TrimEnd(); }
            set { _WJ.field7 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_3
        {
            get { return (_WJ.field5 ?? string.Empty).TrimEnd(); }
            set { _WJ.field5 = (value ?? string.Empty).TrimEnd(); }
        }
        public string PackSize
        {
            get { return (_WJ.field1 ?? string.Empty).TrimEnd(); }
            set { _WJ.field1 = (value ?? string.Empty).TrimEnd(); }
        }
    }
}
