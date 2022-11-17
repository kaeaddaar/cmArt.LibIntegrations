using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.System5.Inventory
{
    public class GenericAggregateToLists<TGroupMe, TGroupByIndex> where TGroupByIndex : IComparable<TGroupByIndex>
    {
        public static List<List<TGroupMe>> ToLists(IEnumerable<TGroupMe> Objs, Func<TGroupMe, TGroupByIndex> fIndex)
        {
            var results = Objs.GroupBy(fIndex).Select(group => group.ToList()).ToList();

            int orig_num_records = Objs.Count();
            int number_of_records_within_pages = 0;
            foreach (var page in results) { number_of_records_within_pages += page.Count(); }

            if (orig_num_records != number_of_records_within_pages)
            {
                throw new Exception($"Wrong number of records in pages. Expected total number of records in pages to be {orig_num_records}, but there were {number_of_records_within_pages} across {results.Count()} pages");
            }

            return results;
        }
    }
}
