using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;
using cmArt.LibIntegrations.SerializationService;
using cmArt.WebJaguar.Connector;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cmArt.WebJaguar.App.Services
{
    public class CachingPattern
    {
        private string _RootName;
        private StaticSettings _Settings;
        private IEnumerable<Product_Root> prev;
        private const int _RecordsPerPage = 10000;

        public CachingPattern(string RootName, StaticSettings settings)
        {
            _Settings = settings;
            _RootName = RootName ?? string.Empty;
            if (_RootName.Count() < 1) { throw new Exception("RootName must have at least 1 character."); }
        }

        public IEnumerable<Product_Root> _01_GetPrev()
        {
            prev = GenericSerialization<Product_Root>.ReadOrDeserializeTable
                (_RootName, _Settings.CachedFiles, _RecordsPerPage);
            return prev ?? new List<Product_Root>();
        }
        public void _02_SaveNewestToCache(IEnumerable<Product_Root> curr)
        {
            curr = curr ?? new List<Product_Root>();
            GenericSerialization<Product_Root>.SerializeToJSON(curr.ToList(), _RootName, _Settings.CachedFiles, _RecordsPerPage);
        }

    }

}
