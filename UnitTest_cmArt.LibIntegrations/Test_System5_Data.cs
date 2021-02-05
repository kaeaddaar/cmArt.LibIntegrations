using Microsoft.VisualStudio.TestTools.UnitTesting;
using cmArt.System5.Data;
using System.Reflection;
using System;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_System_5Data
    {
        const string HelloWorld_NoTrailingSpaces = " Hello World";
        const string HelloWorld_TwoTrailingSpaces = " Hello World  ";
        delegate void CleanMethod();

        [TestMethod]
        public void Test_that_AltSuply_Clean_Trims_Trailing_Spaces_from_required_fields()
        {

            AltSuply AltSuplyRecord = new AltSuply();

            // -- part number tests -- The clean version cleans on init, and in setter(s), otherwise it leaves origin alone
            // Passing in a non-clean record to the constructor of a clean record will clean the origin
            AltSuplyRecord.PartNumber = HelloWorld_TwoTrailingSpaces;
            AltSuply_Clean clean = new AltSuply_Clean(AltSuplyRecord);
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, AltSuplyRecord.PartNumber);
            Assert.AreEqual(" ", AltSuplyRecord.PartNumber.Substring(0, 1));

            // Clean on the clean record will clean the non-clean record
            AltSuplyRecord.PartNumber = HelloWorld_TwoTrailingSpaces;
            clean.Clean();
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, AltSuplyRecord.PartNumber);
            Assert.AreEqual(" ", AltSuplyRecord.PartNumber.Substring(0, 1));

            // setting the value on the non-clean record still allows a non-clean version to exist
            AltSuplyRecord.PartNumber = HelloWorld_TwoTrailingSpaces;
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, AltSuplyRecord.PartNumber);

            // accessing data that is non-clean will clean it up in clean version but source is still unclean
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, clean.PartNumber);
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, AltSuplyRecord.PartNumber);
            Assert.AreEqual(" ", clean.PartNumber.Substring(0, 1));

            // the non-clean base info is cleaned if set from the clean version
            clean.PartNumber = AltSuplyRecord.PartNumber;
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, AltSuplyRecord.PartNumber);
            Assert.AreEqual(" ", AltSuplyRecord.PartNumber.Substring(0, 1));
            // --

            // -- Quick tests to check each other cleaned field
            AltSuply A = new AltSuply();
            AltSuply_Clean B = new AltSuply_Clean(A); // Will find uninitialized or null field issues
            A.PartNumber = HelloWorld_TwoTrailingSpaces;
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, A.PartNumber);
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, B.PartNumber); 
            Assert.AreEqual(HelloWorld_TwoTrailingSpaces, A.PartNumber); // B.PartNumber Get mustn't trim trailing spaces
            B.Clean_PartNumber();
            Assert.AreEqual(HelloWorld_NoTrailingSpaces, A.PartNumber);
            // --


        }
        [TestMethod]
        public void Test_that_null_values_dont_break_clean_field_methods()
        {
            AltSuply A = new AltSuply();
            A.PackSize = null;
            A.PartNumber = null;
            A.Preferred = null;

            AltSuply_Clean B = new AltSuply_Clean(A);
            
            Assert.AreEqual(false, A.PackSize == null);
            Assert.AreEqual(false, A.PartNumber == null);
            Assert.AreEqual(false, A.Preferred == null);

            A.PackSize = null;
            A.PartNumber = null;
            A.Preferred = null;

            Assert.AreEqual(false, B.PackSize == null);
            Assert.AreEqual(false, B.PartNumber == null);
            Assert.AreEqual(false, B.Preferred == null);
        }
        [TestMethod]
        public void Test_that_nullable_props_remove_null()
        {
            AltSuply A = new AltSuply();
            AltSuply_Clean B = new AltSuply_Clean(A);
            B.PackSize = null;
            B.PartNumber = null;
            B.Preferred = null;
            AltSuply_Clean_Null_Checks(B);
        }
        [TestMethod]
        public void Test_For_null_values_in_uninitialized_AltSuply() // putting any brains in the IAltSuply_clean
        {
            AltSuply A = new AltSuply();
            AltSuply_Clean B = new AltSuply_Clean(A);
            AltSuply_Clean_Null_Checks(B);
        }
        private void AltSuply_Clean_Null_Checks(AltSuply_Clean A)
        {
            Assert.IsFalse(A.PackSize == null);
            Assert.IsFalse(A.PartNumber == null);
            Assert.IsFalse(A.Preferred == null);
        }

    }
}
