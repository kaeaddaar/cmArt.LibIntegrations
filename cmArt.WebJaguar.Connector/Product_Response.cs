using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Connector
{
    public class Product_Response
    {
        public Product_Root product { get; set; }
        public string message { get; set; }
    }
}
