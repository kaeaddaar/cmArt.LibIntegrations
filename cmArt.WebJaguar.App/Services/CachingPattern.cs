using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;
using cmArt.LibIntegrations.SerializationService;
using cmArt.WebJaguar.Connector;
using cmArt.WebJaguar.Data;
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
        private IEnumerable<S5_CommonFields> prev;
        private const int _RecordsPerPage = 10000;

        public CachingPattern(string RootName, StaticSettings settings)
        {
            _Settings = settings;
            _RootName = RootName ?? string.Empty;
            if (_RootName.Count() < 1) { throw new Exception("RootName must have at least 1 character."); }
        }

        public IEnumerable<S5_CommonFields> _01_GetPrev()
        {
            prev = GenericSerialization<S5_CommonFields>.ReadOrDeserializeTable
                (_RootName, _Settings.CachedFiles, _RecordsPerPage);
            return prev ?? new List<S5_CommonFields>();
        }
        public void _02_SaveNewestToCache(IEnumerable<S5_CommonFields> curr)
        {
            curr = curr ?? new List<S5_CommonFields>();
            GenericSerialization<S5_CommonFields>.SerializeToJSON(curr.ToList(), _RootName, _Settings.CachedFiles, _RecordsPerPage);
        }

    }

}
