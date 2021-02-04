using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class Stok : IStok
    {
        public long StUnique { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public long PartPtr { get; set; }
        public long ProductPtr { get; set; }
        public Single PriceQty { get; set; }
        public Int16 Department { get; set; }
        public byte StockStatus { get; set; }
        public double PickQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long LocationPtr { get; set; }
        public long PickPriority { get; set; }
        public long Proximity { get; set; }
        public double CostQuantity { get; set; }
        public double Wholesale { get; set; }
        public double WholeExtra { get; set; }
        public double Duty { get; set; }
        public double Freight { get; set; }
        public Int16 CurrencyCode { get; set; }
        public long CheckPtr { get; set; }
        public double Foreign { get; set; }
        public long SupplierPtr { get; set; }
        public long TrandataPtr { get; set; }
        public bool Costed { get; set; }
        public double Weight { get; set; }
        public long HeaderPtr { get; set; }
        public byte CostStatus { get; set; }
        public long BillPtr { get; set; }
        public string Country { get; set; }

    }

}
