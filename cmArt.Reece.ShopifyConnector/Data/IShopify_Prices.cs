using cmArt.LibIntegrations.ETLPatternService;
using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Prices : IShopify_Identity, IEquality_cm<IShopify_Prices>, IDiffable<IShopify_Prices>
    {
        IEnumerable<S5PricePair> Prices { get; set; }
        //decimal WholesaleCost { get; set; }
    }
}
