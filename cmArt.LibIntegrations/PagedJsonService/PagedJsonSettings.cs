using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.PagedJsonService
{
    public class PagedJsonSettings
    {
        public IEnumerable<string> TableNames { get; set; }
        public string CachedFilesDirectory { get; set; }
    }

}
