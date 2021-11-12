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
    }
}
