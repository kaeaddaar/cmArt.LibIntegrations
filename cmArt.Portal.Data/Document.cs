using System;


namespace cmArt.Portal.Data
{
    public class Document : IDocument
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public Guid projectId { get; set; }
        public string documentName { get; set; }//50 characters
        public string documentValue { get; set; }
    }
}
