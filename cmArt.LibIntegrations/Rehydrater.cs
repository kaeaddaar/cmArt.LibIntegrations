using cmArt.BevNet;
using System;
using System.Collections.Generic;
using System.Linq;


namespace cmArt.LibIntegrations
{
    /// <summary>
    /// Update T from TCommon. We need to be able to turn T to TCommon and visa versa, and this code will
    /// use TCommon to update T
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="ICommon"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    //public class Rehydrater<I, ICommon, TKey, TAdapter, TCommon, IAdapter> where TAdapter : IAdapter<ICommon>, ICloneable, ICommon, new()
    //    where TCommon : ICommon, new()
    public class Rehydrater<TFrom_Clean, IFrom_Clean, TTo, ITo, T_Adapter, IKey>
        where TFrom_Clean : IFrom_Clean, new()
        where TTo : ITo, new()
        where T_Adapter : IAdapter<TFrom_Clean, IFrom_Clean, TTo, ITo>, new()
    {
        private IEnumerable<IFrom_Clean> _IntegrationRecords;
        private IEnumerable<ITo> _CommonFieldRecords;
        private Func<ITo, IKey> _fKey;

        public IEnumerable<IFrom_Clean> IntegrationRecords 
        {
            get
            {
                _IntegrationRecords = _IntegrationRecords ?? new List<IFrom_Clean>();
                return _IntegrationRecords;
            }
            set
            {
                _IntegrationRecords = value ?? _IntegrationRecords ?? new List<IFrom_Clean>();
            }
        }
        public IEnumerable<ITo> CommonFieldRecords 
        {
            get
            {
                _CommonFieldRecords = _CommonFieldRecords ?? new List<ITo>();
                return _CommonFieldRecords;
            }
            set
            {
                _CommonFieldRecords = value ?? _CommonFieldRecords ?? new List<ITo>();
            } 
        }
        public Func<ITo, IKey> fKey
        {
            get { return _fKey; }
            set { _fKey = value; }
        }

        public void UpdateIntegrationRecords()
        {
            IEnumerable<T_Adapter> adapters = _IntegrationRecords.Select(ConvertToAdapter());

            // if the following is done on a T_Clean then the result will be cleaned _IntegrationRecords
            // won't clean if the base object is passed in
            IEnumerable<ITo> commonFields = adapters.Select(CopyFrom_CommonFieldsRecords_To_Adapters());
        }

        private static Func<T_Adapter, ITo> CopyFrom_CommonFieldsRecords_To_Adapters()
        {
            return a => 
            {
                ITo cf = new TTo();
                cf = (ITo)a.CopyFrom(cf);
                return cf; 
            };
        }

        private static Func<IFrom_Clean, T_Adapter> ConvertToAdapter()
        {
            return cf =>
            {
                T_Adapter a = new T_Adapter();
                ((IAdapter<TFrom_Clean, IFrom_Clean, TTo, ITo>)a).Init(cf);
                return a;
            };
        }
        // Loader
        // Common Fields
        // Key for TCommon
    }
}
