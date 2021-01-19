using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Linq;


namespace cmArt.LibIntegrations
{
    public class Updater<TCommon, TKey> : IUpdater<TCommon, TKey>
    {
        private IEnumerable<TCommon> _SourceRecords;
        private IEnumerable<TCommon> _DestRecords;
        public IEnumerable<TCommon> SourceRecords { 
            get 
            {
                return _SourceRecords;
            }
            set
            {
                _SourceRecords = _SourceRecords ?? value.AsEnumerable();
            } 
        }

        public IEnumerable<TCommon> DestRecords {
            get
            {
                return _DestRecords;
            }
            set
            {
                _DestRecords = _DestRecords ?? value.AsEnumerable();
            }
        }
        private Func<TCommon, TKey> _fGetKey;
        public Func<TCommon, TKey> fGetKey {
            get
            {
                return _fGetKey;
            }
            set
            {
                _fGetKey = _fGetKey ?? value;
            }
        }
     
        public Updater()
        {

        }
        public IEnumerable<Tuple<TCommon,TCommon>> GetRsWithDiffs()
        {
            Type T = typeof(TCommon);
            FieldInfo[] fields = T.GetFields();

            Dictionary<string, string> diffs = new Dictionary<string, string>();

            IEnumerable<TCommon> src;
            IEnumerable<TCommon> dest;
            src = _SourceRecords;
            dest = _DestRecords;

            

            foreach (var field in fields)
            {

            }

            throw new NotImplementedException();
            return new List<Tuple<TCommon, TCommon>>();
        }


        // TCommon Source
        // TCommon Dest
        // Key for TCommon
        // Routine that Updates fields in Dest based on matching source
    }
}
