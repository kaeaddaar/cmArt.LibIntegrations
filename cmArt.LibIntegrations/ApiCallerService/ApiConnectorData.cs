using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ApiCallerService
{
    /// <summary>
    /// Store Site Root, and security info to be used across severall calls. Basic Auth scenario.
    /// </summary>
    public class ApiConnectorData
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ApiConnectorData()
        {
            Url = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
