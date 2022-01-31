using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Portal.Data
{
    public interface ICopyable<T>
    {
        T CopyFrom(T FromData);
    }
}
