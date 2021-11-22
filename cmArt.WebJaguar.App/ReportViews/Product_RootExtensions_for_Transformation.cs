using cmArt.WebJaguar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App.ReportViews
{
    public static class Product_RootExtensions_for_Transformation
    {
        public static WJ_Data_Export AsWJ_Data_Export(this Product_Root data)
        {
            WJ_Data_Export result = new WJ_Data_Export();
            result.sku = data.sku;
            result.categoryIds = data.categoryIds;
            result.cost = data.cost;
            result.id = data.id;
            result.inventory = data.inventory;
            result.inventoryAFS = data.inventoryAFS;
            result.msrp = data.msrp;
            result.name = data.name;
            result.note = data.note;
            result.field1 = data.field1;
            result.field2 = data.field2;
            result.field3 = data.field3;
            result.field4 = data.field4;
            result.field5 = data.field5;
            result.field6 = data.field6;
            result.field7 = data.field7;
            result.field8 = data.field8;
            result.field9 = data.field9;
            result.field10 = data.field10;
            result.field11 = data.field11;
            result.field12 = data.field12;
            result.field13 = data.field13;
            result.priceTable1 = data.priceTable1;
            result.priceTable10 = data.priceTable10;
            result.priceTable2 = data.priceTable2;
            result.priceTable3 = data.priceTable3;
            result.priceTable4 = data.priceTable4;
            result.priceTable5 = data.priceTable5;
            result.priceTable6 = data.priceTable6;
            result.priceTable7 = data.priceTable7;
            result.priceTable8 = data.priceTable8;
            result.priceTable9 = data.priceTable9;

            return result;
        }
    }
}
