﻿using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ETLPatternService;
using cmArt.LibIntegrations.ReportService;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public class Shopify_Product_LeftSideAdapter : IShopify_Product
    {
        private Shopify_Product _S5;
        private Shopify_Product _Shopify;

        public string Description { get => ((IShopify_Product)_S5).Description; set => ((IShopify_Product)_S5).Description = value; }
        //public decimal WholesaleCost { get => ((IShopify_Product)_S5).WholesaleCost; set => ((IShopify_Product)_S5).WholesaleCost = value; }
        public string WebCategory { get => ((IShopify_Product)_S5).WebCategory; set => ((IShopify_Product)_S5).WebCategory = value; }
        public string Cat { get => ((IShopify_Identity)_S5).Cat; set => ((IShopify_Identity)_S5).Cat = value; }
        public int InvUnique { get => ((IShopify_Identity)_S5).InvUnique; set => ((IShopify_Identity)_S5).InvUnique = value; }
        public string PartNumber { get => ((IShopify_Identity)_S5).PartNumber; set => ((IShopify_Identity)_S5).PartNumber = value; }

        public Shopify_Product_LeftSideAdapter()
        {
            _Init();
        }
        public Shopify_Product_LeftSideAdapter(Generic_Pair<Shopify_Product> shopify_Product_Pair)
        {
            _Init(shopify_Product_Pair);
        }
        private void _Init(Generic_Pair<Shopify_Product> shopify_Product_Pair)
        {
            _S5 = shopify_Product_Pair.S5 ?? new Shopify_Product();
            _Shopify = shopify_Product_Pair.External ?? new Shopify_Product();
        }
        private void _Init()
        {
            _S5 = _S5 ?? new Shopify_Product();
            _Shopify = _Shopify ?? new Shopify_Product();
        }
        public void Init(Generic_Pair<Shopify_Product> shopify_Product_Pair)
        {
            _Init(shopify_Product_Pair);
        }

        public bool cmEquals(IShopify_Product compareTo)
        {
            return ((IEquality_cm<IShopify_Product>)_S5).Equals(compareTo);
        }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return ((ICopyable<IShopify_Product>)_S5).CopyFrom(IFrom);
        }

        public IEnumerable<Changes_View> Diff(IShopify_Product CompareTo)
        {
            return IShopifyDataLoadFormatExtensions.Diff(this, CompareTo);
        }
    }
}
