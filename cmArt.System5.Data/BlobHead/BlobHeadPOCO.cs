using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class BlobHeadPOCO : IBlobHeadPOCO
    {
        public Int32 BlobHeadUnique { get; set; }
        public Int16 FileNo { get; set; }
        public Int32 RecordNo { get; set; }
        public Int16 FileType { get; set; }
        public string Extension { get; set; }       // 3 char
        public Int16 UserNo { get; set; }
        public string DateCreated { get; set; }     // 3 char
        public string DateModified { get; set; }    // 3 char
        public string Secure { get; set; }          // 2 char
        public Int32 BlobSize { get; set; }
        public Int32 Link { get; set; }
        public string DataFileName { get; set; }    // varchar 127
        public string Directory { get; set; }       // varchar 127
        public string TimeCreated { get; set; }
        public Int32 Index { get; set; }
        public Int32 KeyNo { get; set; }

    }

}
