using cmArt.System5.Data;
using System;
using System.Data.Odbc;


namespace cmArt.LibIntegrations.OdbcService.ReaderExtensions
{
    public static class ICommentsExtensions
    {
        public static bool LoadFromReader(this IComments comments, OdbcDataReader reader)
        {
            return comments.LoadFromReader_Ext(reader);
        }
        public static bool LoadFromReader_Ext(this IComments comments, OdbcDataReader reader)
        {
            bool loadSucceeded;
            try
            {
                // example for how to trap nulls by extending the types. See OdbcDataReaderExtensions
                //altSuply./*EndDate*/ = (DateTime)(reader["EndDate"]).CDbNull(new DateTime(1000, 1, 1));
                comments.CUnique = (Int32)reader["CUnique"];
                comments.Comment = (string)reader["Comment"];
                comments.FileNo = (Int16)reader["FileNo"];
                comments.LineNo = (Int16)reader["LineNo"];
                comments.RecordNo = (Int32)reader["RecordNo"];

                loadSucceeded = true;
            }
            catch
            {
                loadSucceeded = false;
            }
            return loadSucceeded;
        }

    }

}
