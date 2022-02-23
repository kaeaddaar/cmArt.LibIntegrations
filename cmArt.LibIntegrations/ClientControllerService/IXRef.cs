using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ClientControllerService
{
    public interface IXRef<T> : IIndex<T>
    {
        IEnumerable<T> GetXRefValues();
    }
    public interface IIndex<T>
    {
        bool IsEmpty(T value);
        T GetIndex();
    }

}
