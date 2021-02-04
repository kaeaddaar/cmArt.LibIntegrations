using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public interface IStok
    {
        long BillPtr { get; set; }
        long CheckPtr { get; set; }
        bool Costed { get; set; }
        double CostQuantity { get; set; }
        byte CostStatus { get; set; }
        string Country { get; set; }
        short CurrencyCode { get; set; }
        DateTime Date { get; set; }
        short Department { get; set; }
        string Description { get; set; }
        double Duty { get; set; }
        DateTime ExpiryDate { get; set; }
        double Foreign { get; set; }
        double Freight { get; set; }
        long HeaderPtr { get; set; }
        long LocationPtr { get; set; }
        string Number { get; set; }
        long PartPtr { get; set; }
        long PickPriority { get; set; }
        double PickQuantity { get; set; }
        float PriceQty { get; set; }
        long ProductPtr { get; set; }
        long Proximity { get; set; }
        byte StockStatus { get; set; }
        long StUnique { get; set; }
        long SupplierPtr { get; set; }
        long TrandataPtr { get; set; }
        double Weight { get; set; }
        double WholeExtra { get; set; }
        double Wholesale { get; set; }
    }

}
