using System;
using System.Collections.Generic;
using System.Text;
using cmArt.LibIntegrations.FileNamesService;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTest_cmArt.LibIntegrations
{
    [TestClass]
    public class Test_FileNameService
    {
        [TestMethod]
        public void GetExtension()
        {
            string FilePathAndName = "C:\\temp\\fileone.txt";
            FileNameService fpn = new FileNameService(FilePathAndName);
            string extension = fpn.GetExtention();
            Assert.AreEqual("txt", extension);
        }
        [TestMethod]
        public void GetFileName()
        {
            string FilePathAndName = "C:\\temp\\fileone.txt";
            FileNameService fpn = new FileNameService(FilePathAndName);
            string fileName = fpn.GetFileName();
            Assert.AreEqual("fileone.txt", fileName);
        }
        [TestMethod]
        public void GetFolder()
        {
            string FilePathAndName = "C:\\temp\\fileone.txt";
            FileNameService fpn = new FileNameService(FilePathAndName);
            string filePath = fpn.GetPath();
            Assert.AreEqual("C:\\temp\\", filePath);
        }

    }
}
