using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using cmArt.LibIntegrations.PriceCalculations;
using cmArt.BevNet.System5;
#nullable enable


namespace cmArt.BevNet
{
    public class AdaptToDataLoadFormat : IDataLoadFormat
    {
        protected IS5InvAssembled _InvAss { get; set; }
        protected QryAccount _SuppInfo { get; set; }

        public AdaptToDataLoadFormat()
        {
            _InvAss = new S5InvAssembled();
            _SuppInfo = new QryAccount();
        }
        public void Init((IS5InvAssembled inv, QryAccount SuppInfo) pair)
        {
            _InvAss = pair.inv;
            _SuppInfo = pair.SuppInfo;
        }
        public string Cat { get => _InvAss.Inv.Cat; set => _InvAss.Inv.Cat = value ?? string.Empty; }
        public int InvUnique { get => _InvAss.Inv.InvUnique; set => _InvAss.Inv.InvUnique = value; }
        public string PartNumber { get => _InvAss.Inv.Part; set => _InvAss.Inv.Part = value ?? string.Empty; }
        public decimal PriceSchedule1_MSRP { get => (decimal)GetPriceSchedule(1).Price; 
            set => throw new NotImplementedException("Doesn't yet support converting a price back into percentage " +
                "based on a formula"); }
        public decimal PriceSchedule2_MinPrice { get => (decimal)GetPriceSchedule(0).Price; 
            set => throw new NotImplementedException("Doesn't yet support converting a price back into percentage " +
                "based on a formula"); }
        public string SupplierCode { get => _SuppInfo.BankInfo; set => _SuppInfo.BankInfo = value ?? string.Empty; }
        public string SupplierName { get => _SuppInfo.AName; set => _SuppInfo.AName = value ?? string.Empty; }
        public string SupplierPartNumber { get => _InvAss.Inv.SuppPart; set => _InvAss.Inv.SuppPart = value ?? string.Empty; }
        public decimal WholesaleCost { get => (decimal)_InvAss.Inv.Wholesale_1; set => _InvAss.Inv.Wholesale_1 = (double)value; }

        
        protected PriceScheduleView GetPriceSchedule(short ScheduleNum)
        {
            var FirstRecord = _InvAss;

            var Bases = FirstRecord.InvPrices_PerInventry_27.PopulateBasePriceSchedInfo_NoPrice(FirstRecord.Inv, 0, 0, 0);
            var prices = Bases.CalculatePrices(FirstRecord.InvPrices_PerInventry_27);

            var price = prices.Where(p => p.Level == ScheduleNum).FirstOrDefault();

            return price;
        }
    }
}
