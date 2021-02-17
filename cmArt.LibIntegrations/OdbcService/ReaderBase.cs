using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace cmArt.LibIntegrations.OdbcService
{
    public abstract class ReaderBase<T>
    {
        public bool LoadFromReader(T POCO, OdbcDataReader reader)
        {
            return LoadFromReader_Ext(POCO, reader);
        }

        protected abstract bool LoadFromReader_Ext(T POCO, OdbcDataReader reader);

        protected object FromReader(OdbcDataReader reader, string FieldName)
        {
            if (reader[FieldName] is DBNull)
            {
                return null;
            }
            object value = reader[FieldName];
            return value;

        }

    }
}
