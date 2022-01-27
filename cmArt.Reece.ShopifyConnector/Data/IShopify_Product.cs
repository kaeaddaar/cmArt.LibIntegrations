using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ETLPatternService;
using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Product : IShopify_Identity, LibIntegrations.ETLPatternService.IEquality_cm<IShopify_Product>, ICopyable<IShopify_Product>, IDiffable<IShopify_Product>
    {
        string Description { get; set; }
        //decimal WholesaleCost { get; set; }
        string WebCategory { get; set; }
    }
}
