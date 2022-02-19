using cmArt.LibIntegrations.ClientControllerService;
using cmArt.LibIntegrations.ReportService;
using cmArt.Reece.ShopifyConnector;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data.OnlineInventory
{
    public class WebInventory : IWebInventory, IXRef<int>, ICopyable<WebInventory>, ICopyableHttpRequest<WebInventory>
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

        public IEnumerable<int> GetXRefValues()
        {
            throw new NotImplementedException();
        }

        public int GetPrimraryKey()
        {
            return this.InvUnique;
        }

        public WebInventory CopyFrom(WebInventory FromData)
        {
            WebInventory _From = FromData ?? new WebInventory();
            this.Cat = FromData.Cat;
            this.Description = FromData.Description;
            this.ImageLocation = FromData.ImageLocation;
            this.InvUnique = FromData.InvUnique;
            this.PartNumber = FromData.PartNumber;
            this.Prices = FromData.Prices;
            this.Quantities = FromData.Quantities;
            this.WebCategory = FromData.WebCategory;
            return this;
        }

        public WebInventory CopyFrom(HttpRequest req, dynamic data)
        {
            this.Cat = utils.GetValue(req, data?.Cat, "Cat");
            this.Description = utils.GetValue(req, data?.Description, "Description");
            this.ImageLocation = utils.GetValue(req, data?.ImageLocation, "ImageLocation");
            this.InvUnique = utils.StringToInt(utils.GetValue(req, data?.InvUnique, "InvUnique"));
            this.PartNumber = utils.GetValue(req, data?.PartNumber, "PartNumber");
            this.Prices = utils.JsonToPrices(req, data?.Prices, "Prices");
            this.Quantities = utils.JsonToQuantities(req, data?.Quantities, "Quantities");
            this.WebCategory = utils.GetValue(req, data?.WebCategory, "WebCategory");

            return this;
        }

        public string ImageLocation { get; set; }
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
