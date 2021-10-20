using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.Data
{
    public static class IS5_CommonFields_In_WJ_Indexes
    {
        public static int UniqueId(IS5_CommonFields_In_WJ data)
        {
            IS5_CommonFields_In_WJ _data = data ?? new S5_CommonFields();
            return _data.InvUnique;
        }
    }
}
