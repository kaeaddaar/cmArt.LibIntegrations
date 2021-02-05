using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System5_Data_AltSuply_Generic
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";

        public class Funcs
        {
            public Func<IAltSuply, string> GetPartNumber = (obj) => { return obj.PartNumber; };
            public Func<IAltSuply, string, int> SetPartNumber = (obj, value) => { obj.PartNumber = value; return 1; };
            public Func<AltSuply> GetNewObj = () => { return new AltSuply(); };
            public Func<AltSuply, AltSuply_Clean> GetNewObj_Clean = (obj) => { return new AltSuply_Clean(obj); };
            public Func<AltSuply_Clean, int> Clean = (objClean) => { objClean.Clean(); return 1; };
            public Func<AltSuply_Clean, int> Clean_Field = (objClean) => { objClean.Clean_PartNumber(); return 1; };
        }

        [TestMethod]
        public void Test_that_AltSuply_Clean_Trims_Trailing_Spaces_from_required_fields()
        {
            Funcs f = new Funcs();

            Generic_Tests<AltSuply, AltSuply_Clean, IAltSuply>.Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
            (
                f.GetPartNumber
                , f.SetPartNumber
                , f.GetNewObj
                , f.GetNewObj_Clean
                , f.Clean
                , f.Clean_Field
            );
        }
        [TestMethod]
        public void Test_that_null_values_dont_break_clean_field_methods()
        {
            Funcs f = new Funcs();
            List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> GetField_SetField_Pairs =
                Get_GetField_SetField_Pairs();

            Generic_Tests<AltSuply, AltSuply_Clean, IAltSuply>.Test_that_null_values_dont_break_clean_field_methods
            (
                GetField_SetField_Pairs
                , f.GetNewObj
                , f.GetNewObj_Clean
            );
        }
        private (Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField) Build_GetSet_ForField(string FieldName)
        {
            PropertyInfo pInfo_PartNumber = typeof(IAltSuply).GetProperty(FieldName);

            Func<IAltSuply, string> GetPartNumber = (obj) => { return (string)pInfo_PartNumber.GetValue(obj); };
            Func<IAltSuply, string, int> SetPartNumber = (obj, value) => { pInfo_PartNumber.SetValue(obj,value); return 1; };
            return (GetPartNumber, SetPartNumber);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            Funcs f = new Funcs();
            List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> GetField_SetField_Pairs =
                Get_GetField_SetField_Pairs();

            Generic_Tests<AltSuply, AltSuply_Clean, IAltSuply>
                .Test_that_nullable_props_remove_null(GetField_SetField_Pairs, f.GetNewObj, f.GetNewObj_Clean);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_AltSuply() // putting any brains in the IAltSuply_clean
        {
            Funcs f = new Funcs();
            List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> GetField_SetField_Pairs =
                Get_GetField_SetField_Pairs();

            Generic_Tests<AltSuply, AltSuply_Clean, IAltSuply>
                .Test_For_null_values_in_uninitialized_Obj(GetField_SetField_Pairs, f.GetNewObj, f.GetNewObj_Clean);
        }
        private List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> Get_GetField_SetField_Pairs()
        {
            List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)> GetField_SetField_Pairs =
                new List<(Func<IAltSuply, string> GetField, Func<IAltSuply, string, int> SetField)>();

            GetField_SetField_Pairs.Add(Build_GetSet_ForField("PartNumber"));
            GetField_SetField_Pairs.Add(Build_GetSet_ForField("PackSize"));
            GetField_SetField_Pairs.Add(Build_GetSet_ForField("Preferred"));

            return GetField_SetField_Pairs;
        }
    }

}
