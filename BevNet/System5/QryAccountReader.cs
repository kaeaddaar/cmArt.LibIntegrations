using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;
using cmArt.LibIntegrations.OdbcService;


namespace cmArt.BevNet.System5
{
    public class QryAccountReader : ReaderBase<QryAccount>
    {
        protected override bool LoadFromReader_Ext(QryAccount POCO, OdbcDataReader reader)
        {
            POCO = POCO ?? new QryAccount();

            bool loadSucceeded = false;

            POCO.AUnique = (Int32)FromReader(reader, "AUnique");
            POCO.AName = (string)FromReader(reader, "AName");
            POCO.BankInfo = (string)FromReader(reader, "BankInfo");

            loadSucceeded = true;
            return loadSucceeded;
        }
    }
}
