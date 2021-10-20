using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.Data
{
    public class adapterWJ_from_S5 : IWJ_CommonFields_In_S5
    {
        protected IS5_CommonFields_In_WJ _CommonFields { get; set; }
        public string upc { get => string.Join(",", _CommonFields.barcodes); set => _CommonFields.barcodes = value.Split(',').AsEnumerable(); }
        public string shortDesc { get => _CommonFields.Description; set => _CommonFields.Description = value; }
        public int sku { get => _CommonFields.InvUnique; set => _CommonFields.InvUnique = value; }
        public string name { get => _CommonFields.PartNumber; set => _CommonFields.PartNumber = value; }
        public string longDesc { get => _CommonFields.WebDescription; set => _CommonFields.WebDescription = value; }
        public float weight { get => _CommonFields.weight; set => _CommonFields.weight = value; }

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
