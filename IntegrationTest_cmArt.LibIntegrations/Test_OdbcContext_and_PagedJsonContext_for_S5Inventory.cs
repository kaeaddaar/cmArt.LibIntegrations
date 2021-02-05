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
        public void Test_Odbc_does_it_load()
        {
            TestProgram.TestOdbc();

        }

        [TestMethod]
        public void Test_PagedJson_does_it_load()
        {
            TestProgram.TestPagedJson();
        }

        private static class TestProgram
        {
            public static void TestOdbc() //Func<string, OdbcOptions> funcOptions
            {
                Options opt = OdbcOptions.GetOptions("DSN=LOCALDELTATEST");
                OdbcContext_S5Inventory context = new OdbcContext_S5Inventory(opt);
            }
            public static void TestPagedJson() //Func<string, OdbcOptions> funcOptions
            {
                Options opt2 = PagedJsonOptions_S5Inventory.GetOptions
                    ("F:\\_Customers\\_ColonialPhotoNHobbyInc\\CachedFiles", new List<string>());
                PagedJsonContext context2 = new PagedJsonContext(opt2);
            }

        }


    }

}
