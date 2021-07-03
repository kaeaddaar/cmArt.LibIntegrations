using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cmArt.System5.Inventory
{
    public static class IS5InventoryExtensions
    {

        public static IEnumerable<IS5InvAssembled> ToAssembled(this IS5Inventory_ReadOnly InvToAssemble)
        {
            IEnumerable<IEnumerable<IStok>> StokLinesPerInventry = new List<List<IStok>>();
            StokLinesPerInventry = GenericAggregateToLists<IStok, int>.ToLists
            (
                InvToAssemble.StokLines
                , Stok_Indexes.InventoryUnique
            );

            var TodaysPrices = InvToAssemble.InvPrices.GetPricesAsOf(DateTime.Now);
            IEnumerable<IEnumerable<IInvPrice>> InvPricesPerInventry;
            InvPricesPerInventry = GenericAggregateToLists<IInvPrice, int>.ToLists
            (
                TodaysPrices
                , InvPrice_Indexes.InventoryUnique
            );

            IEnumerable<IEnumerable<IComments>> CommentsLinesPerInventry;
            CommentsLinesPerInventry = GenericAggregateToLists<IComments, int>.ToLists
            (
                InvToAssemble.CommentsLines.Select(comments => new Comments_Clean(comments))
                , Comments_Indexes.InventoryUnique_File135
            );

            IEnumerable<IEnumerable<IAltSuply>> AltSupliesPerInventry;
            AltSupliesPerInventry = GenericAggregateToLists<IAltSuply, int>.ToLists
            (
                InvToAssemble.AltSuplyLines.Select(altSuply => new AltSuply_Clean(altSuply))
                , AltSuply_Indexes.InventoryUnique
            );

            IEnumerable<IInventry_27> Inventry_27s = InvToAssemble.Inventry_27s.Select(inv => new Inventry_27_Clean(inv));

            IEnumerable<IS5InvAssembled> InventoryAssembled = new List<IS5InvAssembled>();

            InventoryAssembled =
                from inv27 in Inventry_27s

                join pricePerInv in InvPricesPerInventry
                on Inventry_27_Indexes.InventoryUnique(inv27) equals InvPrice_Indexes.InventoryUnique(pricePerInv.First())
                    into priceNullRecords
                from priceNullRecord in priceNullRecords.DefaultIfEmpty()

                join stockPerInv in StokLinesPerInventry
                on Inventry_27_Indexes.InventoryUnique(inv27) equals Stok_Indexes.InventoryUnique(stockPerInv.First())
                    into stockNullRecords
                from stockNullRecord in stockNullRecords.DefaultIfEmpty()

                join ffs in CommentsLinesPerInventry
                on Inventry_27_Indexes.InventoryUnique(inv27) equals Comments_Indexes.InventoryUnique_File135(ffs.First())
                    into ff4NullRecords
                from ff4NullRecord in ff4NullRecords.DefaultIfEmpty()

                join LUs in AltSupliesPerInventry
                on Inventry_27_Indexes.InventoryUnique(inv27) equals AltSuply_Indexes.InventoryUnique(LUs.First())
                    into AltSupplyNullRecords
                from AltSupplyNullRecord in AltSupplyNullRecords.DefaultIfEmpty()

                select (IS5InvAssembled)new S5InvAssembled
                (
                    Inv: inv27
                    , Price_PerInventry_27: priceNullRecord
                    , Stok_PerInventry_27: stockNullRecord
                    , Comments_PerInventry_27: ff4NullRecord
                    , AltSuply_PerInventry_27: AltSupplyNullRecord
                )
            ;

            return InventoryAssembled;
        }
    }

}
