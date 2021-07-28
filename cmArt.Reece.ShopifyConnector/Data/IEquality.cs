using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Reece.ShopifyConnector
{
    public interface IEquality<T>
    {
        bool Equals(T compareTo);
    }
}
