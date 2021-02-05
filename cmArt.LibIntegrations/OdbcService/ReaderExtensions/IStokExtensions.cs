using cmArt.System5.Data;
using System;
using System.Data.Odbc;


namespace cmArt.LibIntegrations.OdbcService.ReaderExtensions
{
    public static class IStokExtensions
    {
        public static bool LoadFromReader(IStok stok, OdbcDataReader reader)
        {
            return stok.LoadFromReader_Ext(reader);
        }
        public static bool LoadFromReader_Ext(this IStok stok, OdbcDataReader reader)
        {
            stok = stok ?? new Stok();
            bool loadSucceeded = false;

            stok.StUnique = (Int32)FromReader(reader, "StUnique");
            stok.Date = (DateTime)(reader["Date"]).CDbNull(new DateTime(1000, 1, 1));
            stok.Number = (string)FromReader(reader, "Number");
            stok.Description = (string)FromReader(reader, "Description");
            stok.PartPtr = (Int32)FromReader(reader, "PartPtr");
            stok.ProductPtr = (Int32)FromReader(reader, "ProductPtr");
            stok.PriceQty = (Single)FromReader(reader, "PriceQty");
            stok.Department = (Int16)FromReader(reader, "Department");
            stok.StockStatus = (byte)FromReader(reader, "StockStatus");
            stok.PickQuantity = (Single)FromReader(reader, "PickQuantity");
            stok.ExpiryDate = (DateTime)(reader["ExpiryDate"]).CDbNull(new DateTime(1000, 1, 1));
            stok.LocationPtr = (Int32)FromReader(reader, "LocationPtr");
            stok.PickPriority = (Int32)FromReader(reader, "PickPriority");
            stok.Proximity = (Int32)FromReader(reader, "Proximity");
            stok.CostQuantity = (Single)FromReader(reader, "CostQuantity");
            stok.Wholesale = (double)FromReader(reader, "Wholesale");
            stok.WholeExtra = (double)FromReader(reader, "WholeExtra");
            stok.Duty = (double)FromReader(reader, "Duty");
            stok.Freight = (double)FromReader(reader, "Freight");
            stok.CurrencyCode = (Int16)FromReader(reader, "CurrencyCode");
            stok.CheckPtr = (Int32)FromReader(reader, "CheckPtr");
            stok.Foreign = (double)FromReader(reader, "Foreign");
            stok.SupplierPtr = (Int32)FromReader(reader, "SupplierPtr");
            stok.TrandataPtr = (Int32)FromReader(reader, "TrandataPtr");
            stok.Costed = (bool)FromReader(reader, "Costed");
            stok.Weight = (double)FromReader(reader, "Weight");
            stok.HeaderPtr = (Int32)FromReader(reader, "HeaderPtr");
            stok.CostStatus = (byte)FromReader(reader, "CostStatus");
            stok.BillPtr = (Int32)FromReader(reader, "BillPtr");
            stok.Country = (string)FromReader(reader, "Country");

            loadSucceeded = true;
            return loadSucceeded;
        }


        public static object FromReader(OdbcDataReader reader, string FieldName)
        {
            if (reader[FieldName] is DBNull)
            {
                return null;
            }
            object value = reader[FieldName];
            return value;

        }
    }

}
