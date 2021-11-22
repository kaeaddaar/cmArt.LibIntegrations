using cmArt.LibIntegrations;
using cmArt.LibIntegrations.ReportService;
using cmArt.System5.Inventory;
using cmArt.WebJaguar.App.ReportViews;
using cmArt.WebJaguar.Data;
using FileHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public class ReportsWJ
    {
        public static void SaveReport(IEnumerable<Changes_View> data, string TableName, String OutputDirectory, ILogger logger)
        {
            IEnumerable<Changes_View> _data = data ?? new List<Changes_View>();
            var engine = new FileHelperAsyncEngine<Changes_View>();
            engine.HeaderText = "InvUnique, Cat, PartNumber, FieldName, S5ValueToSendToExternal, ExternalValueBeforeUpdate";

            using (engine.BeginWriteFile(OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        public static void SaveReport(IEnumerable<S5_CommonFields_Pairs_Flat> data, string TableName, string OutputDirectory, ILogger logger)
        {
            IEnumerable<S5_CommonFields_Pairs_Flat> _data = data ?? new List<S5_CommonFields_Pairs_Flat>();
            var engine = new FileHelperAsyncEngine<S5_CommonFields_Pairs_Flat>();
            engine.HeaderText = "LeftInvUnique, LeftCat, LeftPartNumber, LeftPrices, LeftQuantities, LeftBarcodes, LeftDescription, " +
                ", LeftWebDescription, LeftWeight, LeftFF22" +
                "RightInvUnique, RightCat, RightPartNumber, RightPrices, RightQuantities, RightBarcodes, RightDescription, " +
                ", RightWebDescription, RightWeight, RightFF22";
            using (engine.BeginWriteFile(OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
        public static void SaveReport(IEnumerable<WJ_Data_Export> data, string TableName, String OutputDirectory, ILogger logger)
        {
            IEnumerable<WJ_Data_Export> _data = data ?? new List<WJ_Data_Export>();
            var engine = new FileHelperAsyncEngine<WJ_Data_Export>();
            engine.HeaderText = "id, inventory(qty), inventoryAFS, msrp, name, sku, categoryIds, cost, priceTable1, priceTable2" +
                ", priceTable3, priceTable4, priceTable5, priceTable6, priceTable7, priceTable8, priceTable9, priceTable10, note" +
                ", field1(Units Per Case), field2(Unit), field3(Weight), field4(Volume), field5(Count), field6(Flavor), field7(Size)" +
                ", field8(Promo), field9(S5PartNum), field10(ShortDesc-DNU), field11(UPC-DNU), field12(S5Cat), field13(S5UniqueID)";

            using (engine.BeginWriteFile(OutputDirectory + $"\\{TableName}.csv"))
            {
                foreach (var record in _data)
                {
                    engine.WriteNext(record);
                }
            }
        }
    }

}
