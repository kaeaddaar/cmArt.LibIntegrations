using cmArt.LibIntegrations;
using cmArt.Portal.API6.Data;
using cmArt.Portal.Data6;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.API6.Models
{
    public class WebInventoryClient : WebInventory, ICopyable<IWebInventory>, ICopyableHttpRequest<IWebInventory>
    {
        public IWebInventory CopyFrom(IWebInventory FromData)
        {
            IWebInventory _From = FromData ?? new WebInventoryClient();
            this.Cat = FromData.Cat;
            this.Description = FromData.Description;
            this.ImageUrl = FromData.ImageUrl;
            this.InvUnique = FromData.InvUnique;
            this.PartNumber = FromData.PartNumber;
            this.Prices = FromData.Prices;
            this.Quantities = FromData.Quantities;
            this.WebCategory = FromData.WebCategory;
            return this;
        }


        public IWebInventory CopyFrom(HttpRequest req, dynamic data)
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
    }
}
