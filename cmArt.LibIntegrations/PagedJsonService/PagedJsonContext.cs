using cmArt.System5.Data;
using cmArt.LibIntegrations.SerializationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using cmArt.LibIntegrations.OdbcService;


namespace cmArt.LibIntegrations.PagedJsonService
{
    public class PagedJsonContext
    {

        public IEnumerable<IInvPrice> InvPrices { get; set; }
        public IEnumerable<IInventry_27> Inventry_27s { get; set; }
        public IEnumerable<IAltSuply> AltSuplies { get; set; }
        public IEnumerable<IStok> StokLines { get; set; }
        public IEnumerable<IComments> CommentsLines { get; set; }

        public PagedJsonContext(Options OdbcOptions)
        {
            PagedJsonSettings options = JsonSerializer.Deserialize<PagedJsonSettings>(OdbcOptions.JSON);

            (string InvPrice, string AltSuply, string Inventry, string Stok, string Comments)
                TableNames = ("InvPrice", "AltSuply", "Inventry", "Stok", "Comments");

            (int InvPrice, int AltSuply, int Inventry, int Stok, int Comments)
               PgSize = (100000, 100000, 20000, 20000, 40000);

            InvPrices = GenericSerialization<InvPrice>.ReadOrDeserializeTable
                (TableNames.InvPrice, options.CachedFilesDirectory, PgSize.InvPrice);

            AltSuplies = GenericSerialization<AltSuply>.ReadOrDeserializeTable
                (TableNames.AltSuply, options.CachedFilesDirectory, PgSize.AltSuply);

            CommentsLines = GenericSerialization<Comments>.ReadOrDeserializeTable
                (TableNames.Comments, options.CachedFilesDirectory, PgSize.Comments);

            Inventry_27s = GenericSerialization<Inventry_27>.ReadOrDeserializeTable
                (TableNames.Inventry, options.CachedFilesDirectory, PgSize.Inventry);

            StokLines = GenericSerialization<Stok>.ReadOrDeserializeTable
                (TableNames.Stok, options.CachedFilesDirectory, PgSize.Stok);

        }

    }

}
