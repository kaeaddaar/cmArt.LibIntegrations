using cmArt.LibIntegrations;
using cmArt.Portal.API6.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data6
{
    public class DocumentClient : Document, ICopyableHttpRequest<Document>, ICopyable<Document>, IAs_Type<Document>
    {
        public Document As_Type(Type type)
        {
            Document tmp = new Document();
            CopyFrom(tmp, this);
            return tmp;
        }

        public Document CopyFrom(Document FromData)
        {
            //IDocument _FromData = FromData ?? new Document();
            //this.Id = _FromData.Id;
            //this.CustomerId = _FromData.CustomerId;
            //this.ProjectId = _FromData.ProjectId;
            //this.DocumentName = (_FromData.DocumentName ?? string.Empty).TrimEnd();
            //this.DocumentValue = (_FromData.DocumentValue ?? string.Empty).TrimEnd();

            //return this;

            IDocument _FromData = FromData ?? new Document();
            CopyFrom(this, _FromData);
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
        private Document CopyFrom(IDocument ToData, IDocument FromData)
        {
            IDocument _FromData = FromData ?? new Document();
            this.Id = _FromData.Id;
            this.CustomerId = _FromData.CustomerId;
            this.ProjectId = _FromData.ProjectId;
            this.DocumentName = (_FromData.DocumentName ?? string.Empty).TrimEnd();
            this.DocumentValue = (_FromData.DocumentValue ?? string.Empty).TrimEnd();

            return this;
        }
    }
}
