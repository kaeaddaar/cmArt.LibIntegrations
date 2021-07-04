using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Linq;


namespace cmArt.LibIntegrations
{
    public class UpdateProcess<TCommon, TKey> : IUpdateProcess<TCommon, TKey>
    {
        private IEnumerable<TCommon> _SourceRecords;
        private IEnumerable<TCommon> _DestRecords;

        public IEnumerable<TCommon> SourceRecords
        {
            get
            {
                return _SourceRecords;
            }
            set
            {
                _SourceRecords = _SourceRecords ?? value.AsEnumerable();
            }
        }

        public IEnumerable<TCommon> DestRecords
        {
            get
            {
                return _DestRecords;
            }
            set
            {
                _DestRecords = _DestRecords ?? value.AsEnumerable();
            }
        }
        private Func<TCommon, TKey> _fGetKey;
        public Func<TCommon, TKey> fGetKey
        {
            get
            {
                return _fGetKey;
            }
            set
            {
                _fGetKey = _fGetKey ?? value;
            }
        }

        public UpdateProcess()
        {
        }
        public IEnumerable<TCommon> GetUpdatesByKeys()
        {
            return GetUpdatesByKeys
            (
                SrcRecords_UnfilteredIn: _SourceRecords
                , DestRecords_UnfilteredIn: _DestRecords
                , fGetKey: _fGetKey
            );
        }
        public IEnumerable<TKey> GetMatchingKeys
        (
            IEnumerable<TCommon> SrcRecords_UnfilteredIn
            , IEnumerable<TCommon> DestRecords_UnfilteredIn
            , Func<TCommon, TKey> fGetKey
        )
        {
            IEnumerable<TCommon> SrcRecords_Unfiltered = SrcRecords_UnfilteredIn ?? new List<TCommon>();
            IEnumerable<TCommon> DestRecords_Unfiltered = DestRecords_UnfilteredIn ?? new List<TCommon>();

            var SrcKeys = SrcRecords_Unfiltered.Select(x => fGetKey(x));
            var DestKeys = DestRecords_Unfiltered.Select(x => fGetKey(x));

            var Matching_Keys =
                from src in SrcKeys
                join dest in DestKeys
                on src equals dest
                select src;
            return Matching_Keys;
        }

        private IEnumerable<TCommon> GetUpdatesByKeys // source records that match and have changes are the updated destination records
        (
            IEnumerable<TCommon> SrcRecords_UnfilteredIn
            , IEnumerable<TCommon> DestRecords_UnfilteredIn
            , Func<TCommon, TKey> fGetKey
        )
        {
            IEnumerable<TCommon> SrcRecords_Unfiltered = SrcRecords_UnfilteredIn ?? new List<TCommon>();
            IEnumerable<TCommon> DestRecords_Unfiltered = DestRecords_UnfilteredIn ?? new List<TCommon>();

            var SrcKeys = SrcRecords_Unfiltered.Select(x => fGetKey(x));
            var DestKeys = DestRecords_Unfiltered.Select(x => fGetKey(x));

            var Matching_Keys =
                from src in SrcKeys
                join dest in DestKeys
                on src equals dest
                select src;

            var SrcRecords_Filtered =
                from SrcRecord in SrcRecords_Unfiltered
                join Key in Matching_Keys
                on fGetKey(SrcRecord) equals Key
                select SrcRecord;

            var DestRecords_Filtered =
                from DestRecord in DestRecords_Unfiltered
                join Key in Matching_Keys
                on fGetKey(DestRecord) equals Key
                select DestRecord;

            var UpdatedDestRecords =
                from SrcRecord in SrcRecords_Filtered
                join DestRecord in DestRecords_Filtered
                on fGetKey(SrcRecord) equals fGetKey(DestRecord)
                where !SrcRecord.Equals(DestRecord)
                select SrcRecord;

            return UpdatedDestRecords;
        }

        public IEnumerable<Tuple<TCommon, TCommon>> GetUpdatesByCommonFields(Func<TCommon,TCommon,bool> fEquals)
        {
            return GetUpdatesByCommonFields(SourceRecords: _SourceRecords, DestRecords: _DestRecords, fEquals: fEquals);
        }
        private IEnumerable<Tuple<TCommon, TCommon>> GetUpdatesByCommonFields
        (
            IEnumerable<TCommon> SourceRecords
            , IEnumerable<TCommon> DestRecords
            , Func<TCommon, TCommon, bool> fEquals)
        {
            IEnumerable<TCommon> srcRs = SourceRecords;
            IEnumerable<TCommon> destRs = DestRecords;

            var Common =
            from src in srcRs
            join dest in destRs
            on _fGetKey(src) equals _fGetKey(dest)
            where !fEquals(src, dest)
            select new Tuple<TCommon, TCommon>(src, dest);

            int CountCommon = Common.Count();
            bool result;
            foreach (Tuple<TCommon, TCommon> CommonPair in Common)
            {
                result = fEquals(CommonPair.Item1, CommonPair.Item2);
            }

            return Common;
        }

    }
}
