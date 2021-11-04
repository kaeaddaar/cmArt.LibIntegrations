using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class WJ_CommonFields : IWJ_CommonFields_In_S5
    {
        public string upc { get; set; }//barcodes
        public string shortDesc { get; set; }//Description
        public int sku { get; set; }//InvUnique
        public string name { get; set; }//PartNumber
        public string longDesc { get; set; }//WebDescription
        public float weight { get; set; }//weight
    }
}
