using System;

namespace cmArt.Portal.Data
{
    public interface IDocument
    {
        Guid customerId { get; set; }
        string documentName { get; set; }
        string documentValue { get; set; }
        Guid id { get; set; }
        Guid projectId { get; set; }
    }
}