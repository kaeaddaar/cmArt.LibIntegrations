using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cmArt.System5.Inventory;

namespace cmArt.LibIntegrations.VennMapService
{
    public class VennMap<T,IndexOfT>
    {
        private IEnumerable<T> _infoRecords { get; set; }
        private IEnumerable<IS5InvAssembled> _InvAssRecords { get; set; }
        private Func<T, IndexOfT> _Info_Index;
        private Func<IS5InvAssembled, IndexOfT> _S5InvAssembled_Index;

        // build zenn filter
        public IEnumerable<ValueTuple<T, IS5InvAssembled>> TOnly { get; set; }
        public IEnumerable<ValueTuple<T, IS5InvAssembled>> Both_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, IS5InvAssembled>> Both_NoEcomm { get; set; }
        public IEnumerable<ValueTuple<T, IS5InvAssembled>> InvOnly_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, IS5InvAssembled>> InvOnly_NoEcomm { get; set; }

        public List<ValueTuple<T, IS5InvAssembled>> Both { get; set; } // combination of two from above

        public VennMap
        (
            IEnumerable<T> InfoIn_Records
            , IEnumerable<IS5InvAssembled> S5InvAssembledIn_Records
            , Func<T, IndexOfT> Info_Index
            , Func<IS5InvAssembled, IndexOfT> S5InvAssembled_Index
        )
        {
            _infoRecords = InfoIn_Records;
            _InvAssRecords = S5InvAssembledIn_Records;
            _Info_Index = Info_Index;
            _S5InvAssembled_Index = S5InvAssembled_Index;

            if (Info_Index == null) { throw new ArgumentNullException("the Info_Index must not be null"); }
            _Info_Index = Info_Index;
            if (_S5InvAssembled_Index == null) { throw new ArgumentNullException("the S5InvAssembled_Index must not be null"); }
            _S5InvAssembled_Index = S5InvAssembled_Index;

            // check for null, or empty
            //if (_InvAss == null)
            //{
            //    throw new ArgumentNullException("Please pass in a valid S5InvAssembled object");
            //}
            if (_infoRecords == null)
            {
                throw new ArgumentNullException("Please pass in a valid object of type T");
            }

            IEnumerable<T> InfoRecords = InfoIn_Records;
            IEnumerable<IS5InvAssembled> InvAssRecords = S5InvAssembledIn_Records;

            var ABC =
                from infoRecord in InfoRecords
                join invRecord in InvAssRecords
                on _Info_Index(infoRecord) equals _S5InvAssembled_Index(invRecord)
                into invNullRecords
                from invNullRecord in invNullRecords.DefaultIfEmpty()
                select new ValueTuple<T, IS5InvAssembled>(infoRecord, invNullRecord);

            var DE =
                from invRecord in InvAssRecords
                join infoRecord in InfoRecords
                on _S5InvAssembled_Index(invRecord) equals _Info_Index(infoRecord)
                into infoNullRecords
                from infoNullRecord in infoNullRecords.DefaultIfEmpty()
                where infoNullRecord == null

                select new ValueTuple<T, IS5InvAssembled>(infoNullRecord, invRecord);

            TOnly = ABC.Where(x => x.Item2 == null).ToList();
            Both_Ecomm = ABC.Where(x => x.Item2 != null && x.Item2.Inv.Ecommerce == "Y").ToList();
            Both_NoEcomm = ABC.Where(x => x.Item2 != null && x.Item2.Inv.Ecommerce == "N").ToList();
            InvOnly_Ecomm = DE.Where(x => x.Item1 == null && x.Item2.Inv.Ecommerce == "Y").ToList();
            InvOnly_NoEcomm = DE.Where(x => x.Item1 == null && x.Item2.Inv.Ecommerce == "N").ToList();

            Both = new List<ValueTuple<T, IS5InvAssembled>>(Both_Ecomm);
            foreach ((T, IS5InvAssembled) rs in Both_NoEcomm)
            {
                Both.Add(rs);
            }
        }

    }

}
