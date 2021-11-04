using cmArt.WebJaguar.Connector;
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
                _WJ.upc = string.Join(',', _value);
            }
        }
        public string Description
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
        public int InvUnique
        {
            get
            {
                return _WJ.sku;
            }
            set
            {
                _WJ.sku = value;
            }
        }
        public string PartNumber
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
    }
}
