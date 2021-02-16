using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    //public abstract class Adapter<TFrom, ITo> : ICloneable, IAdapter<TFrom> where TFrom : ITo, new()
    public abstract class Adapter<TFrom, IFrom, TTo, ITo> : IAdapter<TFrom, IFrom, TTo, ITo>
        where TFrom : IFrom, new()
        where TTo : ITo, new()
    {
        protected IFrom _state;
        public Adapter()
        {
        }
        public abstract ITo CopyFrom(ITo From);

        public void Init(IFrom InterfaceToAdaptFrom)
        {
            _state = InterfaceToAdaptFrom ?? (IFrom)(new TFrom());
        }
    }
}
