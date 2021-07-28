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
            UpdateProcess<IofT, Key> updater_Prices = new UpdateProcess<IofT, Key>();
            updater_Prices.fGetKey = fGetKey;
            updater_Prices.SourceRecords = adapters;
            updater_Prices.DestRecords = Data;

            IEnumerable<Tuple<IofT, IofT>> ChangedRecordPairs_Prices = updater_Prices.GetUpdatesByCommonFields(fEquals_Prices);
            IEnumerable<IofT> ChangedRecords_Prices = ChangedRecordPairs_Prices.Select(p => p.Item1);
            return ChangedRecords_Prices;
        }
    }
}
