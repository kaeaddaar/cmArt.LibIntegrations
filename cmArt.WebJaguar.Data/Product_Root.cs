﻿using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    public class Product_Root : IProduct_Root
    {
        public Product_Root()
        {
            this.supplierIds = new List<int>();
            primaryLocation = new Product_PrimaryLocation();
            catIds = new List<int>();
            price = new List<Product_Price>();
            kitParts = new List<KitPart>();
            name = string.Empty;
            _sku = "0";
            categoryIds = string.Empty;
            note = string.Empty;
            masterSku = string.Empty;
            upc = string.Empty;
            shortDesc = string.Empty;
            longDesc = string.Empty;
            field1 = string.Empty;
            field2 = string.Empty;
            field3 = string.Empty;
            field4 = string.Empty;
            field5 = string.Empty;
            field6 = string.Empty;
            field7 = string.Empty;
            field8 = string.Empty;
            field9 = string.Empty;
            field10 = string.Empty;
            field11 = string.Empty;
            field12 = string.Empty;
            field13 = string.Empty;
            field14 = string.Empty;
            field15 = string.Empty;
            field16 = string.Empty;
            field17 = string.Empty;
            field18 = string.Empty;
            field19 = string.Empty;
            field20 = string.Empty;
            field21 = string.Empty;
            field22 = string.Empty;
            field23 = string.Empty;
            field24 = string.Empty;
            field25 = string.Empty;
            field26 = string.Empty;
            field27 = string.Empty;
            field28 = string.Empty;
            field29 = string.Empty;
            field30 = string.Empty;
            field31 = string.Empty;
            field32 = string.Empty;
            field33 = string.Empty;
            field34 = string.Empty;
            field35 = string.Empty;
            field36 = string.Empty;
            field37 = string.Empty;
            field38 = string.Empty;
            field39 = string.Empty;
            field40 = string.Empty;
            field41 = string.Empty;
            field42 = string.Empty;
            field43 = string.Empty;
            field44 = string.Empty;
            field45 = string.Empty;
            field46 = string.Empty;
            field47 = string.Empty;
            field48 = string.Empty;
            field49 = string.Empty;
            field50 = string.Empty;
            field51 = string.Empty;
            field52 = string.Empty;
            field53 = string.Empty;
            field54 = string.Empty;
            field55 = string.Empty;
            field56 = string.Empty;
            field57 = string.Empty;
            field58 = string.Empty;
            field59 = string.Empty;
            field60 = string.Empty;
            field61 = string.Empty;
            field62 = string.Empty;
            field63 = string.Empty;
            field64 = string.Empty;
            field65 = string.Empty;
            field66 = string.Empty;
            field67 = string.Empty;
            field68 = string.Empty;
            field69 = string.Empty;
            field70 = string.Empty;
            field71 = string.Empty;
            field72 = string.Empty;
            field73 = string.Empty;
            field74 = string.Empty;
            field75 = string.Empty;
            field76 = string.Empty;
            field77 = string.Empty;
            field78 = string.Empty;
            field79 = string.Empty;
            field80 = string.Empty;
            field81 = string.Empty;
            field82 = string.Empty;
            field83 = string.Empty;
            field84 = string.Empty;
            field85 = string.Empty;
            field86 = string.Empty;
            field87 = string.Empty;
            field88 = string.Empty;
            field89 = string.Empty;
            field90 = string.Empty;
            field91 = string.Empty;
            field92 = string.Empty;
            field93 = string.Empty;
            field94 = string.Empty;
            field95 = string.Empty;
            field96 = string.Empty;
            field97 = string.Empty;
            field98 = string.Empty;
            field99 = string.Empty;
            field100 = string.Empty;
            keywords = string.Empty;
            recommendedList = string.Empty;
            recommendedListTitle = string.Empty;
            recommendedList1 = string.Empty;
            recommendedListTitle1 = string.Empty;
            recommendedList2 = string.Empty;
            recommendedListTitle2 = string.Empty;
            recommendedListDisplay = string.Empty;
            alsoConsider = string.Empty;
            headTag = string.Empty;
            htmlAddToCart = string.Empty;
            productLayout = string.Empty;
            type = string.Empty;
            optionCode = string.Empty;
            crossItemsCode = string.Empty;
            tab1 = string.Empty;
            tab1Content = string.Empty;
            tab2 = string.Empty;
            tab2Content = string.Empty;
            tab3 = string.Empty;
            tab3Content = string.Empty;
            tab4 = string.Empty;
            tab4Content = string.Empty;
            tab5 = string.Empty;
            tab5Content = string.Empty;
            tab6 = string.Empty;
            tab6Content = string.Empty;
            tab7 = string.Empty;
            tab7Content = string.Empty;
            tab8 = string.Empty;
            tab8Content = string.Empty;
            tab9 = string.Empty;
            tab9Content = string.Empty;
            tab10 = string.Empty;
            tab10Content = string.Empty;
            manufactureName = string.Empty;
            feed = string.Empty;
            deal = string.Empty;
            defaultSupplierSku = string.Empty;
            defaultSupplierName = string.Empty;
            defaultSupplierAccountNumber = string.Empty;
            caseUnitTitle = string.Empty;
            productType = string.Empty;
            qbIncomeAccountType = string.Empty;
            qbExpenseAccountType = string.Empty;
            decoSku = string.Empty;
            productCustomerMarkupTitle = string.Empty;
            configLabel = string.Empty;
            aliasSku = string.Empty;

    }
    public int id { get; set; }
        public decimal inventory { get; set; }
        public decimal inventoryAFS { get; set; }
        public List<int> supplierIds { get; set; }
        public Product_PrimaryLocation primaryLocation { get; set; }
        public List<int> catIds { get; set; }
        public List<Product_Price> price { get; set; }
        //public List<Product_RegionProductMapping> regionProductMapping { get; set; }
        public List<KitPart> kitParts { get; set; }
        public double msrp { get; set; }
        public string name { get; set; }
        private string _sku;
        public string sku
        {
            get
            {
                int result;
                int.TryParse(_sku, out result);
                return result.ToString();
            }
            set
            {
                int result;
                int.TryParse(value, out result);
                _sku = result.ToString();
            }
        }
        public string categoryIds { get; set; }
        public double cost { get; set; }
        public double priceTable1 { get; set; }
        public double priceTable2 { get; set; }
        public double priceTable3 { get; set; }
        public double priceTable4 { get; set; }
        public double priceTable5 { get; set; }
        public double priceTable6 { get; set; }
        public double priceTable7 { get; set; }
        public double priceTable8 { get; set; }
        public double priceTable9 { get; set; }
        public double priceTable10 { get; set; }
        public string note { get; set; }
        public string masterSku { get; set; }
        public int slaveCount { get; set; }
        public double slaveLowestPrice { get; set; }
        public double slaveHighestPrice { get; set; }
        public string upc { get; set; }
        public bool feedFreeze { get; set; }
        public string shortDesc { get; set; }
        public string longDesc { get; set; }
        public float weight { get; set; }
        public bool hideMsrp { get; set; }
        public double unitPrice { get; set; }
        public bool priceByCustomer { get; set; }
        public double catalogPrice { get; set; }
        public double price1 { get; set; }
        public double price2 { get; set; }
        public double price3 { get; set; }
        public double price4 { get; set; }
        public double price5 { get; set; }
        public double price6 { get; set; }
        public double price7 { get; set; }
        public double price8 { get; set; }
        public double price9 { get; set; }
        public double price10 { get; set; }
        public double cost1 { get; set; }
        public double cost2 { get; set; }
        public double cost3 { get; set; }
        public double cost4 { get; set; }
        public double cost5 { get; set; }
        public double cost6 { get; set; }
        public double cost7 { get; set; }
        public double cost8 { get; set; }
        public double cost9 { get; set; }
        public double cost10 { get; set; }
        public double discount1 { get; set; }
        public double discount2 { get; set; }
        public double discount3 { get; set; }
        public double discount4 { get; set; }
        public double discount5 { get; set; }
        public double discount6 { get; set; }
        public double discount7 { get; set; }
        public double discount8 { get; set; }
        public double discount9 { get; set; }
        public double discount10 { get; set; }
        public bool discountPercent1 { get; set; }
        public bool discountPercent2 { get; set; }
        public bool discountPercent3 { get; set; }
        public bool discountPercent4 { get; set; }
        public bool discountPercent5 { get; set; }
        public bool discountPercent6 { get; set; }
        public bool discountPercent7 { get; set; }
        public bool discountPercent8 { get; set; }
        public bool discountPercent9 { get; set; }
        public bool discountPercent10 { get; set; }
        public double marginPercent1 { get; set; }
        public int marginPercent2 { get; set; }
        public int marginPercent3 { get; set; }
        public int marginPercent4 { get; set; }
        public int marginPercent5 { get; set; }
        public int marginPercent6 { get; set; }
        public int marginPercent7 { get; set; }
        public int marginPercent8 { get; set; }
        public int marginPercent9 { get; set; }
        public int marginPercent10 { get; set; }
        public double markupPercent1 { get; set; }
        public int markupPercent2 { get; set; }
        public int markupPercent3 { get; set; }
        public int markupPercent4 { get; set; }
        public int markupPercent5 { get; set; }
        public int markupPercent6 { get; set; }
        public int markupPercent7 { get; set; }
        public int markupPercent8 { get; set; }
        public int markupPercent9 { get; set; }
        public int markupPercent10 { get; set; }
        public int qtyBreak1 { get; set; }
        public int qtyBreak2 { get; set; }
        public int qtyBreak3 { get; set; }
        public int qtyBreak4 { get; set; }
        public int qtyBreak5 { get; set; }
        public int qtyBreak6 { get; set; }
        public int qtyBreak7 { get; set; }
        public int qtyBreak8 { get; set; }
        public int qtyBreak9 { get; set; }
        public bool priceCasePack { get; set; }
        public int priceCasePackQty { get; set; }
        public double priceCasePack1 { get; set; }
        public double priceCasePack2 { get; set; }
        public double priceCasePack3 { get; set; }
        public double priceCasePack4 { get; set; }
        public double priceCasePack5 { get; set; }
        public double priceCasePack6 { get; set; }
        public double priceCasePack7 { get; set; }
        public double priceCasePack8 { get; set; }
        public double priceCasePack9 { get; set; }
        public double priceCasePack10 { get; set; }
        public bool quote { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public string field10 { get; set; }
        public string field11 { get; set; }
        public string field12 { get; set; }
        public string field13 { get; set; }
        public string field14 { get; set; }
        public string field15 { get; set; }
        public string field16 { get; set; }
        public string field17 { get; set; }
        public string field18 { get; set; }
        public string field19 { get; set; }
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
        public string field100 { get; set; }
        public string keywords { get; set; }
        public double kitCost { get; set; }
        public int salesTagId { get; set; }
        public int caseContent { get; set; }
        public string packing { get; set; }
        public double cubicSize { get; set; }
        public int minimumQty { get; set; }
        public int incrementalQty { get; set; }
        public bool loginRequire { get; set; }
        public bool hidePrice { get; set; }
        public bool altLoginRequire { get; set; }
        public string recommendedList { get; set; }
        public string recommendedListTitle { get; set; }
        public string recommendedList1 { get; set; }
        public string recommendedListTitle1 { get; set; }
        public string recommendedList2 { get; set; }
        public string recommendedListTitle2 { get; set; }
        public string recommendedListDisplay { get; set; }
        public string alsoConsider { get; set; }
        public int rank { get; set; }
        public int searchRank { get; set; }
        public bool hideHeader { get; set; }
        public bool hideTopBar { get; set; }
        public bool hideLeftBar { get; set; }
        public bool hideRightBar { get; set; }
        public bool hideFooter { get; set; }
        public bool hideBreadCrumbs { get; set; }
        public string headTag { get; set; }
        public string htmlAddToCart { get; set; }
        public string productLayout { get; set; }
        public bool addToList { get; set; }
        public bool addToPresentation { get; set; }
        public int boxSize { get; set; }
        public double boxExtraAmt { get; set; }
        public string type { get; set; }
        public int optionIndex { get; set; }
        public int temperature { get; set; }
        public bool compare { get; set; }
        public bool taxable { get; set; }
        public bool enableSpecialTax { get; set; }
        public bool searchable { get; set; }
        public string optionCode { get; set; }
        public bool negInventory { get; set; }
        public int lowInventory { get; set; }
        public int desiredInventory { get; set; }
        public bool showNegInventory { get; set; }
        public int orderPendingQty { get; set; }
        public int purchasePendingQty { get; set; }
        public bool inventoryItem { get; set; }
        public double loyaltyPoint { get; set; }
        public bool customShippingEnabled { get; set; }
        public int numCustomLines { get; set; }
        public int customLineCharacter { get; set; }
        public string crossItemsCode { get; set; }
        public bool active { get; set; }
        public string tab1 { get; set; }
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
        public string tab10 { get; set; }
        public string tab10Content { get; set; }
        public string manufactureName { get; set; }
        public bool enableRate { get; set; }
        public string feed { get; set; }
        public string deal { get; set; }
        public int packageL { get; set; }
        public int packageW { get; set; }
        public int packageH { get; set; }
        public int upsMaxItemsInPackage { get; set; }
        public int uspsMaxItemsInPackage { get; set; }
        public bool endQtyPricing { get; set; }
        public bool retainOnCart { get; set; }
        public double siteMapPriority { get; set; }
        public int defaultSupplierId { get; set; }
        public bool costPercent { get; set; }
        public string defaultSupplierSku { get; set; }
        public string defaultSupplierName { get; set; }
        public string defaultSupplierAccountNumber { get; set; }
        public int eventProtection { get; set; }
        public int productLevel { get; set; }
        public double groupSpecialPrice { get; set; }
        public string caseUnitTitle { get; set; }
        public string productType { get; set; }
        public double discountedCost { get; set; }
        public double discountedPrice { get; set; }
        public int limitQty { get; set; }
        public int selectedFieldGroupIndex { get; set; }
        public string qbIncomeAccountType { get; set; }
        public string qbExpenseAccountType { get; set; }
        public string decoSku { get; set; }
        public string productCustomerMarkupTitle { get; set; }
        public string configLabel { get; set; }
        public int locationMinAmount { get; set; }
        public int locationDesiredAmount { get; set; }
        public bool commissionable { get; set; }
        public int amazonCategoryId { get; set; }
        public int amazonSubCategoryId { get; set; }
        public int amazonCategoryId1 { get; set; }
        public int amazonSubCategoryId1 { get; set; }
        public bool hideProduct { get; set; }
        public string aliasSku { get; set; }
        public bool pushedToAccounting { get; set; }
        public int hazardousTier { get; set; }
        public int qtyOnPO { get; set; }
        public bool groundShipping { get; set; }
        public bool backOrderFlag { get; set; }
    }

}
