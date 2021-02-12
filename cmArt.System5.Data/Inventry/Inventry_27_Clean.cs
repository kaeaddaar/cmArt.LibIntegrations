using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable


namespace cmArt.System5.Data
{
    public class Inventry_27_Clean : IInventry_27
    {
        private IInventry_27 _Inventry_27;

        public Inventry_27_Clean(IInventry_27 component)
        {
            _Inventry_27 = component ?? new Inventry_27();

            _Inventry_27.Part = _Inventry_27.Part ?? string.Empty;
            ((IInventry_27)this).CopyFrom(component);
        }

        public void Clean()
        {
            Clean_Part();
        }
        public void Clean_Part()
        {
            _Inventry_27.Part = _Inventry_27.Part.TrimOption();
        }

        public string PartIndex
        {
            get { return _Inventry_27.Part; } // for performance, may not be clean
        }

        public string Part_Raw { get { return _Inventry_27.Part ?? string.Empty; } }
        public string Part
        {
            get { return _Inventry_27.Part.TrimOption(); }
            set { _Inventry_27.Part = value.TrimOption(); }
        }

        public float Brand { get => _Inventry_27.Brand; set => _Inventry_27.Brand = value; }
        public string Cat { get => _Inventry_27.Cat ?? string.Empty; set => _Inventry_27.Cat = value ?? string.Empty; }
        public string Country { get => _Inventry_27.Country ?? string.Empty; set => _Inventry_27.Country = value ?? string.Empty; }
        public string Description { get => _Inventry_27.Description ?? string.Empty; set => _Inventry_27.Description = value ?? string.Empty; }
        public string Description2 { get => _Inventry_27.Description2 ?? string.Empty; set => _Inventry_27.Description2 = value ?? string.Empty; }
        public double Duty_1 { get => _Inventry_27.Duty_1; set => _Inventry_27.Duty_1 = value; }
        public string Ecommerce { get => _Inventry_27.Ecommerce ?? string.Empty; set => _Inventry_27.Ecommerce = value ?? string.Empty; }
        public double Foreign_1 { get => _Inventry_27.Foreign_1; set => _Inventry_27.Foreign_1 = value; }
        public double Freight_1 { get => _Inventry_27.Freight_1; set => _Inventry_27.Freight_1 = value; }
        public int InvUnique { get => _Inventry_27.InvUnique; set => _Inventry_27.InvUnique = value; }
        public string Item { get => _Inventry_27.Item ?? string.Empty; set => _Inventry_27.Item = value ?? string.Empty; }
        public byte KitType { get => _Inventry_27.KitType; set => _Inventry_27.KitType = value; }
        public string Location { get => _Inventry_27.Location ?? string.Empty; set => _Inventry_27.Location = value ?? string.Empty; }
        public string MarkDeleted { get => _Inventry_27.MarkDeleted ?? string.Empty; set => _Inventry_27.MarkDeleted = value ?? string.Empty; }
        public string PackSize { get => _Inventry_27.PackSize ?? string.Empty; set => _Inventry_27.PackSize = value ?? string.Empty; }
        public float PriceQty { get => _Inventry_27.PriceQty; set => _Inventry_27.PriceQty = value; }
        public string Serial { get => _Inventry_27.Serial ?? string.Empty; set => _Inventry_27.Serial = value ?? string.Empty; }
        public string Size_1 { get => _Inventry_27.Size_1 ?? string.Empty; set => _Inventry_27.Size_1 = value ?? string.Empty; }
        public string Size_2 { get => _Inventry_27.Size_2 ?? string.Empty; set => _Inventry_27.Size_2 = value ?? string.Empty; }
        public string Size_3 { get => _Inventry_27.Size_3 ?? string.Empty; set => _Inventry_27.Size_3 = value ?? string.Empty; }
        public float Supplier { get => _Inventry_27.Supplier; set => _Inventry_27.Supplier = value; }
        public string SuppPart { get => _Inventry_27.SuppPart ?? string.Empty; set => _Inventry_27.SuppPart = value ?? string.Empty; }
        public short Tax { get => _Inventry_27.Tax; set => _Inventry_27.Tax = value; }
        public string Units { get => _Inventry_27.Units ?? string.Empty; set => _Inventry_27.Units = value ?? string.Empty; }
        public float Weight { get => _Inventry_27.Weight; set => _Inventry_27.Weight = value; }
        public double WholeExtra_1 { get => _Inventry_27.WholeExtra_1; set => _Inventry_27.WholeExtra_1 = value; }
        public double Wholesale_1 { get => _Inventry_27.Wholesale_1; set => _Inventry_27.Wholesale_1 = value; }
    }

}
