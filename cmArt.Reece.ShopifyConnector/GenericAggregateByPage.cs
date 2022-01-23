using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public static class GenericAggregateByPage<TGroupMe>
    {
        public static List<List<TGroupMe>> ToPages(IEnumerable<TGroupMe> Objs, int PageSize)
        {
            int count = 0;
            Func<TGroupMe, int> fGetPageNumber = (x) => (int)((count++ / PageSize) + 1);
            List<List<TGroupMe>> Pages = GenericAggregateToLists<TGroupMe,int>.ToLists(Objs, fGetPageNumber);

            return Pages;
        }
    }
}
