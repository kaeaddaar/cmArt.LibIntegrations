//using cmArt.LibIntegrations;
//using cmArt.LibIntegrations.ClientControllerService;
using cmArt.LibIntegrations.ClientControllerService;
using cmArt.LibIntegrations.ReportService;
using cmArt.Portal.Data6;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data.InventoryData
{
    public class WebInventory : IWebInventory, IIndex<int>
    {
        private ShopifyDataLoadFormat _Inventory;
        private ShopifyDataLoadFormat Inventory
        {
            get { return _Inventory ?? new ShopifyDataLoadFormat(); }
            set { _Inventory = value ?? _Inventory ?? new ShopifyDataLoadFormat(); }
        }

        public WebInventory()
        {
            Inventory = new ShopifyDataLoadFormat();
        }
        public void Init(ShopifyDataLoadFormat data)
        {
            Inventory = data ?? Inventory ?? new ShopifyDataLoadFormat();
        }

        public bool IsEmpty(int value)
        {
            return this.InvUnique == 0;
        }

        public int GetIndex()
        {
            return this.InvUnique;
        }

        //public IEnumerable<int> GetXRefValues()
        //{
        //    throw new NotImplementedException();
        //}
        //public int GetIndex()
        //{
        //    return this.InvUnique;
        //}
        //public bool IsEmpty(int value)
        //{
        //    return value == 0;
        //}

        public string ImageUrl { get; set; }
        public string Description
        {
            get { return Inventory.Description; }
            set { Inventory.Description = value ?? String.Empty; }
        }
        public string WebCategory
        {
            get { return Inventory.WebCategory; }
            set { Inventory.WebCategory = value ?? String.Empty; }
        }
        public IEnumerable<S5PricePair> Prices
        {
            get { return Inventory.Prices ?? new List<S5PricePair>(); }
            set { Inventory.Prices = value ?? Inventory.Prices ?? new List<S5PricePair>(); }
        }
        public IEnumerable<S5QtyPair> Quantities
        {
            get { return Inventory.Quantities; }
            set { Inventory.Quantities = value ?? Inventory.Quantities ?? new List<S5QtyPair>(); }
        }
        public string Cat
        {
            get { return Inventory.Cat ?? String.Empty; }
            set { Inventory.Cat = value ?? Inventory.Cat ?? String.Empty; }
        }
        public int InvUnique
        {
            get { return Inventory.InvUnique; }
            set { Inventory.InvUnique = value; }
        }
        public string PartNumber
        {
            get { return Inventory.PartNumber ?? String.Empty; }
            set { Inventory.PartNumber = value ?? Inventory.PartNumber ?? String.Empty; }
        }

    }
}
