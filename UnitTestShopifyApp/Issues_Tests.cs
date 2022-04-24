using cmArt.LibIntegrations.GenericJoinsService;
using cmArt.Reece.ShopifyConnector;
using cmArt.Shopify.App.Data;
using cmArt.System5.Data;
using cmArt.System5.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace UnitTestShopifyApp
{
    [TestClass]
    public class Issues_Tests
    {
        [TestMethod]
        public void Duplicate_Discount_Codes_From_Portal_And_Shopify()
        {
            // Duplicate Discounts showing in /discount/list calls
            // Are there duplicates showing in direct shopify calls 
            // Ex: "COTEY- J400WCT" has doubled up discount codes.
            // First need the ability to get discounts for a part from Portal, and from Shopify.
            // Consider using source code to check against DB as well.
            // Maybe add ability to add endpoint to get single part worth of discounts and update them

            // pull discounts from portal
            // pull discounts from shopify
            // check for duplication

            // Result: I built cmArt.Shopify.TestingUI > Pages > ShopifyPriceRules.razor 
            //      I used this portal page, plus associated cmArt.Portal.Api to make shopify calls to pull and display records.
            //      I filtered for parts with duplicates, then created a routine that checked all duplicates and removed entries with empty discounts
            // Resolved as after cleanup the duplicates don't aren't re-added. As such the problem was created previosly and isn't a problem now.
        }

    }
}
