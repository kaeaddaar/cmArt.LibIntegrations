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
            WebJaguarApiUrl = config[$"{prefix}https://shopravis.webjaguar.dev"] ?? string.Empty;
            WebJaguarApiUsername = config[$"{prefix}shopravis"] ?? string.Empty;
            WebJaguarApiPassword = config[$"{prefix}H9pPG9yW58cMP45e"] ?? string.Empty;
        }
        public string WebJaguarApiUrl { get; }
        public string WebJaguarApiUsername { get; }
        public string WebJaguarApiPassword { get; }

    }

}
