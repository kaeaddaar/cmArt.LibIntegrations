using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.PagedJsonService;

namespace IntegrationTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_OdbcContext_and_PagedJsonContext_for_S5Inventory
    {
        [TestMethod]
        public void Test_delete_files_before_creating_new_cached_files()
        {
            Assert.IsTrue(false); // create the test, or test scenario where number of files would need to be reduced.
        }
        [TestMethod]
        public void Test_Writing_Paged_Json_files()
        {
            TestProgram.TestSavingPagedJson();
        }
        [TestMethod]
        public void Test_Odbc_does_it_load()
        {
            TestProgram.TestOdbc();

        }

        [TestMethod]
        public void Test_PagedJson_does_it_load()
        {
            TestProgram.TestReadingPagedJson();
        }

        private static class TestProgram
        {
            public static void TestOdbc() //Func<string, OdbcOptions> funcOptions
            {
                Options opt = OdbcOptions.GetOptions("DSN=LOCALDELTATEST");
                OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);
            }
            public static void TestReadingPagedJson() //Func<string, OdbcOptions> funcOptions
            {
                Options opt2 = PagedJsonOptions_S5Inventory.GetOptions
                    ("F:\\_Customers\\_ColonialPhotoNHobbyInc\\CachedFiles", new List<string>());
                PagedJsonContext context2 = new PagedJsonContext(opt2);
            }
            public static void TestSavingPagedJson()
            {
                Options opt2 = PagedJsonOptions_S5Inventory.GetOptions
                    ("F:\\_Customers\\_ColonialPhotoNHobbyInc\\CachedFiles", new List<string>());
                Options opt3 = PagedJsonOptions_S5Inventory.GetOptions
                    ("F:\\_Customers\\_ColonialPhotoNHobbyInc\\CachedFiles\\WritePagedJsonTest", new List<string>());
                PagedJsonContext context2 = new PagedJsonContext(opt2);
                context2.SaveToPagedFiles(opt3);

            }

        }


    }

}
