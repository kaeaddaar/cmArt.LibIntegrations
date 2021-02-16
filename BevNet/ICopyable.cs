using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public interface ICopyable<I>
    {
        I CopyFrom(I IFrom);
    }
}
