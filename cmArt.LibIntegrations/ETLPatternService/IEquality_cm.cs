using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ETLPatternService
{
    public interface IEquality_cm<T>
    {
        bool cmEquals(T compareTo);
    }
}
