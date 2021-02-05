using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public static class StringExtensions_For_OptionStyleTrimEnd
    {
        public static string TrimOption(this string str)
        {
            Option<string> _comment = str;
            string result = string.Empty;
            _comment.IfSome(cmt => result = cmt.TrimEnd());
            return result;
        }
    }
}
