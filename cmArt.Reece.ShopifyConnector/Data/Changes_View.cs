using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;


namespace cmArt.Reece.ShopifyConnector
{
    [DelimitedRecord(",")]
    public class Changes_View
    {
        public int InvUnique { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string Cat { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string PartNumber { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string FieldName { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string S5ValueToSendToExternal { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string ExternalValueBeforeUpdate { get; set; }
    }

}
