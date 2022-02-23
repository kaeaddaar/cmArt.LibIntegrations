using System;


namespace cmArt.Portal.Data6
{
    public class Document : IDocument
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProjectId { get; set; }
        public string DocumentName { get; set; }//50 characters
        public string DocumentValue { get; set; }
    }
}
