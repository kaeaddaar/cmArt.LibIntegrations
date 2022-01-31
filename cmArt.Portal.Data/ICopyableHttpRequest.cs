using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public interface ICopyableHttpRequest<T>
    {
        T CopyFrom(HttpRequest req, dynamic data);
    }
}
