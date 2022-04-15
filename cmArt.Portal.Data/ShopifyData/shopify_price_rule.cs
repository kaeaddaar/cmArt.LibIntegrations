using cmArt.LibIntegrations.ClientControllerService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data.ShopifyData
{
    public class shopify_price_rule : IPrimaryKey<int>
    {
        public string title { get; set; }
        public string value { get; set; }
        public string id { get; set; }
        public string value_type { get; set; }

        public int GetPrimaryKey()
        {
            int ID = 0;
            int.TryParse(id, out ID);
            return ID;
        }

        public bool IsEmpty(int value)
        {
            return value == 0;
        }
    }
}
