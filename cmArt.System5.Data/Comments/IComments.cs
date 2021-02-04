using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public interface IComments
    {
        string Comment { get; set; }
        int CUnique { get; set; }
        short FileNo { get; set; }
        short LineNo { get; set; }
        int RecordNo { get; set; }
    }

}
