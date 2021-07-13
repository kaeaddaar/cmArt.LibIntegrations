using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Data
{
    public interface IEquality<T>
    {
        bool Equals(T compareTo);
    }
}
