﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public class StaticSettings
    {
        public StaticSettings(IConfiguration config)
        {
            // Feature idea: Add ability validate existense of the keys used below
            string prefix = "info:";
            PortalApiUrl = config[$"{prefix}PortalApiUrl"] ?? string.Empty;
            PortalApiUsername = config[$"{prefix}PortalApiUsername"] ?? string.Empty;
            PortalApiPassword = config[$"{prefix}PortalApiPassword"] ?? string.Empty;
        }
        public string PortalApiUrl { get; }
        public string PortalApiUsername { get; }
        public string PortalApiPassword { get; }

    }

}
