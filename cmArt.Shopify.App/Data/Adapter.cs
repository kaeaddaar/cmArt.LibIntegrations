﻿using cmArt.Shopify.Connector.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace cmArt.Shopify.App.Data
{
    public class ProductAdapter : IShopify_Product
    {
        Product_Product _data;
        public ProductAdapter()
        {
            _data = _data ?? new Product_Product();
        }
        public void Init(Product_Product AdaptFrom)
        {
            _data = AdaptFrom ?? new Product_Product();
        }
        public string Description
        {
            get
            {
                return _data.body_html;
            }
            set
            {
                _data.body_html = value;
            }
        }
        public decimal WholesaleCost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Cat
        {
            get
            {
                return _data.product_type;
            }
            set
            {
                _data.product_type = value;
            }
        }
        public int InvUnique
        {
            get 
            {
                int tmp = 0;
                int.TryParse(_data.handle, out tmp);
                return tmp;
            }
            set
            {
                _data.handle = value.ToString();
            }
        }
        public string PartNumber
        {
            get
            {
                return _data.title;
            }
            set
            {
                _data.title = value;
            }
        }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return IShopify_ProductExtensions.CopyFrom(this, IFrom);
        }

        public bool Equals(IShopify_Product compareTo)
        {
            return IShopify_ProductExtensions.Equals(this, compareTo);
        }
    }
}