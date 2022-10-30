using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace cmArt.Shopify.App.Database
{
    internal class TrackingContext : DbContext
    {
        public TrackingContext() : base("TrackingDatabse")
        {

        }
        public DbSet<Shopify_Product> PocoProductsAdapted { get; set; }
        public DbSet<Shopify_Prices> PocoPricesAdapted { get; set; }
        public DbSet<Shopify_Quantities> PocoQuantitiesAdapted { get; set; }

    }
}
