using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class BlobDataPOCO : IBlobDataPOCO
    {
        public Int32 BlobDataId { get; set; }
        public Int32 RecordNo { get; set; }
        public Int32 LineNo { get; set; }
        public Int16 BSize { get; set; }
        public byte[] BlobD { get; set; }
    }
}
