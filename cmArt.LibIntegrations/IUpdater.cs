using System;
using System.Collections.Generic;

namespace cmArt.LibIntegrations
{
    public interface IUpdater<TCommon, TKey>
    {
        IEnumerable<TCommon> DestRecords { get; set; }
        Func<TCommon, TKey> fGetKey { get; set; }
        IEnumerable<TCommon> SourceRecords { get; set; }

        IEnumerable<Tuple<TCommon, TCommon>> GetRsWithDiffs();
    }
}