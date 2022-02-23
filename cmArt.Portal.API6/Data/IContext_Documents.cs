using cmArt.Portal.Data6;
using Microsoft.EntityFrameworkCore;

namespace cmArt.Portal.API6.Data
{
    public interface IContext_Documents
    {
        DbSet<Document> JsonDocuments { get; set; }
    }
}