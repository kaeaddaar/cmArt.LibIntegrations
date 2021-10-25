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
        private IEnumerable<S5InvAssembledObj> _InvAssRecords { get; set; }
        private Func<T, IndexOfT> _Info_Index;
        private Func<S5InvAssembledObj, IndexOfT> _S5InvAssembled_Index;

        // build zenn filter
        public IEnumerable<ValueTuple<T, S5InvAssembledObj>> TOnly { get; set; }
        public IEnumerable<ValueTuple<T, S5InvAssembledObj>> Both_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, S5InvAssembledObj>> Both_NoEcomm { get; set; }
        public IEnumerable<ValueTuple<T, S5InvAssembledObj>> InvOnly_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, S5InvAssembledObj>> InvOnly_NoEcomm { get; set; }

        public List<ValueTuple<T, S5InvAssembledObj>> Both { get; set; } // combination of two from above

        public VennMap
        (
            IEnumerable<T> InfoIn_Records
            , IEnumerable<S5InvAssembledObj> S5InvAssembledIn_Records
            , Func<T, IndexOfT> Info_Index
            , Func<S5InvAssembledObj, IndexOfT> S5InvAssembled_Index
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
            IEnumerable<S5InvAssembledObj> Inventry_27s = S5InvAssembledIn_Records;

            var ABC =
                from infoRecord in InfoRecords
                join invRecord in Inventry_27s
                on _Info_Index(infoRecord) equals _S5InvAssembled_Index(invRecord)
                into invNullRecords
                from invNullRecord in invNullRecords.DefaultIfEmpty()
                select new ValueTuple<T, S5InvAssembledObj>(infoRecord, invNullRecord);

            var DE =
                from invRecord in Inventry_27s
                join infoRecord in InfoRecords
                on _S5InvAssembled_Index(invRecord) equals _Info_Index(infoRecord)
                into infoNullRecords
                from infoNullRecord in infoNullRecords.DefaultIfEmpty()
                where infoNullRecord == null

                select new ValueTuple<T, S5InvAssembledObj>(infoNullRecord, invRecord);

            TOnly = ABC.Where(x => x.Item2 == null);
            Both_Ecomm = ABC.Where(x => x.Item2 != null && x.Item2.Inv.Ecommerce == "Y");
            Both_NoEcomm = ABC.Where(x => x.Item2 != null && x.Item2.Inv.Ecommerce == "N");
            InvOnly_Ecomm = DE.Where(x => x.Item1 == null && x.Item2.Inv.Ecommerce == "Y");
            InvOnly_NoEcomm = DE.Where(x => x.Item1 == null && x.Item2.Inv.Ecommerce == "N");

            Both = new List<ValueTuple<T, S5InvAssembledObj>>(Both_Ecomm);
            foreach (var rs in Both_NoEcomm)
            {
                Both.Add(rs);
            }
        }

    }

}
