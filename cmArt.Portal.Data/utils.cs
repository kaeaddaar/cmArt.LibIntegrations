using cmArt.Reece.ShopifyConnector;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public static class utils
    {
        public static IEnumerable<S5QtyPair> JsonToQuantities(HttpRequest req, object DynamicFieldResult, string fieldName)
        {
            string json = GetValue(req, DynamicFieldResult, fieldName);
            try
            {
                IEnumerable<S5QtyPair> quantities = (IEnumerable<S5QtyPair>)System.Text.Json.JsonSerializer.Deserialize(json, typeof(List<S5QtyPair>));
                return quantities;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Returning empty list due to error: " + ex.ToString());
                return new List<S5QtyPair>();
            }
        }
        public static IEnumerable<S5PricePair> JsonToPrices(HttpRequest req, object DynamicFieldResult, string fieldName)
        {
            string json = GetValue(req, DynamicFieldResult, fieldName);
            try
            {
                IEnumerable<S5PricePair> prices = (IEnumerable<S5PricePair>)System.Text.Json.JsonSerializer.Deserialize(json, typeof(List<S5PricePair>));
                return prices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Returning empty list due to error: " + ex.ToString());
                return new List<S5PricePair>();
            }
        }
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
