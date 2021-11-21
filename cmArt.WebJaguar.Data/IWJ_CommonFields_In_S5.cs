using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    public interface IWJ_CommonFields_In_S5
    {
        decimal inventory { get; set; }//inventory in stock (same as AFS for this scenario I think)
        decimal inventoryAFS { get; set; }//inventory available
        string upc { get; set; }//barcodes
        string shortDesc { get; set; }//Description
        string sku { get; set; }//InvUnique
        string name { get; set; }//PartNumber
        string longDesc { get; set; }//WebDescription
        float weight { get; set; }//weight
        List<int> catIds { get; set; }//FF for CatIds
        string field9 { get; set; }//Part Number
        string field12 { get; set; }//Sub Category
        string field13 { get; set; }//Unique ID
        double cost { get; set; }//WholsaleCost
        double priceTable1 { get; set; }//schedule 0 price
        double priceTable2 { get; set; }//schedule 1 price
        double priceTable3 { get; set; }//schedule 2 price
        double priceTable4 { get; set; }//schedule 3 price
        double priceTable5 { get; set; }//schedule 4 price
        double priceTable6 { get; set; }//schedule 5 price
        double priceTable7 { get; set; }//schedule 6 price
        double priceTable8 { get; set; }//schedule 7 price
        double priceTable9 { get; set; }//schedule 8 price
        double priceTable10 { get; set; }//schedule 9 price

    }

}
