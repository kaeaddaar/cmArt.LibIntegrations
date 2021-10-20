using System.Collections.Generic;

namespace cmArt.WebJaguar.App.Data
{
    /// <summary>
    /// Web Jaguar (WJ) fields common to System Five (S5)
    /// </summary>
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