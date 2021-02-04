using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class AltSuply : IAltSuply, ICloneable
    {
        public int AUnique { get; set; }
        public int Part { get; set; }
        public int RecordNo { get; set; }
        public double Price { get; set; }
        public string Preferred { get; set; }   // length = 1 character
        public string PartNumber { get; set; }  // length = 50 characters
        public Int16 FileNo { get; set; }
        public double Extra { get; set; }
        public double Freight { get; set; }
        public double Duty { get; set; }
        public string PackSize { get; set; } // length = 8 characters

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
