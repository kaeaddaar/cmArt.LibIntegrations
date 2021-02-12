using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_Stok
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        Func<Stok, Stok_Clean> GetNewObj_Clean = (obj) => { return new Stok_Clean(obj); };

        public class Funcs : FuncBase<Stok, Stok_Clean, IStok>
        {
            public Func<Stok_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };

            public Funcs(Func<Stok, Stok_Clean> GetNewObj_Clean) : base(GetNewObj_Clean)
            {
                IStok p = new Stok();
                Stok_Clean p_clean = new Stok_Clean(p);

                _Trailing_Spaces_Field_Method_Name_Pairs = new List<(string FieldName, string CleanField_MethodName)>();
                // -- trim end fields to check
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.Number), nameof(p_clean.Clean_Number)));
                // --

                _Nullable_String_Field_Names = new List<string>();
                // -- null fields to check
                _Nullable_String_Field_Names.Add(nameof(p.Number));
                _Nullable_String_Field_Names.Add(nameof(p.Country));
                _Nullable_String_Field_Names.Add(nameof(p.Description));
                // --
            }
        }

        [TestMethod]
        public void Test_that_Stok_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            foreach (var pair in f.Trailing_Spaces_Field_Method_Name_Pairs)
            {
                var GetSet = Build_GetSet_ForField(pair.FieldName);

                MethodInfo Clean_Method = typeof(Stok_Clean).GetMethod(pair.CleanField_MethodName);
                Func<Stok_Clean, int> Clean_Field = (objClean) => { Clean_Method.Invoke(objClean, null); return 1; };

                Generic_Tests<Stok, Stok_Clean, IStok>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
                (
                    GetSet.GetField
                    , GetSet.SetField
                    , f.GetNewObj
                    , f.GetNewObj_Clean
                    , f.Clean
                    , Clean_Field
                );
            }
        }
        [TestMethod]
        public void Test_that_null_values_dont_break_clean_field_methods()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Stok, Stok_Clean, IStok>.Test_that_null_values_dont_break_clean_field_methods
            (
                Get_GetField_SetField_Pairs(fieldNames)
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IStok, string> GetField, Func<IStok, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_Field = typeof(IStok).GetProperty(FieldName);
            Func<IStok, string> GetField = (obj) => { return (string)pInfo_Field.GetValue(obj); };
            Func<IStok, string, int> SetField = (obj, value) => { pInfo_Field.SetValue(obj, value); return 1; };
            return (GetField, SetField);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Stok, Stok_Clean, IStok>
                .Test_that_nullable_props_remove_null(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_Stok() // putting any brains in the IStok_clean
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Stok, Stok_Clean, IStok>
                .Test_For_null_values_in_uninitialized_Obj(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IStok, string> GetField, Func<IStok, string, int> SetField)> Get_GetField_SetField_Pairs(IEnumerable<string> FieldNames)
        {
            List<(Func<IStok, string> GetField, Func<IStok, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IStok, string> GetField, Func<IStok, string, int> SetField)>();

            IStok p = new Stok();
            foreach (var fieldName in FieldNames)
            {
                GetField_SetField_Pairs.Add(Build_GetSet_ForField(fieldName));
            }
            return GetField_SetField_Pairs;
        }


    }

}
