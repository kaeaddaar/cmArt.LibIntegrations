using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_Comments
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        Func<Comments, Comments_Clean> GetNewObj_Clean = (obj) => { return new Comments_Clean(obj); };

        public class Funcs : FuncBase<Comments, Comments_Clean, IComments>
        {
            public Func<Comments_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };
            public Func<Comments_Clean, int> Clean_Field = (objClean) => { objClean.Clean_Comment(); return 1; };

            public Funcs(Func<Comments, Comments_Clean> GetNewObj_Clean) : base(GetNewObj_Clean)
            {
                IComments p = new Comments();
                Comments_Clean p_clean = new Comments_Clean(p);

                _Trailing_Spaces_Field_Method_Name_Pairs = new List<(string FieldName, string CleanField_MethodName)>();
                // -- trim end fields to check
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.Comment), nameof(p_clean.Clean_Comment)));
                // --

                _Nullable_String_Field_Names = new List<string>();
                // -- null fields to check
                _Nullable_String_Field_Names.Add(nameof(p.Comment));
                // --
            }
        }

        [TestMethod]
        public void Test_that_Comments_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            foreach (var pair in f.Trailing_Spaces_Field_Method_Name_Pairs)
            {
                var GetSet = Build_GetSet_ForField(pair.FieldName);
                Generic_Tests<Comments, Comments_Clean, IComments>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
                (
                    GetSet.GetField
                    , GetSet.SetField
                    , f.GetNewObj
                    , f.GetNewObj_Clean
                    , f.Clean
                    , f.Clean_Field
                );
            }
        }
        [TestMethod]
        public void Test_that_null_values_dont_break_clean_field_methods()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Comments, Comments_Clean, IComments>.Test_that_null_values_dont_break_clean_field_methods
            (
                Get_GetField_SetField_Pairs(fieldNames)
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IComments, string> GetField, Func<IComments, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_Field = typeof(IComments).GetProperty(FieldName);
            Func<IComments, string> GetField = (obj) => { return (string)pInfo_Field.GetValue(obj); };
            Func<IComments, string, int> SetField = (obj, value) => { pInfo_Field.SetValue(obj, value); return 1; };
            return (GetField, SetField);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Comments, Comments_Clean, IComments>
                .Test_that_nullable_props_remove_null(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_Comments() // putting any brains in the IComments_clean
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Comments, Comments_Clean, IComments>
                .Test_For_null_values_in_uninitialized_Obj(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IComments, string> GetField, Func<IComments, string, int> SetField)> Get_GetField_SetField_Pairs(IEnumerable<string> FieldNames)
        {
            List<(Func<IComments, string> GetField, Func<IComments, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IComments, string> GetField, Func<IComments, string, int> SetField)>();

            IComments p = new Comments();
            foreach (var fieldName in FieldNames)
            {
                GetField_SetField_Pairs.Add(Build_GetSet_ForField(fieldName));
            }
            return GetField_SetField_Pairs;
        }


    }

}
