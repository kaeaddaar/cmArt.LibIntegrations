using cmArt.LibIntegrations;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.ReportViews
{
    public class Shopify_Product_RightSideAdapter : IShopify_Product
    {
        private Shopify_Product _S5;
        private Shopify_Product _Shopify;

        public string Description { get => ((IShopify_Product)_Shopify).Description; set => ((IShopify_Product)_Shopify).Description = value; }
        //public decimal WholesaleCost { get => ((IShopify_Product)_Shopify).WholesaleCost; set => ((IShopify_Product)_Shopify).WholesaleCost = value; }
        public string WebCategory { get => ((IShopify_Product)_Shopify).WebCategory; set => ((IShopify_Product)_Shopify).WebCategory = value; }
        public string Cat { get => ((IShopify_Identity)_Shopify).Cat; set => ((IShopify_Identity)_Shopify).Cat = value; }
        public int InvUnique { get => ((IShopify_Identity)_Shopify).InvUnique; set => ((IShopify_Identity)_Shopify).InvUnique = value; }
        public string PartNumber { get => ((IShopify_Identity)_Shopify).PartNumber; set => ((IShopify_Identity)_Shopify).PartNumber = value; }

        public Shopify_Product_RightSideAdapter()
        {
            _Init();
        }
        public Shopify_Product_RightSideAdapter(Generic_Pair<Shopify_Product> shopify_Product_Pair)
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

        public bool Equals(IShopify_Product compareTo)
        {
            return ((IEquality<IShopify_Product>)_Shopify).Equals(compareTo);
        }

        public IShopify_Product CopyFrom(IShopify_Product IFrom)
        {
            return ((ICopyable<IShopify_Product>)_Shopify).CopyFrom(IFrom);
        }
    }
}
