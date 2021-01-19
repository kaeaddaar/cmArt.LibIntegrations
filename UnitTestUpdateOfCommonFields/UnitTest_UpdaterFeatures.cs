using cmArt.LibIntegrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestUpdateOfCommonFields
{
    [TestClass]
    public class UnitTest_UpdaterFeatures
    {
        const string firstItemStuff = "first item stuff";
        const string secondItemStuff = "second item stuff";
        const string fakeStuff = "fake stuff";
        const string newGuid2Data= "New Guid 2 data";

        
        [TestMethod]
        public void Test_GetRsWithDiffs_OneOfTwoRecordsChanges()
        {
            Updater<Data, Key> updater = new Updater<Data, Key>();
            updater.fGetKey = fGetKey;

            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            updater.SourceRecords = GetSourceRecords(guid1, guid2);
            updater.DestRecords = GetSourceRecords(guid2, guid1);

            IEnumerable<Tuple<Data, Data>> ChangedRecords = updater.GetRsWithDiffs();
            Tuple<Data, Data> UpdatedRecordPair = ChangedRecords.First();

            Assert.AreEqual(newGuid2Data, UpdatedRecordPair.Item1.Stuff);
        }

        [TestMethod]
        public void Test_source_and_destination_records_cant_be_changed_externally_once_initialized()
        {
            Updater<Data, Key> updater = new Updater<Data, Key>();
            IEnumerable<Data> Empty = new List<Data>();
            List<Data> OneRecord = new List<Data>();
            OneRecord.Add(new Data() { Stuff = string.Empty });

            updater.SourceRecords = OneRecord.AsEnumerable();
            updater.DestRecords = OneRecord.AsEnumerable();
            // shouldn't update
            // OneRecord.First().Stuff = "xxx"; // compiler doesn't allow
            Assert.AreEqual(string.Empty, updater.SourceRecords.First().Stuff);
            Assert.AreEqual(string.Empty, updater.DestRecords.First().Stuff);
        }

        [TestMethod]
        public void Test_source_and_destination_records_can_only_be_initialized()
        {
            Updater<Data, Key> updater = new Updater<Data, Key>();
            IEnumerable<Data> Empty = new List<Data>();
            List<Data> OneRecord = new List<Data>();
            OneRecord.Add(new Data());

            // empty then one record
            updater.SourceRecords = Empty.AsEnumerable();
            updater.SourceRecords = OneRecord.AsEnumerable();
            int SourceRecordCount = updater.SourceRecords.Count();
            Assert.AreEqual(0, SourceRecordCount);

            // one record then empty (opposite order from above)
            updater.DestRecords = OneRecord.AsEnumerable();
            updater.DestRecords = Empty.AsEnumerable();
            int DestRecordCount = updater.DestRecords.Count();
            Assert.AreEqual(1, DestRecordCount);

            // catch issues where source points at private dest instance
            updater.SourceRecords = Empty.AsEnumerable();
            updater.SourceRecords = OneRecord.AsEnumerable();
            int SourceRecordCount_Try2 = updater.SourceRecords.Count();
            Assert.AreEqual(0, SourceRecordCount_Try2);


        }

        [TestMethod]
        public void Test_fGetKey_Can_Only_Be_Initialized()
        {
            Func<Data, Key> fGetFakeKey;
            fGetFakeKey = (x) =>
            {
                return new Key() { Stuff = fakeStuff };
            };

            Updater<Data, Key> updater = new Updater<Data, Key>();
            updater.fGetKey = fGetKey;
            updater.fGetKey = fGetFakeKey; // attempt to override fGetKey with Fake Key

            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            updater.SourceRecords = GetSourceRecords(guid1, guid2);

            string result = updater.fGetKey(updater.SourceRecords.First()).Stuff;

            Assert.AreEqual(firstItemStuff, result);

        }

        private IEnumerable<Data> GetSourceRecords(Guid guid1, Guid guid2)
        {
            List<Data> data = new List<Data>();
            data.Add(new Data() { ID = guid1, Stuff = "first item stuff" });
            data.Add(new Data() { ID = guid2, Stuff = "second item stuff" });
            return data;
        }

        public struct Data
        {
            public Guid ID { get; set; }
            public String Stuff { get; set; }
        }
        public struct Key
        {
            public String Stuff { get; set; }
        }
        public Key fGetKey(Data data)
        {
            Key key = new Key();
            key.Stuff = data.Stuff;
            return key;
        }
    }
}
