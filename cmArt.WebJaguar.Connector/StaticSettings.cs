using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector
{
    public class StaticSettings
    {
        public StaticSettings(IConfiguration config)
        {
            // Feature idea: Add ability validate existense of the keys used below
            string prefix = "info:";
            WebJaguarApiUrl = config[$"{prefix}WebJaguarApiUrl"] ?? string.Empty;
            WebJaguarApiUsername = config[$"{prefix}WebJaguarApiUsername"] ?? string.Empty;
            WebJaguarApiPassword = config[$"{prefix}WebJaguarApiPassword"] ?? string.Empty;
        }
        public string WebJaguarApiUrl { get; }
        public string WebJaguarApiUsername { get; }
        public string WebJaguarApiPassword { get; }

    }

}
