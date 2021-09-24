using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.Shopify.App.Services
{

    public class GetAddsNEdits<T, iOfT> where T : iOfT
    {
        // we need the equality function
        private Func<iOfT, iOfT, bool> _fEquals;
        private IEnumerable<T> _SourceData;
        private IEnumerable<T> _DataToChange;
        private IEnumerable<T> _RecordsToAdd;
        private IEnumerable<T> _RecordsToEdit;

        public GetAddsNEdits()
        {

        }
        public void Init(Func<iOfT, iOfT, bool> fEquals, IEnumerable<T> SourceData, IEnumerable<T> DataToChange)
        {

        }
    }
    public class CompareData
    {

    }
}
