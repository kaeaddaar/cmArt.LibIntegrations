using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.LibIntegrations.CsvFileReaderService
{
    /// <summary>
    /// Reads from a CSV file and provides the records.  
    /// 
    /// Throws ArgumentNullException, ArgumentException, ArgumentOutOfRangeException, ObjectDisposedException
    /// , BadUsageException, FileHelpersException
    /// </summary>
    /// <typeparam name="T">Use a POCO for type T that matches the CSV file format, and use FileHelpers attributes 
    /// to decorate the class and its fields. </typeparam>
    public class FileReaderGeneric<T> where T : class, new()
    {
        public static IEnumerable<T> ReadFile(string PathAndFile, bool SupressRecordErrors = false)
        {
            List<T> lstRecords = new List<T>();

            var engine = new FileHelperAsyncEngine<T>();
            engine.Options.IgnoreFirstLines = 1;

            //bool envIsDebug = Environment.GetEnvironmentVariable("ExecutionMode") == "Debug";
            bool envIsDebug = !SupressRecordErrors;
            if (!envIsDebug)
            { engine.ErrorMode = ErrorMode.IgnoreAndContinue; }

            using (engine.BeginReadFile(PathAndFile))
            {
                foreach (T record in engine)
                {
                    lstRecords.Add(record);
                }
            }

            return lstRecords;
        }
    }
}
