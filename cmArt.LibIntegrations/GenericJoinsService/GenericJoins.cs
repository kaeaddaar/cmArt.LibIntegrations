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
            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftRecord in LeftRecords
                join RightRecord in RightRecords
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
            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftRecord in LeftRecords
                join RightRecord in RightRecords
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
            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from LeftNullRecord in LeftRecords.DefaultIfEmpty()
                join RightRecord in RightRecords
                on LeftKey(LeftNullRecord) equals RightKey(RightRecord)
                into RightNullRecords
                from RightNullRecord in RightNullRecords.DefaultIfEmpty()
                select new Tuple<TLeft, TRight>(item1: LeftNullRecord, item2: RightNullRecord);

            return results;
        }
        public static IEnumerable<Tuple<TLeft, TRight>> RightJoin
        (
            IEnumerable<TLeft> LeftRecords
            , IEnumerable<TRight> RightRecords
            , Func<TLeft, iKey> LeftKey
            , Func<TRight, iKey> RightKey
        )
        {
            IEnumerable<Tuple<TLeft, TRight>> results = new List<Tuple<TLeft, TRight>>();

            results =
                from RightRecord in RightRecords
                join LeftRecord in LeftRecords
                on RightKey(RightRecord) equals LeftKey(LeftRecord)
                into LeftNullRecords
                from LeftNullRecord in LeftNullRecords.DefaultIfEmpty()
                select new Tuple<TLeft, TRight>(item1: LeftNullRecord, item2: RightRecord);

            return results;
        }
    }

}
