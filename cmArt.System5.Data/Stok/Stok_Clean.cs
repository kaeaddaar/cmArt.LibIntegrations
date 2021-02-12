using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable


namespace cmArt.System5.Data
{
    public class Stok_Clean : IStok
    {
        private IStok _Stok;

        public Stok_Clean(IStok component)
        {
            _Stok = component ?? new Stok();

            _Stok.Number = _Stok.Number ?? string.Empty;
            ((IStok)this).CopyFrom(component);
        }

        public void Clean()
        {
            Clean_Number();
        }
        public void Clean_Number()
        {
            _Stok.Number = _Stok.Number.TrimOption();
        }

        public string NumberIndex
        {
            get { return _Stok.Number; } // for performance, may not be clean
        }

        public string Part_Raw { get { return _Stok.Number ?? string.Empty; } }
        public string Number
        {
            get { return _Stok.Number.TrimOption(); }
            set { _Stok.Number = value.TrimOption(); }
        }

        public long BillPtr { get => _Stok.BillPtr; set => _Stok.BillPtr = value; }
        public long CheckPtr { get => _Stok.CheckPtr; set => _Stok.CheckPtr = value; }
        public bool Costed { get => _Stok.Costed; set => _Stok.Costed = value; }
        public double CostQuantity { get => _Stok.CostQuantity; set => _Stok.CostQuantity = value; }
        public byte CostStatus { get => _Stok.CostStatus; set => _Stok.CostStatus = value; }
        public string Country { get => _Stok.Country ?? string.Empty; set => _Stok.Country = value ?? string.Empty; }
        public short CurrencyCode { get => _Stok.CurrencyCode; set => _Stok.CurrencyCode = value; }
        public DateTime Date { get => _Stok.Date; set => _Stok.Date = value; }
        public short Department { get => _Stok.Department; set => _Stok.Department = value; }
        public string Description { get => _Stok.Description ?? string.Empty; set => _Stok.Description = value ?? string.Empty; }
        public double Duty { get => _Stok.Duty; set => _Stok.Duty = value; }
        public DateTime ExpiryDate { get => _Stok.ExpiryDate; set => _Stok.ExpiryDate = value; }
        public double Foreign { get => _Stok.Foreign; set => _Stok.Foreign = value; }
        public double Freight { get => _Stok.Freight; set => _Stok.Freight = value; }
        public long HeaderPtr { get => _Stok.HeaderPtr; set => _Stok.HeaderPtr = value; }
        public long LocationPtr { get => _Stok.LocationPtr; set => _Stok.LocationPtr = value; }
        public long PartPtr { get => _Stok.PartPtr; set => _Stok.PartPtr = value; }
        public long PickPriority { get => _Stok.PickPriority; set => _Stok.PickPriority = value; }
        public double PickQuantity { get => _Stok.PickQuantity; set => _Stok.PickQuantity = value; }
        public float PriceQty { get => _Stok.PriceQty; set => _Stok.PriceQty = value; }
        public long ProductPtr { get => _Stok.ProductPtr; set => _Stok.ProductPtr = value; }
        public long Proximity { get => _Stok.Proximity; set => _Stok.Proximity = value; }
        public byte StockStatus { get => _Stok.StockStatus; set => _Stok.StockStatus = value; }
        public long StUnique { get => _Stok.StUnique; set => _Stok.StUnique = value; }
        public long SupplierPtr { get => _Stok.SupplierPtr; set => _Stok.SupplierPtr = value; }
        public long TrandataPtr { get => _Stok.TrandataPtr; set => _Stok.TrandataPtr = value; }
        public double Weight { get => _Stok.Weight; set => _Stok.Weight = value; }
        public double WholeExtra { get => _Stok.WholeExtra; set => _Stok.WholeExtra = value; }
        public double Wholesale { get => _Stok.Wholesale; set => _Stok.Wholesale = value; }
    }

}
