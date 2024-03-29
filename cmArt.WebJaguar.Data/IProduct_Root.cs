﻿using System.Collections.Generic;
using cmArt.WebJaguar.Data;


namespace cmArt.WebJaguar.Data
{
    public interface IProduct_Root : IWJ_CommonFields_In_S5
    {
        int id { get; set; }
        new string field1 { get; set; }
        new string field2 { get; set; }
        new string field5 { get; set; }
        new string field7 { get; set; }
        new string field8 { get; set; }
        new string field9 { get; set; }
        new string field12 { get; set; }
        new string field13 { get; set; }
        new decimal inventory { get; set; }
        new decimal inventoryAFS { get; set; }
        string categoryIds { get; set; }
        new List<int> catIds { get; set; }
        bool active { get; set; }
        new string longDesc { get; set; }
        new string name { get; set; }
        new double priceTable1 { get; set; }
        new double priceTable10 { get; set; }
        new double priceTable2 { get; set; }
        new double priceTable3 { get; set; }
        new double priceTable4 { get; set; }
        new double priceTable5 { get; set; }
        new double priceTable6 { get; set; }
        new double priceTable7 { get; set; }
        new double priceTable8 { get; set; }
        new double priceTable9 { get; set; }
        new string shortDesc { get; set; }
        new string sku { get; set; }
        new string upc { get; set; }
        new float weight { get; set; }
        bool addToList { get; set; }
        bool addToPresentation { get; set; }
        string aliasSku { get; set; }
        string alsoConsider { get; set; }
        bool altLoginRequire { get; set; }
        int amazonCategoryId { get; set; }
        int amazonCategoryId1 { get; set; }
        int amazonSubCategoryId { get; set; }
        int amazonSubCategoryId1 { get; set; }
        bool backOrderFlag { get; set; }
        double boxExtraAmt { get; set; }
        int boxSize { get; set; }
        int caseContent { get; set; }
        string caseUnitTitle { get; set; }
        double catalogPrice { get; set; }
        bool commissionable { get; set; }
        bool compare { get; set; }
        string configLabel { get; set; }
        new double cost { get; set; }
        double cost1 { get; set; }
        double cost10 { get; set; }
        double cost2 { get; set; }
        double cost3 { get; set; }
        double cost4 { get; set; }
        double cost5 { get; set; }
        double cost6 { get; set; }
        double cost7 { get; set; }
        double cost8 { get; set; }
        double cost9 { get; set; }
        bool costPercent { get; set; }
        string crossItemsCode { get; set; }
        double cubicSize { get; set; }
        int customLineCharacter { get; set; }
        bool customShippingEnabled { get; set; }
        string deal { get; set; }
        string decoSku { get; set; }
        string defaultSupplierAccountNumber { get; set; }
        int defaultSupplierId { get; set; }
        string defaultSupplierName { get; set; }
        string defaultSupplierSku { get; set; }
        int desiredInventory { get; set; }
        double discount1 { get; set; }
        double discount10 { get; set; }
        double discount2 { get; set; }
        double discount3 { get; set; }
        double discount4 { get; set; }
        double discount5 { get; set; }
        double discount6 { get; set; }
        double discount7 { get; set; }
        double discount8 { get; set; }
        double discount9 { get; set; }
        double discountedCost { get; set; }
        double discountedPrice { get; set; }
        bool discountPercent1 { get; set; }
        bool discountPercent10 { get; set; }
        bool discountPercent2 { get; set; }
        bool discountPercent3 { get; set; }
        bool discountPercent4 { get; set; }
        bool discountPercent5 { get; set; }
        bool discountPercent6 { get; set; }
        bool discountPercent7 { get; set; }
        bool discountPercent8 { get; set; }
        bool discountPercent9 { get; set; }
        bool enableRate { get; set; }
        bool enableSpecialTax { get; set; }
        bool endQtyPricing { get; set; }
        int eventProtection { get; set; }
        string feed { get; set; }
        bool feedFreeze { get; set; }
        string field10 { get; set; }
        string field100 { get; set; }
        string field11 { get; set; }
        string field14 { get; set; }
        string field15 { get; set; }
        string field16 { get; set; }
        string field17 { get; set; }
        string field18 { get; set; }
        string field19 { get; set; }
        string field20 { get; set; }
        string field21 { get; set; }
        string field22 { get; set; }
        string field23 { get; set; }
        string field24 { get; set; }
        string field25 { get; set; }
        string field26 { get; set; }
        string field27 { get; set; }
        string field28 { get; set; }
        string field29 { get; set; }
        string field3 { get; set; }
        string field30 { get; set; }
        string field31 { get; set; }
        string field32 { get; set; }
        string field33 { get; set; }
        string field34 { get; set; }
        string field35 { get; set; }
        string field36 { get; set; }
        string field37 { get; set; }
        string field38 { get; set; }
        string field39 { get; set; }
        string field4 { get; set; }
        string field40 { get; set; }
        string field41 { get; set; }
        string field42 { get; set; }
        string field43 { get; set; }
        string field44 { get; set; }
        string field45 { get; set; }
        string field46 { get; set; }
        string field47 { get; set; }
        string field48 { get; set; }
        string field49 { get; set; }
        string field50 { get; set; }
        string field51 { get; set; }
        string field52 { get; set; }
        string field53 { get; set; }
        string field54 { get; set; }
        string field55 { get; set; }
        string field56 { get; set; }
        string field57 { get; set; }
        string field58 { get; set; }
        string field59 { get; set; }
        string field6 { get; set; }
        string field60 { get; set; }
        string field61 { get; set; }
        string field62 { get; set; }
        string field63 { get; set; }
        string field64 { get; set; }
        string field65 { get; set; }
        string field66 { get; set; }
        string field67 { get; set; }
        string field68 { get; set; }
        string field69 { get; set; }
        string field70 { get; set; }
        string field71 { get; set; }
        string field72 { get; set; }
        string field73 { get; set; }
        string field74 { get; set; }
        string field75 { get; set; }
        string field76 { get; set; }
        string field77 { get; set; }
        string field78 { get; set; }
        string field79 { get; set; }
        string field80 { get; set; }
        string field81 { get; set; }
        string field82 { get; set; }
        string field83 { get; set; }
        string field84 { get; set; }
        string field85 { get; set; }
        string field86 { get; set; }
        string field87 { get; set; }
        string field88 { get; set; }
        string field89 { get; set; }
        string field90 { get; set; }
        string field91 { get; set; }
        string field92 { get; set; }
        string field93 { get; set; }
        string field94 { get; set; }
        string field95 { get; set; }
        string field96 { get; set; }
        string field97 { get; set; }
        string field98 { get; set; }
        string field99 { get; set; }
        bool groundShipping { get; set; }
        double groupSpecialPrice { get; set; }
        int hazardousTier { get; set; }
        string headTag { get; set; }
        bool hideBreadCrumbs { get; set; }
        bool hideFooter { get; set; }
        bool hideHeader { get; set; }
        bool hideLeftBar { get; set; }
        bool hideMsrp { get; set; }
        bool hidePrice { get; set; }
        bool hideProduct { get; set; }
        bool hideRightBar { get; set; }
        bool hideTopBar { get; set; }
        string htmlAddToCart { get; set; }
        int incrementalQty { get; set; }
        bool inventoryItem { get; set; }
        string keywords { get; set; }
        double kitCost { get; set; }
        List<KitPart> kitParts { get; set; }
        int limitQty { get; set; }
        int locationDesiredAmount { get; set; }
        int locationMinAmount { get; set; }
        bool loginRequire { get; set; }
        int lowInventory { get; set; }
        double loyaltyPoint { get; set; }
        string manufactureName { get; set; }
        double marginPercent1 { get; set; }
        int marginPercent10 { get; set; }
        int marginPercent2 { get; set; }
        int marginPercent3 { get; set; }
        int marginPercent4 { get; set; }
        int marginPercent5 { get; set; }
        int marginPercent6 { get; set; }
        int marginPercent7 { get; set; }
        int marginPercent8 { get; set; }
        int marginPercent9 { get; set; }
        double markupPercent1 { get; set; }
        int markupPercent10 { get; set; }
        int markupPercent2 { get; set; }
        int markupPercent3 { get; set; }
        int markupPercent4 { get; set; }
        int markupPercent5 { get; set; }
        int markupPercent6 { get; set; }
        int markupPercent7 { get; set; }
        int markupPercent8 { get; set; }
        int markupPercent9 { get; set; }
        string masterSku { get; set; }
        int minimumQty { get; set; }
        double msrp { get; set; }
        bool negInventory { get; set; }
        string note { get; set; }
        int numCustomLines { get; set; }
        string optionCode { get; set; }
        int optionIndex { get; set; }
        int orderPendingQty { get; set; }
        int packageH { get; set; }
        int packageL { get; set; }
        int packageW { get; set; }
        string packing { get; set; }
        List<Product_Price> price { get; set; }
        double price1 { get; set; }
        double price10 { get; set; }
        double price2 { get; set; }
        double price3 { get; set; }
        double price4 { get; set; }
        double price5 { get; set; }
        double price6 { get; set; }
        double price7 { get; set; }
        double price8 { get; set; }
        double price9 { get; set; }
        bool priceByCustomer { get; set; }
        bool priceCasePack { get; set; }
        double priceCasePack1 { get; set; }
        double priceCasePack10 { get; set; }
        double priceCasePack2 { get; set; }
        double priceCasePack3 { get; set; }
        double priceCasePack4 { get; set; }
        double priceCasePack5 { get; set; }
        double priceCasePack6 { get; set; }
        double priceCasePack7 { get; set; }
        double priceCasePack8 { get; set; }
        double priceCasePack9 { get; set; }
        int priceCasePackQty { get; set; }
        Product_PrimaryLocation primaryLocation { get; set; }
        string productCustomerMarkupTitle { get; set; }
        string productLayout { get; set; }
        int productLevel { get; set; }
        string productType { get; set; }
        int purchasePendingQty { get; set; }
        bool pushedToAccounting { get; set; }
        string qbExpenseAccountType { get; set; }
        string qbIncomeAccountType { get; set; }
        int qtyBreak1 { get; set; }
        int qtyBreak2 { get; set; }
        int qtyBreak3 { get; set; }
        int qtyBreak4 { get; set; }
        int qtyBreak5 { get; set; }
        int qtyBreak6 { get; set; }
        int qtyBreak7 { get; set; }
        int qtyBreak8 { get; set; }
        int qtyBreak9 { get; set; }
        int qtyOnPO { get; set; }
        bool quote { get; set; }
        int rank { get; set; }
        string recommendedList { get; set; }
        string recommendedList1 { get; set; }
        string recommendedList2 { get; set; }
        string recommendedListDisplay { get; set; }
        string recommendedListTitle { get; set; }
        string recommendedListTitle1 { get; set; }
        string recommendedListTitle2 { get; set; }
        //List<Product_RegionProductMapping> regionProductMapping { get; set; }
        bool retainOnCart { get; set; }
        int salesTagId { get; set; }
        bool searchable { get; set; }
        int searchRank { get; set; }
        int selectedFieldGroupIndex { get; set; }
        bool showNegInventory { get; set; }
        double siteMapPriority { get; set; }
        int slaveCount { get; set; }
        double slaveHighestPrice { get; set; }
        double slaveLowestPrice { get; set; }
        List<int> supplierIds { get; set; }
        string tab1 { get; set; }
        string tab10 { get; set; }
        string tab10Content { get; set; }
        string tab1Content { get; set; }
        string tab2 { get; set; }
        string tab2Content { get; set; }
        string tab3 { get; set; }
        string tab3Content { get; set; }
        string tab4 { get; set; }
        string tab4Content { get; set; }
        string tab5 { get; set; }
        string tab5Content { get; set; }
        string tab6 { get; set; }
        string tab6Content { get; set; }
        string tab7 { get; set; }
        string tab7Content { get; set; }
        string tab8 { get; set; }
        string tab8Content { get; set; }
        string tab9 { get; set; }
        string tab9Content { get; set; }
        bool taxable { get; set; }
        int temperature { get; set; }
        string type { get; set; }
        double unitPrice { get; set; }
        int upsMaxItemsInPackage { get; set; }
        int uspsMaxItemsInPackage { get; set; }
    }
}