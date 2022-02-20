using System;

namespace cmArt.Portal.Data
{
    public interface IDocument
    {
        Guid CustomerId { get; set; }
        string DocumentName { get; set; }
        string DocumentValue { get; set; }
        Guid Id { get; set; }
        Guid ProjectId { get; set; }
    }
}