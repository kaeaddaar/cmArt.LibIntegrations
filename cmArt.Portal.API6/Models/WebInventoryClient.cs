using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ClientControllerService;
using cmArt.Portal.API6.Data;
using cmArt.Portal.Data6;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.API6.Models
{
    public class WebInventoryClient : WebInventory, ICopyable<WebInventory>, ICopyableHttpRequest<WebInventory>, IAs_Type<WebInventory>, IIndex<int>
    {
        public WebInventory As_Type(Type type)
        {
            WebInventory result = new WebInventory();
            CopyFrom(result, this);
            return result;
        }

        public WebInventory CopyFrom(WebInventory FromData)
        {
            //IWebInventory _From = FromData ?? new WebInventoryClient();
            //this.Cat = FromData.Cat;
            //this.Description = FromData.Description;
            //this.ImageUrl = FromData.ImageUrl;
            //this.InvUnique = FromData.InvUnique;
            //this.PartNumber = FromData.PartNumber;
            //this.Prices = FromData.Prices;
            //this.Quantities = FromData.Quantities;
            //this.WebCategory = FromData.WebCategory;
            //return this;

            WebInventory _From = FromData ?? new WebInventory();
            CopyFrom(_From, FromData);
            return _From;
        }


        public WebInventory CopyFrom(HttpRequest req, dynamic data)
        {
            this.Cat = utils.GetValue(req, data?.Cat, "Cat");
            this.Description = utils.GetValue(req, data?.Description, "Description");
            this.ImageUrl = utils.GetValue(req, data?.ImageLocation, "ImageLocation");
            this.InvUnique = utils.StringToInt(utils.GetValue(req, data?.InvUnique, "InvUnique"));
            this.PartNumber = utils.GetValue(req, data?.PartNumber, "PartNumber");
            this.Prices = utils.JsonToPrices(req, data?.Prices, "Prices");
            this.Quantities = utils.JsonToQuantities(req, data?.Quantities, "Quantities");
            this.WebCategory = utils.GetValue(req, data?.WebCategory, "WebCategory");

            return this;
        }

        public int GetIndex()
        {
            return this.InvUnique;
        }

        public bool IsEmpty(int value)
        {
            return (this.InvUnique == 0);
        }

        private IWebInventory CopyFrom(IWebInventory ToData, IWebInventory FromData)
        {
            IWebInventory _From = FromData ?? new WebInventoryClient();
            ToData.Cat = FromData.Cat;
            ToData.Description = FromData.Description;
            ToData.ImageUrl = FromData.ImageUrl;
            ToData.InvUnique = FromData.InvUnique;
            ToData.PartNumber = FromData.PartNumber;
            ToData.Prices = FromData.Prices;
            ToData.Quantities = FromData.Quantities;
            ToData.WebCategory = FromData.WebCategory;
            return this;
        }
    }
}
