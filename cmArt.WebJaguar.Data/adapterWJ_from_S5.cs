﻿using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class adapterWJ_from_S5 : IWJ_CommonFields_In_S5
    {
        protected IS5_CommonFields_In_WJ _CommonFields { get; set; }
        public string upc { get => string.Join(",", _CommonFields.barcodes); set => _CommonFields.barcodes = value.Split(',').AsEnumerable(); }
        public string shortDesc { get => _CommonFields.PartNumber; set => _CommonFields.PartNumber = _CommonFields.PartNumber; }//Read Only
        public string sku 
        {
            get
            {
                return _CommonFields.InvUnique.ToString();
            }
            set 
            {
                //int result;
                //int.TryParse(value, out result);
                //_CommonFields.InvUnique = result;
                _CommonFields.InvUnique = _CommonFields.InvUnique;
            }
        }//Read Only
        public string name { get => _CommonFields.Description; set => _CommonFields.Description = value ?? string.Empty; }
        public string longDesc { get => _CommonFields.WebDescription; set => _CommonFields.WebDescription = value ?? string.Empty; }
        public float weight { get => _CommonFields.weight; set => _CommonFields.weight = value; }
        public List<int> catIds//Inventory Free Form field 22 (22 is Marked For Deletion)
        {
            get
            {
                string strIds = _CommonFields.FF22 ?? string.Empty;
                IEnumerable<string> tmpStrIds = strIds.Split(',');
                List<int> tmpIds = new List<int>();
                foreach (var StrId in tmpStrIds)
                {
                    int tmp;
                    int.TryParse(StrId, out tmp);
                    tmpIds.Add(tmp);
                }
                return tmpIds;
            }
            set
            {
                IEnumerable<int> tmpIds = value ?? new List<int>();
                IEnumerable<string> tmpStrIds = tmpIds.Select(x => x.ToString());
                string results = string.Join(",", tmpStrIds);
                _CommonFields.FF22 = results;
            }
        }
        public string field9//Part Number
        {
            get { return (_CommonFields.PartNumber ?? string.Empty).TrimEnd(); }
            set { _CommonFields.PartNumber = (value ?? string.Empty).TrimEnd(); }
        }
        public string field13//Unique ID
        {
            get { return _CommonFields.InvUnique.ToString() ?? string.Empty; }
            set 
            {
                int tmp;
                int.TryParse(value ?? string.Empty, out tmp);
                bool WeHaveAUniqueToAssign = tmp > 0;
                if (WeHaveAUniqueToAssign)
                {
                    _CommonFields.InvUnique = tmp;
                }
                bool WeAreTryingToResetInvUnique = (value == "0");
                if (WeAreTryingToResetInvUnique)
                {
                    _CommonFields.InvUnique = 0;
                }
            }
        }
        public string field12//Sub Category
        {
            get { return _CommonFields.Cat ?? string.Empty; }
            set { _CommonFields.Cat = value ?? string.Empty; }
        }

        public decimal inventory
        {
            get
            {
                return _CommonFields.Quantities.Sum(x => x.Qty);
            }
            set
            {
                List<S5QtyPair> tmpQuantities = new List<S5QtyPair>();
                tmpQuantities.Add(new S5QtyPair(0, value));
                _CommonFields.Quantities = tmpQuantities;
            }
        }

        public decimal inventoryAFS
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }
        public double cost
        {
            get
            {
                return (double)_CommonFields.WholesaleCost;
            }
            set
            {
                _CommonFields.WholesaleCost = (decimal)value;
            }
        }
        public double priceTable1
        {
            get
            {
                short sched = 0;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 0;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable2
        {
            get
            {
                short sched = 1;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 1;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable3
        {
            get
            {
                short sched = 2;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 2;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable4
        {
            get
            {
                short sched = 3;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 3;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable5
        {
            get
            {
                short sched = 4;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 4;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable6
        {
            get
            {
                short sched = 5;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 5;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable7
        {
            get
            {
                short sched = 6;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 6;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable8
        {
            get
            {
                short sched = 7;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 7;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable9
        {
            get
            {
                short sched = 8;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 8;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }
        public double priceTable10
        {
            get
            {
                short sched = 9;
                return (double)(_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price;
            }
            set
            {
                // WARNING: Ignores when price is missing. Performs assignment on value that will be tossed
                short sched = 9;
                (_CommonFields.Prices.Where(x => x.Level == sched).FirstOrDefault() ?? new S5PricePair()).Price = (decimal)value;
                Console.WriteLine($"priceTable1 Set failed due to price scheule {sched} being null");
            }
        }

        public string field1//WJ Units Per Case - S5 PackSize
        {
            get
            {
                return (_CommonFields.PackSize ?? string.Empty).TrimEnd();
            }
            set
            {
                _CommonFields.PackSize = (value ?? string.Empty).TrimEnd();
            }
        }
        public string field2//WJ Unit - S5 Units
        {
            get
            {
                return (_CommonFields.Units ?? string.Empty).TrimEnd();
            }
            set
            {
                _CommonFields.Units = (value ?? string.Empty).TrimEnd();
            }
        }
        public string field5//WJ Count - S5 Inventry.Size_3
        {
            get
            {
                return (_CommonFields.Size_3 ?? string.Empty).TrimEnd();
            }
            set
            {
                _CommonFields.Size_3 = (value ?? string.Empty).TrimEnd();
            }
        }
        public string field7//WJ Size - S5 Inventry.Size_2
        {
            get
            {
                return (_CommonFields.Size_2 ?? string.Empty).TrimEnd();
            }
            set
            {
                _CommonFields.Size_2 = (value ?? string.Empty).TrimEnd();
            }
        }
        public string field8//WJ Promo - S5 Inventry.Size_1
        {
            get
            {
                return (_CommonFields.Size_1 ?? string.Empty).TrimEnd();
            }
            set
            {
                _CommonFields.Size_1 = (value ?? string.Empty).TrimEnd();
            }
        }

        public adapterWJ_from_S5()
        {
            _CommonFields = new S5_CommonFields();
        }
        public void Init(IS5_CommonFields_In_WJ commonFields)
        {
            _CommonFields = commonFields;
        }

    }
}
