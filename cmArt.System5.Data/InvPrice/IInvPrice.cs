using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public interface IInvPrice
    {
        short Department { get; set; }
        DateTime EndDate { get; set; }
        int InvUnique { get; set; }
        int PartUnique { get; set; }
        float QuanDisc { get; set; }
        double RegularPrice { get; set; }
        string RScheduleType { get; set; }
        double SalePrice { get; set; }
        short ScheduleLevel { get; set; }
        string SScheduleType { get; set; }
        DateTime StartDate { get; set; }
    }

}
