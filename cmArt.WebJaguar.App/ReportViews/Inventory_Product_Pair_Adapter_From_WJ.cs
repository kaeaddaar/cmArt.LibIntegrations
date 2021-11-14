using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ReportService;
using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.ReportViews
{

    public class Inventory_Pair_Adapter : IInventory_Pair_Flat
    {
        private IS5_CommonFields_In_WJ _S5;
        private IS5_CommonFields_In_WJ _Shopify;

        public string LeftDescription { get => ((IS5_CommonFields_In_WJ)_S5).Description; set => ((IS5_CommonFields_In_WJ)_S5).Description = value; }
        public string LeftCat { get => ((IS5_CommonFields_In_WJ)_S5).Cat; set => ((IS5_CommonFields_In_WJ)_S5).Cat = value; }
        public int LeftInvUnique { get => ((IS5_CommonFields_In_WJ)_S5).InvUnique; set => ((IS5_CommonFields_In_WJ)_S5).InvUnique = value; }
        public string LeftPartNumber { get => ((IS5_CommonFields_In_WJ)_S5).PartNumber; set => ((IS5_CommonFields_In_WJ)_S5).PartNumber = value; }
        public string LeftPrices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LeftQuantities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string RightDescription { get => ((IS5_CommonFields_In_WJ)_Shopify).Description; set => ((IS5_CommonFields_In_WJ)_Shopify).Description = value; }
        public string RightCat { get => ((IS5_CommonFields_In_WJ)_Shopify).Cat; set => ((IS5_CommonFields_In_WJ)_Shopify).Cat = value; }
        public int RightInvUnique { get => ((IS5_CommonFields_In_WJ)_Shopify).InvUnique; set => ((IS5_CommonFields_In_WJ)_Shopify).InvUnique = value; }
        public string RightPartNumber { get => ((IS5_CommonFields_In_WJ)_Shopify).PartNumber; set => ((IS5_CommonFields_In_WJ)_Shopify).PartNumber = value; }
        public string RightPrices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RightQuantities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Inventory_Pair_Adapter()
        {
            _Init();
        }
        public Inventory_Pair_Adapter(Generic_Pair<S5_CommonFields> WJ_Product_Pair)
        {
            _Init(WJ_Product_Pair);
        }
        private void _Init(Generic_Pair<S5_CommonFields> WJ_Product_Pair)
        {
            _S5 = WJ_Product_Pair.S5 ?? new S5_CommonFields();
            _Shopify = WJ_Product_Pair.External ?? new S5_CommonFields();
        }
        private void _Init()
        {
            _S5 = _S5 ?? new S5_CommonFields();
            _Shopify = _Shopify ?? new S5_CommonFields();
        }
        public void Init(Generic_Pair<S5_CommonFields> WJ_Product_Pair)
        {
            _Init(WJ_Product_Pair);
        }

    }

}
