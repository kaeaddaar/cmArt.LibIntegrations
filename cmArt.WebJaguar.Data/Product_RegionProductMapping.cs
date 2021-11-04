using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.WebJaguar.Data
{
    public class Product_RegionProductMapping
    {
        public string regionName { get; set; }
        public double price1 { get; set; }
        public int cost1 { get; set; }
        public bool taxable { get; set; }
        public bool active { get; set; }
        public bool hidePrice { get; set; }
        public bool hideMsrp { get; set; }
        public double msrp { get; set; }
        public int discount1 { get; set; }
        public int discountPercent1 { get; set; }
        public int marginPercent1 { get; set; }
        public int markupPercent1 { get; set; }
        public int minimumQty { get; set; }
        public int incrementalQty { get; set; }
        public int qtyBreak1 { get; set; }
        public int priceTable1 { get; set; }
        public bool priceCasePack { get; set; }
        public int priceCasePackQty { get; set; }
        public int priceCasePack1 { get; set; }
        public bool blankDec { get; set; }
        public string blankSku { get; set; }
        public string screenPrintImprintIdName { get; set; }
        public string fullColorImprintIdName { get; set; }
        public string padPrintImprintIdName { get; set; }
        public string embroideryImprintIdName { get; set; }
        public string laserEngravingImprintIdName { get; set; }
        public string debossImprintIdName { get; set; }
        public string embossImprintIdName { get; set; }
        public string heatTransferImprintIdName { get; set; }
        public string etchingImprintIdName { get; set; }
        public string hotStampImprintIdName { get; set; }
        public string sublimationImprintIdName { get; set; }
        public string offsetImprintIdName { get; set; }
        public string flexoImprintIdName { get; set; }
    }

}
