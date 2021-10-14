using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App
{
    public class StaticSettings
    {
        public StaticSettings(IConfiguration config)
        {
            // Feature idea: Add ability validate existense of the keys used below
            string prefix = "Shopifyinfo:";
            CachedFiles = config[$"{prefix}CachedFiles"] ?? string.Empty;
            CSVFiles = config[$"{prefix}CSVFiles"] ?? string.Empty;
            OutputDirectory = config[$"{prefix}OutputDirectory"] ?? string.Empty;
            DSNinfo = config[$"{prefix}DSNinfo"] ?? string.Empty;
            Cachinginfo = config[$"{prefix}Cachinginfo"] ?? string.Empty;
            SupressUpload = config[$"{prefix}SupressUpload"] ?? string.Empty;
            LogfilePath = config[$"{prefix}LogfilePath"] ?? string.Empty;
            Hours = config[$"{prefix}Hours"] ?? string.Empty;
            Minutes = config[$"{prefix}Minutes"] ?? string.Empty;
            Seconds = config[$"{prefix}Seconds"] ?? string.Empty;
            errormail = config[$"{prefix}errormail"] ?? string.Empty;
            smtpaddress = config[$"{prefix}smtpaddress"] ?? string.Empty;
            smtpport = config[$"{prefix}smtpport"] ?? string.Empty;
            enableSSL = config[$"{prefix}enableSSL"] ?? string.Empty;
            fromemailaddress = config[$"{prefix}fromemailaddress"] ?? string.Empty;
            fromemailpassword = config[$"{prefix}fromemailpassword"] ?? string.Empty;
        }
        public string CachedFiles { get; }
        public string CSVFiles { get; }
        public string OutputDirectory { get; }
        public string DSNinfo { get; }
        public string Cachinginfo { get; }
        public string SupressUpload { get; }
        public string LogfilePath { get; }
        public string Hours { get; }
        public string Minutes { get; }
        public string Seconds { get; }
        public string errormail { get; }
        public string smtpaddress { get; }
        public string smtpport { get; }
        public string enableSSL { get; }
        public string fromemailaddress { get; }
        public string fromemailpassword { get; }

    }

}
