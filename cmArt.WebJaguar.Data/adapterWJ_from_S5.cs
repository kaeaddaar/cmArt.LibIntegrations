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
        public string shortDesc { get => _CommonFields.Description; set => _CommonFields.Description = value; }
        public string sku 
        {
            get
            {
                return _CommonFields.InvUnique.ToString();
            }
            set 
            {
                int result;
                int.TryParse(value, out result);
                _CommonFields.InvUnique = result;
            }
        }
        public string name { get => _CommonFields.PartNumber; set => _CommonFields.PartNumber = value; }
        public string longDesc { get => _CommonFields.WebDescription; set => _CommonFields.WebDescription = value; }
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
        public string field12//Sub Category
        {
            get { return _CommonFields.Cat ?? string.Empty; }
            set { _CommonFields.Cat = value ?? string.Empty; }
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
