using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class WJ_CommonFields : IWJ_CommonFields_In_S5
    {
        private List<int> _catIds;
        public WJ_CommonFields()
        {
            _catIds = _catIds ?? new List<int>();
        }
        public string upc { get; set; }//barcodes
        public string shortDesc { get; set; }//Description
        public string sku { get; set; }//InvUnique
        public string name { get; set; }//PartNumber
        public string longDesc { get; set; }//WebDescription
        public float weight { get; set; }//weight
        public List<int> catIds
        {
            get
            {
                return _catIds;
            }
            set
            {
                _catIds = value ?? _catIds ?? new List<int>();
            }
        }
        public string field12 { get; set; }//Sub Category
        public decimal inventory { get; set; }//inventory in stock qty
        public decimal inventoryAFS { get; set; }//inventory available qty
        public double cost { get; set; }//WholesaleCost
        public double priceTable1 { get; set; }//schedule 0
        public double priceTable2 { get; set; }//schedule 1
        public double priceTable3 { get; set; }//schedule 2
        public double priceTable4 { get; set; }//schedule 3
        public double priceTable5 { get; set; }//schedule 4
        public double priceTable6 { get; set; }//schedule 5
        public double priceTable7 { get; set; }//schedule 6
        public double priceTable8 { get; set; }//schedule 7
        public double priceTable9 { get; set; }//schedule 8
        public double priceTable10 { get; set; }//schedule9
    }
}
