using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Product : IShopify_Identity, IEquality<IShopify_Product>, ICopyable<IShopify_Product>, IDiffable<IShopify_Product>
    {
        string Description { get; set; }
        //decimal WholesaleCost { get; set; }
        string WebCategory { get; set; }
    }
}
