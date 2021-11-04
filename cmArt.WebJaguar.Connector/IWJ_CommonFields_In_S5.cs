using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector
{
    public interface IWJ_CommonFields_In_S5
    {
        string upc { get; set; }//barcodes
        string shortDesc { get; set; }//Description
        int sku { get; set; }//InvUnique
        string name { get; set; }//PartNumber
        string longDesc { get; set; }//WebDescription
        float weight { get; set; }//weight
    }

}
