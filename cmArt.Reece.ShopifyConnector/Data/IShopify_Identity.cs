﻿using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IShopify_Identity : IInventoryId
    {
        string Cat { get; set; }
        int InvUnique { get; set; }
        string PartNumber { get; set; }

    }
}
