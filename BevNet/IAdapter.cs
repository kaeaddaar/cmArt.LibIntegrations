using System;

namespace cmArt.BevNet
{
    public interface IAdapter<TFrom, IFrom, TTo, ITo> : ICopyable<ITo>
    {
        void Init(IFrom InterfaceToAdaptTo);
    }
}