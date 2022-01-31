using System;
using System.Collections.Generic;
using System.Linq;


namespace cmArt.LibIntegrations.GenericJoinsService
{
    public class GenericJoins<TLeft, TRight, iKey>
    {
        public static IEnumerable<Tuple<TLeft, TRight>> InnerJoin
        (
            IEnumerable<TLeft> LeftRecords
            , IEnumerable<TRight> RightRecords
            , Func<TLeft, iKey> LeftKey
            , Func<TRight, iKey> RightKey
        )
        {
            IEnumerable<TLeft> _LeftRecords = LeftRecords ?? new List<TLeft>();
            IEnumerable<TRight> _RightRecords = RightRecords ?? new List<TRight>();

            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftRecord in _LeftRecords
                join RightRecord in _RightRecords
                on LeftKey(LeftRecord) equals RightKey(RightRecord)
                select new Tuple<TLeft, TRight>(item1: LeftRecord, item2: RightRecord);

            return results;
        }
        public static IEnumerable<Tuple<TLeft, TRight>> LeftJoin
        (
            IEnumerable<TLeft> LeftRecords
            , IEnumerable<TRight> RightRecords
            , Func<TLeft, iKey> LeftKey
            , Func<TRight, iKey> RightKey
        )
        {
            IEnumerable<TLeft> _LeftRecords = LeftRecords ?? new List<TLeft>();
            IEnumerable<TRight> _RightRecords = RightRecords ?? new List<TRight>();

            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftRecord in _LeftRecords
                join RightRecord in _RightRecords
                on LeftKey(LeftRecord) equals RightKey(RightRecord)
                into RightNullRecords
                from RightNullRecord in RightNullRecords.DefaultIfEmpty()
                select new Tuple<TLeft, TRight>(item1: LeftRecord, item2: RightNullRecord);

            return results;
        }
        public static IEnumerable<Tuple<TLeft, TRight>> FullOuterJoin
        (
            IEnumerable<TLeft> LeftRecords
            , IEnumerable<TRight> RightRecords
            , Func<TLeft, iKey> LeftKey
            , Func<TRight, iKey> RightKey
        )
        {
            IEnumerable<TLeft> _LeftRecords = LeftRecords ?? new List<TLeft>();
            IEnumerable<TRight> _RightRecords = RightRecords ?? new List<TRight>();

            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftNullRecord in _LeftRecords.DefaultIfEmpty()
                join RightRecord in _RightRecords
                on LeftKey(LeftNullRecord) equals RightKey(RightRecord)
                into RightNullRecords
                from RightNullRecord in RightNullRecords.DefaultIfEmpty()
                select new Tuple<TLeft, TRight>(item1: LeftNullRecord, item2: RightNullRecord);

            IEnumerable<Tuple<TLeft, TRight>> results2 = new List<Tuple<TLeft, TRight>>();

            results2 =
                from RightNullRecord in _RightRecords.DefaultIfEmpty()
                join LeftRecord in _LeftRecords
                on RightKey(RightNullRecord) equals LeftKey(LeftRecord)
                into LeftNullRecords
                from LeftNullRecord in LeftNullRecords.DefaultIfEmpty()
                where LeftNullRecord == null
                select new Tuple<TLeft, TRight>(item1: LeftNullRecord, item2: RightNullRecord);

            List<Tuple<TLeft, TRight>> finalResults = new List<Tuple<TLeft, TRight>>();
            foreach (var r in results)
            {
                finalResults.Add(r);
            }
            foreach (var r2 in results2)
            {
                finalResults.Add(r2);
            }
            return finalResults;
        }
        public static IEnumerable<Tuple<TLeft, TRight>> RightJoin
        (
            IEnumerable<TLeft> LeftRecords
            , IEnumerable<TRight> RightRecords
            , Func<TLeft, iKey> LeftKey
            , Func<TRight, iKey> RightKey
        )
        {
            IEnumerable<TLeft> _LeftRecords = LeftRecords ?? new List<TLeft>();
            IEnumerable<TRight> _RightRecords = RightRecords ?? new List<TRight>();

            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from RightRecord in _RightRecords
                join LeftRecord in _LeftRecords
                on RightKey(RightRecord) equals LeftKey(LeftRecord)
                into LeftNullRecords
                from LeftNullRecord in LeftNullRecords.DefaultIfEmpty()
                select new Tuple<TLeft, TRight>(item1: LeftNullRecord, item2: RightRecord);

            return results;
        }
    }

}
