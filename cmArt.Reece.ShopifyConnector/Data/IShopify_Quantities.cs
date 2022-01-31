using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ETLPatternService;
using cmArt.LibIntegrations.ReportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Quantities : IShopify_Identity, IEquality_cm<IShopify_Quantities>, IDiffable<IShopify_Quantities>, ICopyable<IShopify_Quantities>
    {
        IEnumerable<S5QtyPair> Quantities { get; set; }
    }
}
