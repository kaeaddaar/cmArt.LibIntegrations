using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cmArt.System5.Inventory;


namespace cmArt.LibIntegrations.VennMapService
{
    public class VennMap<T, TWithCondition, IndexOfT>
    {
        private IEnumerable<T> _infoRecords { get; set; }
        private IEnumerable<TWithCondition> _InvAssRecords { get; set; }
        private Func<T, IndexOfT> _Info_Index;
        private Func<TWithCondition, IndexOfT> _S5_Index;
        //private Func<IS5InvAssembled, IndexOfT> _S5InvAssembled_Index;
        private Func<TWithCondition, bool> _VennCondition;

        // build zenn filter
        public IEnumerable<ValueTuple<T, TWithCondition>> TOnly { get; set; }
        public IEnumerable<ValueTuple<T, TWithCondition>> Both_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, TWithCondition>> Both_NoEcomm { get; set; }
        public IEnumerable<ValueTuple<T, TWithCondition>> InvOnly_Ecomm { get; set; }
        public IEnumerable<ValueTuple<T, TWithCondition>> InvOnly_NoEcomm { get; set; }

        public List<ValueTuple<T, TWithCondition>> Both { get; set; } // combination of two from above

        public VennMap
        (
            IEnumerable<T> InfoIn_Records
            , IEnumerable<TWithCondition> S5_Records
            , Func<T, IndexOfT> Info_Index
            , Func<TWithCondition, IndexOfT> S5_Index
            , Func<TWithCondition, bool> VennCondition_EcommEqualsY
        )
        {
            _infoRecords = InfoIn_Records;
            _InvAssRecords = S5_Records;

            if (Info_Index == null) { throw new ArgumentNullException("the Info_Index must not be null"); }
            _Info_Index = Info_Index;
            if (S5_Index == null) { throw new ArgumentNullException("the Info_Index must not be null"); }
            _S5_Index = S5_Index;
            if (VennCondition_EcommEqualsY == null) { throw new ArgumentNullException("the Info_Index must not be null"); }
            _VennCondition = VennCondition_EcommEqualsY;

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
            IEnumerable<TWithCondition> InvAssRecords = S5_Records;

            var ABC =
                from infoRecord in InfoRecords
                join invRecord in InvAssRecords
                on _Info_Index(infoRecord) equals _S5_Index(invRecord)
                into invNullRecords
                from invNullRecord in invNullRecords.DefaultIfEmpty()
                select new ValueTuple<T, TWithCondition>(infoRecord, invNullRecord);

            var DE =
                from invRecord in InvAssRecords
                join infoRecord in InfoRecords
                on _S5_Index(invRecord) equals _Info_Index(infoRecord)
                into infoNullRecords
                from infoNullRecord in infoNullRecords.DefaultIfEmpty()
                where infoNullRecord == null

                select new ValueTuple<T, TWithCondition>(infoNullRecord, invRecord);

            TOnly = ABC.Where(x => x.Item2 == null).ToList();
            Both_Ecomm = ABC.Where(x => x.Item2 != null && VennCondition_EcommEqualsY(x.Item2)).ToList();
            Both_NoEcomm = ABC.Where(x => x.Item2 != null && !VennCondition_EcommEqualsY(x.Item2)).ToList();
            InvOnly_Ecomm = DE.Where(x => x.Item1 == null && VennCondition_EcommEqualsY(x.Item2)).ToList();
            InvOnly_NoEcomm = DE.Where(x => x.Item1 == null && !VennCondition_EcommEqualsY(x.Item2)).ToList();

            Both = new List<ValueTuple<T, TWithCondition>>(Both_Ecomm);
            foreach ((T, TWithCondition) rs in Both_NoEcomm)
            {
                Both.Add(rs);
            }
        }

    }

}
