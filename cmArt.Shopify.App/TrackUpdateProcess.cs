using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using cmArt.Shopify.App.Database;

namespace cmArt.Shopify.App
{
    internal class TrackUpdateProcess
    {
        private Guid sessionId;
        private string strSessionId;
        //private IEnumerable<Shopify_Product> PocoProductsAdapted;
        //private IEnumerable<Shopify_Prices> PocoPricesAdapted;
        //private IEnumerable<Shopify_Quantities> PocoQuantitiesAdapted;

        public TrackUpdateProcess()
        {
            sessionId = Guid.NewGuid();
            strSessionId = sessionId.ToString();

        }
        //PocoProductsAdapted;
        public void Save_PocoProductsAdapted(IEnumerable<Shopify_Product> PocoProductsAdaptedIn)
        {
            return; // don' ecute right now
            using (var db = new TrackingContext())
            {
                foreach (var item in PocoProductsAdaptedIn)
                {
                    db.PocoProductsAdapted.Add(item);
                }
                db.SaveChanges();
            }
        }
        //PocoPricesAdapted;
        
        //PocoQuantitiesAdapted;
    }
}
