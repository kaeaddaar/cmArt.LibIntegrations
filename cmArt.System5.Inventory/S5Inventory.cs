using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public class S5Inventory : IS5Inventory
    {
        public short Schedule_0 { get; set; }
        public short Schedule_List { get; set; }
        public short Schedule_Cash { get; set; }
        public short Schedule_Sale { get; set; }

        public S5Inventory()
        {
            AltSupplies = new List<IAltSuply>();
            CommentsLines = new List<IComments>();
            Inventry_27s = new List<IInventry_27>();
            InvPrices = new List<IInvPrice>();
            StokLines = new List<IStok>();
        }

        public S5Inventory
        (
            IEnumerable<IAltSuply> AltSuply_Records
            , IEnumerable<IComments> Comments_Records
            , IEnumerable<IInventry_27> Inventry_27_Records
            , IEnumerable<IInvPrice> InvPrice_Records
            , IEnumerable<IStok> Stok_Records
        )
        {
            IEnumerable<IAltSuply> tmpAltSuply_Records = AltSuply_Records ?? new List<IAltSuply>();
            var tmpComments_Records = Comments_Records ?? new List<IComments>();
            var tmpInventry_27_Records = Inventry_27_Records ?? new List<IInventry_27>();
            var tmpInvPrice_Records = InvPrice_Records ?? new List<IInvPrice>();
            var tmpStok_Records = Stok_Records ?? new List<IStok>();

            _AltSuplies = new List<IAltSuply>(tmpAltSuply_Records);
            _CommentsLines = new List<IComments>(tmpComments_Records);
            _Inventry_27s = new List<IInventry_27>(tmpInventry_27_Records);
            _InvPrices = new List<IInvPrice>(tmpInvPrice_Records);
            _StokRecords = new List<IStok>(tmpStok_Records);
        }

        public void Deconstruct
        (
            out IEnumerable<IAltSuply> AltSuply_Records
            , out IEnumerable<IComments> Comments_Records
            , out IEnumerable<IInventry_27> Inventry_27_Records
            , out IEnumerable<IInvPrice> InvPrice_Records
            , out IEnumerable<IStok> Stok_Records
        )
        {
            AltSuply_Records = new List<IAltSuply>(_AltSuplies);
            Comments_Records = new List<IComments>(_CommentsLines);
            Inventry_27_Records = new List<IInventry_27>(_Inventry_27s);
            InvPrice_Records = new List<IInvPrice>(_InvPrices);
            Stok_Records = new List<IStok>(_StokRecords);
        }

        private IEnumerable<IAltSuply> _AltSuplies;

        public IEnumerable<IAltSuply> AltSupplies
        {
            get { return new List<IAltSuply>(_AltSuplies); }
            set { _AltSuplies = new List<IAltSuply>(value); }
        }

        private IEnumerable<IComments> _CommentsLines;

        public IEnumerable<IComments> CommentsLines
        {
            get { return new List<IComments>(_CommentsLines); }
            set { _CommentsLines = new List<IComments>(value); }
        }

        private IEnumerable<IInventry_27> _Inventry_27s;

        public IEnumerable<IInventry_27> Inventry_27s
        {
            get { return new List<IInventry_27>(_Inventry_27s); }
            set { _Inventry_27s = new List<IInventry_27>(value); }
        }

        private IEnumerable<IInvPrice> _InvPrices;

        public IEnumerable<IInvPrice> InvPrices
        {
            get { return new List<IInvPrice>(_InvPrices); }
            set { _InvPrices = new List<IInvPrice>(value); }
        }

        private IEnumerable<IStok> _StokRecords;

        public IEnumerable<IStok> StokLines
        {
            get { return new List<IStok>(_StokRecords); }
            set { _StokRecords = new List<IStok>(value); }
        }

    }

}
