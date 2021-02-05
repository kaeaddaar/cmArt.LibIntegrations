using cmArt.System5.Data;
using System;
using System.Data.Odbc;


namespace cmArt.LibIntegrations.OdbcService.ReaderExtensions
{
    public static class IInventry_27Extensions
    {
        public static string Part_Fixed(this IInventry_27 inv)
        {
            string _part = inv.Part ?? string.Empty;
            return _part.TrimEnd();
        }

        public static bool LoadFromReader(IInventry_27 inventry_27, OdbcDataReader reader)
        {
            return inventry_27.LoadFromReader_Ext(reader);
        }

        public static bool LoadFromReader_Ext(this IInventry_27 inventry_27, OdbcDataReader reader)
        {
            inventry_27 = inventry_27 ?? new Inventry_27();

            bool loadSucceeded = false;
            inventry_27.InvUnique = (Int32)FromReader(reader, "InvUnique");
            inventry_27.Cat = (string)FromReader(reader, "Cat");
            inventry_27.Part = (string)FromReader(reader, "Part");
            inventry_27.Size_1 = (string)FromReader(reader, "Size_1");
            inventry_27.Size_2 = (string)FromReader(reader, "Size_2");
            inventry_27.Size_3 = (string)FromReader(reader, "Size_3");
            inventry_27.Supplier = (Single)FromReader(reader, "Supplier");
            inventry_27.Description = (string)FromReader(reader, "Description");
            inventry_27.Wholesale_1 = (double)FromReader(reader, "Wholesale_1");
            inventry_27.WholeExtra_1 = (double)FromReader(reader, "WholeExtra_1");
            inventry_27.Freight_1 = (double)FromReader(reader, "Freight_1");
            inventry_27.Duty_1 = (double)FromReader(reader, "Duty_1");
            inventry_27.Foreign_1 = (double)FromReader(reader, "Foreign_1");
            inventry_27.Country = (string)FromReader(reader, "Country");
            inventry_27.Tax = (Int16)FromReader(reader, "Tax");
            inventry_27.SuppPart = (string)FromReader(reader, "SuppPart");
            inventry_27.PriceQty = (Single)FromReader(reader, "PriceQty");
            inventry_27.MarkDeleted = (string)FromReader(reader, "MarkDeleted");
            inventry_27.Brand = (Single)FromReader(reader, "Brand");
            inventry_27.PackSize = (string)FromReader(reader, "PackSize");
            inventry_27.Units = (string)FromReader(reader, "Units");
            inventry_27.Weight = (Single)FromReader(reader, "Weight");
            inventry_27.KitType = (Byte)FromReader(reader, "KitType");
            inventry_27.Serial = (string)FromReader(reader, "Serial");
            inventry_27.Ecommerce = (string)FromReader(reader, "ECommerce");

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
