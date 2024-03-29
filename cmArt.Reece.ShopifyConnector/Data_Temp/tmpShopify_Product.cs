﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class tmpShopify_Product
    {
        private string _Description = string.Empty;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = FixEncoding(value ?? string.Empty);
            }
        }

        public decimal WholesaleCost { get; set; }
        public string Cat { get; set; }
        private int _InvUnique;
        public int InvUnique 
        { 
            get
            {
                return _InvUnique;
            }
            set
            {
                _InvUnique = value;
            }
        }
        public string WebCategory { get; set; }

        private string _PartNumber = string.Empty;
        public string PartNumber 
        {
            get
            {
                return _PartNumber;
            }
            set
            {
                _PartNumber = FixEncoding(value ?? string.Empty);
            }
        }
        private string FixEncoding(string EncodedString)
        {
            string tmp = EncodedString;
            tmp = tmp.Replace("&amp;", "&");
            return tmp;
        }
        public Shopify_Product AsShopify_Product()
        {
            Shopify_Product shopify_Product = new Shopify_Product();
            shopify_Product.Cat = this.Cat;
            shopify_Product.Description = this.Description;
            
            shopify_Product.InvUnique = this.InvUnique;

            shopify_Product.PartNumber = this.PartNumber;
            //shopify_Product.WholesaleCost = this.WholesaleCost;
            shopify_Product.WebCategory = this.WebCategory;

            return shopify_Product;
        }
    }
}
