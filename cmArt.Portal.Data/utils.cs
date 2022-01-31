using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public static class utils
    {
        public static string GetValue(HttpRequest req, object DynamicFieldResult, string fieldName)
        {
            HttpRequest _req = req;
            string _fieldName = fieldName ?? string.Empty;
            object _DynamicFieldResult = DynamicFieldResult ?? new object();

            if (_fieldName == string.Empty) { return string.Empty; }
            
            string result = _req.Query[_fieldName];
            result = result ?? _DynamicFieldResult.ToString() ?? string.Empty;

            return result;
        }
        public static int StringToInt(string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }
        public static decimal StringToDecimal(string value)
        {
            decimal result = 0;
            decimal.TryParse(value, out result);
            return result;
        }
        public static Guid StringToGuid(string value)
        {
            Guid result = Guid.Empty;
            Guid.TryParse(value, out result);
            return result;
        }
    }
}
