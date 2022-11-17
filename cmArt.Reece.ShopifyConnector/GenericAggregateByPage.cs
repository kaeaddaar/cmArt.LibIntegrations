using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
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

            // checks
            int orig_num_records = Objs.Count();
            int number_of_records_within_pages = 0;
            foreach(var page in Pages) { number_of_records_within_pages += page.Count(); }

            if (orig_num_records != number_of_records_within_pages)
            {
                throw new Exception($"Wrong number of records in pages. Expected total number of records in pages to be {orig_num_records}, but there were {number_of_records_within_pages} across {Pages.Count()} pages");
            }

            return Pages;
        }
    }
}
