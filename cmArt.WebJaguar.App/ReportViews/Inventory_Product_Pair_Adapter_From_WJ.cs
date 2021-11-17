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
        public string LeftPrices { get; set; }
        public string LeftQuantities { get; set; }
        public string LeftBarcodes { get; set; }
        public string LeftFF22 { get; set; }
        public string LeftWebDescription { get; set; }
        public float LeftWeight { get; set; }

        public string RightDescription { get => ((IS5_CommonFields_In_WJ)_Shopify).Description; set => ((IS5_CommonFields_In_WJ)_Shopify).Description = value; }
        public string RightCat { get => ((IS5_CommonFields_In_WJ)_Shopify).Cat; set => ((IS5_CommonFields_In_WJ)_Shopify).Cat = value; }
        public int RightInvUnique { get => ((IS5_CommonFields_In_WJ)_Shopify).InvUnique; set => ((IS5_CommonFields_In_WJ)_Shopify).InvUnique = value; }
        public string RightPartNumber { get => ((IS5_CommonFields_In_WJ)_Shopify).PartNumber; set => ((IS5_CommonFields_In_WJ)_Shopify).PartNumber = value; }
        public string RightPrices { get; set; }
        public string RightQuantities { get; set; }
        public string RightBarcodes { get; set; }
        public string RightFF22 { get; set; }
        public string RightWebDescription { get; set; }
        public float RightWeight { get; set; }

        public S5_CommonFields_Pairs_Flat AsS5_CommonFields_Pairs_Flat()
        {
            S5_CommonFields_Pairs_Flat result = new S5_CommonFields_Pairs_Flat();
            result.Leftbarcodes = this.LeftBarcodes;
            result.LeftCat = this.LeftCat;
            result.LeftDescription = this.LeftDescription;
            result.LeftFF22 = this.LeftFF22;
            result.LeftInvUnique = this.LeftInvUnique;
            result.LeftPartNumber = this.LeftPartNumber;
            result.LeftPrices = this.LeftPrices;
            result.LeftQuantities = this.LeftQuantities;
            result.LeftWebDescription = this.LeftWebDescription;
            result.Leftweight = this.LeftWeight;

            result.Rightbarcodes = this.RightBarcodes;
            result.RightCat = this.RightCat;
            result.RightDescription = this.RightDescription;
            result.RightFF22 = this.RightFF22;
            result.RightInvUnique = this.RightInvUnique;
            result.RightPartNumber = this.RightPartNumber;
            result.RightPrices = this.RightPrices;
            result.RightQuantities = this.RightQuantities;
            result.RightWebDescription = this.RightWebDescription;
            result.Rightweight = this.RightWeight;

            return result;
        }

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
