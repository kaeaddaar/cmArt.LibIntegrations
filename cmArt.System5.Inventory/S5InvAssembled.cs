using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public struct S5InvAssembled : IS5InvAssembled
    {
        public S5InvAssembled
        (
            IInventry_27 Inv
            , IEnumerable<IInvPrice> Price_PerInventry_27
            , IEnumerable<IStok> Stok_PerInventry_27
            , IEnumerable<IComments> Comments_PerInventry_27
            , IEnumerable<IAltSuply> AltSuply_PerInventry_27
        )
        {
            _Inv = Inv;
            _Price_PerInventry_27 = Price_PerInventry_27;
            _Stok_PerInventry_27 = Stok_PerInventry_27;
            _Comments_PerInventry_27 = Comments_PerInventry_27;
            _AltSuply_PerInventry_27 = AltSuply_PerInventry_27;
        }

        private IEnumerable<IAltSuply> _AltSuply_PerInventry_27;

        public IEnumerable<IAltSuply> AltSuplies_PerInventry_27
        {
            get { return _AltSuply_PerInventry_27; }
        }


        private IInventry_27 _Inv;

        public IInventry_27 Inv
        {
            get { return _Inv; }
        }

        private IEnumerable<IInvPrice> _Price_PerInventry_27;

        public IEnumerable<IInvPrice> InvPrices_PerInventry_27
        {
            get { return _Price_PerInventry_27; }
        }

        private IEnumerable<IStok> _Stok_PerInventry_27;

        public IEnumerable<IStok> StokLines_PerInventry_27
        {
            get { return _Stok_PerInventry_27; }
        }

        private IEnumerable<IComments> _Comments_PerInventry_27;

        public IEnumerable<IComments> CommentsLines_PerInventry_27
        {
            get { return _Comments_PerInventry_27; }
        }

    }

}
