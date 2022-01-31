using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public class Document : ICopyable<Document>, ICopyableHttpRequest<Document>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProjectId { get; set; }
        public string DocumentName { get; set; }//50 characters
        public string DocumentValue { get; set; }

        public Document CopyFrom(Document FromData)
        {
            Document _FromData = FromData ?? new Document();
            this.Id = _FromData.Id;
            this.CustomerId = _FromData.CustomerId;
            this.ProjectId = _FromData.ProjectId;
            this.DocumentName = (_FromData.DocumentName ?? string.Empty).TrimEnd();
            this.DocumentValue = (_FromData.DocumentValue ?? string.Empty).TrimEnd();

            return this;
        }

        public Document CopyFrom(HttpRequest req, dynamic data)
        {
            this.Id = utils.StringToGuid(utils.GetValue(req, data?.Id, "Id"));
            this.CustomerId = utils.StringToGuid(utils.GetValue(req, data?.CustomerId, "CustomerId"));
            this.ProjectId = utils.StringToGuid(utils.GetValue(req, data?.ProjectId, "ProjectId"));
            this.DocumentName = utils.GetValue(req, data?.DocumentName, "DocumentName");
            this.DocumentValue = utils.GetValue(req, data?.DocumentValue, "DocumentValue");

            return this;
        }
    }
}
