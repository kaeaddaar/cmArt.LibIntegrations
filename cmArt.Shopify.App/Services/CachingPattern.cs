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
    public class CachingPattern
    {
        private string _RootName;
        private StaticSettings _Settings;
        private IEnumerable<Shopify_Product> prev;
        private const int _RecordsPerPage = 10000;

        public CachingPattern(string RootName, StaticSettings settings)
        {
            _Settings = settings;
            _RootName = RootName ?? string.Empty;
            if (_RootName.Count() < 1) { throw new Exception("RootName must have at least 1 character."); }
        }

        public IEnumerable<Shopify_Product> _01_GetPrev()
        {
            prev = GenericSerialization<Shopify_Product>.ReadOrDeserializeTable
                (_RootName, _Settings.CachedFiles, _RecordsPerPage);
            return prev ?? new List<Shopify_Product>();
        }
        public void _02_SaveNewestToCache(IEnumerable<Shopify_Product> curr)
        {
            curr = curr ?? new List<Shopify_Product>();
            GenericSerialization<Shopify_Product>.SerializeToJSON(curr.ToList(), _RootName, _Settings.CachedFiles, _RecordsPerPage);
        }

    }

}
