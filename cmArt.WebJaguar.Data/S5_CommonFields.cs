using cmArt.Reece.ShopifyConnector;
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
        private string _Cat; // needs to map to list of category Ids in WebJaguar
        private string _FF22;
        private string _Units;
        private string _Weight;
        private string _Size_1;
        private string _Size_2;
        private string _Size_3;
        private string _PackSize;

        public string Cat
        {
            get { return (_Cat ?? string.Empty).TrimEnd(); }
            set { _Cat = (value ?? string.Empty).TrimEnd(); }
        }
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
            get { return (_Description ?? string.Empty).TrimEnd(); }
            set { _Description = (value ?? string.Empty).TrimEnd(); }
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
            get { return (_PartNumber ?? string.Empty).TrimEnd(); }
            set { _PartNumber = (value ?? string.Empty).TrimEnd(); }
        }
        public string FF22
        {
            get { return (_FF22 ?? string.Empty).TrimEnd(); }
            set { _FF22 = (value ?? string.Empty).TrimEnd(); }
        }

        public decimal WholesaleCost { get; set; }
        public IEnumerable<S5PricePair> Prices { get; set; }
        public IEnumerable<S5QtyPair> Quantities { get; set; }
        public string Units
        {
            get { return (_Units ?? string.Empty).TrimEnd(); }
            set { _Units = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_1
        {
            get { return (_Size_1 ?? string.Empty).TrimEnd(); }
            set { _Size_1 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_2
        {
            get { return (_Size_2 ?? string.Empty).TrimEnd(); }
            set { _Size_2 = (value ?? string.Empty).TrimEnd(); }
        }
        public string Size_3
        {
            get { return (_Size_3 ?? string.Empty).TrimEnd(); }
            set { _Size_3 = (value ?? string.Empty).TrimEnd(); }
        }
        public string PackSize
        {
            get { return (_PackSize ?? string.Empty).TrimEnd(); }
            set { _PackSize = (value ?? string.Empty).TrimEnd(); }
        }

        public IShopifyDataLoadFormat CopyFrom(IS5_CommonFields_In_WJ IFrom)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IS5_CommonFields_In_WJ compareTo)
        {
            throw new NotImplementedException();
        }
    }
}
