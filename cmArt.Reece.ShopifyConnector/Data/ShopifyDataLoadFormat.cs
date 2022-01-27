using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cmArt.LibIntegrations;
using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.LibIntegrations.ReportService;
using FileHelpers;


namespace cmArt.Reece.ShopifyConnector
{
    [DelimitedRecord(",")]
    public class ShopifyDataLoadFormat : ICopyable<IShopifyDataLoadFormat>, IShopifyDataLoadFormat
    {
        private List<S5PricePair> prices;
        private List<S5QtyPair> quantities;

        public ShopifyDataLoadFormat()
        {
            prices = new List<S5PricePair>();
            quantities = new List<S5QtyPair>();
        }
        public IShopifyDataLoadFormat CopyFrom(IShopifyDataLoadFormat IFrom)
        {
            this.Cat = IFrom.Cat;
            this.InvUnique = IFrom.InvUnique;
            this.PartNumber = IFrom.PartNumber;
            this.Description = IFrom.Description;
            //this.WholesaleCost = IFrom.WholesaleCost;
            this.WebCategory = IFrom.WebCategory;
            foreach (var p in IFrom.Prices)
            {
                this.prices.Add(p);
            }
            return this;
        }

        public bool cmEquals(IShopifyDataLoadFormat compareTo)
        {
            return IShopifyDataLoadFormatExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Product compareTo)
        {
            return IShopifyDataLoadFormatExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Prices compareTo)
        {
            return IShopifyDataLoadFormatExtensions.Equals(this, compareTo);
        }

        public bool cmEquals(IShopify_Quantities compareTo)
        {
            return IShopifyDataLoadFormatExtensions.Equals(this, compareTo);
        }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return IShopify_ProductExtensions.CopyFrom(this, IFrom);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Product CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Prices CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Quantities CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }

        public IEnumerable<Changes_View> Diff(IShopifyDataLoadFormat CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
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
        public IEnumerable<S5PricePair> Prices 
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
                    prices.Add(new S5PricePair(price.Level, price.Price));
                }
            }
        }

        public IEnumerable<S5QtyPair> Quantities
        {
            get
            {
                return (quantities ?? new List<S5QtyPair>()).AsEnumerable();
            }
            set
            {
                quantities = quantities ?? new List<S5QtyPair>();
                quantities.RemoveAll((x) => { return true; });
                foreach (var qtyPair in value)
                {
                    quantities.Add(new S5QtyPair(qtyPair.Location, qtyPair.Qty));
                }
            }
        }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string WebCategory { get; set; }
    }

}
