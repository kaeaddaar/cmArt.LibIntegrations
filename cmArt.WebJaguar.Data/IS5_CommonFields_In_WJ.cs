using cmArt.Reece.ShopifyConnector;
using cmArt.System5.Inventory;
using System.Collections.Generic;

namespace cmArt.WebJaguar.Data
{
    /// <summary>
    /// System Five (S5) fields common to Web Jaguar (WJ)
    /// </summary>
    public interface IS5_CommonFields_In_WJ : IInventoryId
    {
        IEnumerable<string> barcodes { get; set; }//upc
        string Description { get; set; }//shortDesc
        int InvUnique { get; set; }//sku
        string PartNumber { get; set; }//name
        string WebDescription { get; set; }//longDesc
        float weight { get; set; }//weight
        string Cat { get; set; }//Needs to Translate to List of Category Ids in WebJaguar
        string FF22 { get; set; }
        decimal WholesaleCost { get; set; }
        IEnumerable<S5PricePair> Prices { get; set; }
        IEnumerable<S5QtyPair> Quantities { get; set; }

    }
}