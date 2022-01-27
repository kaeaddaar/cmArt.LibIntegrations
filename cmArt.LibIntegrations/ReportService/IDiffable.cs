using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.ReportService
{
    public interface IDiffable<T>
    {
        IEnumerable<Changes_View> Diff(T CompareTo);
    }
}
