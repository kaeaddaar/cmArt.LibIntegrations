using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class ICommentsExtensions_ForCopyFrom
    {
        public static void CopyFrom(this IComments to, IComments from)
        {
            to.Comment = from.Comment;
            to.CUnique = from.CUnique;
            to.FileNo = from.FileNo;
            to.LineNo = from.LineNo;
            to.RecordNo = from.RecordNo;
        }
    }

}
