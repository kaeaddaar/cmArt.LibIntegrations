using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using cmArt.Portal.API6.Data;
using cmArt.Portal.Data6;

namespace cmArt.Portal.API6.Repositories
{
    public class Document_Repository : INotifyPropertyChanged
    {
        IContext_Documents _context;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static List<Document> GetJsonDocuments(Context_Documents context)
        {
            if (context.JsonDocuments.Count() < 1)
            {
                return new List<Document>();
            }
            else
            {
                return context.JsonDocuments.ToList();
            }
        }
        public static Document GetJsonDocument(IContext_Documents context, Guid Id)
        {
            var recordsFiltered = context.JsonDocuments.Where(inv => inv.Id == Id);
            bool HasRecords = recordsFiltered.Count() > 1;
            bool UniqueProvided = Id != Guid.Empty;
            if (HasRecords && UniqueProvided)
            {
                throw new KeyNotFoundException(string.Format("The Document.Id \"{0}\" was not found.", Id));
            }
            return recordsFiltered.FirstOrDefault();
        }
        public static Guid AddJsonDocument(Context_Documents context, Document DocToAdd)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context must not be null");
            }
            if (DocToAdd == null)
            {
                throw new ArgumentNullException("The document you are adding must not be null");
            }

            Document Doc = new Document();
            Doc.Id = DocToAdd.Id;
            Doc.CustomerId = DocToAdd.CustomerId;
            Doc.ProjectId = DocToAdd.ProjectId;
            Doc.DocumentName = (DocToAdd.DocumentName ?? string.Empty).Trim();
            Doc.DocumentValue = (DocToAdd.DocumentValue ?? string.Empty).Trim();


            //Document tmp = context.JsonDocuments
            //    .Where(x => x.CustomerId == Doc.CustomerId && x.ProjectId == Doc.ProjectId && x.DocumentName == Doc.DocumentName)
            //    .FirstOrDefault();
            Document tmp = context.JsonDocuments
                .Where(x => x.Id == Doc.Id)
                .FirstOrDefault();

            bool AlreadyExists = tmp != null;
            if (!AlreadyExists)
            {
                context.JsonDocuments.Add(Doc);
                context.SaveChanges();
                tmp = context.JsonDocuments
                    .Where(x => x.Id == Doc.Id)
                    .FirstOrDefault();
            }
            else
            {
                Document tmpUpdate = context.JsonDocuments
                    .Where(x => x.Id == Doc.Id)
                    .FirstOrDefault();
                bool FoundToEdit = tmpUpdate != null;
                if (FoundToEdit)
                {
                    tmpUpdate.CustomerId = DocToAdd.CustomerId;
                    tmpUpdate.ProjectId = DocToAdd.ProjectId;
                    tmpUpdate.DocumentName = (DocToAdd.DocumentName ?? string.Empty);
                    tmpUpdate.DocumentValue = (DocToAdd.DocumentValue ?? string.Empty).Trim();
                    context.SaveChanges();
                }
            }
            tmp = tmp ?? new Document();

            return tmp.Id;
        }
        public static int DeleteJsonDocument(IContext_Documents context, Document DocDel)
        {
            Document Doc = DocDel ?? new Document();
            Guid Unique = Doc.Id;

            bool found = false;
            IEnumerable<Document> DeletedInventorySimples = context.JsonDocuments.Where(x => x.Id == Unique);
            Document DeletedInventorySimple = DeletedInventorySimples.FirstOrDefault();

            int Count = DeletedInventorySimples.Count();
            found = Count > 0;

            if (!found)
            {
                throw new KeyNotFoundException($"Document not found with unique: [{Unique}]");
            }

            List<Exception> Errors = new List<Exception>();
            try
            {
                context.JsonDocuments.Remove(DeletedInventorySimple);
            }
            catch (Exception e)
            {
                Errors.Add(e);
            }

            string ErrorsJson = JsonSerializer.Serialize<List<Exception>>(Errors);
            Console.WriteLine($"Errors while deleting the Document: {ErrorsJson}");

            return 0;
        }
    }

}
