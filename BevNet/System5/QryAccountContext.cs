using cmArt.LibIntegrations.OdbcService;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace cmArt.BevNet.System5
{
    public class QryAccountContext : OdbcContext<QryAccount>
    {
        protected override List<QryAccount> LoadTableFromDatabase
        (
            string QueryOrTableName
            , string Query
            , Func<QryAccount, OdbcDataReader, bool> LoadFromReader
            , OdbcConnection conn
        )
        {
            throw new NotImplementedException();
        }
    }
}
