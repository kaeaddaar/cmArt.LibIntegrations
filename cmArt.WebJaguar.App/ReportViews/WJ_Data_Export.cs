using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;


namespace cmArt.WebJaguar.App.ReportViews
{
    [DelimitedRecord(",")]
    public class WJ_Data_Export
    {
        public int id { get; set; }
        public decimal inventory { get; set; }
        public decimal inventoryAFS { get; set; }
        public double msrp { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string name { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string sku { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string categoryIds { get; set; }

        public double cost { get; set; }
        public double priceTable1 { get; set; }
        public double priceTable2 { get; set; }
        public double priceTable3 { get; set; }
        public double priceTable4 { get; set; }
        public double priceTable5 { get; set; }
        public double priceTable6 { get; set; }
        public double priceTable7 { get; set; }
        public double priceTable8 { get; set; }
        public double priceTable9 { get; set; }
        public double priceTable10 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string note { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field1 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field2 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field3 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field4 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field5 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field6 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field7 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field8 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field9 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field10 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field11 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field12 { get; set; }

        [FieldQuoted]
        [FieldNullValue(typeof(string), "")]
        public string field13 { get; set; }

    }
}
