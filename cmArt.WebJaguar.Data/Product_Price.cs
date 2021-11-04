using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product_Price
    {
        public double amt { get; set; }
        public double discountAmt { get; set; }
        public int qtyFrom { get; set; }
        public int qtyTo { get; set; }
        public double caseAmt { get; set; }
        public decimal caseDiscountAmt { get; set; }
        public decimal cost { get; set; }
    }





}
