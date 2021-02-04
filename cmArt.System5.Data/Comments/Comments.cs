using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class Comments : IComments, ICloneable
    {
        public Int16 FileNo { get; set; }
        public int RecordNo { get; set; }
        public Int16 LineNo { get; set; }
        public string Comment { get; set; }
        public int CUnique { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
