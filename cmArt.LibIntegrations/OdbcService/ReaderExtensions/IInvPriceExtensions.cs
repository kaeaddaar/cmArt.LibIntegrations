using cmArt.System5.Data;
using System;
using System.Data.Odbc;


namespace cmArt.LibIntegrations.OdbcService.ReaderExtensions
{
    public static class IInvPriceExtensions
    {
        #region Load From Reader Functionality
        public static bool LoadFromReader(IInvPrice invPrice, OdbcDataReader reader)
        {
            return invPrice.LoadFromReader_Ext(reader);
        }

        public static bool LoadFromReader_Ext(this IInvPrice invPrice, OdbcDataReader reader)
        {
            invPrice = invPrice ?? new InvPrice();

            bool loadSucceeded = true;
            try
            {
                //invPrice.Department = (Int16)reader["Department"];
                invPrice.Department = (Int16)TryAssign("Department", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.EndDate = (DateTime)(reader["EndDate"]).CDbNull(new DateTime(1000, 1, 1));
                //invPrice.EndDate = (DateTime)TryAssign("EndDate", reader, new DateTime(1000, 1, 1), loadSucceeded, out loadSucceeded);
                invPrice.InvUnique = (int)TryAssign("InvUnique", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.PartUnique = (int)TryAssign("PartUnique", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.QuanDisc = (Single)TryAssign("QuanDisc", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.RegularPrice = (double)TryAssign("RegularPrice", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.RScheduleType = (string)TryAssign("RScheduleType", reader, "", loadSucceeded, out loadSucceeded);
                invPrice.SalePrice = (double)TryAssign("SalePrice", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.ScheduleLevel = (Int16)TryAssign("ScheduleLevel", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.SScheduleType = (string)TryAssign("SScheduleType", reader, "", loadSucceeded, out loadSucceeded);
                invPrice.StartDate = (DateTime)(reader["StartDate"]).CDbNull(new DateTime(1000, 1, 1));
                loadSucceeded = true;
            }
            catch (Exception e)
            {
                loadSucceeded = false;

                string str = GetReaderInfo(reader);
                Console.WriteLine(str);
            }
            return loadSucceeded;
        }

        private static string GetReaderInfo(OdbcDataReader reader)
        {
            string str = string.Empty;
            try
            {
                object[] fields = new object[reader.VisibleFieldCount];
                reader.GetValues(fields);
                str = string.Empty;
                int index = 0;
                foreach (var field in fields)
                {
                    str += reader.GetName(index) + ": " + field.ToString() + ",";
                }
            }
            catch (OdbcException e)
            {
                str = $"GetReaderInfo failed with message: {e.Message}";
            }
            return str;
        }

        private static object TryAssign(string FieldName, OdbcDataReader reader, object DefaultValue, bool LoadSuccededIn, out bool LoadSucceeded)
        {
            try
            {
                if (LoadSuccededIn) // don't change loadsucceded if already false
                {
                    LoadSucceeded = true;
                }
                else
                {
                    LoadSucceeded = false;
                }
                object val = reader[FieldName];
                if (val == null)
                {
                    LoadSucceeded = false;
                    return DefaultValue;
                }
                else
                {
                    return reader[FieldName];
                }
            }
            catch (OdbcException e)
            {
                LoadSucceeded = false;
                return DefaultValue;
            }
        }

        #endregion Load From Reader Functionality


    }

}
