using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using cmArt.LibIntegrations.OdbcService.ReaderExtensions;


namespace cmArt.LibIntegrations.OdbcService
{
    public abstract class OdbcContext<T> : IDisposable
        where T : class
    {
        private OdbcConnection conn;
        public IEnumerable<T> _Records;

        protected abstract List<T> LoadTableFromDatabase
        (
            string QueryOrTableName
            , string Query
            , Func<T, OdbcDataReader, bool> LoadFromReader
            , OdbcConnection conn
        );
        public void init
        (
            Options OdbcOptions
            , string Query
            , Func<T, OdbcDataReader, bool> LoadFromReader
            , string QueryOrTableName
        )
        {
            string strConn = JsonSerializer.Deserialize<string>(OdbcOptions.JSON);

            conn = new OdbcConnection(strConn);
            conn.Open();
            // check connection open
            _Records = LoadTableFromDatabase
            (
                QueryOrTableName
                , Query
                , LoadFromReader
                , conn
            );

        }
        public OdbcContext()
        {
            _Records = new List<T>();
        }
        
        public void Dispose()
        {
            _Records = null;
            conn.Close();
            conn = null;
        }
    }

}
