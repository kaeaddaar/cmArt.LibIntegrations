using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Inventory
{
    public interface IEquality<T>
    {
        bool Equals(T compareTo);
    }
}
