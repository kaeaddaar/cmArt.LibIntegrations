using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public class AdaptToIShopify_Product : IShopify_Product
    {
        private IShopifyDataLoadFormat _data;

        public string Description { get => _data.Description; set => _data.Description = value; }
        public decimal WholesaleCost { get => _data.WholesaleCost; set => _data.WholesaleCost = value; }
        public string Cat { get => _data.Cat; set => _data.Cat = value; }
        public int InvUnique { get => _data.InvUnique; set => _data.InvUnique = value; }
        public string PartNumber { get => _data.PartNumber; set => _data.PartNumber = value; }
    }
}
