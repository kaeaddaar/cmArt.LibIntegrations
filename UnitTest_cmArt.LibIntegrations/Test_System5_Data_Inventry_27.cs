using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_Inventry_27
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        Func<Inventry_27, Inventry_27_Clean> GetNewObj_Clean = (obj) => { return new Inventry_27_Clean(obj); };

        public class Funcs : FuncBase<Inventry_27, Inventry_27_Clean, IInventry_27>
        {
            public Func<Inventry_27_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };

            public Funcs(Func<Inventry_27, Inventry_27_Clean> GetNewObj_Clean) : base(GetNewObj_Clean)
            {
                IInventry_27 p = new Inventry_27();
                Inventry_27_Clean p_clean = new Inventry_27_Clean(p);

                _Trailing_Spaces_Field_Method_Name_Pairs = new List<(string FieldName, string CleanField_MethodName)>();
                // -- trim end fields to check
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.Part), nameof(p_clean.Clean_Part)));
                // --

                _Nullable_String_Field_Names = new List<string>();
                // -- null fields to check
                _Nullable_String_Field_Names.Add(nameof(p.Part));
                _Nullable_String_Field_Names.Add(nameof(p.Cat));
                _Nullable_String_Field_Names.Add(nameof(p.Country));
                _Nullable_String_Field_Names.Add(nameof(p.Description));
                _Nullable_String_Field_Names.Add(nameof(p.Description2));
                _Nullable_String_Field_Names.Add(nameof(p.Ecommerce));
                _Nullable_String_Field_Names.Add(nameof(p.Item));
                _Nullable_String_Field_Names.Add(nameof(p.Location));
                _Nullable_String_Field_Names.Add(nameof(p.MarkDeleted));
                _Nullable_String_Field_Names.Add(nameof(p.PackSize));
                _Nullable_String_Field_Names.Add(nameof(p.Serial));
                _Nullable_String_Field_Names.Add(nameof(p.Size_1));
                _Nullable_String_Field_Names.Add(nameof(p.Size_2));
                _Nullable_String_Field_Names.Add(nameof(p.Size_3));
                _Nullable_String_Field_Names.Add(nameof(p.SuppPart));
                _Nullable_String_Field_Names.Add(nameof(p.Units));
                // --
            }
        }

        [TestMethod]
        public void Test_that_Inventry_27_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            foreach (var pair in f.Trailing_Spaces_Field_Method_Name_Pairs)
            {
                var GetSet = Build_GetSet_ForField(pair.FieldName);

                MethodInfo Clean_Method = typeof(Inventry_27_Clean).GetMethod(pair.CleanField_MethodName);
                Func<Inventry_27_Clean, int> Clean_Field = (objClean) => { Clean_Method.Invoke(objClean, null); return 1; };

                Generic_Tests<Inventry_27, Inventry_27_Clean, IInventry_27>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
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
            Generic_Tests<Inventry_27, Inventry_27_Clean, IInventry_27>.Test_that_null_values_dont_break_clean_field_methods
            (
                Get_GetField_SetField_Pairs(fieldNames)
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IInventry_27, string> GetField, Func<IInventry_27, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_Field = typeof(IInventry_27).GetProperty(FieldName);
            Func<IInventry_27, string> GetField = (obj) => { return (string)pInfo_Field.GetValue(obj); };
            Func<IInventry_27, string, int> SetField = (obj, value) => { pInfo_Field.SetValue(obj, value); return 1; };
            return (GetField, SetField);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Inventry_27, Inventry_27_Clean, IInventry_27>
                .Test_that_nullable_props_remove_null(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_Inventry_27() // putting any brains in the IInventry_27_clean
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<Inventry_27, Inventry_27_Clean, IInventry_27>
                .Test_For_null_values_in_uninitialized_Obj(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IInventry_27, string> GetField, Func<IInventry_27, string, int> SetField)> Get_GetField_SetField_Pairs(IEnumerable<string> FieldNames)
        {
            List<(Func<IInventry_27, string> GetField, Func<IInventry_27, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IInventry_27, string> GetField, Func<IInventry_27, string, int> SetField)>();

            IInventry_27 p = new Inventry_27();
            foreach (var fieldName in FieldNames)
            {
                GetField_SetField_Pairs.Add(Build_GetSet_ForField(fieldName));
            }
            return GetField_SetField_Pairs;
        }


    }

}
