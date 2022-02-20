using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace cmArt.Portal.API.Data
{
    public interface ICopyableHttpRequest<T>
    {
        T CopyFrom(HttpRequest req, dynamic data);
    }
}
