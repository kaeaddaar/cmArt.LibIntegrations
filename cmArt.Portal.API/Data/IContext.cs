using cmArt.Portal.Data;
using Microsoft.EntityFrameworkCore;

namespace cmArt.Portal.API.Data
{
    public interface IContext
    {
        DbSet<Document> JsonDocuments { get; set; }
    }
}