using System;
using System.Collections.Generic;
using System.Linq;


namespace cmArt.LibIntegrations
{
    /// <summary>
    /// Update T from TCommon. We need to be able to turn T to TCommon and visa versa, and this code will
    /// use TCommon to update T
    /// </summary>
    /// <typeparam name="TFrom_Clean"></typeparam>
    /// <typeparam name="IFrom_Clean"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <typeparam name="ITo"></typeparam>
    /// <typeparam name="T_Adapter"></typeparam>
    /// <typeparam name="IKey"></typeparam>
    //public class Rehydrater<I, ICommon, TKey, TAdapter, TCommon, IAdapter> where TAdapter : IAdapter<ICommon>, ICloneable, ICommon, new()
    //    where TCommon : ICommon, new()
    public class Rehydrater<TFrom_Clean, IFrom_Clean, TTo, ITo, T_Adapter, IKey>
        where TFrom_Clean : IFrom_Clean, new()
        where TTo : ITo, new()
        where T_Adapter : IAdapter<TFrom_Clean, IFrom_Clean, TTo, ITo>, new()
    {
        private IEnumerable<(IFrom_Clean IFrom, ITo ITo)> _From_To_Pairs;
        private Func<ITo, IKey> _fKey;

        public IEnumerable<ValueTuple<IFrom_Clean, ITo>> From_To_Pairs
        {
            get
            {
                _From_To_Pairs = _From_To_Pairs ?? new List<ValueTuple<IFrom_Clean, ITo>>();
                return _From_To_Pairs;
            }
            set
            {
                _From_To_Pairs = value ?? _From_To_Pairs ?? new List<ValueTuple<IFrom_Clean, ITo>>();
            }
        }
        public Func<ITo, IKey> fKey
        {
            get { return _fKey; }
            set { _fKey = value; }
        }

        public void UpdateIntegrationRecords()
        {
            foreach (var pair in _From_To_Pairs)
            {
                T_Adapter adapter = new T_Adapter();
                adapter.Init(pair.IFrom);
                adapter.CopyFrom(pair.ITo);
            }
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

    }
}
