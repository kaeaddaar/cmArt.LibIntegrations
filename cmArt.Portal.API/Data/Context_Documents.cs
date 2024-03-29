﻿using System;
using System.Collections.Generic;
using System.Text;
using cmArt.Portal.Data;
using Microsoft.EntityFrameworkCore;


namespace cmArt.Portal.API.Data
{
    public class Context_Documents : DbContext, IContext_Documents
    {
        public virtual DbSet<Document> JsonDocuments { get; set; }
        //// Ravi's infrastructure
        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=tcp:ravis.database.windows.net,1433;Initial Catalog=RavisRunner;Persist Security Info=False;User ID=ravis_admin;Password=yGx23Ko9a!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        // My Dev Environment
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=tcp:cmart-solutions-integrations.database.windows.net,1433;Initial Catalog=Integrations;Persist Security Info=False;User ID=cmackay;Password=C1!ff0rdRpc0rpc0m;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public Context_Documents()
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
