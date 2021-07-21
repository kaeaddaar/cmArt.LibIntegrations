using System.Collections.Generic;

namespace cmArt.Shopify.Connector.Data
{
    public class Product_Option
    {
        public object id { get; set; }
        public object product_id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public List<string> values { get; set; }
    }


}
