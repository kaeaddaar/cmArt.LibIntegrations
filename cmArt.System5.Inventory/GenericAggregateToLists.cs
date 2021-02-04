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
            return results;
        }
    }
}
