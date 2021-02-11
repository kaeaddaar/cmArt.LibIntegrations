using cmArt.System5.Data.FactoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest_cmArt.LibIntegrations
{
    public class FuncBase<TObj, TObjClean, IObj> where TObj : IObj, new() where TObjClean : IObj
    {
        public Func<TObj> GetNewObj = () => { return new TObj(); };
        //public Func<TObj, TObjClean> GetNewObj_Clean = (obj) => { return new TObjClean(obj); };
        public Func<TObj, TObjClean> GetNewObj_Clean = null;

        protected List<(string FieldName, string CleanField_MethodName)> _Trailing_Spaces_Field_Method_Name_Pairs;
        public IEnumerable<(string FieldName, string CleanField_MethodName)> Trailing_Spaces_Field_Method_Name_Pairs
        {
            get { return _Trailing_Spaces_Field_Method_Name_Pairs; }
            set { _Trailing_Spaces_Field_Method_Name_Pairs = value.ToList(); }
        }

        private List<string> _Nullable_String_Field_Names;
        public IEnumerable<string> Nullable_String_Field_Names
        {
            get { return _Nullable_String_Field_Names; }
            set { _Nullable_String_Field_Names = value.ToList(); }
        }

        //public FuncBase(Func<TObj, TObjClean> GetNewObj_Clean)
        public FuncBase(Func<TObj, TObjClean> GetNewObj_Clean)
        {
            this.GetNewObj_Clean = GetNewObj_Clean;
        }
    }
}
