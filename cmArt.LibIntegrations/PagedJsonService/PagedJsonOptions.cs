using cmArt.LibIntegrations.OdbcService;
using System.Collections.Generic;
using System.Text.Json;

namespace cmArt.LibIntegrations.PagedJsonService
{
    public static class PagedJsonOptions_S5Inventory
    {

        public static Options GetOptions(string CachedFilesDirectory, IEnumerable<string> TableNames)
        {
            PagedJsonSettings options = new PagedJsonSettings();
            options.TableNames = new List<string>(TableNames);
            options.CachedFilesDirectory = CachedFilesDirectory ?? string.Empty;

            string settings = JsonSerializer.Serialize<PagedJsonSettings>(options);
            Options opt = new Options(settings);
            return opt;
        }

    }

}
