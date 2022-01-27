using cmArt.LibIntegrations.ReportService;
using cmArt.LibIntegrations.VennMapService;
using cmArt.System5.Inventory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cmArt.LibIntegrations.ETLPatternService
{
    public class ETLPattern<TCommonFields, ICommonFields, TAdapter, TOrigin, TCFIndex>
        where TCommonFields : ICommonFields, new()
        where ICommonFields : IEquality<ICommonFields>, ICopyable<ICommonFields>, IDiffable<ICommonFields>
        where TAdapter : ICommonFields, IAdapter<ICommonFields, TOrigin>, new()// where TCommonFields : IEquality<TCommonFields>
    {
        private ILogger _logger;
        //private ILogger _loggerApiCalls;
        private IEnumerable<TCommonFields> dataS5;
        private IEnumerable<TCommonFields> dataShopify;
        private VennMap<TCommonFields, TCommonFields, TCFIndex> _vennMap;
        private IEnumerable<TCommonFields> S5CF;
        private readonly List<Changes_View> changes;

        //private Func<ICommonFields, ICommonFields, bool> fEquals;

        public VennMap<TCommonFields, TCommonFields, TCFIndex> vennMap
        {
            get { return _vennMap; }
            set { _vennMap = value; }
        }

        //S5 Extract, S5 Transform, Shopify Extract, Shopify Transform, Compare, Report, Load (Push to Shopify)
        public ETLPattern()
        {

        }
        public void init(IEnumerable<TCommonFields> DataS5, IEnumerable<TCommonFields> DataShopify, ILogger logger)
        {
            _logger = logger;
            //_loggerApiCalls = loggerApiCalls;
            dataS5 = DataS5 ?? new List<TCommonFields>();
            dataShopify = DataShopify ?? new List<TCommonFields>();
        }
        public void TransformS5Data(IEnumerable<TOrigin> Data, ICommonFields IData)//We perform the transformation
        {
            IEnumerable<TOrigin> _Data = Data ?? new List<TOrigin>();
            S5CF = _Data.Select
            (
                x =>
                {
                    TAdapter tmp = new TAdapter();
                    tmp.Init(x);
                    TCommonFields cf = new TCommonFields();
                    cf.CopyFrom(tmp);

                    return cf;
                }
            );
        }
        public void TransformedS5Data(IEnumerable<TCommonFields> Data)//We use transformed data
        {
            S5CF = Data ?? new List<TCommonFields>();
        }
        public void Compare(Func<TCommonFields, TCFIndex> fIndex, Func<TCommonFields, bool> fVennCondition_EcommEqualsY)
        {
            _vennMap = new VennMap<TCommonFields, TCommonFields, TCFIndex>(dataS5, dataShopify, fIndex, fIndex, fVennCondition_EcommEqualsY);
        }
        public void Report()
        {
            // vannMap should be the input for any Report engine so we just need to make vennMap data accessible
            // Get Detailed Differences
        }
        public IEnumerable<Changes_View> GetDetailedDifferences(Func<ICommonFields, ICommonFields, bool> fEquals, Func<ICommonFields, TCFIndex> fUnique) //fIndex should probably consider using IShopifyIdentity style, but may not have to as this is a generic class
        {
            List<Changes_View> changes = new List<Changes_View>();

            IEnumerable<ICommonFields> dataS5_CF = dataS5.Select(x => (ICommonFields)x);
            IEnumerable<ICommonFields> dataShopify_CF = dataShopify.Select(x => (ICommonFields)x);
            _logger.LogInformation("GetDetailedDifferences_And_Create_Reports()");
            var ChangedRecords_Product_Pairs = UpdateProcessPattern<ICommonFields, TCommonFields, TCFIndex>
                .GetChangedRecordPairs(fUnique, fEquals, dataS5_CF, dataShopify_CF);

            IEnumerable<IEnumerable<Changes_View>> tmpProd = ChangedRecords_Product_Pairs.Select(x => x.Item1.Diff(x.Item2));
            IEnumerable<Changes_View> tmpDetailProd = tmpProd.SelectMany(x => x);
            foreach (var x in tmpDetailProd) { changes.Add(x); }

            return changes;
        }
        public void Load()
        {
            // push records to add edit and delete to somewhere where they can be loaded.
            // I think the connector implementations have lots of potential differences. so the idea is to just make vennMap accessible for the data load.
        }

    }

}
