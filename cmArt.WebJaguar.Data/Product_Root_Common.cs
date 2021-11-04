using cmArt.WebJaguar.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class Product_Root_Common : Product_Root, IWJ_CommonFields_In_S5
    {
        public Product_Root_Common()
        {
            supplierIds = new List<int>();
            primaryLocation = new Product_PrimaryLocation();
            catIds = new List<int>();
            price = new List<Product_Price>();
            //regionProductMapping = new List<Product_RegionProductMapping>();
            kitParts = new List<KitPart>();
        }
        int IWJ_CommonFields_In_S5.sku 
        { 
            get
            {
                int result;
                int.TryParse(this.sku, out result);
                return result;
            }
            set
            {
                this.sku = value.ToString();
            }
        }
        public Product_Root_Common SetNotEditableDefaults()
        {
            note = string.Empty;
            masterSku = string.Empty;
            slaveCount = 0;
            slaveLowestPrice = 0;
            slaveHighestPrice = 0;
            feedFreeze = false;
            hideMsrp = false;
            priceByCustomer = false;
            price1 = 0;
            price2 = 0;
            price3 = 0;
            price4 = 0;
            price5 = 0;
            price6 = 0;
            price7 = 0;
            price8 = 0;
            price9 = 0;
            price10 = 0;
            discount1 = 0;
            discount2 = 0;
            discount3 = 0;
            discount4 = 0;
            discount5 = 0;
            discount6 = 0;
            discount7 = 0;
            discount8 = 0;
            discount9 = 0;
            discount10 = 0;
            discountPercent1 = false;
            discountPercent2 = false;
            discountPercent3 = false;
            discountPercent4 = false;
            discountPercent5 = false;
            discountPercent6 = false;
            discountPercent7 = false;
            discountPercent8 = false;
            discountPercent9 = false;
            discountPercent10 = false;
            marginPercent1 = 0;
            marginPercent2 = 0;
            marginPercent3 = 0;
            marginPercent4 = 0;
            marginPercent5 = 0;
            marginPercent6 = 0;
            marginPercent7 = 0;
            marginPercent8 = 0;
            marginPercent9 = 0;
            marginPercent10 = 0;
            markupPercent1 = 0;
            markupPercent2 = 0;
            markupPercent3 = 0;
            markupPercent4 = 0;
            markupPercent5 = 0;
            markupPercent6 = 0;
            markupPercent7 = 0;
            markupPercent8 = 0;
            markupPercent9 = 0;
            markupPercent10 = 0;
            qtyBreak1 = 0;
            qtyBreak2 = 0;
            qtyBreak3 = 0;
            qtyBreak4 = 0;
            qtyBreak5 = 0;
            qtyBreak6 = 0;
            qtyBreak7 = 0;
            qtyBreak8 = 0;
            qtyBreak9 = 0;
            priceCasePack = false;
            priceCasePackQty = 1;
            priceCasePack1 = 0;
            priceCasePack2 = 0;
            priceCasePack3 = 0;
            priceCasePack4 = 0;
            priceCasePack5 = 0;
            priceCasePack6 = 0;
            priceCasePack7 = 0;
            priceCasePack8 = 0;
            priceCasePack9 = 0;
            priceCasePack10 = 0;
            quote = false;
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
            keywords = "product";
            kitCost = 0;
            salesTagId = 1;
            caseContent = 1;
            packing = string.Empty;
            cubicSize = 0;
            minimumQty = 1;
            incrementalQty = 1;
            loginRequire = true;
            hidePrice = false;
            altLoginRequire = false;
            recommendedList = string.Empty;
            recommendedListTitle = string.Empty;
            recommendedList1 = string.Empty;
            recommendedListTitle1 = string.Empty;
            recommendedList2 = string.Empty;
            recommendedListTitle2 = string.Empty;
            recommendedListDisplay = string.Empty;
            alsoConsider = string.Empty;
            rank = 1;
            searchRank = 1;
            hideHeader = false;
            hideTopBar = false;
            hideLeftBar = false;
            hideRightBar = false;
            hideFooter = false;
            hideBreadCrumbs = false;
            headTag = string.Empty;
            htmlAddToCart = string.Empty;
            productLayout = string.Empty;
            addToList = false;
            addToPresentation = false;
            boxSize = 1;
            boxExtraAmt = 0;
            type = string.Empty;
            optionIndex = 1;
            temperature = 1;
            compare = false;
            taxable = true;
            enableSpecialTax = false;
            searchable = true;
            optionCode = string.Empty;
            negInventory = false;
            lowInventory = 1;
            desiredInventory = 1;
            showNegInventory = false;
            orderPendingQty = 1;
            inventoryItem = true;
            loyaltyPoint = 0;
            customShippingEnabled = true;
            numCustomLines = 1;
            crossItemsCode = string.Empty;
            active = true;
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
            enableRate = true;
            feed = string.Empty;
            deal = string.Empty;
            packageL = 0;
            packageW = 0;
            packageH = 0;
            upsMaxItemsInPackage = 1;
            uspsMaxItemsInPackage = 1;
            endQtyPricing = false;
            retainOnCart = true;
            siteMapPriority = 0.5;
            defaultSupplierId = 0;
            costPercent = false;
            defaultSupplierSku = string.Empty;
            defaultSupplierName = string.Empty;
            defaultSupplierAccountNumber = string.Empty;
            eventProtection = 0;
            productLevel = 1;
            groupSpecialPrice = 1;
            caseUnitTitle = string.Empty;
            productType = string.Empty;
            discountedCost = 0;
            discountedPrice = 0;
            limitQty = 1;
            selectedFieldGroupIndex = 1;
            qbIncomeAccountType = string.Empty;
            qbExpenseAccountType = string.Empty;
            decoSku = string.Empty;
            productCustomerMarkupTitle = string.Empty;
            configLabel = "product";
            locationMinAmount = 0;
            locationDesiredAmount = 0;
            commissionable = false;
            amazonCategoryId = 2;
            amazonSubCategoryId = 2;
            amazonCategoryId1 = 1;
            amazonSubCategoryId1 = 3;
            hideProduct = false;
            aliasSku = string.Empty;
            pushedToAccounting = false;
            hazardousTier = 2;
            qtyOnPO = 3;
            groundShipping = false;
            backOrderFlag = false;

            return this;
        }
    }
    //public class Product_Root_Adapter : Product_Root, IWJ_CommonFields_In_S5
    //{
    //    private Product_Root _data;
    //    public Product_Root_Adapter()
    //    {
    //        Init(_data);
    //    }

    //    public string upc
    //    {
    //        get
    //        {
    //            return _data.upc ?? string.Empty;
    //        }
    //        set
    //        {
    //            _data.upc = value ?? string.Empty;
    //        }
    //    }
    //    public string shortDesc
    //    {
    //        _
    //    }
    //    public int sku { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public string longDesc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public float weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    public void Init(Product_Root data)
    //    {
    //        _data = _data ?? new Product_Root();
    //        _data.supplierIds = _data.supplierIds ?? new List<int>();
    //        _data.catIds = _data.catIds ?? new List<int>();
    //        _data.price = _data.price ?? new List<Product_Price>();
    //        _data.regionProductMapping = _data.regionProductMapping ?? new List<Product_RegionProductMapping>();
    //        _data.kitParts = _data.kitParts ?? new List<KitPart>();
    //        _data.primaryLocation = _data.primaryLocation ?? new Product_PrimaryLocation();
    //    }

    //}
}
