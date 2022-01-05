using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.App
{
    public class Transformation_Changes_Pattern<TSource, TCommon, TExternal, TCommon_Pair_Flat> where TCommon : new()
    {
        // Transform: This part of the pattern pulls comparison data to use in the transformation process and prepares it for loading.
        //  - The comparison data could be from the destination to help with a sync process, or the last export to perform a self compare
        //1. An Adapter to convert from the Source Data to a common format is the normal entry point
        //  - We should build in a way that allows us to take the adapted data in directly. 
        //  - Working with adapted data gives us the ability to put this code elsewhere and pass small amounts of data around.
        //2. Get Comparison Data (From Caching Pattern, or External Source) so either pass in a function, or another class with an interface
        //  we can use to get the data
        //3. Get Changed Records
        //4. Get New Records
        //5. Build Reports

        // Load: This part of the pattern loads the transformed data to the external source
        //1. Push changes to external source
        //2. Push new records to external source

        private TCommon _SourceData;
        
        public Transformation_Changes_Pattern()
        {

        }
        public void Init(TCommon SourceData)
        {
            _SourceData = SourceData ?? new TCommon();
        }
        

    }
}
