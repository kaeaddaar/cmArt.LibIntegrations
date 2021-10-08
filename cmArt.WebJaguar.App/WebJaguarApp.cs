using cmArt.LibIntegrations.SerializationService;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public class WebJaguarApp
    {
        public class CachingPattern<T>
        {
            private string _RootName;
            public CachingPattern(string RootName)
            {
                _RootName = RootName ?? string.Empty;
                if (_RootName.Count() < 1) { throw new Exception("RootName must have at least 1 character."); }
            }

            public IEnumerable<T> LoadCurrent(IEnumerable<T> current)
            {
                throw new NotImplementedException();
            }
        }

        public WebJaguarApp()
        {

        }

        public void _01_CurrCashToPrev()
        {

        }
        public void _02_ReloadCurrCash()
        {

        }
        public void _03_Transform()
        {

        }
        public void _04_GetChanges()
        {

        }
        public void _05_GetAdds()
        {

        }
        public void _06_PushChanges()
        {

        }
        public void _07_PushAdds()
        {

        }
        public void _08_BuildReports()
        {

        }

        private IEnumerable<IS5Inventory> Get_InventoryFromSystemFive()
        {
            throw new NotImplementedException();
        }
    }
}
