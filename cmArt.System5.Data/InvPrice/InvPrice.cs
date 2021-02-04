using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class InvPrice : IInvPrice
    {
        public int InvUnique { get; set; } // InvPrice Unique
        public int PartUnique { get; set; }
        public Int16 ScheduleLevel { get; set; }
        public String RScheduleType { get; set; }
        public string SScheduleType { get; set; }
        public double RegularPrice { get; set; }
        public double SalePrice { get; set; }
        public Single QuanDisc { get; set; }
        public Int16 Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

}
