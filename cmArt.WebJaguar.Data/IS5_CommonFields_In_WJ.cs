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
        new int InvUnique { get; set; }//sku
        new string PartNumber { get; set; }//name
        string WebDescription { get; set; }//longDesc
        float weight { get; set; }//weight
        new string Cat { get; set; }//Needs to Translate to List of Category Ids in WebJaguar
        string FF22 { get; set; }
        decimal WholesaleCost { get; set; }
        IEnumerable<S5PricePair> Prices { get; set; }
        IEnumerable<S5QtyPair> Quantities { get; set; }
        string Units { get; set; }
        string Size_1 { get; set; }//Promo
        string Size_2 { get; set; }//Size
        string Size_3 { get; set; }//Count
        string PackSize { get; set; }//Units Per Case
    }
}