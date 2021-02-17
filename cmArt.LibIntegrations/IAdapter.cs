using System;

namespace cmArt.LibIntegrations
{
    public interface IAdapter<TFrom, IFrom, TTo, ITo> : ICopyable<ITo>
    {
        void Init(IFrom InterfaceToAdaptTo);
    }
}