using System;
using System.Collections.Generic;

namespace cmArt.LibIntegrations
{
    public interface IUpdateProcess<TCommon, TKey>
    {
        IEnumerable<TCommon> DestRecords { get; set; }
        Func<TCommon, TKey> fGetKey { get; set; }
        IEnumerable<TCommon> SourceRecords { get; set; }

        IEnumerable<Tuple<TCommon, TCommon>> GetUpdatesByCommonFields(Func<TCommon,TCommon,bool> fEquals);
        IEnumerable<TCommon> GetUpdatesByKeys();
    }
}