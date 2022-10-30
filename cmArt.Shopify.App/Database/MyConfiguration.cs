using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;


namespace cmArt.Shopify.App.Database
{
    internal class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
        }
    }
}
