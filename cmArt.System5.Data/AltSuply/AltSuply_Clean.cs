﻿using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.System5.Data
{
    public class AltSuply_Clean : IAltSuply
    {
        private AltSuply _AltSuply;

        public AltSuply_Clean(AltSuply altSuply)
        {
            _AltSuply = altSuply ?? new AltSuply();
            ((IAltSuply)_AltSuply).CopyFrom(altSuply);

            Clean(); // for advanced scenarios
        }
        public void Clean()
        {
            Clean_PartNumber();
        }

        public void Clean_PartNumber()
        {
            _AltSuply.PartNumber = _AltSuply.PartNumber.TrimEnd();
        }

        public string PartNumber_Raw
        {
            get { return _AltSuply.PartNumber; } // for performance, may not be clean
        }

        public string Barcode => PartNumber;

        public string PartNumber
        {
            get {return _AltSuply.PartNumber.TrimEnd(); } // don't clean, just return cleaned
            set { _AltSuply.PartNumber = value; Clean_PartNumber(); }
        }

        public int AUnique { get => _AltSuply.AUnique; set => _AltSuply.AUnique = value; }
        public double Duty { get => _AltSuply.Duty; set => _AltSuply.Duty = value; }
        public double Extra { get => _AltSuply.Extra; set => _AltSuply.Extra = value; }
        public short FileNo { get => _AltSuply.FileNo; set => _AltSuply.FileNo = value; }
        public double Freight { get => _AltSuply.Freight; set => _AltSuply.Freight = value; }
        public string PackSize { get => _AltSuply.PackSize; set => _AltSuply.PackSize = value; }
        public int Part { get => _AltSuply.Part; set => _AltSuply.Part = value; }
        public string Preferred { get => _AltSuply.Preferred; set => _AltSuply.Preferred = value; }
        public double Price { get => _AltSuply.Price; set => _AltSuply.Price = value; }
        public int RecordNo { get => _AltSuply.RecordNo; set => _AltSuply.RecordNo = value; }
    }
}
