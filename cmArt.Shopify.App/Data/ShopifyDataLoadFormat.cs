using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cmArt.LibIntegrations;
using FileHelpers;


namespace cmArt.Shopify.App.Data
{
    [DelimitedRecord(",")]
    public class ShopifyDataLoadFormat : IShopifyCommonFields, ICopyable<IShopifyDataLoadFormat>, IShopifyDataLoadFormat
    {
        private List<pair> prices;
        public ShopifyDataLoadFormat()
        {
            prices = new List<pair>();
        }
        public IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom)
        {
            this.Cat = IFrom.Cat;
            this.InvUnique = IFrom.InvUnique;
            this.PartNumber = IFrom.PartNumber;
            this.Description = IFrom.Description;
            this.InStock = IFrom.InStock;
            this.WholesaleCost = IFrom.WholesaleCost;
            foreach (var p in IFrom.Prices)
            {
                this.prices.Add(p);
            }
            return this;
        }

        public int InvUnique { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Cat { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string PartNumber { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Description { get; set; }
        public decimal WholesaleCost { get; set; }
        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public decimal InStock { get; set; }
        public IEnumerable<pair> Prices 
        {
            get
            {
                return prices.AsEnumerable();
            }
            set
            {
                prices.RemoveAll((x) => { return true; });
                foreach(var price in value)
                {
                    prices.Add(new pair(price.Level, price.Price));
                }
            }
        }

    }

}
