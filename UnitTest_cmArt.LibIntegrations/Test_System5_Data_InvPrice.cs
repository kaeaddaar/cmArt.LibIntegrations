using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_InvPrice
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        Func<InvPrice, InvPrice_Clean> GetNewObj_Clean = (obj) => { return new InvPrice_Clean(obj); };

        public class Funcs : FuncBase<InvPrice, InvPrice_Clean, IInvPrice>
        {
            public Func<InvPrice_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };

            public Funcs(Func<InvPrice, InvPrice_Clean> GetNewObj_Clean) : base(GetNewObj_Clean)
            {
                IInvPrice p = new InvPrice();
                InvPrice_Clean p_clean = new InvPrice_Clean(p);

                _Trailing_Spaces_Field_Method_Name_Pairs = new List<(string FieldName, string CleanField_MethodName)>();
                // -- trim end fields to check
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.RScheduleType), nameof(p_clean.Clean_RScheduleType)));
                _Trailing_Spaces_Field_Method_Name_Pairs.Add((nameof(p.SScheduleType), nameof(p_clean.Clean_SScheduleType)));
                // --

                _Nullable_String_Field_Names = new List<string>();
                // -- null fields to check
                _Nullable_String_Field_Names.Add(nameof(p.RScheduleType));
                _Nullable_String_Field_Names.Add(nameof(p.SScheduleType));
                // --
            }
        }

        [TestMethod]
        public void Test_that_InvPrice_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            foreach (var pair in f.Trailing_Spaces_Field_Method_Name_Pairs)
            {
                var GetSet = Build_GetSet_ForField(pair.FieldName);

                MethodInfo Clean_Method = typeof(InvPrice_Clean).GetMethod(pair.CleanField_MethodName);
                Func<InvPrice_Clean, int> Clean_Field = (objClean) => { Clean_Method.Invoke(objClean, null); return 1; };

                Generic_Tests<InvPrice, InvPrice_Clean, IInvPrice>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
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
            Generic_Tests<InvPrice, InvPrice_Clean, IInvPrice>.Test_that_null_values_dont_break_clean_field_methods
            (
                Get_GetField_SetField_Pairs(fieldNames)
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IInvPrice, string> GetField, Func<IInvPrice, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_Field = typeof(IInvPrice).GetProperty(FieldName);
            Func<IInvPrice, string> GetField = (obj) => { return (string)pInfo_Field.GetValue(obj); };
            Func<IInvPrice, string, int> SetField = (obj, value) => { pInfo_Field.SetValue(obj, value); return 1; };
            return (GetField, SetField);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<InvPrice, InvPrice_Clean, IInvPrice>
                .Test_that_nullable_props_remove_null(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_InvPrice() // putting any brains in the IInvPrice_clean
        {
            Funcs f = new Funcs(GetNewObj_Clean);
            IEnumerable<string> fieldNames = f.Nullable_String_Field_Names;
            Generic_Tests<InvPrice, InvPrice_Clean, IInvPrice>
                .Test_For_null_values_in_uninitialized_Obj(Get_GetField_SetField_Pairs(fieldNames), f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IInvPrice, string> GetField, Func<IInvPrice, string, int> SetField)> Get_GetField_SetField_Pairs(IEnumerable<string> FieldNames)
        {
            List<(Func<IInvPrice, string> GetField, Func<IInvPrice, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IInvPrice, string> GetField, Func<IInvPrice, string, int> SetField)>();

            IInvPrice p = new InvPrice();
            foreach (var fieldName in FieldNames)
            {
                GetField_SetField_Pairs.Add(Build_GetSet_ForField(fieldName));
            }
            return GetField_SetField_Pairs;
        }


    }

}
