using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product_Price
    {
        public int amt { get; set; }
        public int discountAmt { get; set; }
        public int qtyFrom { get; set; }
        public int qtyTo { get; set; }
        public int caseAmt { get; set; }
        public int caseDiscountAmt { get; set; }
        public int cost { get; set; }
    }





}
