using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ClientControllerService
{
    public interface IXRef<T> : IPrimaryKey<T>
    {
        IEnumerable<T> GetXRefValues();
    }
    public interface IPrimaryKey<T>
    {
        bool IsEmpty(T value);
        T GetPrimaryKey();
    }

}
