using System.Collections.Generic;

namespace cmArt.WebJaguar.App.Data
{
    /// <summary>
    /// System Five (S5) fields common to Web Jaguar (WJ)
    /// </summary>
    public interface IS5_CommonFields_In_WJ
    {
        IEnumerable<string> barcodes { get; set; }//upc
        string Description { get; set; }//shortDesc
        int InvUnique { get; set; }//sku
        string PartNumber { get; set; }//name
        string WebDescription { get; set; }//longDesc
        float weight { get; set; }//weight
    }
}