using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public interface IAltSuply
    {
        int AUnique { get; set; }
        double Duty { get; set; }
        double Extra { get; set; }
        short FileNo { get; set; }
        double Freight { get; set; }
        string PackSize { get; set; }
        int Part { get; set; }
        string PartNumber { get; set; }
        string Preferred { get; set; }
        double Price { get; set; }
        int RecordNo { get; set; }
    }

}
