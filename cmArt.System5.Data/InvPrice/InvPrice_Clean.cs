using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable


namespace cmArt.System5.Data
{
    public class InvPrice_Clean : IInvPrice
    {
        private IInvPrice _InvPrice;

        public InvPrice_Clean(IInvPrice component)
        {
            _InvPrice = component ?? new InvPrice();

            _InvPrice.RScheduleType = _InvPrice.RScheduleType ?? string.Empty;
            _InvPrice.SScheduleType = _InvPrice.SScheduleType ?? string.Empty;
            ((IInvPrice)this).CopyFrom(component);
        }

        public void Clean()
        {
            Clean_RScheduleType();
            Clean_SScheduleType();
        }
        public void Clean_RScheduleType()
        {
            _InvPrice.RScheduleType = _InvPrice.RScheduleType.TrimOption();
        }
        public void Clean_SScheduleType()
        {
            _InvPrice.SScheduleType = _InvPrice.SScheduleType.TrimOption();
        }

        public string RScheduleTypeIndex
        {
            get { return _InvPrice.RScheduleType; } // for performance, may not be clean
        }

        public string Part_Raw { get { return _InvPrice.RScheduleType ?? string.Empty; } }
        public string RScheduleType
        {
            get { return _InvPrice.RScheduleType.TrimOption(); }
            set { _InvPrice.RScheduleType = value.TrimOption(); }
        }
        public string SScheduleType
        {
            get { return _InvPrice.SScheduleType.TrimOption(); }
            set { _InvPrice.SScheduleType = value.TrimOption(); }
        }

        public short Department { get => _InvPrice.Department; set => _InvPrice.Department = value; }
        public DateTime EndDate { get => _InvPrice.EndDate; set => _InvPrice.EndDate = value; }
        public int InvUnique { get => _InvPrice.InvUnique; set => _InvPrice.InvUnique = value; }
        public int PartUnique { get => _InvPrice.PartUnique; set => _InvPrice.PartUnique = value; }
        public float QuanDisc { get => _InvPrice.QuanDisc; set => _InvPrice.QuanDisc = value; }
        public double RegularPrice { get => _InvPrice.RegularPrice; set => _InvPrice.RegularPrice = value; }
        public double SalePrice { get => _InvPrice.SalePrice; set => _InvPrice.SalePrice = value; }
        public short ScheduleLevel { get => _InvPrice.ScheduleLevel; set => _InvPrice.ScheduleLevel = value; }
        public DateTime StartDate { get => _InvPrice.StartDate; set => _InvPrice.StartDate = value; }
    }

}
