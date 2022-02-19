using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ClientControllerService
{
    public interface IXRef<T>
    {
        IEnumerable<T> GetXRefValues();
        T GetPrimraryKey();
    }

}
