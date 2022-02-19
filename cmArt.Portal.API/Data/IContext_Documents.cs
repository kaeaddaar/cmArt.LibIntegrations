using cmArt.Portal.Data;
using Microsoft.EntityFrameworkCore;

namespace cmArt.Portal.API.Data
{
    public interface IContext_Documents
    {
        DbSet<Document> JsonDocuments { get; set; }
    }
}