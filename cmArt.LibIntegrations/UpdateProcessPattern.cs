using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.LibIntegrations
{
    public static class UpdateProcessPattern<IofT, T, Key>
    {
        public static IEnumerable<Tuple<IofT, IofT>> GetChangedRecordPairs
        (
            Func<IofT, Key> fGetKey
            , Func<IofT, IofT, bool> fEquals
            , IEnumerable<IofT> adapters
            , IEnumerable<IofT> Data
        )
        {
            UpdateProcess<IofT, Key> updater = new UpdateProcess<IofT, Key>();
            updater.fGetKey = fGetKey;
            updater.SourceRecords = adapters;
            updater.DestRecords = Data;

            IEnumerable<Tuple<IofT, IofT>> ChangedRecordPairs = updater.GetUpdatesByCommonFields(fEquals);
            return ChangedRecordPairs;
        }       
        public static IEnumerable<IofT> GetChangedRecords
        (
            Func<IofT, Key> fGetKey
            , Func<IofT, IofT, bool> fEquals
            , IEnumerable<IofT> adapters
            , IEnumerable<IofT> Data
        )
        {
            IEnumerable<Tuple<IofT, IofT>> ChangedRecordPairs = GetChangedRecordPairs(fGetKey, fEquals, adapters, Data);
            IEnumerable<IofT> ChangedRecords = ChangedRecordPairs.Select(p => p.Item1);
            return ChangedRecords;
        }
    }
}
