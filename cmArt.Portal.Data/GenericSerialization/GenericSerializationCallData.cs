using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data.GenericSerialization
{
    public class GenericSerializationCallData
    {
        public string TableName { get; set; }
        public string CashedFilesDirectory { get; set; }
        public int RecordsPerPage { get; set; }
    }
}
