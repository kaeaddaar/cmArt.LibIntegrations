using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.ReportViews
{
    public class S5_CommonFields_Pairs_Flat
    {
        public IEnumerable<string> Leftbarcodes { get; set; }
        public string LeftDescription { get; set; }
        public int LeftInvUnique { get; set; }
        public string LeftPartNumber { get; set; }
        public string LeftWebDescription { get; set; }
        public float Leftweight { get; set; }
        public string LeftCat { get; set; }
        public string LeftFF22 { get; set; }

        public IEnumerable<string> Rightbarcodes { get; set; }
        public string RightDescription { get; set; }
        public int RightInvUnique { get; set; }
        public string RightPartNumber { get; set; }
        public string RightWebDescription { get; set; }
        public float Rightweight { get; set; }
        public string RightCat { get; set; }
        public string RightFF22 { get; set; }
    }
}
