using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations
{
    public interface ICopyable<I>
    {
        I CopyFrom(I IFrom);
    }
}
