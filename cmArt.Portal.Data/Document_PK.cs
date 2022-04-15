using cmArt.LibIntegrations.ClientControllerService;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public class Document_PK : Document, IPrimaryKey<Guid>
    {
        public Guid GetPrimaryKey()
        {
            return this.id;
        }

        public bool IsEmpty(Guid value)
        {
            return value == Guid.Empty;
        }
    }
}
