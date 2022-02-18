using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class tmpShopify_Quantities
    {
        public tmpShopify_Quantities()
        {
            Quantities = new List<tmpS5QtyPair>();
        }
        public List<tmpS5QtyPair> Quantities { get; set; }
        public string partNumber { get; set; }
        public int InvUnique { get; set; }
        public string Cat { get; set; }
        public Shopify_Quantities AsShopify_Quantities()
        {
            Shopify_Quantities shopify_Quantities = new Shopify_Quantities();
            shopify_Quantities.Cat = this.Cat;
            shopify_Quantities.InvUnique = this.InvUnique;
            shopify_Quantities.PartNumber = this.partNumber;
            shopify_Quantities.Quantities = this.Quantities.Select(q => q.AsS5QtyPair());
            return shopify_Quantities;
        }
        public void FixShopifyLocations()
        {
            Quantities = Quantities ?? new List<tmpS5QtyPair>();
            foreach (tmpS5QtyPair pair in Quantities)
            {
                if (pair.Location == "62675222726") { pair.Location = "1"; }
                if (pair.Location == "63449497798") { pair.Location = "2"; }
                if (pair.Location == "63449530566") { pair.Location = "3"; }
                if (pair.Location == "63449563334") { pair.Location = "4"; }

            }
        }

    }
}
