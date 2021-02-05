#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class AltSuply_Clean : IAltSuply
    {
        private IAltSuply _AltSuply;

        public AltSuply_Clean(IAltSuply altSuply)
        {
            _AltSuply = altSuply ?? new AltSuply();
            
            // Perform null string checks
            _AltSuply.PackSize = _AltSuply.PackSize ?? string.Empty;
            _AltSuply.PartNumber = _AltSuply.PartNumber ?? string.Empty;
            _AltSuply.Preferred = _AltSuply.Preferred ?? string.Empty;

            ((IAltSuply)this).CopyFrom(altSuply); // performs clean through properties being set during copy from
        }
        public void Clean()
        {
            Clean_PartNumber();
        }

        public void Clean_PartNumber()
        {
            _AltSuply.PartNumber = (_AltSuply.PartNumber ?? string.Empty).TrimEnd();
        }

        public string PartNumber_Raw { get { return (_AltSuply.PartNumber ?? string.Empty); } }

        public string Barcode => PartNumber;

        public string PartNumber
        {
            get {return PartNumber_Raw.TrimEnd(); } // don't clean, just return cleaned
            set { _AltSuply.PartNumber = value; Clean_PartNumber(); }
        }

        public int AUnique { get => _AltSuply.AUnique; set => _AltSuply.AUnique = value; }
        public double Duty { get => _AltSuply.Duty; set => _AltSuply.Duty = value; }
        public double Extra { get => _AltSuply.Extra; set => _AltSuply.Extra = value; }
        public short FileNo { get => _AltSuply.FileNo; set => _AltSuply.FileNo = value; }
        public double Freight { get => _AltSuply.Freight; set => _AltSuply.Freight = value; }
        public string PackSize { get => _AltSuply.PackSize ?? string.Empty; set => _AltSuply.PackSize = value ?? string.Empty; }
        public int Part { get => _AltSuply.Part; set => _AltSuply.Part = value; }
        public string Preferred { get => _AltSuply.Preferred ?? string.Empty; set => _AltSuply.Preferred = value ?? string.Empty; }
        public double Price { get => _AltSuply.Price; set => _AltSuply.Price = value; }
        public int RecordNo { get => _AltSuply.RecordNo; set => _AltSuply.RecordNo = value; }
    }
}
