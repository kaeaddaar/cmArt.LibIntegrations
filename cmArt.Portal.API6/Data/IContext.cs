using cmArt.Portal.Data;
using Microsoft.EntityFrameworkCore;

namespace cmArt.Portal.API6.Data
{
    public interface IContext
    {
        DbSet<Document> JsonDocuments { get; set; }
    }
}