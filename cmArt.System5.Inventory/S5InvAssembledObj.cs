using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.System5.Inventory
{
    public class S5InvAssembledObj
    {
        public S5InvAssembledObj
        (
            IInventry_27 Inv
            , IEnumerable<IInvPrice> Price_PerInventry_27
            , IEnumerable<IStok> Stok_PerInventry_27
            , IEnumerable<IComments> Comments_PerInventry_27
            , IEnumerable<IAltSuply> AltSuply_PerInventry_27
        )
        {
            _Inv = Inv;
            _Price_PerInventry_27 = (Price_PerInventry_27 ?? new List<IInvPrice>()).ToList();
            _Stok_PerInventry_27 = (Stok_PerInventry_27 ?? new List<IStok>()).ToList();
            _Comments_PerInventry_27 = (Comments_PerInventry_27 ?? new List<IComments>()).ToList();
            _AltSuply_PerInventry_27 = (AltSuply_PerInventry_27 ?? new List<IAltSuply>()).ToList();
        }

        private List<IAltSuply> _AltSuply_PerInventry_27;

        public List<IAltSuply> AltSuplies_PerInventry_27
        {
            get { return _AltSuply_PerInventry_27; }
        }


        private IInventry_27 _Inv;

        public IInventry_27 Inv
        {
            get { return _Inv; }
        }

        private List<IInvPrice> _Price_PerInventry_27;

        public List<IInvPrice> InvPrices_PerInventry_27
        {
            get { return _Price_PerInventry_27; }
        }

        private List<IStok> _Stok_PerInventry_27;

        public List<IStok> StokLines_PerInventry_27
        {
            get { return _Stok_PerInventry_27; }
        }

        private List<IComments> _Comments_PerInventry_27;

        public List<IComments> CommentsLines_PerInventry_27
        {
            get { return _Comments_PerInventry_27; }
        }

    }

}
