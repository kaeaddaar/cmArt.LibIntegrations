using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    public class inventoryActivity
    {
        public string sku { get; set; }
        public decimal quantity { get; set; }
        public decimal inventory { get; set; }
        public decimal inventoryAFS { get; set; }
        public bool adjustAvForSaleOnly { get; set; }
    }
}
