using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;
using cmArt.LibIntegrations.SerializationService;
using cmArt.Reece.ShopifyConnector;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.Shopify.App.Services
{
    public class CachingPattern_Shopify_Prices
    {
        private string _RootName;
        private StaticSettings _Settings;
        private IEnumerable<Shopify_Prices> prev;
        private const int _RecordsPerPage = 10000;

        public CachingPattern_Shopify_Prices(string RootName, StaticSettings settings)
        {
            _Settings = settings;
            _RootName = RootName ?? string.Empty;
            if (_RootName.Count() < 1) { throw new Exception("RootName must have at least 1 character."); }
        }

        public IEnumerable<Shopify_Prices> _01_GetPrev()
        {
            prev = GenericSerialization<Shopify_Prices>.ReadOrDeserializeTable
                (_RootName, _Settings.CachedFiles, _RecordsPerPage);
            return prev ?? new List<Shopify_Prices>();
        }
        public void _02_SaveNewestToCache(IEnumerable<Shopify_Prices> curr)
        {
            curr = curr ?? new List<Shopify_Prices>();
            GenericSerialization<Shopify_Prices>.SerializeToJSON(curr.ToList(), _RootName, _Settings.CachedFiles, _RecordsPerPage);
        }

    }

}
