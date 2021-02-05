using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace cmArt.LibIntegrations.OdbcService
{
    public static class OdbcOptions
    {

        public static Options GetOptions(string ConnectionString)
        {
            string _ConnectionString = ConnectionString ?? string.Empty;
            string settings = JsonSerializer.Serialize<string>(ConnectionString);
            Options opt = new Options(settings);
            return opt;
        }

    }

}
