using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ETLPatternService
{
    public interface IAdapter<TTarget, TOrigin>
    {
        void Init(TOrigin data);
    }
}
