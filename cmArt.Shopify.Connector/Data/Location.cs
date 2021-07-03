using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.Connector.Data
{
    public class Location
    {
        public object id { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string province_code { get; set; }
        public bool legacy { get; set; }
        public bool active { get; set; }
        public string admin_graphql_api_id { get; set; }
        public string localized_country_name { get; set; }
        public string localized_province_name { get; set; }
    }

    public class Location_Root
    {
        public List<Location> locations { get; set; }
    }
}
