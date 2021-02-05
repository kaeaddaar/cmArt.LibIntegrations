using cmArt.System5.Data;
using System;
using System.Data.Odbc;


namespace cmArt.LibIntegrations.OdbcService.ReaderExtensions
{
    public static class IAltSuplyExtensions
    {
        public static bool LoadFromReader(IAltSuply altSuply, OdbcDataReader reader)
        {
            return altSuply.LoadFromReader_Ext(reader);
        }
        public static bool LoadFromReader_Ext(this IAltSuply altSuply, OdbcDataReader reader)
        {
            bool loadSucceeded;
            try
            {
                // example for how to trap nulls by extending the types. See OdbcDataReaderExtensions
                //altSuply./*EndDate*/ = (DateTime)(reader["EndDate"]).CDbNull(new DateTime(1000, 1, 1));
                altSuply.AUnique = (Int32)reader["AUnique"];
                altSuply.Part = (Int32)reader["Part"];
                altSuply.RecordNo = (Int32)reader["RecordNo"];
                altSuply.Price = (Double)reader["Price"];
                altSuply.Preferred = (string)reader["Preferred"];
                altSuply.PartNumber = (string)reader["PartNumber"];
                altSuply.FileNo = (Int16)reader["FileNo"];
                altSuply.Extra = (Double)reader["Extra"];
                altSuply.Freight = (Double)reader["Freight"];
                altSuply.Duty = (Double)reader["Duty"];
                altSuply.PackSize = (string)reader["PackSize"];

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
