using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations
{
    public class Copyable<T, I> : State<I>, ICopyable<I>
        where T : I
    {
        public I CopyFrom(I IFrom)
        {
            this._state = this.CopyFrom(IFrom);
            return this._state;
        }

    }
}
