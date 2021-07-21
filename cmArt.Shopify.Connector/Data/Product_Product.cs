using System;
using System.Collections.Generic;

namespace cmArt.Shopify.Connector.Data
{
    public class Product_Product
    {
        public long id { get; set; }
        public string title { get; set; }
        public string body_html { get; set; }
        public string vendor { get; set; }
        public string product_type { get; set; }
        public DateTime created_at { get; set; }
        public string handle { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? published_at { get; set; }
        public object template_suffix { get; set; }
        public string status { get; set; }
        public string published_scope { get; set; }
        public string tags { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Product_Variant> variants { get; set; }
        public List<Product_Option> options { get; set; }
        public List<object> images { get; set; }
        public object image { get; set; }
    }


}
