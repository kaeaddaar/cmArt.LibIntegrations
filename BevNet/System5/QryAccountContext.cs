using cmArt.LibIntegrations.OdbcService;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace cmArt.BevNet.System5
{
    public class QryAccountContext : OdbcContext<QryAccount>
    {
        protected override List<QryAccount> LoadTableFromDatabase
        (
            //string QueryOrTableName
            //, string Query
            //, Func<QryAccount, OdbcDataReader, bool> LoadFromReader
            OdbcConnection conn
        )
        {
            QryAccountReader qryAccountReader = new QryAccountReader();
            string qry = "Select AUnique, AName, BankInfo From Account " +
                "where AType='P' " +  // P is for suppliers
                "and ltrim(BankInfo) <> '';" // only records where extra info is filled in
            ;

            this._Records = OdbcDbLoader<QryAccount>.Load_Table_FromDatabase
            (
                "QryAccount_ForInfo"
                , qry
                , qryAccountReader.LoadFromReader
                , conn
            );
            return this._Records.ToList();
        }
    }
}
