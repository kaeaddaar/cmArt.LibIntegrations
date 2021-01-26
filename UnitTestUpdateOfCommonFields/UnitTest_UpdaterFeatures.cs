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


        [TestMethod]
        public void Test_GetUpdatesByCommonFields_OneOfTwoRecordsChanges()
        {
            UpdateProcess<Data, Guid> updater = new UpdateProcess<Data, Guid>();
            updater.fGetKey = fGetKey;

            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            updater.SourceRecords = GetSourceRecords(guid1, guid2);
            updater.DestRecords = GetDifferentSourceRecords(guid1, guid2);

            IEnumerable<Tuple<Data, Data>> ChangedRecords = updater.GetUpdatesByCommonFields();
            Tuple<Data, Data> UpdatedRecordPair = ChangedRecords.FirstOrDefault();

            Assert.AreEqual(guid2, UpdatedRecordPair.Item1.ID);
            Assert.AreEqual(guid2, UpdatedRecordPair.Item2.ID);
            Assert.AreNotEqual(UpdatedRecordPair.Item1.Stuff, UpdatedRecordPair.Item2.Stuff);
            Assert.AreEqual(1, ChangedRecords.Count());
        }
        [TestMethod]
        public void Test_GetUpdatesByKeys_OneOfTwoRecordsChanges()
        {
            UpdateProcess<Data, Guid> updater = new UpdateProcess<Data, Guid>();
            updater.fGetKey = fGetKey;

            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            updater.SourceRecords = GetSourceRecords(guid1, guid2);
            updater.DestRecords = GetDifferentSourceRecords(guid1, guid2);

            IEnumerable<Data> ChangedRecords = updater.GetUpdatesByKeys();
            Data UpdatedRecord = ChangedRecords.FirstOrDefault();

            Assert.AreEqual(guid2, UpdatedRecord.ID);
            Assert.AreEqual(secondItemStuff, UpdatedRecord.Stuff); // src = original text so remove changes
            Assert.AreEqual(1, ChangedRecords.Count());
        }

        [TestMethod]
        public void Test_source_and_destination_records_cant_be_changed_externally_once_initialized()
        {
            UpdateProcess<Data, Key> updater = new UpdateProcess<Data, Key>();
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
            UpdateProcess<Data, Key> updater = new UpdateProcess<Data, Key>();
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
            Func<Data, Guid> fGetFakeKey;
            fGetFakeKey = (x) =>
            {
                return Guid.NewGuid();
            };

            UpdateProcess<Data, Guid> updater = new UpdateProcess<Data, Guid>();
            updater.fGetKey = fGetKey;
            updater.fGetKey = fGetFakeKey; // attempt to override fGetKey with Fake Key

            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            updater.SourceRecords = GetSourceRecords(guid1, guid2);

            Guid result = updater.fGetKey(updater.SourceRecords.First());

            Assert.AreEqual(guid1, result);

        }

        private IEnumerable<Data> GetSourceRecords(Guid guid1, Guid guid2)
        {
            List<Data> data = new List<Data>();
            data.Add(new Data() { ID = guid1, Stuff = firstItemStuff });
            data.Add(new Data() { ID = guid2, Stuff = secondItemStuff });
            return data;
        }

        private IEnumerable<Data> GetDifferentSourceRecords(Guid guid1, Guid guid2)
        {
            List<Data> data = new List<Data>();
            data.Add(new Data() { ID = guid1, Stuff = firstItemStuff});
            data.Add(new Data() { ID = guid2, Stuff = secondItemStuff + " changed" });
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
        public Guid fGetKey(Data data)
        {
            return data.ID;
        }
    }
}
