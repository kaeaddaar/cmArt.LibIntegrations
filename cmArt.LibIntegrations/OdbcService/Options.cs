using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.OdbcService
{
    public class Options
    {
        public string JSON;

        public Options(string JsonSettings)
        {
            JSON = JsonSettings ?? string.Empty;
        }
    }
}
