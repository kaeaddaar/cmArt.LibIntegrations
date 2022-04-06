using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace cmArt.LibIntegrations.CsvFileReaderService
{
    //public class csvReaderEngine<T> : IEnumerable where T : class, new()
    //{
    //    private List<T> _Things;
    //    public void BeginReadFile(string PathAndFile)
    //    {
    //        string _PathAndFile = PathAndFile ?? string.Empty;

    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return (IEnumerator)GetEnumerator();
    //    }
    //    IEnumerator T.GetEnumerator()
    //    {
    //        return new csvReaderEngine_Enum(_Things);
    //    }
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class csvReaderEngine_Enum<T> : IEnumerator where T : class, new()
    //{
    //    List<T> _Things;
    //    int position = -1;

    //    public bool MoveNext()
    //    {
    //        position++;
    //        return (position < _Things.Count());
    //    }

    //    public void Reset()
    //    {
    //        position = -1;
    //    }

    //    object IEnumerator.Current
    //    {
    //        get { return Current; }
    //    }
    //    public T Current
    //    {
    //        get
    //        {
    //            try
    //            {
    //                return _Things[position];
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                throw new InvalidOperationException();
    //            }
    //        }
    //    }
    //}
    public class csvReader<T> where T : class, new()
    {
        public static IEnumerable<T> ReadFile(string PathAndFile, Func<string,T> fProcessLine, bool SupressRecordErrors = false) // fProcessLine is a function that takes a line of the read file and returns a new object of T
        {
            List<T> lstRecords = new List<T>();
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(PathAndFile))
            {
                T tmp = fProcessLine(line);
                lstRecords.Add(tmp);
                counter++;
            }

            return lstRecords;
        }
    }
}
