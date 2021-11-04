using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    /// <summary>
    /// Representation of data for only the common fields between WebJaguar Product and System Five Inventory
    /// We may not use this at all as we can adapt stright from IS5InventoryAssembled to IProduct_Root
    /// </summary>
    public class S5_CommonFields : IS5_CommonFields_In_WJ
    {
        private float _weight; //weight
        private string _PartNumber; //name
        private string _WebDescription; //longDesc
        private string _Description; //shortDesc
        private IEnumerable<string> _barcodes; //upc
        private int _InvUnique; //sku
        public S5_CommonFields()
        {
            _barcodes = _barcodes ?? new List<string>();
        }

        public float weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string WebDescription
        {
            get { return _WebDescription; }
            set { _WebDescription = value ?? string.Empty; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value ?? string.Empty; }
        }
        public IEnumerable<string> barcodes
        {
            get { return _barcodes ?? new List<string>(); }
            set { _barcodes = value ?? new List<string>(); }
        }
        public int InvUnique
        {
            get { return _InvUnique; }
            set { _InvUnique = value; }
        }
        public string PartNumber
        {
            get { return _PartNumber; }
            set { _PartNumber = value ?? string.Empty; }
        }

    }
}
