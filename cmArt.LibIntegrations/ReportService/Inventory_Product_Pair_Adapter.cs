using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ReportService
{
    public class Inventory_Pair_Adapter : IInventory_Pair_Flat
    {
        private Inventory_Product _S5;
        private Inventory_Product _Shopify;

        public string LeftDescription { get => ((IShopify_Product)_S5).Description; set => ((IShopify_Product)_S5).Description = value; }
        public string LeftCat { get => ((IShopify_Identity)_S5).Cat; set => ((IShopify_Identity)_S5).Cat = value; }
        public int LeftInvUnique { get => ((IShopify_Identity)_S5).InvUnique; set => ((IShopify_Identity)_S5).InvUnique = value; }
        public string LeftPartNumber { get => ((IShopify_Identity)_S5).PartNumber; set => ((IShopify_Identity)_S5).PartNumber = value; }

        public string RightDescription { get => ((IShopify_Product)_Shopify).Description; set => ((IShopify_Product)_Shopify).Description = value; }
        public string RightCat { get => ((IShopify_Identity)_Shopify).Cat; set => ((IShopify_Identity)_Shopify).Cat = value; }
        public int RightInvUnique { get => ((IShopify_Identity)_Shopify).InvUnique; set => ((IShopify_Identity)_Shopify).InvUnique = value; }
        public string RightPartNumber { get => ((IShopify_Identity)_Shopify).PartNumber; set => ((IShopify_Identity)_Shopify).PartNumber = value; }

        public Inventory_Product_Pair_Adapter()
        {
            _Init();
        }
        public Inventory_Product_Pair_Adapter(Generic_Pair<Shopify_Product> shopify_Product_Pair)
        {
            _Init(shopify_Product_Pair);
        }
        private void _Init(Generic_Pair<Shopify_Product> shopify_Product_Pair)
        {
            _S5 = shopify_Product_Pair.S5 ?? new Shopify_Product();
            _Shopify = shopify_Product_Pair.Shopify ?? new Shopify_Product();
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

    }

}
