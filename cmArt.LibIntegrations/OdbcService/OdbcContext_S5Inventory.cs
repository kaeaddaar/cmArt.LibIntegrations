using cmArt.System5.Data;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using cmArt.LibIntegrations.OdbcService.ReaderExtensions;


namespace cmArt.LibIntegrations.OdbcService
{
    public class OdbcContext_S5Inventory : IDisposable
    {
        public (string InvPrice, string AltSuply, string Inventry, string Stok, string Comments)
            TableNames = ("InvPrice", "AltSuply", "Inventry", "Stok", "Comments");

        private OdbcConnection conn;

        public OdbcContext_S5Inventory(Options OdbcOptions)
        {
            string strConn = JsonSerializer.Deserialize<string>(OdbcOptions.JSON);

            conn = new OdbcConnection(strConn);
            conn.Open();
            // check connection open

            CommentsLines = OdbcDbLoader<Comments>.Load_Table_FromDatabase
            (
                TableNames.Comments
                , $"Select * From {TableNames.Comments}"
                , ICommentsExtensions.LoadFromReader
                , conn
            );

            Inventry_27s = OdbcDbLoader<Inventry_27>.Load_Table_FromDatabase
            (
                TableNames.Inventry
                , $"Select * From {TableNames.Inventry}"
                , IInventry_27Extensions.LoadFromReader
                , conn
            );

            AltSuplies = OdbcDbLoader<AltSuply>.Load_Table_FromDatabase
            (
                TableNames.AltSuply
                , $"Select * From {TableNames.AltSuply}"
                , IAltSuplyExtensions.LoadFromReader
                , conn
            );

            InvPrices = OdbcDbLoader<InvPrice>.Load_Table_FromDatabase
            (
                TableNames.InvPrice
                , $"Select * From {TableNames.InvPrice}"
                , IInvPriceExtensions.LoadFromReader
                , conn
            );

            StokLines = OdbcDbLoader<Stok>.Load_Table_FromDatabase
            (
                TableNames.Stok
                , $"Select * From {TableNames.Stok}"
                , IStokExtensions.LoadFromReader
                , conn
            );


        }

        public IEnumerable<IInvPrice> InvPrices { get; set; }
        public IEnumerable<IInventry_27> Inventry_27s { get; set; }
        public IEnumerable<IAltSuply> AltSuplies { get; set; }
        public IEnumerable<IStok> StokLines { get; set; }
        public IEnumerable<IComments> CommentsLines { get; set; }


        public void Dispose()
        {
            InvPrices = null;
            Inventry_27s = null;
            AltSuplies = null;
            StokLines = null;
            CommentsLines = null;
            conn.Close();
            conn = null;
        }
    }

}
