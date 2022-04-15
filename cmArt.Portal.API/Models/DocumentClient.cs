using cmArt.LibIntegrations;
using cmArt.Portal.API.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public class DocumentClient : Document, ICopyableHttpRequest<IDocument>, ICopyable<IDocument>
    {

        public IDocument CopyFrom(IDocument FromData)
        {
            IDocument _FromData = FromData ?? new Document();
            this.id = _FromData.id;
            this.customerId = _FromData.customerId;
            this.projectId = _FromData.projectId;
            this.documentName = (_FromData.documentName ?? string.Empty).TrimEnd();
            this.documentValue = (_FromData.documentValue ?? string.Empty).TrimEnd();

            return this;
        }
        public IDocument CopyFrom(HttpRequest req, dynamic data)
        {
            this.id = utils.StringToGuid(utils.GetValue(req, data?.id, "id"));
            this.customerId = utils.StringToGuid(utils.GetValue(req, data?.customerId, "customerId"));
            this.projectId = utils.StringToGuid(utils.GetValue(req, data?.projectId, "projectId"));
            this.documentName = utils.GetValue(req, data?.documentName, "documentName");
            this.documentValue = utils.GetValue(req, data?.documentValue, "documentValue");

            return this;
        }
    }
}
