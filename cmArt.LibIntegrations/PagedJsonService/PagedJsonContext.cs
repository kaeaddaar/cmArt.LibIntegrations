using cmArt.System5.Data;
using cmArt.LibIntegrations.SerializationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using cmArt.LibIntegrations.OdbcService;
using System.Linq;
using cmArt.System5.Inventory;


namespace cmArt.LibIntegrations.PagedJsonService
{
    public class PagedJsonContext : IS5Inventory_ReadOnly
    {

        public IEnumerable<IInvPrice> InvPrices { get; set; }
        public IEnumerable<IInventry_27> Inventry_27s { get; set; }
        public IEnumerable<IAltSuply> AltSuplyLines { get; set; }
        public IEnumerable<IStok> StokLines { get; set; }
        public IEnumerable<IComments> CommentsLines { get; set; }


        public short Schedule_0 { get; set; }
        public short Schedule_Cash { get; set; }
        public short Schedule_List { get; set; }
        public short Schedule_Sale { get; set; }

        private (string InvPrice, string AltSuply, string Inventry, string Stok, string Comments) TableNames;
        private (int InvPrice, int AltSuply, int Inventry, int Stok, int Comments) PgSize;

        private void ClearExistingPagedFiles(Options DataOptions)
        {
            PagedJsonSettings options = JsonSerializer.Deserialize<PagedJsonSettings>(DataOptions.JSON);
            string directory = $"{options.CachedFilesDirectory}";
            IEnumerable<string> Cachedfiles;
            Cachedfiles = GenericSerialization<InvPrice>.RemoveCachedFileNamesFromDirectory(directory, "InvPrice");
            Cachedfiles = GenericSerialization<Inventry_27>.RemoveCachedFileNamesFromDirectory(directory, "Inventry_27");
            Cachedfiles = GenericSerialization<AltSuply>.RemoveCachedFileNamesFromDirectory(directory, "AltSuply");
            Cachedfiles = GenericSerialization<Stok>.RemoveCachedFileNamesFromDirectory(directory, "Stok");
            Cachedfiles = GenericSerialization<Comments>.RemoveCachedFileNamesFromDirectory(directory, "Comments");

        }
        public PagedJsonContext()
        {
            InvPrices = new List<IInvPrice>();
            Inventry_27s = new List<IInventry_27>();
            AltSuplyLines = new List<IAltSuply>();
            StokLines = new List<IStok>();
            CommentsLines = new List<IComments>();

            init();
        }
        private void init()
        {
            TableNames = ("InvPrice", "AltSuply", "Inventry", "Stok", "Comments");
            PgSize = (100000, 100000, 20000, 20000, 40000);
            Schedule_0 = 0;
            Schedule_Cash = 0;
            Schedule_List = 0;
            Schedule_Sale = 0;
        }
        public void SaveToPagedFiles(Options DataOptions)
        {
            PagedJsonSettings options = JsonSerializer.Deserialize<PagedJsonSettings>(DataOptions.JSON);
            ClearExistingPagedFiles(DataOptions);

            GenericSerialization<IInvPrice>.SerializeToJSON
                (InvPrices.ToList(), TableNames.InvPrice, options.CachedFilesDirectory, PgSize.InvPrice);

            GenericSerialization<IInventry_27>.SerializeToJSON
                (Inventry_27s.ToList(), TableNames.Inventry, options.CachedFilesDirectory, PgSize.Inventry);

            GenericSerialization<IAltSuply>.SerializeToJSON
                (AltSuplyLines.ToList(), TableNames.AltSuply, options.CachedFilesDirectory, PgSize.AltSuply);

            GenericSerialization<IStok>.SerializeToJSON
                (StokLines.ToList(), TableNames.Stok, options.CachedFilesDirectory, PgSize.Stok);

            GenericSerialization<IComments>.SerializeToJSON
                (CommentsLines.ToList(), TableNames.Comments, options.CachedFilesDirectory, PgSize.Comments);

        }
        public PagedJsonContext(Options DataOptions)
        {
            PagedJsonSettings options = JsonSerializer.Deserialize<PagedJsonSettings>(DataOptions.JSON);

            init();

            InvPrices = GenericSerialization<InvPrice>.ReadOrDeserializeTable
                (TableNames.InvPrice, options.CachedFilesDirectory, PgSize.InvPrice);

            AltSuplyLines = GenericSerialization<AltSuply>.ReadOrDeserializeTable
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
