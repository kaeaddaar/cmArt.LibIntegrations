using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ApiCallerService
{
    public class ApiCallFormData
    {
        public string UrlCommand { get; set; } = string.Empty;
        public Dictionary<string, string> Body { get; set; } = new Dictionary<string, string>();
        public ApiCallFormData CopyFrom(ApiCallFormData from)
        {
            this.UrlCommand = from.UrlCommand ?? string.Empty;
            this.Body = from.Body ?? new Dictionary<string, string>();
            return this;
        }
    }

}
