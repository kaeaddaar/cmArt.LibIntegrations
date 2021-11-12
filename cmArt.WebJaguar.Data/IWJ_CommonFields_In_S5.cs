using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    public interface IWJ_CommonFields_In_S5
    {
        string upc { get; set; }//barcodes
        string shortDesc { get; set; }//Description
        string sku { get; set; }//InvUnique
        string name { get; set; }//PartNumber
        string longDesc { get; set; }//WebDescription
        float weight { get; set; }//weight
        List<int> catIds { get; set; }//FF for CatIds
        string field12 { get; set; }//Sub Category
    }

}
