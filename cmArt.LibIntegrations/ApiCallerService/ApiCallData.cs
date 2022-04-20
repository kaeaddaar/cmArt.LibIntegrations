using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ApiCallerService
{
    /// <summary>
    /// store the querystring command, and body of the API call.
    /// </summary>
    public class ApiCallData
    {
        public string UrlCommand { get; set; }
        public string Body { get; set; }
        public string Args { get; set; }
        public ApiCallData CopyFrom(ApiCallData from)
        {
            this.UrlCommand = from.UrlCommand ?? string.Empty;
            this.Body = from.Body ?? string.Empty;
            this.Args = from.Args ?? string.Empty;
            return this;
        }
    }
}
