using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable


namespace cmArt.System5.Data
{
    public class Comments_Clean : IComments
    {
        private IComments _Comments;

        public Comments_Clean(IComments comments)
        {
            _Comments = _Comments ?? new Comments();

            _Comments.Comment = _Comments.Comment ?? string.Empty;
            ((IComments)this).CopyFrom(comments);
        }

        public void Clean()
        {
            Clean_Comment();
        }
        public void Clean_Comment()
        {
            _Comments.Comment = _Comments.Comment.TrimOption();
        }

        public string CommentIndex
        {
            get { return _Comments.Comment; } // for performance, may not be clean
        }

        public string Comment_Raw { get { return _Comments.Comment ?? string.Empty; } }
        public string Comment
        {
            get { return _Comments.Comment.TrimOption(); }
            set { _Comments.Comment = value.TrimOption(); }
        }

        public int CUnique { get => _Comments.CUnique; set => _Comments.CUnique = value; }
        public short FileNo { get => _Comments.FileNo; set => _Comments.FileNo = value; }
        public short LineNo { get => _Comments.LineNo; set => _Comments.LineNo = value; }
        public int RecordNo { get => _Comments.RecordNo; set => _Comments.RecordNo = value; }
    }

}
