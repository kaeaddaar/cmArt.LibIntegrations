using cmArt.Portal.Data;
using cmArt.Portal.Data.InventoryData;
using Microsoft.EntityFrameworkCore;

namespace cmArt.Portal.API.Data
{
    public interface IContext_WebInventory
    {
        DbSet<WebInventory> WebInventoryRecords { get; set; }
    }
}