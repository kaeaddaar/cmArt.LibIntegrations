using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.LibIntegrations
{
    public static class UpdateProcessPattern<IofT, T, Key>
    {
        public static IEnumerable<IofT> GetChangedRecords
        (
            Func<IofT, Key> fGetKey
            , Func<IofT, IofT, bool> fEquals_Prices
            , IEnumerable<IofT> adapters
            , IEnumerable<IofT> Data
        )
        {
            // get changed records (prices comparison)
            UpdateProcess<IofT, Key> updater = new UpdateProcess<IofT, Key>();
            updater.fGetKey = fGetKey;
            updater.SourceRecords = adapters;
            updater.DestRecords = Data;

            IEnumerable<Tuple<IofT, IofT>> ChangedRecordPairs_Prices = updater.GetUpdatesByCommonFields(fEquals_Prices);
            IEnumerable<IofT> ChangedRecords_Prices = ChangedRecordPairs_Prices.Select(p => p.Item1);
            return ChangedRecords_Prices;
        }
    }
}
