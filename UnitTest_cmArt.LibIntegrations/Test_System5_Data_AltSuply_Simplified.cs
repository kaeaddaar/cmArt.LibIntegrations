﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_AltSuply_Simplified
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        Func<AltSuply, AltSuply_Clean> GetNewObj_Clean = (obj) => { return new AltSuply_Clean(obj); };

        public class Funcs : FuncBase<AltSuply, AltSuply_Clean, IAltSuply>
        {
            public Func<AltSuply_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };

            public Funcs(Func<AltSuply, AltSuply_Clean> GetNewObj_Clean) : base(GetNewObj_Clean)
            {
                IAltSuply p = new AltSuply();
                AltSuply_Clean p_clean = new AltSuply_Clean(p);

                _Trailing_Spaces_Field_Method_Name_Pairs = new List<(string FieldName, string CleanField_MethodName)>();
                // -- trim end fields to check
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.PartNumber), nameof(p_clean.Clean_PartNumber)));
                // --

                _Nullable_String_Field_Names = new List<string>();
                // -- null fields to check
                _Nullable_String_Field_Names.Add(nameof(p.PartNumber));
                _Nullable_String_Field_Names.Add(nameof(p.PackSize));
                _Nullable_String_Field_Names.Add(nameof(p.Preferred));
                // --
            }
        }

        [TestMethod]
        public void Test_that_AltSuply_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            foreach (var pair in f.Trailing_Spaces_Field_Method_Name_Pairs)
            {
                var GetSet = Build_GetSet_ForField(pair.FieldName);

                MethodInfo Clean_Method = typeof(AltSuply_Clean).GetMethod(pair.CleanField_MethodName);
                Func<AltSuply_Clean, int> Clean_Field = (objClean) => { Clean_Method.Invoke(objClean, null); return 1; };

                Generic_Test_Lib<AltSuply, AltSuply_Clean, IAltSuply>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
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
            Generic_Test_Lib<AltSuply, AltSuply_Clean, IAltSuply>.Test_that_null_values_dont_break_clean_field_methods
            (
                Get_GetField_SetField_Pairs(fieldNames)
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_Field = typeof(IAltSuply).GetProperty(FieldName);
            Func<IAltSuply, string> GetField = (obj) => { return (string)pInfo_Field.GetValue(obj); };
            Func<IAltSuply, string, int> SetField = (obj, value) => { pInfo_Field.SetValue(obj, value); return 1; };
            return (GetField, SetField);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Test_Lib<AltSuply, AltSuply_Clean, IAltSuply>
                .Test_that_nullable_props_remove_null(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_AltSuply() // putting any brains in the IAltSuply_clean
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Test_Lib<AltSuply, AltSuply_Clean, IAltSuply>
                .Test_For_null_values_in_uninitialized_Obj(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> Get_GetField_SetField_Pairs(IEnumerable<string> FieldNames)
        {
            List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)>();

            IAltSuply p = new AltSuply();
            foreach (var fieldName in FieldNames)
            {
                GetField_SetField_Pairs.Add(Build_GetSet_ForField(fieldName));
            }
            return GetField_SetField_Pairs;
        }


    }

}
