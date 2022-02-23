using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using cmArt.Portal.API6.Data;
using cmArt.Portal.Data6;
using cmArt.Reece.ShopifyConnector;


namespace cmArt.Portal.API6.Repositories
{
    public class WebInventory_Repository : INotifyPropertyChanged
    {
        //IContext_WebInventory _context;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static List<WebInventory> GetWebInventoryRecords(Context_WebInventory context)
        {
            if (context.WebInventoryRecords.Count() < 1)
            {
                return new List<WebInventory>();
            }
            else
            {
                return context.WebInventoryRecords.ToList();
            }
        }
        public static WebInventory GetWebInventoryRecord(IContext_WebInventory context, int Id)
        {
            var recordsFiltered = context.WebInventoryRecords.Where(inv => inv.InvUnique == Id);
            bool HasRecords = recordsFiltered.Count() > 1;
            bool UniqueProvided = Id != 0;
            if (HasRecords && UniqueProvided)
            {
                throw new KeyNotFoundException(string.Format("The WebInventory.Id \"{0}\" was not found.", Id));
            }
            return recordsFiltered.FirstOrDefault();
        }
        public static int AddWebInventoryRecord(Context_WebInventory context, WebInventory DocToAdd)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context must not be null");
            }
            if (DocToAdd == null)
            {
                throw new ArgumentNullException("The WebInventory you are adding must not be null");
            }

            WebInventory Doc = new WebInventory();
            Doc.InvUnique = DocToAdd.InvUnique;
            Doc.Cat = (DocToAdd.Cat ?? string.Empty).Trim();
            Doc.PartNumber = (DocToAdd.PartNumber ?? String.Empty).Trim();
            Doc.Description = (DocToAdd.Description ?? String.Empty).Trim();
            Doc.WebCategory = (DocToAdd.WebCategory ?? String.Empty).Trim();
            Doc.ImageUrl = (DocToAdd.ImageUrl ?? string.Empty).Trim();
            Doc.Prices = DocToAdd.Prices ?? new List<S5PricePair>();
            Doc.Quantities = DocToAdd.Quantities ?? new List<S5QtyPair>();

            WebInventory tmp = context.WebInventoryRecords
                .Where(x => x.InvUnique == Doc.InvUnique)
                .FirstOrDefault();

            bool AlreadyExists = tmp != null;
            if (!AlreadyExists)
            {
                context.WebInventoryRecords.Add(Doc);
                context.SaveChanges();
                tmp = context.WebInventoryRecords
                    .Where(x => x.InvUnique == Doc.InvUnique)
                    .FirstOrDefault();
            }
            else
            {
                WebInventory tmpUpdate = context.WebInventoryRecords
                    .Where(x => x.InvUnique == Doc.InvUnique)
                    .FirstOrDefault();
                bool FoundToEdit = tmpUpdate != null;
                if (FoundToEdit)
                {
                    tmpUpdate.Cat = (DocToAdd.Cat ?? string.Empty).Trim();
                    tmpUpdate.PartNumber = (DocToAdd.PartNumber ?? String.Empty).Trim();
                    tmpUpdate.Description = (DocToAdd.Description ?? String.Empty).Trim();
                    tmpUpdate.WebCategory = (DocToAdd.WebCategory ?? String.Empty).Trim();
                    tmpUpdate.ImageUrl = (DocToAdd.ImageUrl ?? string.Empty).Trim();
                    tmpUpdate.Prices = DocToAdd.Prices ?? new List<S5PricePair>();
                    tmpUpdate.Quantities = DocToAdd.Quantities ?? new List<S5QtyPair>();
                    context.SaveChanges();
                }
            }
            tmp = tmp ?? new WebInventory();

            return tmp.InvUnique;
        }
        public static int DeleteWebInventoryRecord(IContext_WebInventory context, WebInventory DocDel)
        {
            WebInventory Doc = DocDel ?? new WebInventory();
            int Unique = Doc.InvUnique;

            bool found = false;
            IEnumerable<WebInventory> DeletedInventorySimples = context.WebInventoryRecords.Where(x => x.InvUnique == Unique);
            WebInventory DeletedInventorySimple = DeletedInventorySimples.FirstOrDefault();

            int Count = DeletedInventorySimples.Count();
            found = Count > 0;

            if (!found)
            {
                throw new KeyNotFoundException($"WebInventory not found with unique: [{Unique}]");
            }

            List<Exception> Errors = new List<Exception>();
            try
            {
                context.WebInventoryRecords.Remove(DeletedInventorySimple);
            }
            catch (Exception e)
            {
                Errors.Add(e);
            }

            string ErrorsJson = JsonSerializer.Serialize<List<Exception>>(Errors);
            Console.WriteLine($"Errors while deleting the WebInventory: {ErrorsJson}");

            return 0;
        }
    }

}
