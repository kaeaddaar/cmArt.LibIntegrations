﻿using cmArt.System5.Inventory;
using cmArt.WebJaguar.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.Data
{
    public class AdaptToProduct_Root : IProduct_Root
    {
        protected IS5InvAssembled _InvAss { get; set; }

        public AdaptToProduct_Root()
        {
            _InvAss = new S5InvAssembled();
        }
        public void Init(IS5InvAssembled inv)
        {
            _InvAss = inv;
        }
        #region bool active
        private bool _active;
        /// <summary>
        /// Currently set to Ecomm flag, but may use a Freeform field in the future
        /// </summary>
        public bool active
        {
            get
            {
                return (_InvAss.Inv.Ecommerce.ToUpper() == "Y");
            }

            set
            {
                if (value)
                {
                    _InvAss.Inv.Ecommerce = "Y";
                }
                else
                {
                    _InvAss.Inv.Ecommerce = "N";
                }
            }
        }
        #endregion bool active
        public string name
        {
            get
            {
                return _InvAss.Inv.Description ?? string.Empty;
            }
            set
            {
                _InvAss.Inv.Description = value ?? string.Empty;
            }

        }
        public string sku
        {
            get
            {
                return _InvAss.Inv.Part ?? string.Empty;
            }
            set
            {
                _InvAss.Inv.Part = value ?? string.Empty;
            }
        }
        public string upc
        {
            get
            {
                IEnumerable<String> Barcodes = _InvAss.AltSuplies_PerInventry_27.Select(alt => alt.PartNumber.TrimEnd());
                string tmp = string.Empty;
                foreach (var Barcode in Barcodes)
                {
                    tmp += Barcode + ",";
                }
                tmp = tmp.Substring(0, tmp.Length - 1);
                return tmp;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        public string shortDesc 
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            } 
        }
        // longDesc not implemented
        public string longDesc 
        {
            get
            {
                throw new NotImplementedException("Feature to add WebDescription needs to be built.");
            }
            set
            {
                throw new NotImplementedException("Feature to add WebDescription needs to be built.");
            } 
        }
        public float weight 
        {
            get
            {
                return _InvAss.Inv.Weight;
            }
            set
            {
                _InvAss.Inv.Weight = value;
            } 
        }
        public float msrp 
        {
            get
            {
                throw new NotImplementedException("Need to impement S5InventoryLogicService.");
            }
            set
            {
                throw new NotSupportedException();
            } 
        }

        public bool addToList { get; set; }
        public bool addToPresentation { get; set; }
        public string aliasSku { get; set; }
        public string alsoConsider { get; set; }
        public bool altLoginRequire { get; set; }
        public int amazonCategoryId { get; set; }
        public int amazonCategoryId1 { get; set; }
        public int amazonSubCategoryId { get; set; }
        public int amazonSubCategoryId1 { get; set; }
        public bool backOrderFlag { get; set; }
        public double boxExtraAmt { get; set; }
        public int boxSize { get; set; }
        public int caseContent { get; set; }
        public string caseUnitTitle { get; set; }
        public double catalogPrice { get; set; }
        public string categoryIds { get; set; }
        public List<int> catIds { get; set; }
        public bool commissionable { get; set; }
        public bool compare { get; set; }
        public string configLabel { get; set; }
        public double cost { get; set; }
        public int cost1 { get; set; }
        public int cost10 { get; set; }
        public int cost2 { get; set; }
        public int cost3 { get; set; }
        public int cost4 { get; set; }
        public int cost5 { get; set; }
        public int cost6 { get; set; }
        public int cost7 { get; set; }
        public int cost8 { get; set; }
        public int cost9 { get; set; }
        public bool costPercent { get; set; }
        public string crossItemsCode { get; set; }
        public double cubicSize { get; set; }
        public int customLineCharacter { get; set; }
        public bool customShippingEnabled { get; set; }
        public string deal { get; set; }
        public string decoSku { get; set; }
        public string defaultSupplierAccountNumber { get; set; }
        public int defaultSupplierId { get; set; }
        public string defaultSupplierName { get; set; }
        public string defaultSupplierSku { get; set; }
        public int desiredInventory { get; set; }
        public int discount1 { get; set; }
        public int discount10 { get; set; }
        public int discount2 { get; set; }
        public int discount3 { get; set; }
        public int discount4 { get; set; }
        public int discount5 { get; set; }
        public int discount6 { get; set; }
        public int discount7 { get; set; }
        public int discount8 { get; set; }
        public int discount9 { get; set; }
        public int discountedCost { get; set; }
        public int discountedPrice { get; set; }
        public bool discountPercent1 { get; set; }
        public bool discountPercent10 { get; set; }
        public bool discountPercent2 { get; set; }
        public bool discountPercent3 { get; set; }
        public bool discountPercent4 { get; set; }
        public bool discountPercent5 { get; set; }
        public bool discountPercent6 { get; set; }
        public bool discountPercent7 { get; set; }
        public bool discountPercent8 { get; set; }
        public bool discountPercent9 { get; set; }
        public bool enableRate { get; set; }
        public bool enableSpecialTax { get; set; }
        public bool endQtyPricing { get; set; }
        public int eventProtection { get; set; }
        public string feed { get; set; }
        public bool feedFreeze { get; set; }
        public string field1 { get; set; }
        public string field10 { get; set; }
        public string field100 { get; set; }
        public string field11 { get; set; }
        public string field12 { get; set; }
        public string field13 { get; set; }
        public string field14 { get; set; }
        public string field15 { get; set; }
        public string field16 { get; set; }
        public string field17 { get; set; }
        public string field18 { get; set; }
        public string field19 { get; set; }
        public string field2 { get; set; }
        public string field20 { get; set; }
        public string field21 { get; set; }
        public string field22 { get; set; }
        public string field23 { get; set; }
        public string field24 { get; set; }
        public string field25 { get; set; }
        public string field26 { get; set; }
        public string field27 { get; set; }
        public string field28 { get; set; }
        public string field29 { get; set; }
        public string field3 { get; set; }
        public string field30 { get; set; }
        public string field31 { get; set; }
        public string field32 { get; set; }
        public string field33 { get; set; }
        public string field34 { get; set; }
        public string field35 { get; set; }
        public string field36 { get; set; }
        public string field37 { get; set; }
        public string field38 { get; set; }
        public string field39 { get; set; }
        public string field4 { get; set; }
        public string field40 { get; set; }
        public string field41 { get; set; }
        public string field42 { get; set; }
        public string field43 { get; set; }
        public string field44 { get; set; }
        public string field45 { get; set; }
        public string field46 { get; set; }
        public string field47 { get; set; }
        public string field48 { get; set; }
        public string field49 { get; set; }
        public string field5 { get; set; }
        public string field50 { get; set; }
        public string field51 { get; set; }
        public string field52 { get; set; }
        public string field53 { get; set; }
        public string field54 { get; set; }
        public string field55 { get; set; }
        public string field56 { get; set; }
        public string field57 { get; set; }
        public string field58 { get; set; }
        public string field59 { get; set; }
        public string field6 { get; set; }
        public string field60 { get; set; }
        public string field61 { get; set; }
        public string field62 { get; set; }
        public string field63 { get; set; }
        public string field64 { get; set; }
        public string field65 { get; set; }
        public string field66 { get; set; }
        public string field67 { get; set; }
        public string field68 { get; set; }
        public string field69 { get; set; }
        public string field7 { get; set; }
        public string field70 { get; set; }
        public string field71 { get; set; }
        public string field72 { get; set; }
        public string field73 { get; set; }
        public string field74 { get; set; }
        public string field75 { get; set; }
        public string field76 { get; set; }
        public string field77 { get; set; }
        public string field78 { get; set; }
        public string field79 { get; set; }
        public string field8 { get; set; }
        public string field80 { get; set; }
        public string field81 { get; set; }
        public string field82 { get; set; }
        public string field83 { get; set; }
        public string field84 { get; set; }
        public string field85 { get; set; }
        public string field86 { get; set; }
        public string field87 { get; set; }
        public string field88 { get; set; }
        public string field89 { get; set; }
        public string field9 { get; set; }
        public string field90 { get; set; }
        public string field91 { get; set; }
        public string field92 { get; set; }
        public string field93 { get; set; }
        public string field94 { get; set; }
        public string field95 { get; set; }
        public string field96 { get; set; }
        public string field97 { get; set; }
        public string field98 { get; set; }
        public string field99 { get; set; }
        public bool groundShipping { get; set; }
        public int groupSpecialPrice { get; set; }
        public int hazardousTier { get; set; }
        public string headTag { get; set; }
        public bool hideBreadCrumbs { get; set; }
        public bool hideFooter { get; set; }
        public bool hideHeader { get; set; }
        public bool hideLeftBar { get; set; }
        public bool hideMsrp { get; set; }
        public bool hidePrice { get; set; }
        public bool hideProduct { get; set; }
        public bool hideRightBar { get; set; }
        public bool hideTopBar { get; set; }
        public string htmlAddToCart { get; set; }
        public int incrementalQty { get; set; }
        public bool inventoryItem { get; set; }
        public string keywords { get; set; }
        public int kitCost { get; set; }
        public List<KitPart> kitParts { get; set; }
        public int limitQty { get; set; }
        public int locationDesiredAmount { get; set; }
        public int locationMinAmount { get; set; }
        public bool loginRequire { get; set; }
        public int lowInventory { get; set; }
        public double loyaltyPoint { get; set; }
        public string manufactureName { get; set; }
        public double marginPercent1 { get; set; }
        public int marginPercent10 { get; set; }
        public int marginPercent2 { get; set; }
        public int marginPercent3 { get; set; }
        public int marginPercent4 { get; set; }
        public int marginPercent5 { get; set; }
        public int marginPercent6 { get; set; }
        public int marginPercent7 { get; set; }
        public int marginPercent8 { get; set; }
        public int marginPercent9 { get; set; }
        public double markupPercent1 { get; set; }
        public int markupPercent10 { get; set; }
        public int markupPercent2 { get; set; }
        public int markupPercent3 { get; set; }
        public int markupPercent4 { get; set; }
        public int markupPercent5 { get; set; }
        public int markupPercent6 { get; set; }
        public int markupPercent7 { get; set; }
        public int markupPercent8 { get; set; }
        public int markupPercent9 { get; set; }
        public string masterSku { get; set; }
        public int minimumQty { get; set; }
        public bool negInventory { get; set; }
        public string note { get; set; }
        public int numCustomLines { get; set; }
        public string optionCode { get; set; }
        public int optionIndex { get; set; }
        public int orderPendingQty { get; set; }
        public int packageH { get; set; }
        public int packageL { get; set; }
        public int packageW { get; set; }
        public string packing { get; set; }
        public List<Product_Price> price { get; set; }
        public double price1 { get; set; }
        public double price10 { get; set; }
        public int price2 { get; set; }
        public double price3 { get; set; }
        public double price4 { get; set; }
        public double price5 { get; set; }
        public double price6 { get; set; }
        public double price7 { get; set; }
        public double price8 { get; set; }
        public double price9 { get; set; }
        public bool priceByCustomer { get; set; }
        public bool priceCasePack { get; set; }
        public double priceCasePack1 { get; set; }
        public double priceCasePack10 { get; set; }
        public double priceCasePack2 { get; set; }
        public double priceCasePack3 { get; set; }
        public double priceCasePack4 { get; set; }
        public double priceCasePack5 { get; set; }
        public double priceCasePack6 { get; set; }
        public double priceCasePack7 { get; set; }
        public double priceCasePack8 { get; set; }
        public double priceCasePack9 { get; set; }
        public int priceCasePackQty { get; set; }
        public double priceTable1 { get; set; }
        public double priceTable10 { get; set; }
        public int priceTable2 { get; set; }
        public double priceTable3 { get; set; }
        public double priceTable4 { get; set; }
        public double priceTable5 { get; set; }
        public double priceTable6 { get; set; }
        public double priceTable7 { get; set; }
        public double priceTable8 { get; set; }
        public double priceTable9 { get; set; }
        public Product_PrimaryLocation primaryLocation { get; set; }
        public string productCustomerMarkupTitle { get; set; }
        public string productLayout { get; set; }
        public int productLevel { get; set; }
        public string productType { get; set; }
        public int purchasePendingQty { get; set; }
        public bool pushedToAccounting { get; set; }
        public string qbExpenseAccountType { get; set; }
        public string qbIncomeAccountType { get; set; }
        public int qtyBreak1 { get; set; }
        public int qtyBreak2 { get; set; }
        public int qtyBreak3 { get; set; }
        public int qtyBreak4 { get; set; }
        public int qtyBreak5 { get; set; }
        public int qtyBreak6 { get; set; }
        public int qtyBreak7 { get; set; }
        public int qtyBreak8 { get; set; }
        public int qtyBreak9 { get; set; }
        public int qtyOnPO { get; set; }
        public bool quote { get; set; }
        public int rank { get; set; }
        public string recommendedList { get; set; }
        public string recommendedList1 { get; set; }
        public string recommendedList2 { get; set; }
        public string recommendedListDisplay { get; set; }
        public string recommendedListTitle { get; set; }
        public string recommendedListTitle1 { get; set; }
        public string recommendedListTitle2 { get; set; }
        public List<Product_RegionProductMapping> regionProductMapping { get; set; }
        public bool retainOnCart { get; set; }
        public int salesTagId { get; set; }
        public bool searchable { get; set; }
        public int searchRank { get; set; }
        public int selectedFieldGroupIndex { get; set; }
        public bool showNegInventory { get; set; }
        public double siteMapPriority { get; set; }
        public int slaveCount { get; set; }
        public double slaveHighestPrice { get; set; }
        public double slaveLowestPrice { get; set; }
        public List<int> supplierIds { get; set; }
        public string tab1 { get; set; }
        public string tab10 { get; set; }
        public string tab10Content { get; set; }
        public string tab1Content { get; set; }
        public string tab2 { get; set; }
        public string tab2Content { get; set; }
        public string tab3 { get; set; }
        public string tab3Content { get; set; }
        public string tab4 { get; set; }
        public string tab4Content { get; set; }
        public string tab5 { get; set; }
        public string tab5Content { get; set; }
        public string tab6 { get; set; }
        public string tab6Content { get; set; }
        public string tab7 { get; set; }
        public string tab7Content { get; set; }
        public string tab8 { get; set; }
        public string tab8Content { get; set; }
        public string tab9 { get; set; }
        public string tab9Content { get; set; }
        public bool taxable { get; set; }
        public int temperature { get; set; }
        public string type { get; set; }
        public int unitPrice { get; set; }
        public int upsMaxItemsInPackage { get; set; }
        public int uspsMaxItemsInPackage { get; set; }
    }
}
