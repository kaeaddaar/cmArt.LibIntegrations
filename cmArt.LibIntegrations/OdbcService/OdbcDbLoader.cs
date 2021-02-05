using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace cmArt.LibIntegrations.OdbcService
{
    public static class OdbcDbLoader<T> where T : new()
    {
        public static List<T> Load_Table_FromDatabase
        (
            string TableName
            , string CommandText
            , Func<T, OdbcDataReader, bool> LoadFromReader
            , OdbcConnection conn
        )
        {
            //check for connection being open
            
            OdbcCommand command = new OdbcCommand();
            command.Connection = conn;
            command.CommandText = CommandText;
            //command.CommandText = $"Select * From {TableName} ORDER BY FileNo, Part, RecordNo";
            OdbcDataReader reader = command.ExecuteReader();
            System.Data.DataTable tblObj = new System.Data.DataTable();

            List<T> Objs = new List<T>();

            bool LoadSucceeded;
            while (reader.Read())
            {
                T tmpObj = new T();
                LoadSucceeded = LoadFromReader(tmpObj, reader);
                if (!LoadSucceeded)
                {
                    Console.WriteLine($"Load of {TableName} Failed");
                }
                Objs.Add(tmpObj);
            }

            tblObj.TableName = TableName;

            return Objs;
        }

    }

}
