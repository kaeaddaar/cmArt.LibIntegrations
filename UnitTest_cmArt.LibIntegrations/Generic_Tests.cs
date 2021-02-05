using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace UnitTest_cmArt.LibIntegrations
{
    public static class Generic_Tests<TObj, TObjClean, IObj> where TObj : IObj, new() where TObjClean : IObj
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";

        public static void Perform_test_showing_that_Obj_Clean_Trims_Trailing_Spaces_from_field
        (
            Func<IObj, string> GetField
            , Func<IObj, string, int> SetField
            , Func<TObj> GetNewObj
            , Func<TObj, TObjClean> GetNewObj_Clean
            , Func<TObjClean, int> Clean
            , Func<TObjClean, int> Clean_Field
        )
        {
            // -- part number tests -- The clean version cleans on init, and in setter(s), otherwise it leaves origin alone
            // Passing in a non-clean record to the constructor of a clean record will clean the origin
            TObj objRecord = GetNewObj();
            SetField(objRecord, HelloWorld_TwoTrailingSpaces);
            TObjClean objCleanRecord = GetNewObj_Clean(objRecord);
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(objRecord));
            Assert.AreEqual(" ", GetField(objRecord).Substring(0, 1));

            // Clean on the clean record will clean the non-clean record
            SetField(objRecord, HelloWorld_TwoTrailingSpaces);
            Clean(objCleanRecord);
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(objRecord));
            Assert.AreEqual(" ", GetField(objRecord).Substring(0, 1));

            // setting the value on the non-clean record still allows a non-clean version to exist
            SetField(objRecord, HelloWorld_TwoTrailingSpaces);
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, GetField(objRecord));

            // accessing data that is non-clean will clean it up in clean version but source is still unclean
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(objCleanRecord));
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, GetField(objRecord));
            Assert.AreEqual(" ", GetField(objRecord).Substring(0, 1));

            // the non-clean base info is cleaned if set from the clean version
            SetField(objCleanRecord, GetField(objRecord));
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(objRecord));
            Assert.AreEqual(" ", GetField(objRecord).Substring(0, 1));
            // --

            // -- Quick tests to check each other cleaned field
            TObj A = GetNewObj();
            TObjClean B = GetNewObj_Clean(A); // Will find uninitialized or null field issues
            SetField(A, HelloWorld_TwoTrailingSpaces);
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, GetField(A));
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(B));
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, GetField(A)); // B.PartNumber Get mustn't trim trailing spaces
            Clean_Field(B);
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, GetField(A));
            // --
        }
        public static void Test_that_null_values_dont_break_clean_field_methods
        (
            IEnumerable<(Func<IObj, string> GetField, Func<IObj, string, int> SetField)> GetField_SetField_Pairs
            , Func<TObj> GetNewObj
            , Func<TObj, TObjClean> GetNewObj_Clean
        )
        {
            TObj A = GetNewObj();
            foreach(var pair in GetField_SetField_Pairs)
            {
                pair.SetField(A, null);
            }

            TObjClean B = GetNewObj_Clean(A);

            foreach(var pair in GetField_SetField_Pairs)
            {
                Assert.AreEqual(false, pair.GetField(A) == null);
                pair.SetField(A, null);
                Assert.AreEqual(false, pair.GetField(B) == null);
            }
        }
        public static void Test_that_nullable_props_remove_null
        (
            IEnumerable<(Func<IObj, string> GetField, Func<IObj, string, int> SetField)> GetField_Set_Field_Pairs
            , Func<TObj> GetNewObj
            , Func<TObj, TObjClean> GetNewObj_Clean
        )
        {
            TObj A = GetNewObj();
            TObjClean B = GetNewObj_Clean(A);
            
            foreach(var pair in GetField_Set_Field_Pairs)
            {
                pair.SetField(B, null);
            }

            IEnumerable<Func<IObj, string>> FieldGetters = GetField_Set_Field_Pairs.Select(p => p.GetField);
            Obj_Clean_Null_Checks(B, FieldGetters);
        }
        public static void Test_For_null_values_in_uninitialized_Obj // putting any brains in the IAltSuply_clean
        (
            IEnumerable<(Func<IObj, string> GetField, Func<IObj, string, int> SetField)> GetField_Set_Field_Pairs
            , Func<TObj> GetNewObj
            , Func<TObj, TObjClean> GetNewObj_Clean
        )
        {
            TObj A = GetNewObj();
            TObjClean B = GetNewObj_Clean(A);

            IEnumerable<Func<IObj, string>> FieldGetters = GetField_Set_Field_Pairs.Select(p => p.GetField);
            Obj_Clean_Null_Checks(B, FieldGetters);
        }
        private static void Obj_Clean_Null_Checks(TObjClean A, IEnumerable<Func<IObj,string>> FieldGetters)
        {
            foreach (var getter in FieldGetters)
            {
                Assert.IsFalse(getter(A) == null);
            }
        }

    }
}
