using System;
using System.Collections.Generic;

namespace cmArt.LibIntegrations
{
    /// <summary>
    /// Update T from TCommon. We need to be able to turn T to TCommon and visa versa, and this code will
    /// use TCommon to update T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCommon"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Rehydrater<T, TCommon, TKey>
    {
        private IEnumerable<T> _IntegrationRecords;
        private IEnumerable<TCommon> _CommonFieldRecords;
        private Func<TCommon, TKey> _fKey;

        public IEnumerable<T> IntegrationRecords 
        {
            get
            {
                _IntegrationRecords = _IntegrationRecords ?? new List<T>();
                return _IntegrationRecords;
            }
            set
            {
                _IntegrationRecords = _IntegrationRecords ?? value ?? new List<T>();
            }
        }
        public IEnumerable<TCommon> CommonFieldRecords 
        {
            get
            {
                _CommonFieldRecords = _CommonFieldRecords ?? new List<TCommon>();
                return _CommonFieldRecords;
            }
            set
            {
                _CommonFieldRecords = _CommonFieldRecords ?? value ?? new List<TCommon>();
            } 
        }
        public Func<TCommon, TKey> fKey
        {
            get { return _fKey; }
            set { _fKey = value; }
        }

        // Loader
        // Common Fields
        // Key for TCommon
    }
}
