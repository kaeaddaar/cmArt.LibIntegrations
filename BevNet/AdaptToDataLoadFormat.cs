using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace cmArt.BevNet
{
    public class AdaptToDataLoadFormat : IDataLoadFormat
    {
        private IS5InvAssembled _InvAss;

        public AdaptToDataLoadFormat()
        {

        }

        public string Cat { get => _InvAss.Inv.Cat; set => _InvAss.Inv.Cat = value ?? string.Empty; }
        public int InvUnique { get => _InvAss.Inv.InvUnique; set => _InvAss.Inv.InvUnique = value; }
        public string PartNumber { get => _InvAss.Inv.Part; set => _InvAss.Inv.Part = value ?? string.Empty; }
        public decimal PriceSchedule1_MSRP { get => (decimal)GetPriceSchedule(1).RegularPrice; set => throw new NotImplementedException(); }
        public decimal PriceSchedule2_MinPrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SupplierCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SupplierName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SupplierPartNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal WholesaleCost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void init(S5InvAssembled InvAss)
        {
            _InvAss = InvAss;
        }

        protected IInvPrice GetPriceSchedule(short ScheduleNum)
        {
            IInvPrice sched = this._InvAss.InvPrices_PerInventry_27
                .Where(sched => sched.ScheduleLevel == ScheduleNum).FirstOrDefault() 
                ?? new InvPrice();

            throw new NotImplementedException("we don't need this we need price calculation logic");
            return sched;
        }
    }
}
