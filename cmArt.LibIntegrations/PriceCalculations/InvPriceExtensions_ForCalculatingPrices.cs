using cmArt.LibIntegrations.OdbcService;
using cmArt.LibIntegrations.OdbcService.ReaderExtensions;
using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace cmArt.LibIntegrations.PriceCalculations
{
    public static class InvPriceExtensions_ForCalculatingPrices
    {
        public static IEnumerable<IEnumerable<PriceScheduleView>> ToPriceView
        (
            this IEnumerable<InvPrice> TodaysPrices
            , short Schedule_0
            , short Schedule_List
            , short Schedule_Cash
        )
        {
            var ManyPrices = TodaysPrices.GroupBy(price => InvPrice_Indexes.InventoryUnique(price))
                .Select(group => group);

            var ManyPriceInfoPairs = ManyPrices.Select
            (
                p => new ValueTuple<IEnumerable<InvPrice>, IInventoryBasePriceInfo>
                (
                    p
                    , p.CalculateBasePriceSchedules
                    (
                        new Inventry_27() { InvUnique = p.First().PartUnique }
                        , Schedule_0
                        , Schedule_List
                        , Schedule_Cash
                    )
                )
            );
            foreach (var PriceInfoPair in ManyPriceInfoPairs)
            {
                PriceInfoPair.Item2.CalculateBasePrices();
            }
            #region duplicate of linq code using foreach
            if (false)
            {
                List<ValueTuple<IEnumerable<InvPrice>, IInventoryBasePriceInfo>> b;
                b = new List<ValueTuple<IEnumerable<InvPrice>, IInventoryBasePriceInfo>>();

                foreach (var prices in ManyPrices)
                {
                    b.Add
                    (
                        new ValueTuple<IEnumerable<InvPrice>, IInventoryBasePriceInfo>
                        (
                            prices
                            , prices.CalculateBasePriceSchedules(new Inventry_27() { InvUnique = prices.First().PartUnique }, 0, 0, 0)
                        )
                    );
                }
            }
            #endregion duplicate of linq code using foreach

            var PriceViews = ManyPriceInfoPairs.Select(pair => pair.Item2.CalculatePrices(pair.Item1));

            return PriceViews;
        }

        public static IEnumerable<PriceScheduleView> ToPriceView
        (
            this IEnumerable<InvPrice> TodaysPrices_ForPart
            , Inventry_27 InventoryPricesAreFor
            , short Schedule_0
            , short Schedule_List
            , short Schedule_Cash
        )
        {
            var mp = new ValueTuple<int, IEnumerable<InvPrice>>
            (
                InvPrice_Indexes.InventoryUnique(TodaysPrices_ForPart.First())
                , TodaysPrices_ForPart
            );

            var mpp = new ValueTuple<IEnumerable<InvPrice>, IInventoryBasePriceInfo>
            (
                TodaysPrices_ForPart
                , TodaysPrices_ForPart.CalculateBasePriceSchedules
                (
                    InventoryPricesAreFor
                    , Schedule_0
                    , Schedule_List
                    , Schedule_Cash 
                )
            );

            mpp.Item2.CalculateBasePrices();
            var pv = mpp.Item2.CalculatePrices(mpp.Item1);

            return pv;
        }

        public static PriceScheduleView GetView_RegularPrice
        (
            this IInvPrice TodaysInvPriceRecord
            , short PriceScheduleToUse
            , IInventoryBasePriceInfo BasePriceInfo
        )
        {
            PriceScheduleView view;
            view = BasePriceInfo.CalculatePrice(TodaysInvPriceRecord);

            return view;
        }

        public static InventoryBasePriceInfo CalculateBasePriceSchedules // single part
        (
            this IEnumerable<InvPrice> InvPrices_TodaysPrices
            , Inventry_27 InventoryItem
            , short Schedule_0
            , short Schedule_List
            , short Schedule_Cash
        )
        {
            List<Inventry_27> invItems = new List<Inventry_27>();
            invItems.Add(InventoryItem);

            IEnumerable<InventoryBasePriceInfo> BasePricesForSingleInventoryItem;
            BasePricesForSingleInventoryItem = CalculateBasePriceSchedules
            (
                InvPrices_TodaysPrices: InvPrices_TodaysPrices
                , Inventry_27s: invItems
                , Schedule_0: Schedule_0
                , Schedule_List: Schedule_List
                , Schedule_Cash: Schedule_Cash
            );

            return BasePricesForSingleInventoryItem.First(); // first or default?
        }

        public static IEnumerable<InventoryBasePriceInfo> CalculateBasePriceSchedules // many parts
        (
            this IEnumerable<InvPrice> InvPrices_TodaysPrices
            , IEnumerable<Inventry_27> Inventry_27s
            , short Schedule_0
            , short Schedule_List
            , short Schedule_Cash
        )
        {
            List<Int16> BasePriceSchedules = new List<short>();
            BasePriceSchedules.Add(Schedule_0);
            BasePriceSchedules.Add(Schedule_List);
            BasePriceSchedules.Add(Schedule_Cash);

            BasePriceSchedules = BasePriceSchedules.GroupBy(sched => sched, sched => sched)
                .Select(sched => sched.Key).ToList();

            List<InventoryBasePriceInfo> BasePrices = new List<InventoryBasePriceInfo>();

            IEnumerable<IEnumerable<InvPrice>> TodaysPrices_By_InventoryUnique;
            TodaysPrices_By_InventoryUnique = 
                GenericAggregateToLists<InvPrice, int>.ToLists(InvPrices_TodaysPrices, InvPrice_Indexes.InventoryUnique);
            
            foreach (var TodaysPrices in TodaysPrices_By_InventoryUnique)
            {
                BasePrices.Add
                (
                    InvPrices_TodaysPrices.GetBasePriceInfo
                    (
                        Inventry_27s: Inventry_27s
                        , Schedule_0: Schedule_0
                        , Schedule_Cash: Schedule_Cash
                        , Schedule_List: Schedule_List
                    )
                );
            }

            return BasePrices;
        }

        public static List<InvPrice> GetTodaysPrices
        (
            this IEnumerable<InvPrice> unfilteredPrices
            , List<Int16> priceSchedulesToInclude
            , DateTime AsOfDate
            , Int16 DepartmentToInclude = 0
        )
        {
            IEnumerable<InvPrice> _prices;
            _prices = unfilteredPrices;

            _prices = unfilteredPrices.Where
            (
                p => priceSchedulesToInclude.Contains(p.ScheduleLevel)
                && p.Department == DepartmentToInclude
                && p.StartDate < AsOfDate
            );

            var RecentPriceKey =
                from InvPrice in _prices
                group InvPrice by new { InvPrice.PartUnique, InvPrice.ScheduleLevel, InvPrice.Department }
                into newPrices
                let maxDate = newPrices.Max(p => p.StartDate)
                select new
                {
                    PartUnique = newPrices.Key.PartUnique
                    , ScheduleLevel = newPrices.Key.ScheduleLevel
                    , Department = newPrices.Key.Department
                    , MaxDate = maxDate
                };

            // Assemble the key back into a full table of information
            var RecentPrices =
                from InvPriceKey in RecentPriceKey
                join InvPrice1 in _prices on
                new { PartUnique = InvPriceKey.PartUnique, StartDate = InvPriceKey.MaxDate
                    , ScheduleLevel = InvPriceKey.ScheduleLevel, Department = InvPriceKey.Department }  
                equals
                new { PartUnique = InvPrice1.PartUnique, StartDate = InvPrice1.StartDate
                    , ScheduleLevel = InvPrice1.ScheduleLevel, Department = InvPrice1.Department }
                select new InvPrice
                {
                    InvUnique = InvPrice1.InvUnique
                    , Department = InvPrice1.Department
                    , EndDate = InvPrice1.EndDate
                    , PartUnique = InvPrice1.PartUnique
                    , QuanDisc = InvPrice1.QuanDisc
                    , RegularPrice = InvPrice1.RegularPrice
                    , RScheduleType = InvPrice1.RScheduleType
                    , SalePrice = InvPrice1.SalePrice
                    , ScheduleLevel = InvPrice1.ScheduleLevel
                    , SScheduleType = InvPrice1.SScheduleType
                    , StartDate = InvPrice1.StartDate
                };

            return RecentPrices.ToList();
        }

        public static InventoryBasePriceInfo GetBasePriceInfo
        (
            this IEnumerable<InvPrice> TodaysPrices
            , IEnumerable<Inventry_27> Inventry_27s
            , short Schedule_0
            , short Schedule_List
            , short Schedule_Cash
        )
        {

            var PriceInfos =
                from inv in Inventry_27s
                join zero in TodaysPrices.Where(p => p.ScheduleLevel == Schedule_0)
                on inv.InvUnique equals zero.PartUnique
                join list in TodaysPrices.Where(p => p.ScheduleLevel == Schedule_List)
                on inv.InvUnique equals list.PartUnique
                join cash in TodaysPrices.Where(p => p.ScheduleLevel == Schedule_Cash)
                on inv.InvUnique equals cash.PartUnique
                select new InventoryBasePriceInfo
                {
                    PartUnique = inv.InvUnique
                    , Department = zero.Department
                    , WholesaleCost = inv.Wholesale_1
                    , Freight = inv.Freight_1
                    , Duty = inv.Duty_1
                    , Extra = inv.WholeExtra_1
                    , SKU = inv.Part_Fixed()
                    , src_Inventry_27 = inv

                    , ScheduleLevel_0 = zero.ScheduleLevel
                    , RScheduleType_0 = zero.RScheduleType
                    , RegularPrice_0 = zero.RegularPrice
                    , SScheduleType_0 = zero.SScheduleType
                    , SalePrice_0 = zero.SalePrice
                    
                    , ScheduleLevel_List = list.ScheduleLevel
                    , RScheduleType_List = list.RScheduleType
                    , RegularPrice_List = list.RegularPrice
                    , SScheduleType_List = list.SScheduleType
                    , SalePrice_List = list.SalePrice

                    , ScheduleLevel_Cash = cash.ScheduleLevel
                    , RScheduleType_Cash = cash.RScheduleType
                    , RegularPrice_Cash = cash.RegularPrice
                    , SScheduleType_Cash = cash.SScheduleType
                    , SalePrice_Cash = cash.SalePrice
                    

                };

            var PriceInfo = PriceInfos.First();
            PriceInfo.CalculateBasePrices();
            return PriceInfos.First();
        }

        #region Load From Reader Functionality
        public static bool LoadFromReader(InvPrice invPrice, OdbcDataReader reader)
        {
            return invPrice.LoadFromReader_Ext(reader);
        }

        public static bool LoadFromReader_Ext(this InvPrice invPrice, OdbcDataReader reader)
        {
            invPrice = invPrice ?? new InvPrice();

            bool loadSucceeded = true;
            try
            {
                //invPrice.Department = (Int16)reader["Department"];
                invPrice.Department = (Int16)TryAssign("Department", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.EndDate = (DateTime)(reader["EndDate"]).CDbNull(new DateTime(1000, 1, 1));
                //invPrice.EndDate = (DateTime)TryAssign("EndDate", reader, new DateTime(1000, 1, 1), loadSucceeded, out loadSucceeded);
                invPrice.InvUnique = (int)TryAssign("InvUnique", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.PartUnique = (int)TryAssign("PartUnique", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.QuanDisc = (Single)TryAssign("QuanDisc", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.RegularPrice = (double)TryAssign("RegularPrice", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.RScheduleType = (string)TryAssign("RScheduleType", reader, "", loadSucceeded, out loadSucceeded);
                invPrice.SalePrice = (double)TryAssign("SalePrice", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.ScheduleLevel = (Int16)TryAssign("ScheduleLevel", reader, 0, loadSucceeded, out loadSucceeded);
                invPrice.SScheduleType = (string)TryAssign("SScheduleType", reader, "", loadSucceeded, out loadSucceeded);
                invPrice.StartDate = (DateTime)(reader["StartDate"]).CDbNull(new DateTime(1000, 1, 1));
                loadSucceeded = true;
            }
            catch (Exception e)
            {
                loadSucceeded = false;

                string str = GetReaderInfo(reader);
                Console.WriteLine(str);
            }
            return loadSucceeded;
        }

        private static string GetReaderInfo(OdbcDataReader reader)
        {
            string str = string.Empty;
            try
            {
                object[] fields = new object[reader.VisibleFieldCount];
                reader.GetValues(fields);
                str = string.Empty;
                int index = 0;
                foreach (var field in fields)
                {
                    str += reader.GetName(index) + ": " + field.ToString() + ",";
                }
            }
            catch (OdbcException e)
            {
                str = $"GetReaderInfo failed with message: {e.Message}";
            }
            return str;
        }

        private static object TryAssign(string FieldName, OdbcDataReader reader, object DefaultValue, bool LoadSuccededIn, out bool LoadSucceeded)
        {
            try
            {
                if (LoadSuccededIn) // don't change loadsucceded if already false
                {
                    LoadSucceeded = true;
                }
                else
                {
                    LoadSucceeded = false;
                }
                object val = reader[FieldName];
                if (val == null)
                {
                    LoadSucceeded = false;
                    return DefaultValue;
                }    
                else
                {
                    return reader[FieldName];
                }
            }
            catch (OdbcException e)
            {
                LoadSucceeded = false;
                return DefaultValue;
            }
        }

        #endregion Load From Reader Functionality


    }
}
